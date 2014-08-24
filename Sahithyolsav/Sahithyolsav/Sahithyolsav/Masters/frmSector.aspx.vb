Imports ConnectionLib
Imports System.Data.Sql
Public Class frmSector
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillDistrict()
            fillDivision()

            fillSearchDistrict()
            fillSearchDivision()
            bindGrid()
        End If
    End Sub
    Private Sub bindGrid()
        gvsectordetails.DataSource = Sector.getAllSector()
        gvsectordetails.DataBind()
    End Sub
    Private Sub fillDivision()
        Dim dt As New DataTable
        If ddldistrict.SelectedIndex = 0 Then
            dt = Division.getAllDivisions(0)
        Else
            dt = Division.getAllDivisions(ddldistrict.SelectedValue)
        End If
        ddldivision.DataSource = dt
        ddldivision.DataTextField = "vchDivisionName"
        ddldivision.DataValueField = "intDivisionId"
        ddldivision.DataBind()
        ddldivision.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub fillDistrict()
        Dim dt As New DataTable
        dt = District.GetDistricts("", "", 0).Tables(0)
        ddldistrict.DataSource = dt
        ddldistrict.DataTextField = "vchDistrictName"
        ddldistrict.DataValueField = "intDistrictID"
        ddldistrict.DataBind()
        ddldistrict.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub fillSearchDivision()
        Dim dt As New DataTable
        If ddlsearchDistrict.SelectedIndex = 0 Then
            dt = Division.getAllDivisions(0)
        Else
            dt = Division.getAllDivisions(ddlsearchDistrict.SelectedValue)
        End If

        ddlsearchDivision.DataSource = dt
        ddlsearchDivision.DataTextField = "vchDivisionName"
        ddlsearchDivision.DataValueField = "intDivisionId"
        ddlsearchDivision.DataBind()
        ddlsearchDivision.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Private Sub fillSearchDistrict()
        Dim dt As New DataTable
        dt = District.GetDistricts("", "", 0).Tables(0)
        ddlsearchDistrict.DataSource = dt
        ddlsearchDistrict.DataTextField = "vchDistrictName"
        ddlsearchDistrict.DataValueField = "intDistrictID"
        ddlsearchDistrict.DataBind()
        ddlsearchDistrict.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim dt As New DataTable
        Try
            Dim btndetails As ImageButton = TryCast(sender, ImageButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            dt = Sector.getAllSectorById(Convert.ToInt32(gvsectordetails.DataKeys(gvrow.RowIndex).Value.ToString()))
            btnUpdate.Visible = True
            btnSave.Visible = False
            clearFields()
            ddldistrict.SelectedValue = dt.Rows(0).Item(2)
            fillDivision()
            ddldivision.SelectedValue = dt.Rows(0).Item(1)
            txtSectorName.Text = dt.Rows(0).Item(3)
            txtCode.Text = dt.Rows(0).Item(4)
            lblId.Value = gvsectordetails.DataKeys(gvrow.RowIndex).Value.ToString()
            Me.sectorModal.Show()
        Catch ex As Exception

        End Try
      
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        Sector.Deletesector(Convert.ToInt32(gvsectordetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Private Sub clearFields()
        txtSectorName.Text = ""
        ddldistrict.SelectedIndex = 0
        ddldivision.SelectedIndex = 0
        ddlsearchDistrict.SelectedIndex = 0
        ddlsearchDivision.SelectedIndex = 0
        txtCode.Text = ""
        lblmsg.Text = ""
    End Sub

    Protected Sub btnAdditem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdditem.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.sectorModal.Show()
    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        Dim dt As New DataTable
        dt = Sector.getAllSectorBySearchId(txtsearchsectorName.Text.ToString(), Convert.ToInt32(Convert.ToInt32(ddlsearchDistrict.SelectedValue.ToString())), Convert.ToInt32(Convert.ToInt32(ddlsearchDivision.SelectedValue.ToString())))
        gvsectordetails.DataSource = dt
        gvsectordetails.DataBind()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(Convert.ToInt32(ddldivision.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldistrict.SelectedValue.ToString()))
        arrIn.Add(txtSectorName.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Sector.SaveSector(arrIn) Then
            clearFields()
            bindGrid()
            sectorModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList
        arrIn.Add(Convert.ToInt32(lblId.Value))
        arrIn.Add(Convert.ToInt32(ddldivision.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldistrict.SelectedValue.ToString()))
        arrIn.Add(txtSectorName.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Sector.SaveSector(arrIn) Then
            clearFields()
            bindGrid()
            sectorModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Sub ddlsearchDistrict_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsearchDistrict.SelectedIndexChanged
        fillSearchDivision()
    End Sub

    Private Sub ddldistrict_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddldistrict.SelectedIndexChanged
        fillDivision()

        sectorModal.Show()
    End Sub
End Class