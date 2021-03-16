<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="_03WAWapShop_Oliver._Admin._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店综合后台管理系统</title>
    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <style type="text/css">
        .navPoint {
            color: white;
            cursor: hand;
            font-family: Webdings;
            font-size: 9pt;
        }

        .a2 {
            background-color: #A4B6D7;
        }

        body {
            margin: 0px;
        }
    </style>
    <script>
        if (self != top) { top.location = self.location; }
        function switchSysBar() {
            if (switchPoint.innerText == 3) {
                switchPoint.innerText = 4
                document.all("frmTitle").style.display = "none"
            } else {
                switchPoint.innerText = 3
                document.all("frmTitle").style.display = ""
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div id="def_head">
            <asp:Label ID="Label_INC" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
        </div>
        <div class="main_t">
            <span>功能导航</span>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td align="center" nowrap valign="middle" id="frmTitle" width="240">
                    <iframe frameborder="0" id="carnoc" name="carnoc" scrolling="no" src="Left.aspx"
                        style="z-index: 2; width: 240px; height: 100%"></iframe>
                </td>
                <td bgcolor="#EFEFF1" style="width: 9pt">
                    <table border="0" cellpadding="0" cellspacing="0" height="100%">
                        <tr>
                            <td style="height: 100%; width: 13px;" onclick="switchSysBar()">
                                <font style="font-size: 9pt; cursor: default; color: #ffffff">
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <span class="navPoint" id="switchPoint" title="关闭/打开左栏">3</span><br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    屏幕切换 </font>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 100%" valign="top">
                    <iframe frameborder="0" id="Iframe1" scrolling="auto" name="main" src="/_Admin/tab_ShopClient/UserManage.aspx"
                        style="width: 100%; height: 96%"></iframe>
                </td>
            </tr>
        </table>
        <script>
            if (window.screen.width < '1024') { switchSysBar() }
        </script>
    </form>
</body>
</html>
