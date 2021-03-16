<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightApp_EachPage_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.LightApp.LightApp_EachPage_Manage" %>

<%@ Register Src="~/Control/UploadControl/Upload_MultiSeclect.ascx" TagPrefix="uc1" TagName="Upload_MultiSeclect" %>

<%--<%@ Register src="../../../Control/UploadControl/Upload_MultiSeclect.ascx" tagname="Upload_MultiSeclect" tagprefix="uc1" %>--%>

<%--<%@ Register Src="../../Control/UploadControl/Upload.ascx" TagName="Upload" TagPrefix="uc1" %>
<%@ Register Src="../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect"
    TagPrefix="uc2" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
            height: 36px;
            width: 150px;
        }

        .style3 {
            width: 150px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                            align="center" border="0">
                            <tr class="title" bgcolor="#a4b6d7">
                                <td align="center" colspan="2" height="25" style="text-align: center">
                                    <strong>轻应用模块</strong>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right">
                                        <strong>轻应用页面：</strong>
                                    </div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <%--  <uc2:Upload_MultiSeclect ID="Upload_MultiSeclect1" runat="server" />--%>

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

                                    <uc1:Upload_MultiSeclect runat="server" ID="Upload_MultiSeclect" />
                                    上传640*960尺寸的JPG</td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <font face="宋体">
                                        <div align="right">
                                            <strong>轻应用导航显示：</strong>
                                        </div>
                                    </font>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:CheckBox ID="CheckBox_Nav_Bool" runat="server" Text="是否显示导航"
                                            AutoPostBack="True" OnCheckedChanged="CheckBox_Nav_Bool_CheckedChanged" />
                                    </font>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <font face="宋体">
                                        <div align="right">
                                            <strong>导航名称：</strong>
                                        </div>
                                    </font>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTitleNavName" runat="server" MaxLength="8" Width="376px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtTitleNavName" runat="server"
                                            ErrorMessage="导航名称不能为空!" ControlToValidate="txtTitleNavName"
                                            Enabled="False"></asp:RequiredFieldValidator>
                                    </font></td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <font face="宋体">
                                        <div align="right">
                                            <strong>导航地址：</strong>
                                        </div>
                                    </font>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="Textbox_Address" runat="server" Width="376px"></asp:TextBox>
                                    </font>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Textbox_Address" runat="server" ControlToValidate="Textbox_Address"
                                        ErrorMessage="需要HTTP地址"
                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;#=]*)?"
                                        Enabled="False"></asp:RegularExpressionValidator>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="style3">
                                    <div align="right"><strong>排序位置：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <font face="宋体">
                                        <div align="left">
                                            <asp:TextBox ID="txtMenuPos" runat="server" Width="89px">0</asp:TextBox>
                                            <font face="宋体">数字越大 排序越靠后  <span class="style1"><strong>*</strong></span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ErrorMessage="排序位置不能为空 必须是数字" ControlToValidate="txtMenuPos"
                                                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    ErrorMessage="排序位置不能为空!" ControlToValidate="txtMenuPos"></asp:RequiredFieldValidator>
                                            </font>



                                        </div>
                                    </font>



                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="style3">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;
                                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br>
                        <br>
                        <br>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>