<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeyAnswer_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.KeyAnswer_Manage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>关键词回复</title> 
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 
</head>
<body>
<form id="form1" runat="server">
<div class="mhead">
<h1>  关 键 词 回 复 管 理</h1> 
</div>
<table style="width:100%;" class="altrowstable"  >
<tr>
	<td>
	网页请输入网址。消息回复。请在素材编辑中查找ID并输入。<a target="_blank" href="Resource-1.aspx">查看文本消息</a> 
		<a target="_blank" href="Resource-2.aspx">查看单图文</a> <a target="_blank" href="Resource-3.aspx">查看多图文</a> 
		<%-- <a target="_blank" href="Resource-ImageType5.aspx">查看图文</a> <a target="_blank" href="Resource-VoiceType6.aspx">查看语音</a> <a target="_blank" href="Resource-VideoType7.aspx">查看视频</a> <a target="_blank" href="Resource-MusicType8.aspx">查看音乐</a><br />
		--%>
		 <br />
		<table style="width:100%;">
			<tr>
				<td class="stylePercent40">
					关键词：</td>
				<td>
					<asp:TextBox ID="TextBox_KeyName" runat="server" Width="296px" 
						ToolTip="请输入关键词" CssClass="l_input">                                    
						</asp:TextBox>
							<asp:RequiredFieldValidator id="RequiredFieldValidator_TextBox_KeyName" runat="server" 
								ErrorMessage="关键词不能为空，序号小的排前面!" ControlToValidate="TextBox_KeyName"></asp:RequiredFieldValidator>
					</td>
			</tr>
			<tr>
				<td class="stylePercent40">
					回复类型：</td>
				<td>
					<asp:RadioButtonList ID="RadioButtonList_View_Click" runat="server" 
						RepeatDirection="Horizontal">
					   <asp:ListItem Value="1">文本回复</asp:ListItem>
					   <asp:ListItem Selected="True" Value="2">单图文回复</asp:ListItem>
						<asp:ListItem Value="3">多图文回复</asp:ListItem>
						<%-- <asp:ListItem Value="5">图片消息</asp:ListItem>
						 <asp:ListItem Value="6">语音消息</asp:ListItem>
						 <asp:ListItem Value="7">视频消息</asp:ListItem>
						 <asp:ListItem Value="8">音乐消息</asp:ListItem>--%>
					 </asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td class="stylePercent40">
					回复ID：</td>
				<td>
					<asp:TextBox ID="TextBox_MenuContent" runat="server" Width="296px" 
						ToolTip="请输入http网址 或者 相应素材ID"></asp:TextBox>
										<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" 
								ErrorMessage="请输入相应素材ID" ControlToValidate="TextBox_MenuContent"></asp:RequiredFieldValidator>

					<asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_MenuContent" 
						runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_MenuContent" 
						ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
										<asp:RequiredFieldValidator id="RequiredFieldValidator_TextBox_MenuContent" runat="server" 
								ErrorMessage="菜单顺序不能为空，序号小的排前面!" ControlToValidate="TextBox_MenuContent"></asp:RequiredFieldValidator>

				</td>
			</tr>
			<tr>
				<td colspan="2" style="text-align: center">
&nbsp;&nbsp;&nbsp;           
&nbsp;&nbsp;&nbsp;
					<asp:Button ID="Button_Save" runat="server" Text="保存信息" 
						onclick="Button_Save_Click" CssClass="b_input" />
&nbsp;&nbsp;&nbsp;
					</td>
			</tr>
		</table>
	</td>
</tr>
</table> 	
</form>
</body>
</html>
