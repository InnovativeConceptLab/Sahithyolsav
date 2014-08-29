Imports ConnectionLib
Imports System.Data.Sql
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.Web.UI.Control
Imports System.IO
Imports System.Text
Public Class frmItemWiseReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindHeader()
            fillSearchSection()
            '   divgridRport.Visible = False
        End If
    End Sub
    Private Sub fillSearchSection()
        Dim dt As New DataTable
        dt = ConnectionLib.Section.getAllSections()
        ddlSearchSection.DataSource = dt
        ddlSearchSection.DataTextField = "vchSectionName"
        ddlSearchSection.DataValueField = "intSectionID"
        ddlSearchSection.DataBind()
        ' ddlSearchSection.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub

    Private Sub ddlSearchSection_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSearchSection.SelectedIndexChanged
        Dim dt As New DataTable
        If (ddlSearchSection.SelectedValue <> "0") Then
            dt = ConnectionLib.item.getItemsBySectionId(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString()))
            ddlSearchItem.DataSource = dt
            ddlSearchItem.DataTextField = "vchItemName"
            ddlSearchItem.DataValueField = "intItemId"
            ddlSearchItem.DataBind()
        Else
            ddlSearchItem.DataSource = dt
            ddlSearchItem.DataBind()
            '  ddlItem.Items.Insert(0, New ListItem("----Select----", "0"))
        End If
    End Sub

    Protected Sub imgSearch_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        bindReportGrid()
    End Sub
    Private Sub bindReportGrid()
        gvItemDetails.DataSource = Participant.GetProgramChartByItem(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString), Convert.ToInt32(ddlSearchItem.SelectedValue.ToString), UserManagement.UserMapId)
        gvItemDetails.DataBind()
    End Sub
    Protected Sub gvReport_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim colCount As Integer
        colCount = ViewState("ColCount")
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = "SSF SAHITHYOLSAV -2014 " & "[ " & HeaderText() & " ]"
            HeaderCell.ForeColor = Drawing.Color.Black
            HeaderCell.ColumnSpan = colCount + 1
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)
            gvItemDetails.Controls(0).Controls.AddAt(0, HeaderGridRow)

            Dim HeaderGrid1 As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow1 As New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell1 As New TableCell()
            HeaderCell1.Text = "Item Wise Report"
            HeaderCell1.ForeColor = Drawing.Color.Black
            HeaderCell1.ColumnSpan = colCount + 1
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow1.Cells.Add(HeaderCell1)
            gvItemDetails.Controls(0).Controls.AddAt(1, HeaderGridRow1)
        End If
    End Sub
    Private Function HeaderText() As String
        Dim struserLevel As String
        Dim dt As New DataTable
        struserLevel = ""
        If UserManagement.UserTypeId = 1 Then
            struserLevel = "Kerala State Level Reports"
        ElseIf UserManagement.UserTypeId = 2 Then
            dt = District.GetDistricts("", "", UserManagement.UserMapId).Tables(0)
            struserLevel = dt.Rows(0).ItemArray(1).ToString + "- District Level Reports"
        ElseIf UserManagement.UserTypeId = 3 Then
            dt = Division.GetDivision(0, "", "", UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + "- Division Level Reports"
        ElseIf UserManagement.UserTypeId = 4 Then
            dt = Sector.getAllSectorById(UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + "- Sector Level Reports"
        ElseIf UserManagement.UserTypeId = 5 Then
            dt = Unit.getAllUnitById(UserManagement.UserMapId)
            struserLevel = dt.Rows(0).ItemArray(3).ToString + "- Unit Level Reports"
        End If
        Return struserLevel
    End Function
    Private Sub bindHeader()
        participantHeader.Text = HeaderText()
    End Sub

    Protected Sub imgPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPdf.Click
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", _
        ' "attachment;filename=GridViewExport.pdf")
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        'gvItemDetails.AllowPaging = False
        gvItemDetails.DataSource = Participant.GetProgramChartByItem(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString), Convert.ToInt32(ddlSearchItem.SelectedValue.ToString), UserManagement.UserMapId)
        gvItemDetails.DataBind()
        ConnectionLib.Utilities.ExportGrid(gvItemDetails, "pdf", "itemchart")
        'gvItemDetails.RenderControl(hw)
        'Dim sr As New StringReader(sw.ToString())
        'Dim pdfDoc As New Document(PageSize.A4, 7.0F, 7.0F, 7.0F, 0.0F)
        'Dim htmlparser As New HTMLWorker(pdfDoc)
        'PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        'pdfDoc.Open()
        'htmlparser.Parse(sr)
        'pdfDoc.Close()
        'Response.Write(pdfDoc)
        'Response.End()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered
    End Sub
    Protected Function GetUrl(ByVal imagepath As String) As String
        Dim splits As String() = Request.Url.AbsoluteUri.Split("/"c)
        If splits.Length >= 2 Then
            Dim url As String = splits(0) & "//"
            For i As Integer = 2 To 2
                url += splits(i)
                url += "/"
            Next
            Return url + imagepath
        End If
        Return imagepath
    End Function
End Class