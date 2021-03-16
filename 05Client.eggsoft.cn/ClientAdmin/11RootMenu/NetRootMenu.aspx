<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetRootMenu.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._11RootMenu.NetRootMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜单管理</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var jsPost = function (action, values) {
            var id = Math.random();
            document.write('<form id="post' + id + '" name="post' + id + '" action="' + action + '" method="post">');
            for (var key in values) {
                /// document.write('<input type="hidden" name="' + key + '" value="' + values[key] + '" />');

                document.write('<textarea name="' + key + '" rows="2" cols="20" >' + values[key] + '</textarea>');

            }
            document.write('</form>');
            document.getElementById('post' + id).submit();
        }


        function MakeMenuThis() {
            var fullStr1 = "WeiXinMenu_Set.aspx?type=MakeThisMenuPost";

            jsPost(fullStr1, {
                'ReturnURL': window.location.href
            });
        }




        function postwith(to, Content) {
            var myForm = document.create_r_rElement("form");
            myForm.method = "post";
            myForm.action = to;

            var myInput = document.create_r_r_rElement_x("input");
            myInput.setAttribute("name", "Text");
            myInput.setAttribute("value", Content);
            myForm.a(myInput);

            document.body.a(myForm);
            myForm.submit();
            document.body.removeChild(myForm);
        }




    </script>
    <style>
        td {
            font-size: 12px;
            text-align: left;
            width: 432px;
        }

        .style1 {
            width: 85px;
        }

        .auto-style1 {
            width: 37%;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;" class="altrowstable">
                <tr>
                    <th colspan="2" class="title">微店底部菜单管理 </th>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Panel ID="Panel1" Width="300px" CssClass="styleMiddle" runat="server" align="left">
                            <asp:TreeView ID="TreeView_Menu" runat="server">
                            </asp:TreeView>
                        </asp:Panel>

                    </td>
                    <td>请输入网址。查找网址方法：在微信中当前页中触摸右上角，复制链接，把该链接通过QQ或者微信电脑版发送到电脑上。<br />
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td class="style1">菜单排序</td>
                                <td>
                                    <asp:TextBox ID="TextBox_Pos" runat="server" ToolTip="菜单顺序不能为空，序号小的排前面">0</asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_OnlyNum"
                                        runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_Pos"
                                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Pos" runat="server"
                                        ErrorMessage="菜单顺序不能为空，序号小的排前面(在手机上也就是排下面)!" ControlToValidate="TextBox_Pos"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td>菜单名称：  </td>
                                <td>
                                    <asp:TextBox ID="TextBox_MenuName" runat="server" ToolTip="公众平台菜单不能为空"
                                        MaxLength="6" Height="28px">逛街</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ErrorMessage="链接不能为空!" ControlToValidate="TextBox_MenuName"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">菜单内容：</td>
                                <td>
                                    <asp:TextBox ID="TextBox_MenuContent" runat="server" Width="496px"
                                        ToolTip="请输入http网址 或者 相应素材ID" Height="28px" MaxLength="120">http://eggsoft.cn</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_http_Resource" runat="server"
                                        ErrorMessage="请输入http网址"
                                        ControlToValidate="TextBox_MenuContent"></asp:RequiredFieldValidator>


                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="Button_NewRoot" runat="server" Text="新增根菜单"
                                        OnClick="Button_NewRoot_Click" />
                                    &nbsp;&nbsp;&nbsp;          
                                        <asp:Button ID="Button_NewChildMenu" runat="server" Text="新增菜单"
                                            OnClick="Button_NewChildMenu_Click" />
                                    &nbsp;&nbsp;&nbsp;
						<asp:Button ID="Button_EditMenu" runat="server" Text="保存菜单"
                            OnClick="Button_EdiMenu_Click" />
                                    &nbsp;&nbsp;&nbsp;
						<asp:Button ID="Button_DeleteMenu" runat="server" Text="删除菜单"
                            OnClick="Button_DeleteMenu_Click" />
						<asp:Button ID="Button_ReadDefaultMenu0" runat="server" Text="读取默认" OnClick="Button_ReadDefaultMenu0_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>

        </div>
    </form>
</body>
</html>
