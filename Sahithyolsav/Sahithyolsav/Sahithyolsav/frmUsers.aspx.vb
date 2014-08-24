Imports ConnectionLib
Imports System.Data.Sql
Public Class frmUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        '  SetComboVisiblitis()
        If Not IsPostBack Then
            bindGrid()
            fillLevelMapIdCombo()
            Fillcombos()
        End If
    End Sub
    Private Sub Fillcombos()
        FillLevel()
        ddlDistrict.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlDiviosn.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub FillLevel()
        Dim dtLevel As New DataTable
        dtLevel = Level.getAllLevel()
        ddlLevel.DataSource = dtLevel
        ddlLevel.DataTextField = "vchLevelName"
        ddlLevel.DataValueField = "intLevelID"
        ddlLevel.DataBind()
        ddlLevel.Items.Insert(0, New ListItem("----Select----", "0"))

    End Sub
    Private Function getUserData() As DataTable
        Dim dt As New DataTable
        If UserManagement.UserTypeId = 99 Then
            dt = ConnectionLib.UserManagement.getAllUsers()
        Else
            dt = ConnectionLib.UserManagement.getMyUsers(UserManagement.UserID)
        End If
        Return dt
    End Function
    Private Sub bindGrid()
        gvuserdetails.DataSource = getUserData()
        gvuserdetails.DataBind()
    End Sub
    Protected Sub imgChangStatus_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        clearFields()

        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim status As String
        Dim Updated As Boolean
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        lblUserID.Value = gvuserdetails.DataKeys(gvrow.RowIndex).Value.ToString()
        status = gvrow.Cells(6).Text
        Updated = UserManagement.UpdateUserStatus(lblUserID.Value, status)
        If Updated = True Then
            bindGrid()
        End If
    End Sub
    Protected Sub imgbtnMapUser_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        clearFields()

        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

        lblUserID.Value = gvuserdetails.DataKeys(gvrow.RowIndex).Value.ToString()
        If ddlLevel.SelectedIndex = 0 Or ddlLevel.SelectedIndex = 1 Then
            trDistrict.Visible = False
            trDivision.Visible = False
            trSector.Visible = False
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 117px;")
        End If
        Me.ModalMapUser.Show()

    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        clearFields()
        btnUpdate.Visible = True
        btnSave.Visible = False
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        txtUserName.Text = gvrow.Cells(1).Text
        txtUserName.Enabled = False
        txtfname.Text = gvrow.Cells(2).Text
        txtlname.Text = gvrow.Cells(3).Text
        lblId.Value = gvuserdetails.DataKeys(gvrow.RowIndex).Value.ToString()
        Me.UserModal.Show()
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        UserManagement.DeleteUser(Convert.ToInt32(gvuserdetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        If UserManagement.validateUserName(txtUserName.Text) = False Then
            lblmsg.Text = "User Name already exists."
            lblmsg.ForeColor = Drawing.Color.Red
            Me.UserModal.Show()
            Return
        End If
        arrIn.Add(0)
        arrIn.Add(txtUserName.Text)
        arrIn.Add("123") ' Default Password 123
        arrIn.Add(txtfname.Text)
        arrIn.Add(txtlname.Text)
        If ConnectionLib.UserManagement.UserTypeId = 99 Then
            arrIn.Add(1)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 1 Then
            arrIn.Add(2)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 2 Then
            arrIn.Add(3)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 3 Then
            arrIn.Add(4)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 4 Then
            arrIn.Add(5)
        End If
        arrIn.Add(True)
        arrIn.Add(ddlLevelMapIdCombo.SelectedValue)
        arrIn.Add(UserManagement.UserMapId)
        arrIn.Add(UserManagement.UserID)
        If ConnectionLib.UserManagement.SaveUser(arrIn) = True Then
            clearFields()
            bindGrid()
            UserModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If

    End Sub
    Private Sub clearFields()
        txtUserName.Text = ""
        txtfname.Text = ""
        txtlname.Text = ""
        lblmsg.Text = ""
        LblMapmsg.Text = ""
        txtUserName.Enabled = True
        clearUserMapControls()
    End Sub
    Private Sub clearUserMapControls()
        ddlLevel.SelectedIndex = 0
        trDistrict.Visible = False
        trDivision.Visible = False
        trSector.Visible = False
        trUnit.Visible = False
        ddlDistrict.Items.Clear()
        ddlDistrict.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlDiviosn.Items.Clear()
        ddlDiviosn.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlSector.Items.Clear()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlUnit.Items.Clear()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddUser.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.UserModal.Show()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList

        arrIn.Add(Convert.ToInt32(lblId.Value.ToString))
        arrIn.Add(txtUserName.Text)
        arrIn.Add("123") ' Default Password 123
        arrIn.Add(txtfname.Text)
        arrIn.Add(txtlname.Text)
        If ConnectionLib.UserManagement.UserTypeId = 99 Then
            arrIn.Add(1)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 1 Then
            arrIn.Add(2)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 2 Then
            arrIn.Add(3)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 3 Then
            arrIn.Add(4)
        ElseIf ConnectionLib.UserManagement.UserTypeId = 4 Then
            arrIn.Add(5)
        End If
        arrIn.Add(True)
        arrIn.Add(ddlLevelMapIdCombo.SelectedValue)
        arrIn.Add(UserManagement.UserMapId)
        arrIn.Add(UserManagement.UserID)
        If ConnectionLib.UserManagement.SaveUser(arrIn) = True Then
            clearFields()
            bindGrid()
            UserModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        ' SetComboVisiblitis()
        If ddlLevel.SelectedIndex = 2 Then
            Filldistrict()
            trDistrict.Visible = True
            trDivision.Visible = False
            trSector.Visible = False
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 149px;")
        ElseIf ddlLevel.SelectedIndex = 3 Then
            Filldistrict()

            trDistrict.Visible = True
            trDivision.Visible = True
            trSector.Visible = False
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 182px;")
        ElseIf ddlLevel.SelectedIndex = 4 Then
            Filldistrict()
            trDistrict.Visible = True
            trDivision.Visible = True
            trSector.Visible = True
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 216px;")
        ElseIf ddlLevel.SelectedIndex = 5 Then
            Filldistrict()
            trDistrict.Visible = True
            trDivision.Visible = True
            trSector.Visible = True
            trUnit.Visible = True
            PnlPopupUserMap.Attributes.Add("style", "height: 247px;")
        ElseIf ddlLevel.SelectedIndex = 0 Or ddlLevel.SelectedIndex = 1 Then
            Filldistrict()
            trDistrict.Visible = False
            trDivision.Visible = False
            trSector.Visible = False
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 117px;")
        End If

        ddlDiviosn.Items.Clear()
        ddlDiviosn.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlSector.Items.Clear()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlUnit.Items.Clear()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
        ModalMapUser.Show()
    End Sub
    Private Sub Filldistrict()
        Dim dt As New DataTable
        dt = District.GetDistricts("", "", 0).Tables(0)
        ddlDistrict.DataSource = dt
        ddlDistrict.DataTextField = "vchDistrictName"
        ddlDistrict.DataValueField = "intDistrictID"
        ddlDistrict.DataBind()
        ddlDistrict.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub SetComboVisiblitis()
        If ddlLevel.SelectedIndex = 0 Or ddlLevel.SelectedIndex = 1 Then
            trDistrict.Visible = False
            trDivision.Visible = False
            trSector.Visible = False
            trUnit.Visible = False
            PnlPopupUserMap.Attributes.Add("style", "height: 103px;")
        Else
            trDistrict.Visible = True
            trDivision.Visible = True
            trSector.Visible = True
            trUnit.Visible = True
            PnlPopupUserMap.Attributes.Add("style", "height: 269px;")

        End If
    End Sub

    Private Sub ddlDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDistrict.SelectedIndexChanged
        ddlDiviosn.Items.Clear()
        ddlDiviosn.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlSector.Items.Clear()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlUnit.Items.Clear()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
        Filldivision()
        ModalMapUser.Show()
    End Sub
    Private Sub Filldivision()
        Dim dt As New DataTable
        If ddlDistrict.SelectedIndex = 0 Then
            dt = Division.getAllDivisions(0)
        Else
            dt = Division.getAllDivisions(ddlDistrict.SelectedValue)
        End If
        ddlDiviosn.DataSource = dt
        ddlDiviosn.DataTextField = "vchDivisionName"
        ddlDiviosn.DataValueField = "intDivisionId"
        ddlDiviosn.DataBind()
        ddlDiviosn.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub

    Private Sub ddlDiviosn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDiviosn.SelectedIndexChanged

        ddlSector.Items.Clear()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlUnit.Items.Clear()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
        fillSector()
        ModalMapUser.Show()
    End Sub
    Private Sub fillSector()
        Dim dt As New DataTable
        If ddlDiviosn.SelectedIndex = 0 Then
            dt = Sector.getAllSector()
        Else
            dt = Sector.getAllSector(ddlDiviosn.SelectedValue)
        End If

        ddlSector.DataSource = dt
        ddlSector.DataTextField = "vchSectorName"
        ddlSector.DataValueField = "intSectorId"
        ddlSector.DataBind()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))

    End Sub

    Private Sub ddlSector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSector.SelectedIndexChanged
        ddlUnit.Items.Clear()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))
        fillUnit()
        ModalMapUser.Show()
    End Sub
    Private Sub fillUnit()
        Dim dt As New DataTable
        If ddlSector.SelectedIndex = 0 Then
            dt = Unit.getAllUnit()
        Else
            dt = Unit.getAllUnitBySearchId("", 0, 0, ddlSector.SelectedValue)
        End If

        ddlUnit.DataSource = dt
        ddlUnit.DataTextField = "vchUnitName"
        ddlUnit.DataValueField = "intUnitId"
        ddlUnit.DataBind()
        ddlUnit.Items.Insert(0, New ListItem("----Select----", "0"))

    End Sub

    Private Sub ddlUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnit.SelectedIndexChanged
        ModalMapUser.Show()
    End Sub

    Protected Sub ImageSaveMapUser_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageSaveMapUser.Click
        If ValidateUserMap() = "Success" Then
            Dim arrin As New ArrayList
            Try
                arrin.Add(0)
                arrin.Add(lblUserID.Value)
                arrin.Add(ddlLevel.SelectedValue)
                If ddlLevel.SelectedIndex = 1 Then
                    arrin.Add(99)
                ElseIf ddlLevel.SelectedIndex = 2 Then
                    arrin.Add(ddlDistrict.SelectedValue)
                ElseIf ddlLevel.SelectedIndex = 3 Then
                    arrin.Add(ddlDiviosn.SelectedValue)
                ElseIf ddlLevel.SelectedIndex = 4 Then
                    arrin.Add(ddlSector.SelectedValue)
                ElseIf ddlLevel.SelectedIndex = 5 Then
                    arrin.Add(ddlUnit.SelectedValue)
                End If
                arrin.Add(True)
                If ConnectionLib.UserManagement.MapUser(arrin) = True Then
                    clearFields()
                    bindGrid()
                    ModalMapUser.Hide()
                Else
                    lblmsg.Text = "Saving Failed.Try again later"
                    lblmsg.ForeColor = Drawing.Color.Red
                End If
            Catch ex As Exception

            End Try
        Else
            LblMapmsg.Text = ValidateUserMap()
            LblMapmsg.ForeColor = Drawing.Color.Red
            Me.ModalMapUser.Show()
            Return
        End If
    End Sub
    Private Function ValidateUserMap() As String
        Dim ValMsg As String = ""
        If ddlLevel.SelectedIndex = 1 Then
            ValMsg = "Success"
        ElseIf ddlLevel.SelectedIndex = 2 Then
            If ddlDistrict.SelectedIndex = 0 Then
                ValMsg = "Please Select District"
                Return ValMsg
                Exit Function
            Else
                ValMsg = "Success"
            End If
        ElseIf ddlLevel.SelectedIndex = 3 Then
            If ddlDiviosn.SelectedIndex = 0 Then
                ValMsg = "Please Select Division"
                Return ValMsg
                Exit Function
            Else
                ValMsg = "Success"
            End If
        ElseIf ddlLevel.SelectedIndex = 4 Then
            If ddlSector.SelectedIndex = 0 Then
                ValMsg = "Please Select Sector"
                Return ValMsg
                Exit Function
            Else
                ValMsg = "Success"
            End If
        ElseIf ddlLevel.SelectedIndex = 5 Then
            If ddlUnit.SelectedIndex = 0 Then
                ValMsg = "Please Select Unit"
                Return ValMsg
                Exit Function
            Else
                ValMsg = "Success"
            End If
        ElseIf ddlLevel.SelectedIndex = 0 Then
            ValMsg = "Please Select Level"
        End If
        Return ValMsg
    End Function
    Private Sub fillLevelMapIdCombo()
        Dim dt As New DataTable
        If ConnectionLib.UserManagement.UserTypeId = 99 Then
            ddlLevelMapIdCombo.DataSource = dt
            ddlLevelMapIdCombo.DataBind()
            ddlLevelMapIdCombo.Items.Insert(0, New ListItem("State", "1"))
        ElseIf ConnectionLib.UserManagement.UserTypeId = 1 Then
            dt = District.GetDistricts("", "", 0).Tables(0)
            ddlLevelMapIdCombo.DataSource = dt
            ddlLevelMapIdCombo.DataTextField = "vchDistrictName"
            ddlLevelMapIdCombo.DataValueField = "intDistrictID"
            ddlLevelMapIdCombo.DataBind()
            ddlLevelMapIdCombo.Items.Insert(0, New ListItem("----Select----", "0"))
        ElseIf ConnectionLib.UserManagement.UserTypeId = 2 Then
            dt = Division.GetDivision(UserManagement.UserMapId, "", "")
            ddlLevelMapIdCombo.DataSource = dt
            ddlLevelMapIdCombo.DataTextField = "vchDivisionName"
            ddlLevelMapIdCombo.DataValueField = "intDivisionId"
            ddlLevelMapIdCombo.DataBind()
            ddlLevelMapIdCombo.Items.Insert(0, New ListItem("----Select----", "0"))
        ElseIf ConnectionLib.UserManagement.UserTypeId = 3 Then
            dt = Sector.getAllSector(UserManagement.UserMapId)
            ddlLevelMapIdCombo.DataSource = dt
            ddlLevelMapIdCombo.DataTextField = "vchSectorName"
            ddlLevelMapIdCombo.DataValueField = "intSectorId"
            ddlLevelMapIdCombo.DataBind()
            ddlLevelMapIdCombo.Items.Insert(0, New ListItem("----Select----", "0"))
        ElseIf ConnectionLib.UserManagement.UserTypeId = 4 Then
            dt = Unit.getAllUnitBySearchId("", 0, 0, UserManagement.UserMapId)
            ddlLevelMapIdCombo.DataSource = dt
            ddlLevelMapIdCombo.DataTextField = "vchUnitName"
            ddlLevelMapIdCombo.DataValueField = "intUnitId"
            ddlLevelMapIdCombo.DataBind()
            ddlLevelMapIdCombo.Items.Insert(0, New ListItem("----Select----", "0"))
        End If
    End Sub

End Class