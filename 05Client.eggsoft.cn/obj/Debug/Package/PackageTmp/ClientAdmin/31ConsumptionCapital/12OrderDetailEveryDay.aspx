<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="12OrderDetailEveryDay.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._12OrderDetailEveryDay" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>财富系统订单统计及查询</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 17%;
        }

        .floatLeft {
            left: 10px;
            position: absolute;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>财富系统订单统计及查询
            </h1>
        </div>

        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="text-align: right;">下单用户昵称:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_UserName" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">下单用户ID:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBoxUserID" runat="server" Width="114px"></asp:TextBox></td>

                    <td style="text-align: right;">运营中心ID:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBoxCenterName" runat="server" Width="114px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">查询时间类型(倒序)</td>
                    <td style="text-align: left;">
                        <asp:RadioButtonList ID="QureryTimeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">按订单创建时间</asp:ListItem>
                            <asp:ListItem Value="1">按支付时间</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="text-align: right;">查询开始时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_StartTime%>" maxlength="100" id="TextBox_OrderStartTime"
                            name="TextBox_OrderStartTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />

                    <td style="text-align: right;">查询结束时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_EndTime%>" maxlength="100" id="TextBox_OrderEndTime"
                            name="TextBox_OrderEndTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="LabelNumber" runat="server" Text=""></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>

                </tr>

            </table>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">
                    <span class="floatLeft">页面金额统计： 
                        <asp:Label ID="LabelPageMoney" runat="server" Text=""></asp:Label>/<asp:Label ID="LabelPagesAllMoney" runat="server" Text=""></asp:Label>
                    
                    </span>
                    <br />
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF"
                        CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"
                        CssClass="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="序号">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nickname" HeaderText="下单用户">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tab_OrderCreatTime" HeaderText="订单创建时间">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="GoodPrice" HeaderText="商品价格" DataFormatString="{0:C}">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderCount" HeaderText="购买数量" DataFormatString="{0:D}">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="UserID,ShopUserID" DataTextField="TotalMoney" DataNavigateUrlFormatString="../09System_Status/UserStatus_Money.aspx?userid={0}&ShopUserID={1}" HeaderText="支付金额">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <asp:BoundField DataField="PayDateTime" HeaderText="订单支付时间">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="PaywayOrderNum" HeaderText="支付流水号">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ParentNickName" HeaderText="上级用户">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GrandParentIDNickName" HeaderText="上上级用户">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="GoodTypeID" DataTextField="ParentMasterName" DataNavigateUrlFormatString="../31ConsumptionCapital/12OrderDetailEveryDay.aspx?OperationCenterID={0}" HeaderText="运营中心">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="OperationCenterID" HeaderText="运营中心ID">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <%--  <asp:BoundField HeaderText="运营中心所得">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="GrandParentMasterName" HeaderText="上级运营中心">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ParentOperationCenterID" HeaderText="上级运营中心ID">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="上级运营中心所得">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="Over7DaysToBeans" HeaderText="是否转化">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:CheckBoxField DataField="DeliveryBOOLEAN" HeaderText="是否发货">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                            <%--<asp:CheckBoxField DataField="GoodTypeIdBuyInfo" HeaderText="运营商品ID" Visible="False"></asp:CheckBoxField>--%>

                              <asp:BoundField DataField="OutHadGivedUserNum" HeaderText="出局数">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ID" HeaderText="已发放总额">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="ReturnMoneyUnit" HeaderText="财富余额">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                             <asp:BoundField HeaderText="UserID"  DataField="UserID" >
                                <HeaderStyle Width="5%" CssClass="divVisibleNone" />
                                <ItemStyle HorizontalAlign="Center" CssClass="divVisibleNone" VerticalAlign="Middle" />
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
                    <span class="floatLeft">

                        <asp:LinkButton ID="LinkButton_DownLoad" runat="server" OnClick="lbtnLast_Click_DownLoad">表格全部数据下载</asp:LinkButton>

                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
