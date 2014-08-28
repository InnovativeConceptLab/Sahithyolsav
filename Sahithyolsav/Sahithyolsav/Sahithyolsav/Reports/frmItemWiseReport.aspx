<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmItemWiseReport.aspx.vb"
    Inherits="Sahithyolsav.frmItemWiseReport" %>

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
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../../image/loading.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <div>
        <div class="bannerTableStyle" style="width: 100%">
            <table width="100%">
                <tr>
                    <td align="center" style="width: 100%; font-weight: bold; text-transform: uppercase">
                        <asp:Label runat="server" ID="participantHeader" ForeColor="White" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <%--  <div runat="server" id="sublist" class="searchTableStyle" style="display: block;
            width: 99.5%">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center" style="width: 30%">
                        Select Report
                    </td>
                    <td align="left" style="width: 70%">
                        <asp:DropDownList ID="ddlReports" runat="server" Width="70%" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>--%>
        <div class="searchTableStyle" style="width: 99.5%" runat="server" id="divItemSection">
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
                        Select a Item
                    </td>
                    <td align="center" style="width: 20%">
                        <asp:DropDownList ID="ddlSearchItem" runat="server" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 20%">
                        <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/image/submit.jpg" ValidationGroup="J"
                            Height="20px" Width="70px" Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divgridRport" runat="server">
            <asp:ImageButton ID="imgPdf" runat="server" ImageUrl="~/image/downloadButton.jpg"
                ToolTip="Download as pdf" Height="32px" Width="160px" />
            <div style="width: 100%; height: 300px; overflow: auto" class="rounded_corners">
                <asp:GridView runat="server" ID="gvReport" Width="100%" HeaderStyle-Height="30px"
                    EmptyDataText="No Data Seleted" OnRowCreated="gvReport_RowCreated" 
                    AutoGenerateColumns="False">
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
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Section" HeaderText="Section" />
                        <asp:BoundField DataField="Team" HeaderText="Team" />
                        <asp:BoundField DataField="Chess No" HeaderText="Chess No" />
                        <asp:BoundField DataField="Code Letter" HeaderText="Code Letter" />
                        <asp:BoundField DataField="Mark" HeaderText="Mark" />
                        <asp:BoundField DataField="Point" HeaderText="Point" />
                       
                        <asp:TemplateField HeaderText="Photo">
                        <ItemTemplate><asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("ImgPath") %>'></asp:Image></ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
