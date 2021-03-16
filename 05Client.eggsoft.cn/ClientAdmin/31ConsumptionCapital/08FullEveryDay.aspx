<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08FullEveryDay.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._08FullEveryDay" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>消费财富系统</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 16%;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>每日运营统计
            </h1>
        </div>

        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="text-align: right;">订单28%:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_ThisDayMoneyAuto" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_ThisDayMoneyAuto" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">当日手动分红金额:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownListThisDayMoneyByBoss" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBoxThisDayMoneyByBoss" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">开始时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_StartTime%>" maxlength="100" id="TextBox_StartTime"
                            name="TextBox_OrderStartTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                        <%--<asp:TextBox ID="TextBox_OrderCreatTime" runat="server" Width="114px"></asp:TextBox>--%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td style="text-align: right;">结束时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_EndTime%>" maxlength="100" id="TextBox_EndTime"
                            name="TextBox_OrderEndTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                        <%--<asp:TextBox ID="TextBox_OrderCreatTime" runat="server" Width="114px"></asp:TextBox>--%></td>

                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>

                </tr>

                <tr>
                    <td colspan="6">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
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
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ThisDay" HeaderText="当日">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="ThisDay" DataTextField="ThisDayReturnActual" DataNavigateUrlFormatString="06ThisDayReturnActualUser.aspx?AskThisDay={0}" HeaderText="当日实际分红金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <asp:BoundField DataField="ThisDayAllActiveOrder" HeaderText="当日分红订单数">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="EveryOrderGet" HeaderText="每单位订单">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="当日应分红金额" DataField="ThisDayMoneyByBoss">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="当日订单28%金额" DataField="ThisDayMoneyAuto">
                                <HeaderStyle Width="8%" />
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
