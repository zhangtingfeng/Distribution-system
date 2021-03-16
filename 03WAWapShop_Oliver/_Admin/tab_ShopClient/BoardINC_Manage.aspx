<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardINC_Manage.aspx.cs" Inherits="_03WAWapShop_Oliver.Admin.tab_ShopClient.BoardINC_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>商 城 商 户 资 料</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input {
            height: auto;
        }

        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%;">
                <tr>
                    <td valign="top" align="center" style="width: 100%; background-color=#e3e3e3">
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                            align="center" border="0">
                            <tr class="title">
                                <th class="centerAuto" colspan="2" height="25">
                                    <strong>&nbsp;&nbsp; 微 云 基 石 商 城 商 户 资 料（标 准 版）</strong>
                                </th>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;">
                                    <div align="right">
                                        <strong>商城后台用户名：</strong>
                                    </div>
                                </td>
                                <td style="height: 36px;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="TextboxUserName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <font face="宋体"></font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="用户名不能为空!"
                                        ControlToValidate="TextboxUserName" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <div align="right">
                                        <strong>密码：</strong>
                                    </div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:TextBox ID="TextboxUserPassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox>
                                    <ajaxToolkit:PasswordStrength ID="TextboxUserPassword_PasswordStrength" runat="server"
                                        TargetControlID="TextboxUserPassword" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator"
                                        PreferredPasswordLength="6" HelpStatusLabelID="TextBox2_HelpLabel" StrengthStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good"
                                        BarBorderCssClass="BarBorder_TextBox2" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                                        TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" RequiresUpperAndLowerCaseCharacters="true" />
                                    <asp:Label ID="TextBox2_HelpLabel" runat="server" ForeColor="#FF3300" />
                                    <font face="宋体">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextboxUserPassword" runat="server"
                                            ErrorMessage="密码不能为空!" ControlToValidate="TextboxUserPassword" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    </font>
                                    <asp:Label ID="Label_ModifyTip" runat="server" Text="不修改请置空" Visible="False" ForeColor="#FF3300"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <div align="right">
                                        <strong>重复密码：</strong>
                                    </div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxRePassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox><font
                                        face="宋体">
                                        <asp:CompareValidator ID="CompareValidator5451" runat="server" ControlToCompare="TextboxRePassword"
                                            ControlToValidate="TextboxUserPassword" Display="Dynamic" ErrorMessage="两次输入密码是否相同"
                                            ForeColor="#FF3300"></asp:CompareValidator>
                                        <asp:Label ID="Label_ModifyTip0" runat="server" Text="不修改请置空" Visible="False" ForeColor="#FF3300"></asp:Label>
                                    </font>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22" style="font-weight: 700;">
                                    <div align="right">
                                        Email：
                                    </div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="Textbox_Email" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Email不能为空!"
                                        ControlToValidate="Textbox_Email" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email格式不对！"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Textbox_Email"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>真实姓名：</strong>
                                    </div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:TextBox ID="Textbox_RealName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>性别：</strong>
                                    </div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:RadioButtonList ID="RadioButtonList_Sex" runat="server" RepeatDirection="Horizontal"
                                            Width="193px">
                                            <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                            <asp:ListItem Value="0">女</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;">
                                    <div align="right">
                                        <strong>公司名称：</strong>
                                    </div>
                                </td>
                                <td style="height: 36px;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="txtINCName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <font face="宋体">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_INCName" runat="server" ErrorMessage="公司名称不能为空!"
                                            ControlToValidate="txtINCName" Enabled="False" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    </font>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;">
                                    <div align="right">
                                        <strong>公司电话：</strong>
                                    </div>
                                </td>

                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxINCPhone" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;">
                                    <div align="right">
                                        <strong>公司地址：</strong>
                                    </div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxAddress" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>备注信息：</strong>
                                    </div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="Textbox_BeiZhu" runat="server" Width="436px" Height="70px" TextMode="MultiLine"
                                        CssClass="l_input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>授权时间：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" height="22">&nbsp;<asp:TextBox ID="TextboxUser_Authortime" runat="server" Width="176px" CssClass="l_input"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="TextboxUser_Authortime"
                                        DaysModeTitleFormat="yyyy/MM/dd HH" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" />
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3" style="display: none;">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>是否微云基石直营：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" height="22">
                                    <asp:CheckBox ID="CheckBox_ShenMaShopping" runat="server" Text="是否微云基石直营" />
                                    <ajaxToolkit:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="TextboxUser_Authortime"
                                        DaysModeTitleFormat="yyyy/MM/dd HH" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" />
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" class="auto-style1">
                                    <div align="right">
                                        <strong>专业版：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" class="auto-style1">
                                    <ajaxToolkit:CalendarExtender ID="Calendarextender2" runat="server" TargetControlID="TextboxUser_Authortime"
                                        DaysModeTitleFormat="yyyy/MM/dd HH" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" />
                                    <asp:CheckBoxList ID="CheckBoxList_PowerList" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">多媒体购物(微信语音)</asp:ListItem>
                                        <asp:ListItem Value="1">数据分析宝（DA宝）</asp:ListItem>
                                        <asp:ListItem Value="2">商家发起购物券</asp:ListItem>
                                        <asp:ListItem Value="3">可绑定3个微信号</asp:ListItem>
                                        <asp:ListItem Value="4">代理模式</asp:ListItem>
                                        <asp:ListItem Value="4">分红方案设计</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" class="auto-style1">
                                    <div align="right">
                                        <strong>商务高级版：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" class="auto-style1">
                                    <ajaxToolkit:CalendarExtender ID="Calendarextender3" runat="server" TargetControlID="TextboxUser_Authortime"
                                        DaysModeTitleFormat="yyyy/MM/dd HH" Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" />
                                    <asp:CheckBox ID="CheckBox_o2o" runat="server" Text="o2o功能" />
                                    <asp:CheckBox ID="CheckBox_WeiXinPayRedHongBao" runat="server" Text="用户申请提现后马上转账" />
                                    <asp:CheckBox ID="CheckBox_GoodChildClass" runat="server" Text="允许商品子分类" />
                                    <asp:CheckBox ID="CheckBox_AgentStatusPower" runat="server" Text="代理商数据统计" />
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3" <%=strDisPlay%>>
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>合伙人微信关联：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:HyperLink ID="HyperLink_LinkWeiXin" runat="server" NavigateUrl="">扫一扫关联合伙人的微信号</asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink_Clear" runat="server" NavigateUrl="">清空关联</asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label_LinkWeiXin" runat="server" Text="关联微信号，查看你的合伙人收益！" ForeColor="#FF3300"></asp:Label>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3" <%=strDisPlay%>>
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>介绍人微信关联：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:HyperLink ID="HyperLink_Recommand" runat="server" NavigateUrl="">扫一扫关联介绍人的微信号</asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink_Clear_Recommand" runat="server" NavigateUrl="">清空关联</asp:HyperLink>
                                        &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label_LinkWeiXin_Recommond" runat="server" Text="关联微信号，查看你的介绍人收益！" ForeColor="#FF3300"></asp:Label>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3" style="display: none;">
                                <td align="center" height="22">
                                    <div align="right">
                                        <strong>购物红包：</strong>
                                    </div>
                                </td>
                                <td align="left" bgcolor="#ecf5ff" height="22">&nbsp;<asp:TextBox ID="Textbox_Shopping_Vouchers_Money" runat="server" Width="176px" CssClass="l_input">100000</asp:TextBox>
                                    元</td>
                            </tr>

                            
                        </table>
                    </td>
                </tr>
            </table>
        </font>
        <p style="text-align: center">
            <font face="宋体">
                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="144px" OnClick="btnAdd_Click"
                    CssClass="b_input"></asp:Button>
            </font>
        </p>
    </form>
</body>
</html>
