<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board34Channel_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._34WuLiu.Board34Channel_Manage" %>

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
                <th align="center" colspan="2" height="25"><strong>渠道管理</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>渠道名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtChannelName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="渠道名称不能为空!"
                        ControlToValidate="txtChannelName">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>


           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>--%>
                <tr class="tdbg" bgcolor="#e3e3e3">
                    <td align="right" height="35" class="style3">
                        <strong>渠道描述：</strong>
                    </td>
                    <td align="left" bgcolor="#ecf5ff" height="35">
                        <asp:TextBox ID="TextBoxShortDesc" runat="server" Width="552px" CssClass="l_input" MaxLength="350" Height="74px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxShortDesc_ShiWu" runat="server" ErrorMessage="简短描述不能为空!！"
                            ControlToValidate="TextBoxShortDesc" ForeColor="#FF3300">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5TextBoxShortDesc_ShiWu" runat="server" ErrorMessage="不能超过50个字。"
                            ValidationExpression="^(.){1,350}$" ControlToValidate="TextBoxShortDesc" ForeColor="#FF3300">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>  

               <%--   </contenttemplate>
            </asp:UpdatePanel>--%>
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
