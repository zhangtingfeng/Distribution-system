<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckUserEmailInfo.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._17O2O_Shop.CheckUserEmailInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

        function uploadComplete(sender, args) {
            alert("上传成功");
        }

        function uploadError(sender, args) {
            alert("上传错误");
        }


        function CheckClientValidate() {
            var TextboxUserPassword = $('#TextboxUserPassword').val();
            var TextboxRePassword = $('#TextboxRePassword').val();

            if (TextboxUserPassword != TextboxRePassword) {
                alert("两次的密码不相同！");
                return false;
            }



            var srcUp = $("#ImageLogo").attr("src");
            if (srcUp.toString().length == 0) {
                alert("相关图片必须选择！");
                return false;
            }
            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">

    
    </form>   

</body>
</html>