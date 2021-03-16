function do05OnlineListDocumnet() {
    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();
            doAjaxLoad05OnlineList();
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_ls05OnlineList");
            doShareWeiXin();
        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }

}


function doAjaxLoad05OnlineList() {
    var varGetUseid = getUserID();
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLdoGameInfo_OnLinelist = varServiceURL + "/Pub/doOnlineList.asmx/doAjaxLoad05OnlineList?strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;

    var result = -1;
    $.ajax({
        type: "get",
        async: false,
        url: varURLdoGameInfo_OnLinelist,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp20170223Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            result = json.ErrorCode;
            if (result == 0) {
                if (json.RecordCount > 0) {
                    $.each(json.UserOnlineList, function (key, value) {
                        $("#IDaddThisLineOnLineListInfoList").append(addThisLineOnLineListInfoList(value));
                    });
                }
                else {
                    $("#IDaddThisLineOnLineListInfoList").append("暂无在线报名活动");
                }
            }
            else {
                $("#IDaddThisLineOnLineListInfoList").append("在线报名活动暂停");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });
}


function addThisLineOnLineListInfoList(OnLineListInfo) {

    var varOneLine = "";
    //
    //varOneLine += "<div class=\"theme-entry\" onclick=\"/huodong/05olineinfo-" + OnLineListInfo.OnlineID + ".aspx\">";
    varOneLine += "<div class=\"theme-entry\" onclick=\"javascript: window.location.href ='/huodong/05olineinfo-" + OnLineListInfo.OnlineID + ".aspx'\">";
    varOneLine += "      <p class=\"p\">";
    varOneLine += "          <img src=\"" + decodeURIComponent(OnLineListInfo.ImageFull) + "\" style=\"opacity: 1;\">";
    varOneLine += "      </p><div>";
    varOneLine += "          <p>" + decodeURIComponent(OnLineListInfo.Title) + "</p><em>" + decodeURIComponent(OnLineListInfo.DeadLine) + "</em>";
    varOneLine += "              <span>&lt;";
    if (OnLineListInfo.boolIfUserOnline == 1) {
        varOneLine += "              您已报名该活动";
    }
    else {
        varOneLine += "              您未报名该活动";
    }
    varOneLine += "              &gt;</span>";
    varOneLine += "       </div>";
    varOneLine += "  </div>";


    return varOneLine;

}



function doShareWeiXin() {
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var host = window.location.host;
    var varJURL = "https://" + host + "/Huodong/05OnlineList/index.html";

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

    var vardesc = varGetShopClientName + "在线报名列表." + varUserNickName + "";
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    do_GetAjaxShareWeiXin(varShopClientID, varJURL, varGetShopClientName + document.title, vardesc, varimg, ShareShopFunction);
}
