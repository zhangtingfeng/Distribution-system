<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06ThisDayReturnActualUser.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._06ThisDayReturnActualUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户统计</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr class="title">
                <th height="35">当日实际分红统计情况统计 </th>
            </tr>
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">

                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserID" HeaderText="发放用户">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ShopUserID" HeaderText="用户ID">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="PayDateTime" HeaderText="支付时间">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="OrderCount" HeaderText="购买数量">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReturnMoneyUnit" HeaderText="订单未发放总额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                          <%--  <asp:BoundField DataField="sumConsumeOrRechargeWealth" HeaderText="订单已发放总额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>

                           <%--   <asp:HyperLinkField  DataNavigateUrlFields ="userid,OrderDetailID" HeaderText="订单已发放总额" DataTextField="OrderDetailID" DataNavigateUrlFormatString="20Order_FromWealthToMoney.aspx?userid={0}&OrderDetailID={1}">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>--%>
                               <asp:BoundField DataField="OrderDetailID" HeaderText="订单已发放总额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>



                            <asp:BoundField DataField="OrderDetailID" HeaderText="两者之和">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>



                            <asp:BoundField DataField="CreatTime" HeaderText="发放时间">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <%--     <asp:BoundField DataField="ConsumeOrRechargeWealth" HeaderText="发放金额" DataFormatString="{0:C}">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:HyperLinkField DataNavigateUrlFields="UserID" HeaderText="发放金额" DataTextField="ConsumeOrRechargeWealth" DataNavigateUrlFormatString="../09System_Status/UserStatus_Money.aspx?userid={0}">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>



                            <asp:BoundField DataField="ConsumeTypeOrRecharge" HeaderText="描述">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <%--  <asp:BoundField DataField="RemainingSum" HeaderText="财富余额" DataFormatString="{0:C}">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:HyperLinkField DataNavigateUrlFields="UserID" HeaderText="用户财富余额" DataTextField="RemainingSum" DataNavigateUrlFormatString="07User_WealthStatus.aspx?userid={0}">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>



                            <asp:CheckBoxField DataField="Bool_ConsumeOrRecharge" HeaderText="充值或消费">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>



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
            
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
