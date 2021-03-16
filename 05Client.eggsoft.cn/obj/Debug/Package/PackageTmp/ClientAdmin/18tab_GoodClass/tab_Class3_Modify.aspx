<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Class3_Modify.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class3_Modify" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>ClassModifySmall</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="Form1" method="post" runat="server">

        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="45"><strong>修改三级分类</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>一级版块：</strong>
                </td>
                <td height="35" bgcolor="#ecf5ff">
                    <asp:DropDownList ID="ddlBigClass1" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlBigClass1_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
								<asp:DropDownList ID="ddlBigClass2" runat="server">
                                </asp:DropDownList>
                    <asp:Label ID="lblID" runat="server" Visible="False">Label</asp:Label></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>三级名称：</strong>
                </td>
                <td height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtSmallClassName" runat="server" Width="260px" CssClass="l_input"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="小类名称不能为空!" ControlToValidate="txtSmallClassName"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" height="35">
                    <strong>导航小图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Small" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Small" runat="server" />
                    导航小图标 建议尺寸 高度宽度不大于50px 。png格式。模板4适用
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" height="35">
                    <strong>导航大图标：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Nav_PIC_Big" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Nav_PIC_Big" runat="server" />
                    导航大图标 建议尺寸 宽度640px，高度480px 。jpg格式。模板4适用
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="22">
                    <strong>排列位置：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="22">
                    <asp:TextBox ID="txtSmallClassPos" runat="server" Width="260px" CssClass="l_input">0</asp:TextBox>
									<asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="排列位置不能为空!" ControlToValidate="txtSmallClassPos"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSmallClassPos"
                            ErrorMessage="排列位置必须是数字类型！" ValidationExpression="^\d{1,}$"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td style="display: none;" align="right">
                    <strong>版块属性：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" style="display: none;">
                    <div align="left">
                        <asp:CheckBox ID="cbIsShow" runat="server" Text="显示" Checked="True"></asp:CheckBox>
                        <asp:CheckBox ID="cbIsLock" runat="server" Text="锁定" />
                    </div>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#ecf5ff">
                <td>&nbsp;
                </td>
                <td align="center" bgcolor="#ecf5ff" height="45">
                    <div align="left">
                        <asp:Button ID="btnModify" runat="server" Width="72px" Text=" 修  改 " OnClick="btnModify_Click" CssClass="b_input"></asp:Button>
                    </div>
                </td>
            </tr>
           
          
        </table>
    </form>

</body>
</html>
