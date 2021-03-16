<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterOpenID.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._17O2O_Shop.RegisterOpenID" %>

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
            var varO2oID = "<%=pubstrinto2oIDID_%>";
            var varID_ForCheck = "<%=pubStringUserListID_ForCheck_%>";

            var urlData = "O2oID=" + varO2oID + "&pubStringUserListID_ForCheck_=" + varID_ForCheck + "";

            $.ajax({
                type: "POST",
                url: "Handler_SaoYiSaoo2oashx.ashx",
                data: urlData,
                success: function (msg) {
                    if (msg == "1") {
                        alert("关联成功，用户可以与您对话了！");
                        window.location.href = '/ClientAdmin/17O2O_Shop/Board_O2O_ShopOperating.aspx?type=Modify&ID=' + varO2oID;
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
