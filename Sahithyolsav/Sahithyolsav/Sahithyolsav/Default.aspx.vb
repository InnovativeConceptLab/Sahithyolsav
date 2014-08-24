Imports ConnectionLib
Imports System.Reflection.PropertyInfo
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("Invalid") = "yes" Then
            '  System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "ShowLogin('Invalid');", True)
            lbblogin.Text = "Invalid Login Please Try Again"
            lbblogin.ForeColor = Drawing.Color.Red
        Else
            lbblogin.Text = "Login Here ..."
            lbblogin.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    Public Function validateUser(ByVal uname As String, ByVal pwd As String) As Boolean
        Return ConnectionLib.UserManagement.ValidateUser(uname, pwd)
    End Function

    Protected Sub imgLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogin.Click
        If validateUser(txtUserName.Text.ToString, txtPassword.Text.ToString) Then
            Response.Redirect("frmLandingPage.aspx")
        Else
            Response.Redirect("Default.aspx?Invalid=" & "yes")

        End If
    End Sub

    Protected Sub lnkClick_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkClick.Click
        Response.ContentType = "Application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=Test_PDF.pdf")
        Response.TransmitFile(Server.MapPath("~/Files/Sahithyolsavu_manual_2013.pdf"))
        Response.End()
    End Sub
End Class