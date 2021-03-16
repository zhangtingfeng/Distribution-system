<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Long2ShortUrl.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.Long2ShortUrl" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BasicInfo</title>

    <script type="text/javascript" script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="style4">
                    <font face="宋体">
                        <strong>长链接：</strong></font></td>

                <td bgcolor="#ecf5ff" style="width: 80%;">
                    <font face="宋体">
                        <asp:TextBox ID="text_WeiXinUserName_Long" runat="server" Width="500px"
                            ToolTip="" AutoCompleteType="Disabled" CssClass="l_input" MaxLength="1000"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ErrorMessage="不能为空!" ControlToValidate="text_WeiXinUserName_Long"></asp:RequiredFieldValidator>
                    </font>


                </td>
            </tr>

            <tr class="tdbg" bgcolor="#ecf5ff">
                <td align="center" class="style5">&nbsp;
                </td>
                <td align="center" bgcolor="#ecf5ff" height="45">
                    <div align="left">
                        &nbsp;
                <asp:Button ID="btnAdd" runat="server" Text=" 生 成 " Width="72px" OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                    </div>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="auto-style1">
                    <font face="宋体">
                        <strong>短链接：</strong></font></td>
                <td bgcolor="#ecf5ff" class="auto-style1">
                    <font face="宋体">
                        <asp:TextBox ID="text_Short" runat="server" Width="500px"
                            AutoCompleteType="Disabled" ToolTip="" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    </font>


                </td>
            </tr>


           
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="auto-style1">
                    <font face="宋体">
                        <strong>微信在线服务：</strong></font></td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_LinkWeiXin" runat="server" Target="_blank">商户个人的微信推广二维码请到基本资料中上传</asp:HyperLink>
                </td>
            </tr>

            <%=pub__addGoodAndGoodClassShortUrl%>
        </table>
    </form>
</body>
</html>