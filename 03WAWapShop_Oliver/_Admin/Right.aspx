<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Right.aspx.cs" Inherits="_03WAWapShop_Oliver._Admin.Right" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<title>Admin_Index_Main</title> 
<link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 	
</HEAD>
<body>
<form id="Form1" runat="server">
	<table cellSpacing="0" cellPadding="0" width="99%" border="0" class="mtabs">
		<tr bgcolor="#a4b6d7" class="title"> 
			<th vAlign="top" width="100%" colspan="4" style="HEIGHT: 24px; text-align: center;">&nbsp;<%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店相关信息</th>
		
		</tr>
		<tr>
			<td class="border" colSpan="3" style=" text-align:center;">
				<table cellSpacing="1" cellPadding="2" width="748" border="0" align="center"> 
					<tr>
						<td nowrap class="style1" align="right">用户数量：</td>
						<td class="tdbg"  class="style_Right">
							<asp:Literal ID="Literal_UserCount" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td nowrap class="style1" align="right">进驻商户数量：</td>
						<td class="tdbg" class="style_Right">
                        <a href="tab_ShopClient/UserManage.aspx"><asp:Literal ID="Literal_CompanyCount" runat="server"></asp:Literal></a>
						</td>
					</tr>
					<tr>
						<td nowrap class="style1" align="right">资金统计：</td>
						<td class="tdbg" class="style_Right">&nbsp;</td>
					</tr>
					<tr>
						<td nowrap class="style1" align="right">未发货订单数量：</td>
						<td class="tdbg" class="style_Right"><asp:label id="Label_Board_WaitGiveGoods" runat="server"></asp:label> </td>
					</tr>
					<tr>
						<td nowrap class="style1" align="right">已收资金统计7天内：</td>
						<td class="tdbg"><asp:label id="Label_In7Days" runat="server"></asp:label>  </td>
					</tr>
					<tr style="display:none;">
						<td nowrap class="style1" align="right">已收资金统计超过7天：</td>
						<td class="tdbg">
							<asp:Localize ID="Localize_HuanCun" runat="server"></asp:Localize>
						    <asp:label id="Label_Over7Days" runat="server"></asp:label>
						   </td>
					</tr>
					</table>
			</td>
		</tr>
	
		<tr>
			<td colSpan="3" style="text-align: center">
				<asp:Button ID="Button_Clear" runat="server" Text="清空缓存" 
					onclick="Button_Clear_Click" Height="34px" Width="108px" CssClass="b_input" />
			</td>
		</tr>
	</table>

<div class="Loadingdiv" id="Loading"><FONT face="宋体"></FONT></div>
				
</form>
</body>
</HTML>


