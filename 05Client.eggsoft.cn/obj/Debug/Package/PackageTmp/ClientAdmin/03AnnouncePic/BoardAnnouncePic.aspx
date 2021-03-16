<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardAnnouncePic.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._03AnnouncePic.BoardAnnouncePic" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>AnnounceManage</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 	
</head>
<body>
<form id="Form1" method="post" runat="server">

<div class="mhead">
<h1>  轮 播 管  理 </h1><span style="color: #3333FF; display:none;">替换标识符：###announcepic###</span>
                   
<div class="mselct"> 管理选项：<asp:Button 
			ID="btnAdd" runat="server" Text="添加轮播" OnClick="btnAdd_Click" />
    </div>
</div>
<table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
<tr>
	<td valign="top" align="center">  
			<asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"  class="mtab"> 
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="编号">
					<HeaderStyle Width="8%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="centerAuto" />
					</asp:BoundField>

                    <asp:BoundField DataField="Pos" HeaderText="排序位置">
					<HeaderStyle Width="10%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="centerAuto" />
					</asp:BoundField>

					<asp:BoundField DataField="ShowText" HeaderText="标题">
					<HeaderStyle Width="30%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="centerAuto" />
					</asp:BoundField>

					<asp:BoundField DataField="PicUrl" HeaderText="两张图片要大小一致，否则会出现黑框">
					<HeaderStyle Width="20%" />
					<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="centerAuto" />
					</asp:BoundField>


					<asp:BoundField HeaderText="修改">
					<HeaderStyle Width="10%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="centerAuto" />
					</asp:BoundField>

					<asp:BoundField HeaderText="删除">
					<HeaderStyle Width="10%" />
						<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto"  />
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