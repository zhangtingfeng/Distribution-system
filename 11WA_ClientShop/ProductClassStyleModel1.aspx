<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductClassStyleModel1.aspx.cs" Inherits="_11WA_ClientShop.ProductClassStyleModel1" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <title><%=pub_GetAgentShopName_From_Visit__%><%=_Pub_strGoodBody_Title %></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
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
    <script src="/Templet/02ShiYi/js/jquery-1.8.3.js?version=js201709121928" type="text/javascript" defer></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928" defer></script>
    <script src="/Templet/01WYJS/js/func_Image.js?version=js201709121928" defer></script>
    <link href="/Templet/01WYJS/Css/mall.css?version=js201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/01WYJS/Css/base.css?version=js201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/01WYJS/Css/itemListTemplate.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/01WYJS/Css/mall_Templet1.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/03LiuShenMa/Css/02Templet01.css?version=css201709121928" rel="stylesheet" />
    <%=pub_WeiXin__o2o_FootMarker_Location___%>
    <script type="text/javascript">
        function load_Multi() {
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
                    $("#hot_ul").append('<img style="margin: 0px auto;display:block;" src="/images/loading.gif"/>');
                },
                success: function (msg) {
                    //alert(msg);
                    $("#hot_ul").html(msg);
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
    <div id="GoodAgentClass" class="jx_map">
    </div>
    <div class="SAgent_ChoiceGood"></div>
    <section id="search-content">
        <div class="i_wrap margin_auto rel hide" id="item_classes_list_wrap" style="display: block;">
            <ul class="i_ul rel" id="hot_ul">
            </ul>
            <div class="clear"></div>
        </div>
    </section>
    <a href="javascript:void(0);" class="WX_backtop J_backTop J_ytag" id="gotopbtn">返回顶部</a>
    <script type="text/javascript">
        //注册当点击返回顶部的时候，回到网页顶部  
        $('#gotopbtn').click(function () {
            $('body').scrollTop(0);
            test();
        });
        //注册当页面发生滚动事件的时候，判断他有没有滚动条，如果有滚动条就显示“返回”，如果没有就不返回  
        window.onscroll = test;
        function test() {
            if ($('body').scrollTop() == 0) {
                $('#gotopbtn').removeClass("WX_backtop_active");
            } else {
                $('#gotopbtn').addClass("WX_backtop_active");
            }
        }
        $('#menu').click(function () {
            $('#menulist').toggle();
        });
    </script>
    <%=_Pub_03Footer_html%>
</body>
</html>
