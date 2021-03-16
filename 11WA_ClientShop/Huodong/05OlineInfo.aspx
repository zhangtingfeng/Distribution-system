<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05OlineInfo.aspx.cs" Inherits="_11WA_ClientShop.Huodong._05OlineInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0">
    <title>
        <%=strMenuName%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <script src="../Scripts/jquery-1.8.2.min.js"></script>
    <%--<script src="js/jquery.min.js" type="text/javascript"></script>--%>
    <%--<script src="./js/plugmenu.js" type="text/javascript"></script>--%>
    <link href="../Styles/OnlineBaomingindex.css" rel="stylesheet" />
    <%--<link href="Css/OnlineBaomingindex.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="js/OnlineBaomingpccs.js" type="text/javascript"></script>--%>
    <script src="../Scripts/OnlineBaomingpccs.js"></script>
    <script src="../Scripts/plugmenu.js"></script>
    <%--<link rel="stylesheet" href="./css/plugmenu.css" />--%>
    <link href="../Styles/plugmenu.css" rel="stylesheet" />
    <!-- Mobile Devices Support @begin -->
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <!-- apple devices fullscreen -->
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <!-- Mobile Devices Support @end -->
    <%--<link href="./Css/news1_.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Styles/OnlineBaomingnews1_.css" rel="stylesheet" />
    <style>
        .themeStyle {
            background: #363B66;
        }
    </style>
    <script type="text/javascript">
        function aa(Dir)
        { tt.doScroll(Dir); Timer = setTimeout('aa("' + Dir + '")', 100) } //这里100为滚动速度
        function StopScroll() { if (Timer != null) clearTimeout(Timer) }

        function initIt() {
            divColl = document.all.tags("DIV");
            for (i = 0; i < divColl.length; i++) {
                whichEl = divColl(i);
                if (whichEl.className == "child") whichEl.style.display = "none";
            }
        }
        function expands(el) {
            whichEl1 = eval(el + "Child");
            if (whichEl1.style.display == "none") {
                initIt();
                whichEl1.style.display = "block";
            } else { whichEl1.style.display = "none"; }
        }
        var tree = 0;
        function loadThreadFollow() {
            if (tree == 0) {
                document.frames["hiddenframe"].location.replace("Left.aspx");
                tree = 1
            }
        }

        function showsubmenu(sid) {
            whichEl = eval("submenu" + sid);
            imgmenu = eval("imgmenu" + sid);
            if (whichEl.style.display == "none") {
                eval("submenu" + sid + ".style.display=\"\";");
                imgmenu.background = "image/menuup.gif";
            }
            else {
                eval("submenu" + sid + ".style.display=\"none\";");
                imgmenu.background = "image/menudown.gif";
            }
        }

        function loadingmenu(id) {
            var loadmenu = eval("menu" + id);
            if (loadmenu.innerText == "Loading...") {
                document.frames["hiddenframe"].location.replace("Left.aspx?menu=menu&id=" + id + "");
            }
        }
        //top.document.title = "在线报名表";

        $(function () {
            setup();
            var province = $("#hidprovince").val() == "" ? "省份" : $("#hidprovince").val();
            var city = $("#hidcity").val() == "" ? "地级市" : $("#hidcity").val();
            var Area = $("#hidArea").val() == "" ? "市、县级市、县" : $("#hidArea").val();
            $("#province option:contains('" + province + "')").attr('selected', true);
            change(1)
            $("#city option:contains('" + city + "')").attr('selected', true);
            change(2)
            $("#Area option:contains('" + Area + "')").attr('selected', true);
        })

        function OnValid() {
            var Pvalid = /^13[0-9]{9}|15[012356789][0-9]{8}|18[01256789][0-9]{8}|147[0-9]{8}$/;
            if ($("#txtName").val() == "") {
                alert("请输入姓名！");
                return false;
            } else if (!Pvalid.test($("#txtLocalCall").val())) {
                alert("请输入正确的手机号码！");
                return false;
            } else if ($("#province").find("option:selected").val() == "省份") {
                alert("省份！");
                return false;
            }

            $("#hidprovince").val($("#province").find("option:selected").val())
            $("#hidcity").val($("#city").find("option:selected").val())
            $("#hidArea").val($("#Area").find("option:selected").val())
            return true;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/common.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/font-awesome.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/mall.css?version=css201709121928">
    <link rel="stylesheet" type="text/css" href="/Templet/01WYJS/Css/PreFoot.css?version=css201709121928">
    <script src="/Templet/01WYJS/js/jquery-1.js?version=js201709121928" type="text/javascript"></script>
    <script type="text/javascript" src="/Templet/01WYJS/js/jscommon.js?version=js201709121928" charset="gb2312"></script>
    <link href="/Templet/02ShiYi/skin/foot.css?version=css201709121928" rel="stylesheet" type="text/css">
    <script src="/Templet/02ShiYi/js/Footer_common.js?version=js201709121928"></script>
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
</head>
<body id="news">
    <form id="form1" runat="server">
        <div id="mcover" onclick="document.getElementById('mcover').style.display='';">
            <img src="/images/guide.png" />
        </div>
        <div class="page-bizinfo">
            <%--        <div class="header" style="position: relative;">
            <h1 id="activity-name">
                <%=strMenuName%></h1>
        </div>
        <a id="biz-link" class="btn" href="./Default-<%=strWeiXinINC_User_ID%>.aspx" data-transition="slide">
            <div class="arrow">
                <div class="icons arrow-r">
                </div>
            </div>
            <div class="logo">
                <div class="circle">
                </div>
                <img id="img" src="<%=strINCIcon%>">
            </div>
            <div id="nickname">
                <%=strINCFullName%></div>
        </a>
        <div class="showpic">
        </div>--%>
            <div class="text" id="content">
                <%=strOnlineContent%>
                <div id="user_Info" class="user_Info" <%=GetDisplayPassVlideTime%>>
                    <table width="92%" cellspacing="0" cellpadding="0" style="border-collapse: separate; border-spacing: 10px; clear: both; margin: 0 auto; text-align: center;">
                        <!-- <tr>
                        <td class="td_left" colspan="2">
                            <input type="checkbox" name="" id="" value="" />
                           
                        </td>
                    </tr>-->
                        <tr>
                            <td class="td_right">姓名：<br />
                                <input type="text" id="txtName" size="40" runat="server" />
                            </td>
                            <!-- <td class="td_left">
                            
                        </td>-->
                        </tr>
                        <tr>
                            <td class="td_right">手机：<br />
                                <input type="text" id="txtLocalCall" size="40" value="18112345678" runat="server" />
                            </td>
                            <!-- <td class="td_left">
                            
                        </td>-->
                        </tr>

                        <tr <%=displayLiuOrShengOrData("XiangXiDizhi")%>>
                            <td class="td_right">地址：<br />
                                <input type="text" id="Text_Address_New_By_LiuZong" size="40" value="" runat="server" />
                            </td>
                        </tr>

                        <tr <%=displayLiuOrShengOrData("ShengFen")%>>
                            <td class="td_right">省份：<br />

                                <select name="province" id="province">
                                    <option>&nbsp;</option>
                                </select><!--省-->
                                <select name="city" id="city" style="display: none;">
                                    <option>&nbsp;</option>
                                </select><!--市-->
                                <select name="Area" id="Area" style="display: none;">
                                    <option>&nbsp;</option>
                                </select><!--区-->
                                <asp:HiddenField ID="hidprovince" runat="server" />
                                <asp:HiddenField ID="hidcity" runat="server" />
                                <asp:HiddenField ID="hidArea" runat="server" />
                            </td>
                            <!--<td class="td_left">
                            &nbsp;&nbsp;
                            
                        </td>-->
                        </tr>

                        <%=Pub_IFShow_Cus_Item_List%>
                    </table>
                    <br />
                    <div style="clear: both;">
                    </div>
                    <p style="margin: auto; width: 100%; text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="提交" Style="width: 150px; font-size: 24px;" OnClick="btnSubmit_Click"
                            OnClientClick="return OnValid()" />

                    </p>
                    <br />
                </div>
                已参加：
            <ul>
                <li class=" ul_li">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <%#Eval("Name")    %>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:Repeater>
                </li>
            </ul>
                <div id="insert3">
                </div>
            </div>

        </div>


        <%--<uc1:Plug ID="Plug1" runat="server" />--%>
        <%--<uc2:WebBottom ID="WebBottom1" runat="server" />--%>
    </form>
    <div class="ClassCenter">
        技术支持<a href="http://net.shanghaishiyi.com/default-14.aspx">时仪电子</a>
        <a href="https://000001shiyidianzi.eggsoft.cn">微云基石</a>
    </div>
    <%=_Pub_03Footer_html%>
</body>
</html>
