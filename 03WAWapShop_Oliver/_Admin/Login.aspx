<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_03WAWapShop_Oliver._Admin.Login" %>
<!DOCTYPE HTML>
<HTML>
<HEAD>
<meta http-equiv="Content-type" content="text/html; charset=utf-8"> 
<title><%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店综合后台管理系统</title>
<%--      	<LINK href="image/background.css?version=css201709121928" rel="stylesheet">
--%>  
<link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 
</HEAD>
<body class="blubg">  

<div class="shop_login">
<!--标题 -->
<div class="login_title">
	<img src="skin/Images/Login_logo.jpg" />			
</div>
<!-- 登录框 -->
<div class="login_middle">
	<!-- 左边输入 -->
	<div class="login_l">
		<div style=" padding-top:20px;">
			 <form id="Form1" method="post" runat="server">
                <div class="login_t">请用您的账号登录</div> 
				  <div class="login_c"> 
             <ul>
             <li><span>用户名:</span><br> 
				<asp:TextBox id="txtUserID" runat="server" Width="144px" CssClass="l_input"></asp:TextBox>
	<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="用户帐号不能为空!" ControlToValidate="txtUserID" Display="Dynamic"></asp:RequiredFieldValidator></li>
	 
				 <li><span>密码</span>
				<br />
				<asp:TextBox id="txtUserPass" runat="server" TextMode="Password" Width="144px" CssClass="l_input"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator2"
			runat="server" ErrorMessage="用户密码不能为空!" ControlToValidate="txtUserPass" Display="Dynamic"></asp:RequiredFieldValidator></li>
			 <li><span>验证码</span><br />
				
				<asp:TextBox id="txtValidCode" runat="server" Width="72px" CssClass="l_input"></asp:TextBox><a href="?" title="点击刷新"> <img src="/Control/CheckCode.aspx" align="absbottom" border="0" height="30"/></a><asp:RequiredFieldValidator id="RequiredFieldValidator3" 
			runat="server" ErrorMessage="验证码不能为空!" ControlToValidate="txtValidCode" Display="Dynamic"></asp:RequiredFieldValidator></li>

				<%--<div align="center"><BR />--%>
							 <li class="mtop20">			<asp:Button id="btnLogin" runat="server" 
					Text="登 陆" OnClick="btnLogin_Click" Width="100px"   CssClass="b_input"></asp:Button>&nbsp;&nbsp;<input 
					id="Reset1" type="reset" value="重 填" style="width: 100px"  Class="b_reset" />
                </li>
                </ul>
                </div> 
			</form>
		</div>
	</div>
	<!--右边文字说明 -->
	<div class="login_r">
		 <br />
			&nbsp;&nbsp;&nbsp;&nbsp;	欢迎访问微店综合后台微店，在这里您可以购买到称心如意的商品，让您尽享网上购物的情趣。<br />
			<br>微店——<br>&nbsp;&nbsp;&nbsp;&nbsp;中国领先的综合性电商平台
	</div>
</div>
 
<!-- 底部图片 -->
<div class="login_footer">
	<img src="skin/Images/bot_logo.jpg" />
</div>
</div>			
	
</body>
</HTML>


