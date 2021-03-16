<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterOpenID.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._14System_WeiXin.RegisterOpenID" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>RegisterOpenID</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(document).ready(function () {
            var iID = setTimeout("reflesh()", 4000); //单位毫秒或者var iID=setTimeout(clock,2000);
        });
        
        function reflesh() {
            var varid = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString()%>";
            var varID_ForCheck = "<%=pubStringUserListID_ForCheck_%>";

            var urlData = "ID=" + varid + "&pubStringUserListID_ForCheck_=" + varID_ForCheck + "";

            $.ajax({
                type: "POST",
                url: "Handler_SaoYiSao.ashx",
                data: urlData,
                success: function (msg) {
                    if (msg == "1") {
                        alert("关联成功，用户可以与您对话了.已为关联账号增加2000元购物红包.反复扫描,赠送更多！");
                        window.location.href = '/ClientAdmin/10tab_ShopClient/BoardINC_Manage.aspx?type=Modify';
                    }
                }
            });
            var iID = setTimeout("reflesh()", 4000); //单位毫秒或者var iID=setTimeout(clock,2000);

        };
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="99%" border="0">
            <tr class="title">
                <th class="title" valign="middle" width="100%" colspan="4" style="height: 24px">
                    <%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>
                关联微信相关信息
                </th>
            </tr>
            <tr>
                <td class="border" colspan="3" style="text-align: center;">
                    <table cellspacing="1" cellpadding="2" width="748" border="0" align="center">
                        <tr>
                            <td nowrap class="style1" height="35">请扫描二微码进行关联：
                            </td>
                            <td class="tdbg">
                                <asp:Image ID="Image_RegisterOpenID" runat="server" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td nowrap class="style1" colspan="2" height="35">微信扫描后，用户输入文本内容“<asp:Literal ID="Literal_ID" runat="server"></asp:Literal>
                                #对话内容”，即可发送你的微信上面。
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;
                </td>
            </tr>
        </table>
        <div class="Loadingdiv" id="Loading">
        </div>
    </form>
</body>
</html>
