<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scratchcard.aspx.cs" Inherits="_11WA_ClientShop.Huodong.lottery.Scratchcard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <link rel="stylesheet" href="/Huodong/resources/lottery/css/activity-style.css" type="text/css" media="screen,print" />
    <meta charset="UTF-8">
    <meta name="viewport"
        content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <title>刮刮乐</title>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
    <script type="text/javascript" src="/Huodong/resources/jquery/jquery-2.0.3.min.js"></script>
    <script type="text/javascript" src="/Huodong/resources/lottery/js/wScratchPad.js"></script>
   <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <%--<script src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928" type="text/javascript"></script>--%>
    <script type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
</head>
<body data-role="page" class="activity-scratch-card-winning">

    <div class="main">
        <div class="cover">
            <img src="/Huodong/resources/lottery/images/activity-scratch-card-bannerbg.png">
            <div id="prize"></div>
            <div id="scratchpad">
            </div>
        </div>
        <div class="content">
            <div class="boxcontent boxwhite">
                <div class="box">
                    <div class="title-brown">
                        <span>奖项设置：
                        </span>
                    </div>
                    <div class="Detail">
                        <asp:Localize ID="Localize_ShowBonus" runat="server"></asp:Localize>
                        <p style="color: red">
                            如刮到奖品请截图或直接展示给我们的工作人员,否则视为无效。
							<br />
                            本次活动抽奖分为 刮刮卡 及 幸运大转盘, 您只可以选择其中的一个参与，谢谢。
                        </p>
                    </div>
                </div>
            </div>
            <div class="boxcontent boxwhite">
                <div class="box">
                    <div class="title-brown">
                        活动说明：
                    </div>
                    <div class="Detail">
                        <p class="red">
                            本次活动由微云基石提供技术支持 举办，如果你刮到奖品，请截图或直接到我们的展台出示给我们的工作人员，方便对奖。
								<br />
                            本次活动抽奖分为 刮刮卡 及 幸运大转盘 你可以选择其中的一个参与，谢谢。
								<br />
                            注：一人只有一次机会
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <script type="text/javascript">
        $("#scratchpad").wScratchPad({
            width: 200, 				// set width - best to match image width
            height: 60,
            color: 'grey', //覆盖的刮刮层的颜色  
            image: '/Huodong/resources/lottery/images/<%=strPubJPGShow%>', //刮奖结果图片  
            cursor: '/Huodong/resources/lottery/images/mario.png',
            scratchMove: function (e, percent) {
                if (percent > 70)
                    this.clear();
                $(this.canvas).css('margin-right', $(this.canvas).css('margin-right') == "0px" ? "1px" : "0px");
            },
            scratchDown: function (e, percent) {
                $(this.canvas).css('margin-right', $(this.canvas).css('margin-right') == "0px" ? "1px" : "0px");
            },
            scratchUp: function (e, percent) {
                $(this.canvas).css('margin-right', $(this.canvas).css('margin-right') == "0px" ? "1px" : "0px");
            }
        });
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
