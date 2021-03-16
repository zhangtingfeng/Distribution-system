function do02PingTuanListDocumnet() {
    //debugger;
    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();

            doAjaxLoadTuanGouGoodList();
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsTuanGou");
            doShareWeiXin();
        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }
}


function doAjaxLoadTuanGouGoodList() {
    var varGetUseid = getUserID();
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLdoGameInfo_TuanGoulist = varServiceURL + "/Pub/doTuanGou.asmx/doGameInfo_TuanGoulist?strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;

    var result = -1;
    $.ajax({
        type: "get",
        async: false,
        url: varURLdoGameInfo_TuanGoulist,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp20160712Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            result = json.ErrorCode;
            if (result == 0) {
                document.title = decodeURIComponent(json.Title) + "-微云基石团购";
                if (json.RecordCount > 0) {
                    $.each(json.ThisTuanGouGoodInfoList, function (key, value) {
                        $("#LoadTuanGouGoodList").append(addThisLineTuanGouList(value));
                    });
                }
                else {
                    $("#LoadTuanGouGoodList").append("暂无团购商品");
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    //return result;

}


function addThisLineTuanGouList(TuanGouGoodInfo) {

    var varOneLine = "";
    varOneLine += "<dl>";
    varOneLine += "                    <a href=\"/addfunction/02pingtuan/03goods.html?tuangouid=" + TuanGouGoodInfo.TuanGouID + "\">";
    varOneLine += "                        <dd>";
    varOneLine += "                            <div class=\"ddcon\">";
    varOneLine += "                                <h3>" + decodeURIComponent(TuanGouGoodInfo.GoodName) + "【团购价" + TuanGouGoodInfo.EachPeoplePrice + "￥】</h3>";
    varOneLine += "                                 <p>" + decodeURIComponent(TuanGouGoodInfo.ShortInfo) + "&nbsp;<span style=\"color:#ffffff;background-color:#e53333;\">【原价" + TuanGouGoodInfo.PromotePrice + "￥】【" + decodeURIComponent(TuanGouGoodInfo.CompanyShortDesc) + "】 &nbsp; &nbsp;</span></p>";
    varOneLine += "                                <div class=\"tuan_g_core\">";
    varOneLine += "                                    <div class=\"tuan_g_price\">";
    varOneLine += "                                        <i></i>";
    varOneLine += "                                        <span>" + TuanGouGoodInfo.HowManyPeople + "人1团</span>";
    varOneLine += "                                        <b>&yen;" + TuanGouGoodInfo.EachPeoplePrice + "</b>";
    varOneLine += "                                    </div>";
    varOneLine += "                                    <div class=\"tuan_g_btn\">去开团</div>";
    varOneLine += "                                </div>";
    varOneLine += "                            </div>";
    varOneLine += "                        </dd>";
    varOneLine += "                        <dt><img src=\"" + TuanGouGoodInfo.GoodIcon + "\"></dt>";
    varOneLine += "                    </a>";
    varOneLine += "                </dl>";

    return varOneLine;

}


function doShareWeiXin() {
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var host = window.location.host;
    var varJURL = "https://" + host + "/AddFunction/02PingTuan/02PingTuan.html";
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

    var vardesc = varGetShopClientName + "微团购列表." + varUserNickName + ".不用囤货.不用发货.";
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    do_GetAjaxShareWeiXin(varShopClientID, varJURL, varGetShopClientName + document.title, vardesc, varimg, ShareShopFunction);
}
