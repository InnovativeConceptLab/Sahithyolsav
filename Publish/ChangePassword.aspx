<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePassword.aspx.vb" Inherits="Sahithyolsav.ChangePassword1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
   
 
   
   
</head>
<body>
    <form id="form1" runat="server">
  
   
      <table >
                            <tr>
                                <td  style="color:#000000; font-size:12px;font-weight:bold; text-align: left;">
                                    New Password</td>
                                <td>
      
        <asp:TextBox ID="txt_npassword" runat="server" TextMode="Password"></asp:TextBox>
            &nbsp;</td>
                                <td>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
            ControlToValidate="txt_npassword" ErrorMessage="Please enter New password" 
             ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                        <tr>
                            <td style="color:#000000; font-size:12px;font-weight:bold">Confirm Password</td>
                            <td>

        <asp:TextBox ID="txt_ccpassword" runat="server" TextMode="Password"></asp:TextBox>   

                            </td>
                            <td>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
            ControlToValidate="txt_ccpassword"
            ErrorMessage="Please enter Confirm  password" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            &nbsp;</td>
                        </tr>
                        <tr id="trErrorMessage" >
                            <td style="color:#FF0000; font-size:14px; font-weight:bold; font-family: 'Times New Roman', Times, serif; text-align: right;" 
                                colspan="2">
    <asp:Label ID="lbl_msg" Font-Bold="True" ForeColor="#FF3300" runat="server" Text=""></asp:Label>
      <asp:ImageButton ID="btn_update" runat="server" ImageUrl="~/image/update.gif" Style="height: 25px" />
                               </td>
                               <td>
            <asp:CompareValidator ID="CompareValidator1" runat="server"
            ControlToCompare="txt_npassword" ControlToValidate="txt_ccpassword"
            ErrorMessage="Password Mismatch" ForeColor="#FF3300"></asp:CompareValidator>
            &nbsp;</td>
                        </tr>
                        <tr >
                            <td align="right" colspan="2">
                                &nbsp;</td>
                        </tr>
                        </table>
    </form>
</body>
</html>
