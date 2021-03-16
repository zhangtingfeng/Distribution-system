<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload_MultiSeclect.ascx.cs" Inherits="_12upload.eggsoft.cn.Control.UploadControl.Upload_MultiSeclect" %>

<style type="text/css">
    .VisibleFalse {
        display: none;
    }

    .auto-style1 {
        color: #CC0000;
    }
</style>


<script src="/Upload_JS/showModalDialog.js"></script>
<script type="text/javascript">
    function ClearText() {
        var tempt = '<%=TextBox_txtReturnValue.ClientID%>';
        var vardocumentID = document.getElementById(tempt);
        vardocumentID.value = "";
    }

    function TestShowModalDialog() {
        url = "/Control/UploadControl/default.aspx?Upload_Path=<%=public_Upload_Path%>";
        var hotelIdList = window.showModalDialog(url, "hotel", "dialogWidth=800px;dialogHeight=600px;dialogLeft=200px;dialogTop=100px;");
        if (!has_showModalDialog) return;//chrome 返回 因为showModalDialog是阻塞的 open不一样;	
        ParentOpenTest(hotelIdList)
        //$("#content").append(hotelIdList);

        // var rv = window.showModalDialog(url, self, "dialogWidth=800px;dialogHeight=600px;dialogLeft=200px;dialogTop=100px;");
        //alert(getCookie("txtReturnValue"));
        //setCookie("txtReturnValue", "varSrc");
        // alert(getCookie("txtReturnValue"));
        //var txtReturnValue = document.getElementById("txtReturnValue");


    }
    function ParentOpenTest(rv) {
        window.hasOpenWindow = false ;

        var tempt = '<%=TextBox_txtReturnValue.ClientID%>';
        var vardocumentID = document.getElementById(tempt);
        if (rv == undefined) {
            vardocumentID.value = "没有返回值";
        }
        else {
            if ((vardocumentID.value == "没有返回值") || (vardocumentID.value == "") || ('<%=_MultiChoice%>' == 'False')) {
                vardocumentID.value = rv;
            }
            else {
                vardocumentID.value = vardocumentID.value + ";" + rv;
            }
        }
        window.setTimeout("TestOpenClick();", 1000);
    }


    function TestOpenClick() {
        var Button1 = '<%=Button1.ClientID%>';
        document.getElementById(Button1).click();

    }

    function TestOpen() {
        window.open("/Control/UploadControl/default.aspx", "_blank", "dialogWidth=800px;dialogHeight=600px;dialogLeft=200px;dialogTop=100px;");
    }

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="TextBox_txtReturnValue" runat="server" Rows="5" TextMode="MultiLine"
            Width="450px" MaxLength="250"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="VisibleFalse" OnClick="Button1_Click" />
        <span class="auto-style1">*</span><input onclick="TestShowModalDialog();" type="button" value="选择文件">
            <input onclick="ClearText();" type="button" value="清空选择"> </input>
        </input>
    </ContentTemplate>
</asp:UpdatePanel>
