<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_11WA_ClientShop.User.Register" %>

<!DOCTYPE html>
<html class="um landscape min-width-240px min-width-320px min-width-480px min-width-768px min-width-1024px">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<head>
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="target-densitydpi=device-dpi, width=device-width, initial-scale=1, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
    <link rel="stylesheet" href="/AppCanR/css/fonts/font-awesome.min.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-box.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-base.css">
    <link rel="stylesheet" href="/AppCanR/css/ui-color.css">
    <link rel="stylesheet" href="/AppCanR/css/appcan.icon.css">
    <link rel="stylesheet" href="/AppCanR/css/appcan.control.css">
    <link rel="stylesheet" href="/AppCanR/css/main.css">
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script src="/Scripts/jquery-1.8.2.min.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/layer.m.js?version=js201709121928"></script>
    <link href="/Templet/01WYJS/Css/layer.css?version=css201709121928" rel="stylesheet" />

    <script type="text/javascript">
        var varpub_Int_ShopClientID=<%=pub_Int_ShopClientID%>;
        var varpub_Int_Session_CurUserID=<%=pub_Int_Session_CurUserID%>;
        var varGetAppConfiugServicesURL="<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";
    </script>
    <script src="/AppCanR/Register_content/js/main.js?version=a2js201709121928"></script>
</head>
<body class="um-vp bc-bg" ontouchstart>
    <div class="ub ub-ver uinn-a3 ub-fv">
        <div class="ub ub-ver uinn uinn-at1">
            <div class="umar-a uba bc-border c-wh">
                <div class="ub ub-ac ubb umh5 bc-border ">
                    <div class=" uinput ub ub-f1">
                        <div class="uinn fa fa-user sc-text"></div>
                        <input placeholder="手机号码" type="text" class="ub-f1" id="userMobilePhone">
                    </div>
                </div>
                <div class="ub ub-ac ubb umh5 bc-border ">
                    <div class=" uinput ub ub-f1">
                        <div class="uinn fa fa-user sc-text"></div>
                        <input placeholder="验证码" type="text" class="ub-f1" id="CheckPassWord">
                        <input type="hidden" id="ShouldCheckCode">
                        <input value="免费获取验证码" style="background-color: #007DB8" type="button" class="ub-f1 btn bc-text-head  bc-btn uc-a1" id="btnSendCheckPassWord" onclick="settime(this)" />
                    </div>
                </div>
                <div class="ub ub-ac umh5 bc-border ">
                    <div class=" uinput ub ub-f1">
                        <div class="uinn fa fa-lock sc-text"></div>
                        <input placeholder="密码,最少六位.注册即重置密码" type="password" class="umw4 ub-f1" id="userpwd">
                    </div>
                </div>
                <div class="ub ub-ac ubb umh5 bc-border ">
                    <div class=" uinput ub ub-f1">
                        <div class="uinn fa fa-lock sc-text"></div>
                        <input placeholder="重复密码,最少六位" type="password" class="umw4 ub-f1" id="Repeatuserpwd">
                    </div>
                </div>

                <div class="ub ub-ac ubb umh5 bc-border ">
                    <div class=" uinput ub ub-f1">
                        <div class="uinn fa fa-user sc-text"></div>
                        <input placeholder="推荐人手机号码，没有可以不填写" type="text" class="ub-f1" id="RecommanduserPhone">
                    </div>
                </div>
            </div>
            <div class="umar-a uba bc-border c-wh">
            </div>
            <div class="ub ub-ver">
                <div class="uinn-at1">
                    <div class="btn ub ub-ac bc-text-head ub-pc bc-btn uc-a1" id="submit" onclick="FunctionSubmit()">
                        注册
                    </div>
                </div>
            </div>

            <div class="umar-a ub t-blu">
                <div class="ub-f1 ulev-1 uinn3" style="color: #177fc7;" onclick="LoginNow()">
                    已有账号,直接登陆
                </div>
                <div class="ulev-1 uinn3" style="display: none;">
                    无账号快捷登录
                </div>
            </div>

            <button type="submit" class="uinvisible"></button>

        </div>

    </div>
    <%--    <script src="js/appcan.js"></script>
    <script src="js/appcan.control.js"></script>--%>
</body>

</html>
