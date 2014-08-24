Imports ConnectionLib
Imports System.Data.Sql
Public Class frmChessNumber
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillSearchSection()
            pnlContent.Visible = True
            bindHeader()
            fillTeam()
        End If
    End Sub
    Private Sub fillTeam()
        If UserManagement.UserTypeId.ToString = "0" Then
        ElseIf UserManagement.UserTypeId.ToString = "1" Then
            Dim dt As New DataTable
            dt = District.GetDistricts("", "", 0).Tables(0)
            ddlTeam.DataSource = dt
            ddlTeam.DataTextField = "vchDistrictName"
            ddlTeam.DataValueField = "intDistrictID"
            ddlTeam.DataBind()

        ElseIf UserManagement.UserTypeId.ToString = "2" Then
            Dim dt As New DataTable
            dt = Division.GetDivision(UserManagement.UserMapId, "", "", 0)
            ddlTeam.DataSource = dt
            ddlTeam.DataTextField = "vchDivisionName"
            ddlTeam.DataValueField = "intDivisionId"
            ddlTeam.DataBind()
            ddlTeam.Items.Insert(0, New ListItem("----Select----", "0"))
            ddlTeam.Enabled = True
        ElseIf UserManagement.UserTypeId.ToString = "3" Then
            Dim dt As New DataTable
            dt = Sector.getAllSectorBySearchId("", 0, UserManagement.UserMapId)
            ddlTeam.DataSource = dt
            ddlTeam.DataTextField = "vchSectorName"
            ddlTeam.DataValueField = "intSectorId"
            ddlTeam.DataBind()
            ddlTeam.Items.Insert(0, New ListItem("----Select----", "0"))

        ElseIf UserManagement.UserTypeId.ToString = "4" Then
            Dim dt As New DataTable

            dt = Unit.getAllUnitBySearchId("", 0, 0, UserManagement.UserMapId)
            ddlTeam.DataSource = dt
            ddlTeam.DataTextField = "vchUnitName"
            ddlTeam.DataValueField = "intUnitId"
            ddlTeam.DataBind()
            ddlTeam.Items.Insert(0, New ListItem("----Select----", "0"))

        ElseIf UserManagement.UserTypeId.ToString = "5" Then
            Dim dt As New DataTable
            dt = Unit.getAllUnitById(UserManagement.UserMapId)
            ddlTeam.DataSource = dt
            ddlTeam.DataTextField = "vchUnitName"
            ddlTeam.DataValueField = "intUnitId"
            ddlTeam.DataBind()
            ddlTeam.Items.Insert(0, New ListItem("----Select----", "0"))
            ddlTeam.SelectedValue = UserManagement.UserMapId.ToString
        End If
    End Sub
    Private Sub bindHeader()
        Dim struserLevel As String
        Dim dt As New DataTable
        struserLevel = ""
        If UserManagement.UserTypeId = 1 Then
            struserLevel = "Kerala State Level Paricipant List"
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
            struserLevel = dt.Rows(0).ItemArray(4).ToString + " Unit Level Paricipant List"
        End If
        participantHeader.Text = "Chess Number [ " & struserLevel & " ]"
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
    Protected Sub btnChessNumSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim gRow As GridViewRow = Nothing
        Dim lblintParticipantListId As Label = Nothing
        Dim txtChessNum As TextBox = Nothing
        Try
            btnUpdate.Visible = False
            btnSave.Visible = True
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            '  txtUserName.Text = gvrow.Cells(1).Text
            gRow = TryCast(DirectCast(sender, ImageButton).Parent.Parent, GridViewRow)
            lblintParticipantListId = gRow.FindControl("lblintParticipantListId")
            hintParticipantListId.Value = lblintParticipantListId.Text
            txtChessNum = gRow.FindControl("txtchesNum")
            Participant.UpdateChessNumber(Convert.ToInt32(lblintParticipantListId.Text.ToString), txtChessNum.Text.ToString)
            bindGrid()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub bindGrid()
        gvChessNumMapdetails.DataSource = Participant.GetParticipantBySectionId(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString), UserManagement.UserMapId, Convert.ToInt32(ddlTeam.SelectedValue.ToString))
        gvChessNumMapdetails.DataBind()
    End Sub


    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        If ddlSearchSection.SelectedValue <> "0" And ddlTeam.SelectedValue <> "0" Then
            bindGrid()
        End If
    End Sub

    Protected Sub ddlSearchSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSearchSection.SelectedIndexChanged

    End Sub
End Class