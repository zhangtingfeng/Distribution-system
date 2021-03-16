<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IwantRetunMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.IwantRetunMoney" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BoardINC_Basic_Manage</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }

        .auto-style2 {
            width: 80%;
            height: 35px;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 147px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25"><strong>退款基本信息</strong></th>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="auto-style1">
                                <strong>微店订单号码：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:Label ID="Label_OrderNum" runat="server" Text=""></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label_OrderNum_Status" runat="server" ForeColor="#FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="auto-style1">
                                <strong>请求金额：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:TextBox ID="TextBox_Ask_Money" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_Ask_Money" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ControlToValidate="TextBox_Ask_Money" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="TextBox_TextBox_Ask_Money_Hide" runat="server" Visible="False"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="auto-style1">
                                <strong>商品信息：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:Label ID="Label_GoodInfo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="auto-style1">
                                <strong>支付信息：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:Label ID="Label_PayInfo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="auto-style1">
                                <strong>消费者信息：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:Label ID="Label_UserInfo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="100">

                                <strong>请求退款证据信息：<br />
                                </strong>&nbsp;<span class="style_Replace"></span></td>
                            <td style="width: 80%;" bgcolor="#ecf5ff" height="100">
                                <asp:TextBox ID="TextBox_ReturnMoney" runat="server" Height="100px"
                                    TextMode="MultiLine" Width="50%" CssClass="l_input" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <p style="text-align: center">
            <asp:Button ID="btnAdd" runat="server" Text=" 提  交 " Width="72px"
                OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
        </p>
    </form>


</body>
</html>