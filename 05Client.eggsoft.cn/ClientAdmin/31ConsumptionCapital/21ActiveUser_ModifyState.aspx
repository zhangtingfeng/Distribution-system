<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="21ActiveUser_ModifyState.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._09System_Status._21ActiveUser_ModifyState" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>修改激活状态</title>
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
                                <td align="center" colspan="2" height="25" style="text-align: center; font-size: 30px;">修改运营中心</td>
                            </tr>



                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>当前用户微店号：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_UserID_ShopUserID" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>联系人：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_ContactMan" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                    <div align="right"><strong>当前用户昵称：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" class="auto-style3">
                                    <div align="left">
                                        <asp:Label ID="Label_Nickname" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                             <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                    <div align="right"><strong>激活状态：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" class="auto-style3">
                                    <div align="left">
                                        <asp:CheckBox ID="CheckBox1ActiveAccount" runat="server" />
                                        
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
