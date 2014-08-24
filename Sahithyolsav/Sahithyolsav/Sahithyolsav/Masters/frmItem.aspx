<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmItem.aspx.vb" Inherits="Sahithyolsav.frmItem" %>

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
            if ($.trim(document.getElementById("ddlSection").value) == "0") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select a section");
                return false;
            }
            if ($.trim(document.getElementById("txtItemName").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Item Name is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtMarkFrst").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Marks For First is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtMarkSecnd").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Marks For Second is mandatory");
                return false;
            }
            if ($.trim(document.getElementById("txtMarkThrd").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Marks For Third is mandatory");
                return false;
            }
            if (document.getElementById("ChkGrpItem").checked) {
                if (document.getElementById("txtNoofpartcpnts").value == "") {
                    $("#lblmsg").css("display", "block");
                    $("#lblmsg").css("color", "red");
                    $("#lblmsg").text("Number Of Participants is mandatory");
                    return false;
                }

            }
            $("#lblmsg").css("display", "none");
            $("#lblmsg").css("color", "black");
            return true;
        }
        function SetVisibilityNoOfPartcpnts() {

            var prmtype = document.getElementById('ChkGrpItem');

            if (prmtype.checked == true) {
                var binvalue = document.getElementById('Noofpartcpnts');
                binvalue.style.display = "";
            }
            else {
                var binvalue = document.getElementById('Noofpartcpnts');
                binvalue.style.display = "none";
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="itemUpdatePannel" runat="server">
        <ContentTemplate>
            <!-- Grid -->
            <div class="bannerTableStyle" style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnAdditem" runat="server" Text="Add Item" Style="margin-bottom: 5px;" />
                        </td>
                        <td style="width: 90%; font-weight: bold; padding-left: 35%; color: #FFFFFF;">
                            ITEMS
                        </td>
                    </tr>
                </table>
            </div>
            <div class="searchTableStyle" style="width: 99.5%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 33%">
                            Select a Section
                        </td>
                        <td align="center" style="width: 33%">
                            <asp:DropDownList ID="ddlSearchSection" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 33%">
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/image/submit.jpg" ValidationGroup="J"
                                Height="20px" Width="70px" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%" class="rounded_corners">
                <asp:GridView runat="server" ID="gvusectiondetails" DataKeyNames="intItemId" AutoGenerateColumns="False"
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
                        <asp:BoundField DataField="vchItemName" HeaderText="Item Name">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchSectionName" HeaderText="Section Name">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchItemCode" HeaderText="Item code">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgEdit_Click" title="Edit" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                    Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this section ?')"
                                    title="Delete" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- Modal Popup-->
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="itemModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="372px" Width="500px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="3" style="height: 7%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            Item Details
                        </td>
                    </tr>
                    <tr>
                        <asp:HiddenField ID="lblId" runat="server" />
                        <td align="center" style="width: 30%">
                            Select Section
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlSection" runat="server" Width="96%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            Item Name
                        </td>
                        <td style="width: 70%; margin-left: 80px;">
                            <asp:TextBox ID="txtItemName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            Item Code
                        </td>
                        <td style="width: 70%; margin-left: 120px;">
                            <asp:TextBox ID="txtCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            &nbsp;
                        </td>
                        <td style="width: 70%; margin-left: 120px;">
                            <asp:CheckBox ID="ChkGrpItem" runat="server" AutoPostBack="True" Text="Is Group Item" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            No Of Participants
                        </td>
                        <td style="width: 70%; margin-left: 120px;">
                            <asp:TextBox ID="txtNoofpartcpnts" runat="server" Enabled="false" onkeypress="return isNumberKey(event);"
                                PlaceHolder="Enter Number Of Participants" ToolTip="Number Of Participants" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            Marks For First
                        </td>
                        <td style="width: 70%; margin-left: 40px;">
                            <asp:TextBox ID="txtMarkFrst" runat="server" onkeypress="return isNumberKey(event);"
                                TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            Marks For Second
                        </td>
                        <td style="width: 70%; margin-left: 40px;">
                            <asp:TextBox ID="txtMarkSecnd" runat="server" onkeypress="return isNumberKey(event);"
                                TabIndex="2" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            Marks For Third
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtMarkThrd" runat="server" onkeypress="return isNumberKey(event);"
                                TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/image/save.gif" OnClientClick="return ValidateForm()" />
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/image/update.gif" OnClientClick="return ValidateForm()"
                                Visible="false" Style="height: 25px" />
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
