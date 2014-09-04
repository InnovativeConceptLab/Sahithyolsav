Imports ConnectionLib
Imports System.Data.Sql
Imports System.Web.UI.Control
Public Class frmReports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ConnectionLib.UserManagement.Userlogin = False Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            fillReports()
            bindHeader()
            fillSearchSection()
            divgridRport.Visible = False
        End If
    End Sub
    Private Sub fillReports()
        Dim dt As New DataTable
        dt = Reports.GetReportDetails()
        ddlReports.DataSource = dt
        ddlReports.DataTextField = "vchReportName"
        ddlReports.DataValueField = "intReportId"
        ddlReports.DataBind()
        ddlReports.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub

    Protected Sub ddlReports_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlReports.SelectedIndexChanged
        Dim dt As New DataTable
        divgridRport.Visible = False
        dt = Reports.GetReportById(Convert.ToInt32(ddlReports.SelectedValue.ToString()))
        btnPublish.Visible = False
        lblmsg.Visible = False
        If dt.Rows(0).ItemArray(3).ToString = "1" Then
            divItemSection.Visible = True
            ddlSearchItem.Enabled = True
            fillSearchSection()
            If ddlReports.SelectedValue.ToString() = "3" Then
                ddlSearchItem.Enabled = False
                ddlSearchItem.Items.Clear()
            End If
            'ElseIf dt.Rows(0).ItemArray(3).ToString = "2" Then
            '    divItemSection.Visible = True
            '    ddlSearchItem.Enabled = False
            '    fillSearchSection()
        Else
            divItemSection.Visible = False
            ddlSearchItem.Enabled = True
            Dim dtRpt As New DataTable
            Dim arrIn As New ArrayList()
            arrIn.Add(Convert.ToInt32(ddlReports.SelectedValue.ToString()))
            arrIn.Add(0)
            arrIn.Add(0)
            arrIn.Add(UserManagement.UserMapId)
            arrIn.Add(UserManagement.UserTypeId)
            arrIn.Add(0)
            dtRpt = Reports.ExecuteReportQuery(arrIn)
            bindReportGrid(dtRpt)
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
    Private Sub fillSearchSection()
        Dim dt As New DataTable
        dt = Section.getAllSections()
        ddlSearchSection.DataSource = dt
        ddlSearchSection.DataTextField = "vchSectionName"
        ddlSearchSection.DataValueField = "intSectionID"
        ddlSearchSection.DataBind()
        ddlSearchSection.Items.Insert(0, New ListItem("----Select----", "0"))
    End Sub

    Protected Sub ddlSearchSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSearchSection.SelectedIndexChanged
        Dim dt As New DataTable
        btnPublish.Visible = False
        lblmsg.Visible = False
        If ddlReports.SelectedValue <> "3" Then
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
        End If

    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click

        Dim dt As New DataTable
        Dim arrIn As New ArrayList()

        If ddlReports.SelectedValue.ToString() = "1" Then
            btnPublish.Visible = True
        Else
            btnPublish.Visible = False
        End If
        arrIn.Add(Convert.ToInt32(ddlReports.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString()))
        If ddlReports.SelectedValue.ToString = "3" Then
            arrIn.Add(0)
        Else
            arrIn.Add(Convert.ToInt32(ddlSearchItem.SelectedValue.ToString()))
        End If

        arrIn.Add(UserManagement.UserMapId)
        arrIn.Add(UserManagement.UserTypeId)
        arrIn.Add(0)
        dt = Reports.ExecuteReportQuery(arrIn)
        bindReportGrid(dt)

    End Sub
    Private Sub bindReportGrid(ByVal dt As DataTable)
        'Code to bind grid,dt will contain grid data
        divgridRport.Visible = True
        If Not (IsDBNull(dt)) Then
            If dt.Rows.Count > 0 Then
                ViewState("ColCount") = dt.Columns.Count
                gvReport.DataSource = dt
                gvReport.DataBind()
            Else
                gvReport.DataSource = dt
                gvReport.DataBind()
            End If
        Else
            gvReport.DataSource = dt
            gvReport.DataBind()
        End If
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered
    End Sub
    Protected Sub Unnamed1_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub CreateHeader(ByVal StrHeaderText As String, ByVal ColCount As Integer)
        Dim rowHeader As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

        Dim newCells1 As New Literal()
        newCells1.Text = StrHeaderText
        Dim headerCell1 As New TableHeaderCell()

        headerCell1.ForeColor = System.Drawing.Color.Black
        headerCell1.HorizontalAlign = HorizontalAlign.Center
        headerCell1.Font.Size = 14
        headerCell1.Controls.Add(newCells1)
        headerCell1.ColumnSpan = ColCount
        rowHeader.Cells.Add(headerCell1)
        rowHeader.Visible = True
        gvReport.Controls(0).Controls.AddAt(0, rowHeader)

    End Sub

    Protected Sub imgPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPdf.Click
        ' CreateHeader(ddlReports.SelectedItem.ToString, Convert.ToInt32(ViewState("ColCount").ToString) + 1)
        ConnectionLib.Utilities.ExportGrid(gvReport, "pdf", ddlReports.SelectedItem.ToString)

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
            gvReport.Controls(0).Controls.AddAt(0, HeaderGridRow)

            Dim HeaderGrid1 As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow1 As New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell1 As New TableCell()
            HeaderCell1.Text = ddlReports.SelectedItem.ToString
            HeaderCell1.ForeColor = Drawing.Color.Black
            HeaderCell1.ColumnSpan = colCount + 1
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow1.Cells.Add(HeaderCell1)
            gvReport.Controls(0).Controls.AddAt(1, HeaderGridRow1)
        End If
    End Sub

    Protected Sub btnPublish_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPublish.Click
        If ddlReports.SelectedValue.ToString() = 1 Then
            If ddlSearchItem.SelectedValue <> "0" Then
                If ConnectionLib.item.updateItem(Convert.ToInt32(ddlSearchItem.SelectedValue.ToString())) = True Then
                    lblmsg.Visible = True
                    lblmsg.Text = "RESULT PUBLISHED SUCSESSFULLY"
                    lblmsg.ForeColor = Drawing.Color.Green
                Else
                    lblmsg.Visible = True
                    lblmsg.Text = "RESULT PUBLIS FAILED"
                    lblmsg.ForeColor = Drawing.Color.Red
                End If
            End If
        End If
    End Sub
End Class