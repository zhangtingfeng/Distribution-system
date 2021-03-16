<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiFenXiaoLevel_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.MultiFenXiaoLevel_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>编 辑 团 队 奖 励 方 案 管 理</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />




</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25"><strong>编 辑 团 队 奖 励 方 案 管 理</strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>代理分销方案名称：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtTitle" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                <strong>*</strong><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="名称不能为空!"
                                    ControlToValidate="txtTitle" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>当前(上级)代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_FenxiaoParentGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="当前代理所得不能为空!" ControlToValidate="TextBox_FenxiaoParentGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_FenxiaoParentGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>上上级代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_FenxiaoGrandParentGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="上上级代理所得不能为空!" ControlToValidate="TextBox_FenxiaoGrandParentGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox_FenxiaoGrandParentGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>上上上级代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_FenxiaoGreatParentGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="上上上级代理所得不能为空!" ControlToValidate="TextBox_FenxiaoGreatParentGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox_FenxiaoGreatParentGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>直系下级所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_ChildGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="直系下级所得不能为空!" ControlToValidate="TextBox_ChildGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox_ChildGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:CheckBox ID="CheckBox_ChildGet" runat="server" Text="(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>下下级所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_GrandsonGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="下下级所得为空!" ControlToValidate="TextBox_GrandsonGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TextBox_GrandsonGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:CheckBox ID="CheckBox_GrandsonGet" runat="server" Text="(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>下下下级所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_GreatsonGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="下下下级所得不能为空!" ControlToValidate="TextBox_GreatsonGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TextBox_GreatsonGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:CheckBox ID="CheckBox_GreatsonGet" runat="server" Text="(是否加权平均现金)强制现金积分方式或者据用户角色决定购物积分方式" />
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td colspan="2" align="center" height="35" style="width: 72px">(以上数字请按照百分比（相对百分比）填写，总数应是100%。在商品的代理商利润进行百分比分配。0表示该功能不选用。)
                            </td>

                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>当前团队代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_OperationGet" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="当前运营中心所得不能为空!" ControlToValidate="TextBox_OperationGet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox_OperationGet" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>上级团队代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_OperationGetParent" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="上级运营中心所得能为空!" ControlToValidate="TextBox_OperationGetParent"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox_OperationGetParent" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>上上级团队代理所得(%)：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_OperationGetGrandParent" runat="server" Width="376px" CssClass="l_input" MaxLength="20">0</asp:TextBox>
                                <span class="style1">%<strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="上上级运营中心所得不能为空!" ControlToValidate="TextBox_OperationGetGrandParent"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox_OperationGetGrandParent" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td colspan="2" align="center" height="35" style="width: 72px">(以上数字请按照百分比（绝对百分比）填写。按照商品的销售价格进行百分比分配。0表示该功能不选用。)
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
