<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumerWealthAgreement.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.ConsumerWealthAgreement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ContactUs</title>
    <script src="../../../Upload_JS/ckeditor/ckeditor.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/jquery-2.0.3.min.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/ckfinder/ckfinder.js?version=js201709121928" type="text/javascript"></script>

    <script src="../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .border input {
            height: auto;
        }
    </style>

 <%--   <script type="text/javascript">
        function uploadCompleteLogo(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image_Logo").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }

        function uploadComplete(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image1_background").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }
    </script>

    <script type="text/javascript">

        function uC_b(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image1_background").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
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

                if ((srcUp_Image1_background.toString().length == 0) || (srcUp_Image_Logo.toString().length == 0)) {
                    alert("相关图片必须选择！");
                    return false;
                }
                return true;
            }//   Page_BlockSubmit=false;  //当页面中有其他不需要验证的按钮或下拉框时一定要加上这句话，否则其他下拉框第一次提交时不会触发后台代码
        }
    </script>--%>

</head>
<body>
    <form id="Form1" method="post" runat="server" style="width: 640px;">
        <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%;">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 607px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25">编辑协议</th>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style1">
                                <strong>内容显示：</strong><br />
                                &nbsp;<span class="style_Replace"></span></td>
                            <td bgcolor="#ecf5ff" height="35">

                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="Text_setContactUs" runat="server" TextMode="MultiLine" Height="647px"
                                                Width="537px" CssClass="l_input" />
                                            <br />
                                        </td>
                                      <%--  <td class="tdbg" bgcolor="#ecf5ff">也可使用图文编辑器点击打开 <a href="http://editor.o2o10000.cn/index.html" target="_blank">http://editor.o2o10000.cn/index.html</a> 即可在线使用操作。<br />
                                            编辑器必须在360极速浏览器下才可以完美使用，没有的建议先下载一个。 
                                <br />
                                            <a target="_blank" href="http://www.eggsoft.cn/upload/001Vunihicn/图文编辑器教程.doc">图文编辑器教程</a> 、 <a target="_blank" href="http://www.eggsoft.cn/upload/001Vunihicn/公众微信号操作指南.doc">公众微信号操作指南</a>
                                        </td>--%>
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
      
 

        <p style="text-align: center">
            <font face="宋体">
                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="72px"
                    OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
            </font>
        </p>
    </form>

    <script type="text/javascript">
        var ckeditor; //定义全局变量 ckeditor
        $(function () {//当全部DOM元素加载完毕后执行下面语句，不加此句javascript将无法找到TextBox1
            ckeditor = CKEDITOR.replace("<%=Text_setContactUs.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
            CKFinder.setupCKEditor(ckeditor, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合
        });
    </script>

</body>
</html>
