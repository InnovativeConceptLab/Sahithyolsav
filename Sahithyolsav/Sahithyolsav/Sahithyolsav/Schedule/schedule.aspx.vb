Imports ConnectionLib
Public Class schedule
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillSection()
            fillStage()
            bindGrid()
        End If
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
    Private Sub fillStage()
        Dim dt As New DataTable
        dt = ConnectionLib.Schedules.getStages()
        ddlStage.DataSource = dt
        ddlStage.DataTextField = "VchStageName"
        ddlStage.DataValueField = "intStageId"
        ddlStage.DataBind()
        ddlStage.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Protected Sub btnAddSch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddSch.Click
        scheduleModal.Show()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString))
        arrIn.Add(Convert.ToInt32(ddlItem.SelectedValue.ToString))
        arrIn.Add(Convert.ToInt32(ddlStage.SelectedValue.ToString))
        arrIn.Add(Convert.ToDateTime(txtDate.Text))
        arrIn.Add(txtTime1.Text)
        arrIn.Add(txtTime2.Text)
        arrIn.Add(ddlAMPM.SelectedValue.ToString)
        If validateScedule() = True Then
            Schedules.SaveSchedule(arrIn)
            bindGrid()
        Else
            lblmsg.Text = "Can't schedule this item at this time"
            scheduleModal.Show()
        End If


    End Sub
    Private Function validateScedule() As Boolean
        Dim dt As New DataTable
        dt = Schedules.getItemOfCurrrentSchedules(Convert.ToInt32(ddlStage.SelectedValue.ToString), txtTime1.Text, txtTime2.Text, ddlAMPM.SelectedValue.ToString)
        If dt.Rows.Count > 0 Then
            Return Schedules.CheckSceduleVallidation(dt.Rows(0).Item(0).ToString, ddlItem.SelectedValue.ToString, UserManagement.UserTypeId)
        Else
            Return True
        End If
    End Function
    Protected Sub ddlSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSection.SelectedIndexChanged
        scheduleModal.Show()
        bindItemList()
    End Sub
    Private Sub bindItemList()
        Dim dt As New DataTable
        If (ddlSection.SelectedValue <> "0") Then
            dt = ConnectionLib.item.getItemsBySectionId(Convert.ToInt32(ddlSection.SelectedValue.ToString()))
            ddlItem.DataSource = dt
            ddlItem.DataTextField = "vchItemName"
            ddlItem.DataValueField = "intItemId"
            ddlItem.DataBind()
        End If
    End Sub
    Private Sub bindGrid()
        Dim dt As New DataTable
        dt = Schedules.getSchedules()
        gvSchedule.DataSource = dt
        gvSchedule.DataBind()
    End Sub
End Class