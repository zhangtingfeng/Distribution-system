var $ResultSeed;
var Players;
var Winers;
var audio_Running, audio_GetOne;
var resizePart = window.WBActivity.resize = function () { };



$(document).ready(function () {
    var vardloginkeyIfNeedPassword = window.sessionStorage.getItem("loginkeyIfNeedPassword");
    //debugger;
    if (!vardloginkeyIfNeedPassword) {
    } else {
        $(".loginform").fadeOut()
    }

});


var getplayerStep1 = function () {

    //var varShopClientID = '<%=strShopClientID%>'; var var_ShopClient_XianChangHuoDongID = '<%=strXianChangHuoDongID%>'; var varSceenXianChangHuoDongNumber = '<%=strXianChangHuoDongNumberbyShopClientID%>'; var varServiceServicesURL = ""; var varServicesURL_HelpMachine = "";

    var varURL = varServiceServicesURL + "/Other" + "/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_doAllLotMember";
    varURL += "";

    $.ajax({
        type: "get",
        url: varURL,
        data: {
            ShopClientID: varShopClientID,
            ShopClient_XianChangHuoDongID: var_ShopClient_XianChangHuoDongID,
            SceenXianChangHuoDongNumber: varSceenXianChangHuoDongNumber
        },
        async: false,////取数据  。同步
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonpCallBack201605210738", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            resultErrorCode = json.ErrorCode;
            if (resultErrorCode == -4) {
                zAlert.Alert({
                    closed: false,
                    content: "无有效参与人数,游戏初始参数错误，请刷新重试",
                    callback: function () {
                        zAlert.Close();

                        var varJump = '/05XianChangHuoDong/WF_YaoYiYao-' + varShopClientID + '-' + var_ShopClient_XianChangHuoDongID + '.aspx';
                        window.location.href = varJump;
                        ///window.location.reload()

                    }
                })
                //acutdown_start.html("GO!")
            }
            else if (resultErrorCode == 0) {
                varBonusNumberByShopClientID = json.BonusNumber;///本次 抽奖的 编号
                getplayerStep2();///本次 抽奖的 编号
                //getplayerValidUser_lottery_luckyList(varBonusNumberByShopClientID);///已中奖 用户
            } else {
                zAlert.Alert({
                    closed: false,
                    content: "游戏初始参数错误，请刷新重试",
                    callback: function () {
                        zAlert.Close();
                        window.location.reload()
                    }
                })
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            zAlert.Alert({
                closed: false,
                content: "游戏初始参数错误，请刷新重试",
                callback: function () {
                    zAlert.Close();
                    window.location.reload()
                }
            })
        }
    });

};
///和主机 交互 获取 可参与 人数
function getplayerStep2() {
    ///;///本次 抽奖的 编号 {
    //var varShopClientID = '<%=strShopClientID%>'; var var_ShopClient_XianChangHuoDongID = '<%=strXianChangHuoDongID%>'; var varSceenXianChangHuoDongNumber = '<%=strXianChangHuoDongNumberbyShopClientID%>'; var varServiceServicesURL = ""; var varServicesURL_HelpMachine = "";

    var varURL = varServiceServicesURL + "/Pub/doWeiXianChang.asmx/doGet_ValidUserList";
    varURL += "";

    $.ajax({
        type: "get",
        url: varURL,
        data: {
            ShopClientID: varShopClientID,
            BonusNumberByShopClientID: varBonusNumberByShopClientID,
            ShopClient_XianChangHuoDongID: var_ShopClient_XianChangHuoDongID
        },
        async: false,////取数据  。同步
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonpCallBack201605222048", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            resultErrorCode = json.ErrorCode;
            if (resultErrorCode == -2) {
                zAlert.Alert({
                    closed: false,
                    content: "游戏交互初始参数错误，请刷新重试",
                    callback: function () {
                        zAlert.Close();
                        window.location.reload()
                    }
                })
            }
            else if (resultErrorCode == 0) {
                //varBonusNumberByShopClientID = json.BonusNumber;///本次 抽奖的 编号

                if ($.isArray(json.data)) {
                    Players = json.data;///本次 抽奖的 编号
                    $(".userNumber-label").html("编号" + varBonusNumberByShopClientID);
                    $(".usercount-label").html(Players.length + "人");
                }
                //acutdown_start.html("GO!")
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            zAlert.Alert({
                closed: false,
                content: "游戏初始参数错误，请刷新重试",
                callback: function () {
                    zAlert.Close();
                    window.location.reload()
                }
            })
        }
    });


};




