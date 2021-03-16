<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board3D_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.Board3D_Manage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ClassAddBig</title>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="679" border="0" align="center">
            <tr>
                <td valign="top" align="center">                  
                    <table class="border" style="width: 679px; height: 107px" cellspacing="2" cellpadding="0"
                        width="503" align="center" border="0">
                        <tr class="title" bgcolor="#a4b6d7">
                            <td align="center" colspan="2" height="25"><strong>修改文件属性</strong></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" width="150px" height="22">
                                <div align="right"><strong>排列位置：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <div align="left">
                                    <asp:TextBox ID="txtPos" runat="server" Width="260px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtPos"
                                        Display="Dynamic" ErrorMessage="排列位置不能为空!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPos"
                                        Display="Dynamic" ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$"></asp:RegularExpressionValidator>
                                </div>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3">
                                <div align="right"><strong>是否显示：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff">
                                <div align="left">
                                    <asp:CheckBox ID="cbIsShow" runat="server" Text="显示相册" Checked="True"></asp:CheckBox>
                                </div>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td style="width: 138px; height: 22px;" align="center" bgcolor="#e3e3e3">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" style="height: 22px">
                                <div align="left">
                                    &nbsp;
										<asp:Button ID="btnModify" runat="server" Text=" 修  改 " Width="72px" OnClick="btnModify_Click"></asp:Button>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

