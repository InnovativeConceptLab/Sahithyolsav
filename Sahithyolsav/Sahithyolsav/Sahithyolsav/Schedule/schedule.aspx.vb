Imports ConnectionLib
Imports System.Globalization
Public Class schedule
    Inherits System.Web.UI.Page
    'testt
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
        clearFields()
        btnSave.Visible = True
        btnUpdate.Visible = False
        scheduleModal.Show()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString))
        arrIn.Add(Convert.ToInt32(ddlItem.SelectedValue.ToString))
        arrIn.Add(Convert.ToInt32(ddlStage.SelectedValue.ToString))
        arrIn.Add(Convert.ToDateTime(txtDate.Text))
        '  arrIn.Add(Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
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
        Dim items As String = ""
        Dim ValidStage As Boolean = True
        Dim ScheduleDate As String = Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        ValidStage = Schedules.CheckSceduleVallidationWithStage(Convert.ToInt32(ddlStage.SelectedValue.ToString), txtTime1.Text, txtTime2.Text, ddlAMPM.SelectedValue.ToString, ScheduleDate)
        If ValidStage = True Then
            dt = Schedules.getItemOfCurrrentSchedules(Convert.ToInt32(ddlStage.SelectedValue.ToString), txtTime1.Text, txtTime2.Text, ddlAMPM.SelectedValue.ToString, ScheduleDate)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        items = dt.Rows(i).Item(0).ToString
                    Else
                        items = items + "," + dt.Rows(i).Item(0).ToString
                    End If
                Next
                Return Schedules.CheckSceduleVallidation(items, ddlItem.SelectedValue.ToString, UserManagement.UserTypeId) ' 
            Else
                Return True
            End If
        Else
            Return False
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
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim gRow As GridViewRow = Nothing
        Dim lblSchdleID As Label = Nothing
        Dim lblSectID As Label = Nothing
        Dim lblItemID As Label = Nothing
        Dim lblStageID As Label = Nothing
        clearFields()
        Try
            btnUpdate.Visible = True
            btnSave.Visible = False
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            gRow = TryCast(DirectCast(sender, ImageButton).Parent.Parent, GridViewRow)
            lblSchdleID = gRow.FindControl("lblSchdleID")
            lblSectID = gRow.FindControl("lblSectID")
            lblItemID = gRow.FindControl("lblItemID")
            lblStageID = gRow.FindControl("lblStageID")
            lblId.Value = lblSchdleID.Text
            txtDate.Text = gvrow.Cells(4).Text
            'Convert.ToDateTime(gvrow.Cells(4).Text).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
            txtTime1.Text = gvrow.Cells(5).Text.Split(":")(0)
            txtTime2.Text = gvrow.Cells(5).Text.Split(":")(1).Split(" ")(0)
            ddlAMPM.SelectedValue = gvrow.Cells(5).Text.Split(" ")(1)
            ddlSection.SelectedValue = lblSectID.Text
            '   ddlItem.SelectedValue = lblItemID.Text
            ddlStage.SelectedValue = lblStageID.Text
            Me.scheduleModal.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub clearFields()
        txtDate.Text = ""
        txtTime1.Text = ""
        txtTime2.Text = ""
        ddlSection.SelectedIndex = 0
        ddlStage.SelectedIndex = 0
        ddlItem.Items.Clear()
        lblmsg.Text = ""
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList
        Try
            arrIn.Add(lblId.Value)
            arrIn.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString))
            arrIn.Add(Convert.ToInt32(ddlItem.SelectedValue.ToString))
            arrIn.Add(Convert.ToInt32(ddlStage.SelectedValue.ToString))
            arrIn.Add(Convert.ToDateTime(txtDate.Text))
            '    arrIn.Add(Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
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


        Catch ex As Exception

        End Try

      
    End Sub
End Class