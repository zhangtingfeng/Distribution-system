<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinOpenID.aspx.cs" Inherits="_11WA_ClientShop.User.WeiXinOpenID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=640, user-scalable=no, target-densitydpi=device-dpi" />

    <link href="../Styles/Base.css?version=css201709121928" rel="stylesheet" />
    <title></title>
</head>
<body>
    <div style="background-color: rgb(255, 255, 255); display: block;" id="masklayer" class="wx_loading">
        <div class="wx_loading_inner">
            <div class="wx_loading_icon">
                <br />
            </div>
            正在加载...
        </div>
    </div>
    <script type="text/javascript">
        //alert("JSWeiXinOpenID.aspx");
        var varpub_strApplicationCheckName = "<%=pub_strApplicationCheckName%>";//openID
        var varpub_strApplicationCheckName_S_A_t = "<%=pub_strApplicationCheckName_S_A_t%>";
        var varpub_strApplicationCheckName_Scope = "<%=pub_strApplicationCheckName_Scope%>";
        var varpub_strState = "<%=pub_strState%>";



        if ((varpub_strApplicationCheckName != "") && (varpub_strApplicationCheckName_S_A_t != "") && (varpub_strApplicationCheckName_Scope != "") && (varpub_strState != "")) {
            var strApplicationCheckName = "<%=Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName()%>" + "SID" + "<%=pub_strShopClientID%>";

            localStorage.setItem(strApplicationCheckName, varpub_strApplicationCheckName);
            localStorage.setItem(strApplicationCheckName + "_S_A_t", varpub_strApplicationCheckName_S_A_t);
            localStorage.setItem(strApplicationCheckName + "_Scope", varpub_strApplicationCheckName_Scope);
            window.location.href = varpub_strState;
        }
        else {
            alert("出现这个说明不正常，C#应该已经自己跳了");
        }
    </script>


</body>
</html>
