//var rankTopTenList = [];
var RanksPositionList = [];
var PlayersShuzuList = {};///第一名  第二名 第三名
var audio_CutdownPlayer, audio_NewPlayer, audio_Outride, audio_Gameover;/////定义4个声音 object
function findUserByID(cUserIDid, arankTopTenList) {
    if ($.isArray(arankTopTenList)) {
        //var blength = arankTopTenList.length;
        var blength = arankTopTenList.length;
        while (blength--) {
            if (arankTopTenList[blength]["UserIDid"] == cUserIDid) {
                return blength
            }
        }
        return -1
    } else {
        return -1
    }
}


$(document).ready(function () {
    var vardloginkeyIfNeedPassword = window.sessionStorage.getItem("loginkeyIfNeedPassword");
    //debugger;
    if (!vardloginkeyIfNeedPassword) {
    } else {
        $(".loginform").fadeOut()
    }

    if (Audio_GameBackground) {
        Audio_GameBackground.play();
    }

});

///混乱
function mess(brankTopTenList) {
    ///求一个浮点数的地板，就是求一个最接近它的整数，它的值小于或等于这个浮点数。
    var cfloor = Math.floor,
    hrandom = Math.random,
    aList_length = brankTopTenList.length,
    f, e, d, g = cfloor(aList_length / 2) + 1;
    while (g--) {
        f = cfloor(hrandom() * aList_length);
        e = cfloor(hrandom() * aList_length);
        if (f !== e) {
            d = brankTopTenList[f];
            brankTopTenList[f] = brankTopTenList[e];
            brankTopTenList[e] = d
        }
    }
    return brankTopTenList
}
function RndRank() {
    mess(PlayersShuzuList)
} (function (b) {
    var c = 0;
    var d = ["ms", "moz", "webkit", "o"];
    for (var a = 0; a < d.length && !b.requestAnimationFrame; ++a) {
        b.requestAnimationFrame = b[d[a] + "RequestAnimationFrame"];
        b.cancelAnimationFrame = b[d[a] + "CancelAnimationFrame"] || b[d[a] + "CancelRequestAnimationFrame"]
    }
    if (!b.requestAnimationFrame) {
        b.requestAnimationFrame = function (i, f) {
            var e = new Date().getTime();
            var g = Math.max(0, 16 - (e - c));
            var h = b.setTimeout(function () {
                i(e + g)
            },
            g);
            c = e + g;
            return h
        }
    }
    if (!b.cancelAnimationFrame) {
        b.cancelAnimationFrame = function (e) {
            clearTimeout(e)
        }
    }
})(window); (function (a) {
    a.GameTimer = function (b, c) {
        this.__fn = b;
        this.__timeout = c;
        this.__running = false;
        this.__lastTime = Date.now();
        this.__stopcallback = null
    };
    a.GameTimer.prototype.__runer = function () {
        if (Date.now() - this.__lastTime >= this.__timeout) {
            this.__lastTime = Date.now();
            this.__fn.call(this)
        }
        if (this.__running) {
            a.requestAnimationFrame(this.__runer.bind(this))
        } else {
            if (typeof this.__stopcallback === "function") {
                a.setTimeout(this.__stopcallback, 100)
            }
        }
    };
    a.GameTimer.prototype.start = function () {
        this.__running = true;
        this.__runer()
    };
    a.GameTimer.prototype.stop = function (b) {
        this.__running = false;
        this.__stopcallback = b
    }
})(window);
var tick = 1000;
var LineLength = $(window).width();
var PlayStep = 16;
var flgGameStop = false;
var mainTick = new GameTimer(function () {
    debugger;
    $.each(RanksPositionList,
    function (cPos, dValue) {
        var e = dValue + PlayStep;
        if (cPos == 0 || e < RanksPositionList[cPos - 1] - size_Height_1_20 * 3 / 4) {
            RanksPositionList[cPos] = e
        }
    });
    setTopLeftAll20();
    if (flgGameStop) {
        debugger;
        window.clearTimeout(tmr_GameDataLoad);
        mainTick.stop();
        var b = $(".tracklist").width() - size_Height_1_20 + diff;
        for (var a = 0; a < RanksPositionList.length; a++) {
            RanksPositionList[a] = b
        }
        setTopLeftAll20()
    }
},
tick);
////游戏进行中  会一直调用这个   LineLength 窗口宽度  CUTDOWN_TIME=30  应该是最长30秒

