<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Class1_Modify.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class1_Modify" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ClassAddBig</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table  cellspacing="0" cellpadding="0" width="100%" border="0" align="center">

            <tr class="title">
                <td align="center" colspan="2" height="25"><strong>修改一级分类<asp:Label ID="lblBigClassID" runat="server" Text="Label" Visible="False"></asp:Label></strong></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" width="150" height="35">
                    <strong>版块名称：</strong>
                </td>
                <td width="254" bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtBigClassName" runat="server" Width="260px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="txtBigClassName"
                        Display="Dynamic" ErrorMessage="大类名称不能为空!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>导航小图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Small" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Small" runat="server" />
                    导航小图标 建议尺寸 高度宽度不大于50px 。png格式。模板4适用
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>导航大图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Big" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Big" runat="server" />
                    导航大图标 建议尺寸 宽度640px，高度480px 。jpg格式。模板4适用
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>排列位置：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:TextBox ID="txtBigClassPos" runat="server" Width="260px" CssClass="l_input">0</asp:TextBox>
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtBigClassPos"
                            Display="Dynamic" ErrorMessage="排列位置不能为空!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBigClassPos"
                            Display="Dynamic" ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td style="display: none;" align="right">
                    <strong>是否显示：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" style="display: none;">
                    <div align="left">
                        <asp:CheckBox ID="cbIsShow" runat="server" Text="显示版块" Checked="True"></asp:CheckBox>
                    </div>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#ecf5ff">
                <td>&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnModify" runat="server" Text=" 修  改 " Width="72px" OnClick="btnModify_Click" CssClass="b_input"></asp:Button>
                </td>
            </tr>           
        </table>
    </form>
</body>
</html>

