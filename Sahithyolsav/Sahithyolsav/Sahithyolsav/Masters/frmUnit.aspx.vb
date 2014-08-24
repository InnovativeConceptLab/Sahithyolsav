Imports ConnectionLib
Imports System.Data.Sql
Public Class frmUnit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        '  LabelMsg.Text = ""
        If Not IsPostBack Then
            fillDistrict()
            fillSearchDistrict()
            ddlsearchDivision.Items.Insert(0, New ListItem("----Select----", "0"))
            ddlsearchsector.Items.Insert(0, New ListItem("----Select----", "0"))
            '  fillDivision()
            '  fillSearchDivision()
            '  fillSector()
            ' fillSearchSector()
            bindGrid()
        End If
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
    Private Sub fillSector()
        Dim dt As New DataTable
        If ddldivision.SelectedIndex = 0 Then
            dt = Sector.getAllSector()
        Else
            dt = Sector.getAllSector(ddldivision.SelectedValue)
        End If

        ddlSector.DataSource = dt
        ddlSector.DataTextField = "vchSectorName"
        ddlSector.DataValueField = "intSectorId"
        ddlSector.DataBind()
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))

    End Sub
    Private Sub fillSearchSector()

        Dim dt As New DataTable
        If ddlsearchDivision.SelectedIndex = 0 Then
            dt = Sector.getAllSector()
        Else
            dt = Sector.getAllSector(ddlsearchDivision.SelectedValue)
        End If

        ddlsearchsector.DataSource = dt
        ddlsearchsector.DataTextField = "vchSectorName"
        ddlsearchsector.DataValueField = "intSectorId"
        ddlsearchsector.DataBind()
        ddlsearchsector.Items.Insert(0, New ListItem("----Select----", "0"))

    End Sub
    Protected Sub btnAdditem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdditem.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.unitModal.Show()
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim dt As New DataTable
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        Try
            dt = Unit.getAllUnitById(Convert.ToInt32(gvunitdetails.DataKeys(gvrow.RowIndex).Value.ToString()))
            btnUpdate.Visible = True
            btnSave.Visible = False
            clearFields()
            ddldistrict.SelectedValue = dt.Rows(0).Item(3)
            fillDivision()
            ddldivision.SelectedValue = dt.Rows(0).Item(2)
            fillSector()
            ddlSector.SelectedValue = dt.Rows(0).Item(1)
            txtUnit.Text = dt.Rows(0).Item(4)
            txtCode.Text = dt.Rows(0).Item(5)
            lblId.Value = gvunitdetails.DataKeys(gvrow.RowIndex).Value.ToString()
            Me.unitModal.Show()
        Catch ex As Exception

        End Try
       
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        item.Deleteitem(Convert.ToInt32(gvunitdetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Private Sub clearFields()
        If ddlSector.SelectedIndex <> -1 Then
            ddlSector.Items.Clear()
        End If
        If ddldivision.SelectedIndex <> -1 Then
            ddldivision.Items.Clear()
        End If
        If ddldistrict.SelectedIndex <> -1 Then
            ddldistrict.SelectedIndex = 0
        End If
        ddlSector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddldivision.Items.Insert(0, New ListItem("----Select----", "0"))
        If ddlsearchDistrict.SelectedIndex <> -1 Then
            ddlsearchDistrict.SelectedIndex = 0
        End If
        If ddlsearchDivision.SelectedIndex <> -1 Then
            ddlsearchDivision.Items.Clear()
        End If
        If ddlsearchsector.SelectedIndex <> -1 Then
            ddlsearchsector.Items.Clear()
        End If
        ddlsearchsector.Items.Insert(0, New ListItem("----Select----", "0"))
        ddlsearchDivision.Items.Insert(0, New ListItem("----Select----", "0"))
        txtUnit.Text = ""
        txtCode.Text = ""
        lblmsg.Text = ""
    End Sub
    Private Sub bindGrid()
        If Unit.getAllUnit().Rows.Count > 0 Then
            gvunitdetails.DataSource = Unit.getAllUnit()
            gvunitdetails.DataBind()
        Else
            '  LabelMsg.Text = "no"
        End If
       
    End Sub
   

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(Convert.ToInt32(ddlSector.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldivision.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldistrict.SelectedValue.ToString()))
        arrIn.Add(txtUnit.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Unit.SaveUnit(arrIn) Then
            clearFields()
            bindGrid()
            unitModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList
        arrIn.Add(Convert.ToInt32(lblId.Value))
        arrIn.Add(Convert.ToInt32(ddlSector.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldivision.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddldistrict.SelectedValue.ToString()))
        arrIn.Add(txtUnit.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Unit.SaveUnit(arrIn) Then
            clearFields()
            bindGrid()
            unitModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        Dim dt As New DataTable
        dt = Unit.getAllUnitBySearchId(txtSearchUnit.Text.ToString(), Convert.ToInt32(Convert.ToInt32(ddlsearchDistrict.SelectedValue.ToString())), Convert.ToInt32(Convert.ToInt32(ddlsearchDivision.SelectedValue.ToString())), Convert.ToInt32(Convert.ToInt32(ddlsearchsector.SelectedValue.ToString())))

        gvunitdetails.DataSource = dt
        gvunitdetails.DataBind()
      
    End Sub

    Protected Sub ddlsearchDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsearchDivision.SelectedIndexChanged
        fillSearchSector()
    End Sub

    Protected Sub ddlsearchDistrict_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsearchDistrict.SelectedIndexChanged
        fillSearchDivision()

    End Sub

    Protected Sub ddldistrict_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldistrict.SelectedIndexChanged
        fillDivision()
        unitModal.Show()
    End Sub

    Protected Sub ddldivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldivision.SelectedIndexChanged
        fillSector()
        unitModal.Show()
    End Sub
End Class