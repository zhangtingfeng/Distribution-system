<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInfo.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.BasicInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BasicInfo</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <td align="left" colspan="2" height="25">开发者URL（用于接收用户信息）：<asp:HyperLink ID="HyperLink_WeiXin_Developmebt_URL" runat="server" Target="_blank">[HyperLink_WeiXin_Developmebt_URL]</asp:HyperLink>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>公众号服务号微信号：</strong></font></td>

                <td bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_WeiXinHao" runat="server" Width="376px"
                            ToolTip="微信用户搜索关注使用" AutoCompleteType="Disabled" CssClass="l_input" MaxLength="30"></asp:TextBox>


                        <asp:Label ID="Label1" runat="server" Text="微信用户搜索关注使用"></asp:Label>


                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="微信号不能为空!" ControlToValidate="TextBox_WeiXinHao"></asp:RequiredFieldValidator>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>微信用户名：</strong></font></td>

                <td bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="text_WeiXinUserName" runat="server" Width="376px"
                            ToolTip="可能是你的邮箱 如 2010535775@qq.com" AutoCompleteType="Disabled" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>微信密码：</strong></font></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="text_WeiXinUserPassword" runat="server" Width="376px"
                            AutoCompleteType="Disabled" ToolTip="您的微信登陆密码" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>Token：</strong></font></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_Token" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="100"></asp:TextBox>
                        *<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Token不能为空!" ControlToValidate="Textbox_Token"></asp:RequiredFieldValidator>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>微信开发者 AppId：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="txtTitle_WeiXinAppId" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="100"></asp:TextBox>
                        *自定义菜单必填</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>微信开发者 Secret：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_WeiXinAppSecret" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="100"></asp:TextBox>
                        *自定义菜单必填</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>EncodingAESKey(消息加解密密钥：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_EncodingAESKey" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>微信引导用户关注页面：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_GuideSubscribePageFromWeiXinD" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>微信支付商户ID PartnerId：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_WeiXinPayID" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>商户支付密钥 Key PartnerKey：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_PartnerKey" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>访客消息通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_VisitMessage" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>智能访客消息通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox1_WisdomVisitMessage" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>成功付款通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_PayMessage" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>礼包领取通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_LiBaoLingQuTongZhi" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>会员充值通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_TempleInputMoneyMessage" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>到账通知 模板ID：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox1AccountNotice" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                        *行业IT科技 - 互联网|电子商务</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3" style="display: none;">
                <td align="right" class="style4">
                    <strong>证书文件路径apiclient_cert.pem：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <asp:FileUpload ID="FileUpload_Apiclient_cert_Pem" runat="server" Width="376px" />
                    <asp:Label ID="Label_Apiclient_cert_Pem" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>支付证书文件密码：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="Textbox_Apiclient_cert_Pem_Password" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="250"></asp:TextBox>
                    </font></td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center"  colspan=2 class ="style4">
                    <strong>
                        微信小程序配置
                    </strong>
                </td>
                
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>小程序名称：</strong></font></td>

                <td bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_SmallProgram_name" runat="server" Width="376px"
                            ToolTip="微信用户搜索关注使用" AutoCompleteType="Disabled" CssClass="l_input" MaxLength="30"></asp:TextBox>


                        <asp:Label ID="Label2" runat="server" Text="微信用户搜索关注使用"></asp:Label>


                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="微信号不能为空!" ControlToValidate="TextBox_WeiXinHao"></asp:RequiredFieldValidator>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>小程序用户名：</strong></font></td>

                <td bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_SmallProgram_UsersName" runat="server" Width="376px"
                            ToolTip="可能是你的邮箱 如 2010535775@qq.com" AutoCompleteType="Disabled" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    </font>


                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>小程序微信密码：</strong></font></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_SmallProgram_PWD" runat="server" Width="376px"
                            AutoCompleteType="Disabled" ToolTip="您的微信登陆密码" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    </font>


                </td>
            </tr>
            
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>小程序微信开发者 AppId：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_SmallProgram_APPID" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="100"></asp:TextBox>
                        *自定义菜单必填</font></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <strong>小程序微信开发者 Secret：</strong></td>
                <td height="35" bgcolor="#ecf5ff">
                    <font face="宋体">
                        <asp:TextBox ID="TextBox_SmallProgram_Secret" runat="server" Width="376px"
                            AutoCompleteType="Disabled" CssClass="l_input" MaxLength="100"></asp:TextBox>
                        *自定义菜单必填</font></td>
            </tr>

            <tr class="tdbg" bgcolor="#ecf5ff">
                <td align="center" class="style5">&nbsp;
                </td>
                <td align="center" bgcolor="#ecf5ff" height="45">
                    <div align="left">
                        &nbsp;
                <asp:Button ID="btnAdd" runat="server" Text=" 保 存 " Width="72px" OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
