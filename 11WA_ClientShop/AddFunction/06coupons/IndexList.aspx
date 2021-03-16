<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexList.aspx.cs" Inherits="_11WA_ClientShop.AddFunction._06coupons.IndexList" %>

<!DOCTYPE html>
<html>
<head>
    <title>全场可领取的购物券</title>
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

    <link href="/Templet/01WYJS/Css/layer.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
   
    </script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
    <script src="/Templet/01WYJS/js/layer.m.js"></script>
    <link href="/Templet/01WYJS/Css/layer.css" rel="stylesheet" />
</head>
<body onload="load_Multi();">



    <div class="bgc">
        <ul id="myTab1" class="coupons-nav">
            <li style="width: 100%;"><a href="javascript:;" id="unUsed" style="margin-left: -1px;">可领取的购物券</a> </li>
        </ul>
        <div class="kong">
            <img src="/AddFunction/06coupons/Images/coupon_null.png" />
            <p>暂无优惠券</p>
        </div>
        <div class="container">
            <div id="myTabContent" style="float: left;">
                <div class="coupons_list">
                    <ul id="ulCoupons">
                        <%--<li style="width: 100% !important">
                            <span class="coupons_price"><i>￥</i>10.00<em>全场商品可用</em></span><div class="coupons_tips">
                                <div class="coupons_tips_left"><span><i class="icon_tips icon-icon_warning"></i>&nbsp;订单无限制 </span><span><i class="icon_clock icon-icon_time"></i>&nbsp; 2017.04.08-2021.01.03</span></div>
                                <a class='btn_receive' style="float:right; margin-right:4.5rem" href='javascript:goToLook("")'>查看</a>
                                <a class='btn_receive' href='javascript:goToUse("")'>立即领取</a>
                            </div>
                        </li>--%>
                        <%=pub_ShowList%>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script src="/Templet/01WYJS/js/layer.m.js?version=js201709121928"></script>
    <div class="bgc">
        <a class="btn_receive_Get" href='javascript:inputYouHuiQuanAction()'>输入优惠券号码</a>
    </div>
    <div class="bgc" style="margin-top: 30px;">
        <a class="btn_receive_Get" href='javascript:WatchYouHuiQuanAction()'>查看我的优惠券</a>
    </div>

    <script type="text/javascript">
        function WatchYouHuiQuanAction() {
            window.location.href = "/addfunction/06coupons/index.aspx";
        }


        function inputYouHuiQuanAction() {
            var UserVouchersNum = prompt("输入您获得优惠券号码（以80开头的）", "");
            if (UserVouchersNum != null && UserVouchersNum != undefined && UserVouchersNum.length > 0) {
                inputYouHuiQuan(UserVouchersNum);
            }
        }

        function load_Multi() {
            var varQueryStringList = new QueryString();
            var vargoToGetVouchersNumID = varQueryStringList["goToShareIDVouchersNum"];///大写
            var vargoToGetVouchersNumIDToLower = varQueryStringList["gotoshareidvouchersnum"];///小写
            if (vargoToGetVouchersNumID != null && vargoToGetVouchersNumID != undefined && vargoToGetVouchersNumID != '') {
                setTimeout("inputYouHuiQuan('" + vargoToGetVouchersNumID + "')", 1000);//1000为1秒钟,设置为1分钟。  
            }
            if (vargoToGetVouchersNumIDToLower != null && vargoToGetVouchersNumIDToLower != undefined && vargoToGetVouchersNumIDToLower != '') {
                setTimeout("inputYouHuiQuan('" + vargoToGetVouchersNumIDToLower + "')", 1000);//1000为1秒钟,设置为1分钟。  
            }


            var vargoToGetVouchersNumID=varQueryStringList["goToGetVouchersNumID"];
            var vargotogetvouchersnumidToLower=varQueryStringList["gotogetvouchersnumid"];
            ///扫描二维码 自动领取购物券
            if (vargoToGetVouchersNumID != null && vargoToGetVouchersNumID != undefined && vargoToGetVouchersNumID != '') {
                setTimeout("goToUse('" + vargoToGetVouchersNumID + "')", 2000);//goToUse(varID)。  
            }
            if (vargotogetvouchersnumidToLower != null && vargotogetvouchersnumidToLower != undefined && vargotogetvouchersnumidToLower != '') {
                setTimeout("goToUse('" + vargotogetvouchersnumidToLower + "')", 2000);//goToUse(varID)。  
            }
        }


        function inputYouHuiQuan(varUserVouchersNum) {




            var url = "<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>/Order/DoGetYouHuiQuan.asmx/doGetOneYouHuiQuan?&strpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>&strpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>&VouchersNum=" + varUserVouchersNum;
            //loading带文字
            layer.open({
                type: 2
              , content: '领用中', time: 2
            });

            var result = -1;
            $.ajax({
                type: "POST",
                url: url,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonp201704170623Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    result = (json.ErrorCode);
                    if (result == -5) {
                        //提示
                        layer.open({
                            content: '优惠券已被领用完毕'
                          , skin: 'msg'
                          , time: 2 //2秒后自动关闭
                        });
                    } else if (result == -4) {
                        //提示
                        layer.open({
                            content: '优惠券不存在'
                          , skin: 'msg'
                          , time: 2 //2秒后自动关闭
                        });
                    } else if (result == -3) {
                        //提示
                        layer.open({
                            content: '优惠券已过期  '
                          , skin: 'msg'
                          , time: 2 //2秒后自动关闭
                        });
                    } else if (result == 2) {
                        layer.open({
                            content: '优惠券已被' + decodeURIComponent(json.ErrorCodeGetNickName) + '领用'
                         , skin: 'msg'
                         , time: 2 //2秒后自动关闭
                        });
                    }
                    else if (result == 3) {
                        layer.open({
                            content: '优惠券限制领用' + json.LimitOnePerson + '张'
                         , skin: 'msg'
                         , time: 2 //2秒后自动关闭
                        });
                    }
                    else if (result == 1) {
                        //loading带文字
                        layer.open({
                            type: 2, time: 1
                          , content: '领用成功',
                            end: function () {
                                window.location.href = "/addfunction/06coupons/index.aspx";
                            }
                        });
                    }
                    else if (result == 4) {
                        //loading带文字
                        layer.open({
                            content: decodeURIComponent(json.GotFromFriend) + '分享的优惠券已被成功领用'
                          , skin: 'msg'
                          , time: 2, //2秒后自动关闭
                            end: function () {
                                window.location.href = "/addfunction/06coupons/index.aspx";
                            }
                        });
                    }
                    return;
                },
                error: function () {
                }
            });
            return result;
        }





        function goToUse(varID) {
            var url = "<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>/Order/DoGetYouHuiQuan.asmx/doGetOneYouHuiQuan?&strpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>&strpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>&VouchersSchemeID=" + varID;
            //loading带文字
            layer.open({
                type: 2
              , content: '领用中', time: 2
            });

            var result = -1;
            $.ajax({
                type: "POST",
                url: url,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonp1Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    result = (json.ErrorCode);
                    if (result == -5) {
                        //提示
                        layer.open({
                            content: '优惠券已被领用完毕'
                          , skin: 'msg'
                          , time: 2 //2秒后自动关闭
                        });
                    }
                    else if (result == -4 || result == -3) {
                        //提示
                        layer.open({
                            content: '优惠券不存在或者已过期  '
                          , skin: 'msg'
                          , time: 2 //2秒后自动关闭
                        });
                    }
                    else if (result == 3) {
                        layer.open({
                            content: '优惠券限制领用' + json.LimitOnePerson + '张'
                         , skin: 'msg'
                         , time: 2 //2秒后自动关闭
                        });
                    } else if (result == 1) {
                        //loading带文字
                        layer.open({
                            type: 2, time: 1
                          , content: '领用成功',
                            end: function () {
                                window.location.href = "/addfunction/06coupons/index.aspx";
                            }
                        });
                    }
                    return;
                },
                error: function () {
                }
            });
            return result;
        }



    </script>
    <%=_Pub_03Footer_html%>
</body>
</html>