///得到每个精灵的位置
function gameTick() {
    $.each(PlayersShuzuList,
    function (a, Item) {
        //var c = parseInt(bUserShakeCount) + LineLength / (CUTDOWN_TIME / (tick / 1000));
        //var cSpriteXPos = parseInt(bThisUserAllScoreShakeCount) * 1.0 * LineLength / varCountHowMany;
        //debugger;
        var cSpriteXPos = parseInt(PlayersShuzuList[a].ThisUserAllScoreShakeCount) * 1.0 * roundLength / varCountHowMany;


        //if (a == 0 || c < RanksPositionList[a - 1] - size_Height_1_20 * 3 / 4) {
        RanksPositionList[a] = cSpriteXPos
        //}
    });
    ///debugger;
    setTopLeftAll20()
}

function setTopLeftAll20() {
    $(".tracklist .player").each(function () {
        var bEverytrack = $(this),
        aUserIDid = bEverytrack.attr("UserIDid");
        if (findUserByID(aUserIDid, PlayersShuzuList) < 0) {
            bEverytrack.remove().removeClass("rotateout")
        }
    });

    //$.each(Array, function (i, value) {
    //    this;      //this指向当前元素
    //    i;         //i表示Array当前下标
    //    value;     //value表示Array当前元素
    //});

    $.each(PlayersShuzuList,
    function (b_i, d_Value) {////示Array当前下标   value表示Array当前元素
        var cidNum = d_Value.idNum;
        var cUserIDid = d_Value.UserIDid;
        //debugger;
        var now_player_player = $(".player.player" + cidNum).attr("UserIDid");
        if (now_player_player == undefined)///需要页面追加
        {
            var aghost = PlayersShuzuList[cidNum].$elm = $PlayeSeed.clone().addClass("player" + cidNum).attr("UserIDid", cUserIDid);
            aghost.find(".head").css({
                "background-image": "url(" + PlayersShuzuList[b_i]["avatar"] + ")"
            }).addClass("shake");
            aghost.find(".nickname").html(decodeURIComponent(PlayersShuzuList[b_i]["nick_name"]));
            if (audio_NewPlayer) {
                audio_NewPlayer.play()
            }


            var e = RanksPositionList[b_i] - size_Height_1_20 * 2;
            if (e < 0) {
                e = 0
            }
            aghost.css({
                left: e,
                top: lineHeight * b_i + diff
            }).appendTo(".tracklist")
        }
        else if (now_player_player != cUserIDid) {
            $(".player.player" + cidNum).find(".head").css({
                "background-image": "url(" + PlayersShuzuList[b_i]["avatar"] + ")"
            }).addClass("shake");
            $(".player.player" + cidNum).find(".nickname").html(decodeURIComponent(PlayersShuzuList[b_i]["nick_name"]));
            $(".player.player" + cidNum).attr("UserIDid", cUserIDid);

            $(".player.player" + cidNum).css({
                left: RanksPositionList[b_i] + "px",
                top: lineHeight * b_i + diff
            })

            //if (PlayersShuzuList[cidNum]) {
            //    if (!PlayersShuzuList[cidNum].$elm) {

            //    } else {
            //        if (audio_Outride) {
            //            audio_Outride.play()
            //        }
            //    }
            //    var e = RanksPositionList[b_i] - size_Height_1_20 * 2;
            //    if (e < 0) {
            //        e = 0
            //    }
            //    PlayersShuzuList[cidNum].$elm.css({
            //        left: e,
            //        top: lineHeight * b_i + diff
            //    }).appendTo(".tracklist")
            //} else { }
        } if (now_player_player == cUserIDid) {///原轨道不变 设置位置即可
            $(".player.player" + cidNum).css({
                left: RanksPositionList[b_i] + "px",
                top: lineHeight * b_i + diff
            })
        }
    })
}


