Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Data.SqlClient
Imports System.Globalization

Imports System.Text
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.Threading
Imports System.Diagnostics
Imports System.Collections.Generic
Public Class frmTestPdf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim table As New DataTable()
            table.Columns.Add("Dosage", GetType(Integer))
            table.Columns.Add("Drug", GetType(String))
            table.Columns.Add("Patient", GetType(String))

            '
            ' Here we add five DataRows.
            '
            table.Rows.Add(25, "Indocin", "David")
            table.Rows.Add(50, "Enebrel", "Sam")
            table.Rows.Add(10, "Hydralazine", "Christoff")
            table.Rows.Add(21, "Combivent", "Janet")
            table.Rows.Add(100, "Dilantin", "Melanie")
            gv.DataSource = table
            gv.DataBind()
        End If
    End Sub
    Private Sub print()
        Dim form As New HtmlForm
        form.Controls.Add(gv)
        Dim sw As New StringWriter()
        Dim hTextWriter As New HtmlTextWriter(sw)
        form.Controls(0).RenderControl(hTextWriter)
        Dim html As String = sw.ToString()
        Dim Doc As New Document()

        PdfWriter.GetInstance(Doc, New FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\AmitJain.pdf", FileMode.Create))
        Doc.Open()
        Dim xmlReader As New System.Xml.XmlTextReader(New StringReader(html))
        HtmlParser.Parse(Doc, xmlReader)

        Doc.Close()
        Dim Path As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\AmitJain.pdf"
        ShowPdf(Path)
    End Sub

    Private Sub ShowPdf(ByVal strS As String)
        Response.ClearContent()
        Response.ClearHeaders()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", Convert.ToString("attachment; filename=") & strS)
        Response.TransmitFile(strS)
        Response.[End]()
        'Response.WriteFile(strS);
        Response.Flush()
        Response.Clear()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        print()
    End Sub
End Class