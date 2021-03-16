<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="_03WAWapShop_Oliver.Admin.tab_ShopClient.UserManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>商户管理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="mhead">
        <h1>商 户 管 理 </h1>
    </div>
    <form id="Form1" method="post" runat="server">
        <asp:GridView ID="gvUser" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0" Font-Size="12px"
            GridLines="Horizontal" OnRowDataBound="gvUser_RowDataBound" Width="100%" CssClass="mtab" Border="0">
            <Columns>
          
                <asp:BoundField DataField="ID" HeaderText="编号">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>


                <asp:BoundField DataField="UserName" HeaderText="帐号">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
            

                <asp:BoundField DataField="ShopClientName" HeaderText="商户名称">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>

                 <asp:BoundField DataField="PayedAverageMoney_In7Days" HeaderText="7日内客单价">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
                  <asp:BoundField DataField="AllPayedMoney_In7Days" HeaderText="7日总收入">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
                  <asp:BoundField DataField="AllPayedOrder_In7Days" HeaderText="7日支付订单总数">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
              

                <asp:BoundField DataField="AllNotDelivery" HeaderText="未发货订单">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
                <asp:BoundField DataField="AllNotDelivery_In7Days" HeaderText="7日未发货订单">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>

                <asp:BoundField DataField="AllOrder_In7Days" HeaderText="7日所有订单">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>

                <asp:BoundField DataField="sumTotalMoney" HeaderText="7日订单总额">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>


                <asp:BoundField HeaderText="管理">
                    <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>

        <table class="centerAuto" height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td class="centerAuto" width="100%" valign="top" align="center">
                    <asp:Button ID="btnAddAll" runat="server" OnClick="btnAddAll_Click" Text="添加会员" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDeleteAll" runat="server" OnClick="btnDeleteAll_Click" Text="删除选中项" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                    &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                        Width="43px">
                    </asp:DropDownList>页 &nbsp;&nbsp;&nbsp; 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>