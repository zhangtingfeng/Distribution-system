<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.ContactUs1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ContactUs</title>
    <script src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .border input {
            height: auto;
        }
    </style>

    <script type="text/javascript"> 
        function uploadCompleteLogo(sender, args) {
            var varIDIndex="<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
    var fileName = args.get_fileName().lastIndexOf('.');            
    var a = args.get_fileName().substring(fileName).toLowerCase();
    if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
        $("#Image_Logo").attr("src", "/upload/TempUpload/"+varIDIndex+"_" + args.get_fileName());
    }    
}

function uploadComplete(sender, args) {
    var varIDIndex="<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
    var fileName = args.get_fileName().lastIndexOf('.');            
    var a = args.get_fileName().substring(fileName).toLowerCase();
    if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
        $("#Image1_background").attr("src", "/upload/TempUpload/"+varIDIndex+"_" + args.get_fileName());
    }    
}
    </script>

    <script type="text/javascript">

        function uC_b(sender, args) {
            var varIDIndex="<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
    var fileName = args.get_fileName().lastIndexOf('.');            
    var a = args.get_fileName().substring(fileName).toLowerCase();
    if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
        $("#Image1_background").attr("src", "/upload/TempUpload/"+varIDIndex+"_" + args.get_fileName());
    }    
}

function uploadComplete_FileUpload_Logo(sender, args) {
   
}
function uploadError(sender, args) {
    alert("上传错误");
}


function CheckClientValidate() {
    if (Page_ClientValidate()) {

        var srcUp_Image1_background = $("#Image1_background").attr("src");
        var srcUp_Image_Logo = $("#Image_Logo").attr("src");

        if {(srcUp_Image1_background.toString().length == 0) || (srcUp_Image_Logo.toString().length == 0) } {
            alert("相关图片必须选择！");
            return false;
        }
    return true;
}//   Page_BlockSubmit=false;  //当页面中有其他不需要验证的按钮或下拉框时一定要加上这句话，否则其他下拉框第一次提交时不会触发后台代码
}
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25"><strong>&nbsp;&nbsp; 商铺首页</strong></th>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style1">
                                <strong>是否显示首页：</strong></td>
                            <td bgcolor="#ecf5ff" width="80%">
                                <div style="display: none;">
                                    <cc2:ColorPicker ID="ColorPicker_FontColor" runat="server" />
                                </div>
                                &nbsp;
                                <asp:CheckBox ID="CheckBoxIfShow" runat="server" Text="是否显示首页" />
                            </td>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style1">
                                <strong>首页类型：</strong></td>
                            <td bgcolor="#ecf5ff" height="35">
                                <div style="display: none;">
                                    <cc2:ColorPicker ID="ColorPicker_BackgroundColor" runat="server"
                                        PopupPosition="BottomRight" />
                                </div>
                                &nbsp;<asp:RadioButtonList ID="RadioButtonList_Choice" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">链接显示</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">内容显示</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style1">
                                <strong>链接显示：</strong></td>
                            <td bgcolor="#ecf5ff" height="35">
                                <div style="display: none;">
                                    <cc2:ColorPicker ID="ColorPicker1" runat="server"
                                        PopupPosition="BottomRight" />
                                </div>
                                <asp:TextBox ID="TextBox_LINK" runat="server" Width="678px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style1">
                                <strong>内容显示：</strong><br />
                                &nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" height="35">

                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="Text_setContactUs" runat="server" TextMode="MultiLine" Height="147px"
                                                Width="476px" CssClass="l_input" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="center" height="35" class="style_Left">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="35">
                                <div align="left">
                                    &nbsp;
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </FONT>

 

        <p style="text-align: center">
            <font face="宋体">
                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="72px"
                    OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
            </font>
        </p>
    </form>

  

</body>
</html>