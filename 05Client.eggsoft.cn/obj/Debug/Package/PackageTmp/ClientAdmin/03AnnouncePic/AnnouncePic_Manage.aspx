<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnnouncePic_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._03AnnouncePic.AnnouncePic_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceAdd</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function uploadComplete(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image_Small").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }

        function uploadError(sender, args) {
            alert("上传错误");
        }


        function CheckClientValidate() {
            if (Page_ClientValidate()) {

                if ($("#txtTitle").val().length == 0) {
                    alert("标题不能为空！");
                    $("#txtTitle").focus();
                    return false;
                }
                if ($("#Link0").val().length == 0) {
                    alert("链接不能为空！没有请使用默认的#");
                    $("#Link0").focus();
                    return false;
                }


                var srcUp = $("#Image_Small").attr("src");
                if (srcUp.toString().length == 0) {
                    alert("相关图片必须选择！");
                    $("#Image_Small").focus();
                    return false;
                }
                return true;
            }
        }

    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>轮 播 管 理</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>标题：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtTitle" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style3">
                    <strong>&nbsp; 缩略图：</strong></td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="ImageButton" runat="server" Height="50px" />
                    <asp:FileUpload ID="FileUpload_Button" runat="server" />
                    <br />
                    1.经过多次测试，图片大小建议640*320的jpg格式的图片是最佳的视觉效果。<br />
                    2.满屏的请使用640*940的jpg格式的.视觉效果良好。</td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style3">
                    <strong>链接：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Link0" runat="server" Width="376px" CssClass="l_input">#</asp:TextBox>
                    <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="链接不能为空!"
                        ControlToValidate="Link0"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="45">
                    <strong>排列位置：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtClassPos" runat="server" Width="260px" CssClass="l_input">0</asp:TextBox>
                    排序从小到大，0最小<asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtClassPos"
                        Display="Dynamic" ErrorMessage="排列位置不能为空!"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtClassPos"
                        Display="Dynamic" ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$"></asp:RegularExpressionValidator>
                    &nbsp;
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
