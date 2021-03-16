<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPic.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._11RootMenu.QQMake.GetPic1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Scripts/jquery-1.8.3.js?version=js201509012202" type="text/javascript"></script>
    <link href="../../skin/default.css?version=css201509012202" rel="stylesheet" type="text/css" />
    <script language="JavaScript">
<!--
    var flashID;

    function getID(swfID) {
        if (navigator.appName.indexOf("Microsoft") > -1) {
            flashID = window[swfID];
        } else {
            flashID = document[swfID];
        }
    }

    function loadFrontImg(str) {
        flashID.changeFrontImg(str);
    }

    //-->
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 150px;
            height: 120px;
        }
        .auto-style2 {
            width: 120px;
            height: 150px;
        }
        .auto-style3 {
            text-align: right;
        }
    </style>

</head>
<body onload="getID('makepic');">
    <form id="form1" runat="server">
    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">

       

         <tr bgcolor="#e3e3e3">
            <td class="auto-style3">
                 <div style="text-align: center">
                 <input id="FileUpload_ErWeiMa" style=";" type="file" runat="server" />

                手机截屏您的推广二维码
                     
                      
                     <asp:Button ID="Button_ErWeiMa" runat="server" Text="确  定" OnClick="Button_ErWeiMa_Click" />
                     
                     </div>


            </td>
            <td bgcolor="#ecf5ff">
                <img alt="" class="auto-style2" src="makepic.as3333px123.jpg" /></td>
        </tr>

        </table>
    </form>
</body>
</html>
