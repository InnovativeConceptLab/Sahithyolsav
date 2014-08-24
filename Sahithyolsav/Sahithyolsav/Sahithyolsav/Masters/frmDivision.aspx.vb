Imports System.Data.Sql
Imports ConnectionLib
Imports System.Configuration
Imports System.Collections
Public Class frmDivision
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            FillCombo()
            bindGrid()

        End If
    End Sub
    Private Sub FillCombo()
        Dim dtDidtrct As New DataTable
        dtDidtrct = District.GetDistricts("", "", 0).Tables(0)
        ddlDistrict.DataSource = dtDidtrct
        ddlDistrict.DataTextField = "vchDistrictName"
        ddlDistrict.DataValueField = "intDistrictID"
        ddlDistrict.DataBind()
        ddlDistrictS.DataSource = dtDidtrct
        ddlDistrictS.DataTextField = "vchDistrictName"
        ddlDistrictS.DataValueField = "intDistrictID"
        ddlDistrictS.DataBind()
        ddlDistrictS.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Function getDivisionData() As DataTable
        Dim dt As New DataTable
        If ddlDistrictS.SelectedIndex = 0 Then
            dt = ConnectionLib.Division.GetDivision()
        Else
            dt = ConnectionLib.Division.GetDivision(ddlDistrictS.SelectedValue)
        End If

        Return dt
    End Function
    Private Sub bindGrid()
        gvDivision.DataSource = getDivisionData()
        gvDivision.DataBind()
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
            txtDivsnName.Text = gvrow.Cells(1).Text
            txtDivsnCode.Text = gvrow.Cells(2).Text
            ddlDistrict.SelectedValue = lblDistID.Text
            lblId.Value = gvDivision.DataKeys(gvrow.RowIndex).Value.ToString()
            Me.DivisionModal.Show()
        Catch ex As Exception

        End Try

      
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        Division.DeleteDivision(Convert.ToInt32(gvDivision.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
       
        arrIn.Add(0)
        arrIn.Add(ddlDistrict.SelectedValue)

        arrIn.Add(txtDivsnName.Text)
        arrIn.Add(txtDivsnCode.Text)
       
        If ConnectionLib.Division.SaveDivision(arrIn) = True Then
            clearFields()
            bindGrid()
            DivisionModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If

    End Sub
    Private Sub clearFields()
        ' txtUserName.Text = ""
        ddlDistrictS.SelectedIndex = 0
        txtDivsnName.Text = ""
        txtDivsnCode.Text = ""
        lblmsg.Text = ""
    End Sub

    Protected Sub btnAddDivision_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDivision.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.DivisionModal.Show()
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList

        arrIn.Add(Convert.ToInt32(lblId.Value.ToString))

        arrIn.Add(ddlDistrict.SelectedValue) ' DistrictID Combo
        arrIn.Add(txtDivsnName.Text)
        arrIn.Add(txtDivsnCode.Text)
      
        If ConnectionLib.Division.SaveDivision(arrIn) = True Then
            clearFields()
            bindGrid()
            DivisionModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub
   
    Protected Sub imgSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        Try
            bindGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class