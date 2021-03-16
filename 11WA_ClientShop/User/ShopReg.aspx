<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopReg.aspx.cs" Inherits="_11WA_ClientShop.User.ShopReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户注册</title>
<!-- Mobile Devices Support @begin -->
            <meta content="no-cache,must-revalidate" http-equiv="Cache-Control" /><meta content="no-cache" http-equiv="pragma" /><meta content="0" http-equiv="expires" /><meta content="telephone=no, address=no" name="format-detection" /><meta content="width=device-width, initial-scale=1.0" name="viewport" /><meta name="apple-mobile-web-app-capable" content="yes" /> <!-- apple devices fullscreen -->
            <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
        <!-- Mobile Devices Support @end -->
        <link href="Styles/<%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityTemplet")%>/style.css?version=css201709121928" rel="stylesheet" type="text/css" />
       
</head>
<body>

   <div class="top"> * 号必填<a href="ShopLogin.aspx" class="btn_2">登陆</a></div>  
<div class="box_weixin">
    <form runat="server" class="form_box">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody><tr>
        <td class="title">用户名：</td>
        <td class="input"><asp:TextBox ID="txt_reguid" runat="server"></asp:TextBox>
		    </td>
        <td><span>*</span><div id="showResult" style="float:left"></div>
          </td>
      </tr>
    </tbody></table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody><tr>
        <td class="title">密码：</td>
        <td class="input"><asp:TextBox ID="txt_regpwd" runat="server" TextMode="Password"></asp:TextBox>
		    </td>
        <td><span>*</span></td>
      </tr>
    </tbody></table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody><tr>
        <td class="title">确认密码：</td>
        <td class="input"><asp:TextBox ID="txt_regrepwd" runat="server" TextMode="Password"></asp:TextBox>
									</td>
        <td><span>*</span></td>
      </tr>
    </tbody></table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody><tr>
        <td class="title">邮箱：</td>
        <td class="input"><asp:TextBox ID="txt_regemail" runat="server"></asp:TextBox>
		    </td>
        <td><span>*</span></td>
      </tr>
    </tbody></table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody><tr>
        <td class="title">手机号码：</td>
        <td class="input"><asp:TextBox ID="txt_regphoneno" runat="server"></asp:TextBox>
		    </td>
        <td><span>*</span></td>
      </tr>
    </tbody></table>
    <div class="btn">
        <asp:Button 
            ID="btn_reg" runat="server" Text="注  册" 
            onclientclick="return checkReg();" onclick="btn_reg_Click" />
    </div>
      <div style="display:block;height:50px"></div>

  </form>
 </div>  
 


    <script src="../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

        });
        function checkReg() {

            var uid = $.trim($("#txt_reguid").val().replace(/[　]/g, ''));
            var pwd = $.trim($("#txt_regpwd").val().replace(/[　]/g, ''));
            var repwd = $.trim($("#txt_regrepwd").val().replace(/[　]/g, ''));
            var email = $.trim($("#txt_regemail").val().replace(/[　]/g, ''));
            var phoneno = $.trim($("#txt_regphoneno").val().replace(/[　]/g, ''));

            if (uid == "") {
                alert("用户名不能为空");
                $("#txt_reguid").val("");
                return false;
            }


            if (uid.length < 4 || uid.length > 40) {
                alert("用户名长度应在4-40个字符之间");
                return false;
            }
            if (pwd == "") {
                alert("密码不能为空");
                $("#txt_regpwd").val("");
                return false;
            }
            if (pwd.length < 6 || pwd.length > 16) {
                alert("密码长度应在6-16个字符之间");
                return false;
            }
            if (pwd != repwd) {
                alert("两次密码不一致");
                return false;
            }
            if (email == "") {
                alert("邮箱地址不能为空");
                $("#txt_regemail").val("");
                return false;
            }
            if (email != "") {
                var regemail = /^\w{3,}@\w+(\.\w+)+$/;
                if (!regemail.test(email)) {
                    alert("邮箱地址格式不正确");
                    return false;
                }
            }
            if (phoneno == "") {
                alert("手机号不能为空");
                $("#txt_regphoneno").val("");
                return false;
            }
            if (phoneno.length != 11) {
                alert("手机号位数不正确");
                return false;
            }
            else {
                var reg = /^(1(([35][0-9])|(47)|[8][0123456789]))\d{8}$/;
                if (!reg.test(phoneno)) {
                    alert("手机号输入错误");
                    return false;
                }
            }
            return true;
        }
        </script>
      <asp:Panel ID="WUC_Bottom" runat="server">
    </asp:Panel>
</body>
</html>
