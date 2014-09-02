Imports ConnectionLib
Imports System.Data.Sql
Public Class frmGenerateChessNum
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGenerte_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerte.Click
        If ConnectionLib.Participant.GenerateChessNumbers(UserManagement.UserTypeId) = True Then
            lblchessNumMsg.Text = "CHESS NUMBER GENERATED SUCSESSFULLY"
            lblchessNumMsg.ForeColor = Drawing.Color.Green
            lblchessNumMsg.Visible = True
        End If
    End Sub
End Class