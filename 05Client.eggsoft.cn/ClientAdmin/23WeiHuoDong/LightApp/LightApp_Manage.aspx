<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightApp_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.LightApp.LightApp_Manage" %>

<%--<%@ Register Src="../../Control/UploadControl/Upload.ascx" TagName="Upload" TagPrefix="uc1" %>
<%@ Register Src="../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect"--%>
    <%--TagPrefix="uc2" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <link href="../../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        .style1
        {
            color: #CC0000;
        }
        .style2
        {
            height: 36px;
            width: 150px;
        }
        .style3
        {
            width: 150px;
        }
        .auto-style1 {
            color: #FF3300;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
       <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">                   
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                        align="center" border="0">
                        <tr class="title" bgcolor="#a4b6d7">
                            <td align="center" colspan="2" height="25" style="text-align: center">
                                <strong>轻应用模块</strong>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td bgcolor="#e3e3e3" class="style2 styleRight">
                                <div align="right">
                                    <strong>轻应用名称：</strong></div>
                            </td>
                            <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtTitle" runat="server" Width="376px"></asp:TextBox>
                                <font face="宋体"><span class="style1"><strong>*</strong></span></font><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="名称不能为空!" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td bgcolor="#e3e3e3" class="style2 styleRight">
                                <font face="宋体">
                                    <div align="right">
                                        <strong>轻应用描述：</strong></div>
                                </font>
                            </td>
                            <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                <font face="宋体">
                                    <asp:TextBox ID="txtTitleDesc" runat="server" Width="839px" Height="48px" TextMode="MultiLine"></asp:TextBox>
                                </font>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td bgcolor="#e3e3e3" class="style2 styleRight">
                                <font face="宋体">
                                    <div align="right">
                                        <strong>声音选择<br />
                                        （<span class="auto-style1">仅限微云基石VIP客户使用</span>）：</strong></div>
                                </font>
                            </td>
                            <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                <asp:FileUpload ID="FileUpload_Mp3" runat="server" />
                                <asp:HyperLink ID="HyperLink__Mp3path" runat="server"></asp:HyperLink>
                            &nbsp; 限定文件大小5M 上传时间不超过10分钟</td>
                        </tr>
                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3">
                                &nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <div align="left">
                                    &nbsp;
                                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click">
                                    </asp:Button></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>