var start = window.WBActivity.start = function () {
    window.WBActivity.hideLoading();
    $(".Panel.Top").css({
        top: 0
    });
    $(".Panel.Bottom").css({
        bottom: 0
    });
    $(".Panel.Lottery").css({
        display: "block",
        opacity: 1
    });
    //$(".mp_account_codeimage").hide();
    //debugger;
    //$.joinUser.show(PATH_ACTIVITY + "lottery_member",
    //function () {
    var cAudio_Running = document.getElementById("Audio_Running");
    if (cAudio_Running.play) {
        audio_Running = cAudio_Running
    }
    var bAudio_Result = document.getElementById("Audio_Result");
    if (bAudio_Result.play) {
        audio_GetOne = bAudio_Result;
    }
    $(".userNumber-label").html("");
    $(".usercount-label").html("加载数据中...");
    $(".control.button-run").show();
    $ResultSeed = $(".lottery-right .result-line");
    if (join_status == "1") {
        getplayerStep1();

    }
    //$(".join_user_btn").on("click",
    //function () {
    //    $.getJSON(PATH_ACTIVITY + "/lottery_status?callback=", {
    //        scene_id: scene_id,
    //        action: "start"
    //    },
    //    function (d) {
    //        if (d.ret == 0) {
    //            console.log("开始抽奖");
    //            join_status = 1;
    //            $.joinUser.hide();
    //            getplayerStep1()
    //        } else {
    //            alert(res.msg)
    //        }
    //    })
    //});
    $(".control.button-run").on("click",
    function () {
        start_game()
    });
    $(".control.button-stop").on("click",
    function () {
        stop_game();
        window.clearTimeout(stop_playanimate)
    });
    $(".control.button-nextround").on("click",
    function () {
        window.location.reload()
    });
    $(".button-reset").on("click",
    function () {
        window.location.reload();
        //join_status = 0;
        //$.getJSON(PATH_ACTIVITY + "/lottery_status?callback=", {
        //    scene_id: scene_id,
        //    action: "restart"
        //},
        //function (d) {
        //    if (d.ret == 0) {
        //        window.location.reload()
        //    } else {
        //        alert(d.msg)
        //    }
        //})
    });
    $(".button-showresult").on("click",
    function () {
        aresult_list()
    });
    function aresult_list(e) {
        debugger;

        //zAlert.Confirm({
        //    closed: false, title: "是否结束本轮抽奖", content: "结束本轮抽奖后，可以得到有效统计数据?", sureTxt: "否", sureCallback: function () {
        showReslutList(0);
        //        zAlert.Close()
        //    }, cancelTxt: "是", cancelCallback: function () {
        //        showReslutList(1);
        //        zAlert.Close()
        //    }
        //})


    }

    function showReslutList(varIFEndThisChouJiang) {
        var g = "result_list_ChouJiang.aspx?BonusNumberByShopClientID=" + varBonusNumberByShopClientID + "&ShopClientID=" + varShopClientID + "&IFEndThisChouJiang=" + varIFEndThisChouJiang;
        $.showPage(g)
    }


    $(".select-button").on("click",
    function (h) {
        var g = $(this),
        d = $(".select-value"),
        f = d.text();
        if (g.hasClass("minus")) {
            if (f > 1) {
                f--;
                d.text(f)
            }
        } else {
            if (g.hasClass("plus")) {
                if (f < Players.length) {
                    f++
                } else {
                    f = Players.length
                }
                d.text(f)
            }
        }
        h.preventDefault();
        return false
    })
    //})
};

