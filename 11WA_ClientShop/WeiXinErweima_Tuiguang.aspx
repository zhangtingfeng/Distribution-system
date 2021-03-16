<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinErweima_Tuiguang.aspx.cs" Inherits="_11WA_ClientShop.WeiXinErweima_Tuiguang" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>联系我们_客服</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta http-equiv="x-dns-prefetch-control" content="on">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <script src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928" type="text/javascript"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <style type="text/css">
        .Image_MarkerErWeiMa {
            top: 0px;
            padding-bottom: 50px;
            width: 100%;
            position: absolute;
            z-index: 100;
            display: block;
            max-width: 640px;
        }
    </style>
    <%=pub_WeiXin__o2o_FootMarker_Location___%>
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
    </script>
    <asp:Literal ID="Literal_WeiXinShare" runat="server"></asp:Literal>
</head>
<body>
    <!--mask begin-->
    <div class="mask"></div>
    <!--mask end-->

    <div style="clear: both;"></div>
    <div class="user_itlist_nb">
        <div class="title">
            <h2>暂无客服证书</h2>
            <h1><a href="<%=Pub_Agent_Path%>/edityourshopini.aspx">申请代理联系客服</a></h1>
        </div>
    </div>
    <asp:Image ID="Image_MarkerErWeiMa" CssClass="Image_MarkerErWeiMa" runat="server" />
    <div class="h30" id="h30" style=""></div>
    <%=_Pub_03Footer_html%>
</body>
</html>