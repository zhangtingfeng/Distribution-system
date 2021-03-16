<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Net16SendMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._16SendMoney.Net16SendMoney" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BasicInfo</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Textbox_Timer0").calendar();
            $("#Textbox_Timer1").calendar();
        });
    </script>
    <style type="text/css">
        td {
            height: 30px;
            text-align: left;
        }

        .auto-style1 {
            width: 20%;
        }

        input {
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <strong>有效开始时间：</strong>
                    <strong>
                        <br />
                        有效结束时间：</strong>
                </td>
                <td bgcolor="#ecf5ff" align="left">

                    <input type="text" value="<%=strTextbox_Timer0_Text%>" maxlength="100" id="Text-SecondBuyStart"
                        name="Text-SecondBuyStart" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                        readonly="true" class="calendarFocus l_input" style="cursor: pointer" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input
                        type="text" value="<%=strTextbox_Timer1_Text%>" maxlength="100" id="Text-SecondBuyEnd"
                        name="Text-SecondBuyEnd" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                        class="calendarFocus l_input" readonly="true" style="cursor: pointer" />
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>推送对象：</strong></font></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="RadioButtonList_SendToType" runat="server" Style="text-align: left">
                            </asp:RadioButtonList>

                            <asp:CheckBox ID="CheckBox_Test" runat="server" Text="推送给特定的用户ID" AutoPostBack="True" OnCheckedChanged="CheckBox_Test_CheckedChanged" /><asp:TextBox ID="TextBox_Test" runat="server" ToolTip="要先测试特定的用户ID" Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="不能为空!"
                                ControlToValidate="TextBox_Test" ForeColor="#FF3300" Enabled="False"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>红包类型：</strong></font></td>

                <td bgcolor="#ecf5ff" align="left">
                    <asp:RadioButtonList ID="RadioButtonList_RedType" runat="server">
                        <asp:ListItem Value="1">现金红包</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">购物券（购物红包）</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>推送总金额：</strong></font></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:TextBox ID="Textbox_Price" runat="server" Width="100px" CssClass="l_input">10</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Price"
                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="总金额不能为空!"
                        ControlToValidate="Textbox_Price" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>推送个数：</strong></font></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:TextBox ID="Textbox_HowMany" runat="server" Width="100px" CssClass="l_input">2</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="个数不能为空!"
                        ControlToValidate="Textbox_HowMany" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    &nbsp; 如果个数小于推送总数，来的晚的人就抢不到了</td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>推送标题：</strong></font></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:TextBox ID="MsgTypeNewsTitle" runat="server" Width="688px" CssClass="l_input" Height="41px" MaxLength="255">分红 块抢</asp:TextBox>
                </td>
            </tr>


            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>推送内容：</strong></font></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:TextBox ID="Textbox_Content" runat="server" Width="688px" CssClass="l_input" Height="248px" MaxLength="255" TextMode="MultiLine">分红 块抢 分红方案 </asp:TextBox>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">&nbsp;</td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Button ID="Button_Save" runat="server" Height="35px" Text="保存" Width="135px" OnClick="Button_Save_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button_Save_Send" runat="server" Height="35px" Text="保存并放入推送队列" Width="135px" OnClick="Button_Save_Send_Click" />
                </td>
            </tr>

        </table>
    </form>
</body>
</html>