<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03OperationCenter_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._03OperationCenter_Manage" %>

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
                            <th align="center" colspan="2" height="25"><strong>管 理 运 营 中 心 </strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>微店ID：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtUserID" runat="server" Width="200px" CssClass="l_input" MaxLength="200"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtUserID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="微店ID不能为空!" ControlToValidate="txtUserID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>注明公司或个人名称：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox1MasterName" runat="server" Width="100px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="注明公司或个人名称不能为空!" ControlToValidate="TextBox1MasterName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>注明公司或个人名称联系人手机号：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox2MasterPhone" runat="server" Width="100px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="注明公司或个人名称联系人手机号不能为空!" ControlToValidate="TextBox2MasterPhone"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>注明公司或个人名称联系人地址：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox3MasterAddress" runat="server" Width="400px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>银行账户姓名：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox4BankAccountUserName" runat="server" Width="400px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>开户行：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox5BankAccountName" runat="server" Width="400px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>银行账号：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox6BankAccountNumber" runat="server" Width="400px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>运 营 收 入：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:HyperLink ID="HyperLink_b003_TotalCredits_OperationCenter" runat="server" Enabled="False" Target="_blank">收入 点击查看详情</asp:HyperLink>
                            </td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3" style="display:none">
                            <td height="35" class="style3" align="right">
                                <strong>账户运营状态：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBoxRunningState" runat="server" Text="账户运营状态。不选表示取消运营资格，但是不影响提现申请" Checked="True" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3" style="display:none">
                            <td height="35" class="style3" align="right">
                                <strong>账户是否可以申请提现：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBoxAccountState" runat="server" Text="银行账户状态。不选表示冻结账户" Checked="True" />
                            </td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>是否股东账户：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:CheckBox ID="CheckBox1ShareholderState" runat="server" Text="是否股东账户" Checked="True" />
                            </td>
                        </tr>
                          <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                    <div align="right"><strong>选择上级：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                    <div align="left" style="margin-right: 100px;">
                                        <asp:DropDownList ID="DropDownListChoiceParentIDList" runat="server">
                                        </asp:DropDownList>
                                    </div>
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
