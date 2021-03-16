<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_29AdminPower.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange.Manage_29AdminPower" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>设置用户</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>设置用户权限</strong></th>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>用户真实姓名：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxUserRealName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>登陆用户名：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtInputMoneyShopClientAdmin" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="用户名不能为空!"
                        ControlToValidate="txtInputMoneyShopClientAdmin">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>密码：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxUserPassword" runat="server" TextMode="Password" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="TextboxUserPassword"
                        StrengthIndicatorType="BarIndicator" PreferredPasswordLength="12" MinimumNumericCharacters="3"
                        MinimumSymbolCharacters="1" BarIndicatorCssClass="bartype" BarBorderCssClass="barborder">
                    </asp:PasswordStrength>
                    <asp:Label ID="TextBox2_HelpLabel" runat="server" />
                    <asp:Label ID="TextboxUserPasswordLabel_ModifyTip" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextboxUserPassword" runat="server"
                        ErrorMessage="密码不能为空!" ControlToValidate="TextboxUserPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>重复密码：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxRePassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator5451" runat="server" ControlToCompare="TextboxRePassword"
                        ControlToValidate="TextboxUserPassword" Display="Dynamic" ErrorMessage="两次输入密码是否相同"></asp:CompareValidator>
                    <asp:Label ID="TextboxRePasswordLabel_ModifyTip0" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
                </td>
            </tr>
            
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>角色选择：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                  <asp:DropDownList ID="DropDownList_RoleSelect" runat="server" Height="20px" Width="201px">
                                </asp:DropDownList>
                </td>
            </tr>
              <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>所在组织机构选择：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                  <asp:DropDownList ID="DropDownListEnterpriseOrganization" runat="server" Height="20px" Width="201px">
                                </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    &nbsp;</td>
                <td bgcolor="#ecf5ff">
                    &nbsp;</td>
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
