var intDiff = -1;//倒计时总秒数量
function loadBuySelectType_Multi() {
    //debugger;
    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();

            var KanJiaID = varQueryStringList["kanjiaid"];
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsKanJiaRule");
            doGameInfo_KanJiaTopicDescContent(KanJiaID);


            $("#nickname").val(localStorage.getItem('GetUserNickName201709121928_Open_0609'));


            var varParentID = 0;
            var varparentagentid = varQueryStringList["parentagentid"];////转发ID
            var varparentagentadid = varQueryStringList["parentagentadid"];////转发ID
            if ((varparentagentadid != null) && (varparentagentadid != undefined)) {
                varParentID = varparentagentadid;
            }
            else if ((varparentagentid != null) && (varparentagentid != undefined)) {
                varParentID = varparentagentid;
            }
            var MasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
            doGameInfo_KanJia_myProperty(KanJiaID, varGetUseid, MasterUserID, varParentID);////后台检查varGetUseid 是不是MasterUserID  do_MeGetAjaxShareWeiXin
            doGameInfo_KanJia_doVisitWeiKanJiaAction(KanJiaID, varGetUseid, MasterUserID, varParentID);///发送访问消息
        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }
}

///帮砍一刀
function HelpKanJia() {
    ////组织页面数据 进行跳转
    if ($("#buybtnHead").html() == "原商品详情") {

        try {
            var varQueryStringList = new QueryString();
            var KanJiaID = varQueryStringList["kanjiaid"];
            var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
            varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/doGameInfo_KanJiaTopicDescContent?strWeikanjiaid=" + KanJiaID + "&ShopClientID=" + ShopClientID;
            var result = -1;
            $.ajax({
                type: "get",
                url: varURL,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonp7059Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
                success: function (json) {
                    result = parseInt(json.ErrorCode);
                    if (result == 0) {
                        var idGoodIDint = json.GoodIDint;
                        window.location = "/product-" + idGoodIDint + ".aspx";
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
        catch (e)
        { alert('语句异常：' + e.message) }



    }
    else {///帮助砍价
        try {
            var varQueryStringList = new QueryString();
            var KanJiaID = varQueryStringList["kanjiaid"];
            var MasterUserID = varQueryStringList["masteruserid"];
            var varGetUseid = getUserID();
            var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
            varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/IWantDoKanJia_Help?strWeiKanJiaID=" + KanJiaID + "&ShopClientID=" + ShopClientID;
            varURL += "&MasterUserID=" + MasterUserID;
            varURL += "&UserID=" + varGetUseid;
            var result = -1;
            $.ajax({
                type: "get",
                url: varURL,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonp1000Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
                success: function (json) {
                    result = json.ErrorCode;
                    if (result == 0) {
                        layer.open({
                            type: 2,
                            content: "砍价成功,成功砍掉￥" + json.MyHelperPrice + ",感谢你的参与",
                            time: 4,
                            end: function (layer) {
                                location.reload();
                                //document.URL = location.href;///Javascript刷新页面的几种方法：
                            }
                        });
                    }
                    else if (result == 2) {
                        layer.open({
                            type: 2,
                            content: "已经帮助他砍过了￥" + json.MyHelperPrice + ",可分享朋友圈继续帮助他砍价",
                            time: 4,
                            end: function (layer) {
                                $("#mcoverWeiKanJia").css("display", "block");  // 点击弹出层，弹出层消失
                                ///document.URL = location.href;///Javascript刷新页面的几种方法：
                            }
                        });
                    }
                    else if ((result == 1) || (result == 3)) {
                        layer.open({
                            type: 2,
                            content: "帮助砍价必须先关注",
                            time: 4,
                            end: function (layer) {
                                var varlink = json.GuideSubscribe;
                                window.location.href = varlink;
                            }
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
        catch (e)
        { alert('语句异常：' + e.message) }

    }

}



///我要发起
function IWantDoKanJia() {
    $("#MaterInpuInfo_Text").show();
    $("#MaterInpuInfo_Button").show();
    $('html,body').animate({ scrollTop: $("#MaterInpuInfo_Text").offset().top - 50 }, 500);
    $("#username").focus();
    layer.open({ type: 2, time: 1, content: '请填写姓名，手机号后点击"马上报名参加活动"！' });

}

function IWantDoKanJia_Submit() {
    /// debugger;

    try {
        //location.hash = "#username";/// js跳转到锚点

        var varusername = $("#username").val().trim();
        var vartel = $("#tel").val().trim();
        var varnickname = $("#nickname").val().trim();

        if (varusername == "") {
            $("#username").val(varnickname);
            alert("亲爱的" + varnickname + ",姓名不可为空");
            $("#username").focus();
            return;
        }

        if (vartel == "") {
            alert("亲爱的" + varnickname + ",电话不可为空");
            $("#tel").focus();
            return;
        }

        if (vartel.length != 11) {
            alert('请输入有效的手机号码！');
            $("#tel").focus();
            return false;
        }

        var varGetUseid = getUserID();

        var varQueryStringList = new QueryString();
        var KanJiaID = varQueryStringList["kanjiaid"];
        var varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/IWantDoKanJia_Submit?strWeiKanJiaID=" + KanJiaID + "&UserID=" + varGetUseid;
        varURL += "&varusername=" + encodeURI(encodeURI(varusername)) + "&vartel=" + encodeURI(encodeURI(vartel));
        var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
        varURL += "&ShopClientID=" + ShopClientID;
        var result = -1;
        $.ajax({
            type: "get",
            url: varURL,
            dataType: "jsonp",
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonp0060Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
            beforeSend: function () { //加载条
                layer.open({ type: 2, time: 2 });
            },
            success: function (json) {
                result = (json.ErrorCode);
                if (result == 0) {
                    var varJURL = "/Huodong/WeiKanJia/default.html?kanjiaid=" + KanJiaID;
                    varJURL += "&masteruserid=" + varGetUseid;
                    var varParentID = 0;
                    if (json.parentagentadid > 0) {
                        varJURL += "&parentagentadid=" + json.parentagentadid;
                        varParentID = json.parentagentadid;
                    }
                    if (json.parentagentid > 0) {
                        varJURL += "&parentagentid=" + json.parentagentid;
                        varParentID = json.parentagentid;
                    }
                    layer.open({
                        type: 2,
                        content: "发起成功,正在为您加载砍价，仅有库存" + json.KuCunCount + ",请尽快分享朋友圈,请您的朋友帮您砍价,否则可能会被抢光",
                        time: 5,
                        end: function (layer) {
                            window.location = varJURL;
                        }
                    });
                }
                else if ((result == 4)) {
                    layer.open({
                        type: 2,
                        content: "发起砍价必须是我们的代理分销商",
                        time: 2,
                        end: function (layer) {
                            var varlink = json.AgentSubscribe;
                            window.location.href = varlink;
                        }
                    });
                }
                else if ((result == 1) || (result == 3)) {
                    layer.open({
                        type: 2,
                        content: "发起砍价必须先关注",
                        time: 2,
                        end: function (layer) {
                            var varlink = json.GuideSubscribe;
                            window.location.href = varlink;
                        }
                    });
                }
                else if ((result == 5)) {
                    layer.open({
                        type: 2,
                        content: "发起砍价必须是有收获地址",
                        time: 2,
                        end: function (layer) {
                            window.location.href = "/cart_self.aspx";///Javascript刷新页面的几种方法：

                            //var varlink = json.AgentSubscribe;
                            //window.location.href = varlink;
                        }
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

    } catch (e) { alert('语句异常：' + e.message) }
}

function BaobiAndShareMessage() {

}



function doMakeHtml_AnnouncePic_WeiKanJiaList(varGooidKanJiaID) {

    varURL = varServiceURL + "/Pub/doClickThis_HowToGetProduct.asmx/doGetProduct_newTmpletAnnouncePic_GoodList";
    $.ajax({
        type: 'GET',
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        data: 'strGoodID=' + varGooidKanJiaID,
        jsonpCallback: "jsonp201607031817Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#Anounce").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
        },
        success: function (json) {
            //alert(msg);
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                //alert(msg);
                $("#Anounce").html(decodeURIComponent(json.msg));
            };
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    })
}


function do_MeGetAjaxShareWeiXin(varimg, varlink, varTitle, vardesc) {
       do_GetAjaxShareWeiXin(localStorage.getItem('GetShopClientID201709121928_Open_0609'), varlink, varTitle, vardesc, varimg, ShareShopFunction);
}

///倒计时
function timer(intDiff) {
    if (intDiff > 0) {
        window.setInterval(function () {
            var day = 0,
                hour = 0,
                minute = 0,
                second = 0;//时间默认值
            if (intDiff > 0) {
                day = Math.floor(intDiff / (60 * 60 * 24));
                hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
                minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
                second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
            }
            if (minute <= 9) minute = '0' + minute;
            if (second <= 9) second = '0' + second;
            $('#day_show').html(day);
            $('#hour_show').html('<s id="h"></s>' + hour);
            $('#minute_show').html('<s></s>' + minute);
            $('#second_show').html('<s></s>' + second);
            intDiff--;
        }, 1000);

        console.log("用于输出普通信息intDiff=" + intDiff);
        //console.info("用于输出提示性信息");
        //console.error("用于输出错误信息");
        //console.warn("用于输出警示信息");
    }
}





function doGameInfo_KanJiaTopicDescContent(KanJiaID) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/doGameInfo_KanJiaTopicDescContent?strWeiKanJiaID=" + KanJiaID + "&ShopClientID=" + ShopClientID;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp7059Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        beforeSend: function () {
            $("#Anounce").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
        },
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                $("#idKanJiaTopicDescContent").html(decodeURIComponent(json.KanJiaTopicDescContent));
                $("#idKanJiaRule").html(decodeURIComponent(json.KanJiaRule));
                $(".ClsbuybuttonNow").html("立即购买(￥" + json.StartPrice + "元),仅有库存" + json.KuCunCount + "件");
                //debugger;
                intDiff = json.EndTimeInt;
                if (intDiff > 0) {
                    timer(intDiff);
                }
                doMakeHtml_AnnouncePic_WeiKanJiaList(json.GoodIDint);///轮播图

                //AlertShow("扫描成功,正在为您登陆", "doJumptoWhere(\"" + json.UserOpenID + "\")")
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




function doGameInfo_KanJia_myProperty(KanJiaID, varGetUseid, varMasterUserID, varParentID) {
    //debugger;
    if (varMasterUserID == undefined || varMasterUserID == "" || varMasterUserID == 0 || varMasterUserID == "0") {
        $(".Q-buy-btn_Now").html("原商品详情");

        var WeiXin_imgAllPageUrl = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
        var host = window.location.host;
        var varJURL = "https://" + host + "/Huodong/WeiKanJia/default.html?kanjiaid=" + KanJiaID;
        var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
        var varTitle = "" + varGetShopClientName + "正在进行" + "微砍价活动,告诉亲朋好友快来发起砍价吧!";
        do_MeGetAjaxShareWeiXin(WeiXin_imgAllPageUrl, varJURL, varTitle, varGetShopClientName + "微砍价活动。不用囤货，不用发货。");
        document.title = varTitle;
        return;
    }

    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/doGameInfo_KanJia_myProperty?strWeiKanJiaID=" + KanJiaID + "&strUserID=" + varGetUseid + "&strMasterUserID=" + varMasterUserID + "&ShopClientID=" + ShopClientID;
    varURL += "&varParentID=" + varParentID;
    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp7050Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                resultExsit_WeiKanJia_Master = parseInt(json.Exsit_WeiKanJia_Master);
                if (resultExsit_WeiKanJia_Master > -1) {
                    var resultExsit_WeiKanJia_MasterList = json.JsonList;

                    if (resultExsit_WeiKanJia_MasterList.length > 0) {
                        for (var iiii = 0; iiii < resultExsit_WeiKanJia_MasterList.length; iiii++) {
                            var tableobj = document.getElementById("idMyKanJiaInfo_Helper_Table");
                            var rowobj = tableobj.insertRow(tableobj.rows.length);
                            var cell1 = rowobj.insertCell(rowobj.cells.length);
                            var cell2 = rowobj.insertCell(rowobj.cells.length);
                            var cell3 = rowobj.insertCell(rowobj.cells.length);
                            var cell4 = rowobj.insertCell(rowobj.cells.length);

                            var varkey = resultExsit_WeiKanJia_MasterList[iiii];
                            cell1.innerHTML = "<img class=\"imgHeadHelp\" src=\"" + varkey.UserIMG + "\">";
                            cell1.width = "68";
                            cell1.style = "tx";
                            cell2.innerHTML = decodeURIComponent(varkey.UserNickName);
                            cell2.width = "127";
                            cell3.innerHTML = "￥" + varkey.MyHelperPrice;
                            cell4.innerHTML = "￥" + varkey.AfterMyHelperPrice;
                            cell4.style = "yes";
                        }
                        $("#idMyKanJiaInfo_Helper_Table").show();
                    }
                    $("#idMyKanJiaInfo").html(decodeURIComponent(json.MyKanJiaInfo));
                    $("#idMyKanJiaInfo").show();
                    $(".ClsbuybuttonNow").html("立即购买(￥" + json.DecimalMyCanBuy + "元),仅有库存" + json.KuCunCount + "件");


                    var host = window.location.host;
                    var varJURL = "https://" + host + "/Huodong/WeiKanJia/default.html?kanjiaid=" + KanJiaID;
                    varJURL += "&masteruserid=" + varMasterUserID;
                    var varParentID = 0;
                    if (json.parentagentadid > 0) {
                        varJURL += "&parentagentadid=" + json.parentagentadid;
                        varParentID = json.parentagentadid;
                    }
                    if (json.parentagentid > 0) {
                        varJURL += "&parentagentid=" + json.parentagentid;
                        varParentID = json.parentagentid;
                    }
                    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');

                    var varTitle = decodeURIComponent(json.MasterUserIDContactMan) + "正在进行" + varGetShopClientName + ", " + decodeURIComponent(json.Topic) + "微砍价活动,已有" + resultExsit_WeiKanJia_MasterList.length + "人帮他助力砍价,最低" + json.EndPrice + "元可得,亲朋好友都来帮忙砍价吧!";
                    do_MeGetAjaxShareWeiXin(json.MasterUserIDHeadIMG, varJURL, varTitle, decodeURIComponent(json.WeiXinDes));
                    document.title = varTitle;
                }
            }
            else if (result == -1) {
                $(".ClsbuybuttonNow").html("立即购买(￥" + json.DecimalMyCanBuy + "元),仅有库存" + json.KuCunCount + "件");
                $(".Q-buy-btn_Now").html("原商品详情");
                var WeiXin_imgAllPageUrl = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
                var host = window.location.host;
                var varJURL = "https://" + host + "/Huodong/WeiKanJia/default.html?kanjiaid=" + KanJiaID;
                var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
                var varTitle = "" + varGetShopClientName + "正在进行" + decodeURIComponent(json.Topic) + "微砍价活动,最低" + json.EndPrice + "元可得,亲朋好友快来发起砍价吧!";
                do_MeGetAjaxShareWeiXin(WeiXin_imgAllPageUrl, varJURL, varTitle, decodeURIComponent(json.WeiXinDes));
                document.title = varTitle;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
        //error: function () {
        //    debugger;
        //    alert('fail');
        //}
    });


    return result;
}

function weChat() {
    $("#mcoverWeiKanJia").css("display", "none");  // 点击弹出层，弹出层消失
}


///发送访问消息
function doGameInfo_KanJia_doVisitWeiKanJiaAction(KanJiaID, varGetUseid, varMasterUserID, varParentID) {
    //debugger;
    if (varMasterUserID == undefined || varMasterUserID == "" || varMasterUserID == 0 || varMasterUserID == "0") {
        return;
    }

    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/doVisitWeiKanJiaAction?strWeiKanJiaID=" + KanJiaID + "&strUserID=" + varGetUseid + "&strMasterUserID=" + varMasterUserID + "&ShopClientID=" + ShopClientID;
    varURL += "&varParentID=" + varParentID;
    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp7051Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
        //error: function () {
        //    debugger;
        //    alert('fail');
        //}
    });


    return result;
}

///发送帮助砍价消息
function doGameInfo_KanJia_doHelpWeiKanJiaAction(KanJiaID, varGetUseid, varMasterUserID, varParentID) {
    //debugger;
    if (varMasterUserID == undefined || varMasterUserID == "" || varMasterUserID == 0 || varMasterUserID == "0") {
        return;
    }

    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    varURL = varServiceURL + "/Pub/WS_WeiKanJia.asmx/doHelpWeiKanJiaAction?strWeiKanJiaID=" + KanJiaID + "&strUserID=" + varGetUseid + "&strMasterUserID=" + varMasterUserID + "&ShopClientID=" + ShopClientID;
    varURL += "&varParentID=" + varParentID;
    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp7051Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
        //error: function () {
        //    debugger;
        //    alert('fail');
        //}
    });


    return result;
}

function ASKPayAsync() {
    var varGetUseid = getUserID();
    var varQueryStringList = new QueryString();
    var MasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
    var KanJiaID = varQueryStringList["kanjiaid"];
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    varURL = varServiceURL + "/Order/DoOrder.asmx/_Service_AddToCart_WeiKanJia?strWeiKanJiaID=" + KanJiaID + "&strUserID=" + varGetUseid + "&strMasterUserID=" + MasterUserID + "&ShopClientID=" + ShopClientID;
    var varUserSafeCode = localStorage.getItem('CurUserID201709121928_UserSafeCode');///ShareShopFunction  分享商城事件 全局变量使用
    var TSign = hex_md5_8(String(MasterUserID) + String(varGetUseid) + String(KanJiaID) + String(ShopClientID) + varUserSafeCode);
    varURL += "&TSign=" + TSign;

    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp", async: false,
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201602111154Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { //加载条
            layer.open({ type: 2, time: 2 });
        },
        success: function (data) {


            if (data.ErrorCode == "81") {///都准备好了，可以开始支付
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 1
                });
                self.location = '/cart_good.aspx?type=weikanjiafirstorderpay&buythisorderid=' + data.OrderINT;////   待付款  用户可以自己付款   可以提取第一个 未支付的订单进行支付
                ///buyThis(" + strOrderID + ")
                ///doPayAcyion(data.OrderINT, varpub_Int_ShopClientID);
            }
            else if (data.ErrorCode == "82") {///0支付  购物券支付，不用付钱
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 2
                });
                self.location = '/cart_good2.aspx';////   0支付  购物券支付，不用付钱
            }
            else if (data.ErrorCode == -44) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！库存不足！",
                    time: 1
                });
            }
            else {
                layer.open({
                    type: 2,
                    content: "未知错误，可能是库存不足",
                    time: 2
                });
            }
            return;

        },
        error: function () {
            // debugger;
            // alert('fail');
        }
    });


    return result;
}