<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfflineShop.aspx.cs" Inherits="_11WA_ClientShop.OfflineShop" %>

<html class="no-js " lang="zh-CN">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="keywords" content="微云基石,移动电商服务平台">
    <meta name="HandheldFriendly" content="True">
    <meta name="MobileOptimized" content="320">
    <meta name="format-detection" content="telephone=no">
    <meta http-equiv="cleartype" content="on">
    <title>线下门店<%=pub_GetNickName%></title>
    <script src="/Scripts/jquery-1.8.2.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" href="/Templet/01WYJS/Css/o2o_base_929451b805.css?version=css201709121928" onerror="_cdnFallback(this)">
    <link rel="stylesheet" href="/Templet/01WYJS/Css/o2o_offline_shop_2f8ff40332.css?version=css201709121928" onerror="_cdnFallback(this)">
    <link href="/Templet/01WYJS/Css/mall.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common-v4.css?version=css201709121928">
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
    </script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
</head>

<body class="body-gray">
    <div class="container ">
        <%=protected_stro2oHead%>
        <%=protected_stro2oShopInfo%>
        <!-- <textarea id="footer-delay" style="display:none;"> -->
        <!-- </textarea> -->
    </div>

    <%--   <script src="./线下门店_files/common_452e0e827f.js" onerror="_cdnFallback(this)"></script>
    <script src="./线下门店_files/base_3e0bc67bba.js" onerror="_cdnFallback(this)"></script>

    <script src="./线下门店_files/u_b.js" onerror="_cdnFallback(this)"></script>
    <script src="./线下门店_files/offline_shop_85638ed91c.js" onerror="_cdnFallback(this)"></script>--%>
    <%-- <script>
        window.addEventListener('load', function () {
            var _hmt = _hmt || [];
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?96801ca9ab090e6db01b2b8377a4e979";
            hm.async = true;
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        });
</script>--%>
    <%=_Pub_03Footer_html%>
    <%=pub_WeiXin__o2o_FootMarker_Location___%>
</body>
</html>
