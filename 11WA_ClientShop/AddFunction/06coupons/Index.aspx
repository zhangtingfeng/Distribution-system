<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="_11WA_ClientShop.AddFunction._06coupons.Index" %>

<!DOCTYPE html>
<html>
<head>
    <title>优惠券</title>
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">


    <link href="/AddFunction/06coupons/Css/index.css?version=css201709121928" rel="stylesheet" />
    <script src="/Scripts/jquery-1.8.2.min.js?version=js201709121928"></script>

    <link href="/Templet/01WYJS/Css/mall.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>

    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
   
    </script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
    <script src="/Templet/01WYJS/js/layer.m.js"></script>
    <link href="/Templet/01WYJS/Css/layer.css" rel="stylesheet" />


    <style type="text/css">
        #mcoverShareGouWuQuan {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.7);
            display: none;
            z-index: 20000;
        }
    </style>

</head>
<body>
    <div id="mcoverShareGouWuQuan" onclick="weChatDisplayNone()" style="display: none;">
        <img src="/images/guide1.png">
        <a id="mcoverGuangGao" href="/">技术支持：微云基石</a>
    </div>


    <div class="bgc">
        <ul id="myTab1" class="coupons-nav">
            <li><a href="javascript:;" id="unUsed" style="margin-left: -1px;">未使用</a> </li>
            <li><a href="javascript:;" id="used">已使用</a> </li>
            <li><a href="javascript:;" id="expired">已过期</a> </li>
        </ul>
        <div class="kong">
            <img src="/AddFunction/06coupons/Images/coupon_null.png" />
            <p>暂无优惠券</p>
        </div>
        <div class="container">
            <div id="myTabContent" style="float: left; width: 100%;">
                <div class="coupons_list">
                    <ul id="ulCoupons">
                        <%--<li>
                            <span class="coupons_price"><i>￥</i>10.00<em>全场商品可用</em></span><div class="coupons_tips">
                                <div class="coupons_tips_left"><span><i class="icon_tips icon-icon_warning"></i>&nbsp;订单无限制 </span><span><i class="icon_clock icon-icon_time"></i>&nbsp; 2017.04.08-2021.01.03</span></div>
                                <a class='btn_receive' href='javascript:goToUse("")'>去使用</a>
                            </div>
                        </li>--%>
                        <%=pub_MyShowList%>
                    </ul>
                </div>
            </div>
        </div>
    </div>


    <div class="bgc">
        <a class="btn_receive_Get" href='javascript:goToLook()'>还没有优惠券吗?查看这里</a>
    </div>


    <script type="text/javascript">

        $(function () {

            var usedType = (new QueryString())["usedtype"];
            if (!usedType || usedType == 1) {
                $('#unUsed').addClass('border-blue');//下划线选中

            }
            else if (usedType == 2) {
                $('#used').addClass('border-blue');//下划线选中
                $('#ulCoupons').addClass('bg_curve_ed');////过期标志  含有加灰色的 !important;          


            }
            else if (usedType == 3) {
                $('#expired').addClass('border-blue');//下划线选中
                $('#ulCoupons').addClass('bg_curve_ed');////过期标志       含有加灰色的 !important;          
            }

            if ($("#ulCoupons li").length == 0) {
                $(".kong").show();
                $(".container").hide();
            }
        });

        $('#used').click(function () {
            window.location.href = "<%=Pub_Agent_Path%>/addfunction/06coupons/index.aspx?usedtype=2";
        });

        $('#unUsed').click(function () {
            window.location.href = "<%=Pub_Agent_Path%>/addfunction/06coupons/index.aspx?usedtype=1";
        });

        $('#expired').click(function () {
            window.location.href = "<%=Pub_Agent_Path%>/addfunction/06coupons/index.aspx?usedtype=3";
        });

        function goToUse(productIds) {
            if (productIds == "0" || productIds == "") {
                window.location.href = "<%=Pub_Agent_Path%>/default.aspx";
            }
            else {
                window.location.href = "<%=Pub_Agent_Path%>/product-" + productIds + ".aspx";
            }
        }


        function goToLook() {
            window.location.href = "<%=Pub_Agent_Path%>/addfunction/06coupons/indexlist.aspx";
        }


        function weChatDisplayNone() {
            $("#mcoverShareGouWuQuan").css("display", "none");  // 点击弹出层，弹出层消失
        }


        //分享之后的继续调用
        function AfterShareContinuesAskShareGouWuan() {
            weChatDisplayNone();

            layer.open({
                type: 2,
                content: "请等待您的朋友领取",
                time: 2
            });
        }

        function goToShare(goToShareIDVouchersNum, ShareMoney, varstrVouchers_Title) {
            path = '<%=(Eggsoft.Common.Application.AppUrl + Pub_Agent_Path)%>' + '/addfunction/06coupons/indexlist.aspx?gotoshareidvouchersnum=' + goToShareIDVouchersNum;
            WeiXin_descAppPageContent = '<%=pub_GetAgentShopName_From_Visit__%>' +' '+ varstrVouchers_Title + '优惠券分享';
            WeiXin_shareAppAllPageTitle = '<%=pub_GetNickName_From_Share__%>' + '分享了一张¥' + ShareMoney +" "+ varstrVouchers_Title + '优惠券';

            wx.onMenuShareAppMessage({
                title: WeiXin_shareAppAllPageTitle, // 分享标题
                desc: WeiXin_descAppPageContent, // 分享描述
                link: path, // 分享链接
                imgUrl: WeiXin_imgAllPageUrl, // 分享图标
                type: '', // 分享类型,music、video或link，不填默认为link
                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    //	alert("arg1");
                    AfterShareContinuesAskShareGouWuan();
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    //  alert("用户取消分享朋友后执行的回调函数");
                    // 用户取消分享后执行的回调函数
                },

            });

            wx.onMenuShareTimeline({
                title: WeiXin_shareAppAllPageTitle, // 分享标题
                link: path, // 分享链接
                imgUrl: WeiXin_imgAllPageUrl, // 分享图标
                success: function () {
                    ///alert("arg2");
                    AfterShareContinuesAskShareGouWuan();
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    //  alert("用户取消分享朋友圈后执行的回调函数");
                    // 用户取消分享后执行的回调函数
                },
            });





            layer.open({
                type: 2,
                content: "最后一步,分享朋友圈或者发送给好友",
                time: 2,
                end: function (layer) {
                    $("#mcoverShareGouWuQuan").css("display", "block");  // 点击弹出层，弹出层消失
                }
            });


        }

    </script>
    <%=_Pub_03Footer_html%>
</body>
</html>
