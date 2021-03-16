<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_29RolePower.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange.Manage_29RolePower" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>设置角色</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        p {
            margin-top: 10px;
        }

        .auto-style2 {
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>设置角色</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>角色名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtRoleName" runat="server" Width="376px" CssClass="l_input" ToolTip="如营业员、发货员、管理员、内勤、老板"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="角色名称不能为空!"
                        ControlToValidate="txtRoleName">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="250" height="35">
                    <strong>权限范围：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu1_BasicSetting" runat="server" />
                        <strong><span class="auto-style2">基本设置</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu1_BasicSetting_BoardINC_Manage" runat="server" />
                        基本资料<br />
                        <asp:CheckBox ID="CheckBox_submenu1_BasicSetting_ShopPar" runat="server" />
                        相关参数<br />
                        <asp:CheckBox ID="CheckBox_submenu1_BasicSetting_Style_Model" runat="server" />
                        模版选择<br />
                        <asp:CheckBox ID="CheckBox_submenu1_BasicSetting_MakeHtml" runat="server" />
                        生成缓存(静态页)<br />
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage" runat="server" />
                        <strong><span class="auto-style2">应用管理</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_AgentChecked" runat="server" />
                        分销商/代理商管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_Good" runat="server" />
                        商品管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_WeiTuanGou" runat="server" />
                        微团购<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_GuidePages" runat="server" />
                        资讯管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_Vouchers" runat="server" />
                        优惠券<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_FreightTemplate" runat="server" />
                        运费模板<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_Class_BoardSet" runat="server" />
                        商品分类管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_AnnouncePic" runat="server" />
                        轮播图片管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_O2O_Shop" runat="server" />
                        O2O门店管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_NetRootMenu" runat="server" />
                        底部菜单管理<br />
                        <asp:CheckBox ID="CheckBox_submenu2_ApplicationManage_URLShow" runat="server" />
                        网站常用链接<br />
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage" runat="server" />
                        <strong><span class="auto-style2">订单管理</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage_NeedMoney" runat="server" />
                        待收款<br />
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage_WaitGiveGoods" runat="server" />
                        待发货<br />
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage_UserGetGoods" runat="server" />
                        待收货<br />
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage_Wait_Finished" runat="server" />
                        已完成<br />
                        <asp:CheckBox ID="CheckBox_submenu3_OrderManage_Board" runat="server" />
                        结算管理
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage" runat="server" /><strong>
                            <span class="auto-style2">扩展应用</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_GuWuQuanChange" runat="server" />
                        积分兑换<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_ZhuanZhuanChe" runat="server" />
                        大转盘 刮刮卡<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_OnlineBaoMing" runat="server" />
                        在线报名<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_LightApp" runat="server" />
                        轻应用<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_GameSendJiFenBoard" runat="server" />
                        游戏推广<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_25WeiXianChang_BoardSet" runat="server" />
                        现场活动<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_Board_WeiKanJian" runat="server" />
                        微砍价<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_Board_01ZC_Project" runat="server" />
                        众筹<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_16SendMoney" runat="server" />
                        分红设计方案<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_Board28Member" runat="server" />
                        会员卡管理   
                        <br />
                        <asp:CheckBox ID="CheckBox_ExtendManage_32NoticeGuidePages" runat="server" />
                        公告管理    
                    </p>

                    <p>
                        <asp:CheckBox ID="CheckBox_o2oShop" runat="server" /><strong><span class="auto-style2">消费积分（适合门店）</span></strong><br />
                        <asp:CheckBox ID="CheckBox_o2oShop_cardMember" runat="server" />会员积分奖励政策<br />
                       
                    </p>

                    <p>
                        <asp:CheckBox ID="CheckBox_ConsumptionCapitalManage" runat="server" /><strong>
                            <span class="auto-style2">消费财富系统</span></strong><br />
                        <asp:CheckBox ID="CheckBox_02OperationCenter" runat="server" />
                        运营中心管理<br />
                        <asp:CheckBox ID="CheckBox_04OperationGoods" runat="server" />
                        消费商品及财富返还管理<br />
                        <asp:CheckBox ID="CheckBox_08FullEveryDay" runat="server" />
                        每日运营统计<br />
                        <asp:CheckBox ID="CheckBox_10BalanceofPaymentStatistics" runat="server" />
                        运营中心收支统计<br />
                        <asp:CheckBox ID="CheckBox_09OperationUserStatus" runat="server" />
                        运营中心会员统计<br />
                        <asp:CheckBox ID="CheckBox_12OrderDetailEveryDay" runat="server" />
                        运营中心订单统计<br />
                        <asp:CheckBox ID="CheckBox_14WealthMoneyControlOperationCenter" runat="server" />
                        积分管理<br />
                        <asp:CheckBox ID="CheckBox_18OperationWtiteOrder" runat="server" />
                        运营中心录单管理<br />
                        <asp:CheckBox ID="CheckBox_16CheckModifyParent" runat="server" />
                        运营中心申请调整上下级关系<br />
                        <asp:CheckBox ID="CheckBox_13OperationCenter_OrderManage" runat="server" />
                        消费财富系统线下收单录入                       
                    </p>

                    <p>
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment" runat="server" /><strong><span class="auto-style2">开发模式管理</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_BasicInfo" runat="server" />
                        基本信息管理<br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_BoardJPG" runat="server" />
                        批量素材管理<br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_Resource" runat="server" />
                        素材管理<br />
                        <%-- <asp:CheckBox ID="CheckBox_submenu44_Devolopment_Resource_1" runat="server" />
                        文本 单图文 多图文<br />--%>
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_Subscribe" runat="server" />
                        关注时回复-微信<br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_KeyAnswer" runat="server" />
                        关键词回复-微信<br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_KeyAnswer_Default" runat="server" />
                        默认回复-微信<br />
                        <asp:CheckBox ID="CheckBox_submenu44_Devolopment_WeiXinMenu" runat="server" />
                        公众平台菜单管理-微信
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus" runat="server" />
                        <strong><span class="auto-style2">数据统计</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_GuidePagesVisit" runat="server" />
                        咨询访问统计<br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_GoodsVisit" runat="server" />
                        商品访问统计<br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_UserStatus" runat="server" />
                        用户统计<br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_AgentStatus" runat="server" />
                        分销代理商统计<br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_UserSignWorkingEveryDay" runat="server" />
                        用户签到统计<br />
                        <asp:CheckBox ID="CheckBox_submenu4_DataStutus_UserOrderDetailEveryDay" runat="server" />
                        订单统计
                    </p>
                    <p>

                        <asp:CheckBox ID="CheckBox_submenu434_MoneyManage" runat="server" />
                        <strong><span class="auto-style2">财务管理</span></strong><br />

                        <asp:CheckBox ID="CheckBox_submenu434_MoneyManage_IS_UserFinance_check_DrawMoney" runat="server" />
                        用户申请提现<br />

                        <asp:CheckBox ID="CheckBox_submenu434_MoneyManage_IS_FinanceAdmin_check_ReturnMoney" runat="server" />
                        订单取消申请
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBox_submenu65_ShopAdvance" runat="server" />
                        <strong><span class="auto-style2">商家高级</span></strong><br />
                        <asp:CheckBox ID="CheckBox_submenu65_ShopAdvance_FenXiaoLevel" runat="server" />
                        分销代理级别<br />
                        <asp:CheckBox ID="CheckBox_submenu65_ShopAdvance_Agent_Level" runat="server" />
                        代理级别管理<br />
                        <asp:CheckBox ID="CheckBox_submenu65_ShopAdvance_BoardGood_XML" runat="server" />
                        商家字典<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_UserMoneyControl" runat="server" />
                        积分管理<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_Board28Member_MemberBonus" runat="server" />
                        充值奖励政策管理<br />
                        <asp:CheckBox ID="CheckBox_submenu411_ExtendManage_Board_29AdminPower" runat="server" />
                        权限管理
                    </p>
                    <p>
                        <asp:CheckBox ID="CheckBoxWuLIuAdvance" runat="server" />
                        <strong><span class="auto-style2">物流高级</span></strong><br />
                        <asp:CheckBox ID="CheckBoxWuLIuAdvance_ChannelChange" runat="server" />
                        物流渠道管理<br />
                        <asp:CheckBox ID="CheckBoxWuLIuAdvance_ZoneChange" runat="server" />
                        渠道分区管理<br />
                        <asp:CheckBox ID="CheckBoxWuLIuAdvance_PriceChange" runat="server" />
                        分区运价管理                       
                    </p>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
