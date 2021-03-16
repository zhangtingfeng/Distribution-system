using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class _01BoardINC_Manage_ShopClient_ShopPar : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
        private EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = new EggsoftWX.Model.tab_ShopClient_ShopPar();


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_ShopPar")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                SetClass(sender, e);
            }
        }


        public String DisPlayPower(string stringDisPlayPower)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            string strNotDisPlay = " style=\"DISPLAY: none\"";
            bool boolPower = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, stringDisPlayPower);
            if (boolPower)
            {
                strNotDisPlay = "";
            }
            return strNotDisPlay;
        }


        private void SetClass(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + str_Pub_ShopClientID);


                if (tab_ShopClient_ShopPar_Model != null)
                {

                    Textbox_Price_MoneyShopShareGiveMoney.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.ShopShareGiveMoney.toDecimal());
                    Textbox_GouWuQuan_ShopShareGiveVouchers.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers.toDecimal());
                    Textbox_Good_Money.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveMoney.toDecimal());
                    Textbox_Good_GouWuQuan.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers.toDecimal());
                    Textbox_Money_Subs.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.SubscribeGiveMoney.toDecimal());
                    Textbox2_GouWuQuan_Subs.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.SubscribeGiveVouchers.toDecimal());
                    Textbox_Money_FirstVisit.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.Money_FirstVisitShop.toDecimal());
                    Textbox_GouWuQuan_FirstVisit.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GouWuQuan_FirstVisitShop.toDecimal());
                    Textbox_SubscribeTipInfo.Text = String.IsNullOrEmpty(tab_ShopClient_ShopPar_Model.SubscribeTipInfo) ? "亲,您还没有关注我们,点这里开始关注." : tab_ShopClient_ShopPar_Model.SubscribeTipInfo;
                    CheckBoxList_AskAgent.Items[1].Selected = tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy.toBoolean();
                    CheckBox_PayMoneyMustHaveAddress.Checked = tab_ShopClient_ShopPar_Model.PayMoneyMustHaveAddress.toBoolean();
                    CheckBox_OnlyShowLunBoTu.Checked = tab_ShopClient_ShopPar_Model.DeafaultOnlyShowAnounceBitmap.toBoolean();
                    CheckBox_GiveMoneyAfterOntime.Checked = tab_ShopClient_ShopPar_Model.GiveMoneyAfterOntime.toBoolean();
                    CheckBox_BankTime.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "OnlyBankTime");//银行工作日

                    //CheckBox_BuyMySelfIfGetMoney.Checked = tab_ShopClient_ShopPar_Model.BuyMySelfIfGetMoney.toBoolean();
                    //CheckBox_TopAgent.Checked = tab_ShopClient_ShopPar_Model.TopAgent.toBoolean();

                    Textbox_LimitMoney.Text = Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.LimitMoney_AskMoney.toDecimal());
                    string strLimitMoney_PresentFrequency = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "LimitMoney_PresentFrequency").toString();///提现频率限制
                    if (string.IsNullOrEmpty(strLimitMoney_PresentFrequency)) strLimitMoney_PresentFrequency = "Unlimited";///默认不限制 提现频率限制单位
                    RadioButtonList_LimitMoney_PresentFrequency.SelectedValue = strLimitMoney_PresentFrequency;
                    Textbox_LimitMoney_MAX.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "LimitMoney_MAX");///
                    Textbox_LimitMoney_OnceEveryDay.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "LimitMoney_OnceEveryDay");///

                }

                CheckBox_GoodsShowYunFei.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "GoodsShowYunFei");
                //CheckBox_ShareFirstManORLastMan.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "ShareFirstManORLastMan");
                CheckBox_UserDrawMoneyShareFriend.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "UserDrawMoneyShareFriend");
                CheckBox_CloseShareGouWuQuan.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "CloseShareGouWuQuan");
                CheckBox1_CloseShareXianJinHongBao.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "CloseShareXianJinHongBao");


                CheckBox_EveryOneAutoAgentOnlyIsAngel.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "EveryOneAutoAgentOnlyIsAngel");//天使分销功能，对标微信小店功能，任何访问都自动给予代理权，不过只有提出代理申请的用户才能参与分销提成、团队奖励
                CheckBox_CloseGoodsShareAndStatus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "CloseGoodsShareAndStatus");//商品页面的分享访问头像及统计可关闭，商户根据自身需要可选择勾选关闭


                CheckBox_TempletVisitMessage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "TempletVisitMessage");
                CheckBox_TemplePayMessage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "TempletPayMessage");
                CheckBox_weixinMultiDuoKeFu.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "weixinMultiDuoKeFu");///微信多客服
                CheckBox_V3_js_API.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "V3_js_API");///启用新版微信支付V3版本（js API支付）
                CheckBox_Quick.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "OneKeyQuickPay");//启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））
                Textbox_VouchersShopName.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "VouchersShopName");///高腾币
                Textbox_AgentShopTextDesc.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "AgentShopTextDesc");///我为蓝梦代言          ///

                Textbox_ShareShopXianJin_EveryDay.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ShareShopXianJin_EveryDay");///
                Textbox_ShareShopGouWuQuan_EveryDay.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ShareShopGouWuQuan_EveryDay");///
                Textbox_ShareGoodXianJin_EveryDay.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ShareGoodXianJin_EveryDay");///
                Textbox_ShareGoodGouWuQuan_EveryDay.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ShareGoodGouWuQuan_EveryDay");///

                Textbox_SignWorkingEveryDay_Money.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "SignWorkingEveryDay_Money");///
                Textbox_SignWorkingEveryDay_GouWuQuan.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "SignWorkingEveryDay_GouWuQuan");///

                Textbox_ScanAgentErWeiMaMoney.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ScanAgentErWeiMaMoney_Money");///扫描代理二维码奖励现金
                Textbox_ScanAgentErWeiMaGouWuQuan.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ScanAgentErWeiMaGouWuQuan_GouWuQuan");///扫描代理二维码奖励购物券



                #region 更新商品时自动更新代理商的经销商品范围，代理商不用重新挑选商品
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(str_Pub_ShopClientID));
                CheckBox_AutoMidifyAgentGoods.Checked = Convert.ToBoolean(Model_tab_ShopClient.AutoMidifyAgentGoods);
                #endregion

                #region 接收消息选项
                set_stringAcceptMSGList(Model_tab_ShopClient);
                #endregion

                #region 商品分享量基数 商品点赞量基数 商品分享人数基数
                Textbox_GoodShareBase.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "GaoDao_GoodShareBase");///
                Textbox_HitCount.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "GaoDao_HitCount");///
                Textbox_SharePeopleNum.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "GaoDao_SharePeopleNum");///zzz
                Textbox_VisitPeopleBaseNum.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "GaoDao_VisitPeopleBaseNum");///zzz
                #endregion 商品分享量基数 商品点赞量基数 商品分享人数基数

                #region 会员自助付款 充值
                Textbox1DoSelf_51_GoodID.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "DoSelf_51_GoodID");///
                Textbox1InputMoney_GoodID.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "InputMoney_GoodID");///
                #endregion 会员自助付款 充值

                #region 每个有效转发咨询奖励
                Textbox_BonusMoney_ShareGuidePages.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "BonusMoney_ShareGuidePages");///每个有效转发咨询奖励现金
                Textbox_BonusGouWuQuan_ShareGuidePages.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "BonusGouWuQuan_ShareGuidePages");///每个有效转发咨询奖励购物券
                #endregion 每个有效转发咨询奖励


                #region 是否启用优惠券及购物红包功能
                CheckBox_Shopping_Vouchers.Checked = Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers);
                #endregion

                #region 是否推送优惠券消息
                Textbox_SendYouHuiQuanID.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "SendYouHuiQuanID");///



                #endregion 是否推送优惠券消息

                //#region 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）
                Textbox_ConsumptionCapital_OperationCenterID.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ConsumptionCapital_OperationCenterID");///
                Textbox_YunYingZhongXin_AdvanceAgentID.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "YunYingZhongXin_AdvanceAgentID");///
                //CheckBox_ShowConsumerWealthAgreement.Checked = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "ShowConsumerWealthAgreement");


                Textbox_tab_AnnouncePic.Text = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "AnnouncePic_Height");///为轮播图设置固定高度

                //#endregion 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）

                btnAdd.Text = "保 存";

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SaveMode();



            JsUtil.ShowMsg("修改成功!", "01BoardINC_Manage_ShopClient_ShopPar.aspx?type=Modify");
        }
        private void SaveMode()
        {
            string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (String.IsNullOrEmpty(Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "TempleVisitMessage")) && String.IsNullOrEmpty(Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "TempleWisdomVisitMessage")))
            {
                if (CheckBox_TempletVisitMessage.Checked)
                {
                    JsUtil.ShowMsg("必须在开发模式中设置访客消息通知或者智能访客消息通知 模板ID，才能启用浏览消息微信模板!", -1);
                    return;
                }
            }
            if (String.IsNullOrEmpty(Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "TemplePayMessage")))
            {
                if (CheckBox_TemplePayMessage.Checked)
                {
                    JsUtil.ShowMsg("必须在开发模式中设置成功付款通知 模板ID，才能启用成功付款通知微信模板!", -1);
                    return;
                }
            }
            if (!CheckBox_V3_js_API.Checked && CheckBox_Quick.Checked)///必须先启用V3支付
            {
                JsUtil.ShowMsg("启用一键快捷支付必须先启用V3支付!", -1);
                return;
                //Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "OneKeyQuickPay", CheckBox_Quick.Checked);///启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））
            }
            #region 是否推送优惠券消息
            Int32 Int32SendYouHuiQuanID = Textbox_SendYouHuiQuanID.Text.toInt32();
            if (Int32SendYouHuiQuanID > 0)
            {
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLL_tab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme Model_tab_ShopClient_Shopping_VouchersScheme = BLL_tab_ShopClient_Shopping_VouchersScheme.GetModel("ID=@ID and ShopClientID=@ShopClientID and HowToGet=0", Int32SendYouHuiQuanID, str_Pub_ShopClientID.toInt32());

                if (Model_tab_ShopClient_Shopping_VouchersScheme == null)
                {
                    JsUtil.ShowMsg("线上发放的优惠券编号不合法!", -1);
                    return;

                }
            }


            #endregion 是否推送优惠券消息



            #region 运营中心默认配置编号（中心ID）
            if (str_Pub_ShopClientID == "21")
            {
                EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                if (Textbox_ConsumptionCapital_OperationCenterID.Text.toInt32() != 0 && !BLL_b002_OperationCenter.Exists("ID=@ID and ShopClient_ID=@ShopClient_ID", Textbox_ConsumptionCapital_OperationCenterID.Text.toInt32().ToString(), str_Pub_ShopClientID))
                {
                    JsUtil.ShowMsg("未找到匹配的运营中心", -1);
                    return;
                }
            }
            #endregion 运营中心默认配置编号（中心ID）
            #region 批准运营中心关联代理编号
            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
            if (Textbox_YunYingZhongXin_AdvanceAgentID.Text.toInt32() != 0 && !BLL_tab_ShopClient_Agent_Level.Exists("ID=" + Textbox_YunYingZhongXin_AdvanceAgentID.Text.toInt32().ToString() + " and ShopClientID=" + str_Pub_ShopClientID))
            {
                JsUtil.ShowMsg("未找到匹配批准运营中心关联代理编号 关联代理编号 (团队性质编号)", -1);
                return;
            }
            #endregion 批准运营中心关联代理编号



            tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + str_Pub_ShopClientID);

            Decimal shopMoney = 0;
            Decimal shopGouWuQuan = 0;
            Decimal GoodMoney = 0;
            Decimal GoodGouWuQuan = 0;
            Decimal SubscribeMoney = 0;
            Decimal SubscribeGouWuQuan = 0;
            Decimal Money_FirstVisit = 0;
            Decimal GouWuQuan_FirstVisit = 0;
            Decimal LimitMoney_AskMoney = 0;
            Decimal ShareShopXianJin_EveryDay = 0;
            Decimal ShareShopGouWuQuan_EveryDay = 0;
            Decimal ShareGoodXianJin_EveryDay = 0;
            Decimal ShareGoodGouWuQuan_EveryDay = 0;
            Decimal SignWorkingEveryDay_Money = 0;///每日签到奖励现金
            Decimal SignWorkingEveryDay_GouWuQuan = 0;//每日签到奖励购物券
            Decimal ScanAgentErWeiMaMoney_Money = 0;//扫描代理二维码奖励现金
            Decimal ScanAgentErWeiMaGouWuQuan_GouWuQuan = 0;///扫描代理二维码奖励购物券
            Int32 int32GaoDao_GoodShareBase = 0;
            Int32 int32GaoDao_HitCount = 0;
            Int32 int32GaoDao_SharePeopleNum = 0;
            Int32 int32GaoDao_VisitPeopleBaseNum = 0;
            #region 会员自助付款 充值
            Int32 int32Self_51_GoodID = 0;
            Int32 int32InputMoney_GoodID = 0;
            #endregion 会员自助付款 充值

            Decimal.TryParse(Textbox_Price_MoneyShopShareGiveMoney.Text, out shopMoney);
            Decimal.TryParse(Textbox_GouWuQuan_ShopShareGiveVouchers.Text, out shopGouWuQuan);
            Decimal.TryParse(Textbox_Good_Money.Text, out GoodMoney);
            Decimal.TryParse(Textbox_Good_GouWuQuan.Text, out GoodGouWuQuan);
            Decimal.TryParse(Textbox_Money_Subs.Text, out SubscribeMoney);
            Decimal.TryParse(Textbox2_GouWuQuan_Subs.Text, out SubscribeGouWuQuan);
            Decimal.TryParse(Textbox_Money_FirstVisit.Text, out Money_FirstVisit);
            Decimal.TryParse(Textbox_GouWuQuan_FirstVisit.Text, out GouWuQuan_FirstVisit);
            Decimal.TryParse(Textbox_LimitMoney.Text, out LimitMoney_AskMoney);
            Decimal.TryParse(Textbox_ShareShopXianJin_EveryDay.Text, out ShareShopXianJin_EveryDay);
            Decimal.TryParse(Textbox_ShareShopGouWuQuan_EveryDay.Text, out ShareShopGouWuQuan_EveryDay);
            Decimal.TryParse(Textbox_ShareGoodXianJin_EveryDay.Text, out ShareGoodXianJin_EveryDay);
            Decimal.TryParse(Textbox_ShareGoodGouWuQuan_EveryDay.Text, out ShareGoodGouWuQuan_EveryDay);
            Decimal.TryParse(Textbox_SignWorkingEveryDay_Money.Text, out SignWorkingEveryDay_Money);
            Decimal.TryParse(Textbox_SignWorkingEveryDay_GouWuQuan.Text, out SignWorkingEveryDay_GouWuQuan);
            Decimal.TryParse(Textbox_ScanAgentErWeiMaMoney.Text, out ScanAgentErWeiMaMoney_Money);///扫描代理二维码奖励现金
            Decimal.TryParse(Textbox_ScanAgentErWeiMaGouWuQuan.Text, out ScanAgentErWeiMaGouWuQuan_GouWuQuan);//扫描代理二维码奖励购物券
            Int32.TryParse(Textbox_GoodShareBase.Text, out int32GaoDao_GoodShareBase);
            Int32.TryParse(Textbox_HitCount.Text, out int32GaoDao_HitCount);
            Int32.TryParse(Textbox_SharePeopleNum.Text, out int32GaoDao_SharePeopleNum);
            Int32.TryParse(Textbox_VisitPeopleBaseNum.Text, out int32GaoDao_VisitPeopleBaseNum);

            #region 会员自助付款 充值
            Int32.TryParse(Textbox1DoSelf_51_GoodID.Text, out int32Self_51_GoodID);
            Int32.TryParse(Textbox1InputMoney_GoodID.Text, out int32InputMoney_GoodID);
            #endregion 会员自助付款 充值


            #region 每个有效转发咨询奖励
            Decimal BonusMoney_ShareGuidePages = 0;//每个有效转发咨询奖励现金
            Decimal BonusGouWuQuan_ShareGuidePages = 0;///每个有效转发咨询奖励购物券
            Decimal.TryParse(Textbox_BonusMoney_ShareGuidePages.Text, out BonusMoney_ShareGuidePages);///扫描代理二维码奖励现金
            Decimal.TryParse(Textbox_BonusGouWuQuan_ShareGuidePages.Text, out BonusGouWuQuan_ShareGuidePages);//扫描代理二维码奖励购物券
            #endregion 每个有效转发咨询奖励



            #region 旧有
            string strSubscribeTipInfo = Eggsoft.Common.CommUtil.NoHTML(Textbox_SubscribeTipInfo.Text.Trim());

            if (tab_ShopClient_ShopPar_Model != null)
            {
                tab_ShopClient_ShopPar_Model.ShopShareGiveMoney = shopMoney;
                tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers = shopGouWuQuan;
                tab_ShopClient_ShopPar_Model.GoodShareGiveMoney = GoodMoney;
                tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers = GoodGouWuQuan;
                tab_ShopClient_ShopPar_Model.SubscribeGiveMoney = SubscribeMoney;
                tab_ShopClient_ShopPar_Model.SubscribeGiveVouchers = SubscribeGouWuQuan;
                tab_ShopClient_ShopPar_Model.Money_FirstVisitShop = Money_FirstVisit;
                tab_ShopClient_ShopPar_Model.GouWuQuan_FirstVisitShop = GouWuQuan_FirstVisit;
                tab_ShopClient_ShopPar_Model.SubscribeTipInfo = strSubscribeTipInfo;

                //tab_ShopClient_ShopPar_Model.AskAgentAuto = CheckBoxList_AskAgent.Items[0].Selected;///微信封杀 临时关闭
                tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy = CheckBoxList_AskAgent.Items[1].Selected;///微信封杀 临时关闭
                tab_ShopClient_ShopPar_Model.PayMoneyMustHaveAddress = CheckBox_PayMoneyMustHaveAddress.Checked;
                tab_ShopClient_ShopPar_Model.DeafaultOnlyShowAnounceBitmap = CheckBox_OnlyShowLunBoTu.Checked;
                tab_ShopClient_ShopPar_Model.GiveMoneyAfterOntime = CheckBox_GiveMoneyAfterOntime.Checked;
                Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "OnlyBankTime", CheckBox_BankTime.Checked);//银行工作日

                //tab_ShopClient_ShopPar_Model.BuyMySelfIfGetMoney = CheckBox_BuyMySelfIfGetMoney.Checked;
                //tab_ShopClient_ShopPar_Model.TopAgent = CheckBox_TopAgent.Checked;


                tab_ShopClient_ShopPar_Model.LimitMoney_AskMoney = LimitMoney_AskMoney;


                tab_ShopClient_ShopPar_bll.Update(tab_ShopClient_ShopPar_Model);
            }
            else
            {
                tab_ShopClient_ShopPar_Model = new EggsoftWX.Model.tab_ShopClient_ShopPar();
                tab_ShopClient_ShopPar_Model.ShopShareGiveMoney = shopMoney;
                tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers = shopGouWuQuan;
                tab_ShopClient_ShopPar_Model.GoodShareGiveMoney = GoodMoney;
                tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers = GoodGouWuQuan;
                tab_ShopClient_ShopPar_Model.SubscribeGiveMoney = SubscribeMoney;
                tab_ShopClient_ShopPar_Model.SubscribeGiveVouchers = SubscribeGouWuQuan;
                tab_ShopClient_ShopPar_Model.Money_FirstVisitShop = Money_FirstVisit;
                tab_ShopClient_ShopPar_Model.GouWuQuan_FirstVisitShop = GouWuQuan_FirstVisit;
                tab_ShopClient_ShopPar_Model.SubscribeTipInfo = strSubscribeTipInfo;
                tab_ShopClient_ShopPar_Model.LimitMoney_AskMoney = LimitMoney_AskMoney;

                //tab_ShopClient_ShopPar_Model.AskAgentAuto = CheckBoxList_AskAgent.Items[0].Selected;///微信封杀 临时关闭
                tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy = CheckBoxList_AskAgent.Items[1].Selected;///微信封杀 临时关闭
                tab_ShopClient_ShopPar_Model.PayMoneyMustHaveAddress = CheckBox_PayMoneyMustHaveAddress.Checked;

                tab_ShopClient_ShopPar_Model.DeafaultOnlyShowAnounceBitmap = CheckBox_OnlyShowLunBoTu.Checked;
                tab_ShopClient_ShopPar_Model.GiveMoneyAfterOntime = CheckBox_GiveMoneyAfterOntime.Checked;
                Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "OnlyBankTime", CheckBox_BankTime.Checked);//银行工作日

                //tab_ShopClient_ShopPar_Model.BuyMySelfIfGetMoney = CheckBox_BuyMySelfIfGetMoney.Checked;
                //tab_ShopClient_ShopPar_Model.TopAgent = CheckBox_TopAgent.Checked;




                tab_ShopClient_ShopPar_Model.ShopClientID = Int32.Parse(str_Pub_ShopClientID);
                tab_ShopClient_ShopPar_bll.Add(tab_ShopClient_ShopPar_Model);
            }
            #endregion 旧有
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "LimitMoney_MAX", Textbox_LimitMoney_MAX.Text.toDecimal().ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "LimitMoney_OnceEveryDay", Textbox_LimitMoney_OnceEveryDay.Text.toInt32().ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "LimitMoney_PresentFrequency", RadioButtonList_LimitMoney_PresentFrequency.SelectedValue.ToString());//////提现频率限制


            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "GoodsShowYunFei", CheckBox_GoodsShowYunFei.Checked);
            //Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShareFirstManORLastMan", CheckBox_ShareFirstManORLastMan.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "UserDrawMoneyShareFriend", CheckBox_UserDrawMoneyShareFriend.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "CloseShareGouWuQuan", CheckBox_CloseShareGouWuQuan.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "CloseShareXianJinHongBao", CheckBox1_CloseShareXianJinHongBao.Checked);

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "EveryOneAutoAgentOnlyIsAngel", CheckBox_EveryOneAutoAgentOnlyIsAngel.Checked);//天使分销功能，对标微信小店功能，任何访问都自动给予代理权，不过只有提出代理申请的用户才能参与分销提成、团队奖励
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "CloseGoodsShareAndStatus", CheckBox_CloseGoodsShareAndStatus.Checked);//商品页面的分享访问头像及统计可关闭，商户根据自身需要可选择勾选关闭

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "TempletVisitMessage", CheckBox_TempletVisitMessage.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "TempletPayMessage", CheckBox_TemplePayMessage.Checked);

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "weixinMultiDuoKeFu", CheckBox_weixinMultiDuoKeFu.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "V3_js_API", CheckBox_V3_js_API.Checked);///启用新版微信支付V3版本（js API支付）
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "OneKeyQuickPay", CheckBox_Quick.Checked);///启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "AgentShopTextDesc", Textbox_AgentShopTextDesc.Text.Trim());///我为蓝梦代言

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "VouchersShopName", Textbox_VouchersShopName.Text.Trim());///高腾币
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShareShopXianJin_EveryDay", ShareShopXianJin_EveryDay.ToString());
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShareShopGouWuQuan_EveryDay", ShareShopGouWuQuan_EveryDay.ToString());
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShareGoodXianJin_EveryDay", ShareGoodXianJin_EveryDay.ToString());
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShareGoodGouWuQuan_EveryDay", ShareGoodGouWuQuan_EveryDay.ToString());

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "SignWorkingEveryDay_Money", SignWorkingEveryDay_Money.ToString());
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "SignWorkingEveryDay_GouWuQuan", SignWorkingEveryDay_GouWuQuan.ToString());

            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ScanAgentErWeiMaMoney_Money", ScanAgentErWeiMaMoney_Money.ToString());///扫描代理二维码奖励现金
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ScanAgentErWeiMaGouWuQuan_GouWuQuan", ScanAgentErWeiMaGouWuQuan_GouWuQuan.ToString());///扫描代理二维码奖励购物券


            #region 更新商品时自动更新代理商的经销商品范围，代理商不用重新挑选商品
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(str_Pub_ShopClientID));
            Model_tab_ShopClient.AutoMidifyAgentGoods = CheckBox_AutoMidifyAgentGoods.Checked;
            #endregion

            #region 接收消息选项

            Model_tab_ShopClient.XML = get_stringAcceptMSGList(Model_tab_ShopClient.XML);

            #endregion


            #region 商品分享量基数 商品点赞量基数 商品分享人数基数
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "GaoDao_GoodShareBase", int32GaoDao_GoodShareBase.ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "GaoDao_HitCount", int32GaoDao_HitCount.ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "GaoDao_SharePeopleNum", int32GaoDao_SharePeopleNum.ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "GaoDao_VisitPeopleBaseNum", int32GaoDao_VisitPeopleBaseNum.ToString());///
            #endregion 商品分享量基数 商品点赞量基数 商品分享人数基数

            #region 会员自助付款 充值
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "DoSelf_51_GoodID", int32Self_51_GoodID.ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "InputMoney_GoodID", int32InputMoney_GoodID.ToString());///
            #endregion 会员自助付款 充值

            #region 每个有效转发咨询奖励
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "BonusMoney_ShareGuidePages", BonusMoney_ShareGuidePages.ToString());///每个有效转发咨询奖励现金
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "BonusGouWuQuan_ShareGuidePages", BonusGouWuQuan_ShareGuidePages.ToString());///每个有效转发咨询奖励购物券
            #endregion 每个有效转发咨询奖励


            #region 是否启用优惠券及购物红包功能
            Model_tab_ShopClient.Shopping_Vouchers = CheckBox_Shopping_Vouchers.Checked;
            #endregion

            #region 是否推送优惠券消息


            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "SendYouHuiQuanID", Int32SendYouHuiQuanID.toString());///
            #endregion 是否推送优惠券消息



            BLL_tab_ShopClient.Update(Model_tab_ShopClient);


            #region 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ConsumptionCapital_OperationCenterID", Textbox_ConsumptionCapital_OperationCenterID.Text.toInt32().ToString());///
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "YunYingZhongXin_AdvanceAgentID", Textbox_YunYingZhongXin_AdvanceAgentID.Text.toInt32().ToString());///
            //Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "ShowConsumerWealthAgreement", CheckBox_ShowConsumerWealthAgreement.Checked);
            #endregion 运营中心默认配置编号（ConsumptionCapital_OperationCenterID）

            ///为轮播图设置固定高度
            Eggsoft_Public_CL.Pub.boolSaveShowPower(str_Pub_ShopClientID, "AnnouncePic_Height", Textbox_tab_AnnouncePic.Text.toInt32().ToString());///


        }




        protected void set_stringAcceptMSGList(EggsoftWX.Model.tab_ShopClient Model)
        {
            if (string.IsNullOrEmpty(Model.XML) == false)
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model.XML, System.Text.Encoding.UTF8);

                if (string.IsNullOrEmpty(myFahuoDan.AcceptMsgList) == false)
                {
                    String[] stringPowerList = myFahuoDan.AcceptMsgList.Split(',');

                    for (int i = 0; i < stringPowerList.Length; i++)
                    {
                        CheckBoxLis_AcceptMsgList.Items[i].Selected = stringPowerList[i] == "1";
                    }
                }
            }
        }

        protected string get_stringAcceptMSGList(string strModel_XML)
        {
            string stringAcceptMsgList = "";
            for (int i = 0; i < CheckBoxLis_AcceptMsgList.Items.Count; i++)
            {
                string str0or1 = CheckBoxLis_AcceptMsgList.Items[i].Selected ? "1" : "0";
                if (i == 0)
                {
                    stringAcceptMsgList = str0or1;
                }
                else
                {
                    stringAcceptMsgList += "," + str0or1;
                }
            }



            if (string.IsNullOrEmpty(strModel_XML) == false)
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strModel_XML, System.Text.Encoding.UTF8);
                myFahuoDan.AcceptMsgList = stringAcceptMsgList;
                strModel_XML = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);
            }
            else
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = new Eggsoft_Public_CL.XML__Class_Shop_Client();
                myFahuoDan.AcceptMsgList = stringAcceptMsgList;
                strModel_XML = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);
            }
            return strModel_XML;
        }
    }
}