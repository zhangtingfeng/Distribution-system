<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinMenu.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.WeiXinMenu" %>

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
                    <th colspan="4" class="title">公众平台菜单管理 </th>
                </tr>
                <tr>
                    <tr>
                        <td class="stylePercent40 styleMiddle" rowspan="3">
                            <asp:Panel ID="Panel1" Width="300px" CssClass="styleMiddle" runat="server" >
                                <asp:TreeView ID="TreeView_Menu" runat="server" align="left">
                                </asp:TreeView>
                            </asp:Panel>
                            <br /><br /><br /><br /><br />

                            <asp:Panel ID="Panel2" Width="300px" CssClass="styleMiddle" runat="server" >
                                <asp:Button ID="ButtonReReadRootMenu" runat="server" Text="同步应用管理的网站底部菜单" OnClick="ButtonReReadRootMenu_Click" />
                            </asp:Panel>
                        </td>
                        <td>开发者URL（用于接收用户信息）：<asp:Label ID="Label_Development" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>网页请输入网址。消息回复。请在素材编辑中查找ID并输入。<a target="_blank" href="Resource-1.aspx">查看文本消息</a>
                            <a target="_blank" href="Resource-2.aspx">查看单图文</a> <a target="_blank" href="Resource-3.aspx">查看多图文</a><br />
                            <br />
                            <table style="width: 100%;">
                                <tr style="display: none;">
                                    <td class="style1">菜单排序：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Pos" runat="server" ToolTip="菜单顺序不能为空，序号小的排前面">0</asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_OnlyNum"
                                            runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_Pos"
                                            ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Pos" runat="server"
                                            ErrorMessage="菜单顺序不能为空，序号小的排前面!" ControlToValidate="TextBox_Pos"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                 菜单名称：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox_MenuName" runat="server" ToolTip="公众平台菜单不能为空"
                                            MaxLength="14">请输入菜单名称</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ErrorMessage="公众平台菜单不能为空!" ControlToValidate="TextBox_MenuName"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">菜单类型：</td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonList_View_Click" runat="server"
                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                            OnSelectedIndexChanged="RadioButtonList_View_Click_SelectedIndexChanged"
                                            ToolTip="请输入，如果含有子菜单，该处功能自动失效">
                                            <asp:ListItem Value="1">文本回复</asp:ListItem>
                                            <asp:ListItem Value="2">单图文回复</asp:ListItem>
                                            <asp:ListItem Value="3">多图文回复</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="4">网页</asp:ListItem>
                                            <asp:ListItem Value="13">高级模板回复</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">菜单内容：</td>
                                    <td>
                                        <asp:TextBox ID="TextBox_MenuContent" runat="server" Width="496px"
                                            ToolTip="请输入http网址 或者 相应素材ID" Height="25px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_http_Resource" runat="server"
                                            ErrorMessage="请输入http网址 或者 相应素材ID"
                                            ControlToValidate="TextBox_MenuContent" Visible="False"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Http"
                                            runat="server" ControlToValidate="TextBox_MenuContent" ErrorMessage="这不是网页地址"
                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                                            Visible="False"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_MenuContent"
                                            runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_MenuContent"
                                            ValidationExpression="^[0-9]*$" Visible="False"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_MenuContent" runat="server"
                                            ErrorMessage="菜单顺序不能为空，序号小的排前面!"
                                            ControlToValidate="TextBox_MenuContent" Visible="False"></asp:RequiredFieldValidator>

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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
                                目前自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替。请注意，<b>创建自定义菜单后，由于微信客户端缓存，需要24小时微信客户端才会展现出来。</b>建议测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。
                            </p>
                            <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
                                目前自定义菜单接口可实现两种类型按钮，如下：
                            </p>
                            <pre style="font-style: normal; font-variant: normal; font-weight: normal; font-size: 12px; font-family: 'MicroSoft YaHei', 'Courier New', 'Andale Mono', monospace; padding: 5px 10px; border: 1px solid rgb(204, 204, 204); color: rgb(51, 51, 51); background-color: rgb(248, 248, 248); line-height: 20px; white-space: pre-wrap; word-wrap: break-word; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; margin-left: 10px; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px;"><b>click：（文本回复、单图文回复、多图文回复）</b>
用户点击click类型按钮后，微信服务器会通过消息接口推送消息类型为event	的结构给开发者（参考消息接口指南），并且带上按钮中开发者填写的key值，开发者可以通过自定义的key值与用户进行交互；
<b>view：（网页）</b>
用户点击view类型按钮后，微信客户端将会打开开发者在按钮中填写的url值	（即网页链接），达到打开网页的目的，建议与网页授权获取用户基本信息接口结合，获得用户的登入个人信息。
</pre>
                            <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
                                <br />
                                提交菜单申请
			   <img title="提交菜单申请" onclick="MakeMenuThis()" style="cursor: pointer" src="../skin/images/tj.png">
                                <br />
                            </p>
                        </td>
                    </tr>
            </table>

        </div>
    </form>
</body>
</html>