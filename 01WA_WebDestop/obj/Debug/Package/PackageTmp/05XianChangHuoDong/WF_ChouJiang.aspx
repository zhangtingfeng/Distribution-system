<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WF_ChouJiang.aspx.cs" Inherits="_01WA_WebDestop._05XianChangHuoDong.WF_ChouJiang" %>

<!DOCTYPE html>
<html class=" js flexbox flexboxlegacy canvas canvastext webgl no-touch geolocation postmessage websqldatabase indexeddb hashchange history draganddrop websockets rgba hsla multiplebgs backgroundsize borderimage borderradius boxshadow textshadow opacity cssanimations csscolumns cssgradients cssreflections csstransforms csstransforms3d csstransitions fontface generatedcontent video audio localstorage sessionstorage webworkers applicationcache svg inlinesvg smil svgclippaths">
<html>
<head runat="server">
    <meta charset="utf-8">
    <title>抽奖 - 微现场</title>
    <link href="./Css/base.css"
        rel="stylesheet" type="text/css">
    <link href="./Css/zAlert.css" rel="stylesheet"
        type="text/css">
    <link href="./Css/screen_lottery_free.css" rel="stylesheet"
        type="text/css">
    <script type="text/javascript">
        var SCENE_INFO = { "title": "微现场体验", "top_title": ["八十号仓库"], "top_font_size": "33", "memo": "", "top_img": "/05XianChangHuoDong/Images/ff07e678b4a02c5f5718d31fd7c687d9_260_151.png", "bg_img": "/05XianChangHuoDong/Images/4.jpg", "bottom_img": "/05XianChangHuoDong/Images/4d1de04c5b1c79ccd999891d1d761a11_350_175.gif", "diy_css": "123", "keyword": "微现场", "mp_username": "微云基石", "func": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" }, "navbar": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" } };
    </script>
    <script src="./js/modernizr.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="./js/zAlert.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var varShopClientID = '<%=strShopClientID%>'; var var_ShopClient_XianChangHuoDongID = '<%=strXianChangHuoDongID%>'; var varSceenXianChangHuoDongNumber = '<%=strXianChangHuoDongNumberbyShopClientID%>'; var varServiceServicesURL = ""; 
        varServiceServicesURL = '<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>';  // document.getElementById("ServiceServicesURLID").value;
       
        var varBonusNumberByShopClientID = 0;///本次 抽奖的 编号
        //var PATH_ACTIVITY = '/data/scene/';
        var SCENE_INFO = { "title": "微现场体验", "top_title": ["八十号仓库"], "top_font_size": "33", "memo": "", "top_img": "/05XianChangHuoDong/Images/ff07e678b4a02c5f5718d31fd7c687d9_260_151.png", "bg_img": "http:\/\/www.bama555.com\/assets\/img\/scene\/4.jpg", "bottom_img": "/05XianChangHuoDong/Images/4d1de04c5b1c79ccd999891d1d761a11_350_175.gif", "diy_css": "123", "keyword": "微现场", "mp_username": "微云基石", "func": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" }, "navbar": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" } };
        var join_status = '1';
        var varBonusNumberByShopClientID = 0;/////代替scene_id  作为 本次抽奖的编号

        // var join_status = '0';
    </script>
    <script src="./js/baseWF_ChouJiang.js" type="text/javascript" charset="utf-8"></script>
    <script src="./js/screen_lottery_free.js" type="text/javascript" charset="utf-8"></script>