function showGameResult() {
    var b = $(".result-layer").show();
    var d = $(".result-label", b).show().addClass("pulse");
    var a = $(".result-cup", b).hide();
    var cGetBonus = 3;
    if (audio_Gameover) {
        audio_Gameover.play()
    }
    window.setTimeout(function () {
        d.fadeOut(function () {
            a.show(function () {
                if (cGetBonus >= 1 && PlayersShuzuList[0]) {///0第一名
                    window.setTimeout(function () {
                        var e = $PlayeSeed.clone().addClass("result").css({
                            left: "50%",
                            "margin-left": "-65px",
                            width: "160px",
                            height: "160px",
                            bottom: "150px"
                        });
                        e.find(".head").css({
                            "background-image": "url(" + PlayersShuzuList[0]["UserIDWeiXinHeadURL"] + ")"
                        }).addClass("shake");
                        e.find(".nickname").html(decodeURIComponent(PlayersShuzuList[0]["nick_name"]));
                        e.appendTo(a).addClass("bounce")
                    },
                    800)
                }
                if (cGetBonus >= 2 && PlayersShuzuList[1]) {///1第一名
                    window.setTimeout(function () {
                        var e = $PlayeSeed.clone().addClass("result").css({
                            left: "40px",
                            width: "100px",
                            height: "100px",
                            bottom: "120px"
                        });
                        e.find(".head").css({
                            "background-image": "url(" + PlayersShuzuList[1]["UserIDWeiXinHeadURL"] + ")"
                        }).addClass("shake");
                        e.find(".nickname").html(decodeURIComponent(PlayersShuzuList[1]["nick_name"]));
                        e.appendTo(a).addClass("bounce")
                    },
                    1800)
                }
                if (cGetBonus >= 3 && PlayersShuzuList[2]) {///3第一名
                    window.setTimeout(function () {
                        var e = $PlayeSeed.clone().addClass("result").css({
                            right: "30px",
                            width: "70px",
                            height: "70px",
                            bottom: "100px"
                        });
                        e.find(".head").css({
                            "background-image": "url(" + PlayersShuzuList[2]["UserIDWeiXinHeadURL"] + ")"
                        }).addClass("shake");
                        e.find(".nickname").html(decodeURIComponent(PlayersShuzuList[2]["nick_name"]));
                        e.appendTo(a).addClass("bounce")
                    },
                    2800)
                }
            })
        }).removeClass("pulse")
    },
    1000)
}

var $PlayeSeed, lineHeight, diff = 10;
var size_Height_1_20;
var resizePart = window.WBActivity.resize = function () {
    var bPanel_Track = $(".Panel.Track"),
    atrackchildrenlist = bPanel_Track.find(".tracklist").children();
    size_Height_1_20 = lineHeight = bPanel_Track.height() / SHAKE_LINE;
    roundLength = $(".Panel.Track .tracklist").width() - size_Height_1_20;
    atrackchildrenlist.each(function () {
        $(this).css({
            height: size_Height_1_20,
            "line-height": size_Height_1_20 + "px",
            "font-size": size_Height_1_20 * 3 / 5 + "px"
        }).find(".track-start,.track-end").css({
            width: size_Height_1_20 + "px",
            height: size_Height_1_20 + "px"
        })
    });
    $PlayeSeed = $('<div class="player"><div class="head"></div><div class="nickname"></div></div>').css({
        width: size_Height_1_20 - diff * 2,
        height: size_Height_1_20 - diff * 2
    })
};

var start = window.WBActivity.start = function () {
    ///输入密码后 会来到这里  123456
    window.WBActivity.hideLoading();
    $(".Panel.Top").css({
        top: 0
    });
    $(".Panel.Bottom").css({
        bottom: 0
    });
    $(".Panel.Track").css({
        display: "block",
        opacity: 1
    });
    //debugger;正在圆环上显示用户
    $.joinUser.show(varServiceServicesURL + "/Other" + "/01XianChangHuoDong/01XianChangHuoDong.asmx/doshake_parter",
    function () {
        //debugger;
        var fCutdownPlayer = document.getElementById("Audio_CutdownPlayer");
        if (fCutdownPlayer.play) {
            audio_CutdownPlayer = fCutdownPlayer
        }
        var eNewPlayer = document.getElementById("Audio_NewPlayer");
        if (eNewPlayer.play) {
            audio_NewPlayer = eNewPlayer
        }
        var dOutride = document.getElementById("Audio_Outride");
        if (dOutride.play) {
            audio_Outride = dOutride
        }
        var bGameover = document.getElementById("Audio_Gameover");
        if (bGameover.play) {
            audio_Gameover = bGameover
        }

        createTrack();
        resizePart();
        ///默认赛道数量
        for (var a = 0; a < SHAKE_LINE; a++) {
            RanksPositionList[a] = 0
        }
        $(".game-start").on("click",
        function (g) {
            cutdown_start()
        });
        $(".button.reset").on("click",////再玩一次
        function () {
            nextRound();
        });
        $(".button.result").on("click",
        function () {
            //alert("暂时没有这个功能");
            cresult_list()
        });
        function cresult_list(h) {
            debugger;
            var g = "result_list.aspx?SceenXianChangHuoDongNumber=" + varSceenXianChangHuoDongNumber + "&ShopClientID=" + varShopClientID;
            if (h != undefined) {
                g += "&rotate_id=" + h
            }
            $.showPage(g)
        }
    })
};



