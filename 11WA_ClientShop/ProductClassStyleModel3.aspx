<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductClassStyleModel3.aspx.cs" Inherits="_11WA_ClientShop.ProductClassStyleModel3" %>

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
    <script src="/Templet/02ShiYi/js/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="/Templet/02ShiYi/skin/ys.css?version=css201709121928" rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <link rel="stylesheet" type="text/css" href="/Templet/02ShiYi/skin/box_swipe.css?version=css201709121928" media="all">
    <script type="text/javascript" src="/Templet/02ShiYi/js/swipe.js?version=js201709121928"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/func_Image.js?version=js201709121928"></script>
    <link href="/Templet/01WYJS/Css/mall.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/01WYJS/Css/base.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/01WYJS/Css/itemListTemplate.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/03LiuShenMa/Css/03LiuShenMa.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/04StyleModel/Css/StyleSheet4.css?version=css201709121928" rel="stylesheet" />
    <script src="/Templet/04StyleModel/Js/StyleSheet4.js?version=js201709121928"></script>
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
                    var varmsgList = msg.split('######');
                    $("#ShopNavIcon").html(varmsgList[0]);
                    $("#GoodClassStyle4").html(varmsgList[1]);
                    adjustShopNavIcon();
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
                    $("#SAgent_ProductNewGoodClass").append('<img style="margin: 0px auto;display:block;" src="/images/loading.gif"/>');
                },
                success: function (msg) {
                    //alert(msg);
                    $("#SAgent_ProductNewGoodClass").html("<div class=\"GoodClassStyle4NavTwoGood\"><ul>" + msg + "</ul></div>");
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

    <section class="main_title" style="" id="top2">
        <h2 id="topname"><%=_Pub_strGoodBody_Title %></h2>
        <a href="/" data-type="back" class="go-back" id="backurl"><span class="icons fa fa-angle-left" data-icon=""></span></a>
    </section>
    <div class="h30" id="h30" style="height: 40px; display: block;"></div>





    <div id="ShopNavIcon" class="jx_map">
        <%--<%=_Pub_ProductGoodClass_%>--%>
    </div>

    <div id="GoodClassStyle4" class="jx_list">
        <%--<%=_Pub_strGoodBodyest%>--%>
    </div>


    <div class="SAgent_ChoiceGood" style="background: white url('/Templet/03LiuShenMa/Images/FaveriMore1.jpg') no-repeat;margin-bottom:10px;    background-size: 100% 50px;"></div>
    <div id="SAgent_ProductNewGoodClass">
    </div>

    <%=_Pub_03Footer_html%>
</body>
</html>
