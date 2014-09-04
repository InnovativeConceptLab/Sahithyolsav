<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmViewResults.aspx.vb" Inherits="Sahithyolsav.frmViewResults1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
         <ContentTemplate>
          <iframe id="iframeContent" src="../Reports/frmViewReportWithoutLogin.aspx" width="100%" frameborder="0" style="margin-top:15px;"  onload='javascript:resizeIframe(this);'></iframe>
    </ContentTemplate>
    
    </asp:UpdatePanel>
    
</asp:Content>
