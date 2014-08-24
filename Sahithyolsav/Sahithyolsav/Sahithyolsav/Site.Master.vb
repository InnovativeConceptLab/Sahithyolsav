Imports ConnectionLib
Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        menuManage()
        If ConnectionLib.UserManagement.Userlogin Then
            loginInfo.Text = "Welcome  " + ConnectionLib.UserManagement.UserName + "  ,"
            logout.Text = "Logout"
        End If
    End Sub

    Protected Sub logout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles logout.Click
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub
    Private Sub menuManage()
        If UserManagement.UserTypeId = 99 Then
            limenu1.Visible = True
        Else
            limenu1.Visible = False
        End If
        If UserManagement.UserTypeId = 5 Then
            limenu2.Visible = False
        Else
            limenu2.Visible = True
        End If
    End Sub

End Class