</head>
<body class="FUN LOTTERY_FREE" style='background-image: url("<%=strBackground_PIC_BigScreen%>");'>
    <form id="form1" runat="server">
        <audio id="Audio_Running" src="./Mp3/lottery_running.mp3"
            preload="preload" loop="loop">
        </audio>
        <audio id="Audio_Result" src="./Mp3/lottery_getone.mp3"
            preload="preload">
        </audio>
        <div class="Panel Top">
            <asp:Image ID="activity_logo" class="activity_logo" runat="server" />

            <%--<img class="activity_logo" src="./Images/ff07e678b4a02c5f5718d31fd7c687d9_260_151.png">--%>
            <div class="top_title" style="font-size: 33px;">
                <div>
                    <asp:Literal ID="Literal_ShopClientName" runat="server"></asp:Literal>
                </div>
            </div>
            <asp:Image ID="Image1_ErWeiMa" class="mp_account_codeimage" runat="server" />

            <%--<img class="mp_account_codeimage" src="./Images/qrcode_wxsyb88_1.jpg">--%>
        </div>
        <div class="Panel Lottery">
            <div class="lottery-left">
                <div class="lottery-title">
                    <span class="title-label">抽奖</span><span class="userNumber-label"></span><span class="usercount-label">
                    ...
                    ...
                    </span>
                </div>
                <div class="lottery-run">
                    <div class="user"><span class="nick-name"></span></div>
                    <div class="control button-run">抽奖</div>
                    <div class="control button-stop">停止</div>
                    <!--<div class="control button-nextround">下一轮</div> -->
                </div>
                <div class="lottery-bottom">
                    <div class="round-num">

                        <div class="select-panel">
                            选取奖项级别：
                            <div class="select-panel_LevelList">
                                <asp:DropDownList ID="DropDownList_LevelList" runat="server">
                                </asp:DropDownList>&nbsp&nbsp
                            </div>
                            选取人数：
                        <div class="select-button minus">-</div>
                            <div class="select-value">1</div>
                            <div class="select-button plus">+</div>
                        </div>

                    </div>
                    <div class="button-reset">重新抽奖</div>
                    <div class="button-showresult">显示结果</div>
                    <!-- <div class="button-reload">重新报名</div>
                <div class="button-save">保存结果</div> -->
                </div>
            </div>
            <div class="lottery-right">
                <div class="result-line">
                    <div class="result-num">1</div>
                    <!--
                <div class="user">
                    <span class="nick-name">张三</span>
                </div>
                -->
                </div>
            </div>
        </div>
        <div class="Panel Bottom">
            <asp:Image ID="Image_logo" class="support_logo" runat="server" />

            <%--<img class="support_logo" src="./Images/4d1de04c5b1c79ccd999891d1d761a11_350_175.gif">--%>
            <div class="helperpanel pulse">
                搜索关注<span class="mp_account"></span><br>
                发送<span class="activity_key">微现场</span>即可参与
         
            
            </div>
            <div class="navbar">
                <a class="navbaritem fullscreen" href="#">
                    <div class="icon"></div>
                    <div class="label">全屏</div>
                </a>

                <asp:HyperLink ID="HyperLink_ChouJiang" class="navbaritem lottery hover" runat="server"><div class="icon"></div>
                <div class="label">抽奖</div>
                </asp:HyperLink>

                <asp:HyperLink ID="HyperLinkWF_YaoYiYao" class="navbaritem rocker" runat="server"><div class="icon"></div>
                <div class="label">摇一摇</div>
                </asp:HyperLink>

                <%--            <a class="navbaritem rocker" href="http://wxsyb.bama555.com/activity/free_shake?id=62378">
                <div class="icon"></div>
                <div class="label">摇一摇</div>
            </a>--%>

                <%--<a class="navbaritem pairup" href="http://wxsyb.bama555.com/activity/free_pairup?id=62378">
                <div class="icon"></div>
                <div class="label">对对碰</div>
            </a><a class="navbaritem vote" href="http://wxsyb.bama555.com/activity/free_vote?id=62378">
                <div class="icon"></div>
                <div class="label">投票</div>
            </a><a class="navbaritem wall" href="http://wxsyb.bama555.com/activity/wall?id=62378">
                <div class="icon"></div>
                <div class="label">微信上墙</div>
            </a><a class="navbaritem answer" href="http://wxsyb.bama555.com/activity/free_answer?id=62378">
                <div class="icon"></div>
                <div class="label">抢答</div>
            </a>--%>
            </div>
        </div>
        <div class="loginform">
            <div class="activity_title">微现场体验</div>
            <div>
                <input class="password" id="password" type="password" placeholder="请输入微现场的管理密码" value="">
            </div>
            <div class="submitline">
                <button class="button-login">开启</button>
            </div>
        </div>
        <div class="loader">
            <div class="icon"></div>
        </div>
        <div class="join_user">
            <div class="label">微信扫一扫，发送<span class="activity_key">微现场</span>即可参与</div>
            <asp:Image ID="Image_codeImg" class="codeImg" runat="server" />

            <%--<img class="codeImg" src="./Images/qrcode_wxsyb88_1.jpg">--%>
            <div class="radar">
                <div class="join_num">已有<em>0</em>人加入</div>
                <div class="round w300">
                    <div class="round w200">
                        <div class="round w100">
                            <div class="round w1">
                                <div class="line"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="users"></div>
            </div>
            <%--<div class="join_user_btn">开始抽奖</div>--%>
        </div>
    </form>
</body>
</html>
