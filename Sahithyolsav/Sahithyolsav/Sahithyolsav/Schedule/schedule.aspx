<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="schedule.aspx.vb" Inherits="Sahithyolsav.schedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <script type="text/javascript">
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }
        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../../image/loading.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="levelUpdatePnel" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlContent" runat="server" Visible="true">
                <div class="bannerTableStyle" style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td align="center" style="width: 100%; font-weight: bold; text-transform: uppercase;">
                                Schedule Program
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div>
                <asp:Button ID="btnAddSch" runat="server" Text="Add Schedule" OnClientClick="loadUpdatePanel()" />
            </div>
            <div style="width: 100%; height: 350px; overflow: auto" class="rounded_corners">
                <asp:GridView runat="server" ID="gvSchedule" DataKeyNames="intShceduleID" AutoGenerateColumns="false"
                    Width="100%" HeaderStyle-Height="30px" EmptyDataText="No Data Seleted">
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
                        <asp:BoundField DataField="vchSectionName" HeaderText="Section Name" />
                        <asp:BoundField DataField="vchItemName" HeaderText="Item Name" />
                        <asp:BoundField DataField="VchStageName" HeaderText="Stage" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                        <asp:BoundField DataField="Time" HeaderText="Time" />
                        <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/edit.gif" runat="server" Width="25"
                                    Height="25" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Modal Popup-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="targetButton" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="scheduleModal" runat="server" TargetControlID="targetButton"
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
                            <asp:HiddenField ID="hintItemId" runat="server" />
                            <asp:HiddenField ID="hCodeLetterMapID" runat="server" />
                            Create a Schedule
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Section Name
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" Width="90%"
                                onchange="ShowLoading()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Select Item
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="false" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Select Stage
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlStage" runat="server" AutoPostBack="false" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Date
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtDate" runat="server" Width="90%" Enabled="true" />
                            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate" PopupButtonID="imgbtnCalendar"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Time
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtTime1" runat="server" Width="20%" Enabled="true" />
                            &nbsp;&nbsp;<asp:TextBox ID="txtTime2" runat="server" Width="20%" Enabled="true" />
                        </td>
                    </tr>
                    <td style="width: 30%" align="right">
                        AM/PM
                    </td>
                    <td style="width: 70%">
                        <asp:DropDownList ID="ddlAMPM" runat="server" AutoPostBack="false" Width="90%">
                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                             <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/image/save.gif" OnClientClick="return ValidateForm()" />
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
