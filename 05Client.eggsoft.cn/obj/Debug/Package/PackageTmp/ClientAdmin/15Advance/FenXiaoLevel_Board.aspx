<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FenXiaoLevel_Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.FenXiaoLevel_Board" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BoardGoodClass</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />



</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>分 销 代 理 级 别 管 理 </h1>
            <span style="color: #3333FF; display: none;">替换标识符：###productlist###</span>
            <div class="mselct">
                管理选项：<asp:Button ID="btnAdd" runat="server" Text="添加分销代理级别" OnClick="btnAdd_Click" Style="text-align: center" />
            &nbsp;
                <asp:Label ID="Label_Show" runat="server" Text="Label_Show"></asp:Label>
                <asp:Button ID="btnResetAllGood" runat="server" Text="自动更新所有代理商的商品" OnClick="btnResetAllGood_Click" Style="text-align: center" />
            </div>
        </div>
        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">

                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="15%" CssClass="divVisibleNone"/>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ID" HeaderText="分销级别">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="名称">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            
                            <asp:BoundField DataField="LevelPercent" HeaderText="分销分成">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Sort" HeaderText="排序">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="删除">
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
                    </asp:DropDownList>页 </td>
            </tr>
        </table>
    </form>
    <p>
        模式解读：</p>
    <p>
        商品管理中分别设置市场价、促销价、代理商所得。 
        <br />
        代理商所得会自动按照促销价折换成百分比提供给代理商。如存在上级代理商，会按照 1：3 比例自动折换好。 
        <br />
        假设一件商品的进货价格是 100 元，商家所的利润是 20 元。这时间的市场价是 150 元。那么代理商的利润就是 30 元。商铺的销售收入是 150 元。30 元是需要结算给代理商的，我们系统里面叫代理商或者分销商利润。 那么 30 元是如何在代理商中结算划分那？如果只有仅有一个代理商，代理商全部得到这个 30 元。 如果存在 2 个代理分销商。则 2 个人分这个 30 元。2 个人应该如何划分？我们系统默认按照 1：3 分。举例来说。A B 都是代理商。A 是 B 的上级。 C 到 B 的商铺买了 150 元的商品。那么 A 应该得到 30*1/4，A 得到 7.5 元， B 应该得到 30*3/4，22.5 元（4 是分母，1+3 的意思，1：3 划分嘛）。如果 C 消费后也成为了代理分销商。D 在 C 的代理店铺消费了。那么 B 和 C 分这个 30 元。A 就没有了。发展下级店铺以此类推 。</p>
</body>
</html>