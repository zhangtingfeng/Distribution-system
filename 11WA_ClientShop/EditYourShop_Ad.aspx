<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditYourShop_Ad.aspx.cs" Inherits="_11WA_ClientShop.EditYourShop_Ad" %>


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

    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/foundation.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/normalize.css?version=css201709121928">
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


    <link href="/Templet/01WYJS/js/jquery.alert.v1.2/jquery.alert.css?version=css201709121928" rel="stylesheet" />
    <script src="/Templet/01WYJS/js/jquery.alert.v1.2/jquery.easydrag.js?version=js201709121928"></script>
    <script src="/Templet/01WYJS/js/jquery.alert.v1.2/jquery.alert.js?version=js201709121928"></script>

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
    <div id="dingwei" style="display: none;">已经提交,请稍等</div>

    <!--submit errow tip begin-->
    <div data-alert="" class="alert-box alert" style="display: none;" id="errerMsg">请输入微店名！<a href="#" class="close">×</a></div>
    <!--submit errow tip end-->


    <!--topbar begin-->
    <div class="fixed">
        <nav class="tab-bar">
            <section class="left-small">
            </section>
            <section class="middle tab-bar-section">
                <h1>
                    <div class="title">编辑代理微店</div>
                </h1>
            </section>
            <section class="right-small right-small-text2" id="saveBtn">
                <a href="javascript:void(0)" onclick="_BtnSave()" class="button [radius round] top-button">提交</a>
            </section>
        </nav>
    </div>
    <!--topbar end-->

    <!--have begin-->
    <div class="storeedit mlr-15">
        <form>

            <div class="row">
                <div class="large-12 columns">
                    <label>店铺名称</label>
                </div>
                <div class="small-8 columns input-col">
                    <input id="ShopName" value="<%=pub_stringEditShopList[0]%>" placeholder="店铺名称" type="text">
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>联系人姓名</label>
                </div>
                <div class="small-8 columns input-col">
                    <input id="ContactName" value="<%=pub_stringEditShopList[1]%>" placeholder="真实姓名(向您转账信息)" type="text">
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>手机号码</label>
                </div>
                <div class="small-8 columns input-col">
                    <input id="ContactMobile" value="<%=pub_stringEditShopList[2]%>" placeholder="联系人手机" type="text">
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>支付宝或微信</label>
                </div>
                <div class="small-8 columns input-col">
                    <input id="AlipayOrWeiXinPay" value="<%=pub_stringEditShopList[3]%>" placeholder="您的支付宝或者微信号" type="text">
                </div>
            </div>
            <%=pub_AddExpListTextShowString%>

            <div class="row">
                <div class="large-12 columns">
                    <label>选择代理级别</label>
                </div>
                <div class="small-8 columns input-col">
                    <select name="AgentLevelList" id="AgentLevelList">
                        <%=pub_AgentLevelList%>
                    </select>
                </div>
            </div>
            <!--01 end-->
            <!--01 begin-->
            <div style="display: block; height: 10px;">
            </div>
            <div class="tip-means mb-20">

                <%=_pub_GetAgentAdpolText%>
            </div>
            <div class="form-title-class">
                <label>选择代理商品</label>
            </div>
            <div class="row">
                <div class="large-12 columns pl0">
                    <input checked="checked" id="checkbox1" onclick="checkAll(this)" type="checkbox"><label for="checkbox1">全选</label>
                </div>
            </div>
            <!--row01 begin-->
            <div id="device" class="category gridalicious">
                <%=pub_stringChoiceProductClsssList %>
                <div id="cleareHnb5" style="clear: both; height: 0px; width: 0px; display: block;"></div>
            </div>


            <!--row02 end-->
            <!--01 end-->
        </form>
    </div>

    <!--have end-->


    <script src="/Templet/01WYJS/js/jquery.js?version=js201709121928"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#device").gridalicious({
                gutter: 10,
                width: 150,

                animationOptions: {
                    speed: 150,
                    duration: 400,
                    complete: null
                },
            });

        });
    </script>


    <script type="text/javascript">
        var oldcids = '<%=pub_StrUserIDSelectProductList%>'.split(',');
        for (var i = 0; i < oldcids.length; i++) {
            $("div [name=columns][cid=" + oldcids[i] + "]").addClass("current");
        }
        var cids = oldcids;
        var cidcount = $("div [name=columns]").length;
        if (oldcids.length == cidcount) {
            document.getElementById("checkbox1").checked = true;
        }
        var title = "";
        function checkAll(obj) {
            if (obj.checked) {
                //$("#cidAll").attr("checked", "checked");
                cids = new Array();
                $("div [name=columns]").each(function () {
                    $(this).addClass("current");
                    cids.push($(this).attr("cid"));
                })
            } else {
                // $("#cidAll").attr("checked", "");
                $("div [name=columns]").each(function () {
                    $(this).removeClass("current");
                })
                cids = new Array();
            }
        }
        $(function () {
            //$("div[name=columns]").click(function () {
            //    if (!$(this).hasClass('current')) {
            //        cids.push($(this).attr('cid'));
            //        $(this).addClass("current");
            //    } else {
            //        cids.splice(cids.indexOf($(this).attr('cid')), 1);
            //        $(this).removeClass("current");
            //    }
            //    if (cids.length < cidcount) {
            //        document.getElementById("checkbox1").checked = false;
            //    } else {
            //        document.getElementById("checkbox1").checked = true;
            //    }
            //})

            $(document).on('click', "div[name=columns]", function () {
                if (!$(this).hasClass('current')) {
                    cids.push($(this).attr('cid'));
                    $(this).addClass("current");
                } else {
                    cids.splice(cids.indexOf($(this).attr('cid')), 1);
                    $(this).removeClass("current");
                }
                if (cids.length < cidcount) {
                    document.getElementById("checkbox1").checked = false;
                } else {
                    document.getElementById("checkbox1").checked = true;
                }
            });

        })


    </script>

    <script type="text/javascript">
        function fGetValue(elementId) {
            var obj = $(elementId); if (obj.length > 0) {///因为jQuery对象永远都有返回值，所以$("someID") 总是TRUE ，IF语句没有起到任何判断作用。正确的写法应该是： 
                return $(elementId).val().trim();
            }
            else {
                return;
            }
        }

        function _BtnSave() {
            var varShopName = $("#ShopName").val().trim();
            var varContactName = $("#ContactName").val().trim();
            var varContactMobile = $("#ContactMobile").val().trim();
            var varAlipayOrWeiXinPay = $("#AlipayOrWeiXinPay").val().trim();
            var varAgentLevelList = $("#AgentLevelList").val().trim();

            if (varShopName == "") {
                ShowMsg("店铺名称不可为空");
                return;
            }
            if (varContactMobile.length == 0) {
                alert('请输入手机号码！');
                return false;
            }
            if (varContactMobile.length != 11) {
                alert('请输入有效的手机号码！');
                return false;
            }
            if (varContactName == "") {
                ShowMsg("联系人不可为空");
                return;
            }
            if (cids.length == 0) {
                ShowMsg("请选择代理商品");
                return;
            }
            /*
          String strUserID = context.QueryString["UserID"];
          String strParentID = context.QueryString["ParentID"];
          String strShopName = context.QueryString["ShopName"];
          String strContactName = context.QueryString["ContactName"];
          String strContactMobile = context.QueryString["ContactMobile"];
          String strAlipayOrWeiXinPay = context.QueryString["AlipayOrWeiXinPay"];
          String strChoiceGoodList = context.QueryString["ChoiceGoodList"];
          String strSaveAgentOrAutoSaveChoiceGoodList = context.QueryString["SaveAgentOrAutoSaveChoiceGoodList"];
          */

            var varURL = "<%=Eggsoft.Common.CommAuthen._Services_Get_Services_URL()%>/User/WS_Agent_ChoiceGoods.asmx/_Service_Agent_Save";
            varURL += "?UserID=<%=pub_Int_Session_CurUserID%>&ParentID=<%=pub_Int_CurParentID%>";
            varURL += "&ShopClientID=" + varShopClientID;
            varURL += "&ShopName=" + encodeURI(encodeURI(varShopName));
            varURL += "&ContactName=" + encodeURI(encodeURI(varContactName));
            varURL += "&ContactMobile=" + varContactMobile;
            varURL += "&AlipayOrWeiXinPay=" + encodeURI(encodeURI(varAlipayOrWeiXinPay));
            varURL += "&ChoiceGoodList=" + cids;
            varURL += "&AgentAdLevel=" + varAgentLevelList;
            var varfGetValue = fGetValue("#AddExp0");
            if (varfGetValue) varURL += "&AddExp0=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp1");
            if (varfGetValue) varURL += "&AddExp1=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp2");
            if (varfGetValue) varURL += "&AddExp2=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp3");
            if (varfGetValue) varURL += "&AddExp3=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp4");
            if (varfGetValue) varURL += "&AddExp4=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp5");
            if (varfGetValue) varURL += "&AddExp6=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp7");
            if (varfGetValue) varURL += "&AddExp7=" + encodeURI(encodeURI(varfGetValue));
            varfGetValue = fGetValue("#AddExp8");
            if (varfGetValue) varURL += "&AddExp8=" + encodeURI(encodeURI(varfGetValue));

            $.ajax({
                type: "get",
                url: varURL,
                beforeSend:
                    function () {
                        $("#dingwei").css("display", "block");
                    },
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                jsonpCallback: "jsonpCallback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json;charset=utf-8",
                success: function (json) {
                    if (json.ErrorCode == "3") {
                        $.alerts.okButton = "确定";
                        jAlert("申请成功,已经Email通知店主.请等待审批！", '提示', callBackvardoT);
                        return false;

                        //alert('申请成功,已经Email通知店主.请等待审批！');
                        //刷新当前页面
                        //document.location.reload();
                    }
                    else if (json.ErrorCode == "4") {


                        $.alerts.okButton = "确定";
                        jAlert("修改成功", '提示', callBackvardoT);
                        return;
                        //alert('修改成功！');
                        //刷新当前页面
                        //document.location.reload();
                    }
                    else if (json.ErrorCode == "5") {
                        $.alerts.okButton = "确定";
                        jAlert("新增成功,已经Email通知店主", '提示', callBackvardoT);
                        return;
                        //alert('新增成功,已经Email通知店主！');
                        //刷新当前页面
                        //document.location.reload();
                    }
                    else {
                        $.alerts.okButton = "确定";
                        jAlert("保存错误", '提示', callBackvardoF);
                        return;

                        //alert('保存错误！');
                    }
                },
                complete: function () {
                    $("#dingwei").css("display", "none");
                },
                error: function () {
                    // debugger;
                    // alert('fail');
                }
            });
        }


        function callBackvardoT() {
            //document.location.reload();
            self.location ='<%=Pub_Agent_Path%>/mywebuy.aspx';
        }
        function callBackvardoF() {
        }
    </script>


    <%--<script>
        $(document).ready(function () {

            //$("#a").click(function () {
            $.alerts.okButton = "确定";
            jAlert("公司名称必须填写！", '提示');
            return false;
            //});
        });
    </script>--%>

    <%=_Pub_03Footer_html%>
</body>
</html>
