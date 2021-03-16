<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_O2O_Shop.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._17O2O_Shop.Board_O2O_Shop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mhead">
            <h1>o2o门店管理</h1>
        </div>
        <div class="mselct">
            <asp:Button ID="btnAdd" runat="server" Text="添加o2o门店管理" OnClick="btnAdd_Click" />
        </div>
        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0"
            Width="100%" Font-Size="12px" CssClass="mtab" Border="0" OnRowDataBound="gvAnnounce_RowDataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField HeaderText="店铺名称" DataField="ShopName">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField HeaderText="联系人" DataField="ContactMan">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="联系电话" DataField="Tel">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="修改时间" DataField="UpdateTime">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="经度" DataField="BaiDulng">
                    <HeaderStyle Width="4%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="纬度" DataField="BaiDulat">
                    <HeaderStyle Width="4%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="省份" DataField="AdddressProvince">
                    <HeaderStyle Width="4%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="市" DataField="AdddressCity">
                    <HeaderStyle Width="4%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="县" DataField="AdddressCountry">
                    <HeaderStyle Width="4%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="联系地址" DataField="ShopAdress">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="修改">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>

                <asp:BoundField HeaderText="删除">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <table height="100%" cellspacing="0" style="text-align: center;" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="left" style="text-align: center;">
                    <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;
					<asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                    &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                        Width="43px">
                    </asp:DropDownList>页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>