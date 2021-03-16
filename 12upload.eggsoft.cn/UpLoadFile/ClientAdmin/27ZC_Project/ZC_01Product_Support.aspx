<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZC_01Product_Support.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin._27ZC_Project.ZC_01Product_Support" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>众筹档位页面详情页面</title>
    <script src="../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/jquery-2.0.3.min.js?version=js201709121928" type="text/javascript"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .border input {
        }

        .auto-style1 {
            color: #CC0000;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">

                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                        align="center" border="0">

                        <tr class="title">
                            <th align="center" colspan="2" height="25">
                                <strong>众 筹 档 位 详 情 页 面</strong>
                            </th>
                        </tr>
                        
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>档位名称：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="TextboxName" runat="server" Width="100px" CssClass="l_input" MaxLength="10"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="当前档位名称不能为空!"
                                    ControlToValidate="TextboxName" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>档位支付金额：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextboxSalesPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxSalesPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="档位金额不能为空!"
                                    ControlToValidate="TextboxSalesPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>本档位代理商所得：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextboxAgentPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Text="分销代理所得，即三级分销所得，0表示没有"></asp:Label><span class="auto-style1">
                                
                                *</span><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxAgentPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="代理商所得不能为空!"
                                    ControlToValidate="TextboxAgentPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>




                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>档位名额限制：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="TextboxSalesLimit" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="TextboxSalesLimit" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="档位商品销售数量不能为空!"
                                    ControlToValidate="TextboxSalesLimit" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                0表示没有限制</td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>&nbsp;<strong>档位上架状态：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:RadioButtonList ID="RadioButtonList_IsSaled" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">上架</asp:ListItem>
                                    <asp:ListItem Value="0">下架</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>当前档位承诺或回报（最多205字）：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="TextboxSalesPricePromiseAndReturn" runat="server" Width="639px" CssClass="l_input" Height="85px" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="当前档位承诺或回报不能为空!"
                                    ControlToValidate="TextboxSalesPricePromiseAndReturn" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>是否需要发货：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:RadioButtonList ID="RadioButtonList_SupportWay" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">支付即发货</asp:ListItem>
                                    <asp:ListItem Value="1">双色球计算中奖发货</asp:ListItem>
                                    <asp:ListItem Value="2">福彩3D计算中奖发货</asp:ListItem>
                                    <asp:ListItem Value="3">无偿支持,不需回报,无需发货</asp:ListItem>
                                    <asp:ListItem Value="4">股权类众筹,后期回报,无需发货</asp:ListItem>
                                </asp:RadioButtonList>
                                本平台不支持众筹库存量/销量自动统计

                                <br />
                                双色球计算方法表示使用双色球数据（从小到大排序） （双色球开奖时间为每周二、四、日的21：30），系统将在（每周二、四、日）10点开奖。  
                                <br />
                                如果选择，必须输入满足多少个后开奖。 开奖结果 微信推送给相关用户。或者 登陆网页查看 。<br />
                                双色球将使用用户的订单号+双色球数据，取开奖个数的余数，按照余数，查找购买顺序决定中奖者。<br />
                                <br />
                                福彩3D计算方法表示使用3d数据（每天晚上8点30分） 系统将在每天的10点开奖。  
                                <br />
                                如果选择，必须输入满足多少个后开奖。 开奖结果 微信推送给相关用户。或者 登陆网页查看
                                。<br />
                                福彩3D将使用购买顺序+福彩3D，取开奖个数的余数，按照余数，查找购买顺序决定中奖者。

                            </td>
                        </tr>
                       


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>(双色球/福彩3D)<br />
                                    每满足多少个抽奖：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="TextboxSupportHowMany" runat="server" Width="100px" CssClass="l_input">1</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="TextboxSupportHowMany" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="每满足多少个抽奖不能为空!"
                                    ControlToValidate="TextboxSupportHowMany" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                每销售多少个开奖，如果满足，开奖时间见开奖方法。如果不满足，也开奖，开奖时间在项目结束当天的晚10点
                            </td>
                        </tr>
                       
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>参与众筹是否必须关注：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">

                                <asp:CheckBox ID="CheckBox_MustSubscribe" runat="server" Text="参与众筹是否必须关注" />

                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>参与众筹是否有联系方式：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">

                                <asp:CheckBox ID="CheckBox_MustAddress" runat="server" Text="参与众筹是否必须输入收获地址" />

                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>一个账户只能参与本档位一次众筹：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBoxOnlyBuyOneOnlyOneAccount" runat="server" Text="一个微信号只能参与本档位一次众筹（本系统不支持一个手机切换微信号）" />

                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>排序位置：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="Textbox_Sort" runat="server" Width="100px" CssClass="l_input">1</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_Sort" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="排序位置不能为空!"
                                    ControlToValidate="Textbox_Sort" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="right" height="45" class="style4">&nbsp;
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="45">
                                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="100px" OnClientClick="return CheckClientValidate();"
                                    OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    &nbsp;
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
