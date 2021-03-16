<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardSearchkeywordList_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._51SearchkeywordList.BoardSearchkeywordList_Manage" %>

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
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>搜 索 关 键 词 管 理</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>关键词：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="关键词不能为空!"
                        ControlToValidate="txtKeyword"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>关键词搜索频度：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBoxKeywordCount" runat="server" Width="376px" CssClass="l_input">0</asp:TextBox>
                    数字越大，排序越靠前<asp:RegularExpressionValidator ID="RegularExpressionValidator_OnlyNum"
                                        runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_TextBoxKeywordCount"
                                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                    </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style3">
                    <strong>搜索地区来源：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextBoxSearchArea" runat="server" Width="376px" CssClass="l_input" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style3">
                    <strong>搜索用户昵称来源：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextBoxSearchUserNickName" runat="server" Width="376px" CssClass="l_input" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
