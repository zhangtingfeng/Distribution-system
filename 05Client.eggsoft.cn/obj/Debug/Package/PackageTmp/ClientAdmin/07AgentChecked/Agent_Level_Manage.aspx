<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Level_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Agent_Level_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>管 理 商 家 代 理 分 类</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function uploadComplete(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image_Cover").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }
        function uploadError(sender, args) {
            alert("上传错误");
        }
    </script>

    <script language="javascript" type="text/javascript">
        function CheckClientValidate() {
            if (Page_ClientValidate()) {

                var srcUp = $("#Image_Cover").attr("src");
                if (srcUp.toString().length == 0) {
                    alert("相关图片必须选择！");
                    return false;
                }
                return true;
            }
            //Page_BlockSubmit = false;  //当页面中有其他不需要验证的按钮或下拉框时一定要加上这句话，否则其他下拉框第一次提交时不会触发后台代码
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25"><strong>管 理 商 家 代 理 分 类</strong></th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td height="35" class="style3" align="right">
                                <strong>商家代理级别名称：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtTitle" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                                <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="代理名称不能为空!" ControlToValidate="txtTitle"></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                <div align="right"><strong>能否取得下级团队的收入：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <div align="left">
                                    <asp:CheckBox ID="CheckBox_AgentOperationGetChild" runat="server" Text="能否取得下级团队分佣" />
                                    <br />
                                    <asp:CheckBox ID="CheckBox_AgentOperationGetGrandChild" runat="server" Text="能否取得下下级团队分佣" />
                                    <br />
                                    <%--<asp:CheckBox ID="CheckBox_AgentOperationGetGreatChild" runat="server" Text="能否取得下下下级团队分佣"  />--%>
                                </div>
                                <div align="right" style="margin-right: 100px;">
                                </div>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3" style="display: none">
                            <td height="35" class="style3" align="right">
                                <strong>等值购物券商品价格：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <div align="left">
                                    <asp:TextBox ID="TextBox_Vouchers_Consume_Or_Recharge" runat="server">0.00</asp:TextBox>
                                    <asp:Label ID="Label1" runat="server" Text="等值购物券商品价格"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                        ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ControlToValidate="TextBox_Vouchers_Consume_Or_Recharge"
                                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="等值购物券商品价格金额不能为空!"
                                        ControlToValidate="TextBox_Vouchers_Consume_Or_Recharge" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3" style="display: none">
                            <td height="35" class="style3" align="right">
                                <strong>商家代理级别说明：</strong>
                            </td>
                            <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextBox_AgentlevelMemo" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style3">
                                <strong>产品价格：</strong></td>
                            <td align="left" bgcolor="#ecf5ff" height="35">

                                <div align="left">
                                    <table id="tab">
                                        <asp:Literal ID="Literal_Agent_Percent_Line" runat="server" Text=""></asp:Literal>
                                    </table>
                                </div>
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
                            <td align="center" height="35" colspan="2">&nbsp;
                            &nbsp;
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
