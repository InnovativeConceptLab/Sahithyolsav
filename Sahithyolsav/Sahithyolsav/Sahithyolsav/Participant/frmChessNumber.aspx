<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmChessNumber.aspx.vb"
    Inherits="Sahithyolsav.frmChessNumber" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function loadUpdatePanel() {
            $("#pnlContent").css("display", "block");
        }
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
            ShowLoading();
            $("#lblmsg").css("display", "none");
            $("#lblmsg").css("color", "black");
            return true;
        }
    </script>
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
            z-index: 10002;
        }
    </style>
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
            <div class="searchTableStyle" style="display: none; width: 99.5%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td runat="server" id="level1Text" align="center" style="width: 50%">
                            Select Level
                        </td>
                        <td runat="server" id="level1" style="width: 50%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdCombo1" runat="server" Width="45%" AutoPostBack="true"
                                onChange="loadUpdatePanel()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="sublist" class="searchTableStyle" style="display: none; width: 99.5%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td runat="server" id="level2" align="center" style="width: 25%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdCombo2" runat="server" Width="90%" AutoPostBack="true"
                                onChange="loadUpdatePanel()">
                            </asp:DropDownList>
                        </td>
                        <td runat="server" id="level3" align="center" style="width: 25%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdCombo3" runat="server" Width="90%" AutoPostBack="true"
                                onChange="loadUpdatePanel()">
                            </asp:DropDownList>
                        </td>
                        <td runat="server" id="level4" align="center" style="width: 25%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdCombo4" runat="server" Width="90%" AutoPostBack="true"
                                onChange="loadUpdatePanel()">
                            </asp:DropDownList>
                        </td>
                        <td runat="server" id="level5" align="center" style="width: 25%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdCombo5" runat="server" Width="90%" AutoPostBack="true"
                                onChange="loadUpdatePanel()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlContent" runat="server" Visible="false">
                <div class="bannerTableStyle" style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td align="center" style="width: 100%; font-weight: bold; text-transform: uppercase;">
                                <asp:Label runat="server" ID="participantHeader" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="searchTableStyle" style="width: 99.5%" runat="server" id="divGenerate">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="width: 100%">
                                <asp:Button ID="btnGenerte" runat="server" Text="GENERATE CHESS NUMBER" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100%">
                                <asp:Label ID="lblchessNumMsg" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="searchTableStyle" style="width: 99.5%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="width: 20%">
                                Select a Section
                            </td>
                            <td align="center" style="width: 20%">
                                <asp:DropDownList ID="ddlSearchSection" runat="server" Width="90%" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 20%">
                                Select a Team
                            </td>
                            <td align="center" style="width: 20%">
                                <asp:DropDownList ID="ddlTeam" runat="server" Width="90%" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 33%">
                                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/image/submit.jpg" ValidationGroup="J"
                                    Height="20px" Width="70px" Style="margin-left: 0px" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; height: 320px; overflow: auto" class="rounded_corners">
                    <asp:GridView runat="server" ID="gvChessNumMapdetails" DataKeyNames="intParticipantListId"
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
                            <asp:BoundField DataField="vchPartcipantName" HeaderText="Name" />
                            <asp:BoundField DataField="Participant From" HeaderText="Team" />
                            <asp:BoundField DataField="vchSectionName" HeaderText="Section Name" />
                            <asp:BoundField DataField="vchChessNo" HeaderText="Chess No" />
                            <asp:TemplateField HeaderText="DistrictID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblintParticipantListId" runat="server" Text='<%# Bind("intParticipantListId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chess Number" Visible="true">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtchesNum" runat="server" Width="80px" Text='<%# Bind("vchChessNo") %>' />
                                </ItemTemplate>
                                <ItemStyle Font-Size="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSaveCode" runat="server" OnClick="btnChessNumSave_Click"
                                        ImageUrl="~/image/submit.jpg" />
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
            <asp:ModalPopupExtender ID="codeLetterModal" runat="server" TargetControlID="targetButton"
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
                            Participant Details
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Participant Name
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtParticipant" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Section
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtSection" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Item
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtItem" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Chess Number
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtChessNum" runat="server" Width="90%" Enabled="false" />
                        </td>
                    </tr>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Code Letter
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtCodeLetter" runat="server" Width="90%" />
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
