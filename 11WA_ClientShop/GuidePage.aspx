<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidePage.aspx.cs" Inherits="_11WA_ClientShop.GuidePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0" />
    <title><%=pub_GetAgentShopName_From_Visit__%></title>
    <meta http-equiv="Content-type" content="text/html; charset=UTF-8" />
    <script type="text/javascript" src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928"></script>
    <!-- Mobile Devices Support @begin -->
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control" />
    <meta content="no-cache" http-equiv="pragma" />
    <meta content="0" http-equiv="expires" />
    <meta content="telephone=no, address=no" name="format-detection" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <!-- apple devices fullscreen -->
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <!-- Mobile Devices Support @end -->
    <link href="Templet/01WYJS/Css/guidepage.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        .themeStyle {
            background: #363B66;
        }
    </style>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <meta http-equiv="Content-type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
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

    <script type="text/javascript">
        function load_Multi() {
            doVisitThisPage(); doGuide_Content();
        }
        function doVisitThisPage() {
            var url = "<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>/Pub/doGuidePage.asmx/doGuidePageAction?strThisShowID=<%=pub_strMenuID%>&strpInt_QueryString_ParentID=<%=pInt_DB_ParentID%>&strpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>&strpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>";

            var result = -1;
            $.ajax({
                type: "POST",
                url: url,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonp12Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json; charset=utf-8",
                success: function (myjson) {
                    result = parseInt(myjson.ErrorCode);
                    return;
                },
                error: function () {
                }
            });
            return result;
        }
        function doGuide_Content() {
            $.ajax({
                type: 'GET',
                url: '/Handler/guidepage_Content.ashx',
                dataType: 'text',
                data: 'strintMenuID=<%=pub_strMenuID%>',
                success: function (msg) {
                    $("#contentShow").html(msg);
                },
                error: function (data) {
                }
            })
        }
    </script>
    <%=pub_WeiXin__o2o_FootMarker_Location___%>
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
   
    </script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
</head>
<body id="news" onload="load_Multi();">

    <div class="page-bizinfo">

        <div class="header" style="position: relative;">
            <h1 id="activity-name"><%=pub_strMenuName%></h1>
        </div>

        <div class="showpic"></div>
        <div class="text" id="content">
            <div id="contentShow">
            </div>
            <%=pub_strMenuListContent%>
            <div id="insert3"></div>
        </div>
    </div>
    <%=_Pub_03Footer_html%>
</body>
</html>
