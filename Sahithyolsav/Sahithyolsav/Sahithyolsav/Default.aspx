<%@ Page Title="SSF Sahithyolsav-2014" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="Sahithyolsav._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/template.css" rel="stylesheet" type="text/css" />
    <link href="Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="Scripts/jquery.validationEngine.js" type="text/javascript"></script>
    <style type="text/css">
        .loading
        {
            /*  position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: Gray;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=90);
            opacity: 0.9;
            -moz-opacity: 0.9;*/
            font-family: Arial;
            font-size: 10pt;
            width: 100%;
            height: 100%;
            display: none;
            position: fixed;
            background-color: Gray;
            z-index: 99;
            filter: Alpha(Opacity=90);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("form").validationEngine();
        });
        function validateUser() {
            $.ajax({
                type: "POST",
                url: "Default.aspx/validateUser",
                data: '{uname: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" ,pwd:"' + $("#<%=txtPassword.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function ShowLogin(Level) {

            var tr = document.getElementById('trlogin');
            tr.style.display = "";
            var a = document.getElementById('lbllevel');
            a.innerHTML = Level + '  Login';
            var b = document.getElementById('<%=lbblogin.ClientID %>');
            b.innerHTML = 'Login Here ...';
            b.style.color = 'Blue';

            return false;

        }
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass(" .ModalPopupBG");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: 0, left: 0 });
            }, 100);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
      
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div>
                <table style="font-family: Arial, Helvetica,sans-serif; font-size: 13px;" width="100%">
                    <tr>
                        <td style="width: 20%; border-right: 1px solid #4b6c9e">
                            <table style="font-family: Arial, Helvetica, sans-serif;" width="100%">
                                <tr>
                                    <td align="center">
                                        <img src="image/sahithyotsav-logo.png" width="150px" height="190px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                            <b>Contact</b>
                                            <br />
                                            Kerala Sate Sunni Students Federation
                                            <br />
                                            Students Center
                                            <br />
                                            Calicut, Kerala, India
                                            <br />
                                            Pin 673004
                                            <br />
                                            <br />
                                            Phone: 0495 2722104<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0495 4010991
                                            <br />
                                            Email: <a href="mailto:mail@ssfkeralainfo.com">mail@ssfkeralainfo.com</a>
                                            <br />
                                            &nbsp;_______________________<br />
                                            <br />
                                            Related Links
                                            <br />
                                            &nbsp;&nbsp;&nbsp; IPB Books
                                            <br />
                                            &nbsp;&nbsp;&nbsp; Risala Online
                                            <br />
                                            &nbsp;&nbsp;&nbsp; Siraj Daily
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60%" valign="top">
                            <table style="font-family: Arial, Helvetica, sans-serif;" width="100%">
                                <tr>
                                    <td>
                                        <img src="image/ssfflag.jpg" width="190px" height="150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom" style="color: #06A0E4; font-size: 20px; font-weight: bold">
                                        KERALA STATE
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #06A0E4; font-size: 30px; font-weight: bold">
                                        SUNNI STUDENTS FEDERATION (SSF)
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000; font-size: 30px; font-weight: bold">
                                        SAHITHYOLSAV -14
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000;">
                                        sahithyolsav niyamavali&nbsp;<asp:LinkButton ID="lnkClick" runat="server" Text="Click here"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000; font-size: 20px; font-weight: bold">
                                        STATE SAHITHYOLSAV -14
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000; font-size: 20px; font-weight: bold">
                                        DATED 2014 SEPTEMBER 5, 6
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000; font-size: 20px; font-weight: bold">
                                        AT
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="color: #000000; font-size: 20px; font-weight: bold">
                                        KASARAGOD
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20%; border-left: 1px solid #4b6c9e; vertical-align: top">
                            <table style="font-family: Arial, Helvetica, sans-serif;" width="100%">
                                <tr>
                                    <td style="padding-left: 25px; font-family: 'Times New Roman', Times, serif; font-size: 12px;
                                        font-weight: bold;">
                                        <asp:Label ID="lbblogin" runat="server" Text="Login Here ..." ForeColor="#3366FF"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnAdmin" runat="server" Text="ADMIN" class="myButton" OnClientClick="return ShowLogin('ADMIN');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnState" runat="server" Text="STATE" class="myButton" OnClientClick="return ShowLogin('STATE');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnDistrict" runat="server" Text="DISTRICT" class="myButton" OnClientClick="return ShowLogin('DISTRICT');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnDivision" runat="server" Text="DIVISION" class="myButton" OnClientClick="return ShowLogin('DIVISION');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnSector" runat="server" Text="SECTOR" class="myButton" OnClientClick="return ShowLogin('SECTOR');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding: 25px">
                                    <td style="text-align: center; padding: 5px">
                                        <asp:Button ID="btnUnit" runat="server" Text="UNIT" class="myButton" OnClientClick="return ShowLogin('UNIT');">
                                        </asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="trlogin" style="display: none;">
                                    <td align="center">
                                        <table style="border: 2px solid #4b6c9e">
                                            <tr>
                                                <td id="lbllevel" style="color: #FFFFFF; font-size: 12px; font-weight: bold; font-family: 'Times New Roman', Times, serif;
                                                    font-style: normal; background-color: #4b6c9e; text-align: center;" colspan="2">
                                                    Login
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: #06A0E4; font-size: 12px; font-weight: bold; text-align: left;">
                                                    USER ID
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: #06A0E4; font-size: 12px; font-weight: bold">
                                                    PASSWORD
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPassword" TextMode="password" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trErrorMessage">
                                                <td style="color: #FF0000; font-size: 14px; font-weight: bold; font-family: 'Times New Roman', Times, serif;
                                                    text-align: center;" colspan="2">
                                                    <asp:Label ID="lblErrorMsg" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="~/image/submit.jpg" ValidationGroup="J"
                                                        Height="20px" Width="70px" Style="margin-left: 0px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="loading" align="center" style="padding: 18% 0% 0% 0%">
        <img src="../../image/loading.gif" />
    </div>
</asp:Content>
