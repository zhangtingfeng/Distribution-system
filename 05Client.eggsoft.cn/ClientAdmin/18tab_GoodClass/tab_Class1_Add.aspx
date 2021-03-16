<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Class1_Add.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class1_Add" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>ClassAddBig</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            width="503" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>添加一级分类</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="36" width="150">
                    <strong>分类名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtBigClassName" runat="server" Width="260px" CssClass="l_input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="大类名称不能为空!" ControlToValidate="txtBigClassName" Display="Dynamic"></asp:RequiredFieldValidator></td>
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
                        <br />
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="排列位置不能为空!" ControlToValidate="txtBigClassPos" Display="Dynamic"></asp:RequiredFieldValidator><br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBigClassPos"
                            ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#ecf5ff">
                <td align="center">&nbsp;
                </td>
                <td align="center" bgcolor="#ecf5ff" height="45">
                    <div align="left">
                        &nbsp;
			<asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                    </div>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
