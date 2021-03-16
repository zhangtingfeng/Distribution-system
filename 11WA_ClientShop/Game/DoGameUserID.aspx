<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoGameUserID.aspx.cs" Inherits="_11WA_ClientShop.Game.DoGameUserID" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=640, user-scalable=no, target-densitydpi=device-dpi" />
    <link href="/Styles/Base.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <div style="background-color: rgb(255, 255, 255); display: block;" id="masklayer"
        class="wx_loading">
        <div class="wx_loading_inner">
            <div class="wx_loading_icon">
                <br />
            </div>
            正在加载...
        </div>
    </div>

    <script type="text/javascript">
        var varPub_Agent_Path = '<%=Pub_Agent_Path%>';
        var varPub_DB_ParentID = '<%=Pub_DB_ParentID%>';
        ///alert(varPub_DB_ParentID);

        //debugger;  
        localStorage.setItem("ServiceServicesURL201709121928_Open_0609", '<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>');
        localStorage.setItem("CurUserID201709121928_Open_0609", '<%=pub_Int_Session_CurUserID%>');
        localStorage.setItem("CurUserID201709121928_UserSafeCode", '<%=pub_UserSafeCode%>');
        localStorage.setItem("GetShopClientID201709121928_Open_0609", '<%=pub_Int_ShopClientID%>');
        localStorage.setItem("GetUserNickName201709121928_Open_0609", '<%=Pub_GetNickName%>');
        localStorage.setItem("GetPub_Agent_Path201709121928_Open_0609", varPub_Agent_Path);
        localStorage.setItem("GetPub_Link_ParentID201709121928_Open_0609", varPub_DB_ParentID);
        var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
                              
        localStorage.setItem("GetShopClientName201709121928_Open_0609", '<%=Pub_GetShopClientName%>');
        localStorage.setItem("GetUserHeadImage201709121928_Open_0609", '<%=Pub_GetUserHeadImage%>');
        localStorage.setItem("GetUser_MyDisk_HeadImage201709121928_Open_0609", '<%=Pub_Get_MyDisk_HeadImage%>');
       // debugger;
        var gamehost = "https://" + window.location.host + "<%=Pub_WeiXinAuthorstrGameCallBackURl%>";//获取域名

        //alert("DoGameUserID window.location.href=" + gamehost);
        window.location.href = gamehost;
    </script>

</body>
</html>
