function AllSales(clickNum) {
    // debugger;

    $("#liAllSales1").removeClass("weactive");
    $("#liAllSales2").removeClass("weactive");
    $("#liAllSales3").removeClass("weactive");

    var marker = "#liAllSales" + clickNum;
    $("#liAllSales" + clickNum).addClass("weactive");

    $("#ul_UserAllSales").hide();
    $("#ul_UserLastMonthSales").hide();
    $("#ul_UserThisMonthSales").hide();


    if (clickNum == '1') {
        $("#ul_UserAllSales").show();
    }
    else if (clickNum == '2') {
        $("#ul_UserLastMonthSales").show();
    }
    else if (clickNum == '3') {
        $("#ul_UserThisMonthSales").show();
    }
}


function loadBuySelectType_Multi() {
    //debugger;
    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();

            doGameInfo_All_DescContent(varGetUseid);
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsSalesOrder");

        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }
}



function doGameInfo_All_DescContent(varGetUseid) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    varURL = varServiceURL + "/Pub/doVisitStatistics.asmx/doVisitStatistics_DescContent?strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp70439Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            result = json.ErrorCode;
            if (result == 0) {
                //debugger;
                $("#UserAllSales").html("¥" + json.AllSalesMoney_my_AND_myAllSon);
                $("#UserLastMonthSales").html("¥" + json.LastMonthSales_my_AND_myAllSon);
                $("#UserThisMonthSales").html("¥" + json.ThisMonth_SalesMoney_my_AND_myAllSon);
                $("#StatisUpdateTime").html("" + decodeURIComponent(json.UpdateTime));

                do_MeGetAjaxShareWeiXinSalesOrder(json);

                $.each(json.AllSalesMoney_my_AND_myAllSonList, function (key, value) {
                    $("#ul_UserAllSales").append(addThisLine(value));
                });

                $.each(json.LastMonthSales_my_AND_myAllSonList, function (key, value) {
                    $("#ul_UserLastMonthSales").append(addThisLine(value));
                });
                $.each(json.ThisMonth_SalesMoney_my_AND_myAllSonList, function (key, value) {
                    $("#ul_UserThisMonthSales").append(addThisLine(value));
                });
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    return result;
}


function addThisLine(varLine) {

    var varLineContent = "";

    varLineContent += "	<li>\n";
    varLineContent += "	              <div class=\"ShowSalesMoneythelist_ul_li\">\n";
    varLineContent += "	                  <div class=\"ul_li_Classs_30_Percent\">\n";
    varLineContent += "	                       <div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + varLine.ID + "</div>\n";
    varLineContent += "	                  </div>\n";
    varLineContent += "	                  <div class=\"ul_li_Classs_30_Percent\">\n";
    varLineContent += "	                      <div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + decodeURIComponent(varLine.ShopName) + "</div>\n";
    varLineContent += "	                  </div>\n";
    varLineContent += "	                 <div class=\"ul_li_Classs_30_Percent\">\n";
    varLineContent += "	                     <div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + varLine.SalesMoney + "</div>\n";
    varLineContent += "	                 </div>\n";
    varLineContent += "	             </div>\n";
    varLineContent += "	          </li>\n";



    return varLineContent;
}



function do_MeGetAjaxShareWeiXinSalesOrder(json) {
    var host = window.location.host;
    var WeiXin_imgAllPageUrl = "https://" + host + "/AddFunction/Sales123.jpg";
    var host = window.location.host;
    var varJURL = "https://" + host + "/addfunction/salesorder.html";
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var varTitle = "" + varGetShopClientName + "业绩排行榜!";
    var vardesc = varGetShopClientName + "业绩排行榜。不用囤货，不用发货。";


    if (json.parentagentadid > 0) {
        varJURL += "?parentagentadid=" + json.parentagentadid;
    }
    if (json.parentagentid > 0) {
        varJURL += "?parentagentid=" + json.parentagentid;
    }

    do_GetAjaxShareWeiXin(localStorage.getItem('GetShopClientID201709121928_Open_0609'), varJURL, varTitle, vardesc, WeiXin_imgAllPageUrl, ShareShopFunction);


}
