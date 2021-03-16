<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Html5LocalStorge.aspx.cs" Inherits="_11WA_ClientShop.User.Html5LocalStorge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=640, user-scalable=no, target-densitydpi=device-dpi" />
    <link href="../Styles/Base.css" rel="stylesheet" />
    <script src="/User/Scripts/Html5LocalStorge.js?version=js201709121928"></script>
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
        JSuserAgent();
        var strApplicationCheckName = "<%=Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName()%>" + "SID" + "<%=pub_strShopClientID%>";
        var varstrApplicationCheckName = localStorage.getItem(strApplicationCheckName);///openid
        var varstrApplicationCheckName_S_A_t = localStorage.getItem(strApplicationCheckName + "_S_A_t");
        var varstrApplicationCheckName_Scope = localStorage.getItem(strApplicationCheckName + "_Scope");
        //debugger;
        if ((varstrApplicationCheckName == null) || (varstrApplicationCheckName_S_A_t == null) || (varstrApplicationCheckName_Scope == null)) {
            var varNotWeiXinButAppCan = "/User/AppLogin.aspx" + "?LocalStorgeCallbackURL=" + encodeURIComponent("<%=pub_strAspxCallBackURL%>");

            if (isWeiXin()) {///跳微信认证页面
                window.location.href = "<%=pub_strmyOauth1URL%>";
            }
            else if (isAPPCan()) {///跳手机注册登录页面
                window.location.href = varNotWeiXinButAppCan;
            }
            else if (isAlipay_TianMao_TaoBao_()) {///跳手机注册登录页面
                window.location.href = varNotWeiXinButAppCan;
            }
            else {
                window.location.href = "<%=pub_strmyOauth1URL%>";///暂时只支持微信
                //// window.location.href = varNotWeiXinButAppCan;
            }
        }
        else {
            var strURL = "WeiXinOpenID.aspx?type=ReadedlocalStorageFromWeiXinOpenID&myjson_OpenID_openid=" + varstrApplicationCheckName;
            strURL += "&LocalStorgeCallbackURL=" + "<%=pub_strAspxCallBackURL%>";
            strURL += "&ScopeAccess_token=" + varstrApplicationCheckName_S_A_t;
            strURL += "&scope=" + varstrApplicationCheckName_Scope;
            window.location.href = strURL;
        }
    </script>

</body>
</html>
