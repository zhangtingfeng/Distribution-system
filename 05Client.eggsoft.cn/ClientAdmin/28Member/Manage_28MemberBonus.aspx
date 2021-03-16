<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_28MemberBonus.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange.Manage_28MemberBonus" %>

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
                <th align="center" colspan="2" height="25"><strong>设置充值奖励政策</strong></th>
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
                    <strong>赠送金额：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxBonusMoney" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="赠送金额不能为空!"
                        ControlToValidate="TextBoxBonusMoney">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextBoxBonusMoney"
                        ForeColor="#FF3300">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>赠送购物券：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxBonusGouWuQuan" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="赠送购物券金额不能为空!"
                        ControlToValidate="TextBoxBonusGouWuQuan">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式000.00"
                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextBoxBonusGouWuQuan"
                        ForeColor="#FF3300">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>政策描述：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxBonusDesc" runat="server" Width="376px" CssClass="l_input" MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
