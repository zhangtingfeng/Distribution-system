<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="18OperationWtiteOrder.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._18OperationWtiteOrder" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>运营中心录单系统</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 16%;
        }

        .auto-style2 {
            height: 37px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>运 营 中 心 录 单 管 理
            </h1>
        </div>

        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="width: 16%; text-align: right;">处理状态:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:CheckBox ID="CheckBox_IfDone" runat="server" Text="是否处理过" Checked="false" />
                    </td>

                    <td style="width: 16%; text-align: right;">用户ID:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_userShopID" runat="server"></asp:TextBox></td>

                    <td style="width: 16%; text-align: right;">运营中心编号:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_OperationNumber" runat="server"></asp:TextBox></td>

                </tr>
                <tr>
                    <td style="text-align: right;" class="auto-style2"></td>
                    <td style="text-align: left;" class="auto-style2"></td>
                    <td style="text-align: right;" class="auto-style2"></td>
                    <td style="text-align: left;" class="auto-style2"></td>
                </tr>

                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" />
                    </td>

                </tr>

            </table>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF"
                        CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"
                        CssClass="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="序号">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="运营中心编号" DataField="OperationCenterID">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                              <asp:BoundField HeaderText="运营中心名称" DataField="OperationCenterID">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="运营中心联系电话" DataField="OperationCenterTel">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="下单用户ID" DataField="BuyOrderShopUserID">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="姓名" DataField="BuyOrderShopUserIDRealName">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="身份证" DataField="BuyOrderShopUserIDIDCard">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="联系电话" DataField="BuyOrderShopUserIDContactPhone">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上级用户ID" DataField="BuyParentShopUserID">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="支付流水号" DataField="PaySerialNumber">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="支付时间" DataField="OrderPayTime">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:BoundField HeaderText="处理状态" DataField="FeedbackStatus">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="最近更新时间" DataField="UpdateTime">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="管理">
                                <HeaderStyle Width="7%" />
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
                    </asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
