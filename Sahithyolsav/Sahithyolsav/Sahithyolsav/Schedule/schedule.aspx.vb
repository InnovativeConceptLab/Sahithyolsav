Imports ConnectionLib
Public Class schedule
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillSection()
            fillStage()
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
        arrIn.Add(Date.Today)
        arrIn.Add("Time")
        arrIn.Add("AM")
        Schedules.SaveSchedule(arrIn)
    End Sub

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
End Class