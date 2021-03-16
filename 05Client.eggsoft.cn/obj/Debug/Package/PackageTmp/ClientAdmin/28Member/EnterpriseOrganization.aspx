<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseOrganization.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._28Member.EnterpriseOrganization1" %>

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
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;" class="altrowstable">
                <tr>
                    <th colspan="2" class="title">企业组织机构管理 </th>
                </tr>
                    <tr>
                        <td class="stylePercent40 styleMiddle">
                            <asp:Panel ID="Panel1" Width="300px" CssClass="styleMiddle" runat="server" >
                                <asp:TreeView ID="TreeView_Organization" runat="server" align="left" Width="201px" Height="251px">
                                </asp:TreeView>
                            </asp:Panel>
                           

                        </td>
                        <td>
                            
                            <table style="width: 110%;">
                                <tr>
                                    <td class="style1">组织机构排序：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Pos" runat="server" ToolTip="组织机构顺序不能为空，序号小的排前面">0</asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_OnlyNum"
                                            runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_Pos"
                                            ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Pos" runat="server"
                                            ErrorMessage="组织机构顺序不能为空，序号小的排前面!" ControlToValidate="TextBox_Pos"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">组织机构名称：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox_OrganizationName" runat="server" ToolTip="组织机构不能为空"
                                            MaxLength="14"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ErrorMessage="组织机构不能为空!" ControlToValidate="TextBox_OrganizationName"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                       <asp:Button ID="ButtonSaveLevel" runat="server" Text="新增为同级机构"
                                            OnClick="Button_SaveLevelOrganization_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="Button_NewChildOrganization" runat="server" Text="新增为子机构"
                                            OnClick="Button_NewChildOrganization_Click" />
                                        &nbsp;&nbsp;
						<asp:Button ID="Button_EditSaveOrganization" runat="server" Text="保存机构"
                            OnClick="Button_EditSaveOrganization_Click" />
                                        &nbsp;&nbsp;
						<asp:Button ID="Button_DeleteOrganization" runat="server" Text="删除机构"
                            OnClick="Button_DeleteOrganization_Click" />
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
