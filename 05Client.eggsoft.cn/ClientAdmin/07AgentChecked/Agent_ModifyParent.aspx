<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_ModifyParent.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Agent_ModifyParent" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>代理管理</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .borderClass td {
            text-align: right;
            padding-left: 5px;
        }

        .auto-style1 {
            width: 20%;
        }

        .auto-style2 {
            width: 20%;
            height: 22px;
        }

        .auto-style3 {
            height: 22px;
        }
    </style>

    <script type="text/javascript">

        function checkAll(obj) {
            $('input[type="checkbox"]').attr("checked", obj.checked);

        }
    </script>
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
                                <td align="center" colspan="2" height="25" style="text-align: center; font-size: 30px;">修改上级</td>
                            </tr>




                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理联系人：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_ContactMan" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理店铺名：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_ShopClientName" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                    <div align="right"><strong>选择上级：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                    <div align="left" style="margin-right: 100px;">

                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                按照昵称/姓名/电话查找<asp:TextBox ID="TextBox_NickName" runat="server" AutoPostBack="True" OnTextChanged="TextBox_NickName_TextChanged"></asp:TextBox>
                                                <br />按照用户ID号查找(0表示不设上级)<asp:TextBox ID="TextBox_ShopUserID" runat="server" AutoPostBack="True" OnTextChanged="TextBox_ShopUserID_TextChanged"></asp:TextBox>
                                                <asp:Image ID="Image_HeadURL" runat="server" Width="40px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1"></td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="auto-style1">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;<br />
                                        &nbsp;<asp:Button ID="btnAdd" runat="server" Text=" 保 存 " Width="72px" OnClick="btnAdd_Click"></asp:Button>
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