var tmr_cutdown_start;
var cutdown_start = function () {
    var varCount = $(".join_num em").html();
    if (varCount == "0") {
        zAlert.Alert({
            closed: true,
            content: "游戏参与人数不足,无法启动",
            callback: function () {
                zAlert.Close();
            }
        })
        return;
    }

    //debugger;
    $.joinUser.hide();
    join_status = 1; //0还没有倒计时开始  1表示 开始聊
    var acutdown_start = $(".cutdown-start"),
    bready_time = SHAKE_INFO.ready_time * 1 + 1;///default 准备时间5
    acutdown_start.html("").show().css({
        "margin-left": -acutdown_start.width() / 2 + "px",
        "margin-top": -acutdown_start.height() / 2 + "px",
        "font-size": acutdown_start.height() * 0.7 + "px",
        "line-height": acutdown_start.height() + "px"
    }).addClass("cutdownan-imation");
    tmr_cutdown_start = window.setInterval(function () {
        bready_time--;
        if (bready_time == 0) {
            var varURL = varServiceServicesURL + "/Other/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_doshake_status_StartAction";
            varURL += "";

            $.ajax({
                type: "get",
                url: varURL,
                data: {
                    CountHowMany: varCountHowMany,
                    LongShakeTime: varLongShakeTime,
                    ShopClientID: varShopClientID,
                    MaxTracks: varMaxTracks,
                    SceenXianChangHuoDongNumber: varSceenXianChangHuoDongNumber,
                    action: "start"
                },
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonpCallBack201604230729", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
                success: function (json) {
                    resultErrorCode = json.ErrorCode;
                    if (resultErrorCode == 2) {
                        acutdown_start.html("GO!")
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
        } else {
            if (bready_time < 0) {////准备时间  结束  5S 到达了
                //debugger;
                window.clearInterval(tmr_cutdown_start);
                acutdown_start.hide();
                gameTimeRun();
                showSlogan();///口号; 广告语; 标语

            } else {

                audio_CutdownPlayer.play();
                acutdown_start.html(bready_time)
            }
        }
    },
    1000)
};

var tmr_GameDataLoad;
var gameTimeRun = function () {////摇一摇页面排序
    var varURL = varServiceServicesURL + "/Other" + "/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_doshake_sort";
    varURL += "";

    $.ajax({
        type: "get",
        url: varURL,
        data: {
            ShopClientID: varShopClientID,
            SceenXianChangHuoDongNumber: varSceenXianChangHuoDongNumber
        },
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonpCallBack201604230907", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        success: function (json) {
            resultErrorCode = json.ErrorCode;
            if (resultErrorCode == 0) {
                if ($.isArray(json.players)) {
                    /// SHAKE_LINE默认赛道数量
                    //debugger;
                    //PlayersShuzuList = rankTopTenList = json.players;

                    //rankTopTenList = json.players.slice(0, json.players.length);///返回一个新的数组，包含从 start 到 end （不包括该元素）的 arrayObject 中的元素。
                    PlayersShuzuList = json.players.slice(0, json.players.length);///返回一个新的数组，包含从 start 到 end （不包括该元素）的 arrayObject 中的元素。


                    //rankTopTenList = json.players.slice(0, SHAKE_LINE);///返回一个新的数组，包含从 start 到 end （不包括该元素）的 arrayObject 中的元素。
                    //var a = 0;//rankTopTenList.length;////取20个数组
                    //debugger;
                    //while (a++) {
                    //    var cidNum = rankTopTenList[a]["idNum"];
                    //    if (!PlayersShuzuList[cidNum]) {
                    //        PlayersShuzuList[cidNum] = {
                    //            data: rankTopTenList[a]
                    //        }
                    //    }
                    //}
                }
                gameTick();
                tmr_GameDataLoad = window.setTimeout(gameTimeRun, tick); // tick=1000   /1s/   ---gameTimeRun WService.asmx / doshake_sort
            }
            else if ((resultErrorCode == 2) || (resultErrorCode == 3)) {////超时聊  超过 60秒聊  或者 有人 摇动聊 600次
                console.log("超时时间 已到");//，加载时控制台就会自动显示如下内容。
                var e = $(".tracklist").width() - size_Height_1_20 + diff;
                for (var b = 0; b < RanksPositionList.length; b++) {
                    RanksPositionList[b] = e
                }
                setTopLeftAll20();
                window.setTimeout(function () {
                    if ((json.players != undefined) && ($.isArray(json.players))) {
                        PlayersShuzuList = json.players.slice(0, 3);///返回一个新的数组，包含从 start 到 end （不包括该元素）的 arrayObject 中的元素。
                    }
                    showGameResult();
                    hideSlogan();
                },
                660)
            } else {
                zAlert.Alert({
                    content: "无法获得游戏数据，与游戏服务器断开，请刷新重试！",
                    callback: function () {
                        zAlert.Close()
                    }
                })
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("通讯错误，请检查网络 ");
        }
    });


};

var roundTime, roundLength;
var nextRound = function () {///再玩一次
    join_status = 0; //0还没有倒计时开始  1表示 开始聊
    window.location.reload();




    //$.getJSON(PATH_ACTIVITY + "/shake_status?callback=", {
    //    SceenXianChangHuoDongNumber: varSceenXianChangHuoDongNumber,
    //    action: "restart"
    //},
    //function (a) {
    //    debugger;
    //    if (a.ret != 0) {
    //        //alert("shake_status");
    //        alert(res.msg)
    //    } else {
    //        join_status = 0; //0还没有倒计时开始  1表示 开始聊
    //        window.location.reload()
    //    }
    //}).fail(function () {
    //    alert("shake_status?callback fail(function () ")
    //})
};



function createTrack() {
    //debugger;
    var b = "";
    ///默认赛道数量
    for (var a = 0; a < SHAKE_LINE; a++) {
        b += '<div class="trackline"><div class="track-start">' + (a + 1) + '</div><div class="track-end"></div></div>'
    }
    $(b).appendTo(".Track .tracklist").hide().each(function (c) {
        var d = $(this);
        window.setTimeout(function () {
            d.show().addClass("leftfadein")
        },
        100 * c)
    })
}

function showScore(b) {
    var a = "/activity/shake/winner_list?SceenXianChangHuoDongNumber=" + varSceenXianChangHuoDongNumber;
    if (b != undefined) {
        a += "&rotate_id=" + b
    }
    $.showPage(a)
}
var tmr_slogan;///口号; 广告语; 标语


/*标语，口号; 呐喊声; （商业广告上用的） 短语;
[网络]	口号; 广告语; 标语;
[例句]They could campaign on the slogan 'We'll take less of your money'.
他们可能打出这样的竞选口号：“我们会让您少出钱”。*/
function showSlogan() {
    $(".Panel.Top").css({
        top: "-" + $(".Panel.Top").height() + "px"
    });
    $(".Panel.Bottom").css({
        bottom: "-" + $(".Panel.Bottom").height() + "px"
    });
    var c = ($.isArray(SHAKE_INFO.slogan_list) && SHAKE_INFO.slogan_list.length > 0) ? SHAKE_INFO.slogan_list : SLOGANS;
    var a = c.length;
    var b = $(".Panel.SloganList").css({
        top: "-15%"
    }).show();
    b.css({
        top: 0,
        "line-height": b.height() + "px"
    });
    tmr_slogan = window.setInterval(function () {
        b.html(c[Math.floor(Math.random() * a)])
    },
    1000)
}
function hideSlogan() {
    window.clearInterval(tmr_slogan);
    $(".Panel.SloganList").hide();
    $(".Panel.Top").css({
        top: 0
    });
    $(".Panel.Bottom").css({
        bottom: 0
    })
};