<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmParticipantList.aspx.vb"
    Inherits="Sahithyolsav.frmParticipantList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <link href="../Styles/IiframeCss.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/commonScripts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function chkBoxChecck(chkid) {
            var checkboxCollection = document.getElementById('<%=chkItemList.ClientID %>').getElementsByTagName('input');
            for (var i = 0; i < checkboxCollection.length; i++) {
                if (checkboxCollection[i].type.toString().toLowerCase() == "checkbox") {

                }
            }
        }
        function loadUpdatePanel() {
            $("#pnlContent").css("display", "block");
        }
        function ValidateForm() {
            if ($.trim(document.getElementById("txtParticipant").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please eneter the particiapant name");
                return false;
            }
            if ($.trim(document.getElementById("ddlPartcipantLevelIdComboPopUp").value) == "0") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select level");
                return false;
            }

            if ($.trim(document.getElementById("ddlSection").value) == "0") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select a section");
                return false;
            }
            //Validate Item
            var selected = [];
            $('#chkItemList').find("input:checked").each(function () { selected.push($(this).attr('name')); });
            if (selected.length == 0) {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please select a Item");
                return false;
            }
            if ($.trim(document.getElementById("ddlType").value) == "1") {
                if (selected.length > 1) {
                    $("#lblmsg").css("display", "block");
                    $("#lblmsg").css("color", "red");
                    $("#lblmsg").text("You can select only one group item");
                    return false;
                }
            }
            if (selected.length > 4) {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("You can select only 3 Item");
                return false;
            }
            if ($.trim(document.getElementById("txtGroupParticiapnt").value) == "") {
                $("#lblmsg").css("display", "block");
                $("#lblmsg").css("color", "red");
                $("#lblmsg").text("Please eneter the particiapnt in the group");
                return false;
            }
            ShowLoading();
            $("#lblmsg").css("display", "none");
            $("#lblmsg").css("color", "black");
            return true;
        }
        function checkImage(fupID) {
            // ShowLoading();
            var btnSendAppro = document.getElementById(fupID);
            if (btnSendAppro.value == "") {

                alert('Please Select Image .Then try to upload');
                return false
            }
            else {
                return true;
            }
        }
        function checkImageExtension(elem) {

            var filePath = elem.value;



            if (filePath.indexOf('.') == -1)

                return false;



            var validExtensions = new Array();

            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            //Add valid extentions in this array

            validExtensions[0] = 'jpg';

            //validExtensions[1] = 'pdf';



            for (var i = 0; i < validExtensions.length; i++) {

                if (ext == validExtensions[i]) {
                    document.getElementById('btnUpload').disabled = false;
                    return true;
                }

            }


            document.getElementById('btnUpload').disabled = true;
            alert('The only Image extension  allowed is JPG !');

            return false;

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
            <div class="searchTableStyle" style="width: 99.5%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td runat="server" id="level1Text" align="center" style="width: 30%">
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
            <asp:Panel ID="pnlContent" runat="server" Visible="false">
                <div class="bannerTableStyle" style="width: 100%">
                    <table width="100%">
                        <tr>
                            <td align="center" style="width: 10%">
                                <asp:Button ID="btnParticipant" runat="server" Text="Add Participant" OnClientClick="loadUpdatePanel()" />
                                <%--  <asp:ImageButton ID="btnParticipant" runat="server" ImageUrl="~/image/addpart.png" OnClientClick="return ValidateForm()"
                                    Style="height: 25px" />--%>
                            </td>
                            <td style="width: 90%; padding-left: 30%; font-weight: bold;text-transform:uppercase;">
                                <asp:Label runat="server" ID="participantHeader" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%; height: 350px; overflow: auto" class="rounded_corners">
                    <asp:GridView runat="server" ID="gvParticipantdetails" DataKeyNames="intParticipantListId" 
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
                            <asp:BoundField DataField="Section Name" HeaderText="Section Name" />
                            <asp:BoundField DataField="Program Level" HeaderText="Program Level" />
                            <asp:BoundField DataField="Participant From" HeaderText="Team" />
                            <asp:BoundField DataField="vchChessNo" HeaderText="Chess Number" />
            <%--                 <asp:TemplateField HeaderText="Gateway" Visible ="false" >
                        <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblintAge" runat="server" Text='<%#Eval("Age") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Age" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblintAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Age" Visible="false">
                                 <ItemTemplate>
                                    <asp:Label ID="lblCampus" runat="server" Text='<%# Bind("vchCampusName") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Age" Visible="false">
                                 <ItemTemplate>
                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("vchCourse") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                </asp:TemplateField>
                             
                          
                            <asp:TemplateField HeaderStyle-Width="60px" HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtn" ImageUrl="~/image/edit.png" runat="server" Width="25"
                                        Height="25" OnClick="imgEdit_Click" />
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/image/delete.gif" runat="server" Width="25"
                                        Height="25" OnClick="delete_Click" OnClientClick="return confirm('Are You Want to Delete this Paricipant ?')" />
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
            <asp:ModalPopupExtender ID="participantModal" runat="server" TargetControlID="targetButton"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="530px" Width="666px"
                Style="display: block" ScrollBars="Vertical">
                <table width="100%" style="border: Solid 3px #4b6c9e; width: 650px; height: 100%; overflow: scroll;"
                    class="" cellpadding="5" cellspacing="0">
                    <tr style="background-color: #4b6c9e">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            <asp:HiddenField ID="lblId" runat="server" />
                            <asp:HiddenField ID="hparticpantLitsId" runat="server" />
                            <asp:HiddenField ID="hparticpantId" runat="server" />
                            Participant Details
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                           Select Program Type
                        </td>
                        <td  style="width:70%" >
                               <asp:DropDownList ID="ddlType" runat="server" Width="90%" AutoPostBack="true"
                                onchange="ShowLoading()">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Participant Name
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtParticipant" runat="server" Width="85%" />
                        </td>
                    </tr>
              
                    <tr>
                        <td align="right" style="width: 30%">
                            &nbsp;</td>
                        <td style="width: 70%">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:FileUpload ID="fileUploadImage" runat="server"  onchange="checkImageExtension(this);"></asp:FileUpload>
                <asp:Button ID="btnUpload" runat="server" Text="Upload Image" OnClick="btnUpload_Click" OnClientClick="return checkImage('fileUploadImage');"/>
           
            </ContentTemplate>
            <Triggers>
               <asp:PostBackTrigger ControlID="btnUpload"  />
            
            </Triggers>
 </asp:UpdatePanel>
                             </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            &nbsp;</td>
                        <td style="width: 70%; color: #33CC33;">
                            Click Upload Button to Upload the Photo</td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Age</td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtAge" runat="server" Width="50%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%">
                            Select
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlPartcipantLevelIdComboPopUp" runat="server" 
                                onchange="ShowLoading()" Width="90%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Section
                        </td>
                        <td style="width: 70%">
                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" Width="90%"
                                onchange="ShowLoading()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="campusname" runat="server" visible="false" >
                        <td align="right" style="width: 30%">
                          Campus</td>
                        <td style="width: 70%">
                         <asp:TextBox ID="txtCampus" runat="server" Width="85%" /></td>
                    </tr>
                    <tr id="course" runat="server" visible="false">
                        <td align="right" style="width: 30%">
                           Course</td>
                        <td style="width: 70%">
                          <asp:TextBox ID="txtCorse" runat="server" Width="85%" /></td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Select Item
                        </td>
                        <td style="width: 70%">
                            <asp:Panel ID="chkBoxPanel" runat="server" ScrollBars="Vertical" Width="90%" Height="120px"
                                BackColor="AliceBlue" BorderColor="Gray" BorderWidth="1" Font-Names="Tahoma"
                                Font-Size="Medium" Visible="true">
                                <asp:CheckBoxList ID="chkItemList" runat="server" Width="90%" Height="120px" AutoPostBack="false">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                    </tr>
                    </tr>
                    <tr style="display: none;">
                        <td style="width: 30%" align="right">
                            Chess No
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtChessNo" runat="server" Width="90%" Visible="false" />
                        </td>
                    </tr>
                     <tr id="rowGrpParticipant" runat="server">
                        <td style="width: 30%" align="right">
                            Group Participant
                        </td>
                        <td style="width: 70%">
                            <asp:TextBox TextMode="MultiLine"  ID="txtGroupParticiapnt" runat="server" Width="90%" /><br />
                            <span style="color:Green">Enter the group member seperated by comma(name1,name2)</span>
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
