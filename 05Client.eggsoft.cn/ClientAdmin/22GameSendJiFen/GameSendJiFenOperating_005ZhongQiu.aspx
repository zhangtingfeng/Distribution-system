<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameSendJiFenOperating_005ZhongQiu.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._22GameSendJiFen.GameSendJiFenOperating_005ZhongQiu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .border input {
            height: auto;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Text_Shopping_Vouchers_Start").calendar();
            $("#Text_Shopping_Vouchers_End").calendar();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>国庆贺卡</strong>
                </th>
            </tr>
            <tr class="tdbg" style="<%=DisPlayStatus_New_None%>" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>编号：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>自定义游戏名称名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_Name" runat="server" Width="137px" CssClass="l_input" MaxLength="20">国庆贺卡</asp:TextBox>
                    <span class="style1"><strong>*</strong></span> <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="名称不能为空!" ControlToValidate="TextBox_Name"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>赠送类型：</strong></td>
                <td bgcolor="#ecf5ff" align="left" >
                    <asp:RadioButtonList ID="RadioButtonList_SendType" runat="server" CssClass="l_input" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">商城现金</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">商城购物券</asp:ListItem>
                        <asp:ListItem Enabled="False" Value="3">微信红包</asp:ListItem>
                        <asp:ListItem Enabled="False" Value="4">微信零钱</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>定制一个国庆贺卡送：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtSendMoney" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>元
                <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="金额不能为空!" ControlToValidate="txtSendMoney"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSendMoney"
                        ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>游戏终止日期：</strong></td>
                <td bgcolor="#ecf5ff">
                    <input id="TextBox_EndTime"  class="calendarFocus l_input" maxlength="100" name="TextBox_EndTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)" readonly="true" style="cursor: pointer" type="text" value="<%=strTextBox_EndTime%>" /></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="72px" OnClick="btnAdd_Click"
                        CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
