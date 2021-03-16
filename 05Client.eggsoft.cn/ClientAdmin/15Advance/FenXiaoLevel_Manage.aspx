<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FenXiaoLevel_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.FenXiaoLevel_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>分 销 代 理 级 别 管 理</title>
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
                            <th align="center" colspan="2" height="25"><strong>管 理 分 销 代 理 级 别 </strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>商家代理分销名称：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtTitle" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>商家代理分销提成：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_LevelPercent" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="分类不能为空!" ControlToValidate="TextBox_LevelPercent"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox_LevelPercent" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style3">
                                <strong>排序位置：</strong></td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="txtMenuPos" runat="server" Width="89px" CssClass="l_input" MaxLength="10">0</asp:TextBox>
                                数字越大 排序越靠后  <span class="style1"><strong>*</strong></span>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="排序位置不能为空 必须是数字" ControlToValidate="txtMenuPos"
                                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ErrorMessage="排序位置不能为空!" ControlToValidate="txtMenuPos"></asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                       

                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="center" height="35" style="width: 72px">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="35">&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px"
                        OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>