var stop_playanimate;
var start_game = function () {
    winer_count = $(".select-value").text() * 1;
    var ausercount = parseInt($(".lottery-title .usercount-label").text());
    if (winer_count <= ausercount) {
        $(".control.button-run").hide();
        flgPlaying = true;
        curr_index = 0;



        ///预先设定中奖用户，前端只是随便 转转 中奖用户
        ///;///本次 抽奖的 编号 {
        //var varShopClientID = '<%=strShopClientID%>'; var var_ShopClient_XianChangHuoDongID = '<%=strXianChangHuoDongID%>'; var varSceenXianChangHuoDongNumber = '<%=strXianChangHuoDongNumberbyShopClientID%>'; var varServiceServicesURL = ""; var varServicesURL_HelpMachine = "";
        var varURL = varServiceServicesURL + "/Pub/doWeiXianChang.asmx/doGet_ValidUser_lottery_luckyList";
        varURL += "";

        $.ajax({
            type: "get",
            url: varURL,
            data: {
                ShopClientID: varShopClientID,
                BonusNumberByShopClientID: varBonusNumberByShopClientID,
                ShopClient_XianChangHuoDongID: var_ShopClient_XianChangHuoDongID,
                winer_countnum: winer_count
            },
            async: false,////取数据  。同步
            dataType: "jsonp",
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonpCallBack201605260645", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
            success: function (json) {
                resultErrorCode = json.ErrorCode;
                if (resultErrorCode == -2) {
                    zAlert.Alert({
                        closed: false,
                        content: "游戏交互初始参数错误，请刷新重试",
                        callback: function () {
                            zAlert.Close();
                            window.location.reload()
                        }
                    })
                }
                else if (resultErrorCode == 0) {
                    //varBonusNumberByShopClientID = json.BonusNumber;///本次 抽奖的 编号

                    if ($.isArray(json.data)) {
                        Winers = json.data;///本次 抽奖的 编号

                        playanimate();
                        window.setTimeout(function () {
                            $(".control.button-stop").fadeIn()
                        },
                        500);
                        if (audio_Running) {
                            audio_Running.play()
                        }
                    }
                    //acutdown_start.html("GO!")
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                zAlert.Alert({
                    closed: false,
                    content: "游戏初始参数错误，请刷新重试",
                    callback: function () {
                        zAlert.Close();
                        window.location.reload()
                    }
                })
            }
        });
    } else {
        zAlert.Alert({
            content: "计划选" + winer_count + "人，但是只剩" + ausercount + "人可选，请减少选取数！",
            callback: function () {
                zAlert.Close()
            }
        })
    }
};
var stop_game = function () {
    $(".control.button-stop").hide();
    $(".control.button-run").show();

    if (audio_Running) {
        audio_Running.pause();
    }

    if ($.isArray(Players)) {
        winer_count = $(".select-value").text() * 1;
        var a = parseInt($(".lottery-title .usercount-label").text());
        if (winer_count <= a) {
            $(".lottery-title .usercount-label").html(a - winer_count);
            getWiner();
        } else {
            zAlert.Alert({
                content: "计划选" + winer_count + "人，但是只剩" + Players.length + "人可选，请减少选取数！",
                callback: function () {
                    zAlert.Close()
                }
            })
        }
    } else {
        zAlert.Alert({
            content: "无法获得游戏数据，与游戏服务器断开，请刷新重试！",
            callback: function () {
                zAlert.Close()
            }
        })
    }
};


///告诉服务器端 有人 中奖聊
var tellServerWiner = function () {
    var atellServerWinerWiners = Winers;
    if (atellServerWinerWiners.length > 0) {
        var varselectedIndex = DropDownList_LevelList.selectedIndex;
        var varselectedtext = (DropDownList_LevelList.options[varselectedIndex].text);///encodeURI(encodeURI

        var varstringUserIDList = "";
        for (var i = 0; i < atellServerWinerWiners.length; i++) {
            varstringUserIDList += atellServerWinerWiners[i].UserID + "#";
        }
        varstringUserIDList = varstringUserIDList.substring(0, varstringUserIDList.length - 1);

        var varURL = varServiceServicesURL + "/Pub/doWeiXianChang.asmx/dotellServerWiner_luckyList";
        varURL += "";

        $.ajax({
            type: "get",
            url: varURL,
            data: {
                ShopClientID: varShopClientID,
                BonusNumberByShopClientID: varBonusNumberByShopClientID,
                ShopClient_XianChangHuoDongID: var_ShopClient_XianChangHuoDongID,
                selectedIndex: varselectedIndex,
                selectedtext: varselectedtext,
                UserIDList: varstringUserIDList
            },
            async: true,////可以异步
            dataType: "jsonp",
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonpCallBack201605260945", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
            success: function (json) {
                resultErrorCode = json.ErrorCode;
                if (resultErrorCode == 0) {

                }
                else {
                    zAlert.Alert({
                        closed: false,
                        content: "游戏交互初始参数错误，请刷新重试",
                        callback: function () {
                            zAlert.Close();
                            window.location.reload()
                        }
                    })
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                zAlert.Alert({
                    closed: false,
                    content: "游戏初始参数错误，请刷新重试",
                    callback: function () {
                        zAlert.Close();
                        window.location.reload()
                    }
                })
            }
        });

    }

}


////停止运动 。得到  幸运的 号码
var winer_count = 0;
var getWiner = function () {

    var varselectedIndex = DropDownList_LevelList.selectedIndex;
    var varselectedtext = (DropDownList_LevelList.options[varselectedIndex].text);///encodeURI(encodeURI


    flgPlaying = false;
    window.clearTimeout(tmr_playanimate);
    var aWiners = Winers;
    tellServerWiner();///告诉服务器端 有人 中奖聊

    for (var b = aWiners.length - 1; b >= 0; b--) {
        $(".lottery-run .user").css({
            "background-image": "url(" + aWiners[b].HeadImageUrl + ")"
        }).attr({
            HeadImageUrl: aWiners[b].HeadImageUrl,
            UserID: aWiners[b].UserID,
            UserNickName: aWiners[b].UserNickName + "(" + varselectedtext + ")"
        });

        $(".lottery-run .user .nick-name").html(aWiners[b].UserNickName + "(" + varselectedtext + ")");
        console.log("31" + aWiners[b].UserNickName);
        console.log("32" + $(".lottery-run .user .nick-name").html());


        getUserAnimation();//。得到  幸运的 号码的动画
    };
    window.setTimeout(function () {
        $(".lottery-run .user").attr("style", "");
        $(".lottery-run .user .nick-name").html("")
    },
    300);
};



var getUserAnimation = function () {///停止运动 。得到  幸运的 号码的动画
    if (audio_GetOne) {
        audio_GetOne.play()
    }
    $(".lottery-right").scrollTop(0);
    var bresultlength = $(".lottery-right").scroll(0).children(".result-line").length - 1;
    var aResultSeed = $ResultSeed.clone();
    aResultSeed.find(".result-num").html((bresultlength + 1));
    aResultSeed.prependTo(".lottery-right").slideDown();////整体下移
    var eAllresultline = aResultSeed.offset();
    var cFromRun = $(".lottery-run .user");

    console.log("1" + cFromRun.css("background-image"));

    var dcFromRun = cFromRun.clone().appendTo("body").css({
        position: "absolute",
        top: cFromRun.offset().top,
        left: cFromRun.offset().left,
        width: cFromRun.width(),
        height: cFromRun.height()
    }).animate({
        width: 60,
        height: 60,
        top: eAllresultline.top + 5,
        left: eAllresultline.left + 50
    },
    500,
    function () {
        var g = dcFromRun.css("background-image");

        dcFromRun.appendTo(aResultSeed).removeAttr("style").css({
            "background-image": g
        });
        console.log("2" + dcFromRun.css("background-image"));

      

    })
};


var curr_index = 0;
var flgPlaying = false;
var tmr_playanimate;
var playanimate = function () {
    if (Players[curr_index]) {
        var aPlayers = Players[curr_index];
        $(".lottery-run .user").css({
            "background-image": "url(" + aPlayers.HeadImageUrl + ")"
        });
        $(".lottery-run .user .nick-name").html(decodeURIComponent(aPlayers.UserNickName));
        curr_index++;
        if (curr_index >= Players.length) {
            curr_index = 0
        }
        if (flgPlaying) {
            tmr_playanimate = window.setTimeout(playanimate, 100)
        }
    }
};