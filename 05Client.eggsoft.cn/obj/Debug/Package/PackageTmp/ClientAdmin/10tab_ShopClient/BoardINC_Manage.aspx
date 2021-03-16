<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardINC_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.BoardINC_Manage1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <style type="text/css">
        .border input {
        }
    </style>
    <title>公司会员资料</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function uploadComplete_Button(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#ImageButton").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }

        function uploadCompleteLogo(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image_Logo").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }

        function uploadError(sender, args) {
            alert("上传错误");
        }


        function CheckClientValidate() {

            if (Page_ClientValidate()) {
                var TextboxUserPassword = $('#TextboxUserPassword').val();
                var TextboxRePassword = $('#TextboxRePassword').val();

                if (TextboxUserPassword != TextboxRePassword) {
                    alert("两次的密码不相同！");
                    return false;
                }

                var srcUp = $("#ImageLogo").attr("src");
                if (srcUp.toString().length == 0) {
                    alert("相关图片必须选择！");
                    return false;
                }
                return true;
            }
            //   Page_BlockSubmit=false;  //当页面中有其他不需要验证的按钮或下拉框时一定要加上这句话，否则其他下拉框第一次提交时不会触发后台代码

        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title" bgcolor="#a4b6d7">
                <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25">公司会员资料
                </th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>用户名：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:Label ID="Label_UserName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>密码：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxUserPassword" runat="server" TextMode="Password" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="TextboxUserPassword"
                        StrengthIndicatorType="BarIndicator" PreferredPasswordLength="12" MinimumNumericCharacters="3"
                        MinimumSymbolCharacters="1" BarIndicatorCssClass="bartype" BarBorderCssClass="barborder">
                    </asp:PasswordStrength>
                    <asp:Label ID="TextBox2_HelpLabel" runat="server" />
                    <asp:Label ID="Label_ModifyTip" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextboxUserPassword" runat="server"
                        ErrorMessage="密码不能为空!" ControlToValidate="TextboxUserPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>重复密码：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxRePassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator5451" runat="server" ControlToCompare="TextboxRePassword"
                        ControlToValidate="TextboxUserPassword" Display="Dynamic" ErrorMessage="两次输入密码是否相同"></asp:CompareValidator>
                    <asp:Label ID="Label_ModifyTip0" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" style="font-weight: 700;">Email：
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Textbox_Email" runat="server" Width="156px" AutoPostBack="True"
                        OnTextChanged="Textbox_Email_TextChanged" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Email不能为空!"
                        ControlToValidate="Textbox_Email"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email格式不对！"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Textbox_Email"></asp:RegularExpressionValidator>
                    <asp:Literal ID="Literal_CheckEmail" runat="server"></asp:Literal>
                    <asp:Button ID="Button_CheckEmail_Click" runat="server" Text="验证Email" OnClick="Button_CheckEmail_Click_Click" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>绑定微信号：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:HyperLink ID="HyperLink_LinkWeiXin" runat="server" NavigateUrl="/ClientAdmin/14System_WeiXin/RegisterOpenID.aspx?type=AskRalation">扫一扫关联商户的微信号</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/ClientAdmin/14System_WeiXin/RegisterOpenID.aspx?type=ClearRalation">清空关联</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label_LinkWeiXin" runat="server" Text="关联微信号，及时和用户沟通交流！" ForeColor="#663300"></asp:Label>
                    </div>
                </td>
            </tr>

           


        </table>
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>公司名称：</strong>
                </td>
                <td height="35" bgcolor="#ecf5ff" width="80%">
                    <asp:TextBox ID="txtINCName" runat="server" Width="376px" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_INCName" runat="server" ErrorMessage="公司名称不能为空!"
                        ControlToValidate="txtINCName"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" style="font-weight: 700;">
                    <strong>公司地址：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxAddress" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>公司类型：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:DropDownList ID="DropDownList_INC" runat="server" Height="20px" Width="157px">
                        <asp:ListItem>公司类型</asp:ListItem>
                        <asp:ListItem>事业单位或社会团体</asp:ListItem>
                        <asp:ListItem>个体经营</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg" style="display: none;" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>按钮图片：</strong><br />
                    (建议大小：130*130)
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="ImageButton" runat="server" Height="50px" />
                    <asp:FileUpload ID="FileUpload_Button" runat="server" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>主营行业：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownList_Class1" runat="server" AutoPostBack="True" Height="20px"
                                OnSelectedIndexChanged="DropDownList_Class1_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Class2" runat="server" AutoPostBack="True" Height="20px"
                                OnSelectedIndexChanged="DropDownList_Class2_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Class3" runat="server" Height="20px" Width="101px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3" style="display: none;">
                <td align="right" height="35" class="style2">
                    <strong>商品排列方式：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:RadioButtonList ID="RadioButtonList_GoodListShowType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">1列</asp:ListItem>
                        <asp:ListItem Value="2">2列</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none;" class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>菜单栏颜色：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <cc2:ColorPicker ID="ColorPicker_MenuBar_Color" runat="server" />
                </td>
            </tr>
            <tr style="display: none;" class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>店铺文字颜色：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <cc2:ColorPicker ID="ColorPicker_Font" runat="server" />
                </td>
            </tr>
            <tr style="display: none;" class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>店铺背景颜色：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <cc2:ColorPicker ID="ColorPicker_BackColor" runat="server" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>Logo图片：</strong><br />
                    建议大小：80*200<br />
                    PNG透明
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="Image_Logo" runat="server" Height="50px" />
                    <asp:FileUpload ID="FileUpload_Logo" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox_AddPic_Auto" runat="server" Text="自动为商品图片添加该水印" />
                    (按照640*400的比例从左上角添加)</td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>联系人姓名：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:TextBox ID="Textbox_RealName" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    </div>
                </td>
            </tr>


            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>联系人职位：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:TextBox ID="Textbox_ContactManPostion" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>性别：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:RadioButtonList ID="RadioButtonList_Sex" runat="server" RepeatDirection="Horizontal"
                            Width="193px">
                            <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                            <asp:ListItem Value="0">女</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" style="font-weight: 700;">
                    <strong>联系手机：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxINCPhone" runat="server" Width="376px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="手机号不能为空!"
                        ControlToValidate="TextboxINCPhone"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>业务介绍：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Textbox_BeiZhu" runat="server" Width="436px" Height="25px"
                        CssClass="l_input" MaxLength="16"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>上传联系人二维码：</strong><br />
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="Image_ContactManErWeiMa" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_ContactManErWeiMa" runat="server" />提供该项可生成微信二维码服务，链接地址请到常用链接中查看</td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>上传微信公众平台二维码：</strong><br />
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="Image_GongZhongPingTaiErWeiMa" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_GongZhongPingTaiErWeiMa" runat="server" />PC顶部显示二维码使用，可直接扫一扫登陆使用</td>
            </tr>


           
            <tr style="display: none;" class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="35">
                    <strong>授权到期时间：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="35">
                    <asp:Literal ID="Literal_Authortime" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <p style="text-align: center">
            <font face="宋体">
                <asp:Button ID="btnAdd" runat="server" Text=" 保存 " Width="100px" OnClick="btnAdd_Click"
                    OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
        </p>
    </form>
</body>
</html>