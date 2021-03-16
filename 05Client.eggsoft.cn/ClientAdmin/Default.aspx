<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin.Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title><%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>--微云基石微店后台管理系统</title>
    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //document.domain = 'eggsoft.cn';
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

    <script type="text/javascript" defer="defer">
        //debugger;
       // Iframe1.src = document.getElementById("Iframe1_src").value;
    </script>

</head>
<body style="background-color: #87CEFA;">
    <form id="Form1" method="post" runat="server">

        <input id="Iframe1_src" type="hidden" runat="server" />
        <div id="def_head">
            <asp:Label ID="Label_INC" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
        </div>
        <div class="main_t">
            <span>功能导航</span>
        </div>

        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td align="center" nowrap valign="middle" id="frmTitle" width="240">
                    <iframe runat="server" frameborder="0" id="carnoc" name="carnoc" scrolling="no" src="/ClientAdmin/Left.aspx" style="z-index: 2; width: 240px; height: 100%"></iframe>
                </td>
                <td bgcolor="#a4b6d7" style="width: 9pt">
                    <table border="0" cellpadding="0" cellspacing="0" height="100%">
                        <tr>
                            <td style="height: 100%; width: 13px;" onclick="switchSysBar()">
                                <font style="font-size: 9pt; cursor: default; color: #ffffff">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <span class="navPoint" id="switchPoint" title="关闭/打开左栏"></span><br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    屏幕切换 </font>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 100%" valign="top">
                    <%--<iframe frameborder="0" id="Iframe1888" runat="server" scrolling="auto" name="main" src="/ClientAdmin/right.aspx" style="width:100%;height:96%;"></iframe>--%>
                    <iframe frameborder="0" id="Iframe1" runat="server" scrolling="auto" name="main" src="/ClientAdmin/right.aspx"  style="width:100%;height:96%;"></iframe>
                </td>
            </tr>
        </table>
        <script>
            if (window.screen.width < '1024') { switchSysBar() }
        </script>
    </form>
</body>
</html>

