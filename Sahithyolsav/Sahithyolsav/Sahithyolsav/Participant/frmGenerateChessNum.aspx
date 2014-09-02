<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmGenerateChessNum.aspx.vb"
    Inherits="Sahithyolsav.frmGenerateChessNum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
            <div>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
