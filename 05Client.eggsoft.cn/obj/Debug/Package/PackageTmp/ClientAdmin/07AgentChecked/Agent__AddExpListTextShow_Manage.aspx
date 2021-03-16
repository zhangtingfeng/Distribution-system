<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent__AddExpListTextShow_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Agent__AddExpListTextShow_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>代理商自定义字段</title>
    <script src="../../Image/Times.js"></script>

    <script src="/Upload_JS/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="/Upload_JS/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="/Upload_JS/ckfinder/ckfinder.js" type="text/javascript"></script>
    <link href="../../Image/background.css" rel="stylesheet" type="text/css" />
    <script src="../../Image/jquery-1.8.3.js"></script>
    <link href="../../Image/jquery-calendar.css" rel="stylesheet" />
    <script src="../../Image/jquery-calendar.js"></script>


    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />

    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .style2 {
            height: 36px;
            width: 98px;
        }

        .style3 {
            width: 98px;
        }

        .style4 {
            color: #CC00CC;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <br>

                        <br>
                        <br>
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title" bgcolor="#a4b6d7">
                                <td align="center" colspan="2" height="25" style="text-align: center"><strong>代理商自定义字段</strong></td>
                            </tr>
                            <tr class="title" bgcolor="#a4b6d7">
                                <td align="center" colspan="2" height="25" style="text-align: left">系统已默认添加了（店铺名称、联系人姓名、手机号码、支付宝或微信）四个字段，需要更多（如身份证、介绍人）请在下面添加</td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#a4b6d7" class="style2 styleRight">
                                    <div align="right"><strong>自定义字段管理：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Width="831px">
                                                <asp:ListBox ID="ListBox_Item" runat="server" Width="199px"></asp:ListBox>
                                                <font face="宋体">
                                                <asp:Button ID="Button_Del" runat="server" OnClick="Button_Del_Click" Text="删除字段" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
                                                <asp:TextBox ID="TextBox_Item" runat="server"></asp:TextBox>
                                                <asp:Button ID="Button_Add" runat="server" OnClick="Button_Add_Click" Text="增加字段" />

                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#a4b6d7" height="22" class="style3">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;
											<asp:Button ID="btnAdd" runat="server" Text=" 保 存 " Width="72px" OnClick="btnAdd_Click"></asp:Button>
                                    </div>
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
