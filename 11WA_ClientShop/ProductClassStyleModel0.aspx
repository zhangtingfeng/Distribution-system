<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductClassStyleModel0.aspx.cs" Inherits="_11WA_ClientShop.ProductClassStyleModel0" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title><%=pub_GetAgentShopName_From_Visit__%><%=_Pub_strGoodBody_Title %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta http-equiv="x-dns-prefetch-control" content="on">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <script async src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928" type="text/javascript"></script>
    <script async type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script async src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/func_Image.js?version=js201709121928"></script>
  
    <%=pub_WeiXin__o2o_FootMarker_Location___%>
    <script type="text/javascript">
        function load_Multi() {
            //AutoResizeThisImage();
            doDefault_SAgent_ProductGoodClass();
            doDefault_SAgent_ProductGood();
        }
        function doDefault_SAgent_ProductGoodClass() {
            $.ajax({
                type: 'GET',
                url: '/Handler/Default_SAgent_ProductGoodClass.ashx',
                dataType: 'text',
                data: '<%=_Pub_ProductGoodClass_%>&strpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>&strpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>',
                beforeSend: function () {
                    $("#GoodAgentClass").append('<img style="margin: 0px auto;display:block;" src="/images/loading.gif"/>');
                },
                success: function (msg) {
                    //alert(msg);
                    $("#GoodAgentClass").html(msg);
                },
                error: function (data) {
                }
            })
        }
        function doDefault_SAgent_ProductGood() {
            $.ajax({
                type: 'GET',
                url: '/Handler/Default_SAgent_ProductNewGood.ashx',
                dataType: 'text',
                data: '<%=_Pub_ProductGood_%>&strpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>&strpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>&strpInt_QueryString_ParentID=<%=pub_Int_CurParentID%>',
                beforeSend: function () {
                    $("#_Pub_strGoodBodyest").append('<img style="margin: 0px auto;display:block;" src="/images/loading.gif"/>');
                },
                success: function (msg) {
                    //alert(msg);
                    $("#_Pub_strGoodBodyest").html(msg);
                },
                error: function (data) {
                }
            })
        }
    </script>
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
    </script>
    <asp:Literal ID="Literal_WeiXinShare" runat="server"></asp:Literal>
</head>
<body onload="load_Multi()">
    <!--mask begin-->
    <div class="mask"></div>
    <!--mask end-->
    <%=_Pub_02Topbar_html%>
    <div style="clear: both;"></div>
    <div class="user_itlist_nb" style="display: none;">
        <div class="title">
            <h2>新品推荐</h2>
        </div>
    </div>
    <section class="main_title" style="" id="top2">
        <h2 id="topname"><%=_Pub_strGoodBody_Title %></h2>
        <a href="/" data-type="back" class="go-back" id="backurl"><span class="icons fa fa-angle-left" data-icon=""></span></a>
    </section>
    <div class="h30" id="h30" style=""></div>

    <div class="WX_con" id="J_main">
        <div class="jx">
            <div id="_Pub_strGoodBodyest" class="jx_list">
                <%--<%=_Pub_strGoodBodyest%>--%>
            </div>
            <div class="jx_map">
                <div id="GoodAgentClass" class="jx_map_bd WX_cat_list">
                    <%--<%=_Pub_ProductGoodClass_%>--%>
                </div>
            </div>
        </div>
    </div>
    <%=_Pub_03Footer_html%>
</body>
</html>
