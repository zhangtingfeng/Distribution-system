<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suggestion_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.Suggestion_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Control/WebUC_DateTime.ascx" TagName="WebUC_DateTime" TagPrefix="uc1" %>
<%@ Register Src="../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Goods Add</title>
    <script src="Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/ckeditor/ckeditor.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/jquery-2.0.3.min.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/ckfinder/ckfinder.js?version=js201709121928" type="text/javascript"></script>

    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="Images/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="Images/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .border input {
        }

        .auto-style1 {
            color: #CC0000;
        }
    </style>
   
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                   
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                        align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25">
                                <strong>意 见  信  息</strong>
                            </th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>商品名称：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtName" runat="server" Width="376px" CssClass="l_input" MaxLength="40"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="标题不能为空!"
                                    ControlToValidate="txtName" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>详细描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="txtContent_LongInfo" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" OnInit="txtContent_Init" CssClass="l_input" />
                                <span class="auto-style1">*</span></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="right" height="45" class="style4">&nbsp;
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="45">
                                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="100px" OnClientClick="return CheckClientValidate();"
                                    OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    &nbsp;
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            var ckeditor; //定义全局变量 ckeditor
            $(function () {//当全部DOM元素加载完毕后执行下面语句，不加此句javascript将无法找到TextBox1
                //var aa = "";
                ckeditor = CKEDITOR.replace("<%=txtContent_LongInfo.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditor, "../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合
            });
        </script>
    </form>
</body>
</html>