<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuaGuaKa_ZhuanZhuanChe.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.GuaGuaKa_ZhuanZhuanChe" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceAdd</title>
    <script src="../../Upload_JS/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../Upload_JS/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="../../Upload_JS/ckfinder/ckfinder.js" type="text/javascript"></script>
     <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <%--<link href="../Image/background.css" rel="stylesheet" type="text/css" />
    <link href="../image/bbs.css" rel="stylesheet" type="text/css">--%>
    <style type="text/css">
        .style4 {
            height: 36px;
            width: 150px;
        }

        .style5 {
        }

        td {
            text-align: left;
        }

        .style6 {
            font-size: large;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%; text-align: left;">
                        <br>

                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title" bgcolor="#a4b6d7" style="text-align: center;">
                                <td align="center" colspan="4" height="25" class="style6"><strong>大转盘&nbsp; 刮刮卡抽奖设置</strong></td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <font face="宋体">特等奖</font><strong>：</strong></td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeTitle_0" runat="server" Width="200px">微商城</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeTitle_0"></asp:RequiredFieldValidator>
                                    </font>


                                </td>
                                <td bgcolor="#e3e3e3" class="style4 styleRight">抽奖总数：</td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount_0" runat="server" Width="152px">1</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeAllCount_0"></asp:RequiredFieldValidator>
                                    </font>


                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                        ControlToValidate="txtTypeAllCount_0"></asp:RegularExpressionValidator>


                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <strong><font face="宋体">一等奖</font>：</strong></td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeTitle_1" runat="server" Width="200px">微官网</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeTitle_1"></asp:RequiredFieldValidator>
                                    </font>


                                </td>
                                <td bgcolor="#e3e3e3" class="style4 styleRight">抽奖总数：</td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount_1" runat="server" Width="152px">5</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeAllCount_1"></asp:RequiredFieldValidator>


                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="txtTypeAllCount_1"></asp:RegularExpressionValidator>


                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <font face="宋体">二等奖：</font></td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeTitle_2" runat="server" Width="200px">服务号</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeTitle_2"></asp:RequiredFieldValidator>
                                    </font>


                                </td>
                                <td bgcolor="#e3e3e3" class="style4 styleRight">抽奖总数：</td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount_2" runat="server" Width="152px">10</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeAllCount_2"></asp:RequiredFieldValidator>


                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="txtTypeAllCount_2"></asp:RegularExpressionValidator>


                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <font face="宋体">三等奖：</font></td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeTitle_3" runat="server" Width="200px">订阅号</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeTitle_3"></asp:RequiredFieldValidator>
                                    </font>


                                </td>
                                <td bgcolor="#e3e3e3" class="style4 styleRight">抽奖总数：</td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount_3" runat="server" Width="152px">20</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeAllCount_3"></asp:RequiredFieldValidator>


                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="txtTypeAllCount_3"></asp:RegularExpressionValidator>


                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">四等<font face="宋体">奖：</font></td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeTitle_4" runat="server" Width="200px">杯子 </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeTitle_4"></asp:RequiredFieldValidator>
                                    </font>


                                </td>
                                <td bgcolor="#e3e3e3" class="style4 styleRight">抽奖总数：</td>
                                <td style="height: 36px; width: 500px;" bgcolor="#ecf5ff">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount_4" runat="server" Width="152px">200</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                            ErrorMessage="奖品不能为空!" ControlToValidate="txtTypeAllCount_4"></asp:RequiredFieldValidator>


                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="txtTypeAllCount_4"></asp:RegularExpressionValidator>


                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <font face="宋体">抽奖总数：</font></td>
                                <td style="height: 36px;" bgcolor="#ecf5ff" colspan="3">
                                    <font face="宋体">
                                        <asp:TextBox ID="txtTypeAllCount" runat="server" Width="152px">10000</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                            ErrorMessage="抽奖总数不能为空!" ControlToValidate="txtTypeAllCount"></asp:RequiredFieldValidator>


                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="txtTypeAllCount"></asp:RegularExpressionValidator>


                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">
                                    <font face="宋体">再次参与抽奖间隔时间：</font></td>
                                <td style="height: 36px;" bgcolor="#ecf5ff" colspan="3">
                                    <font face="宋体">
                                        <asp:TextBox ID="TextBox_CanSecondTime" runat="server" Width="152px">1440</asp:TextBox>
                                        分钟<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="再次参与抽奖间隔时间不能为空!" ControlToValidate="TextBox_CanSecondTime"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                            ErrorMessage="必须输入数字" ValidationExpression="^[0-9]*$"
                                            ControlToValidate="TextBox_CanSecondTime"></asp:RegularExpressionValidator>
                                    </font>


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">运行状态：</td>
                                <td style="height: 36px;" bgcolor="#ecf5ff" colspan="3">
                                    <asp:CheckBox ID="CheckBoxRun" runat="server" Text="是否正在抽奖中" Checked="True" />


                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">刮刮卡：</td>
                                <td style="height: 36px;" bgcolor="#ecf5ff" colspan="3">
                                    <asp:HyperLink ID="HyperLink_GuaGuaKa" runat="server">
                                        <asp:Image ID="Image1_GuaGuaKa" runat="server" />
                                    </asp:HyperLink>
                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" bgcolor="#e3e3e3" class="style4 styleRight">转转车：</td>
                                <td style="height: 36px;" bgcolor="#ecf5ff" colspan="3">
                                    <asp:HyperLink ID="HyperLink_ZhuanZhuanChe" runat="server">
                                        <asp:Image ID="Image2_ZhuanZhuanChe" runat="server" />
                                    </asp:HyperLink>
                                </td>
                            </tr>


                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="style5" colspan="4">&nbsp;
									    <font face="宋体">
                                            <div align="left" style="text-align: center">
                                                &nbsp;
											<asp:Button ID="btnAdd" runat="server" Text=" 保 存 " Width="92px"
                                                OnClick="btnAdd_Click" Height="29px"></asp:Button>
                                            </div>



                                        </font>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>



        </font>


    </form>
</body>
</html>
