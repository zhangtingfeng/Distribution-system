<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentStatus.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._20Agent_Status.AgentStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>代理商管理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        a:link {
            color: blue;
        }

        .centerAuto {
            margin: auto;
            text-align: center;
        }

         .floatRight {
            right:10px;
            position:absolute;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>&nbsp;昨日报表-分销商/代理商统计</h1>

            <div class="mselct">
                <table style="width: 90%;">
                    <tr>
                        <td style="width: 12%; text-align: right;">用户编号:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">店铺名称:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ShopName" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">联系人:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ContactMan" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">昵称:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_NickName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">排序方式:</td>
                        <td style="text-align: left;" colspan="5">
                            <asp:RadioButtonList ID="RadioButtonList_AgentMoney" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="5">所有销量(含下线)</asp:ListItem>
                                <asp:ListItem Value="0">所有分销(含下线)</asp:ListItem>
                                <asp:ListItem Value="1">下线个数</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2">分销商总分销收入</asp:ListItem>
                                <asp:ListItem Value="3">现金余额</asp:ListItem>
                                <asp:ListItem Value="4">购物券余额</asp:ListItem>
                                 <asp:ListItem Value="6">上月销量(含下线)</asp:ListItem>
                                 <asp:ListItem Value="7">本月销量(含下线)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                        <td>
                            <asp:RadioButtonList ID="RadioButtonList__AgentMoney_OrderBy" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">升序</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">降序</asp:ListItem>
                            </asp:RadioButtonList></td>

                        <td style="text-align: left;">
                            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>
                    </tr>

                </table>
            </div>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center" style="text-align: center;">

                    <font face="宋体">
                        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="1px" BorderColor="DarkGray" CellSpacing="1" CellPadding="5"
                            Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                            <HeaderStyle BackColor="#EAF7FF" Height="40" />
                            <AlternatingRowStyle BackColor="ActiveBorder" />
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="用户编号">
                                    <HeaderStyle Width="5%" CssClass="divVisibleNone" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" Height="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopUserID" HeaderText="用户编号">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" Height="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopName" HeaderText="店铺名称">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="UserRealName" HeaderText="联系人">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NickName" HeaderText="昵称">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ContactPhone" HeaderText="联系电话">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>




                                <asp:BoundField DataField="AlipayNumOrWeiXinPay" HeaderText="支付账号">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="UpdateTime" HeaderText="更新日期">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>


                                <asp:BoundField DataField="AgentLevelSelect" HeaderText="代理级别">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="../09System_Status/UserStatus_AllSales.aspx?userid={0}" DataTextField="AllSalesMoney_my_AND_myAllSon" HeaderText="总销量￥">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="AllFenXiaoMoney_my_AND_myAllSon" HeaderText="总分销收入">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="myAgentSonSum" HeaderText="下线个数">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>


                                <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="../09System_Status/UserStatus_FenXiao.aspx?userid={0}" DataTextField="AllFenXiaoMoney" HeaderText="会员分销收入￥">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:HyperLinkField>

                                <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="../09System_Status/UserStatus_Money.aspx?userid={0}" DataTextField="RemainingSum" HeaderText="微店现金￥">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="../09System_Status/UserStatus_Quan.aspx?userid={0}" DataTextField="RemainingSum_Vouchers" HeaderText="购物券￥">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:HyperLinkField>

                                <asp:BoundField DataField="ParentID" HeaderText="上级代理商">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="LastMonthSales_my_AND_myAllSon" HeaderText="上月销量">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="ThisMonth_SalesMoney_my_AND_myAllSon" HeaderText="本月销量">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                        <br />

                        &nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                        &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                            Width="43px">
                        </asp:DropDownList>页</font>
                    <asp:LinkButton ID="LinkButton_DownLoad" CssClass="floatRight" runat="server" OnClick="lbtnLast_Click_DownLoad">表格下载</asp:LinkButton>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>