Imports ConnectionLib
Imports System.Data.Sql
Public Class frmTabulation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillSearchSection()
            fillSection()
            pnlContent.Visible = True
            bindHeader()
        End If
    End Sub
    Private Sub bindHeader()
        Dim struserLevel As String
        Dim dt As New DataTable
        struserLevel = ""
        If UserManagement.UserTypeId = 1 Then
            struserLevel = " KERALA STATE LEVEL PARTICIPANT LIST"
        ElseIf UserManagement.UserTypeId = 2 Then
            dt = District.GetDistricts("", "", UserManagement.UserMapId).Tables(0)
            struserLevel = dt.Rows(0).ItemArray(1).ToString + " District Level Paricipant List"
        ElseIf UserManagement.UserTypeId = 3 Then
            dt = Division.GetDivision(0, "", "", UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + " Division Level Paricipant List"
        ElseIf UserManagement.UserTypeId = 4 Then
            dt = Sector.getAllSectorById(UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + " Sector Level Paricipant List"
        ElseIf UserManagement.UserTypeId = 5 Then
            dt = Unit.getAllUnitById(UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + " Unit Level Paricipant List"
        End If
        participantHeader.Text = "TABULATION [ " & struserLevel & " ]"
    End Sub
    Private Sub fillSearchSection()
        Dim dt As New DataTable
        dt = Section.getAllSections()
        ddlSearchSection.DataSource = dt
        ddlSearchSection.DataTextField = "vchSectionName"
        ddlSearchSection.DataValueField = "intSectionID"
        ddlSearchSection.DataBind()
        ddlSearchSection.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub fillLevels()
        Dim dt As New DataTable
        dt = Level.getAllLevel()
        ddlPartcipantLevelIdCombo1.DataSource = dt
        ddlPartcipantLevelIdCombo1.DataTextField = "vchLevelName"
        ddlPartcipantLevelIdCombo1.DataValueField = "intLevelID"
        ddlPartcipantLevelIdCombo1.DataBind()
        ddlPartcipantLevelIdCombo1.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub SetComboBox()
        If ddlPartcipantLevelIdCombo1.SelectedValue = "0" Or ddlPartcipantLevelIdCombo1.SelectedValue = "1" Then
            ddlPartcipantLevelIdCombo2.Visible = False
            ddlPartcipantLevelIdCombo3.Visible = False
            ddlPartcipantLevelIdCombo4.Visible = False
            ddlPartcipantLevelIdCombo5.Visible = False
            sublist.Visible = False
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "2" Then
            fillDistrict()
            sublist.Visible = True
            ddlPartcipantLevelIdCombo2.Visible = True
            ddlPartcipantLevelIdCombo3.Visible = False
            ddlPartcipantLevelIdCombo4.Visible = False
            ddlPartcipantLevelIdCombo5.Visible = False
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "3" Then
            fillDistrict()
            fillDivision()
            sublist.Visible = True
            ddlPartcipantLevelIdCombo2.Visible = True
            ddlPartcipantLevelIdCombo3.Visible = True
            ddlPartcipantLevelIdCombo4.Visible = False
            ddlPartcipantLevelIdCombo5.Visible = False
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "4" Then
            fillDistrict()
            fillDivision()
            fillSector()
            sublist.Visible = True
            ddlPartcipantLevelIdCombo2.Visible = True
            ddlPartcipantLevelIdCombo3.Visible = True
            ddlPartcipantLevelIdCombo4.Visible = True
            ddlPartcipantLevelIdCombo5.Visible = False
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "5" Then
            fillDistrict()
            fillDivision()
            fillSector()
            fillUnit()
            sublist.Visible = True
            ddlPartcipantLevelIdCombo2.Visible = True
            ddlPartcipantLevelIdCombo3.Visible = True
            ddlPartcipantLevelIdCombo4.Visible = True
            ddlPartcipantLevelIdCombo5.Visible = True
        End If
        CheckForBindGrid()
    End Sub
    Private Sub fillDistrict()
        Dim dt As New DataTable
        dt = District.GetDistricts("", "", 0).Tables(0)
        ddlPartcipantLevelIdCombo2.DataSource = dt
        ddlPartcipantLevelIdCombo2.DataTextField = "vchDistrictName"
        ddlPartcipantLevelIdCombo2.DataValueField = "intDistrictID"
        ddlPartcipantLevelIdCombo2.DataBind()
        ddlPartcipantLevelIdCombo2.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub fillDivision()
        Dim dt As New DataTable
        ddlPartcipantLevelIdCombo3.DataSource = dt
        ddlPartcipantLevelIdCombo3.DataBind()
    End Sub
    Private Sub fillSector()
        Dim dt As New DataTable
        ddlPartcipantLevelIdCombo4.DataSource = dt
        ddlPartcipantLevelIdCombo4.DataBind()
    End Sub
    Private Sub fillUnit()
        Dim dt As New DataTable
        ddlPartcipantLevelIdCombo5.DataSource = dt
        ddlPartcipantLevelIdCombo5.DataBind()
    End Sub
    Private Sub CheckForBindGrid()
        pnlContent.Visible = False
        If ddlPartcipantLevelIdCombo1.SelectedValue = "0" Then
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "1" Then
            pnlContent.Visible = True
            bindGrid()
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "2" Then
            If ddlPartcipantLevelIdCombo2.SelectedValue <> "0" Then
                pnlContent.Visible = True
                participantHeader.Text = "Add Participant for " + ddlPartcipantLevelIdCombo2.SelectedItem.Text + "  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "   [  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "  Level ]"
                ' bindGrid()
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "3" Then
            If ddlPartcipantLevelIdCombo2.SelectedValue <> "0" And ddlPartcipantLevelIdCombo3.SelectedValue <> "0" Then
                pnlContent.Visible = True
                participantHeader.Text = "Add Participant for " + ddlPartcipantLevelIdCombo3.SelectedItem.Text + "  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "   [  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "  Level ]"
                'bindGrid()
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "4" Then
            If ddlPartcipantLevelIdCombo2.SelectedValue <> "0" And ddlPartcipantLevelIdCombo3.SelectedValue <> "0" And ddlPartcipantLevelIdCombo4.SelectedValue <> "0" Then
                pnlContent.Visible = True
                participantHeader.Text = "Add Participant for " + ddlPartcipantLevelIdCombo4.SelectedItem.Text + "  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "   [  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "  Level ]"
                ' bindGrid()
            End If
        ElseIf ddlPartcipantLevelIdCombo1.SelectedValue = "5" Then
            If ddlPartcipantLevelIdCombo2.SelectedValue <> "0" And ddlPartcipantLevelIdCombo3.SelectedValue <> "0" And ddlPartcipantLevelIdCombo4.SelectedValue <> "0" _
                            And ddlPartcipantLevelIdCombo5.SelectedValue <> "0" Then
                pnlContent.Visible = True
                participantHeader.Text = "Add Participant for " + ddlPartcipantLevelIdCombo5.SelectedItem.Text + "  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "   [  " + ddlPartcipantLevelIdCombo1.SelectedItem.Text + "  Level ]"
                ' bindGrid()
            End If
        End If
    End Sub
    Private Sub bindGrid()
        gvCodeLtterMapdetails.DataSource = Participant.getParticipantItemBySectionandItemId(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString), Convert.ToInt32(ddlSearchItem.SelectedValue.ToString), UserManagement.UserMapId)
        gvCodeLtterMapdetails.DataBind()
    End Sub
    Private Sub fillSection()

    End Sub

    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim gRow As GridViewRow = Nothing
        Dim lblintParticipantListId As Label = Nothing
        Dim lblintItemId As Label = Nothing
        Dim lblCodeLetterMapID As Label = Nothing
        Try
            btnUpdate.Visible = False
            btnSave.Visible = True
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            '  txtUserName.Text = gvrow.Cells(1).Text
            gRow = TryCast(DirectCast(sender, ImageButton).Parent.Parent, GridViewRow)
            lblintParticipantListId = gRow.FindControl("lblintParticipantListId")
            lblintItemId = gRow.FindControl("lblintItemId")
            lblCodeLetterMapID = gRow.FindControl("lblCodeLetterMapID")
            txtParticipant.Text = gvrow.Cells(1).Text
            txtSection.Text = gvrow.Cells(2).Text
            txtItem.Text = gvrow.Cells(3).Text
            txtChessNum.Text = gvrow.Cells(4).Text
            txtCodeLetter.Text = gvrow.Cells(5).Text
            txtMarks.Text = gvrow.Cells(6).Text
            hintParticipantListId.Value = lblintParticipantListId.Text
            hintItemId.Value = lblintItemId.Text
            htabulationid.Value = gvCodeLtterMapdetails.DataKeys(gvrow.RowIndex).Value.ToString()
            hCodeLetterMapID.Value = lblCodeLetterMapID.Text
            Me.codeLetterModal.Show()
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub btnSaveMark_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim gRow As GridViewRow = Nothing
        Dim lblintParticipantListId As Label = Nothing
        Dim lblintItemId As Label = Nothing
        Dim lblCodeLetterMapID As Label = Nothing
        Dim txtMarkTextBox1 As TextBox = Nothing
        Dim txtMarkTextBox2 As TextBox = Nothing
        Dim txtMarkTextBox3 As TextBox = Nothing
        Dim FinalPoint As Integer
        Dim grade As String
        Try
            btnUpdate.Visible = False
            btnSave.Visible = True
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            '  txtUserName.Text = gvrow.Cells(1).Text
            gRow = TryCast(DirectCast(sender, ImageButton).Parent.Parent, GridViewRow)
            lblintParticipantListId = gRow.FindControl("lblintParticipantListId")
            lblintItemId = gRow.FindControl("lblintItemId")
            lblCodeLetterMapID = gRow.FindControl("lblCodeLetterMapID")
            txtParticipant.Text = gvrow.Cells(1).Text
            txtSection.Text = gvrow.Cells(2).Text
            txtItem.Text = gvrow.Cells(3).Text
            txtChessNum.Text = gvrow.Cells(4).Text
            txtCodeLetter.Text = gvrow.Cells(5).Text
            hintParticipantListId.Value = lblintParticipantListId.Text
            hintItemId.Value = lblintItemId.Text
            htabulationid.Value = gvCodeLtterMapdetails.DataKeys(gvrow.RowIndex).Value.ToString()
            hCodeLetterMapID.Value = lblCodeLetterMapID.Text
            txtMarkTextBox1 = gRow.FindControl("txtMarks1")
            txtMarkTextBox2 = gRow.FindControl("txtMarks2")
            txtMarkTextBox3 = gRow.FindControl("txtMarks3")

            Dim totalMarks1, totalMarks2, totalMarks3, totalMarks As Decimal
            Dim dividedBy As Integer = 0
            If txtMarkTextBox1.Text.ToString.Trim <> "" Then
                dividedBy = dividedBy + 100
                totalMarks1 = Convert.ToDecimal(txtMarkTextBox1.Text.ToString)
            Else
                totalMarks1 = 0
            End If
            If txtMarkTextBox2.Text.ToString.Trim <> "" Then
                dividedBy = dividedBy + 100
                totalMarks2 = Convert.ToDecimal(txtMarkTextBox2.Text.ToString)
            Else
                totalMarks2 = 0
            End If
            If txtMarkTextBox3.Text.ToString.Trim <> "" Then
                dividedBy = dividedBy + 100
                totalMarks3 = Convert.ToDecimal(txtMarkTextBox3.Text.ToString)
            Else
                totalMarks3 = 0
            End If

            totalMarks = totalMarks1 + totalMarks2 + totalMarks3
            totalMarks = (totalMarks * 100) / dividedBy
            FinalPoint = GetPoint(totalMarks, hintItemId.Value)
            grade = GetGrade(totalMarks)

            Dim arrin As New ArrayList
            arrin.Add(Convert.ToInt32(htabulationid.Value.ToString))
            arrin.Add(Convert.ToInt32(hintParticipantListId.Value))
            arrin.Add(Convert.ToInt32(hCodeLetterMapID.Value))
            arrin.Add(Convert.ToDecimal(totalMarks))
            arrin.Add(DateTime.Now)
            arrin.Add(Convert.ToInt32(hintItemId.Value))
            arrin.Add(FinalPoint)
            arrin.Add(FinalPoint)
            arrin.Add(grade)
            Participant.SaveTabulation(arrin)
            bindGrid()
        Catch ex As Exception

        End Try


    End Sub
    Private Function GetPoint(ByVal totalmarks As Integer, ByVal itemID As String) As Integer
        Dim arrin As New ArrayList
        Dim DtPoint As New DataTable
        Dim Point As Integer
        arrin.Add(totalmarks)
        arrin.Add(itemID)
        If totalmarks >= 70 And totalmarks <= 100 Then
            ' DtPoint = item.GetPoints(itemID, "First")
            Point = 3 'DtPoint.Rows(0).Item(0)
        ElseIf totalmarks >= 60 And totalmarks <= 69 Then
            'DtPoint = item.GetPoints(itemID, "Second")
            Point = 2 'DtPoint.Rows(0).Item(0)
        ElseIf totalmarks >= 50 And totalmarks <= 59 Then
            'DtPoint = item.GetPoints(itemID, "Third")
            Point = 1 'DtPoint.Rows(0).Item(0)
        Else
            Point = 0
        End If
        Return Point
    End Function
    Private Function GetGrade(ByVal totalmarks As Integer) As String
        Dim grade As String
        If totalmarks >= 70 And totalmarks <= 100 Then
            grade = "A"
        ElseIf totalmarks >= 60 And totalmarks <= 69 Then
            grade = "B"
        ElseIf totalmarks >= 50 And totalmarks <= 59 Then
            grade = "C"
        Else
            grade = "D"
        End If
        Return grade
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrin As New ArrayList
        arrin.Add(Convert.ToInt32(htabulationid.Value.ToString))
        arrin.Add(Convert.ToInt32(hintParticipantListId.Value))
        arrin.Add(Convert.ToInt32(hCodeLetterMapID.Value))
        arrin.Add(Convert.ToDecimal(txtMarks.Text.ToString))
        arrin.Add(DateTime.Now)
        arrin.Add(Convert.ToInt32(hintItemId.Value))
        Participant.SaveTabulation(arrin)
        bindGrid()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        bindGrid()
    End Sub

    Protected Sub ddlPartcipantLevelIdCombo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo1.SelectedIndexChanged
        SetComboBox()
    End Sub

    Protected Sub ddlPartcipantLevelIdCombo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo2.SelectedIndexChanged
        Dim dt As New DataTable
        fillSector()
        fillUnit()
        If ddlPartcipantLevelIdCombo2.SelectedValue <> "0" Then
            dt = Division.getAllDivisions(Convert.ToInt32(ddlPartcipantLevelIdCombo2.SelectedValue))
            ddlPartcipantLevelIdCombo3.DataSource = dt
            ddlPartcipantLevelIdCombo3.DataTextField = "vchDivisionName"
            ddlPartcipantLevelIdCombo3.DataValueField = "intDivisionId"
            ddlPartcipantLevelIdCombo3.DataBind()
            ddlPartcipantLevelIdCombo3.Items.Insert(0, New ListItem("----Select----", "0"))
        Else
            ddlPartcipantLevelIdCombo3.DataSource = dt
            ddlPartcipantLevelIdCombo3.DataBind()
        End If
        CheckForBindGrid()
    End Sub

    Protected Sub ddlPartcipantLevelIdCombo3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo3.SelectedIndexChanged
        Dim dt As New DataTable
        fillUnit()
        If ddlPartcipantLevelIdCombo3.SelectedValue <> "0" Then
            dt = Sector.getAllSector(Convert.ToInt32(ddlPartcipantLevelIdCombo3.SelectedValue))
            ddlPartcipantLevelIdCombo4.DataSource = dt
            ddlPartcipantLevelIdCombo4.DataTextField = "vchSectorName"
            ddlPartcipantLevelIdCombo4.DataValueField = "intSectorId"
            ddlPartcipantLevelIdCombo4.DataBind()
            ddlPartcipantLevelIdCombo4.Items.Insert(0, New ListItem("----Select----", "0"))
        Else
            ddlPartcipantLevelIdCombo4.DataSource = dt
            ddlPartcipantLevelIdCombo4.DataBind()
        End If
        CheckForBindGrid()
    End Sub

    Protected Sub ddlPartcipantLevelIdCombo4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo4.SelectedIndexChanged
        Dim dt As New DataTable
        If ddlPartcipantLevelIdCombo4.SelectedValue <> "0" Then
            dt = Unit.getAllUnitBySearchId("", 0, 0, Convert.ToInt32(ddlPartcipantLevelIdCombo4.SelectedValue))
            ddlPartcipantLevelIdCombo5.DataSource = dt
            ddlPartcipantLevelIdCombo5.DataTextField = "vchUnitName"
            ddlPartcipantLevelIdCombo5.DataValueField = "intUnitId"
            ddlPartcipantLevelIdCombo5.DataBind()
            ddlPartcipantLevelIdCombo5.Items.Insert(0, New ListItem("----Select----", "0"))
        Else
            ddlPartcipantLevelIdCombo5.DataSource = dt
            ddlPartcipantLevelIdCombo5.DataBind()
        End If
        CheckForBindGrid()
    End Sub

    Protected Sub ddlPartcipantLevelIdCombo5_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPartcipantLevelIdCombo5.SelectedIndexChanged
        CheckForBindGrid()
    End Sub

    Protected Sub ddlSearchSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSearchSection.SelectedIndexChanged
        Dim dt As New DataTable
        If (ddlSearchSection.SelectedValue <> "0") Then
            dt = ConnectionLib.item.getItemsBySectionId(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString()))
            ddlSearchItem.DataSource = dt
            ddlSearchItem.DataTextField = "vchItemName"
            ddlSearchItem.DataValueField = "intItemId"
            ddlSearchItem.DataBind()
        Else
            ddlSearchItem.DataSource = dt
            ddlSearchItem.DataBind()
            '  ddlItem.Items.Insert(0, New ListItem("----Select----", "0"))
        End If
    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        bindGrid()
    End Sub
End Class