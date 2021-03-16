function do02PingTuanHelpDocumnet() {
    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();

            //doGameInfo_All_DescContent(varGetUseid);
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsTuanGou");
            doShareWeiXin();
        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }

}

function doShareWeiXin() {
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var host = window.location.host;
    var varJURL = "https://" + host + "/AddFunction/02PingTuan/01Help.html";

    var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
    var varParentID = 0;
    if ((varDB_ParentID != null) && (varDB_ParentID != undefined) && (varDB_ParentID != '')) {
        varParentID = varDB_ParentID;
    }
    else {
        var varparentagentid = varQueryStringList["parentagentid"];////转发ID
        var varparentagentadid = varQueryStringList["parentagentadid"];////转发ID
        if ((varparentagentadid != null) && (varparentagentadid != undefined)) {
            varParentID = varparentagentadid;
        }
        else if ((varparentagentid != null) && (varparentagentid != undefined)) {
            varParentID = varparentagentid;
        }
    }
    //var MasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
    varJURL = varJURL + "?parentagentid=" + varParentID;

    var varimg = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
    var varUserNickName = localStorage.getItem('GetUserNickName201709121928_Open_0609');

    var vardesc = varGetShopClientName + "微团购帮助." + varUserNickName + ".不用囤货，不用发货。";
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    do_GetAjaxShareWeiXin(varShopClientID, varJURL, varGetShopClientName + document.title, vardesc, varimg, ShareShopFunction);
}


