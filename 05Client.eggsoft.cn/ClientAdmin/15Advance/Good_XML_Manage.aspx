<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Good_XML_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.Good_XML_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>XML定义</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

 
 
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25">商家字典</th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>商家自有XML名称：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtName" runat="server" Width="376px" CssClass="l_input">商家自有XML名称</asp:TextBox>
                                <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="名称不能为空!" ControlToValidate="txtName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style3">
                                <strong>XML规则：</strong></td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="txtXML" runat="server" Width="500px" CssClass="l_input" Height="241px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="规则不能为空!" ControlToValidate="txtXML"></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style3">
                                <strong>受影响商品列表：</strong></td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:CheckBoxList ID="CheckBoxList_ini_GoodList" runat="server">
                                </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="center" height="35" style="width: 72px">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="35">&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="100px"
                        OnClick="btnAdd_Click" CssClass="b_input"></asp:Button></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>