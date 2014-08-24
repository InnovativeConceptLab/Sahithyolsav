Imports ConnectionLib
Imports System.Data.Sql
Public Class frmItem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillSection()
            bindGrid()
            fillSearchSection()
        End If
    End Sub
    Private Sub fillSection()
        Dim dt As New DataTable
        dt = Section.getAllSections()
        ddlSection.DataSource = dt
        ddlSection.DataTextField = "vchSectionName"
        ddlSection.DataValueField = "intSectionID"
        ddlSection.DataBind()
        ddlSection.Items.Insert(0, New ListItem("----Select----", "0"))
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
    Private Sub bindGrid()
        gvusectiondetails.DataSource = item.getAllItems()
        gvusectiondetails.DataBind()
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim dt As New DataTable

        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        dt = item.getItemsById(Convert.ToInt32(gvusectiondetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        '   btnUpdate.Visible = True
        btnSave.Visible = False
        clearFields()
        ddlSection.SelectedValue = dt.Rows(0).Item(3)
        txtItemName.Text = dt.Rows(0).Item(1)
        txtCode.Text = dt.Rows(0).Item(2)
        If dt.Rows(0).Item(5) = "1" Then
            ChkGrpItem.Checked = True
            txtNoofpartcpnts.Enabled = True
        Else
            ChkGrpItem.Checked = False
            txtNoofpartcpnts.Enabled = False
        End If
        txtNoofpartcpnts.Text = dt.Rows(0).Item(6)
        txtMarkFrst.Text = dt.Rows(0).Item(7)
        txtMarkSecnd.Text = dt.Rows(0).Item(8)
        txtMarkThrd.Text = dt.Rows(0).Item(9)
        lblId.Value = gvusectiondetails.DataKeys(gvrow.RowIndex).Value.ToString()
        ' ClientScript.RegisterStartupScript(Me.GetType(), "Hide2", "<script> SetVisibilitytxtNoofpartcpnts();  </script>")
        Me.itemModal.Show()
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        item.Deleteitem(Convert.ToInt32(gvusectiondetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Private Sub clearFields()
        txtItemName.Text = ""
        txtCode.Text = ""
        lblmsg.Text = ""
        ddlSection.SelectedIndex = 0
        ChkGrpItem.Checked = False
        txtNoofpartcpnts.Text = ""
        txtNoofpartcpnts.Enabled = False
        txtMarkFrst.Text = ""
        txtMarkSecnd.Text = ""
        txtMarkThrd.Text = ""
    End Sub

    Protected Sub btnAdditem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdditem.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.itemModal.Show()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        arrIn.Add(0)
        arrIn.Add(txtItemName.Text)
        arrIn.Add(txtCode.Text)
        arrIn.Add(Convert.ToInt32(ddlSection.SelectedValue.ToString()))
        If ChkGrpItem.Checked = True Then
            arrIn.Add(True)
            arrIn.Add(txtNoofpartcpnts.Text.ToString)
        Else
            arrIn.Add(False)
            arrIn.Add(1)
        End If
        arrIn.Add(txtMarkFrst.Text)
        arrIn.Add(txtMarkSecnd.Text)
        arrIn.Add(txtMarkThrd.Text)
        If ConnectionLib.item.SaveItem(arrIn) = True Then
            clearFields()
            bindGrid()
            itemModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        If ddlSearchSection.SelectedValue <> "0" Then
            Dim dt As New DataTable
            dt = item.getItemsBySectionId(Convert.ToInt32(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString())))
            gvusectiondetails.DataSource = dt
            gvusectiondetails.DataBind()
        Else
            bindGrid()
        End If

    End Sub

    Protected Sub ddlSearchSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSearchSection.SelectedIndexChanged

    End Sub

    Private Sub ChkGrpItem_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChkGrpItem.CheckedChanged
        If ChkGrpItem.Checked = True Then
            txtNoofpartcpnts.Enabled = True
        Else
            txtNoofpartcpnts.Enabled = False
            txtNoofpartcpnts.Text = ""
        End If
        itemModal.Show()
    End Sub
End Class