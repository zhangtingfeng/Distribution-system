<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Class2_Add.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class2_Add" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<title>ClassAddSmall</title> 
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />  	
</HEAD>
<body>
<form id="Form1" method="post" runat="server">
	<table height="679" cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
		<tr>
			<td vAlign="top" align="center"> 
				<table class="border" style="WIDTH: 100%; HEIGHT: 107px" cellSpacing="2" cellPadding="0"
					align="center" border="0">
					<tr class="title" >
						<th align="center" colSpan="2" height="25"><strong>添加二级分类</strong></th>
					</tr>
					<tr class="tdbg" bgColor="#e3e3e3">
						<td  width="150"   align="right"  height="35">
							<strong>一级版块：</strong>
						</td>
						<td  bgColor="#ecf5ff">
							<asp:DropDownList ID="ddlBigClass" runat="server">
							</asp:DropDownList>
						</td>
					</tr>
					<tr class="tdbg" bgColor="#e3e3e3">
						<td  align="right"  height="35">
							<strong>二级名称：</strong>
						</td>
						<td  bgColor="#ecf5ff">
							<asp:textbox id="txtSmallClassName" runat="server" Width="260px" CssClass="l_input"></asp:textbox>
							<br />
								<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="小类名称不能为空!" ControlToValidate="txtSmallClassName" Display="Dynamic"></asp:requiredfieldvalidator></td>
					</tr>
                    <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>导航小图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Small" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Small" runat="server" />
                    导航小图标 建议尺寸 高度宽度不大于50px 。png格式。模板4适用
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>导航大图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Big" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Big" runat="server" />
                    导航大图标 建议尺寸 宽度640px，高度480px 。jpg格式。模板4适用
                </td>
            </tr>
					<tr class="tdbg" bgColor="#e3e3e3">
						<td align="right"  height="35">
							<strong>排列位置：</strong>
						</td>
						<td align="center" bgColor="#ecf5ff" height="35">
							<div align="left">
								<asp:textbox id="txtSmallClassPos" runat="server" Width="260px" CssClass="l_input">0</asp:textbox>
								<br />
								<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="排列位置不能为空!" ControlToValidate="txtSmallClassPos" Display="Dynamic"></asp:requiredfieldvalidator>
								<br />
								<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSmallClassPos"
									ErrorMessage="排列位置必须是数字类型!" ValidationExpression="^\d{1,}$" Display="Dynamic"></asp:RegularExpressionValidator></div>
						</td>
					</tr>
					
					<tr class="tdbg" bgColor="#e3e3e3">
						<td style="display: none;" align="right" height="35" >
							<strong>版块属性：</strong>
						</td>
						<td align="center" bgColor="#ecf5ff" style="display: none;">
							<div align="left">
								<asp:checkbox id="cbIsShow" runat="server" Text="显示" Checked="True"></asp:checkbox>
								<asp:CheckBox ID="cbIsLock" runat="server" Text="锁定" /></div>
						</td>
					</tr>
					
					<tr class="tdbg" bgColor="#ecf5ff">
						<td   align="center" >&nbsp;
						</td>
						<td align="center" bgColor="#ecf5ff" height="45" >
							<div align="left">
								<asp:button id="btnAdd" runat="server" Width="72px" Text=" 添  加 " OnClick="btnAdd_Click" CssClass="b_input"></asp:button></div>
						</td>
					</tr>
					 
				</table>
			</td>
		</tr>
	</table>
</form>
</body>
</HTML>
