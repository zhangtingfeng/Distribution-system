<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wheel.aspx.cs" Inherits="_11WA_ClientShop.Huodong.lottery.Wheel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta name="viewport"
        content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="微云基石">
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>

    <title>幸运大抽奖</title>
    <link href="/Huodong/resources/lottery/css/activity-style.css"
        rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <script src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928" type="text/javascript"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
</head>

<body class="activity-lottery-winning">
    <div class="main">

        <div id="outercont">
            <div id="outer-cont" style="overflow: hidden;">
                <div id="outer">
                    <img
                        src="../resources/lottery/images/activity-lottery-1_meitu_1.png"
                        width="310px">
                </div>
            </div>
            <div id="inner-cont">
                <div id="inner">
                    <img
                        src="../resources/lottery/images/activity-lottery-2.png">
                </div>
            </div>
        </div>

        <div class="content">

            <div class="boxcontent boxyellow">
                <div class="box">
                    <div class="">
                        <span>奖项设置：</span>
                    </div>
                    <div class="Detail">
                        <asp:Localize ID="Localize_ShowBonus" runat="server"></asp:Localize>

                        <p style="color: red">
                            如抽到奖品请截图或直接展示给我们的工作人员,否则视为无效。
							<br />
                            本次活动抽奖分为 刮刮卡 及 幸运大转盘, 你只可以选择其中的一个参与，谢谢。
                        </p>
                    </div>
                </div>
            </div>
            <div class="boxcontent boxyellow">
                <div class="box">
                    <div>活动说明：</div>
                    <div class="Detail">
                        <p>
                            <font color="red">如果你抽到奖品，请截图或直接到我们的展台出示给我们的工作人员，方便对奖。
						<br />
                                注：一人一天只有一次机会
                            </font>
                        </p>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script type="text/javascript"
        src="../resources/jquery/jquery-2.0.3.min.js"></script>
    <script type="text/javascript"
        src="../resources/lottery/js/jQueryRotate.js"></script>
    <script type="text/javascript"
        src="../resources/lottery/js/jquery.easing.min.js"></script>
    <script type="text/javascript"
        src="../resources/lottery/js/alert.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#inner").click(function () {
                lottery();
            });
        });

        function lottery() {



            $("#outer").rotate({ //inner内部指针转动，outer，外部转盘转动
                duration: 5000, //转动时间 
                angle: 0, //开始角度 
                animateTo: <%=strPubAngle%>, //转动角度 
                easing: $.easing.easeOutSine, //动画扩展 
                callback: function () {
                    //if(p==1){
                    //	alert(msg+ ' 编码为：'+json.id+' \n请你截图到我们的展台出示给我们的工作人员');
                    //}else
                    alert("<%=strPubBonus%>");
                    /* var con = confirm(msg + '\n还要再来一次吗？');
	                if (con) {
	                lottery();
	                } else {
	                return false;
	                } */
                }
            });

        }
    </script>
    <style type="text/css">
        .ClassCenter {
            margin: 0px auto;
            height: 20px;
            width: 100%;
            text-align: center;
            display: block;
            font-size: x-small;
        }
    </style>
    <div class="ClassCenter">
        技术支持<a href="http://net.shanghaishiyi.com/default-14.aspx">时仪电子</a>
        <a href="https://000001shiyidianzi.eggsoft.cn">微云基石</a>
    </div>
    <%=_Pub_03Footer_html%>
</body>
</html>
