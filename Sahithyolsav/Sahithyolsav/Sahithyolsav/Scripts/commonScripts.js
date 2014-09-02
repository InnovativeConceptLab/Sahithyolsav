function resizeIframe(obj) {
    obj.style.height = ($(window).height() - 100) + "px";  //(obj.contentWindow.document.body.scrollHeight + 40) + 'px';
}
function loadIframe(id) {
    switch (id) {
        case 'home':
            document.getElementById('iframeContent').src = "/frmMain.aspx";
            break;
        case 'dist':
            document.getElementById('iframeContent').src = "../Masters/frmDistrict.aspx";
            break;
        case 'user':
            document.getElementById('iframeContent').src = "/frmUsers.aspx";
            break;
        case 'section':
            document.getElementById('iframeContent').src = "../Masters/frmSection.aspx";
            break;
        case 'item':
            document.getElementById('iframeContent').src = "../Masters/frmItem.aspx";
            break;
        case 'sector':
            document.getElementById('iframeContent').src = "../Masters/frmSector.aspx";
            break;
        case 'unit':
            document.getElementById('iframeContent').src = "../Masters/frmUnit.aspx";
            break;
        case 'div':
            document.getElementById('iframeContent').src = "../Masters/frmDivision.aspx";
            break;
        case 'part':
            document.getElementById('iframeContent').src = "../Participant/frmParticipantList.aspx";
            break;
        case 'codeletter':
            document.getElementById('iframeContent').src = "../Participant/frmCodeLetterMap.aspx";
            break;
        case 'tabulation':
            document.getElementById('iframeContent').src = "../Participant/frmTabulation.aspx";
            break;
        case 'pgrm':
            document.getElementById('iframeContent').src = "/frmProgramSettings.aspx";
            break;
        case 'chess':
            document.getElementById('iframeContent').src = "../Participant/frmChessNumber.aspx";
            break;
        case 'pswd':
            document.getElementById('iframeContent').src = "/ChangePassword.aspx";
            break;
        case 'rpt':
            document.getElementById('iframeContent').src = "../Reports/frmReports.aspx";
            break;
        case 'stage':
            document.getElementById('iframeContent').src = "../Schedule/frmStage.aspx";
            break;
        case 'sch':
            document.getElementById('iframeContent').src = "../Schedule/schedule.aspx";
            break;
        case 'Itemrpt':
            document.getElementById('iframeContent').src = "../Reports/frmItemWiseReport.aspx";
            break;
        case 'Gchess':
            document.getElementById('iframeContent').src = "../Participant/frmGenerateChessNum.aspx";
            break;
    }
}
function isNumberKey(e) {
    var specialKeys = new Array();
    specialKeys.push(8);
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
    return ret;
}
function isNumberStarKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode; 
    if (charCode != 42 && charCode > 31
            && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function ShowLoading() {

    document.getElementById("pnlpopup").style.zIndex = "10000";
    document.getElementById("pnlpopup").style.zIndex = "10000";
}
