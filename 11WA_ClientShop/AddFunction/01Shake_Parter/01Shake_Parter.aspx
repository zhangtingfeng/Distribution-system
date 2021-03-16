<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01Shake_Parter.aspx.cs" Inherits="_11WA_ClientShop.AddFunction._01Shake_Parter._01Shake_Parter" %>

<!DOCTYPE>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="/AddFunction/01Shake_Parter/css/shake.css" type="text/css">
    <script src="/Templet/01WYJS/js/layer.m.js?version=js201709121928"></script>
    <link href="/Templet/01WYJS/Css/layer.css?version=css201709121928" rel="stylesheet" />
    <meta http-equiv="x-rim-auto-match" content="none" />
    <title>摇一摇</title>
    <script src="/Scripts/jquery-1.8.2.min.js"></script>
    <style>
        #shakeValue {
            color: gray;
            font-size: smaller;
            position: absolute;
            top: 10px;
            left: 20px;
        }

        .headimgurl {
            text-align: center;
            margin-bottom: 20px;
        }

            .headimgurl img {
                border-radius: 50%;
            }

        .rank {
            width: 100%;
            margin: 10px auto;
            text-align: center;
            display: none;
        }

            .rank a {
                color: #ffffff;
            }
    </style>

    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给    传递变量\n";
        var varUserIDHeadURL = "<%=Pub_Get_MyDisk_HeadImage%>";///传递给    传递变量\n";
        var varUserIDWeiXinHeadURL = "<%=Pub_Get_MyWeiXin_HeadImage%>";///传递给    传递变量\n";
        var varUserIDNickName = "<%=pub_varUserIDNickName%>";///传递给    传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给    传递变量
        //var varShopClientIDWeiXianChangNum = "</%=ShopClientIDWeiXianChangNum%>";///手机端获取 当前 可以 进行的活动的编号   前端
       <%-- var varServicesURL_HelpMachine = "<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL_HelpMachine()%>";///传递给 product.js   传递变量--%>

        var varpub_varUserHostAddress = "<%=pub_varUserHostAddress%>";
        //var varpub_varREMOTE_ADDRip = "<0%=pub_varREMOTE_ADDRip%>";
     
        var varpub_NeedAlertShallSubscribe = <%=pub_NeedAlertShallSubscribe%>;
        var varpub_NeedAlertShallAddress = <%=pub_NeedAlertShallAddress%>;
        var varpub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = "<%=pub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_%>";

        
    </script>
    <asp:Literal ID="Literal_WeiXinShare" runat="server"></asp:Literal>


    <script type="text/javascript">
        // DeviceOrientation将底层的方向传感器和运动传感器进行了高级封装，提供了DOM事件的支持。
        // 这个特性包括两个事件：
        // 1、deviceOrientation：封装了方向传感器数据的事件，可以获取手机静止状态下的方向数据（手机所处的角度、方位和朝向等）。
        // 2、deviceMotion：封装了运动传感器的事件，可以获取手机运动状态下的运动加速度等数据。
        // 使用这两个事件，可以很能够实现重力感应、指南针等有趣的功能。

        // 现在在很多Native应用中有一个非常常见而时尚的功能 —— 摇一摇，摇一摇找人、摇一摇看新闻、摇一摇找金币。。。
        // 也许在android或者ios的客户端上对这个功能你已经很了解了，但是现在，我将告诉你如何在手机网页上实现摇一摇的功能。

        // 先来让我们了解一下设备运动事件 —— DeviceMotionEvent:返回设备关于加速度和旋转的相关信息，其中加速度的数据包含以下三个方向：
        // x：横向贯穿手机屏幕；
        // y：纵向贯穿手机屏幕；
        // z：垂直手机屏幕。
        // 鉴于有些设备没有排除重力的影响，所以该事件会返回两个属性：
        // 1、accelerationIncludingGravity(含重力的加速度)
        // 2、acceleration(排除重力影响的加速度)

        ///onload="init()"
        // 首先在页面上要监听运动传感事件
        function init() {
            debugger;
            if (varpub_NeedAlertShallSubscribe==1)
            {
                layer.open({
                    type: 2,
                    content: "你必须先关注才能参加摇一摇获奖活动",
                    time: 4,
                    end: function (layer) {
                        //debugger;
                        //history.go(0);
                        //location.reload();
                        window.location.href=varpub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_; 
                        //document.URL = varpub_strGet_GuideSubscribePageFromWeiXinD_ShopClientID_;///Javascript刷新页面的几种方法：
                    }
                });
            }
            if (varpub_NeedAlertShallAddress==1)
            {
                layer.open({
                    type: 2,
                    content: "你必须有收获地址才能参加摇一摇获奖活动",
                    time: 4,
                    end: function (layer) {
                        window.location.href = "/cart_self.aspx";///Javascript刷新页面的几种方法：
                    }
                });
            }


            CheckHuoDongStatus();
        }


        ///检查摇动活动 是不是 开始聊  不断的检测状态   手机 前段的 检查  
        function CheckHuoDongStatus() {
            varURL = varServiceURL+"/Other" + "/01XianChangHuoDong/01XianChangHuoDong.asmx/doGetStatue_XianChangHuoDongAction?strUseid=" + varUserID + "&strShopClientID=" + varShopClientID + "&UserIDHeadURL=" + encodeURI(encodeURI(varUserIDHeadURL)) + "&UserIDWeiXinHeadURL=" + encodeURI(encodeURI(varUserIDWeiXinHeadURL)) + "&UserIDNickName=" + encodeURI(encodeURI(varUserIDNickName));
            varURL += "&pub_varUserHostAddress=" + varpub_varUserHostAddress;
          
            var result = -1;
            $.ajax({
                type: "get",
                url: varURL,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonpCallBack201604102044", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
                success: function (json) {
                    result = json.ErrorCode;
                    if (result == 0) {////检测开始状态 。提示 用户 。快点开始 摇晃手机吧  当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束
                        if (json.XianChangHuoDongStatus == 2) {
                            layer.open({
                                type: 2,
                                content: "编号为活动" + json.XianChangHuoDongNum + "已经开始，请摇动手机抢大奖，加油啊",
                                time: 2
                            });
                            $("#shakeValue").html("编号" + json.XianChangHuoDongNum + "的开始,大力摇动");
                            init_YouShouldShakeNow(json.XianChangHuoDongNum);
                            setTimeout(SendinfoStatusToServer, 500);//循环 检查 要不要 发送数据
                        }
                        else if (json.XianChangHuoDongStatus == 1) {///看看 大屏幕 有你不
                            $("#shakeValue").html("编号" + json.XianChangHuoDongNum + "的活动尚未开始");
                            setTimeout(CheckHuoDongStatus, 500);
                        }
                    }
                    else if (result == -2) {
                        layer.open({
                            type: 2,
                            content: "暂无进行中的活动，请听从现场活动",
                            time: 10,
                            end: function (layer) {
                                //debugger;
                                history.go(0);
                                //location.reload();
                                //document.URL = location.href;///Javascript刷新页面的几种方法：
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
        }

        var varNumberISDoing = "";////正在摇动的编号

        function init_YouShouldShakeNow(jsonISDoingNumber) {///活动开始聊
            if (jsonISDoingNumber != '' && jsonISDoingNumber != undefined && jsonISDoingNumber != null) {
                varNumberISDoing = jsonISDoingNumber;//正在摇动的编号
            }
            Dong_plz_Listen_Doing = true;///已经得到  通知 。开始了
            ////开始检测 摇动事件
            if (window.DeviceMotionEvent) {
                // 移动浏览器支持运动传感事件
                window.addEventListener('devicemotion', deviceMotionHandler, false);
                $("#yaoyiyaoyes").show();
            } else {
                // 移动浏览器不支持运动传感事件
                layer.open({
                    type: 2,
                    content: "移动浏览器不支持运动传感事件,无法参与活动",
                    time: 10
                });
            }

        }


        // 那么，我们如何计算用户是否是在摇动手机呢？可以从以下几点进行考虑：
        // 1、其实用户在摇动手机的时候始终都是以一个方向为主进行摇动的；
        // 2、用户在摇动手机的时候在x、y、z三个方向都会有相应的想速度的变化；
        // 3、不能把用户正常的手机运动行为当做摇一摇（手机放在兜里，走路的时候也会有加速度的变化）。
        // 从以上三点考虑，针对三个方向上的加速度进行计算，间隔测量他们，考察他们在固定时间段里的变化率，而且需要确定一个阀值来触发摇一摇之后的操作。

        // 首先，定义一个摇动的阀值
        var SHAKE_THRESHOLD = 3000;
        var Dong_plz_Listen_Doing = false;//////请听从  现场  现在 没有。。true is    false  no
        //总长度
        var TotalShake = 0;///服务器端 返回的 摇的 总数
        //var varChangestate_Donging = false;///是否已经 启动页面轮换 他自己带有定时器

        var TotalScore = 0;///分数 是 本人的 TotalShake/最高的 TotalShake
        // 定义一个变量保存上次更新的时间
        var last_update = 0;
        // 紧接着定义x、y、z记录三个轴的数据以及上一次出发的时间
        var x;
        var y;
        var z;
        var last_x;
        var last_y;
        var last_z;
        var count = 0;
        var shakeNums = 0;//记录摇的次数,每摇两次算一次
        var shakeNumsBySend = 0;//记录摇的次数,每摇两次算一次

        var pageShakeShowNums = 0;///页面反馈的摇的次数，100毫秒 变换一下页面。   累加到10  调用结束


        function deviceMotionHandler(eventData) {
            // 获取含重力的加速度
            var acceleration = eventData.accelerationIncludingGravity;
            // 获取当前时间
            var curTime = new Date().getTime();
            var diffTime = curTime - last_update;
            // 固定时间段
            if (diffTime > 100) {
                last_update = curTime;

                x = acceleration.x;
                y = acceleration.y;
                z = acceleration.z; //1.5

                var speed = Math.abs(x - last_x + y - last_y + z - last_z) / diffTime * 10000;

                if (speed > SHAKE_THRESHOLD) {
                    // TODO:在此处可以实现摇一摇之后所要进行的数据逻辑操作steps
                    //　count++;
                    $("#yaoyiyaoresult").show();
                    if (Dong_plz_Listen_Doing) {//请听从  现场  现在 没有。。true is    false  no
                        shakeNums++;/// StatisScore();////摇动2次算一次 1最大
                        if (shakeNums = 1) {//摇动2次算一次 1最大
                            shakeNums = 0;
                            shakeNumsBySend++;
                        }
                    }
                    else {
                        layer.open({
                            type: 2,
                            content: "活动尚未开始,请听从现场活动",
                            time: 2
                        });
                    }
                    ///state1();
                }
                last_x = x;
                last_y = y;
                last_z = z;
            }
        }
        //循环 检查 要不要 发送数据
        function SendinfoStatusToServer() {
            if (Dong_plz_Listen_Doing) {
                var varSendURL = varServiceURL + "/Other" + "/01XianChangHuoDong/01XianChangHuoDong.asmx/doShakePhone_XianChangHuoDongAction?strUseid=" + varUserID + "&strShopClientID=" + varShopClientID + "&strvarNumberISDoing=" + varNumberISDoing;
                varSendURL += "&shakeNumsBySend=" + shakeNumsBySend;

                var result = -1;
                $.ajax({
                    type: "get",
                    url: varSendURL,
                    dataType: "jsonp",
                    jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                    jsonpCallback: "jsonpCallBack201604102044", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                    contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
                    success: function (json) {
                        ///当前现场 活动 是否开始  
                        //0表示 尚未开始 
                        //1表示现场区域开发，但是 摇奖尚未开始 
                        //2表示正在进行 
                        //3表示当前已结束
                        result = json.ErrorCode;
                        if (result == 2) {////检测开始状态 。
                            Dong_plz_Listen_Doing = true;///还可以继续摇动

                            if (shakeNumsBySend > 0) {////表示确实摇动了 。
                                pageShakeShowNums = 0;///执行10次后 会自动结束
                                Changestate1_Donging();///变换页面  用户正在摇 友好性较强  pageShakeShowNums 执行10次后 会自动结束
                                var fAudio_shake_SoundPlayer = document.getElementById("Audio_shake_SoundPlayer");
                                fAudio_shake_SoundPlayer.play();
                                TotalShake = json.TotalShake;
                                if (TotalShake > 0) {////如果 发送的是0  会返回 -1
                                    $("#shakeValue").html(varNumberISDoing + ":" + TotalShake);///显示给 用户 。摇的 总次数
                                }
                            }

                            setTimeout(SendinfoStatusToServer, 500);////循环 检查 要不要 发送数据
                        }
                        else if (result == 3) {
                            Dong_plz_Listen_Doing = false;///结束摇动
                            $("#shakeValue").html("编号为" + varNumberISDoing + "结束");///显示给 用户 
                            //varChangestate_Donging == false;///是否已经 启动页面轮换 他自己带有定时器
                            layer.open({
                                type: 2,
                                content: "编号为" + varNumberISDoing + "摇一摇已竞赛结束,请看大屏幕,听从现场抽奖活动,",
                                time: 2
                            });
                            CheckHuoDongStatus();///可以重新摇了
                            //alert(2);
                        }
                        else if (result == -1) {///系统出错聊，建议友情提示用户 结束聊
                            Dong_plz_Listen_Doing = false;///结束摇动
                            //varChangestate_Donging == false;///是否已经 启动页面轮换 他自己带有定时器
                            layer.open({
                                type: 2,
                                content: "活动已结束,请听从现场活动",
                                time: 2
                            });
                            CheckHuoDongStatus();///可以重新摇了
                            //alert(3);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest.status);
                        console.log(XMLHttpRequest.readyState);
                        console.log(textStatus);
                    }
                });
                shakeNumsBySend = 0;//////重置摇的次数
            }
        }



        function Changestate1_Donging() {
            $(".shakebgimg").removeClass("r2");
            $(".shakebgimg").addClass("r1");
            pageShakeShowNums++;//pageShakeShowNums 执行10次后 会自动结束
            if (pageShakeShowNums < 10) {///pageShakeShowNums 执行10次后 会自动结束
                if (Dong_plz_Listen_Doing) {
                    setTimeout(Changestate2_Donging, 100);
                }
            }
        }
        function Changestate2_Donging() {
            $(".shakebgimg").removeClass("r1");
            $(".shakebgimg").addClass("r2");
            //if (Dong_plz_Listen_Doing) {
            if (Dong_plz_Listen_Doing) {
                setTimeout(Changestate1_Donging, 100);
            }
            //}
        }




    </script>
</head>
<body onload="init()">
    <audio id="Audio_shake_SucessPlayer" src="http://helpmachine.o2o10000.cn/01XianChangHuoDong/shake_Sucess.mp3" preload="preload"></audio>
    <audio id="Audio_shake_SoundPlayer" src="http://helpmachine.o2o10000.cn/01XianChangHuoDong/shake_Sound.mp3" preload="preload"></audio>
    <div class="">
        <div id="yaoyiyaoyes" style="font-size: 15px; margin: 10px; line-height: 30px; display: none;">
        </div>
        <div id="loading_div"></div>
        <div id="yaoyiyaoresult" style="font-size: 10px; margin: 10px; line-height: 30px; display: none;"></div>


        <style>
            #imgAct {
                padding-left: 6px;
            }
        </style>
    </div>
    <div id="shakeValue">活动尚未开始</div>
    <div class="headimgurl">
        <asp:Image ID="Imagewx_qlogo_cn_mmopen" runat="server" Width="100" Height="100" />
    </div>
    <div id="demo" class="shakebgimg">
        <img src="/AddFunction/01Shake_Parter/images/shankImg.png" />
    </div>
</body>
</html>
