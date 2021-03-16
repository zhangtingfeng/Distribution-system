<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppLogin.aspx.cs" Inherits="_11WA_ClientShop.User.AppLogin" %>

<!DOCTYPE html>
<html class="um landscape">
<head>
    <title></title>

    <meta charset="utf-8">
    <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <link rel="stylesheet" href="/AppCanR/css/fonts/font-awesome.min.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-box.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-base.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-color.css">
    <link rel="stylesheet" href="/AppCanR/css/appcan.icon.css">
    <link rel="stylesheet" href="/AppCanR/css/appcan.control.css">
    <link rel="stylesheet" href="/AppCanR/AppLogin_content/css/main.css">
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/layer.m.js?version=js201709121928"></script>
    <link href="/Templet/01WYJS/Css/layer.css?version=css201709121928" rel="stylesheet" />

    <script type="text/javascript">
        var varpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>;
        var varpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>;
        var varGetAppConfiugServicesURL="<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";
    </script>
    <script src="/AppCanR/AppLogin_content/Js/main.js?version=js201709121928"></script>
    <style>
        .logo {
            background-image: url('<%=strFirstImageFullName%>');
        }
    </style>

</head>
<body class="um-vp bc-bg" ontouchstart>
    <div id="page_0" class="up ub ub-ver" tabindex="0" style="position: relative;">
        <div id="content" class="ub-f1 tx-l ub ub-ver c-wh1">
            <div class="uinn ub ub-ac ub-pc">
                <div class="logo ub-img umhw1"></div>
            </div>
            <div class="uinn ub ub-ac ub-pc ub-ver">
                <div class="ulev0 t-blu addfont umar-t">
                    <%=strShopClientName%>
                </div>
                <div class="ulev1 t-blu addfont uinn">
                    移动商务平台
                </div>
            </div>
            <div class="ub ub-ver ub-f1">
                <form method="get" action="http://www.appcan.cn">
                    <div class="uba b-gra umar-a uc-a1 c-wh">
                        <div class="ub ub-ac">
                            <div class="umhw resuser ub-img umar-l"></div>
                            <div class="ub-f1 uinput uinn">
                                <input placeholder="手机" id="uid" value="" type="text" class="uc-a1">
                            </div>
                        </div>
                    </div>
                    <div class="uba b-gra umar-a uc-a1 c-wh">
                        <div class="ub ub-ac">
                            <div class="umhw respwd ub-img umar-l"></div>
                            <div class="ub-f1 uinput uinn">
                                <input placeholder="密码" id="upwd" value="" type="password" class="uc-a1">
                            </div>
                        </div>
                    </div>
                    <div class="ub umar-a ub-ac" style="display: none !important;">
                        <div class="checkbox umar-r">
                            <input type="checkbox" class="uabs ub-con">
                        </div>
                        <div class="lv_title ub-f1 marg-l ub ub-ver ut-m line1">
                            记住用户名
                        </div>
                        <div class="ub t-blu umar-r ulev-1">
                            忘记密码
                        </div>
                    </div>
                    <div class="uin uinn">
                        <div class="btn ub ub-ac bc-text-head ub-pc bc-btn uc-a1" id="submit" onclick="btnLoginNow()">
                            登　录
                        </div>
                    </div>
                    <div class="umar-a ub t-blu">
                        <div class="ub-f1 ulev-1 uinn3" onclick="RegisterNow()">
                            立即注册/忘记密码
                        </div>
                        <div class="ulev-1 uinn3" style="display: none;">
                            无账号快捷登录
                        </div>
                    </div>
                    <button type="submit" class="uinvisible"></button>
                </form>
            </div>
        </div>
        <div id="footer" class=""></div>
    </div>
    <script src="/AppCanR/js/appcan.js"></script>
    <script src="/AppCanR/js/appcan.control.js"></script>
</body>

</html>
