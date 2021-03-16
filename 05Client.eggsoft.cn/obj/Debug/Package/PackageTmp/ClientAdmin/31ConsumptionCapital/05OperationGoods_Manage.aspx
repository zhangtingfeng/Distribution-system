<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05OperationGoods_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._05OperationGoods_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>运 营 中 心 管 理</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25"><strong>管 理 消 费 商 品 </strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                <div align="right"><strong>选择运营商品：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                <div align="left" style="margin-right: 100px;">
                                    <asp:DropDownList ID="DropDownList1GoodID" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>商品运营状态：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBox_RunningStatus" runat="server" Text="是否没有下架。商品运营状态。不选表示消费购买暂停，但是不影响提现申请" Checked="True" />
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>财富返还计划：</strong></td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_Return_MoneyConsumerWeighting" runat="server" Text="消费者加权平均直接返还%："></asp:Label>
                                <asp:TextBox ID="Textbox_MoneyConsumerWeighting" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_MoneyConsumerWeighting"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="消费者加权平均直接返还不能为空!"
                                    ControlToValidate="Textbox_MoneyConsumerWeighting" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <span>（企业利润的X%作为消费者消费投资回报给消费者）</span><br />
                                <asp:Label ID="Label_ReturnMoneyShareA" runat="server" Text="分享者A（间接推）%："></asp:Label>
                                <asp:TextBox ID="Textbox1_ReturnMoneyShareA" runat="server" Width="100px" CssClass="l_input">5</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox1_ReturnMoneyShareA"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="分享者A（间接推）不能为空!"
                                    ControlToValidate="Textbox1_ReturnMoneyShareA" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <span lang="EN-US">(</span><span>X%广告费）</span><br />
                                <asp:Label ID="Label_ReturnMoneyShareB" runat="server" Text="分享者B（直接推）%："></asp:Label>
                                <asp:TextBox ID="Textbox2_ReturnMoneyShareB" runat="server" Width="100px" CssClass="l_input">15</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox2_ReturnMoneyShareB"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="分享者B（直接推）不能为空!"
                                    ControlToValidate="Textbox2_ReturnMoneyShareB" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <span lang="EN-US">(</span><span>X%广告费）</span><br />
                                <asp:Label ID="Label_ReturnMoneyOperationShareA" runat="server" Text="运营中心A（间接推）%："></asp:Label>
                                <asp:TextBox ID="Textbox3_ReturnMoneyOperationShareA" runat="server" Width="100px" CssClass="l_input">3</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox3_ReturnMoneyOperationShareA"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="运营中心A（间接推）不能为空!"
                                    ControlToValidate="Textbox3_ReturnMoneyOperationShareA" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <span>（X%人工费）</span><br />
                                <asp:Label ID="Label_ReturnMoneyOperationShareB" runat="server" Text="运营中心B（直接推）%："></asp:Label>
                                <asp:TextBox ID="Textbox4_ReturnMoneyOperationShareB" runat="server" Width="100px" CssClass="l_input">10</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox4_ReturnMoneyOperationShareB"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="运营中心B（只接推）不能为空!"
                                    ControlToValidate="Textbox4_ReturnMoneyOperationShareB" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <span>（X%人工费）</span><br />
                                <asp:Label ID="Label5_ReturnMoneyToCompany" runat="server" Text="公司成本%："></asp:Label>
                                <asp:TextBox ID="Textbox5_ReturnMoneyToCompany" runat="server" Width="100px" CssClass="l_input">67</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox5_ReturnMoneyToCompany"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="公司成本不能为空!"
                                    ControlToValidate="Textbox5_ReturnMoneyToCompany" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                （<span>产品成本、运营成本、税金、物流费、系统维护费</span>）<br />


                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>消费者财富返还计划：</strong></td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="Textbox_Price_ReturnConsumerWealth" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>
                                。<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="格式整数"
                                    ValidationExpression="^\d+$" ControlToValidate="Textbox_Price_ReturnConsumerWealth"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price0" runat="server" ErrorMessage="消费者财富返还计划不能为空!"
                                    ControlToValidate="Textbox_Price_ReturnConsumerWealth" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                消费者下单购买。按照购买金额进行财富返还。消费者的财富中心直接增加多少倍的财富基金。按照企业返还给消费者的回报，每日加权分红</td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>财富返还方式（每天如何归还钱）：</strong></td>
                            <td width="80%" height="35" bgcolor="#ecf5ff">
                                <asp:Literal ID="Literal1" runat="server" Text="股东类"></asp:Literal>
                                <asp:TextBox ID="Textbox_TextboxMoneyConsumerAllOrderA" runat="server" CssClass="l_input" Width="100px">-1</asp:TextBox>
                                <span class="auto-style1">*</span> <span style="color: rgb(96, 96, 96); font-family: 微软雅黑, 'Microsoft YaHei'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(236, 245, 255);">-1表示明天不分红，0表示采用实际收入分红，&gt;0表示采用本数据分红。 参与分红的每日商城订单总数。如果这里的值大于0，则消费者加权平均直接返还将失效</span><asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="运营中心默认每日商城总销量不能为空!"
                                    ControlToValidate="Textbox_TextboxMoneyConsumerAllOrderA" ForeColor="#FF3300"></asp:RequiredFieldValidator><br />
                                <asp:Literal ID="Literal3ConsumerAllOrderA" runat="server"></asp:Literal>
                               <hr style=" height:2px;border:none;border-top:2px dotted grey;" />
                                <asp:Literal ID="Literal2" runat="server" Text="非股东类"></asp:Literal>
                                <asp:TextBox ID="Textbox_TextboxMoneyConsumerAllOrderB" runat="server" CssClass="l_input" Width="100px">-1</asp:TextBox>
                                <span class="auto-style1">*</span> <span style="color: rgb(96, 96, 96); font-family: 微软雅黑, 'Microsoft YaHei'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(236, 245, 255);">-1表示明天不分红，0表示采用实际收入分红，&gt;0表示采用本数据分红。 参与分红的每日商城订单总数。如果这里的值大于0，则消费者加权平均直接返还将失效</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="运营中心默认每日商城总销量不能为空!"
                                    ControlToValidate="Textbox_TextboxMoneyConsumerAllOrderB" ForeColor="#FF3300"></asp:RequiredFieldValidator><br />
                                <asp:Literal ID="Literal4ConsumerAllOrderB" runat="server"></asp:Literal>
                            </td>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>消费财富协议：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBox_ShowConsumerWealthAgreement" runat="server" Text="消费财富选购前必须同意消费财富协议" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>编辑协议：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:HyperLink ID="HyperLinkConsumerWealthAgreement" runat="server" Enabled="False">编辑协议</asp:HyperLink>
                                <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="消费财富选购前必须同意消费财富协议" Checked="True" />--%>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>编辑提现须知：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:HyperLink ID="HyperLink1ConsumerWealthDrawMoney" runat="server" Enabled="False">编辑提现须知</asp:HyperLink>
                                <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="消费财富选购前必须同意消费财富协议" Checked="True" />--%>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                <div align="right"><strong>选择优惠商品：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                <div align="left" style="margin-right: 100px;">
                                    <asp:DropDownList ID="DropDownListDiscountGood" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>


                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>每个自然月限制单数：</strong></td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="Textbox_LimitBuyEveryMonth" runat="server" Width="100px" CssClass="l_input">99999</asp:TextBox>
                                。<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="格式整数"
                                    ValidationExpression="^\d+$" ControlToValidate="Textbox_LimitBuyEveryMonth"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="每个自然月限制单数不能为空!"
                                    ControlToValidate="Textbox_LimitBuyEveryMonth" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                消费者下单购买。按照购买金额进行财富返还。消费者的财富中心直接增加多少倍的财富基金。按照企业返还给消费者的回报，每日加权分红</td>
                        </tr>


                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="center" height="35" style="width: 72px">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="35">&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px"
                        OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
