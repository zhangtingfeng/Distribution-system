<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="_03WAWapShop_Oliver._Admin.Left" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Left</title>
    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script>
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
        top.document.title = "系统";
    </script>
</head>
<body class="leftbg">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" align="center">
            <tr>
                <td class="menu_title" onmouseover="this.className='menu_title2';" onmouseout="this.className='menu_title';"
                    background="image/title_bg_quit.gif" height="25">
                    <span><b><a href="default.aspx">回到首页</a></b> | <a target="_top" href="Logout.aspx">退出</a></span>
                </td>
            </tr>
            <tr>
                <td align="center" onmouseover="aa('up')" onmouseout="StopScroll()">&nbsp;
                </td>
            </tr>
        </table>
        <script>
            var he = document.body.clientHeight - 105
            document.write("<div id=tt style=height:" + he + ";overflow:hidden>")
        </script>
        <table class="LiSec_menu" cellspacing="0" cellpadding="0" width="100%" align="center">
            <!--基本管理 Start-->
            <tr <%=DisPlay(1)%>>
                <td id="imgmenu1" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(1)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>系统维护</span>
                </td>
            </tr>
            <tr>
                <td id="submenu1" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="/Status/Default_DoOrderShow.aspx">每日更新</a></li>
                            <li><a target="main" href="tab_User/Board_User_Question.aspx">客户投诉</a></li>
                            <li><a target="main" href="tab_ShopClient/Board_ShopClient_Question.aspx">商户消息</a></li>
                            <%--<li><a target="main" href="01Shopping_Vouchers/De_Login.aspx">购物券管理</a></li>--%>
                            <asp:Literal ID="Literal_Author" runat="server"></asp:Literal>
                            <%--                            <li><a target="main" href="Template/MakeHtml.aspx">静态页生成</a></li>--%>
                            <li><a target="main" href="tab_User/UserRegOrSubscribe.aspx">用户统计</a></li>
                            <%--                            <li><a target="main" href="tab_System/DBMember.aspx">数据库</a></li>--%>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--基本管理 End-->
            <!--商品分类管理 Start-->
            <tr <%=DisPlay(2)%>>
                <td id="imgmenu2" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(2)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>功能模块</span>
                </td>
            </tr>
            <tr>
                <td id="submenu2" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="tab_System/02AnnouncePic/BoardAnnouncePic.aspx">首页轮播图片</a> </li>
                            <li><a target="main" href="02InviteRegCode/BoardInviteRegCode.aspx">邀请码管理</a> </li>
                            <%--                            <li><a target="main" href="tab_System/Board_Manage.aspx">微店设置</a> </li>--%>
                            <li><a target="main" href="tab_Class/tab_Class_BoardSet.aspx">商品分类管理</a> </li>
                            <li><a target="main" href="tab_Class/IS_Admin_check_Goods.aspx">商品审核管理</a> </li>
                            <li><a target="main" href="tab_ShopClient/IS_Admin_check_DrawMoney.aspx">商户提款审核管理</a> </li>
                            <li><a target="main" href="tab_ShopClient/IS_Admin_check_ReturnMoney.aspx">商户退款审核管理</a> </li>
                            <li><a target="main" href="/_Admin/tab_System/01DistributionMoney/Board_DistributionMoney.aspx">分销方案提成级别</a> </li>
                        </ul>
                    </div>
                    <br>
                </td>
            </tr>
            <!--版块管理 End-->

            <!--商户管理模块 Start-->
            <tr <%=DisPlay(3)%>>
                <td id="imgmenu3" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(3)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>商户管理模块</span>
                </td>
            </tr>
            <tr>
                <td id="submenu3" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="tab_ShopClient/BoardINC_Manage.aspx?type=add">添加商户</a></li>
                            <li><a target="main" href="tab_ShopClient/UserManage.aspx">商户列表</a></li>
                            <li><a target="main" href="tab_ShopClient/Suggestion_List.aspx">意见反馈</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--会员管理 End-->

            <!--开发模式 Start-->
            <tr>
                <td id="imgmenu4" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(4)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>开发模式管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu4" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=DisPlay(41)%>><a target="main" href="EngineerMode/BasicInfo.aspx">基本信息管理</a></li>
                            <li><a target="main" href="EngineerMode/BoardJPG.aspx">批量素材管理</a></li>
                            <li><a target="main" href="EngineerMode/Resource-1.aspx">素材管理</a><br />
                                <div id="FontSmall">
                                    <table style="width: 100%; border-width: thin; border-color: #FFFF00; border-top-width: medium;">
                                        <tr>
                                            <td>
                                                <a target="main" href="EngineerMode/Resource-1.aspx"><span>文本</span></a>
                                                <%--  <a target="main" href="EngineerMode/Resource-ImageType5.aspx"><span>图片</span></a>
                                                --%>
                                                <a target="main" href="EngineerMode/Resource-2.aspx"><span>单图文</span></a> <a target="main"
                                                    href="EngineerMode/Resource-3.aspx"><span>多图文</span></a>
                                                <%--   <a target="main" href="EngineerMode/Resource-VoiceType6.aspx"><span>语音</span></a>
                                    <a target="main" href="EngineerMode/Resource-VideoType7.aspx"><span>视频</span></a>
                                  <a target="main" href="EngineerMode/Resource-MusicType8.aspx"><span>音乐</span></a>--%>
                                                <%--<a target="main" href="EngineerMode/Resource-ZhengWen.aspx"><span>正文</span></a>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li <%=getStyleShow("WeiXin")%>><a target="main" href="EngineerMode/Subscribe.aspx">关注时回复-微信</a></li>
                            <li <%=getStyleShow("YiXin")%>><a target="main" href="EngineerMode/Subscribe_YiXin.aspx">关注时回复-易信</a></li>
                            <li <%=getStyleShow("WeiXin")%>><a target="main" href="EngineerMode/WeiXinMenu.aspx">公众平台菜单管理-微信</a></li>
                            <li <%=getStyleShow("YiXin")%>><a target="main" href="EngineerMode/WeiXinMenu_YiXin.aspx">公众平台菜单管理-易信</a></li>
                            <li><a target="main" href="EngineerMode/KeyAnswer_Board.aspx">关键词回复</a></li>
                            <li><a target="main" href="SendMessage/ShowUser.aspx">用户管理</a></li>
                        </ul>
                    </div>
                    <br>
                </td>
            </tr>
            <!--开发模式 End-->


            <!--帮助文档管理 Start-->
            <tr>
                <td id="imgmenu5" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(5)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>帮助文档管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu5" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="Help_Content/Board_Help_Class.aspx?BuyOrSalse=Salse">卖家帮助分类</a></li>
                            <li><a target="main" href="Help_Content/Board_HelpContent.aspx?BuyOrSalse=Salse">卖家帮助文档</a></li>
                            <li><a target="main" href="Help_Content/Board_Help_Class.aspx?BuyOrSalse=Buy">买家帮助分类</a></li>
                            <li><a target="main" href="Help_Content/Board_HelpContent.aspx?BuyOrSalse=Buy">买家帮助文档</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--帮助文档管理 End-->

            <tr <%=DisPlay(255)%>>
                <td class="menu_title" onmouseover="this.className='menu_title2';" onmouseout="this.className='menu_title';"
                    background="image/title_bg_quit.gif" height="25">
                    <span>信息统计</span>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="right.aspx">
                                <%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>微店相关信息</a> </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

