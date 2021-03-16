<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board02GuWuQuanChange_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange.Board02GuWuQuanChange_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceAdd</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">积分<strong>兑换（购物券兑换现金或其他）管理</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>原始积分数量：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtGouWuQuan" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="用户的购物券不能为空!"
                        ControlToValidate="txtGouWuQuan">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="txtGouWuQuan"
                        ForeColor="#FF3300">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>
                       <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>兑换目标：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:RadioButtonList ID="RadioButtonList_ChangeDestination" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList_ChangeWay_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="xianjin" Selected="True">现金（现金兑换成购物券）</asp:ListItem>
                        <asp:ListItem Value="qita">其他（可兑换成其他实物、商品或服务，需客服介入进行管理）</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
                <tr class="tdbg" bgcolor="#e3e3e3">
                    <td align="right" height="35" class="style3">
                        <strong>兑换方式：</strong>
                    </td>
                    <td align="left" bgcolor="#ecf5ff" height="35">
                           <asp:RadioButtonList ID="RadioButtonList_ChangeAutoOrHand" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Auto" Selected="True">自动兑换（平台自动处理）</asp:ListItem>
                                <asp:ListItem Value="Hand">手动处理（平台客服人员参与沟通，并进行管理）</asp:ListItem>
                            </asp:RadioButtonList>
                      

                    </td>
                </tr>
                <tr class="tdbg" bgcolor="#e3e3e3">
                    <td class="style3" align="right" width="250" height="35">
                        <strong>现金数目：</strong>
                    </td>
                    <td bgcolor="#ecf5ff">
                        <asp:TextBox ID="txtXianJin" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1txt_XianJin" runat="server" ErrorMessage="兑换目标现金不能为空!"
                            ControlToValidate="txtXianJin">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3txt_XianJin" runat="server" ErrorMessage="格式000.00"
                            ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="txtXianJin"
                            ForeColor="#FF3300">
                        </asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr class="tdbg" bgcolor="#e3e3e3">
                    <td align="right" height="35" class="style3">
                        <strong>实物、商品或服务简短描述：</strong>
                    </td>
                    <td align="left" bgcolor="#ecf5ff" height="35">
                        <asp:TextBox ID="TextBoxShortDesc" runat="server" Width="376px" CssClass="l_input" MaxLength="150"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxShortDesc_ShiWu" runat="server" ErrorMessage="简短描述不能为空!！"
                            ControlToValidate="TextBoxShortDesc" ForeColor="#FF3300">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5TextBoxShortDesc_ShiWu" runat="server" ErrorMessage="不能超过50个字。"
                            ValidationExpression="^(.){1,50}$" ControlToValidate="TextBoxShortDesc" ForeColor="#FF3300">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>  

                  </contenttemplate>
            </asp:UpdatePanel>
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
