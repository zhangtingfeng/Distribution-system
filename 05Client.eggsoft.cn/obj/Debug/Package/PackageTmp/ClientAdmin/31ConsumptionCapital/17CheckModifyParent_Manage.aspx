<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="17CheckModifyParent_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._17CheckModifyParent_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>运 营 中 心 管 理</title>
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
                            <th align="center" colspan="2" height="25"><strong>处 理 运 营 中 心 申 请 变 更 上 下 级 关 系 </strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                <div align="right"><strong>申请信息：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                <div align="left" style="margin-right: 100px;">
                                    <asp:Label ID="Label1AskForInfo" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                <div align="right"><strong>运营中心信息：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                <div align="left" style="margin-right: 100px;">
                                    <asp:Label ID="Label1OperationCenterInfo" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                       <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                <div align="right"><strong>附加申请信息：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                <div align="left" style="margin-right: 100px;">
                                    <asp:Label ID="Label1UserExtraMemo" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>接受或者退回（拒绝）：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:RadioButtonList ID="RadioButtonList1FeedbackStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">接受（已处理）</asp:ListItem>
                                    <asp:ListItem Value="2">退回（拒绝）</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>反馈详情描述：</strong></td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox1FeedbackMemo" runat="server" Height="99px" TextMode="MultiLine" Width="431px"></asp:TextBox>
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
