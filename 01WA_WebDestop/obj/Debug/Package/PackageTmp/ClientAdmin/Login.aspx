<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin.Login" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title><%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店合作商户管理系统</title>
    <%--      	<LINK href="image/background.css?version=css201709121928" rel="stylesheet">
    --%>
    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            color: #FF0000;
        }

        .myb_input {
            background: none repeat scroll 0 0 #4e78a8;
            border: 1px solid #4270a4;
            color: #fff;
            cursor: pointer;
            font-size: 20px;
            height: 35px;
            width: 150px;
            text-align: center;
        }
    </style>
    <%--   <link href="../ShopReg/res/ui/css/screen.css?v=3.9" media="screen, projection" rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="../ShopReg/res/ui/css/base.css?v=3.9">
    <link rel="stylesheet" type="text/css" href="../ShopReg/res/passport/css/login.css?v=3.9">--%>
</head>
<body class="blubg">
    <!-- 标题 -->
    <div class="login_title">
        <div style="width: 700px; height: 100px; display: none; margin: 0px auto; text-align: center;">
            <div style="text-align: left; color: red; font-size: x-large"></div>
            <div style="color: white; font-size: large">更新优惠券功能。</div>
            <%--<a target="_blank" href="http://www.eggsoft.cn/news/info.aspx?id=155"><div style="color: white; font-size: large;color: lightgray; ">微信打击三级分销了吗,点击查看</div>--%>
        </div>
    </div>
    <div class="shop_login">
        <!-- 登录框 -->
        <div class="login_middle">
            <!-- 左边输入 -->
            <div class="login_l">
                <div style="padding-top: 20px;">
                    <form id="Form1" method="post" runat="server">
                        <div class="login_t">请用您的账号登录</div>
                        <div class="login_c">
                            <ul>
                                <li><span>用户名：</span><br>
                                    <asp:TextBox ID="txtUserID" runat="server" Width="144px" CssClass="l_input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户帐号不能为空!" ControlToValidate="txtUserID" Display="Dynamic"></asp:RequiredFieldValidator>
                                </li>
                                <li><span>密码：</span>
                                    <br />
                                    <asp:TextBox ID="txtUserPass" runat="server" TextMode="Password" Width="144px" CssClass="l_input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ErrorMessage="用户密码不能为空!" ControlToValidate="txtUserPass" Display="Dynamic"></asp:RequiredFieldValidator>
                                </li>
                                <li><span>验证码：</span><br />

                                    <asp:TextBox ID="txtValidCode" runat="server" Width="72px" CssClass="l_input"></asp:TextBox><a href="?" title="点击刷新"><img src="/Control/CheckCode.aspx" align="absbottom" border="0" /></a><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server" ErrorMessage="验证码不能为空!" ControlToValidate="txtValidCode" Display="Dynamic"></asp:RequiredFieldValidator></li>

                                <%--<div align="center"><BR />--%>
                                <li>
                                    <asp:Button ID="btnLogin" runat="server"
                                        Text="登  陆" OnClick="btnLogin_Click" Width="100px" CssClass="b_input"></asp:Button>&nbsp;&nbsp;<input
                                            id="Reset1" type="reset" value="重  填" style="width: 100px" class="b_reset" />
                                </li>
                            </ul>
                        </div>
                    </form>
                </div>
            </div>
            <!-- 右边文字说明 -->
            <div class="login_r">
                <br />
                <span class="auto-style1"><strong>强烈推荐您使用360浏览器使用本系统，否则可能有兼容问题。</strong></span>
			<br>
                —你永久的可靠的朋友
		 
            </div>


            <div class="reg" style="display: none;">
                <p>
                    还没有账号？<br>
                    赶快免费注册一个吧！
                </p>
                <p>
                    <asp:HyperLink ID="HyperLink1" Width="150px" CssClass="myb_input" runat="server" NavigateUrl="http://eggsoft.cn/">立 即 注 册</asp:HyperLink>
                </p>
            </div>
        </div>
        <!-- 底部图片 -->
        <div class="login_footer" style="color: blue;display:none;">
            宣传页地址:<a href="https://Upload.eggsoft.cn/UpLoad/flash/03Show.pdf"><span style="color: blue">https://Upload.eggsoft.cn/UpLoad/flash/03Show.pdf</span></a>
        </div>
        <div class="login_footer">
            <img src="Image/AdminLogin_webuysys_footer.jpg" />
        </div>
    </div>

</body>
</html>
