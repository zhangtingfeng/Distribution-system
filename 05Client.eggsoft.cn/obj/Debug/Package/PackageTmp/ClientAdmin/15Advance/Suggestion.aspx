<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suggestion.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.Suggestion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <style type="text/css">
        .border input {
        }
    
* {
    margin: 0;
    padding: 0;
}

    </style>
    <title>意见反馈</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201506100633" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201506100633" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <asp:TextBox ID="Id" runat="server" type="hidden"/> 
        <table class="border" style="width: 100%; height: 30px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title" bgcolor="#a4b6d7">
                <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25">意见反馈信息
                </th>
            </tr>
        </table>
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
 
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>反馈主题：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Title" runat="server" Width="509px" Height="25px"
                        CssClass="l_input" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="反馈主题不能为空!"
                        ControlToValidate="Title"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>反馈内容：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Content" runat="server"  Wrap=true TextMode="MultiLine"  MaxLength="1073741823" Width="1008px" Height="300px"
                        CssClass="l_input"  style="OVERFLOW-Y:visible"></asp:TextBox>
                </td>
            </tr>

             </table>
        <p style="text-align: center">
            <font face="宋体">
                <asp:Button ID="btnAdd" runat="server" Text=" 保存 " Width="100px" OnClick="btnAdd_Click"
                    OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
        </p>
    </form>
</body>
</html>
