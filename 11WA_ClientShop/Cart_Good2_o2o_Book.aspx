<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart_Good2_o2o_Book.aspx.cs" Inherits="_11WA_ClientShop.Cart_Good2_o2o_Book" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>取货凭证 转发无效</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta http-equiv="x-dns-prefetch-control" content="on">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <script src="/Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>

    <style type="text/css">
        .Image_MarkerErWeiMa {
            top: 0px;
            padding-bottom: 50px;
            width: 70%; 
            z-index: 100;
            display: block;
            max-width: 640px;
            margin:0px auto;
        }
    </style>


    <script language="JavaScript" type="text/javascript">

        $(document).ready(function () {
            var iID = setTimeout("reflesh()", 4000); //单位毫秒或者var iID=setTimeout(clock,2000);
        });

        function reflesh() {
            var varorderid = "<%=_Pub_Orderid_%>";

            var urlData = "orderid=" + varorderid + "";

            $.ajax({
                type: "POST",
                url: "/Handler/Cart_Good2_o2o_Book.ashx",
                data: urlData,
                success: function (msg) {
                    if (msg == "1") {
                        alert("二维码已被扫描，货物已被送出！");
                        window.location.href = '<%=Pub_Agent_Path%>/cart_good3.aspx';
                    }
                }
            });
            var iID = setTimeout("reflesh()", 4000); //单位毫秒或者var iID=setTimeout(clock,2000);

        };
    </script>

    <%=pub_WeiXin__o2o_FootMarker_Location___%>
</head>
<body>
    <!--mask begin-->
    <div class="mask"></div>
    <!--mask end-->

    <div style="clear: both;"></div>
    <div class="user_itlist_nb">
        <div class="title">
            <h2>取货凭证</h2>
        </div>
    </div>

    <asp:Image ID="Image_MarkerErWeiMa" CssClass="Image_MarkerErWeiMa" runat="server" />

    <div class="h30" id="h30" style=""></div>


    <%=_Pub_03Footer_html%>
</body>
</html>
