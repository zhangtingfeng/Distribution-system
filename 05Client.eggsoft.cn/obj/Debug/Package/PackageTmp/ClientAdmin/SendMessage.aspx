<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin.SendMessage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="_7he7.ColorPicker" namespace="Karpach.WebControls" tagprefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD >
<title>ContactUs</title>
<script src="../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
<link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 	

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

</HEAD>
<body>
<form id="Form1" method="post" runat="server"> 
    <table class="border" style="WIDTH: 100%; " cellSpacing="2" cellPadding="0" align="center" border="0">
        <tr class="title" >
            <th bgColor="#ecf5ff" class="centerAuto" colSpan="2" height="35"><strong>&nbsp;&nbsp; 发送文本消息</strong></th>
        </tr> 
        <tr class="tdbg" bgColor="#e3e3e3">
            <td align="right"  height="35" class="style1Percent20" width="150">
                发送对象：</td>
            <td bgColor="#ecf5ff" > 
               <asp:Literal ID="Literal_SendMessage" runat="server"></asp:Literal> 
            </td>
        </tr>
 
        <tr class="tdbg" bgColor="#e3e3e3">
            <td align="right"  height="22" class="style1Percent20">
                  详细内容： <br />
                
                 </td>
            <td bgColor="#ecf5ff" height="22">
                          
                      <table style="width:100%;">
                          <tr>
                              <td>
                      <asp:TextBox ID="Text_Contact" runat="server" TextMode="MultiLine" Height="147px" 
            Width="476px" CssClass="l_input" />
                                  <br />
                      <asp:RequiredFieldValidator id="RequiredFieldValidator_ContactUs" 
                          runat="server" ErrorMessage="详细内容不能为空!" ControlToValidate="Text_Contact" 
                          ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    </td>
                          </tr>
                          </table> 
            </td>
        </tr>
        
    </table>  
<p style="text-align: center">
<FONT face="宋体" >
<asp:button id="btnAdd" runat="server" Text=" 保  存 " Width="72px" 
OnClick="btnAdd_Click" onclientclick="return CheckClientValidate();" CssClass="b_input"></asp:button>
</FONT>     
</p>
</form>
</body>
</HTML>