<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsVisit.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._09System_Status.GoodsVisit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>商品访问统计</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />    
</head>
<body>
<form id="form1" runat="server">
<table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
<tr class="title">
<th height="35"> 商品访问统计 </th>
</tr>
<tr>
	<td Width="100%" valign="top" align="center" style=" text-align:center;"> 
			<asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0"
				Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
			 
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="编号">
					<HeaderStyle Width="8%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>

					 <asp:BoundField DataField="GoodID" HeaderText="商品编号">
					<HeaderStyle Width="8%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>
					
					<asp:BoundField DataField="Name" HeaderText="商品名称">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>


					<asp:BoundField DataField="userID" HeaderText="访问人">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>

					  <asp:BoundField DataField="Parent_UserID" HeaderText="分享人">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>
                                      

					 <asp:BoundField DataField="Count_Visit" HeaderText="访问次数">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>

					<asp:BoundField DataField="UpdateTime" HeaderText="最后访问时间">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>


				 <%--   <asp:BoundField HeaderText="修改">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>

					<asp:BoundField HeaderText="删除">
					<HeaderStyle Width="10%" />
				   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
					</asp:BoundField>--%>
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
			</asp:DropDownList>页 
            
            </td></tr></table>
</form>
</body>
</html>