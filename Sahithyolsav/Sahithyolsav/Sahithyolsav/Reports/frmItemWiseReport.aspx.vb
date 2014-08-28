Imports ConnectionLib
Imports System.Data.Sql
Imports System.Web.UI.Control
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
        dt = Section.getAllSections()
        ddlSearchSection.DataSource = dt
        ddlSearchSection.DataTextField = "vchSectionName"
        ddlSearchSection.DataValueField = "intSectionID"
        ddlSearchSection.DataBind()
        ddlSearchSection.Items.Insert(0, New ListItem("----Select----", "0"))
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
        Dim dt As New DataTable
        Dim arrIn As New ArrayList()
        arrIn.Add(1)
        arrIn.Add(Convert.ToInt32(ddlSearchSection.SelectedValue.ToString()))
        arrIn.Add(Convert.ToInt32(ddlSearchItem.SelectedValue.ToString()))
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
            HeaderCell1.Text = "Item Wise Report"
            HeaderCell1.ForeColor = Drawing.Color.Black
            HeaderCell1.ColumnSpan = colCount + 1
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow1.Cells.Add(HeaderCell1)
            gvReport.Controls(0).Controls.AddAt(1, HeaderGridRow1)
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
End Class