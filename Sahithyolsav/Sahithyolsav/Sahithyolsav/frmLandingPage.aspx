<%@ Page Title="SSF Sahithyolsav-2014" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmLandingPage.aspx.vb" Inherits="Sahithyolsav.frmLandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#menubar').css('display', 'block');
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
         <ContentTemplate>
          <iframe id="iframeContent" src="frmMain.aspx" width="100%" frameborder="0" style="margin-top:15px;"  onload='javascript:resizeIframe(this);'></iframe>
    </ContentTemplate>
    
    </asp:UpdatePanel>
    
</asp:Content>
