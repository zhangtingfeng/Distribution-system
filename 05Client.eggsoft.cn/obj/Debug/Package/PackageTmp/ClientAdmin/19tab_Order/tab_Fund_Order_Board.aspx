<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Fund_Order_Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._19tab_Order.tab_Fund_Order_Board" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>结算信息管理</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr class="title">
                <th align="center" valign="top" class="style1">
                    <strong>结算信息管理</strong>
                </th>
            </tr>
        </table>
        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="width: 16%; text-align: right;">下单用户ID:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 16%; text-align: right;">下单用户姓名:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_UserInfo" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">订单号:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_OrderNum" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: right;">实际支付金额:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_PayPrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_PayPrice" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">商品名称:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_GoodName" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">订单开始时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_OrderStartTime%>" maxlength="100" id="TextBox_OrderStartTime"
                            name="TextBox_OrderStartTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                        <%--<asp:TextBox ID="TextBox_OrderCreatTime" runat="server" Width="114px"></asp:TextBox>--%></td>

                </tr>

                <tr>
                    <td style="text-align: right;">商品总金额:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_GoodAllPrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_GoodAllPrice" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">订单商品数量:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_AllGoodsCount" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_AllGoodsCount" runat="server" Width="114px"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">订单结束时间:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <input
                            type="text" value="<%=strTextBox_OrderEndTime%>" maxlength="100" id="TextBox_OrderEndTime"
                            name="TextBox_OrderEndTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                        <%--<asp:TextBox ID="TextBox_OrderCreatTime" runat="server" Width="114px"></asp:TextBox>--%></td>
                </tr>

                <tr>
                    <td style="text-align: right;">分销代理商:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_AgentShow" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">运费:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_Freight" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_Freight" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">门店信息:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:TextBox ID="TextBox_TakeGoodInfo" runat="server"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td style="width: 16%; text-align: right;">收货地址:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_UserAddress" runat="server"></asp:TextBox></td>
                    <td style="width: 16%; text-align: right;">收货电话:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:TextBox ID="TextBox_Tel" runat="server"></asp:TextBox></td>

                    <td>&nbsp;</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>

                </tr>

            </table>
        </div>

        <asp:DataList ID="DataList1" runat="server" CellSpacing="0" CellPadding="0" Width="100%"
            OnItemDataBound="DataList1_ItemDataBound"
            HorizontalAlign="Center">
            <HeaderTemplate>
                <table id="DataList2" width="100%" border="0" cellpadding="0" cellspacing="0" class="border mtab">
                    <tr bgcolor="#0000CC" class="title" style="text-align: center;">
                        <th width="14%" height="25" align="center">
                            <strong>订单号</strong></th>
                        <th width="8%" height="25" align="center">
                            <strong>支付状态</strong></th>
                        <th width="8%" height="25" align="center">
                            <strong>客户</strong></th>
                        <th width="15%" height="25" align="center">
                            <strong>订单时间</strong></th>
                        <th width="15%" height="25" align="center">
                            <strong>发生金额</strong></th>
                        <th width="15%" height="25" align="center">
                            <strong>商家所得</strong></th>
                        <th width="15%" height="25" align="center">
                            <strong>操作选项</strong></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr bgcolor="#FFFFFF" class="tdbg">
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eval("OrderNum")%>
                        <asp:HiddenField ID="Order_ID" runat="server" Value='<%#Eval("ID") %>' />

                    </td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#getPayStatus(Eval("PayStatus").ToString())%></td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eggsoft_Public_CL.Pub.GetNickName(Eval("UserID").ToString())%> 编号ID:<%#Eval("ShopUserID").ToString()%></td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eval("CreateDateTime")%></td>
                    <td height="40" width="15%" class="styleOrder">¥<%#Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Eval("TotalMoney").ToString()))%></td>
                    <td height="40" width="15%" class="styleOrder">¥<%#Eggsoft_Public_CL.Pub.getPubMoney(Eggsoft_Public_CL.GoodP.GetThisOrderShopGet(Int32.Parse(Eval("ID").ToString())))%></td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#showOperate(Eval("ID").ToString())%>
                    </td>
                </tr>
                <tr bgcolor="#eeeeee" width="100%">
                    <td colspan="7" width="100%">
                        <asp:DataList ID='OrderDatail' EnableViewState='false' runat='server' CssClass="style100Percent" Width="100%">
                            <HeaderTemplate>
                                <table width="90%" border="0" style="float: right; margin-bottom: 30px;" cellpadding="0" cellspacing="0" class="border mtab">

                                    <%--<table width="100%" border="0" cellpadding="0" cellspacing="0" class="border mtab">--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="<%#getColor(Eval("ID").ToString())%>">
                                    <td height="22" width="10%">
                                        <a href="#" title="产品短号<%#Eval("GoodID")%>"><%#Eval("GoodName")%></a>
                                        <%#Eggsoft_Public_CL.Pub.getPubTuanGouDescToAdministrator(Eval("GoodType").ToString(),Eval("GoodTypeId").ToString(),Eval("GoodTypeIdBuyInfo").ToString(),Eval("ID").ToString())%> 
                                   (详单ID:<%#(Eval("ID").ToString())%>)

                                    </td>
                                    <%--<td height="22" width="10%">
                                        <a href="#" title="产品短号<%#Eval("GoodID")%>"><%#Eval("GoodName")%></a>
                                    </td>--%>
                                    <td height="22" width="10%">¥
                                    <%#Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Eval("GoodPrice").ToString()))%>                                       
                                    </td>
                                    <td height="22" width="10%">
                                        <%#Eval("OrderCount").ToString()%>
                                    </td>
                                    <td height="22" width="10%">
                                        <a href="/ClientAdmin/09System_Status/UserStatus_Money.aspx?userid=<%#Eval("UserID").ToString()%>" target="_blank" title="现金使用代账" <%#((Convert.ToDecimal(Eval("MoneyCredits").ToString())>0)?"":"style='display:none'")%>><%#Eval("MoneyCredits").ToString()%></a>
                                        <a href="/ClientAdmin/09System_Status/UserStatus_Quan.aspx?userid=<%#Eval("UserID").ToString()%>" target="_blank" title="购物积分使用代账" <%# Eggsoft.Common.ObjectExtended.toDecimal(Eval("MoneyWeBuy8Credits"))>0?"":"style='display:none'"%>><%#Eval("MoneyWeBuy8Credits").ToString()%></a>
                                        <a href="#" title="购物券使用代账"><%#Eval("VouchersNum_List").ToString()%></a>
                                        <a href="/ClientAdmin/31ConsumptionCapital/07User_WealthStatus.aspx?userid=<%#Eval("UserID").ToString()%>" target="_blank" title="财富积分使用代账" <%# Eggsoft.Common.ObjectExtended.toDecimal(Eval("WealthMoney"))>0?"":"style='display:none'"%>><%#Eval("WealthMoney").ToString()%></a>
                                    </td>
                                    <td height="22" width="10%">
                                        <a href="#" title="总额"><%#Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Eval("GoodPrice").ToString())*Int32.Parse(Eval("OrderCount").ToString()))%></a>
                                    </td>
                                    <td height="22" width="10%">
                                        <a href="#" title="运费"><%#Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Eval("Freight").ToString()==""?"0":Eval("Freight").ToString()))%></a>
                                    </td>
                                    <td height="22" width="10%">
                                        <%#Eval("FreightShowText").ToString()%>
                                    </td>
                                    <td height="22" width="10%">上级:<%#Eggsoft_Public_CL.Pub.GetNickName(Eval("ParentID").ToString())%><br />
                                        上上级:<%#Eggsoft_Public_CL.Pub.GetNickName(Eval("GrandParentID").ToString())%><br />
                                        上上上级:<%#Eggsoft_Public_CL.Pub.GetNickName(Eval("GreatParentID").ToString())%></td>
                                    <td width="20%"><%#showFenXiaoList(Eval("ID").ToString())%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:DataList>
                    </td>
                </tr>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>
        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Never"
            EnableUrlRewriting="true" UrlRewritePattern=""
            OnPageChanged="AspNetPager1_PageChanged" NumericButtonTextFormatString="{0}"
            CurrentPageButtonClass="page-num-Current"
            MoreButtonsClass="page-num" PagingButtonsClass="page-num"
            ShowPrevNext="False" NumericButtonCount="10" PageSize="20"
            ShowFirstLast="False"
            HorizontalAlign="Center">
        </webdiyer:AspNetPager>








    </form>
</body>
</html>
