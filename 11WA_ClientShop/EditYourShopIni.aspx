<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditYourShopIni.aspx.cs" Inherits="_11WA_ClientShop.EditYourShopIni" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="initial-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=pub_GetAgentShopName_From_Visit__%></title>
    <link rel="stylesheet" type="text/css" href="http://qiniu.eggsoft.cn/foundation.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/normalize.css?version=css201709121928">
    <script src="/Templet/01WYJS/js/ClassFirst.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/ClassSub.js?version=js201709121928"></script>
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common-v4.css?version=css201709121928">


    <script src="/Templet/01WYJS/js/jquery_002.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/foundation.js?version=js201709121928"></script>
    <meta class="foundation-data-attribute-namespace">
    <meta class="foundation-mq-xxlarge">
    <meta class="foundation-mq-xlarge">
    <meta class="foundation-mq-large">
    <meta class="foundation-mq-medium">
    <meta class="foundation-mq-small">
    <script src="/Templet/01WYJS/js/func.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/Common.js?version=js201709121928"></script>





    <script src="/Templet/02ShiYi/js/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="/Templet/01WYJS/Css/mall.css?version=css201709121928" type="text/css" rel="stylesheet">
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>

    <%=pub_WeiXin__o2o_FootMarker_Location___%>
    <script src="/Templet/01WYJS/js/Base.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varUserID = "<%=pub_Int_Session_CurUserID%>";///传递给 product.js   传递变量\n";
        var varServiceURL = "<%= Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";///传递给 product.js   传递变量\n";
        var varShopClientID = "<%=pub_Int_ShopClientID%>";///传递给 product.js   传递变量
   
    </script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
</head>

<body class="body-gray">

    <!--topbar begin-->
    <div class="fixed">
        <nav class="tab-bar">
            <section class="left_small_Button right-small-text2" id="saveBtn1">
                <a href="javascript:void(0)" onclick="_BtnDoAd()" style="width: 100%;" class="button_EditShopini">我要做代理商</a>
            </section>
            <section class="right_small_Button right-small-text2" id="saveBtn2">
                <a href="javascript:void(0)" onclick="_BtnDo()" style="width: 100%;" class="button_EditShopini">我要做分销商</a>
            </section>
        </nav>
    </div>
    <!--topbar end-->



    <div class="tip-means mb-20" style="margin-top: 60px;">
        <%=_pub_GetAgentAdpolText%>
    </div>

    <div class="tip-means mb-20" style="margin-top: 20px;">
        <%=_pub_GetAgentpolText%>
    </div>




    <%=_Pub_03Footer_html%>


    <script type="text/javascript">


        function _BtnDoAd() {
            self.location = '<%=Pub_Agent_Path%>/edityourshop_ad.aspx';


        }
        function _BtnDo() {

            self.location = '<%=Pub_Agent_Path%>/edityourshop.aspx';

        }
    </script>
</body>
</html>