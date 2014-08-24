<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmUsers.aspx.vb" Inherits="Sahithyolsav.frmUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ddl
        {
            width: 113px;
            background-color: #ffffff;
            color: #507cd1;
            border: 1px solid #507cd1;
            border-radius: 3px;
        }
    </style>
    <script src="Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ValidateForm() {
            if ($.trim(document.getElementById("txtUserName").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("User Name is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtfname").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("First Name is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtlname").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Last Name is mandatory");
                return false;
            }
            $("#lblmsg").css("display", "none");
            $("#lblmsg").css("color", "black");
            return true;
        }
        function SetVisibilityofCombos() {
            var level = document.getElementById('ddlLevel').value;
            var tr = document.getElementById('trDistrict');
            var trDiv = document.getElementById('trDivision');
            var trSec = document.getElementById('trSector');
            var trUnt = document.getElementById('trUnit');
            //var tr2 = document.getElementById('trhtl1');
            if (level == 0 || level == 1) {
                tr.style.display = "none";
                trDiv.style.display = "none";
                trSec.style.display = "none";
                trUnt.style.display = "none";
            }
            else {
                tr.style.display = "";
                trDiv.style.display = "";
                trSec.style.display = "";
                trUnt.style.display = "";
            }

            // tr2.style.display = "";

        }
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="userUpdatePannel" runat="server">
        <ContentTemplate>
            <div class="bannerTableStyle" style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnAddUser" runat="server" Text="Add User" />
                        </td>
                        <td style="width: 90%; font-weight: bold; padding-left: 35%; color: #FFFFFF;">
                            Users
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Grid -->
            <div style="width: 100%; height: 100%;" class="rounded_corners">
                <asp:GridView runat="server" ID="gvuserdetails" DataKeyNames="intUserId" AutoGenerateColumns="False"
                    Width="100%" HeaderStyle-Height="30px">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SlNo">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5px" ForeColor="White" />
                            <ItemStyle Width="5px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="vchUserName" HeaderText="UserName">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchFName" HeaderText="FirstName">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchLName" HeaderText="LastName">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="intUserId" HeaderText="" Visible="false" />
                        <asp:BoundField DataField="intUserTypeId" HeaderText="" Visible="false" />
                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="true">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgEdit_Click" title="Edit" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                    Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this User ?')"
                                    title="Delete" />
                                <asp:ImageButton ID="imgChangStatus" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgChangStatus_Click" title="Change Status" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- Modal Popup-->
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="240px" Width="400px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            User Details
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            <asp:HiddenField ID="lblId" runat="server" />
                            UserName
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtUserName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%" >
                            FirstName
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtfname" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            LastName
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtlname" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Select:
                        </td>
                        <td style="width:70%">
                            <asp:DropDownList ID="ddlLevelMapIdCombo" runat="server" AutoPostBack="false" Width="97%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/image/save.gif" OnClientClick="return ValidateForm()" />
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/image/update.gif" OnClientClick="return ValidateForm()" />
                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/image/cancel.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblmsg" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="targetButton2" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalMapUser" runat="server" TargetControlID="targetButton2"
                PopupControlID="PnlPopupUserMap" CancelControlID="BtnCancelMap" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PnlPopupUserMap" runat="server" BackColor="White" Height="103px" Width="400px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="5" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="3" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            UserMap Details
                        </td>
                    </tr>
                    <tr height="32px">
                        <asp:HiddenField ID="lblUserID" runat="server" />
                        <td align="right" height="13px">
                            Select Level :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="ddl" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDistrict" height="33px" runat="server">
                        <td align="right">
                            Select District :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trDivision" height="33px" runat="server">
                        <td align="right">
                            Select Division :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDiviosn" runat="server" AutoPostBack="True" CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trSector" height="33px" runat="server">
                        <td align="right">
                            Select Sector :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSector" runat="server" AutoPostBack="True" CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trUnit" height="33px" runat="server">
                        <td align="right">
                            Select Unit :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr height="32px">
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImageSaveMapUser" runat="server" ImageUrl="~/image/save.gif" />
                            <asp:ImageButton ID="BtnCancelMap" runat="server" ImageUrl="~/image/cancel.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="LblMapmsg" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
