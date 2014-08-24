Imports System.Data.Sql
Imports ConnectionLib
Imports System.Configuration
Imports System.Collections

Public Class frmDistrict
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then

            bindGrid()

        End If
    End Sub
    Private Function getDivisionData() As DataTable
        Dim dt As New DataTable

        dt = ConnectionLib.District.GetDistricts("", "").Tables(0)
      
        Return dt
    End Function
    Private Sub bindGrid()
        gvDistrict.DataSource = getDivisionData()
        gvDistrict.DataBind()
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim gRow As GridViewRow = Nothing
        Dim lblDistID As Label = Nothing
        clearFields()
        Try
            btnUpdate.Visible = True
            btnSave.Visible = False
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            '  txtUserName.Text = gvrow.Cells(1).Text
            gRow = TryCast(DirectCast(sender, ImageButton).Parent.Parent, GridViewRow)
            lblDistID = gRow.FindControl("lblDistrictID")
            txtDistName.Text = gvrow.Cells(1).Text
            txtDistCode.Text = gvrow.Cells(2).Text

            lblId.Value = gvDistrict.DataKeys(gvrow.RowIndex).Value.ToString()
            Me.DistrictModal.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub clearFields()
        ' txtUserName.Text = ""

        txtDistName.Text = ""
        txtDistCode.Text = ""
        lblmsg.Text = ""
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        District.DeleteDistricts(Convert.ToInt32(gvDistrict.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList

        arrIn.Add(0)
        arrIn.Add(txtDistName.Text)
        arrIn.Add(txtDistCode.Text)

        If ConnectionLib.District.SaveDistrict(arrIn) = True Then
            clearFields()
            bindGrid()
            DistrictModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If

    End Sub
    Protected Sub btnAddDistrict_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDistrict.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.DistrictModal.Show()
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList

        arrIn.Add(Convert.ToInt32(lblId.Value.ToString))

        arrIn.Add(txtDistName.Text)
        arrIn.Add(txtDistCode.Text)

        If ConnectionLib.District.SaveDistrict(arrIn) = True Then
            clearFields()
            bindGrid()
            DistrictModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

End Class