<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmProgramSettings.aspx.vb" Inherits="Sahithyolsav.frmProgramSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="levelUpdatePnel" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlContent" runat="server" Visible="true">
                <div class="bannerTableStyle" style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td align="center" style="width: 10%">
                                <asp:Button ID="btnAdd" Text="Add" runat="server" />
                            </td>
                            <td align="center" style="width: 90%; font-weight: bold;">
                                <asp:Label runat="server" ID="participantHeader" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
             
                <div style="width: 100%;" class="rounded_corners">
                    <asp:GridView runat="server" ID="gvCodeLtterMapdetails" DataKeyNames="CodeLetterMapID"
                        AutoGenerateColumns="false" Width="100%" HeaderStyle-Height="30px" EmptyDataText="No Data Seleted">
                        <RowStyle BackColor="#EFF3FB" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SlNo#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="5px" ForeColor="White" />
                                <ItemStyle Width="5px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="vchPartcipantName" HeaderText="Participant Name" />
                            <asp:BoundField DataField="Participant From" HeaderText="Participant From" />
                            <asp:BoundField DataField="vchSectionName" HeaderText="Section Name" />
                            <asp:BoundField DataField="vchItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="vchChessNo" HeaderText="Chess No" />
                            <asp:BoundField DataField="CodeLetter" HeaderText="Code Letter" />
                            <asp:TemplateField HeaderText="DistrictID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblintParticipantListId" runat="server" Text='<%# Bind("intParticipantListId") %>'></asp:Label>
                                    <asp:Label ID="lblintItemId" runat="server" Text='<%# Bind("intItemId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                                <ItemTemplate>
                                   <%-- <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                        Height="25" OnClick="imgEdit_Click" />--%>
                                  <%--  <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                        Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this section ?')" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Modal Popup-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="pgrmsettingModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="350px" Width="420px"
                Style="display: block">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 420px; height: 100%"
                    class="" cellpadding="5" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            <asp:HiddenField ID="hintParticipantListId" runat="server" />
                            Participant Details
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Program Date
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtPgrmDate" runat="server" Width="90%" Enabled="false" />
                            <asp:CalendarExtender ID="CalPgrmDate" TargetControlID="txtPgrmDate" PopupButtonID="imgbtnCalendar" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Last Date of Entry
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtSection" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/image/save.gif" OnClientClick="return ValidateForm()" />
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/image/update.gif" OnClientClick="return ValidateForm()"
                                Style="height: 25px" />
                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/image/cancel.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
