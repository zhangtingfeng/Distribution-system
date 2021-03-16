<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload.ascx.cs" Inherits="_12upload.eggsoft.cn.Control.UploadControl.Upload" %>
<script src="/js/Cookie.js?version=js201508180713" type="text/javascript"></script>
 <script src="/Upload_JS/showModalDialog.js"></script>
<script type="text/javascript">
   


    function TestShowModalDialog() {
        debugger;
        var rv = window.showModalDialog("/Control/UploadControl/default.aspx", self, "dialogWidth=800px;dialogHeight=600px;dialogLeft=200px;dialogTop=100px;");

        //alert(getCookie("txtReturnValue"));
        //setCookie("txtReturnValue", "varSrc");
        // alert(getCookie("txtReturnValue"));
        //var txtReturnValue = document.getElementById("txtReturnValue");
        if (rv == undefined) {
            txtReturnValue.value = "没有返回值";
        }
        else {
            txtReturnValue.value = rv;
        }
        //alert(getCookie("txtReturnValue"));
        //var txtReturnValue=(String)session.getAttribute("txtReturnValue");
        //txtReturnValue.value = txtReturnValue;
    }
    function TestOpen() {
        window.open("/Control/UploadControl/default.aspx", "_blank", "dialogWidth=800px;dialogHeight=600px;dialogLeft=200px;dialogTop=100px;");
    }
</script>
<input type="text" id="txtReturnValue"
    name="txtReturnValue" style="width: 297px" readonly="readonly"><input type="button" value="选择文件" onclick="TestShowModalDialog();">

<%--<br>
                               <INPUT type="button" value="用window.open()方法" onclick = "TestOpen();"><br>--%>
              