<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Order_Board_WaitGiveGoods.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._19tab_Order.tab_Order_Board_WaitGiveGoods" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>订单管理</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>
    <%--  <script src="../../Scripts/layer-v3.0.3/layer.js" type="text/javascript"></script>
    <link href="../../Styles/layer-v3.0.3skin/layer.css" rel="stylesheet" type="text/css"/>--%>
    <%--    <script src="../../Test/layer-v3.0.3/layer/layer.js"></script>--%>
</head>
<body id="mybody">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr class="title">
                <th align="center" valign="top" class="style1">
                    <strong>待 发 货 订 单 管 理</strong>
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
                            class="calendarFocus l_input" readonly="true" style="cursor: pointer" />
                    </td>
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


                    <td></td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" />
                    </td>

                </tr>

            </table>
        </div>

        <asp:DataList ID="DataList1" runat="server" CellSpacing="0" CellPadding="0" Width="100%"
            OnItemDataBound="DataList1_ItemDataBound" HorizontalAlign="Center">
            <HeaderTemplate>
                <table id="DataList2" width="100%" border="0" cellpadding="0" cellspacing="0" class="border mtab">
                    <tr bgcolor="#0000CC" class="title" style="text-align: center;">
                        <th width="14%" height="25" align="center">
                            <strong>订单号</strong>
                        </th>
                        <th width="8%" height="25" align="center">
                            <strong>用户信息</strong>
                        </th>
                        <th width="8%" height="25" align="center">
                            <strong>用户地址</strong>
                        </th>
                        <th width="15%" height="25" align="center">
                            <strong>订单时间</strong>
                        </th>
                        <th width="15%" height="25" align="center">
                            <strong>订单金额</strong>
                        </th>
                        <th width="15%" height="25" align="center">
                            <strong>支付类型</strong>
                        </th>
                        <th width="15%" height="25" align="center">
                            <strong>发货管理</strong>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr bgcolor="#FFFFFF" class="tdbg">
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eval("OrderNum")%>
                        <asp:HiddenField ID="Order_ID" runat="server" Value='<%#Eval("ID") %>' />
                    </td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#getUserName(Eval("UserID").ToString(),Eval("ShopClient_ID").ToString())%> 编号ID:<%#Eval("ShopUserID").ToString()%>
                    </td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eggsoft_Public_CL.GoodP.getAddress(Eval("User_Address").ToString(),Eval("O2OTakedID").ToString())%>
                    </td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#Eval("CreateDateTime")%>
                    </td>
                    <td height="40" width="15%" class="styleOrder">¥<%#(Eval("TotalMoney").ToString())%></td>
                    <td height="40" width="15%" class="styleOrder">
                        <%#(Eggsoft_Public_CL.Pub.gePayChineseName(Eval("PayWay").ToString()))%>
                    </td>
                    <td height="40" width="15%" class="styleOrder">
                        <a href="javascript:void(0)" title="<%#getGetFaHuoXML(Eval("ID").ToString())%>" id="showFaHuoFloat<%#Eval("ID")%>"
                            onclick="showFaHuoFloat(<%#Eval("ID")%>)">输入发货情况</a><br />
                        <a href="javascript:void(0)"
                            id="TuiKuanFloat<%#Eval("ID")%>" onclick="CancelThis_TuiKuan(<%#Eval("ID")%>,<%#Eval("TotalMoney")%>)">取消订单(退款)</a>
                    </td>
                </tr>
                <tr bgcolor="#eeeeee" width="100%">
                    <td colspan="7" width="100%">
                        <asp:DataList ID='OrderDatail' EnableViewState='false' runat='server' CssClass="style100Percent"
                            OnItemDataBound="DataList2_ItemDataBound" Width="100%">
                            <HeaderTemplate>
                                <table width="90%" border="0" style="float: right; margin-bottom: 30px;" cellpadding="0" cellspacing="0" class="border mtab">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="<%#getColor(Eval("ID").ToString())%>">
                                    <td height="22" width="10%">
                                        <a href="#" title="产品短号<%#Eval("GoodID")%>"><%#Eval("GoodName")%></a>
                                        <%#Eggsoft_Public_CL.Pub.getPubTuanGouDescToAdministrator(Eval("GoodType").ToString(),Eval("GoodTypeId").ToString(),Eval("GoodTypeIdBuyInfo").ToString(),Eval("ID").ToString())%> 
                                       (详单ID:<%#(Eval("ID").ToString())%>)
                                    </td>
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
            EnableUrlRewriting="true" UrlRewritePattern="" OnPageChanged="AspNetPager1_PageChanged"
            NumericButtonTextFormatString="{0}" CurrentPageButtonClass="page-num-Current"
            MoreButtonsClass="page-num" PagingButtonsClass="page-num" ShowPrevNext="False"
            NumericButtonCount="10" PageSize="20" ShowFirstLast="False" HorizontalAlign="Center">
        </webdiyer:AspNetPager>
        <script type="text/javascript" language="javascript">
            function ShowMoneyNo()                        //隐藏两个层 
            {
                document.getElementById("divModifyMoney").style.display = "none";
            }

            function Show_PayStatus_No()                        //隐藏两个层 
            {
                document.getElementById("div_PayStatus").style.display = "none";
            }

            function Show_FaHuo_No()                        //隐藏两个层 
            {
                document.getElementById("div_FaHuo").style.display = "none";
            }

            function $Me(id) {
                return (document.getElementById) ? document.getElementById(id) : document.all[id];
            }




            function CancelThis_TuiKuan(intOrderNum, varTotalMoney) {

                var refundMoney = prompt("请输入在线支付订单的退款金额,单位为元.输入0表示不在线退款", varTotalMoney)
                if (refundMoney != null && refundMoney != "") {
                    if (window.confirm('退款金额为' + refundMoney + '元。你确定要取消该订单吗？')) {
                        window.location.href = "?type=CancelThis&OrderINT=" + intOrderNum + "&ReturnMoney=" + refundMoney;
                    } else {
                    }
                }

                //if (window.confirm('你确定要取消该订单吗？')) {
                //    window.location.href = "?type=CancelThis&OrderINT=" + intOrderNum;
                //} else {
                //}
            }




            function showFaHuoFloat(orderID)                    //根据屏幕的大小显示两个层 
            {
                $("#TextBox_OrderID").val(orderID);
                var varshowFaHuoFloat = "#showFaHuoFloat" + orderID;
                var varTitle = $(varshowFaHuoFloat).attr("title"); //借用showFloat的
                if (varTitle != null) {
                    myArray = varTitle.split("#");

                    $("#TextBox_Which").val(myArray[0]);
                    $("#TextBox_Num").val(myArray[1]);
                    $("#TextBox_ToName").val(myArray[2]);
                    $("#TextBox_ToPhone").val(myArray[3]);
                    $("#TextBox_ToAddress").val(myArray[4]);
                    $("#TextBox_FromName").val(myArray[5]);
                    $("#TextBox_FromPhone").val(myArray[6]);
                    $("#TextBox_FromAddres").val(myArray[7]);

                }


                document.getElementById("div_FaHuo").style.display = "";
            }


            function getRange()                      //得到屏幕的大小 
            {
                var top = document.body.scrollTop;
                var left = document.body.scrollLeft;
                var height = document.body.clientHeight;
                var width = document.body.clientWidth;

                if (top == 0 && left == 0 && height == 0 && width == 0) {
                    top = document.documentElement.scrollTop;
                    left = document.documentElement.scrollLeft;
                    height = document.documentElement.clientHeight;
                    width = document.documentElement.clientWidth;
                }
                return { top: top, left: left, height: height, width: width };
            }
        </script>
        <%--<a href="javascript:void(0)" onclick="showFloat(666)">登陆 </a>    //登陆链接 
        --%>
        <!--加一个半透明层-->
        <%--    <div id="doing" style="filter:alpha(opacity=30);-moz-opacity:0.3;opacity:0.3;background-color:#000;width:100%;height:100%;z-index:1000;position: absolute;left:0;top:0;display:none;overflow: hidden;"> 
</div>  --%>
        <!--加一个登录层-->
        <div id="div_FaHuo" style="border: solid 10px #898989; background: #fff; padding: 10px; width: 60%; z-index: 1001; position: absolute; display: none; top: 50%; text-align: center; left: 50%; margin: -200px 0 0 -400px;">
            <br />
            <br />
            <table width="80%" border="0" style="border: 0px solid #898989; margin: auto;">
                <tr>
                    <td style="width: 30%;">快递公司：
                    
                                    
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_OrderID" CssClass="styleVisibleFalse l_input" runat="server"> </asp:TextBox>
                        <%--<asp:TextBox ID="TextBox_Which" runat="server" CssClass="l_input"> </asp:TextBox>--%>
                        <asp:DropDownList ID="DropDownList_WhichCompany" CssClass="l_input" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;">发货单号：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_Num" runat="server" CssClass="l_input" placeholder="必填 至少输入8位"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;">收货人姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_ToName" runat="server" CssClass="l_input" placeholder="必填"> </asp:TextBox>
                    </td>
                </tr>
                <tr class="styleVisibleFalse">
                    <td style="width: 30%;">收货人电话：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_ToPhone" runat="server" CssClass="l_input styleVisibleFalse"> </asp:TextBox>
                    </td>
                </tr>
                <tr class="styleVisibleFalse">
                    <td style="width: 30%;">收货人地址：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_ToAddress" runat="server" CssClass="l_input styleVisibleFalse"> </asp:TextBox>
                    </td>
                </tr>
                <tr class="styleVisibleFalse">
                    <td style="width: 30%;">发货人姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_FromName" runat="server" CssClass="l_input styleVisibleFalse"> </asp:TextBox>
                    </td>
                </tr>
                <tr class="styleVisibleFalse">
                    <td style="width: 30%;">发货人电话：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_FromPhone" runat="server" CssClass="l_input styleVisibleFalse"> </asp:TextBox>
                    </td>
                </tr>
                <tr class="styleVisibleFalse">
                    <td style="width: 30%;">发货人地址：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_FromAddres" runat="server" CssClass="l_input styleVisibleFalse"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;"></td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="Button_FaHuo" runat="server" Text=" 确 定 " OnClick="Button_FaHuo_Click"
                            CssClass="b_input" />&nbsp; &nbsp; &nbsp;
                    <input id="ButtonFaHuo" type="button" value=" 取 消 " onclick="Show_FaHuo_No()" cssclass="b_reset" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </form>
</body>
</html>
