Imports ConnectionLib
Imports System.Data.Sql
Public Class frmStage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindGrid()
        End If
    End Sub

    Protected Sub btnAddStage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddStage.Click
        txtPlace.Text = ""
        txtStage.Text = ""
        stageModal.Show()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(txtStage.Text.ToString)
        arrIn.Add(txtPlace.Text.ToString)
        Schedules.SaveStage(arrIn)
        bindGrid()
    End Sub
    Private Sub bindGrid()
        Dim dt As New DataTable
        dt = Schedules.getStages()
        gvStage.DataSource = dt
        gvStage.DataBind()
    End Sub
    Protected Sub imgDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim stageId As Integer
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        stageId = Convert.ToInt32(gvStage.DataKeys(gvrow.RowIndex).Value.ToString())
        Schedules.DeleteStage(stageId)
        bindGrid()
    End Sub
End Class