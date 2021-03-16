<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_28Member.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange.Manage_28Member" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>设置充值政策</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>添加会员卡/充值</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>充值方式/交易流水号：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxBankSeraillnum" runat="server" Width="376px" CssClass="l_input" ToolTip="现金/POS/微信收款"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="充值方式/交易流水号不能为空!"
                        ControlToValidate="TextBoxBankSeraillnum">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>充值金额：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtInputMoney" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="用户的充值金额不能为空!"
                        ControlToValidate="txtInputMoney">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="txtInputMoney"
                        ForeColor="#FF3300">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>会员手机号码：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_MemberMobile" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="用户的手机号码不能为空!"
                        ControlToValidate="TextBox_MemberMobile">
                    </asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBox_YanZhengMa" runat="server" Width="76px" CssClass="l_input"></asp:TextBox>

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="ButtonSendCode" runat="server" Text="发送验证码" OnClick="ButtonSendCode_Click" />
                            <asp:Timer ID="Timer1SendCode" runat="server" Enabled="False" Interval="1000" OnTick="Timer1SendCode_Tick">
                            </asp:Timer>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                    <asp:TextBox ID="TextBox_YanZhengMaHide" runat="server" Width="76px" CssClass="l_input" ReadOnly="True" Visible="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
