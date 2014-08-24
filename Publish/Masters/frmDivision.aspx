<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDivision.aspx.vb" Inherits="Sahithyolsav.frmDivision" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ValidateForm() {
            try {

                var DistName = document.getElementById("txtDivsnName").value;
                //  var DistCode = document.getElementById("txtDistrictCodeU").value;

                if (DistName == "") {
                    alert("Division Name is mandatory");
                    document.getElementById("txtDivsnName").focus();
                    return false;
                }
                //                if (DistCode == "") {
                //                    alert("District Code is mandatory");
                //                    document.getElementById("txtDistrictCodeU").focus();
                //                    return false;
                //                }
                else {
                    return true;
                }
            }


            catch (err) {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="userUpdatePannel" runat="server">
        <ContentTemplate>
            <!-- Grid -->
            <div class="bannerTableStyle" style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Button ID="btnAddDivision" runat="server" Text="Add Division" />
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                        <td width="25%" style="color: #FFFFFF">
                            DIVISION
                        </td>
                        <td width="25%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="searchTableStyle" style="width: 99.5%; height: 22px;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 33%">
                            Select District
                        </td>
                        <td align="center" style="width: 33%">
                            <asp:DropDownList ID="ddlDistrictS" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 33%">
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/image/srch-btn2.jpg" ValidationGroup="11" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; height: 300px;" class="rounded_corners">
                <asp:GridView runat="server" ID="gvDivision" DataKeyNames="intDivisionId" AutoGenerateColumns="False"
                    Width="100%">
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
                        <asp:BoundField DataField="vchDivisionName" HeaderText="DivisionName">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vchDivisionCode" HeaderText="DivisionCode">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="intDistrictId" HeaderText="" Visible="false" />
                        <asp:BoundField DataField="vchDistrictName" HeaderText="District" Visible="true">
                            <HeaderStyle ForeColor="White" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                    Height="25" OnClick="imgEdit_Click" title="Edit" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                    Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this User ?')"
                                    title="Delete" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DistrictID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDistrictID" runat="server" Text='<%# Bind("intDistrictID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <!-- Modal Popup-->
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="DivisionModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="195px" Width="400px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            Division Details
                        </td>
                    </tr>
                    <tr>
                        <asp:HiddenField ID="lblId" runat="server" />
                        <td align="center" style="width: 30%">
                            District
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="97%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            DivisionName
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtDivsnName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 30%">
                            DivisionCode
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtDivsnCode" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
