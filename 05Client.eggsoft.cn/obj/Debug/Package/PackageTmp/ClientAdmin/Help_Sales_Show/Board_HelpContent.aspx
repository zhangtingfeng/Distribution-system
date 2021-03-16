<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_HelpContent.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin.Help_Sales_Show.Board_HelpContent" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>卖 家 帮 助 文 档</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />   

</head>
<body>
<form id="Form1" method="post" runat="server">
<div class="mhead">
<h1>卖 家 帮 助 文 档 </h1> 
</div>
<table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
	<tr>
		<td Width="100%" valign="top" align="center"> 
				<asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" 
                    BorderColor="#EFEFEF" CellPadding="0"
					Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
				 
					<Columns>
                    
						<asp:BoundField DataField="ID" HeaderText="编号">
						<HeaderStyle Width="15%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
						</asp:BoundField>


						<asp:BoundField DataField="Name" HeaderText="名称">
						<HeaderStyle Width="15%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
						</asp:BoundField>
                        					

						  <asp:BoundField DataField="Help_Class1_ID" HeaderText="帮助分类">
						<HeaderStyle Width="15%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
						</asp:BoundField>		

						<asp:BoundField DataField="Sort" HeaderText="排序">
						<HeaderStyle Width="15%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
						</asp:BoundField>			
					</Columns>
				</asp:GridView>
				&nbsp;
				<br />
				<asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
				&nbsp;&nbsp;
				<asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
				<asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
				<asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
				<asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
				&nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
					Width="43px">
				</asp:DropDownList>页 </td></tr></table>
</form>
</body>
</html>
