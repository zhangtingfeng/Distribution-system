<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin.Left" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>代理分销平台</title>
    <link href="/ClientAdmin/skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="/Scripts/md5.js?version=js201709121928"></script>
    <script type="text/javascript" src="/ClientAdmin/skin/default.js?version=js201709121928"></script>
    <script type="text/javascript">
        var varShopClientID =<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>;
        var varNetUserSafeCode ="<%=Eggsoft.Common.DESCrypt.hex_md5_2(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")+"Aakfnkasjfdaskjfhas")%>";
        var varServiceURL = "<%=Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()%>";

        function aa(Dir) { tt.doScroll(Dir); Timer = setTimeout('aa("' + Dir + '")', 100) } //这里100为滚动速度
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
        var varMemoryOldsid = 0;
        var varMemoryOldsidOpened = 0;
        function showsubmenu(sid) {



            whichEl = eval("submenu" + sid);
            imgmenu = eval("imgmenu" + sid);
            if (whichEl.style.display == "none") {
                eval("submenu" + sid + ".style.display=\"\";");
                imgmenu.background = "image/menuup.gif";

                if (varMemoryOldsid != 0) {
                    if (varMemoryOldsid != sid) {
                        if (varMemoryOldsid != sid) {
                            eval("submenu" + varMemoryOldsid + ".style.display=\"none\";");
                        }
                    }
                }
                varMemoryOldsid = sid;
                eval("var varMemoryOldsidOpened" + sid + " = 1");

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
        top.document.title = "代理分销平台（微云基石）";
    </script>



</head>
<body class="leftbg">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" align="center">
            <tr>
                <td class="menu_title" onmouseover="this.className='menu_title2';" onmouseout="this.className='menu_title';"
                    background="image/title_bg_quit.gif" height="25">
                    <span><b><a target="main" href="Right.aspx"><font color="215DC6">回到首页</font></a></b>
                        | <a target="_top" href="Logout.aspx"><font color="215DC6">退出</font></a></span>
                </td>
            </tr>
            <tr>
                <td align="center" onmouseover="aa('up')" onmouseout="StopScroll()">&nbsp;
                </td>
            </tr>
        </table>
        <!--   <script>
            var he = document.body.clientHeight - 105
            document.write("<div id=tt style=height:" + he + ";overflow:hidden>")
        </script>-->
        <table class="LiSec_menu" cellspacing="0" cellpadding="0" width="100%" align="center">
            <!--基本管理 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting")%>>
                <td id="imgmenu1" class="menu_title" onmouseover="this.className='menu_title2';" onclick="showsubmenu(1)" onmouseout="this.className='menu_title';" style="cursor: hand" background="image/menudown.gif" height="25">
                    <span>基本设置</span>
                </td>
            </tr>
            <tr>
                <td id="submenu1" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_BoardINC_Manage")%>>
                                <asp:HyperLink ID="HyperLink_BoardINC_Manage" runat="server" Target="main">基本资料</asp:HyperLink></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_ShopPar")%>>
                                <asp:HyperLink ID="HyperLink_tab_ShopClient_ShopPar" runat="server" Target="main">相关参数</asp:HyperLink></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_Style_Model")%>><a target="main" href="08Style_Model/Style_Model.aspx">模版选择</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_MakeHtml")%>>
                                <asp:HyperLink ID="HyperLink_MakeHtml" runat="server" ToolTip="云更新" Target="main">生成缓存(静态页)</asp:HyperLink></li>
                        </ul>

                    </div>
                </td>
            </tr>
            <!--基本管理 End-->






            <!--商品管理 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage")%>>
                <td id="imgmenu2" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(2)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>应用管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu2" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_AgentChecked")%>><a target="main" href="07AgentChecked/Board_AgentChecked.aspx">分销商/代理商管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Good")%>><a target="main" href="18tab_GoodClass/Board_Good.aspx">商品管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_WeiTuanGou")%>><a target="main" href="26WeiTuanGou/Board_WeiTuanGou.aspx">微团购</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_GuidePages")%>><a target="main" href="04GuidePages/GuidePages_Board.aspx">资讯管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Vouchers")%>><a target="main" href="01Shopping_Vouchers/Shopping_Vouchers_Board.aspx">优惠券</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_FreightTemplate")%>><a target="main" href="10tab_ShopClient/FreightTemplate.aspx">运费模板</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Class_BoardSet")%>><a target="main" href="18tab_GoodClass/tab_Class_BoardSet.aspx">商品分类管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_AnnouncePic")%>><a target="main" href="03AnnouncePic/BoardAnnouncePic.aspx">轮播图片管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_O2O_Shop")%>><a target="main" href="17O2O_Shop/Board_O2O_Shop.aspx">O2O门店管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_NetRootMenu")%>><a target="main" href="11RootMenu/NetRootMenu.aspx">底部菜单管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_URLShow")%>><a target="main" href="11RootMenu/URLShow.aspx">网站常用链接</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--版块管理 End-->
            <!--订单管理 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage")%>>
                <td id="imgmenu3" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(3)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>订单管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu3" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_NeedMoney")%>><a target="main" href="19tab_Order/tab_Order_Board_NeedMoney.aspx?ini=true">待收款</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_WaitGiveGoods")%>><a target="main" href="19tab_Order/tab_Order_Board_WaitGiveGoods.aspx?ini=true">待发货</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_UserGetGoods")%>><a target="main" href="19tab_Order/tab_Order_Board_Wait_UserGetGoods.aspx?ini=true">待收货</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_Wait_Finished")%>><a target="main" href="19tab_Order/tab_Order_Board_Wait_Finished.aspx?ini=true">已完成</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("OrderManage_Board")%>><a target="main" href="19tab_Order/tab_Fund_Order_Board.aspx?ini=true">结算管理</a></li>

                        </ul>
                    </div>
                </td>
            </tr>
            <!--订单管理 End-->






            <!--扩展应用 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage")%>>
                <td id="imgmenu411" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(411)" onmouseout="this.className='menu_title';" style="cursor: hand;"
                    background="image/menudown.gif" height="25">
                    <span>扩展应用</span>
                </td>
            </tr>
            <tr>
                <td id="submenu411" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_GuWuQuanChange")%>>
                                <a target="main" href="02GuWuQuanChange/Board02GuWuQuanChange.aspx">积分兑换</a>
                            </li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_ZhuanZhuanChe")%>>
                                <a target="main" href="23WeiHuoDong/GuaGuaKa_ZhuanZhuanChe.aspx">大转盘 刮刮卡</a>
                            </li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_OnlineBaoMing")%>><a target="main" href="23WeiHuoDong/OnlineBaoMing/OnlineBaoMing.aspx">在线报名</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_LightApp")%>><a target="main" href="23WeiHuoDong/LightApp/LightApp_Boad.aspx">轻应用</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_GameSendJiFenBoard")%>><a target="main" href="22GameSendJiFen/GameSendJiFenBoard.aspx">游戏推广</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_25WeiXianChang_BoardSet")%>><a target="main" href="25WeiXianChang/25WeiXianChang_BoardSet.aspx">现场活动</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_WeiKanJian")%>><a target="main" href="24WeiKanJian/Board_WeiKanJian.aspx">微砍价</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_01ZC_Project")%>><a target="main" href="27ZC_Project/Board_01ZC_Project.aspx">众筹</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_16SendMoney")%>><a target="main" href="16SendMoney/16SendMoney_Board.aspx">分红设计方案</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board28Member")%>><a target="main" href="28Member/Board_28Member.aspx">会员卡管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_32NoticeGuidePages")%>><a target="main" href="32NoticeGuidePages/01NoticeGuidePagesBoard.aspx">公告管理</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--分红设计 End-->


            <!--门店积分应用 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("CheckBox_o2oShop")%>>
                <td id="imgmenu20190115" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(20190115)" onmouseout="this.className='menu_title';" style="cursor: hand;"
                    background="image/menudown.gif" height="25">
                    <span>门店消费积分</span>
                </td>
            </tr>
            <tr>
                <td id="submenu20190115" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("CheckBox_o2oShop_cardMember")%>>
                                <a target="main" href="80o2oShop_cardMember/Board80o2oShop_cardMember.aspx">会员积分奖励政策</a>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--门店积分应用 End-->

            <!--消费财富系统 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage")%>>
                <td id="imgmenu412" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(412)" onmouseout="this.className='menu_title';" style="cursor: hand;"
                    background="image/menudown.gif" height="25">
                    <span>消费财富系统(线下)</span>
                </td>
            </tr>
            <tr>
                <td id="submenu412" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_02OperationCenter")%>><a target="main" href="31ConsumptionCapital/02OperationCenter.aspx">运营中心管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_04OperationGoods")%>><a target="main" href="31ConsumptionCapital/04OperationGoods.aspx">消费商品及财富返还管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_08FullEveryDay")%>><a target="main" href="31ConsumptionCapital/08FullEveryDay.aspx">每日运营统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_10BalanceofPaymentStatistics")%>><a target="main" href="31ConsumptionCapital/10BalanceofPaymentStatistics.aspx">运营中心收支统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_09OperationUserStatus")%>><a target="main" href="31ConsumptionCapital/09OperationUserStatus.aspx">运营中心会员统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_12OrderDetailEveryDay")%>><a target="main" href="31ConsumptionCapital/12OrderDetailEveryDay.aspx">运营中心订单统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_14WealthMoneyControlOperationCenter")%>><a target="main" href="31ConsumptionCapital/14WealthMoneyControlOperationCenter.aspx">积分管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_18OperationWtiteOrder")%>><a target="main" href="31ConsumptionCapital/18OperationWtiteOrder.aspx">运营中心录单系统</a>

                                <div class="LeftBarShowFloatText">
                                    <div class="LeftBarShowFloatTextChild">
                                        <span id="Info_b013_WriteOrderByOperation" class="LeftBarShowFloatTextChildGouWuCheNumShow">10</span>
                                    </div>
                                </div>
                            </li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_16CheckModifyParent")%>><a target="main" href="31ConsumptionCapital/16CheckModifyParent.aspx">运营中心申请调整上下级关系</a>
                                <div class="LeftBarShowFloatText">
                                    <div class="LeftBarShowFloatTextChild">
                                        <span id="Info_b010_AskModifyParent" class="LeftBarShowFloatTextChildGouWuCheNumShow">10</span>
                                    </div>
                                </div>

                            </li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_13OperationCenter_OrderManage")%>><a target="main" href="31ConsumptionCapital/13OperationCenter_OrderManage.aspx?type=add&CallBackUrl=../19tab_Order/tab_Order_Board_Wait_UserGetGoods.aspx">消费财富系统线下收单录入</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--消费财富系统 End-->

            <!--开发模式 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment")%>>
                <td id="imgmenu44" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(44)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>开发模式管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu44" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_BasicInfo")%>><a target="main" href="05EngineerMode/BasicInfo.aspx">基本信息管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_BoardJPG")%>><a target="main" href="05EngineerMode/BoardJPG.aspx">批量素材管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_Resource")%>><a target="main" href="05EngineerMode/Resource-1.aspx">素材管理</a><br />
                                <div id="FontSmall">
                                    <table style="width: 100%; border-width: thin; border-color: #FFFF00; border-top-width: medium;">
                                        <tr>
                                            <td>
                                                <a target="main" href="05EngineerMode/Resource-1.aspx"><span>文本</span></a>
                                                <a target="main" href="05EngineerMode/Resource-2.aspx"><span>单图文</span></a> <a target="main"
                                                    href="05EngineerMode/Resource-3.aspx"><span>多图文</span></a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_Subscribe")%>><a target="main" href="05EngineerMode/Subscribe.aspx">关注时回复-微信</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_KeyAnswer")%>><a target="main" href="05EngineerMode/KeyAnswer_Board.aspx">关键词回复-微信</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_KeyAnswer_Default")%>><a target="main" href="05EngineerMode/KeyAnswer_Default.aspx">默认回复-微信</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_WeiXinMenu")%>><a target="main" href="05EngineerMode/WeiXinMenu.aspx">公众平台菜单管理-微信</a></li>
                            <%--<li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_SendYouHuiQuan")%>><a target="main" href="05EngineerMode/01YouHuiQuan.aspx">扫描代理推送优惠券</a></li>--%>
                        </ul>
                    </div>
                    <br>
                </td>
            </tr>
            <!--开发模式 End-->



            <!--数据统计 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus")%>>
                <td id="imgmenu4" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(4)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>数据统计</span>
                </td>
            </tr>
            <tr>
                <td id="submenu4" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_GuidePagesVisit")%>><a target="main" href="09System_Status/GuidePagesVisit.aspx">咨询访问统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_GoodsVisit")%>><a target="main" href="09System_Status/GoodsVisit.aspx">商品访问统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_UserStatus")%>><a target="main" href="09System_Status/UserStatus.aspx">用户统计</a></li>
                            <%--<li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_AgentStatus")%>><a target="main" href="20Agent_Status/AgentStatus.aspx">分销代理商统计</a></li>暂时去掉20171220--%>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_UserSignWorkingEveryDay")%>><a target="main" href="09System_Status/UserSignWorkingEveryDay.aspx">用户签到统计</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("DataStutus_OrderDetail")%>><a target="main" href="09System_Status/01OrderDetailEveryDay.aspx">订单统计</a></li>

                        </ul>
                    </div>
                </td>
            </tr>
            <!--数据统计 End-->


            <!--财务管理 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage")%>>
                <td id="imgmenu434" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(434)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>财务管理</span>
                </td>
            </tr>
            <tr>
                <td id="submenu434" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage_IS_UserFinance_check_DrawMoney")%>><a target="main" href="12UserAskMoney/IS_UserFinance_check_DrawMoney.aspx">用户申请提现</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage_IS_FinanceAdmin_check_ReturnMoney")%>><a target="main" href="13Order_Cancel/IS_FinanceAdmin_check_ReturnMoney.aspx">订单取消申请</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("MoneyManage_SMSWatch")%>><a target="main" href="30SMSWatch/Board_30SMSWatch.aspx">查看手机绑定短消息</a></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <!--财务管理 End-->




            <!--微信微店管理 Start-->
            <%--<tr <%=DisPlay("Good_VisitAnalysis")%>>
                <td id="imgmenu5" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(5)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>消息管理</span>
                </td>
            </tr>
            <tr <%=DisPlay("Good_VisitAnalysis")%>>
                <td id="submenu5">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="MessageShow/MessageShowBoard.aspx">用户消息查看</a></li>
                        </ul>
                    </div>
                </td>
            </tr>--%>
            <!--订单管理 End-->



            <!--商家高级 含XML Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance")%>>
                <td id="imgmenu65" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(65)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>商家高级</span>
                </td>
            </tr>
            <tr>
                <td id="submenu65" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance_FenXiaoLevel")%>><a target="main" href="15Advance/MultiFenXiaoLevel_Board.aspx">团队奖励方案管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance_Agent_Level")%>><a target="main" href="07AgentChecked/Board_Agent_Level.aspx">代理级别管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ShopAdvance_BoardGood_XML")%>><a target="main" href="15Advance/BoardGood_XML.aspx">商家字典</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_UserMoneyControl")%>><a target="main" href="21GuWuQuanAndMoneyControl/UserMoneyControl.aspx">积分管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_Board_29AdminPower")%>><a target="main" href="29ShopClientPower/Board_29AdminPower.aspx">账号权限管理</a></li>
                        </ul>
                </td>
            </tr>
            <!--商家高级 含XML End-->

            <!--物流运输查询 Start-->
            <tr <%=Eggsoft_Public_CL.PubMember.DisPlayPower("WuLIuAdvance")%>>
                <td id="imgmenu134" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(134)" onmouseout="this.className='menu_title';" style="cursor: hand"
                    background="image/menudown.gif" height="25">
                    <span>物流高级</span>
                </td>
            </tr>
            <tr>
                <td id="submenu134" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("WuLIuAdvance_ChannelChange")%>><a target="main" href="34WuLiu/Board34ChannelChange.aspx">物流渠道管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("WuLIuAdvance_ZoneChange")%>><a target="main" href="34WuLiu/Board34ZoneChange.aspx">渠道分区管理</a></li>
                            <li <%=Eggsoft_Public_CL.PubMember.DisPlayPower("WuLIuAdvance_PriceChange")%>><a target="main" href="34WuLiu/Board34PriceChange.aspx">分区运价管理</a></li>
                        </ul>
                </td>
            </tr>
            <!--物流运输查询  End-->

            <!--PC增强版-->
            <tr>
                <td id="imgmenu565" class="menu_title" onmouseover="this.className='menu_title2';"
                    onclick="showsubmenu(565)" onmouseout="this.className='menu_title';" style="cursor: hand; display: none;"
                    background="image/menudown.gif" height="25">
                    <span>电脑PC版（测试）</span>
                </td>
            </tr>
            <%-- <tr>
                <td id="submenu565" style="display: none">
                    <div class="sec_menu">
                        <ul>
                            <li><a target="main" href="50PC_ShopClient/BoardINC_Manage.aspx?type=modify">基本资料</a></li>
                        </ul>
                </td>
            </tr>--%>
            <!--PC增强版-->
        </table>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            showMyInfoNum("Info_b013_WriteOrderByOperation", "Info_b013_WriteOrderByOperation", varShopClientID, varNetUserSafeCode, varServiceURL);///运营中心录入订单  需要处理
            showMyInfoNum("Info_b010_AskModifyParent", "Info_b010_AskModifyParent", varShopClientID, varNetUserSafeCode, varServiceURL);///运营中心申请修改上下级关系
        })

    </script>

</body>
</html>
