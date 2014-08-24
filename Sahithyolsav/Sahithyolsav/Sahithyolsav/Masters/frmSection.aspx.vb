Imports ConnectionLib
Imports System.Data.Sql
Public Class frmSection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            bindGrid()
        End If
    End Sub
    Private Function getAllSections() As DataTable
        Dim dt As New DataTable
        dt = ConnectionLib.Section.getAllSections()
        Return dt
    End Function
    Private Sub bindGrid()
        gvusectiondetails.DataSource = getAllSections()
        gvusectiondetails.DataBind()
    End Sub
    Protected Sub imgEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        clearFields()
        btnUpdate.Visible = True
        btnSave.Visible = False
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        txtSectionName.Text = gvrow.Cells(1).Text
        txtSectionName.Enabled = False
        txtLevel.Text = gvrow.Cells(2).Text
        txtCode.Text = gvrow.Cells(3).Text
        lblId.Value = gvusectiondetails.DataKeys(gvrow.RowIndex).Value.ToString()
        Me.sectionModal.Show()
    End Sub
    Protected Sub delete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim btndetails As ImageButton = TryCast(sender, ImageButton)
        Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
        Section.DeleteSection(Convert.ToInt32(gvusectiondetails.DataKeys(gvrow.RowIndex).Value.ToString()))
        bindGrid()
    End Sub
    Private Sub clearFields()
        txtSectionName.Text = ""
        txtCode.Text = ""
        txtLevel.Text = ""
        lblmsg.Text = ""
        txtSectionName.Enabled = True
    End Sub

    Protected Sub btnAddSection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddSection.Click
        clearFields()
        btnUpdate.Visible = False
        btnSave.Visible = True
        Me.sectionModal.Show()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Dim arrIn As New ArrayList
        If Section.validateSectionName(txtSectionName.Text) = False Then
            lblmsg.Text = "Section Name already exists."
            lblmsg.ForeColor = Drawing.Color.Red
            Me.sectionModal.Show()
            Return
        End If
        arrIn.Add(0)
        arrIn.Add(txtSectionName.Text)
        arrIn.Add(txtLevel.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Section.SaveSection(arrIn) = True Then
            clearFields()
            bindGrid()
            sectionModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUpdate.Click
        Dim arrIn As New ArrayList
     
        arrIn.Add(Convert.ToInt32(lblId.Value.ToString))
        arrIn.Add(txtSectionName.Text)
        arrIn.Add(txtLevel.Text)
        arrIn.Add(txtCode.Text)
        If ConnectionLib.Section.SaveSection(arrIn) = True Then
            clearFields()
            bindGrid()
            sectionModal.Hide()
        Else
            lblmsg.Text = "Saving Failed.Try again later"
            lblmsg.ForeColor = Drawing.Color.Red
        End If
    End Sub
End Class