<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSector.aspx.vb" Inherits="Sahithyolsav.frmSector" %>

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
            if ($.trim(document.getElementById("ddldistrict").value) == "0") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select a district");
                return false;
            }
            if ($.trim(document.getElementById("ddldivision").value) == "0") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select a division");
                return false;
            }
            if ($.trim(document.getElementById("txtSectorName").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Sector Name is mandatory");
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
    <asp:UpdatePanel ID="sectorUpdatePannel" runat="server">
        <ContentTemplate>
            <div class="bannerTableStyle" style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnAdditem" runat="server" Text="Add sector" Style="margin-bottom: 5px;" />
                        </td>
                        <td style="width: 90%; font-weight: bold; padding-left: 35%;">
                            Sector
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Grid -->
            <div class="searchTableStyle" style="width: 99.5%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 14%">
                            Select a District
                        </td>
                        <td align="center" style="width: 14%">
                            <asp:DropDownList ID="ddlsearchDistrict" runat="server" Width="90%" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 14%">
                            Select a Division
                        </td>
                        <td align="center" style="width: 14%">
                            <asp:DropDownList ID="ddlsearchDivision" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 14%">
                            Sector Name
                        </td>
                        <td align="center" style="width: 14%">
                            <asp:TextBox runat="server" ID="txtsearchsectorName"></asp:TextBox>
                        </td>
                        <td align="center" style="width: 14%">
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/image/submit.jpg" ValidationGroup="J"
                                Height="20px" Width="70px" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; height: 300px;" class="rounded_corners">
                <asp:GridView runat="server" ID="gvsectordetails" DataKeyNames="intSectorId" AutoGenerateColumns="false"
                    Width="100%" HeaderStyle-Height="30px">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                     <asp:TemplateField HeaderText="SlNo"> <ItemTemplate> <%#Container.DataItemIndex+1 %> </ItemTemplate> 
                       <HeaderStyle Width="5px" />
                       <ItemStyle Width="5px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="vchSectorName" HeaderText="Sector Name" />
                        <asp:BoundField DataField="vchSectionCode" HeaderText="Sector Code" />
                        <asp:BoundField DataField="vchDivisionName" HeaderText="Division" />
                        <asp:BoundField DataField="vchDistrictName" HeaderText="District" />
                        <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgEdit_Click" title="Edit"/>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                    Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this section ?')" title="Delete"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- Modal Popup-->
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="sectorModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground" >
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="400px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            Sector Details
                        </td>
                        <asp:HiddenField ID="lblId" runat="server" />
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Select District
                        </td>
                        <td style="width:70%">
                            <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" Width="97%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Select Division
                        </td>
                        <td style="width:70%">
                            <asp:DropDownList ID="ddldivision" runat="server" Width="97%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Sector Name
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="txtSectorName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width:30%">
                            Sector Code
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
