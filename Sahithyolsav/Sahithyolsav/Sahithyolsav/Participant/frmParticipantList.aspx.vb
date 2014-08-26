Imports ConnectionLib
Imports System.Data.Sql
Public Class frmParticipantList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillLevels()
            '  SetComboBox()
            fillSection()
            manageAddBuuton()
            fillType()
        End If
    End Sub
    Private Sub fillType()
        Dim dt As New DataTable
        ddlType.Items.Insert(0, New ListItem("Single Item", "0"))
        ddlType.Items.Insert(1, New ListItem("Group Item", "1"))
    End Sub

    Private Sub manageAddBuuton()
        If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
            btnParticipant.Visible = True
            btnUpdate.Enabled = True
        Else
            If UserManagement.UserTypeId = 5 Then
                btnParticipant.Visible = True
                btnUpdate.Enabled = True
            Else
                btnParticipant.Visible = False
                btnUpdate.Enabled = False
            End If
        End If
    End Sub
    Private Sub bindHeader()
        participantHeader.Text = "Paricipant List in " & ddlPartcipantLevelIdCombo1.SelectedItem.ToString
    End Sub
    Private Sub fillLevels()
        Dim dt As New DataTable
        dt = Level.getAllLevelforSerMapID(UserManagement.UserTypeId)
        ddlPartcipantLevelIdCombo1.DataSource = dt
        ddlPartcipantLevelIdCombo1.DataTextField = "vchLevelName"
        ddlPartcipantLevelIdCombo1.DataValueField = "intLevelID"
        ddlPartcipantLevelIdCombo1.DataBind()
        ddlPartcipantLevelIdCombo1.Items.Insert(0, New ListItem("Please Select", "0"))
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim dt As New DataTable
        Dim dtGrp As New DataTable
        Dim participantListId As Integer
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        clearFields()
        loadPopup()
        btnSave.Visible = False
        btnUpdate.Visible = True
        participantListId = Convert.ToInt32(gvParticipantdetails.DataKeys(gvrow.RowIndex).Value.ToString())
        dt = Participant.getParticipantListById(participantListId)
        If dt.Rows.Count > 0 Then

            '----------
            hparticpantLitsId.Value = dt.Rows(0).ItemArray(0)
            hparticpantId.Value = dt.Rows(0).ItemArray(13)
            txtParticipant.Text = dt.Rows(0).ItemArray(12)
            ddlPartcipantLevelIdComboPopUp.SelectedValue = dt.Rows(0).ItemArray(8)
            ddlSection.SelectedValue = dt.Rows(0).ItemArray(3)
            txtChessNo.Text = dt.Rows(0).ItemArray(2)

            '----------
            If dt.Rows(0).ItemArray(14).ToString = "True" Then
                ddlType.SelectedIndex = 1
                rowGrpParticipant.Visible = True
                dtGrp = Participant.getParticipantGroupByID(Convert.ToInt32(hparticpantLitsId.Value.ToString))
                Dim grpParticipant As String = ""
                For value As Integer = 0 To dtGrp.Rows.Count - 1
                    If value = 0 Then
                        grpParticipant = grpParticipant + dtGrp.Rows(value).ItemArray(2).ToString
                    Else
                        grpParticipant = grpParticipant + "," + dtGrp.Rows(value).ItemArray(2).ToString
                    End If

                Next
                txtGroupParticiapnt.Text = grpParticipant
            Else
                ddlType.SelectedIndex = 0
                rowGrpParticipant.Visible = False
            End If
            ddlType.Enabled = False
            '----------

            bindItemList()

            Dim itemlist As DataTable
            itemlist = Participant.getItemListByParticipantId(participantListId)
            For value As Integer = 0 To itemlist.Rows.Count - 1
                For Each item As ListItem In chkItemList.Items
                    If item.Value.ToString = itemlist.Rows(value).ItemArray(2).ToString Then
                        If itemlist.Rows(value).ItemArray(3).ToString = "Yes" Then
                            item.Selected = True
                        Else
                            item.Selected = False
                        End If
                    End If

                Next
            Next
        End If
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        Dim participantListId As Integer
        participantListId = Convert.ToInt32(gvParticipantdetails.DataKeys(gvrow.RowIndex).Value.ToString())
        Participant.DeleteParticipant(participantListId)
        Participant.DeleteParticipantGroup(participantListId)
        bindGrid()
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo1.SelectedIndexChanged
        manageAddBuuton()
        bindGrid()
        If ddlPartcipantLevelIdCombo1.SelectedValue <> "0" Then
            bindHeader()
        End If
    End Sub
 
    Protected Sub btnParticipant_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnParticipant.Click
        clearFields()
        btnSave.Visible = True
        btnUpdate.Visible = False
        loadPopup()
    End Sub
    Private Sub loadPopup()
        ddlType.SelectedIndex = 0
        rowGrpParticipant.Visible = False
        If ddlPartcipantLevelIdCombo1.SelectedValue = "0" Then
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "1" Then
            Dim dt As New DataTable
            dt = District.GetDistricts("", "", 0).Tables(0)
            ddlPartcipantLevelIdComboPopUp.DataSource = dt
            ddlPartcipantLevelIdComboPopUp.DataTextField = "vchDistrictName"
            ddlPartcipantLevelIdComboPopUp.DataValueField = "intDistrictID"
            ddlPartcipantLevelIdComboPopUp.DataBind()
            ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                ddlPartcipantLevelIdComboPopUp.SelectedValue = UserManagement.UserMapId.ToString
                ddlPartcipantLevelIdComboPopUp.Enabled = False
                txtChessNo.Enabled = False
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "2" Then
            Dim dt As New DataTable

            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                dt = Division.GetDivision(0, "", "", Convert.ToInt32(UserManagement.UserMapId))
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchDivisionName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intDivisionId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                ddlPartcipantLevelIdComboPopUp.SelectedValue = UserManagement.UserMapId.ToString
                ddlPartcipantLevelIdComboPopUp.Enabled = False
                txtChessNo.Enabled = False
            Else
                dt = Division.GetDivision(UserManagement.UserMapId, "", "", 0)
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchDivisionName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intDivisionId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                ddlPartcipantLevelIdComboPopUp.Enabled = True
                txtChessNo.Enabled = True
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "3" Then
            Dim dt As New DataTable
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                dt = Sector.getAllSectorById(UserManagement.UserMapId)
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchSectorName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intSectorId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                ddlPartcipantLevelIdComboPopUp.SelectedValue = UserManagement.UserMapId.ToString
                ddlPartcipantLevelIdComboPopUp.Enabled = False
                txtChessNo.Enabled = False
            Else
                dt = Sector.getAllSectorBySearchId("", 0, UserManagement.UserMapId)
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchSectorName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intSectorId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                txtChessNo.Enabled = True
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "4" Then
            Dim dt As New DataTable
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                dt = Unit.getAllUnitById(UserManagement.UserMapId)
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchUnitName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intUnitId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                ddlPartcipantLevelIdComboPopUp.SelectedValue = UserManagement.UserMapId.ToString
                ddlPartcipantLevelIdComboPopUp.Enabled = False
                txtChessNo.Enabled = False
            Else
                dt = Unit.getAllUnitBySearchId("", 0, 0, UserManagement.UserMapId)
                ddlPartcipantLevelIdComboPopUp.DataSource = dt
                ddlPartcipantLevelIdComboPopUp.DataTextField = "vchUnitName"
                ddlPartcipantLevelIdComboPopUp.DataValueField = "intUnitId"
                ddlPartcipantLevelIdComboPopUp.DataBind()
                ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
                txtChessNo.Enabled = True
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "5" Then
            Dim dt As New DataTable
            dt = Unit.getAllUnitById(UserManagement.UserMapId)
            ddlPartcipantLevelIdComboPopUp.DataSource = dt
            ddlPartcipantLevelIdComboPopUp.DataTextField = "vchUnitName"
            ddlPartcipantLevelIdComboPopUp.DataValueField = "intUnitId"
            ddlPartcipantLevelIdComboPopUp.DataBind()
            ddlPartcipantLevelIdComboPopUp.Items.Insert(0, New ListItem("----Select----", "0"))
            ddlPartcipantLevelIdComboPopUp.SelectedValue = UserManagement.UserMapId.ToString
            ddlPartcipantLevelIdComboPopUp.Enabled = False
            txtChessNo.Enabled = True
        End If
        participantModal.Show()
    End Sub
    Private Sub clearFields()
        txtParticipant.Text = ""
        txtChessNo.Text = ""
        lblId.Value = ""
        txtChessNo.Enabled = True
        lblmsg.Text = ""
        ddlPartcipantLevelIdComboPopUp.Enabled = True
        fillSection()
        Dim dt As New DataTable
        chkItemList.DataSource = dt
        chkItemList.DataBind()
        For Each item As ListItem In chkItemList.Items
            item.Selected = False
        Next
        txtGroupParticiapnt.Text = ""
        ddlType.Enabled = True
    End Sub

    Private Sub bindGrid()
        pnlContent.Visible = True
        Dim dt As New DataTable
        If UserManagement.UserTypeId = 1 Then
            dt = Participant.getParticipantByLevelId(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue.ToString))
        Else
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                dt = Participant.getParticipantByLevelId(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue.ToString), UserManagement.UserHigherMapId, UserManagement.UserID)
            Else
                dt = Participant.getParticipantByLevelId(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue.ToString), UserManagement.UserMapId)
            End If
        End If

        gvParticipantdetails.DataSource = dt
        gvParticipantdetails.DataBind()
    End Sub
    Private Sub fillSection()
        Dim dt As New DataTable
        dt = ConnectionLib.Section.getAllSections()
        ddlSection.DataSource = dt
        ddlSection.DataTextField = "vchSectionName"
        ddlSection.DataValueField = "intSectionID"
        ddlSection.DataBind()
        ddlSection.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub


    Protected Sub ddlSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSection.SelectedIndexChanged
        participantModal.Show()
        bindItemList()
        If ddlSection.SelectedItem.ToString = "campus".ToLower Then
            campusname.Visible = True
            ' campuscourse.Visible = True
        Else
            campusname.Visible = False
            '  campuscourse.Visible = False
        End If
    End Sub
    Private Sub bindItemList()
        Dim dt As New DataTable
        If (ddlSection.SelectedValue <> "0") Then
            If ddlType.SelectedValue = "1" Then
                dt = ConnectionLib.item.getGroupItemsBySectionId(Convert.ToInt32(ddlSection.SelectedValue.ToString()))
            Else
                dt = ConnectionLib.item.getItemsBySectionId(Convert.ToInt32(ddlSection.SelectedValue.ToString()))
            End If

            chkItemList.DataSource = dt
            chkItemList.DataTextField = "vchItemName"
            chkItemList.DataValueField = "intItemId"
            chkItemList.DataBind()
        Else
            chkItemList.DataSource = dt
            chkItemList.DataBind()
        End If
    End Sub

    Private Function validateItem()
        If ddlPartcipantLevelIdCombo1.SelectedValue <> "5" Then
            For Each item As ListItem In chkItemList.Items
                If item.Selected = True Then
                    If Participant.ValidateItem(Convert.ToInt32(ddlPartcipantLevelIdComboPopUp.SelectedValue.ToString), Convert.ToInt32(item.Value.ToString()), 0) = True Then
                        lblmsg.Visible = True
                        lblmsg.Text = "Already One Participant is Partciapting from  " & ddlPartcipantLevelIdComboPopUp.SelectedItem.ToString() & " for item " & item.Text & ". Please delselect this item"
                        lblmsg.ForeColor = Drawing.Color.Red
                        Return True
                    End If
                End If
            Next
        End If
        Return False
        lblmsg.Visible = False
    End Function
    Private Function validateItemUpdate(ByVal participantID As Integer)
        If ddlPartcipantLevelIdCombo1.SelectedValue <> "5" Then
            For Each item As ListItem In chkItemList.Items
                If item.Selected = True Then
                    If Participant.ValidateItem(Convert.ToInt32(ddlPartcipantLevelIdComboPopUp.SelectedValue.ToString), Convert.ToInt32(item.Value.ToString()), participantID) = True Then
                        lblmsg.Visible = True
                        lblmsg.Text = "Already one Participant is Partciapting from  " & ddlPartcipantLevelIdComboPopUp.SelectedItem.ToString() & " for item " & item.Text & ". Please delselect this item"
                        lblmsg.ForeColor = Drawing.Color.Red
                        Return True
                    End If
                End If
            Next
        End If
        Return False
        lblmsg.Visible = False
    End Function
   
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrItemList As New ArrayList
        Dim arrParticipant As New ArrayList
        Dim arrParticipantList As New ArrayList
        Dim arrParticipantGrpList As New ArrayList
        Dim retParticipantId, retParticipantListId As Integer

        If validateItem() = True Then
            participantModal.Show()
            Exit Sub
        End If
        '    fileUploadImage.SaveAs(Server.MapPath("Images/" + img.ImageUrl))

        arrParticipant.Add(0)
        arrParticipant.Add(txtParticipant.Text.ToString())
        arrParticipant.Add(0)
        arrParticipant.Add(True)
        arrParticipant.Add(Session("ImagePath"))
        arrParticipant.Add(txtAge.Text)
        arrParticipant.Add(txtCampus.Text)
        arrParticipant.Add(txtCorse.Text)
        If Participant.SaveParticipant(arrParticipant, retParticipantId) = True Then
            arrParticipantList.Add(0)
            arrParticipantList.Add(retParticipantId)
            arrParticipantList.Add(txtChessNo.Text.ToString)
            arrParticipantList.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString))
            arrParticipantList.Add(DBNull.Value) ' As of now not using
            arrParticipantList.Add(DateTime.Now)
            arrParticipantList.Add(UserManagement.UserID)
            arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue.ToString))
            arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdComboPopUp.SelectedValue.ToString))
            arrParticipantList.Add(True)
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                arrParticipantList.Add(UserManagement.UserHigherMapId)
            Else
                arrParticipantList.Add(UserManagement.UserMapId)
            End If
            If ddlPartcipantLevelIdCombo1.SelectedValue = "1" Then
                arrParticipantList.Add(1)
            Else
                arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue) + 1)
            End If
            If ddlType.SelectedValue = "0" Then
                arrParticipantList.Add(0)
                arrParticipantList.Add(0)
            Else
                arrParticipantList.Add(1)
                For Each item As ListItem In chkItemList.Items
                    If item.Selected = True Then
                        arrParticipantList.Add(ConnectionLib.item.getMaxNumofParticiapntForItem(Convert.ToInt32(item.Value.ToString())))
                    End If
                Next
            End If
            If Participant.SaveParticipantList(arrParticipantList, retParticipantListId) Then
               
                For Each item As ListItem In chkItemList.Items
                    arrItemList.Add(0) ' When Update
                    arrItemList.Add(retParticipantListId)
                    arrItemList.Add(item.Value.ToString())
                    If item.Selected = True Then
                        arrItemList.Add("Yes")
                    Else
                        arrItemList.Add("No")
                    End If
                    Participant.SaveItemList(arrItemList)
                    arrItemList.Clear()
                Next
                If ddlType.SelectedValue = "1" Then
                    Participant.DeleteParticipantGroup(retParticipantListId)
                    Dim names As String() = txtGroupParticiapnt.Text.ToString.Split(","c)
                    For Each participantName As String In names
                        arrParticipantGrpList.Add(0)
                        arrParticipantGrpList.Add(retParticipantListId)
                        arrParticipantGrpList.Add(participantName)
                        Participant.SaveParticipantGroupList(arrParticipantGrpList)
                        arrParticipantGrpList.Clear()
                    Next
                End If
            End If
        End If
        bindGrid()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrItemList As New ArrayList
        Dim arrParticipant As New ArrayList
        Dim arrParticipantList As New ArrayList
        Dim arrParticipantGrpList As New ArrayList
        Dim retParticipantId, retParticipantListId As Integer

        If validateItemUpdate(Convert.ToInt32(hparticpantId.Value)) = True Then
            participantModal.Show()
            Exit Sub
        End If

        arrParticipant.Add(Convert.ToInt32(hparticpantId.Value))
        arrParticipant.Add(txtParticipant.Text.ToString())
        arrParticipant.Add(0)
        arrParticipant.Add(True)
        If Participant.SaveParticipant(arrParticipant, retParticipantId) = True Then
            arrParticipantList.Add(Convert.ToInt32(hparticpantLitsId.Value))
            arrParticipantList.Add(retParticipantId)
            arrParticipantList.Add(txtChessNo.Text.ToString)
            arrParticipantList.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString))
            arrParticipantList.Add(DBNull.Value) ' As of now not using
            arrParticipantList.Add(DateTime.Now)
            arrParticipantList.Add(UserManagement.UserID)
            arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue.ToString))
            arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdComboPopUp.SelectedValue.ToString))
            arrParticipantList.Add(True)
            If ddlPartcipantLevelIdCombo1.SelectedValue.ToString <> UserManagement.UserTypeId.ToString Then
                arrParticipantList.Add(UserManagement.UserHigherMapId)
            Else
                arrParticipantList.Add(UserManagement.UserMapId)
            End If
            If ddlPartcipantLevelIdCombo1.SelectedValue = "1" Then
                arrParticipantList.Add(1)
            Else
                arrParticipantList.Add(Convert.ToInt32(ddlPartcipantLevelIdCombo1.SelectedValue) + 1)
            End If
            If ddlType.SelectedValue = "0" Then
                arrParticipantList.Add(0)
                arrParticipantList.Add(0)
            Else
                arrParticipantList.Add(1)
                For Each item As ListItem In chkItemList.Items
                    If item.Selected = True Then
                        arrParticipantList.Add(ConnectionLib.item.getMaxNumofParticiapntForItem(Convert.ToInt32(item.Value.ToString())))
                    End If
                Next
            End If
            If Participant.SaveParticipantList(arrParticipantList, retParticipantListId) Then
                Participant.ResetItemList(retParticipantListId)
                For Each item As ListItem In chkItemList.Items
                    arrItemList.Add(0) ' When Update
                    arrItemList.Add(retParticipantListId)
                    arrItemList.Add(item.Value.ToString())
                    If item.Selected = True Then
                        arrItemList.Add("Yes")
                    Else
                        arrItemList.Add("No")
                    End If
                    Participant.SaveItemList(arrItemList)
                    arrItemList.Clear()
                Next
                If ddlType.SelectedValue = "1" Then
                    Participant.DeleteParticipantGroup(retParticipantListId)
                    Dim names As String() = txtGroupParticiapnt.Text.ToString.Split(","c)
                    For Each participantName As String In names
                        arrParticipantGrpList.Add(0)
                        arrParticipantGrpList.Add(retParticipantListId)
                        arrParticipantGrpList.Add(participantName)
                        Participant.SaveParticipantGroupList(arrParticipantGrpList)
                        arrParticipantGrpList.Clear()
                    Next
                End If
            End If
        End If
        bindGrid()
    End Sub

    'Protected Sub rbgSingleItem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbgSingleItem.CheckedChanged
    '    If rbgSingleItem.Checked Then
    '        rowGrpParticipant.Visible = False
    '    Else
    '        rowGrpParticipant.Visible = True
    '    End If
    '    participantModal.Show()
    'End Sub

    'Protected Sub rbgGroupItem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbgGroupItem.CheckedChanged
    '    If rbgSingleItem.Checked Then
    '        rowGrpParticipant.Visible = False
    '    Else
    '        rowGrpParticipant.Visible = True
    '    End If
    '    participantModal.Show()
    'End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancel.Click
         participantModal.Dispose()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlType.SelectedIndexChanged
        clearFields()
        ddlPartcipantLevelIdComboPopUp.Enabled = False
        If ddlType.SelectedValue = "0" Then
            rowGrpParticipant.Visible = False
        Else
            rowGrpParticipant.Visible = True
        End If
        participantModal.Show()
    End Sub

  
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If fileUploadImage.HasFile Then
            Dim fileName As String
            fileName = fileUploadImage.FileName
            Dim randomValue = CInt(Math.Floor((100 - 1 + 1) * Rnd())) + 1
            fileUploadImage.SaveAs(Server.MapPath("~/ParticipantPhoto/" + "_" + randomValue.ToString + fileUploadImage.FileName))
           
            Session("ImagePath") = "_" + randomValue.ToString + fileUploadImage.FileName
            ''   fileUploadImage.SaveAs(MapPath("~/Image/" + fileUploadImage.FileName))

            'Dim img1 As System.Drawing.Image = System.Drawing.Image.FromFile(MapPath("~/image/") + fileUploadImage.FileName)
            'img.ImageUrl = "~/Image/" + fileUploadImage.FileName
        End If
        participantModal.Show()
    End Sub

End Class