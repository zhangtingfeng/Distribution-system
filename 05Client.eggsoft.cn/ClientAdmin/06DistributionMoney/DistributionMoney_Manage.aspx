<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributionMoney_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._06DistributionMoney.DistributionMoney_Manage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>分销方案提成级别</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .borderClass td {
            text-align: right;
        }

        .auto-style1 {
            width: 20%;
        }
    </style>


</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="TableIDDD" height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <br>
                        <br>
                        <br>
                        <table class="borderClass" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title" bgcolor="#a4b6d7">
                                <td align="center" colspan="2" height="25" style="text-align: center; font-size: 30px;">分销方案提成级别</td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>分销命名：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <font face="宋体">
                                            <asp:TextBox ID="TextBox_Name" runat="server" Width="376px" ToolTip="分销命名" MaxLength="20">普通业务</asp:TextBox>
                                        </font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="分销命名不能为空!"
                                            ControlToValidate="TextBox_Name"></asp:RequiredFieldValidator>

                                    &nbsp;&nbsp;&nbsp;&nbsp; 以下所有数据相加应该等于100</div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:DropDownList ID="DropDownList_Partner0" runat="server" Height="16px">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>一级代理：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:DropDownList ID="DropDownList_Partner1" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>二级代理：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:DropDownList ID="DropDownList_Partner2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>商店收入及支付费率：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:TextBox ID="TextBox_ShopGet_FeiLv" runat="server"></asp:TextBox>
                                        微信 、支付宝费率1.5%不等
                                    </div>
                                </td>
                            </tr>



                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="auto-style1">&nbsp;
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
                        &nbsp;</td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>
