<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSection.aspx.vb" Inherits="Sahithyolsav.frmSection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ValidateForm() {
            if ($.trim(document.getElementById("txtSectionName").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Section Name is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtLevel").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Section level is mandatory");
                return false;
            }

            $("#lblmsg").css("display", "none");
            $("#lblmsg").css("color", "black");
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="sectionUpdatePannel" runat="server">
        <ContentTemplate>
            <div class="bannerTableStyle" style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnAddSection" runat="server" Text="Add Section" />
                        </td>
                        <td style="width: 90%; font-weight: bold; padding-left: 35%; color: #FFFFFF;">
                            Section
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Grid -->
            <div style="width: 100%; height: 300px;" class="rounded_corners">
                <asp:GridView runat="server" ID="gvusectiondetails" DataKeyNames="intSectionID" AutoGenerateColumns="False"
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
                        <asp:BoundField DataField="vchSectionName" HeaderText="Section Name" >
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="intSectionLevel" HeaderText="Section Level" >
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchSectionCode" HeaderText="Section Code" >
                        <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgEdit_Click" title="Edit"/>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                    Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this section ?')" title="Delete"/>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- Modal Popup-->
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="sectionModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="220px" Width="400px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            Section Details
                        </td>
                    </tr>
                    <tr>
                        <asp:HiddenField ID="lblId" runat="server" />
                        <td align="center" style="width:30%">
                            Section Name
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtSectionName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Section Level
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtLevel" runat="server" onkeypress="return isNumberKey(event);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Section Code
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/image/save.gif" OnClientClick="return ValidateForm()" />
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/image/update.gif" OnClientClick="return ValidateForm()"
                                Style="height: 25px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
