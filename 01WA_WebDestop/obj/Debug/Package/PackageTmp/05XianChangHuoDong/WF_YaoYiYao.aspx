<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WF_YaoYiYao.aspx.cs" Inherits="_01WA_WebDestop._05XianChangHuoDong.WF_YaoYiYao" %>

<!DOCTYPE html>
<html class=" js flexbox flexboxlegacy canvas canvastext webgl no-touch geolocation postmessage websqldatabase indexeddb hashchange history draganddrop websockets rgba hsla multiplebgs backgroundsize borderimage borderradius boxshadow textshadow opacity cssanimations csscolumns cssgradients cssreflections csstransforms csstransforms3d csstransitions fontface generatedcontent video audio localstorage sessionstorage webworkers applicationcache svg inlinesvg smil svgclippaths">
<head runat="server">
    <meta charset="utf-8">
    <title>微云基石 摇一摇 - 微现场</title>
    <link rel="stylesheet" type="text/css" href="./Css/base.css">
    <link rel="stylesheet" type="text/css" href="./Css/zAlert.css">
    <link rel="stylesheet" type="text/css" href="./Css/screen_shake.css">
    <script type="text/javascript" src="./Js/modernizr.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.8.2.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="./Js/zAlert.js" charset="utf-8"></script>
    <script type="text/javascript">
        var varShopClientID = <%=strShopClientID%>; var varSceenXianChangHuoDongNumber = <%=strXianChangHuoDongNumberbyShopClientID%>; var varServiceServicesURL = ""; var varLongShakeTime = <%=intLongShakeTime%>; var varCountHowMany = <%=intCountHowMany%>;var varMaxTracks = <%=intMaxTracks%>;
        varServiceServicesURL = '<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>';  // document.getElementById("ServiceServicesURLID").value;
      
        //var PATH_ACTIVITY = '/data/scene/';
        var SCENE_INFO = { "title": "代理平台微现场体验", "top_title": ["微云基石微现场演示"], "top_font_size": "35", "memo": "", "top_img": "./Images/db7fb69362e09de1241f76ab24a0f734_258_258.jpg", "bg_img": "./Images/2.jpg", "bottom_img": "./Images/3c95a5df3dceb9334c4e522c483634de_640_1181.jpg", "diy_css": "微云基石", "keyword": "微现场", "mp_username": "微云基石", "func": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" }, "navbar": { "wall": "1", "lottery": "1", "shake": "1", "pairup": "1", "vote": "1", "answer": "1" } };//
        var SHAKE_INFO = { "id": "26715", "scene_id": "62378", "title": "你中奖了！", "shake_line": "10", "slogan": "再大力！|再大力，再大力!|摇，大力摇！", "voice": "4", "ready_time": "5", "countdown": "30", "filter_parter": "0", "filter_cparter": "0", "rank_link": "{\"1\":\"\",\"2\":\"\",\"3\":\"\",\"4\":\"\",\"5\":\"\",\"6\":\"\",\"7\":\"\",\"8\":\"\",\"9\":\"\",\"10\":\"\",\"11\":\"\",\"12\":\"\"}", "dt_add": "1442455245", "status": "0", "is_free": "0", "dt_free": "0", "slogan_list": ["再大力！|再大力，再大力!|摇，大力摇！"] };//
        /*当前轮次序号*/
        var READY_TIME = 5;//默认准备时间
        var CUTDOWN_TIME = SHAKE_INFO['countdown'] * 1;//默认倒计时
        var SHAKE_LINE = varMaxTracks;//SHAKE_INFO['shake_line'] * 1;//默认赛道数量
        var CURR_ROUND_ID = 0;//标记Free轮次
        var SLOGANS = [
            '再大力！',
            '再大力,再大力！',
            '再大力,再大力,再大力！',
            '摇，大力摇',
            '快点摇啊，别停！',
            '摇啊，摇啊，摇啊',
            '小心手机，别飞出去伤到花花草草',
            '看灰机～～～'
        ];
        var join_status = '0';/// //0还没有倒计时开始  1表示 开始聊


        //DocumentLoad();
       
    </script>
    <script type="text/javascript" src="./JS/base.js" charset="utf-8"></script>
    <script type="text/javascript" src="./Js/screen_shake_free.js" charset="utf-8"></script>
</head>
<body class="FUN SHAKE_FREE" style="background-image: url(<%=strBackground_PIC_BigScreen%>);">
    <audio id="Audio_CutdownPlayer" src="./Mp3/shake_cutdown.mp3" preload="preload"></audio>
    <audio id="Audio_NewPlayer" src="./Mp3/shake_new.mp3" preload="preload"></audio>
    <audio id="Audio_Outride" src="./Mp3/shake_outride.mp3" preload="preload"></audio>
    <audio id="Audio_Gameover" src="./Mp3/shake_gameover.mp3" preload="preload"></audio>
    <audio id="Audio_GameBackground" src="<%=strBackground_SoundPath%>" preload="preload"></audio>

    <!-- 开始界面 -->
    <div class="join_user">
        <div class="label">
            <asp:Literal ID="Literal_ShopClientName" runat="server"></asp:Literal>微信扫一扫，发送<span class="activity_key">微现场</span>即可参与
        </div>
        <%--<img class="codeImg" src="./Images/saved_resource.jpg">--%>
        <asp:Image ID="Image_codeImg" class="codeImg" runat="server" />
        <div class="codeImgText">
            <asp:Literal ID="Literal_Agent" runat="server"></asp:Literal>
        </div>
        <div class="radar">
            <div class="join_num"><span id="XianChangHuoDongNum"></span>已有<em>0</em>人加入</div>
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
        <div class="game-start">开始倒计时</div>
        <!-- <div class="join_user_btn">开始报名</div> -->
    </div>
    <!-- 头部 -->
    <div class="Panel Top">
        <asp:Image ID="activity_logo" class="activity_logo" runat="server" />
        <%--<img class="activity_logo" src="./Images/db7fb69362e09de1241f76ab24a0f734_258_258.jpg">--%>
        <div class="top_title" style="font-size: 35px">
            <div>微云基石微现场演示</div>
        </div>
        <asp:Image ID="Image1_ErWeiMa" class="mp_account_codeimage" runat="server" />
        <%--<img class="mp_account_codeimage" src="./Images/saved_resource.jpg">--%>
    </div>

    <!-- 开始游戏头部 -->
    <div class="Panel SloganList"></div>
    <!-- 开始游戏 -->
    <div class="Panel Track">
        <div class="tracklist"></div>
        <div class="cutdown-end"></div>
        <div class="track-tool"></div>
        <div class="track-result"></div>
    </div>
    <!-- 游戏结束 -->
    <div class="result-layer">
        <div class="result-label">GAME OVER</div>
        <div class="result-cup">
            <!--<span class="button nexttound">开始下一轮</span>-->
            <!--<span class="button allresult">全部排名</span>-->
            <!--  <span class="button restart">重新报名</span>
            <span class="button save">保存结果</span> -->
            <span class="button result">查看结果</span>
            <span class="button reset">再玩一次</span>
        </div>
    </div>
    <!-- 底部 -->
    <div class="Panel Bottom">
        <asp:Image ID="Image_logo" class="support_logo" runat="server" />
        <%--<img class="support_logo" src="./Images/3c95a5df3dceb9334c4e522c483634de_640_1181.jpg">--%>
        <div class="helperpanel pulse">
            搜索关注<span class="mp_account"><asp:Literal ID="LiteralWeiXinHao" runat="server"></asp:Literal></span><br>
            发送<span class="activity_key">微现场</span>即可参与
       
        </div>
        <div class="navbar">
            <a class="navbaritem fullscreen" href="#">
                <div class="icon"></div>
                <div class="label">全屏</div>
            </a>
            <asp:HyperLink ID="HyperLink_ChouJiang" class="navbaritem lottery" runat="server"><div class="icon"></div>
                <div class="label">抽奖</div>
            </asp:HyperLink>
           <%-- <a class="navbaritem lottery" href="./WF_ChouJiang.aspx">
                <div class="icon"></div>
                <div class="label">抽奖</div>
            </a>--%>
            <asp:HyperLink ID="HyperLinkWF_YaoYiYao" class="navbaritem rocker hover" runat="server"><div class="icon"></div>
                <div class="label">摇一摇</div>
            </asp:HyperLink>
            <%--  <a class="navbaritem rocker hover" href="./WF_YaoYiYao.aspx">
                <div class="icon"></div>
                <div class="label">摇一摇</div>
            </a>--%>
            <a style="display: none;" class="navbaritem pairup" href="/activity/free_pairup?id=62378">
                <div class="icon"></div>
                <div class="label">对对碰</div>
            </a>
            <a style="display: none;" class="navbaritem vote" href="/activity/free_vote?id=62378">
                <div class="icon"></div>
                <div class="label">投票</div>
            </a>
            <a style="display: none;" class="navbaritem wall" href="/activity/wall?id=62378">
                <div class="icon"></div>
                <div class="label">微信上墙</div>
            </a>
            <a style="display: none;" class="navbaritem answer" href="/activity/free_answer?id=62378">
                <div class="icon"></div>
                <div class="label">抢答</div>
            </a>
        </div>
    </div>
    <!-- 微现场管理密码登录 -->
    <div class="loginform" style="display: block;">
        <div class="activity_title">微云基石微现场体验</div>
        <div>
            <input id="password" class="password" placeholder="请输入微现场的管理密码" type="password">
        </div>
        <div class="submitline">
            <button class="button-login">开启</button>
        </div>
    </div>
    <!-- 加载logding -->
    <div class="loader" style="display: none;">
        <div class="icon"></div>
    </div>
    <!--round welcome +++++++++++++++++++++++++++++++++++++++++-->
    <!-- <div class="round-welcome"> -->
    <!-- <div class="label top">微信扫一扫，发送<span class="activity_key">微现场</span>然后点击“摇一摇”</div> -->
    <!-- <img src="https://open.weixin.qq.com/qr/code/?username=wxsyb88">
        <div class="label bottom"><span class="shake-icon shake"></span>听从现场指挥，游戏开始后不停摇动手机</div>
        <div class="button-start">开始游戏</div>
        <div class="button restart">重新报名</div>
    </div> -->
    <!--round welcome +++++++++++++++++++++++++++++++++++++++++-->
    <!-- 开始游戏倒计时 -->
    <div class="cutdown-start cutdownan-imation"></div>

</body>
</html>
