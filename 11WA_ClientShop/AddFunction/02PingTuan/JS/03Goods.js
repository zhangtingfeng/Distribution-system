function doLoadGoodsDocumnet() {

    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();
            var varTuanGouID = varQueryStringList["tuangouid"];
            var vartuangouidnumber = varQueryStringList["tuangouidnumber"];
            doAjaxLoadTuanGouGood(varTuanGouID, varGetUseid, vartuangouidnumber);
            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsTuanGou");

        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }
}



function doAjaxLoadTuanGouGood(varTuanGouID, varGetUseid, vartuangouidnumber) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLGameInfo_TuanGou = varServiceURL + "/Pub/doTuanGou.asmx/doGameInfo_TuanGou?strTuanGouID=" + varTuanGouID + "&strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID + "&strtuangouidnumber=" + vartuangouidnumber;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURLGameInfo_TuanGou,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp20160618Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        beforeSend: function () {
            $("#Anounce").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
            $("#content_TuanFouRule").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
            $("#content_LongInfo").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
        },
        success: function (jsonReturn) {
            result = jsonReturn.ErrorCode;
            var json = jsonReturn.ThisTuanGouGoodInfo;
            if (result == 0) {
                varSourceGoodID = json.SourceGoodID;

                document.title = "团购" + decodeURIComponent(json.GoodName);

                $("#TuanGoodName").html(decodeURIComponent(json.GoodName));
                $("#TuanShortInfo").html(decodeURIComponent(json.ShortInfo));
                $("#TuanEachPeoplePrice").html("￥" + json.EachPeoplePrice);///开团或者支付参团
                $("#TuanEachPeoplePriceMySelf").html("￥" + json.EachPeoplePrice);///独立开团
                $("#TuanHowManyPeople").html(json.HowManyPeople + "人团");
                $("#TuanHowManyPeopleMySelf").html(json.HowManyPeople + "人团");
                $("#TuanPromotePrice").html(json.PromotePrice);
                $("#content_TuanFouRule").html(decodeURIComponent(json.TuanFouRule));
                $("#content_LongInfo").html(decodeURIComponent(json.LongInfo));

                $("#InviteHowmanyPeople").html(json.HowManyPeople - 1);
                doMakeHtml_AnnouncePic_GoodList(json.SourceGoodID)
                //$("#Anounce").html(decodeURIComponent(json.AnouncePic));

                if (json.ParterRole > 0)///说明 不是团长就是 团员
                {//参与人角色  1 表示发起人  2  表示 参与人
                    var varTuanParterDesc = "";
                    if (json.ParterRole == 1) {
                        varTuanParterDesc = "尊敬的团长大人,";
                        varTuanParterDesc += "已有" + json.ParterCount + "人参与您发起的团购,";
                        if (json.HowManyPeople - json.ParterCount == 0) {
                            varTuanParterDesc += "<span style=\"color:blue\">拼团完成</span>,请查看物流情况."
                        }
                        else {
                            varTuanParterDesc += "还需要" + (json.HowManyPeople - json.ParterCount) + "人参与才能成功.点击右上角发送朋友圈邀请亲朋好友来参与吧";
                        }
                    }
                    else if (json.ParterRole == 2) {
                        varTuanParterDesc = "感谢您参与团购,本团购正在进行中.";
                        varTuanParterDesc += "已有" + json.ParterCount + "人和您共同团购,";
                        if (json.HowManyPeople - json.ParterCount == 0) {
                            varTuanParterDesc += "<span style=\"color:blue\">拼团完成</span>,请查看物流情况."
                        }
                        else {
                            varTuanParterDesc += "还需要" + (json.HowManyPeople - json.ParterCount) + "人参与才能成功.点击右上角快发送朋友圈邀请亲朋好友来参与吧";
                        }
                    }

                    var uiidTuanZhangTeam_Content = document.getElementById("idTuanZhangTeam_Content");
                    uiidTuanZhangTeam_Content.style.display = "block";
                    var uiididTuanZhangTeam_title = document.getElementById("idTuanZhangTeam_title");
                    uiididTuanZhangTeam_title.style.display = "block";

                    $("#idTuanZhangTeam_title").html(varTuanParterDesc);
                }
                var varParterRoleList = (json.ParterRoleList);
                if (varParterRoleList.length > 0) {
                    //$("#TuanHowManyPeople").html($("#TuanHowManyPeople").html() + " 支付参团");

                    var uiidTuanZhangTeam_Content = document.getElementById("idTuanZhangTeam_Content");
                    uiidTuanZhangTeam_Content.style.display = "block";

                    var uiTuanYuanList = document.getElementById("TuanYuanList");
                    uiTuanYuanList.style.display = "block";

                    var varThisLine = "";
                    varThisLine += "<div class=\"MemberShip\" style=\"font-weight:bold;line-height:20px;height:20px;\">";
                    varThisLine += "    <span class=\"HeadIMG\">头像 </span>";
                    varThisLine += "    <span class=\"NickName\">昵称</span>";
                    varThisLine += "    <span class=\"ParterRoleDesc\">组团情况</span>";
                    varThisLine += "   <span class=\"PayTime\">参团日期</span>";
                    varThisLine += "    <div></div>";
                    varThisLine += "</div>";
                    $("#TuanYuanList").append(varThisLine);

                    for (var i = 0; i < varParterRoleList.length; i++) {
                        varThisLine = "";
                        varThisLine += "<div class=\"MemberShip\">";
                        //<img src="http://qiniu.eggsoft.cn/upload/000001_sh/images/201412290104267888.jpg">
                        varThisLine += "<span class=\"HeadIMG\"><a href=\"" + varParterRoleList[i].EachHeadImageUrl + "\"> <img class=\"HeadIMGCss\" src=\"" + varParterRoleList[i].EachHeadImageUrl + "\"></a></span>";
                        varThisLine += "<span class=\"NickName\">" + decodeURIComponent(varParterRoleList[i].EachNickName) + "</span>";
                        varThisLine += "<span class=\"ParterRoleDesc\">" + decodeURIComponent(varParterRoleList[i].EachParterRoleDesc) + "</span>";
                        varThisLine += "<span class=\"PayTime\">" + varParterRoleList[i].EachPayTime + "</span>";
                        varThisLine += "<\div>";
                        $("#TuanYuanList").append(varThisLine);
                    }

                    var VARJoinTuanGouTip = "已有" + varParterRoleList.length + "人参加团长“";
                    VARJoinTuanGouTip += decodeURIComponent(varParterRoleList[0].EachNickName) + "”的团,";
                    VARJoinTuanGouTip += "还需要" + (parseInt(json.HowManyPeople) - json.ParterCount) + "人可以组团成功.请猛戳下方按钮“支付参团”.";
                    VARJoinTuanGouTip += "   详见下方拼团玩法";
                    $("#JoinTuanGouTip").html(VARJoinTuanGouTip);
                    $("#TuanHowManyPeople").html("支付参团");
                    $("#TuanHowManyPeopleMySelf").html("独立开团");

                    if (json.ParterRole != 1) {////参与人 才有这个选项
                        document.getElementById("buyZuTuan").style.width = "30%"
                        document.getElementById("buySingle").style.width = "31%";
                        document.getElementById("buyMySelfZuTuan").style.width = "30%";
                        document.getElementById("buyMySelfZuTuan").style.display = "block";
                    }
                }
                else {
                    $("#TuanHowManyPeople").html($("#TuanHowManyPeople").html() + " 支付开团");
                    $("#TuanHowManyPeopleMySelf").html($("#TuanHowManyPeople").html() + " 支付独立开团");
                }



                doShareWeiXin(json.TuanGouIDNumber);

                var intDiff = json.doubleMaxLengthTime;
                if (intDiff > 0) {
                    var uiidTuanZhangTeam_Countdown = document.getElementById("Countdown");
                    uiidTuanZhangTeam_Countdown.style.display = "block";
                    timer(intDiff);
                } else if (intDiff < 0) {
                    $("#TuanHowManyPeople").html("本活动已终止");
                    $("#TuanHowManyPeopleMySelf").html("本活动已终止");
                    $("#buyZuTuan").removeAttr("onclick");////移除参团购买事件
                    $("#buyMySelfZuTuan").removeAttr("onclick");////移除独团购买事件

                }
            }
            else if (result == -2) {
                $("#TuanShortInfo").html("团购产品非法访问");
                $("#content_TuanFouRule").html("团购产品非法访问");
                $("#content_LongInfo").html("团购产品非法访问");
            }
            else if (result == -3) {
                $("#TuanShortInfo").html("产品已下线");
                $("#content_TuanFouRule").html("产品已下线");
                $("#content_LongInfo").html("产品已下线");
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


function doMakeHtml_AnnouncePic_GoodList(varstrGoodID) {
    //var city = $("#Text1").val();
    varURL = varServiceURL + "/Pub/doClickThis_HowToGetProduct.asmx/doGetProduct_newTmpletAnnouncePic_GoodList";
    $.ajax({
        type: 'GET',
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        data: 'strGoodID=' + varstrGoodID,
        jsonpCallback: "jsonp201607031724Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
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


function doShareWeiXin(varTuanGouIDNumber) {
    varGlobalTuanGouIDNumber = varTuanGouIDNumber;//参与谁的团  购买时间使用该变量
    var varQueryStringList = new QueryString();

    var varGetUseid = getUserID();
    var varParentID = 0;
    var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
    //alert("varDB_ParentID11=" + varDB_ParentID);
    if ((varDB_ParentID != null) && (varDB_ParentID != undefined) && (varDB_ParentID != '')) {
        //alert("varDB_ParentID1=" + varDB_ParentID);
        varParentID = varDB_ParentID;
    }
    else {
        var varparentagentid = varQueryStringList["parentagentid"];////转发ID
        var varparentagentadid = varQueryStringList["parentagentadid"];////转发ID
        if ((varparentagentadid != null) && (varparentagentadid = undefined)) {
            varParentID = varparentagentadid;
            //alert("varDB_ParentID2=" + varDB_ParentID);
        }
        else if ((varparentagentid != null) && (varparentagentid = undefined)) {
            varParentID = varparentagentid;
            //alert("varDB_ParentID3=" + varDB_ParentID);
        }
        //alert("varDB_ParentID4=" + varDB_ParentID);
    }
    //var varTuanGouIDNumber = varQueryStringList["TuanGouIDNumber"];////没有就是 没有 没有的话只能发起
    //varTuanGouIDNumber=

    var varMasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
    var host = window.location.host;
    var varJURL = "https://" + host + "/addfunction/02pingtuan/03goods.html?tuangouid=" + varQueryStringList["tuangouid"];
    varJURL = varJURL + "&parentagentid=" + varParentID + "&tuangouidnumber=" + varTuanGouIDNumber;
    if (varMasterUserID != undefined && varMasterUserID != "" && varMasterUserID != 0 && varMasterUserID != "0") {
        varJURL = varJURL + "&MasterUserID=" + varMasterUserID;
    }
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var varimg = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
    var varUserNickName = localStorage.getItem('GetUserNickName201709121928_Open_0609');

    var vardesc = varGetShopClientName + "微团购活动." + varUserNickName + ".不用囤货.不用发货。";
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    //alert(varJURL);
    do_GetAjaxShareWeiXin(varShopClientID, varJURL, "拼团" + " " + document.title + varGetShopClientName, vardesc, varimg, ShareShopFunction);
}

function buySingleORZuTuan(varbuySingleORZuTuan1Or2) {
    if (varbuySingleORZuTuan1Or2 == 1) {////单独购买
        layer.open({
            type: 2,
            content: "请稍等,正在为您准备购物车",
            time: 3
        });

        var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
        var varGetURLBuy = "";
        varGetURLBuy += "UserID=" + varUserID;
        varGetURLBuy += "&goodid=" + varSourceGoodID;
        varGetURLBuy += "&parentid=" + varDB_ParentID;
        varGetURLBuy += "&buycount=1";
        varGetURLBuy += "&multibuytype=0";

        var TSign = hex_md5_8(String(varUserID) + String(varSourceGoodID) + String(varDB_ParentID) + "1" + "0" + varUserSafeCode);

        varGetURLBuy += "&TSign=" + TSign;
        //单独购买的
        var url = varServiceURL + "/Order/DoOrder.asmx/_Service_AddToCart?" + varGetURLBuy; //单独购买的
        var result = -1;
        $.ajax({
            type: "get",
            url: url,
            dataType: "jsonp",
            async:false,
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonp7160621Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                result = parseInt(json.ErrorCode);
                if (result == -1) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！",
                        time: 1
                    });
                }
                else if (result == -2) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！购买限制，在订单中已存在！",
                        time: 1
                    });
                }
                else if (result == -44) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！库存不足！",
                        time: 1
                    });
                }
                self.location = '/cart.aspx';////   转入购物车
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.status);
                console.log(XMLHttpRequest.readyState);
                console.log(textStatus);
            }
        });
    }
    else if (varbuySingleORZuTuan1Or2 == 2 || varbuySingleORZuTuan1Or2 == 3) {///组团购买
        layer.open({
            type: 2,
            content: "请稍等,正在为准备团购商品.有效支付后分享朋友圈后才能完成组团购买.",
            time: 3
        });
        var varQueryStringList = new QueryString();
        var varTuanGouID = varQueryStringList["tuangouid"];

        var varTuanGouIDNumber = varGlobalTuanGouIDNumber;//varQueryStringList["TuanGouIDNumber"];
        if (varbuySingleORZuTuan1Or2 == 3) varTuanGouIDNumber = 0;//独立开团购买的

        var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
        var varGetURLBuy = "";
        varGetURLBuy += "UserID=" + varUserID;
        varGetURLBuy += "&goodid=" + varSourceGoodID;
        varGetURLBuy += "&parentid=" + varDB_ParentID;
        varGetURLBuy += "&buycount=1";
        varGetURLBuy += "&multibuytype=0";
        varGetURLBuy += "&tuangouid=" + varTuanGouID;
        varGetURLBuy += "&tuangouidnumber=" + varTuanGouIDNumber;


        var url = varServiceURL + "/Order/DoTuanGou.asmx/_Service_AddToCart_TuanGou?" + varGetURLBuy;

        var result = -1;
        $.ajax({
            type: "get",
            url: url,
            dataType: "jsonp", async: false,
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonp7160622Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                result = parseInt(json.ErrorCode);

                if (result == -1) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！",
                        time: 1
                    });
                }
                else if (result == -22) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！购买限制，在订单中已存在！",
                        time: 2,
                        end: function (layer) {
                            self.location = '/cart_good.aspx';////转到订单表
                        }
                    });
                }
                else if (result == -23) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！购买限制，在购物车中已存在！",
                        time: 2,
                        end: function (layer) {
                            self.location = '/cart.aspx';////转到购物车
                        }
                    });
                }
                else if (result == -44) {
                    layer.open({
                        type: 2,
                        content: "购物车添加失败！库存不足！",
                        time: 1
                    });
                }
                else if (result == 1) {
                    self.location = '/cart.aspx';////   转入购物车
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.status);
                console.log(XMLHttpRequest.readyState);
                console.log(textStatus);
            }
        });
    }
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



