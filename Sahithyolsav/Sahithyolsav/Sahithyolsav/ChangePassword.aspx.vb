Imports ConnectionLib
Public Class ChangePassword1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
       
        If ConnectionLib.UserManagement.UpdatePassword(ConnectionLib.UserManagement.UserID, txt_npassword.Text.Trim) Then
            lbl_msg.Text = "Password changed Successfully"
        Else
            lbl_msg.Text = "Failed Please Try Again Later"
        End If
      
    End Sub

End Class