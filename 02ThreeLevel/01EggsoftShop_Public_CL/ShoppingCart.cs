using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{
    /// <summary>
    /// 全场免运费使用
    /// </summary>
    public class AllcartYunFeiList
    {
        private string _yunfeiText;
        private int _intGoodID;
        private Decimal _DecimalGoodPrice;
        private Decimal _DecimalAllkg;
        private Decimal _DecimalAllGoodPrice;
        private Decimal _DecimalAllFright;
        private int _GoodCount;
        private int _FreightTempletID;
        private Decimal _MAXKgNoFright;
        private Decimal _MAXMoneyNoFright;
        private int _MAXIntNoFright;
        public string stryunfeiText
        {
            get { return _yunfeiText; }
            set { _yunfeiText = value; }
        }
        public int intGoodID
        {
            get { return _intGoodID; }
            set { _intGoodID = value; }
        }
        public Decimal DecimalAllkg
        {
            get { return _DecimalAllkg; }
            set { _DecimalAllkg = value; }
        }
        public Decimal DecimalGoodPrice
        {
            get { return _DecimalGoodPrice; }
            set { _DecimalGoodPrice = value; }
        }
        public Decimal DecimalAllGoodPrice
        {
            get { return _DecimalAllGoodPrice; }
            set { _DecimalAllGoodPrice = value; }
        }
        public Decimal DecimalAllFright
        {
            get { return _DecimalAllFright; }
            set { _DecimalAllFright = value; }
        }
        public int GoodCount
        {
            get { return _GoodCount; }
            set { _GoodCount = value; }
        }
        public int FreightTempletID
        {
            get { return _FreightTempletID; }
            set { _FreightTempletID = value; }
        }


        public Decimal MAXKgNoFright
        {
            get { return _MAXKgNoFright; }
            set { _MAXKgNoFright = value; }
        }
        public Decimal MAXMoneyNoFright
        {
            get { return _MAXMoneyNoFright; }
            set { _MAXMoneyNoFright = value; }
        }
        public int MAXIntNoFright
        {
            get { return _MAXIntNoFright; }
            set { _MAXIntNoFright = value; }
        }

    }

    /// <summary>
    /// 计算各个财富积分 减分使用
    /// </summary>
    public class Wealth_OperationUserList
    {
        public int OrderID { get; set; }
        public int OrderDetailID { get; set; }
        public int b008_OpterationUserActiveReturnMoneyOrderNumID { get; set; }
        public Decimal? CurMoney { get; set; }
        /// <summary>
        /// 使用的钱
        /// </summary>
        public Decimal? UsedMoney { get; set; }
        /// <summary>
        /// 反写 是进入那个 购物车ID使用
        /// </summary>
        public int? b015_OrderDetail_WealthBuyID { get; set; }
    }

    /// <summary>
    ///ShoppingCart 的摘要说明
    /// </summary>
    public class ShoppingCart
    {
        public static Dictionary<string, DateTime?> tasksAddToShoppingCart = new Dictionary<string, DateTime?>();
        public static Object mytasksObjectShoppingCart = new Object();


        //private static Object thisLockAddToShoppingCart = new Object();
        /// <summary>
        /// 加入购物车 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="GoodID">无论是 微砍价  还是 团购 该值必须填写商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心</param>
        /// <param name="pInt_QueryString_ParentID"></param>
        /// <param name="intBuyCount"></param>
        /// <param name="MultiBuyType"></param>
        /// <param name="VouchersNum_List"></param>
        /// <param name="buyType_KanJia_Tuan_Clound_Chou">0是 正常 购买  商品 来源 类型  表的 主键   正常订单 这个 为空  .微砍价 团购 会出现相关主键 商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单</param>
        /// <param name="KanJia_Tuan_Clound_ChouID">微砍价  团购表的主键</param>
        /// <param name="KanJia_Tuan_Clound_ChouActualID">微砍价  团购行动记录表的的主键  不同的购买类型  放置不同的实际主键</param>
        /// <param name="KanJia_Tuan_Clound_ChouActualID">用户的购买留言  。众筹购买 可能会留下留言</param>
        /// <returns></returns>
        public static int AddToShoppingCart(int userID, int GoodID, int intBuyCount, Int32 MultiBuyType, String[] Money_List, String[] Wealth_List, String[] VouchersNum_List, int buyType_KanJia_Tuan_Clound_Chou, int KanJia_Tuan_Clound_ChouID, int KanJia_Tuan_Clound_ChouActualID, string strUserSaySomthing)
        /// public static int AddToShoppingCart(int userID, int GoodID, int pInt_QueryString_ParentID, int intBuyCount, Int32 MultiBuyType, String[] Money_List, String[] Wealth_List, String[] VouchersNum_List, int buyType_KanJia_Tuan_Clound_Chou, int KanJia_Tuan_Clound_ChouID, int KanJia_Tuan_Clound_ChouActualID, string strUserSaySomthing)

        {
            string strAdd000000NumKey = Eggsoft.Common.StringNum.Add000000Num(userID, 8) + Eggsoft.Common.StringNum.Add000000Num(GoodID, 8);


            int intdoReturn = -1;////建议返回的变量  处理过程使用
            int intLastReturn = 0;//最终返回的变量
            //lock (thisLockAddToShoppingCart)
            try
            {
                #region 防止多个线程执行一个strLicenseNo+strCompanyCode任务   

                DateTime? DateTimeRunning = null;
                lock (mytasksObjectShoppingCart)
                {
                    tasksAddToShoppingCart.TryGetValue(strAdd000000NumKey, out DateTimeRunning);
                    if (DateTimeRunning == null) tasksAddToShoppingCart[strAdd000000NumKey] = DateTime.Now;
                }
                while (DateTimeRunning != null)
                {
                    if ((DateTime.Now - DateTimeRunning.Value).TotalSeconds < 20)
                    {
                        System.Threading.Thread.Sleep(1000);
                        lock (mytasksObjectShoppingCart)
                        {
                            tasksAddToShoppingCart.TryGetValue(strAdd000000NumKey, out DateTimeRunning);
                            if (DateTimeRunning == null)
                            {
                                tasksAddToShoppingCart[strAdd000000NumKey] = DateTime.Now;
                                break;
                            }
                        }
                    }
                    else
                    {
                        #region 超时报警日志
                        Eggsoft.Common.debug_Log.Call_WriteLog("GoodID" + GoodID.ToString() + " GoodID" + GoodID.ToString() + "  " + tasksAddToShoppingCart.toJsonString(), "购物车添加", "超时报警日志");
                        //Console.WriteLine();
                        return 0;
                        #endregion
                    }
                }

                #endregion




                EggsoftWX.BLL.tab_Order_ShopingCart my_BLL_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

                #region 检查库存限制 秒杀限制等等  变量的准备
                bool boolIfcanAddCart = false;

                Int64 LimitKunCount = GoodP.Get_KuCunCount_From_Good_ID(GoodID);
                if (LimitKunCount < intBuyCount)
                {
                    boolIfcanAddCart = false;////库存不足了
                    return -44;
                }
                int LimitTimerBuy_MaxSalesCount = GoodP.getLimitTimerBuy_MaxSalesCount_FromGoodID(GoodID);
                bool boolIfLimitTimerBuy = GoodP.get_Bool_LimitTimer_FromGoodID(GoodID);//是否秒杀限制期间  是的话 一人只能买这些，不能多买了
                //加入购物车 困库存就减少

                //int intCanCart = LimitKunCount > LimitTimerBuy_MaxSalesCount ? LimitTimerBuy_MaxSalesCount : LimitKunCount;
                //你的购物车 有多少
                string strCanKunCunWhere = "select sum(GoodIDCount) as CanAddCount  from tab_Order_ShopingCart where GoodID=" + GoodID + " and IsDeleted<>1 and UserID=" + userID;
                string stringintCheckHaveAddCartGoodID = my_BLL_tab_Order_ShopingCart.SelectList(strCanKunCunWhere).Tables[0].Rows[0][0].ToString();
                int intCheckHaveAddCartGoodID = 0;
                int.TryParse(stringintCheckHaveAddCartGoodID, out intCheckHaveAddCartGoodID);
                //你的已支付的订单中 有多少 //不在当前秒杀期间的 创建的订单 其不算数，不检查
                int int_LimitTimerBuy_CheckHaveAddCart_Order_AllGoods = 0;

                if (boolIfLimitTimerBuy)
                {
                    EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList().GetModel(GoodID);

                    EggsoftWX.BLL.View_Order_AllGoods BLL_View_Order_AllGoods = new EggsoftWX.BLL.View_Order_AllGoods();


                    string strThisTime = "datediff(ss,'" + Model_View_SecondSalesGoodList.LimitTimerBuy_StartTime + "',CreatDateTime)> 0";
                    strThisTime += " and datediff(ss,'" + Model_View_SecondSalesGoodList.LimitTimerBuy_EndTime + "',CreatDateTime)< 0";
                    string str_LimitTimerBuy_CanKunCunWhere_Order_AllGoods = "select sum(OrderCount) as OrderCount  from View_Order_AllGoods where " + strThisTime + " and GoodID=" + GoodID + " and UserID=" + userID;
                    bool boolExsit = BLL_View_Order_AllGoods.Exists(strThisTime + " and GoodID=" + GoodID + " and UserID=" + userID);
                    if (boolExsit)
                    {
                        string str_LimitTimerBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods = BLL_View_Order_AllGoods.SelectList(str_LimitTimerBuy_CanKunCunWhere_Order_AllGoods).Tables[0].Rows[0][0].ToString();
                        int.TryParse(str_LimitTimerBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods, out int_LimitTimerBuy_CheckHaveAddCart_Order_AllGoods);
                    }

                }
                #endregion

                #region 实现限制购买
                if (boolIfLimitTimerBuy)
                {
                    //购物车已存在的                                现在要加入的         已有的订单                                         秒杀期间限制购买的
                    if ((intCheckHaveAddCartGoodID + intBuyCount + int_LimitTimerBuy_CheckHaveAddCart_Order_AllGoods) <= LimitTimerBuy_MaxSalesCount)
                    {
                        boolIfcanAddCart = true;
                    }
                    else
                    {
                        boolIfcanAddCart = false;
                        intdoReturn = -1;
                    }
                }
                else
                {
                    //购物车已存在的                                现在要加入的         已有的订单                                         秒杀期间限制购买的
                    if ((intBuyCount) <= LimitTimerBuy_MaxSalesCount)
                    {
                        boolIfcanAddCart = true;
                    }
                    else
                    {
                        boolIfcanAddCart = false;                    //Eggsoft.Common.JsUtil.ShowMsg("购买限制，请等待下一轮秒杀！");
                        intdoReturn = -2;
                    }
                }
                #endregion 实现限制购买

                #region 检查团购是否限制性 购买  同一微信号（本店不支持一个手机切换微信号）是否可以在一次团购活动中购买多个。（在购物车/订单表中只能购买一次）
                if (buyType_KanJia_Tuan_Clound_Chou == 2)
                {
                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(KanJia_Tuan_Clound_ChouID);
                    if ((Model_tab_TuanGou != null) && (Model_tab_TuanGou.BuyMultiOnlyOneAccount == true))////有限制性购买的问题
                    {
                        string str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods = "select sum(OrderCount) as OrderCount  from View_Order_AllGoods where GoodType=2 and GoodTypeId=" + KanJia_Tuan_Clound_ChouID + " and GoodID=" + GoodID + " and UserID=" + userID;
                        EggsoftWX.BLL.View_Order_AllGoods BLL_View_Order_AllGoods = new EggsoftWX.BLL.View_Order_AllGoods();
                        string str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods = BLL_View_Order_AllGoods.SelectList(str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods).Tables[0].Rows[0][0].ToString();
                        int int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods = 0;
                        int.TryParse(str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods, out int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods);
                        if (int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods > 0)
                        {
                            intdoReturn = -22;///订单表中已存在。。。可能是未支付
                            boolIfcanAddCart = false;
                        }
                        else
                        {

                            EggsoftWX.BLL.tab_Order_ShopingCart MMMtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                            string str_Limittab_TuanGouBuy_tab_Order_ShopingCart = "select sum(GoodIDCount) as CartCount  from tab_Order_ShopingCart where GoodType=2 and GoodTypeId=" + KanJia_Tuan_Clound_ChouID + " and IsDeleted<>1 and GoodID=" + GoodID + " and UserID=" + userID;
                            string strCartCount = MMMtab_Order_ShopingCart.SelectList(str_Limittab_TuanGouBuy_tab_Order_ShopingCart).Tables[0].Rows[0][0].ToString();
                            int intCartCount = 0;
                            int.TryParse(strCartCount, out intCartCount);
                            if (intCartCount > 0)
                            {
                                intdoReturn = -23;///购物车中已存在
                                boolIfcanAddCart = false;
                            }
                        }
                    }
                }


                #endregion


                #region 检查众筹档位是否限制性 购买  同一微信号（本店不支持一个手机切换微信号）是否可以在一次档位活动中购买多个。（在购物车/订单表中只能购买一次）
                if (buyType_KanJia_Tuan_Clound_Chou == 3)
                {
                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(KanJia_Tuan_Clound_ChouActualID);
                    if ((Model_tab_ZC_01Product_Support != null) && (Model_tab_ZC_01Product_Support.OnlyBuyOneOnlyOneAccount == true))////有限制性购买的问题
                    {
                        string str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods = "select sum(OrderCount) as OrderCount  from View_Order_AllGoods where GoodType=3 and GoodTypeId=" + KanJia_Tuan_Clound_ChouID + " and GoodID=" + GoodID + " and GoodTypeIdBuyInfo='" + KanJia_Tuan_Clound_ChouActualID + "' and UserID=" + userID;
                        EggsoftWX.BLL.View_Order_AllGoods BLL_View_Order_AllGoods = new EggsoftWX.BLL.View_Order_AllGoods();
                        string str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods = BLL_View_Order_AllGoods.SelectList(str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods).Tables[0].Rows[0][0].ToString();
                        int int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods = 0;
                        int.TryParse(str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods, out int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods);
                        if (int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods > 0)
                        {
                            intdoReturn = -22;///订单表中已存在。。。可能是未支付
                            boolIfcanAddCart = false;
                        }
                        else
                        {

                            EggsoftWX.BLL.tab_Order_ShopingCart MMMtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                            string str_Limittab_TuanGouBuy_tab_Order_ShopingCart = "select sum(GoodIDCount) as CartCount  from tab_Order_ShopingCart where GoodType=3 and GoodTypeId=" + KanJia_Tuan_Clound_ChouID + " and IsDeleted<>1 and GoodTypeIdBuyInfo='" + KanJia_Tuan_Clound_ChouActualID + "' and GoodID=" + GoodID + " and UserID=" + userID;
                            string strCartCount = MMMtab_Order_ShopingCart.SelectList(str_Limittab_TuanGouBuy_tab_Order_ShopingCart).Tables[0].Rows[0][0].ToString();
                            int intCartCount = 0;
                            int.TryParse(strCartCount, out intCartCount);
                            if (intCartCount > 0)
                            {
                                intdoReturn = -23;///购物车中已存在
                                boolIfcanAddCart = false;
                            }
                        }
                    }
                }


                #endregion


                #region 检查使用的 现金 购物积分  财富积分是否合法
                #region  如果使用聊购物券,就检查一下购物券是否合法 buyType_KanJia_Tuan_Clound_Chou=0 的 商品 会有这些购物券的 问题
                //检查购物券状态
                if (VouchersNum_List[0] != "0")
                {
                    if (VouchersNum_List[0] == "4")
                    {
                        EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                        EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();
                        Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + VouchersNum_List[1] + "'");
                        if (Model_tab_Shopping_Vouchers == null)
                        {
                            boolIfcanAddCart = false;
                            intdoReturn = -4;
                        }
                        if (Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID.toInt32() > 0)///已被使用了
                        {
                            boolIfcanAddCart = false;
                            intdoReturn = -4;
                        }
                    }
                }
                #endregion buyType_KanJia_Tuan_Clound_Chou=0 的 商品 会有这些购物券的 问题

                if (Money_List[0] != "0")
                {
                    if (Money_List[0] == "1")
                    {
                        Decimal myCountMoney = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(userID, out myCountMoney);
                        if (Money_List[1].toDecimal() > myCountMoney)
                        {
                            boolIfcanAddCart = false;
                            intdoReturn = -4;
                        }
                    }
                }
                if (Wealth_List[0] != "0")
                {
                    Decimal DecimalUseMoney = Wealth_List[1].toDecimal();
                    if (Wealth_List[0] == "1" && DecimalUseMoney > 0)
                    {
                        Decimal myCountTotalWealth_ = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(userID, out myCountTotalWealth_);
                        if (Wealth_List[1].toDecimal() > myCountTotalWealth_)
                        {
                            boolIfcanAddCart = false;
                            intdoReturn = -4;
                        }
                    }
                }
                if (VouchersNum_List[0] != "0")
                {

                    if (VouchersNum_List[0] == "2")//微店红包
                    {
                        Decimal myCountMoney_Vouchers = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(userID, out myCountMoney_Vouchers);
                        if (VouchersNum_List[1].toDecimal() > myCountMoney_Vouchers)
                        {
                            boolIfcanAddCart = false;
                            intdoReturn = -4;
                        }
                    }
                }
                #endregion 

                if (boolIfcanAddCart)
                {
                    #region 天使不享受本店购买提成  运营中心商品不享受本店分销提成
                    //if (userID == pInt_QueryString_ParentID)
                    //{
                    //    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    //    bool boolOnlyIsAngel = BLL_tab_ShopClient_Agent_.Exists("UserID=" + pInt_QueryString_ParentID + "   and IsDeleted=0 and (OnlyIsAngel=1)");///上级代理可能会被取消权限
                    //    if (boolOnlyIsAngel || buyType_KanJia_Tuan_Clound_Chou == 6)
                    //    {
                    //        pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(userID);////修正天使
                    //    }


                    //}
                    #endregion 天使不享受本店购买提成 运营中心商品不享受本店分销提成


                    #region 扣除操作 buyType_KanJia_Tuan_Clound_Chou=0 的 商品 会有这些购物券的 问题
                    int intWealth_List = 0;///0 表示不存在该项   1表示存在该项 并且扣除成功  2表示存在该项但是扣除失败
                    int intMoney_List = 0;///0 表示不存在该项   1表示存在该项 并且扣除成功  2表示存在该项但是扣除失败
                    int intVouchersNum_List = 0;///0 表示不存在该项   1表示存在该项 并且扣除成功  2表示存在该项但是扣除失败







                    #region 1 扣除现金
                    if (Money_List[0] != "0")
                    {
                        if (Money_List[0] == "1")
                        {
                            intMoney_List = 1;
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 80;// 80表示清除购物车
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal.Parse(Money_List[1]);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车" + GoodP.Get_GoodNameFromGoodID(GoodID);
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = userID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            int intAddID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                            if (!(intAddID > 0))
                            {
                                intMoney_List = 2;
                            }
                            //return -1;
                        }
                    }
                    #endregion 扣除现金



                    #region 2 扣除购物券 购物积分
                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();


                    if (VouchersNum_List[0] != "0")
                    {

                        if (VouchersNum_List[0] == "2" && intMoney_List != 2)//微店红包
                        {
                            intVouchersNum_List = 1;
                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal.Parse(VouchersNum_List[1]);
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车" + GoodP.Get_GoodNameFromGoodID(GoodID);
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = userID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            int intAddID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                            if (!(intAddID > 0)) intVouchersNum_List = 2;// return - 1;
                        }
                        else if (VouchersNum_List[0] == "3" && intMoney_List != 2)
                        {
                            intVouchersNum_List = 1;
                            EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge BLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge Model_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                            Model_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Int32.Parse(VouchersNum_List[1]);
                            Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车" + GoodP.Get_GoodNameFromGoodID(GoodID);
                            Model_tab_TotalBeans_Consume_Or_Recharge.UserID = userID;
                            Model_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            int intAddID = BLL_tab_TotalBeans_Consume_Or_Recharge.Add(Model_tab_TotalBeans_Consume_Or_Recharge);
                            if (!(intAddID > 0)) intVouchersNum_List = 2;// return -1;
                        }
                        else if (VouchersNum_List[0] == "4" && intMoney_List != 2)
                        {
                            intVouchersNum_List = 1;
                            Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + VouchersNum_List[1] + "'");
                            Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = -1;
                            Model_tab_Shopping_Vouchers.UserID = userID;
                            Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                            BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                        }
                    }
                    #endregion 3扣除购物券 购物积分


                    #region 3 扣除财富积分

                    ArrayList myArrayListb008_OpterationUserActiveReturnMoneyOrderNum = new ArrayList();

                    if (Wealth_List[0] != "0")
                    {
                        Decimal DecimalUseMoney = Wealth_List[1].toDecimal();
                        if (Wealth_List[0] == "1" && DecimalUseMoney > 0 && intMoney_List != 2 && intVouchersNum_List != 2)
                        {
                            intWealth_List = 1;
                            int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                            EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();

                            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                            string strSQL = "select sum(ReturnMoneyUnit) from b008_OpterationUserActiveReturnMoneyOrderNum where UserID=@UserID and ShopClient_ID=@ShopClient_ID and OrderID is null and ActiveOrderNum>0";
                            Decimal DecimalReturnMoneyUnit = my_BLL_b006_TotalWealth_OperationUser.SelectList(strSQL, userID, intShopClientID).Tables[0].Rows[0][0].toDecimal();
                            if (DecimalReturnMoneyUnit >= DecimalUseMoney)
                            {
                                #region  按照支付时间排序  减去相应 财富表中的积分  这是一个算法
                                string strSQLTable = "select ID,ReturnMoneyUnit,OrderDetailID,OrderID,ActiveOrderNum from b008_OpterationUserActiveReturnMoneyOrderNum where UserID=@UserID and ShopClient_ID=@ShopClient_ID and OrderID is not null order by PayDateTime desc";
                                System.Data.DataTable Data_DataTable = my_BLL_b006_TotalWealth_OperationUser.SelectList(strSQLTable, userID, intShopClientID).Tables[0];


                                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                                {

                                    Wealth_OperationUserList CurWealth_OperationUserList = new Wealth_OperationUserList();
                                    CurWealth_OperationUserList.CurMoney = Data_DataTable.Rows[i]["ReturnMoneyUnit"].toDecimal();
                                    CurWealth_OperationUserList.b008_OpterationUserActiveReturnMoneyOrderNumID = Data_DataTable.Rows[i]["ID"].toInt32();
                                    CurWealth_OperationUserList.OrderID = Data_DataTable.Rows[i]["OrderID"].toInt32();
                                    CurWealth_OperationUserList.OrderDetailID = Data_DataTable.Rows[i]["OrderDetailID"].toInt32();
                                    myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Add(CurWealth_OperationUserList);
                                }

                                Decimal DecimalNeedMoney = DecimalUseMoney;
                                for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                                {
                                    Wealth_OperationUserList curWealth_OperationUser = (Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                                    if (DecimalNeedMoney <= curWealth_OperationUser.CurMoney)
                                    {
                                        curWealth_OperationUser.UsedMoney = DecimalNeedMoney;///curWealth_OperationUser.CurMoney - 
                                        myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                                        break;
                                    }
                                    else
                                    {
                                        curWealth_OperationUser.UsedMoney = curWealth_OperationUser.CurMoney;
                                        myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                                        DecimalNeedMoney = DecimalNeedMoney - (Decimal)curWealth_OperationUser.CurMoney;
                                    }

                                }
                                #endregion  按照支付时间排序  减去相应 财富表中的积分


                                #region  按照支付时间排序  减去相应 财富表中的积分  这是动作
                                for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                                {
                                    Wealth_OperationUserList curWealth_OperationUser = (Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                                    if (curWealth_OperationUser.UsedMoney > 0)
                                    {
                                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                        Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                                        Model_b006_TotalWealth_OperationUser.OrderDetailID = curWealth_OperationUser.OrderDetailID;
                                        Model_b006_TotalWealth_OperationUser.UserID = userID;
                                        Model_b006_TotalWealth_OperationUser.ShopClient_ID = intShopClientID;
                                        Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = curWealth_OperationUser.UsedMoney;
                                        Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "购物车" + GoodP.Get_GoodNameFromGoodID(GoodID);
                                        int intAddID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                                        if (!(intAddID > 0))
                                        {
                                            intWealth_List = 2;
                                            break;
                                        }
                                        //return -1;

                                        EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel(curWealth_OperationUser.b008_OpterationUserActiveReturnMoneyOrderNumID);
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit - curWealth_OperationUser.UsedMoney;
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                        if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit <= 0)
                                        {
                                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum;
                                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + "出局";
                                        }
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                        my_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                        EggsoftWX.Model.b015_OrderDetail_WealthBuy Model_b015_OrderDetail_WealthBuy = new EggsoftWX.Model.b015_OrderDetail_WealthBuy();
                                        Model_b015_OrderDetail_WealthBuy.UserID = userID;
                                        Model_b015_OrderDetail_WealthBuy.OrdetailID = curWealth_OperationUser.OrderDetailID;
                                        Model_b015_OrderDetail_WealthBuy.ShopClientID = intShopClientID;
                                        Model_b015_OrderDetail_WealthBuy.OrderID = curWealth_OperationUser.OrderID;
                                        Model_b015_OrderDetail_WealthBuy.ShopClientID = intShopClientID;
                                        Model_b015_OrderDetail_WealthBuy.UseOrNotuse = true;
                                        Model_b015_OrderDetail_WealthBuy.HowMuchWealth = curWealth_OperationUser.UsedMoney;
                                        Model_b015_OrderDetail_WealthBuy.CreateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                        curWealth_OperationUser.b015_OrderDetail_WealthBuyID = BLL_b015_OrderDetail_WealthBuy.Add(Model_b015_OrderDetail_WealthBuy);
                                        myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                                    }
                                }

                                #region 处理财富积分失败的问题
                                if (intWealth_List == 2)
                                {
                                    /////比较麻烦 以后遇到了再处理
                                    Eggsoft.Common.debug_Log.Call_WriteLog("扣除财富积分出现麻烦事userID=" + userID, "购物车财富积分", "程序报错");

                                }
                                #endregion 处理财富积分失败的问题

                                #endregion   按照支付时间排序  减去相应 财富表中的积分  这是动作
                            }



                        }
                    }



                    #endregion 财富积分


                    #region 考虑扣除失败的问题
                    if (intWealth_List == 2) //谁成功了 就把谁找回来  是按照顺序找的 先1现金 在2购物券 再3财富积分
                    {
                        if (intMoney_List == 1)////谁成功了 就把谁找回来
                        {
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 80;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal.Parse(Money_List[1]);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车财富积分扣除失败返回" + GoodP.Get_GoodNameFromGoodID(GoodID);
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = userID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            int intAddID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                        }
                        if (intVouchersNum_List == 1)////谁成功了 就把谁找回来
                        {
                            if (VouchersNum_List[0] == "2")//微店红包
                            {
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal.Parse(VouchersNum_List[1]);
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车财富积分扣除失败返回" + GoodP.Get_GoodNameFromGoodID(GoodID);
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = userID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                                int intAddID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                            }
                            else if (VouchersNum_List[0] == "3")
                            {
                                intVouchersNum_List = 1;
                                EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge BLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge Model_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                                Model_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Int32.Parse(VouchersNum_List[1]);
                                Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车财富积分扣除失败返回" + GoodP.Get_GoodNameFromGoodID(GoodID);
                                Model_tab_TotalBeans_Consume_Or_Recharge.UserID = userID;
                                Model_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                                int intAddID = BLL_tab_TotalBeans_Consume_Or_Recharge.Add(Model_tab_TotalBeans_Consume_Or_Recharge);
                                if (!(intAddID > 0)) intVouchersNum_List = 2;// return -1;
                            }
                            else if (VouchersNum_List[0] == "4")
                            {
                                intVouchersNum_List = 1;
                                Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + VouchersNum_List[1] + "'");
                                Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = 0;
                                Model_tab_Shopping_Vouchers.UserID = userID;
                                Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                                BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                            }
                        }
                    }
                    if (intVouchersNum_List == 2)
                    {
                        if (intMoney_List == 1)////谁成功了 就把谁找回来  是按照顺序找的   先1现金 在2购物券 再3财富积分
                        {
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 80;// 80表示清除购物车
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal.Parse(Money_List[1]);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购物车财富积分扣除失败返回" + GoodP.Get_GoodNameFromGoodID(GoodID);
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = userID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                            int intAddID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                        }
                    }


                    #endregion 考虑扣除失败的问题
                    if (intWealth_List == 2 || intMoney_List == 2 || intVouchersNum_List == 2)
                    {
                        return -1;////失败处理
                    }

                    #endregion buyType_KanJia_Tuan_Clound_Chou=0 的 商品 会有这些购物券的 问题

                    #region 处理添加进购物车的 过程
                    String str_boolIfcanAddCart_Where = "GoodID=" + GoodID + " and UserID=" + userID + " and MultiBuyType=" + MultiBuyType + " and IsDeleted<>1 ";
                    str_boolIfcanAddCart_Where += " and GoodType=" + buyType_KanJia_Tuan_Clound_Chou + " and GoodTypeId=" + KanJia_Tuan_Clound_ChouID + " and GoodTypeIdBuyInfo='" + KanJia_Tuan_Clound_ChouActualID + "'";

                    bool str_tab_Order_ShopingCart_Exsit = my_BLL_tab_Order_ShopingCart.Exists(str_boolIfcanAddCart_Where);

                    if (str_tab_Order_ShopingCart_Exsit)
                    {
                        #region 购物车已存在
                        //已存在 检查库存

                        ///
                        string strMoneyCredits_List = "";

                        if (Money_List[0] != "0")
                        {
                            if (Money_List[0] == "1")
                            {
                                strMoneyCredits_List = ",MoneyCredits=MoneyCredits +" + Money_List[1] + "";
                            }
                        }

                        string strMoneyWealth_List = "";
                        if (Wealth_List[0] != "0")
                        {
                            if (Wealth_List[0] == "1")
                            {
                                strMoneyWealth_List = ",WealthMoney=WealthMoney +" + Wealth_List[1] + "";
                            }
                        }

                        string strVouchersNum_List = "";
                        if (VouchersNum_List[0] != "0")
                        {
                            if (VouchersNum_List[0] == "2")
                            {
                                strVouchersNum_List = ",MoneyWeBuy8Credits=MoneyWeBuy8Credits +" + VouchersNum_List[1] + "";
                            }
                            else if (VouchersNum_List[0] == "3")
                            {
                                strVouchersNum_List = ",Beans=Beans+" + VouchersNum_List[1] + "";
                            }
                            else if (VouchersNum_List[0] == "4")
                            {
                                strVouchersNum_List = ",VouchersNum_List=isnull(VouchersNum_List,'')+'," + VouchersNum_List[1] + "#" + VouchersNum_List[2] + "'";
                            }
                        }
                        string strUserSaySql = "";
                        if (String.IsNullOrEmpty(strUserSaySomthing) == false)
                        {
                            strUserSaySql = "UserSay=isnull(cast(UserSay as varchar(max)),'')+'" + strUserSaySomthing + "'";
                        }
                        //my_BLL_tab_Order_ShopingCart.Update("UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',GoodIDCount=GoodIDCount+" + intBuyCount + strMoneyCredits_List + strMoneyWealth_List + strVouchersNum_List, str_boolIfcanAddCart_Where);
                        my_BLL_tab_Order_ShopingCart.Update("UpdateTime=getdate(),[UpdateBy]='购物车已存在的更新',GoodIDCount=GoodIDCount+" + intBuyCount + strMoneyCredits_List + strMoneyWealth_List + strVouchersNum_List, str_boolIfcanAddCart_Where);

                        #region 增加购物车未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "购物车修改";
                        Model_b011_InfoAlertMessage.CreateBy = "购物车修改";
                        Model_b011_InfoAlertMessage.UpdateBy = "购物车修改";
                        Model_b011_InfoAlertMessage.UserID = userID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                        Model_b011_InfoAlertMessage.Type = "Info_cart";
                        Model_b011_InfoAlertMessage.TypeTableID = GoodID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物车未处理信息


                        #region 反写购物券 财富积分  记录是那个购物车 有利于程序的清晰
                        EggsoftWX.Model.tab_Order_ShopingCart Modeltab_Order_ShopingCart = my_BLL_tab_Order_ShopingCart.GetModel(str_boolIfcanAddCart_Where);

                        if (Modeltab_Order_ShopingCart != null && Model_tab_Shopping_Vouchers != null && Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID == -1)
                        {
                            if (VouchersNum_List.Length >= 3) Model_tab_Shopping_Vouchers.MoneyUsed = VouchersNum_List[2].toDecimal();
                            Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = Modeltab_Order_ShopingCart.ID;
                            Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                            BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                        }
                        if (myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count > 0)
                        {
                            string strSQLIN = "";

                            for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                            {
                                Wealth_OperationUserList curWealth_OperationUser = (Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                                if (curWealth_OperationUser.b015_OrderDetail_WealthBuyID.toInt32() > 0)
                                {
                                    if (String.IsNullOrEmpty(strSQLIN) == false) strSQLIN += ",";
                                    strSQLIN += "" + curWealth_OperationUser.b015_OrderDetail_WealthBuyID.toString();
                                }
                            }
                            string strUpdate = "update b015_OrderDetail_WealthBuy set ShopingCartID=" + Modeltab_Order_ShopingCart.ID + " where ID in (" + strSQLIN + ")";
                            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdate);
                        }
                        #endregion 反写购物券 记录是那个购物车
                        #endregion 购物车已存在
                    }
                    else
                    {
                        EggsoftWX.Model.tab_Order_ShopingCart Model = new EggsoftWX.Model.tab_Order_ShopingCart();
                        Model.ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                        Model.GoodID = GoodID;
                        Model.GoodIDCount = intBuyCount;
                        Model.UserID = userID;
                        //Model.ParentID = pInt_QueryString_ParentID;
                        //Model.GrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_ParentID);
                        //Model.GreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(Convert.ToInt32(Model.GrandParentID));
                        Model.MultiBuyType = MultiBuyType;
                        Model.CreatTime = DateTime.Now;
                        Model.GoodType = buyType_KanJia_Tuan_Clound_Chou;///2  是 团购类型  运营中心类型6
                        Model.GoodTypeId = KanJia_Tuan_Clound_ChouID;///团购主键    运营中心ID
                        Model.GoodTypeIdBuyInfo = KanJia_Tuan_Clound_ChouActualID.ToString();///2  是 团购商品
                        Model.UserSay = strUserSaySomthing;                                                     ///
                                                                                                                ///                    

                        #region 购物券类型
                        if (Money_List[0] != "0")
                        {
                            if (Money_List[0] == "1")
                            {
                                Model.MoneyCredits = Decimal.Parse(Money_List[1]);
                            }
                        }

                        if (Wealth_List[0] != "0")
                        {
                            if (Wealth_List[0] == "1")
                            {
                                Model.WealthMoney = Decimal.Parse(Wealth_List[1]);
                            }
                        }


                        if (VouchersNum_List[0] != "0")
                        {
                            if (VouchersNum_List[0] == "2")
                            {
                                Model.MoneyWeBuy8Credits = Decimal.Parse(VouchersNum_List[1]);
                            }
                            else if (VouchersNum_List[0] == "3")
                            {
                                Model.Beans = Int16.Parse(VouchersNum_List[1]);
                            }
                            else if (VouchersNum_List[0] == "4")
                            {
                                Model.VouchersNum_List = VouchersNum_List[1] + "#" + VouchersNum_List[2];
                            }
                        }
                        #endregion 购物券类型
                        Int32 Int32ShopingCart = my_BLL_tab_Order_ShopingCart.Add(Model);


                        #region 增加购物车未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "购物车添加";
                        Model_b011_InfoAlertMessage.CreateBy = "购物车添加";
                        Model_b011_InfoAlertMessage.UpdateBy = "购物车添加";
                        Model_b011_InfoAlertMessage.UserID = userID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString());
                        Model_b011_InfoAlertMessage.Type = "Info_cart";
                        Model_b011_InfoAlertMessage.TypeTableID = Int32ShopingCart;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物车未处理信息

                        #region 反写购物券 财富积分 记录是那个购物车  有利于程序的清晰
                        if (Model_tab_Shopping_Vouchers != null && Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID == -1)
                        {
                            if (VouchersNum_List.Length >= 3) Model_tab_Shopping_Vouchers.MoneyUsed = VouchersNum_List[2].toDecimal();
                            Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = Int32ShopingCart;
                            Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                            BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                        }

                        if (myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count > 0)
                        {
                            string strSQLIN = "";

                            for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                            {
                                Wealth_OperationUserList curWealth_OperationUser = (Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                                if (curWealth_OperationUser.b015_OrderDetail_WealthBuyID.toInt32() > 0)
                                {
                                    if (String.IsNullOrEmpty(strSQLIN) == false) strSQLIN += ",";
                                    strSQLIN += "" + curWealth_OperationUser.b015_OrderDetail_WealthBuyID.toString();
                                }
                            }
                            string strUpdate = "update b015_OrderDetail_WealthBuy set ShopingCartID=" + Int32ShopingCart + " where ID in (" + strSQLIN + ")";
                            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdate);
                        }

                        #endregion 反写购物券 记录是那个购物车

                    }
                    #region 减少库存
                    EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    my_BLL_tab_Goods.Update("KuCunCount=KuCunCount-" + intBuyCount, "ID=" + GoodID.ToString());
                    #endregion
                    intLastReturn = 1;



                    #endregion 处理添加进购物车的 过程
                }
                else
                {
                    if (intdoReturn != -1)
                    {
                        intLastReturn = intdoReturn;
                    }
                    else
                    {
                        intLastReturn = 0;
                    }
                }

            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "添加购物车", "超时报警日志程序报错");
            }

            finally
            {
                DateTime? runTime = null;
                lock (mytasksObjectShoppingCart)
                {
                    tasksAddToShoppingCart.TryGetValue(strAdd000000NumKey, out runTime);
                    if (runTime != null)
                    {
                        tasksAddToShoppingCart.Remove(strAdd000000NumKey);
                    }
                }

            }
            return intLastReturn;
        }

        /// <summary> 
        /// 移除购物车子项 
        /// </summary> 
        /// <param></param> 
        public static void MinusShoppingCart(int GoodID)
        {
            int userID = Pub_GetOpenID_And_.getUserIDFromCookies();

            EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            EggsoftWX.Model.tab_Order_ShopingCart my_tab_Order_ShopingCart_Model = my_tab_Order_ShopingCart.GetModel("GoodID=" + GoodID + " and UserID=" + userID + " and IsDeleted<>1");

            if (my_tab_Order_ShopingCart_Model != null)
            {
                if (my_tab_Order_ShopingCart_Model.GoodIDCount > 1)
                {
                    my_tab_Order_ShopingCart_Model.GoodIDCount = my_tab_Order_ShopingCart_Model.GoodIDCount - 1;
                    my_tab_Order_ShopingCart_Model.UpdateTime = DateTime.Now;
                    my_tab_Order_ShopingCart.Update(my_tab_Order_ShopingCart_Model);
                }
                else
                {
                    //my_tab_Order_ShopingCart_Model.IsDeletedTime = DateTime.Now;  触发器中处理时间问题
                    my_tab_Order_ShopingCart.Delete(my_tab_Order_ShopingCart_Model.ID);
                }
            }
            Eggsoft.Common.debug_Log.Call_WriteLog("my_tab_Order_ShopingCart.Delete(my_tab_Order_ShopingCart_Model.ID)");
        }

        /// <summary>
        /// 清空购物车      假的清空 是指 商品被从购物车移进订单表
        /// </summary>
        /// <param name="argClearIfToOrderTable">true  是清空  真正 删除 归还余额等</param>
        /// <param name="ChoiceuserID">服务调用 直接 传ID。前台调用 自己找Cookies</param>
        public static void ClearShoppingCart(bool argClearIfToOrderTable, int ChoiceuserID, string strDeleteReason = "")
        {
            int userID = ChoiceuserID;

            #region 注销购物车未读消息
            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart' and Readed=0", userID, Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString()));
            #endregion 注销购物车未读消息


            #region  归还库存

            EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
            ////false  是到订单表中去  true表示确实是删除 清空购物车
            if (argClearIfToOrderTable == true)  //这时候还删除  什么，，，只有购物车直接清空 才用这个
            {
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                System.Data.DataTable Data_DataTable = my_tab_Order_ShopingCart.GetList("UserID=" + userID + " and IsDeleted<>1 order by id asc").Tables[0];
                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string strThisID = Data_DataTable.Rows[i]["ID"].ToString();
                    string strGoodIDCount = Data_DataTable.Rows[i]["GoodIDCount"].ToString();
                    string strGoodID = Data_DataTable.Rows[i]["GoodID"].ToString();

                    EggsoftWX.BLL.tab_Goods my_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    my_tab_Goods.Update("UpdateTime=getdate(),KuCunCount=KuCunCount+" + Int32.Parse(strGoodIDCount), "id=" + strGoodID);
                    ClearShoppingCartThisID(Int32.Parse(strThisID));
                }
            }
            #endregion 

            #region
            //my_tab_Order_ShopingCart.Delete("UserID=" + userID);///存储过程不能删除 。。。可能是 select @ID = ID from deleted  不能触发
            my_tab_Order_ShopingCart.Update("IsDeleted=1,IsDeletedTime=getdate(),UpdateTime=getdate(),UpdateBy='" + strDeleteReason + "'", "UserID=" + userID + " and IsDeleted=0");
            #endregion
        }


        public static void ClearShoppingCartThisID(int intThisCartID)////归还金额
        {
            lock ("iriuemrjkdfmjdfd" + intThisCartID)
            {
                EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                EggsoftWX.Model.tab_Order_ShopingCart my_Model_tab_Order_ShopingCart = my_tab_Order_ShopingCart.GetModel(intThisCartID);

                //string strGoodIDCountID = Data_DataTable.Rows[i]["GoodIDCount"].ToString();
                //string strGoodID = Data_DataTable.Rows[i]["GoodID"].ToString();
                //string strVouchersNum_List = Data_DataTable.Rows[i]["VouchersNum_List"].ToString();
                //string strBeans = Data_DataTable.Rows[i]["Beans"].ToString();
                //string strMoneyCredits = Data_DataTable.Rows[i]["MoneyCredits"].ToString();
                //string strMoneyWeBuy8Credits = Data_DataTable.Rows[i]["MoneyWeBuy8Credits"].ToString();



                #region---归还金额

                if (Decimal.Round(Convert.ToDecimal(my_Model_tab_Order_ShopingCart.MoneyCredits), 2) > 0)
                {
                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 80;// 80表示清除购物车
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Convert.ToDecimal(my_Model_tab_Order_ShopingCart.MoneyCredits);
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "清空购物车" + GoodP.Get_GoodNameFromGoodID(Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodID));
                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order_ShopingCart.UserID);
                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order_ShopingCart.ShopClientID;
                    int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                    #region 增加账户余额未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = "清空购物车";
                    Model_b011_InfoAlertMessage.UpdateBy = "清空购物车";
                    Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加账户余额未处理信息
                }

                #endregion

                #region---归还财富积分

                if (Decimal.Round(Convert.ToDecimal(my_Model_tab_Order_ShopingCart.WealthMoney), 2) > 0)
                {
                    //intThisCartID
                    EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();
                    System.Data.DataTable Data_DataTable = BLL_b015_OrderDetail_WealthBuy.GetList("userid=@userid and ShopingCartID=@ShopingCartID and UseOrNotuse=1", my_Model_tab_Order_ShopingCart.UserID, intThisCartID).Tables[0];
                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {
                        Int32 Int32b015_OrderDetail_WealthBuy = Data_DataTable.Rows[i]["ID"].toInt32();
                        Int32 OrdetailID = Data_DataTable.Rows[i]["OrdetailID"].toInt32();
                        string strDesc = Data_DataTable.Rows[i]["CreateBy"].toString();
                        Decimal DecimalHowMuchWealth = Data_DataTable.Rows[i]["HowMuchWealth"].toDecimal();

                        EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                        Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                        Model_b006_TotalWealth_OperationUser.OrderDetailID = OrdetailID;
                        Model_b006_TotalWealth_OperationUser.UserID = my_Model_tab_Order_ShopingCart.UserID;
                        Model_b006_TotalWealth_OperationUser.ShopClient_ID = my_Model_tab_Order_ShopingCart.ShopClientID;
                        Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalHowMuchWealth;
                        Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "清空购物车" + strDesc;
                        int intTableID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                        #region 增加财富积分未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "清空购物车" + strDesc;
                        Model_b011_InfoAlertMessage.CreateBy = "清空购物车";
                        Model_b011_InfoAlertMessage.UpdateBy = "清空购物车";
                        Model_b011_InfoAlertMessage.UserID = my_Model_tab_Order_ShopingCart.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = my_Model_tab_Order_ShopingCart.ShopClientID;
                        Model_b011_InfoAlertMessage.Type = "Info_TotalWealth";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加财富积分未处理信息  


                        EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                        EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and OrderDetailID=@OrderDetailID and ShopClient_ID=@ShopClient_ID", my_Model_tab_Order_ShopingCart.UserID, OrdetailID, my_Model_tab_Order_ShopingCart.ShopClientID);
                        if (Model_b008_OpterationUserActiveReturnMoneyOrderNum != null)
                        {
                            if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32() == 0)
                            {
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum.toInt32();
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = 0;
                            }
                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() + DecimalHowMuchWealth;
                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                            Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy;
                            my_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);
                        }

                        EggsoftWX.Model.b015_OrderDetail_WealthBuy Model_b015_OrderDetail_WealthBuy = BLL_b015_OrderDetail_WealthBuy.GetModel(Int32b015_OrderDetail_WealthBuy);
                        Model_b015_OrderDetail_WealthBuy.UseOrNotuse = false;
                        Model_b015_OrderDetail_WealthBuy.CreateBy = "清空购物车";
                        BLL_b015_OrderDetail_WealthBuy.Add(Model_b015_OrderDetail_WealthBuy);
                    }



                }
                #endregion---归还财富积分


                #region---归还微店金额

                if (Decimal.Round(Convert.ToDecimal(my_Model_tab_Order_ShopingCart.MoneyWeBuy8Credits), 2) > 0)
                {
                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Convert.ToDecimal(my_Model_tab_Order_ShopingCart.MoneyWeBuy8Credits);
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "清空购物车" + GoodP.Get_GoodNameFromGoodID(Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodID));
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order_ShopingCart.UserID);
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order_ShopingCart.ShopClientID;
                    int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                    #region 增加购物券未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = "清空购物车";
                    Model_b011_InfoAlertMessage.UpdateBy = "清空购物车";
                    Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加购物券未处理信息  
                }

                #endregion

                #region---归还微店豆

                if (my_Model_tab_Order_ShopingCart.Beans > 0)
                {
                    EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge BLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge Model_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                    Model_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Convert.ToInt32(my_Model_tab_Order_ShopingCart.Beans);
                    Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "清空购物车" + GoodP.Get_GoodNameFromGoodID(Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodID));
                    Model_tab_TotalBeans_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order_ShopingCart.UserID);
                    Model_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order_ShopingCart.ShopClientID;
                    BLL_tab_TotalBeans_Consume_Or_Recharge.Add(Model_tab_TotalBeans_Consume_Or_Recharge);
                }

                #endregion

                #region---归还购物券
                //---归还购物券
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                string strVouchersNum_List = my_Model_tab_Order_ShopingCart.VouchersNum_List;
                if (String.IsNullOrEmpty(strVouchersNum_List) == false)
                {
                    string[] strEachList = strVouchersNum_List.Split(',');
                    for (int k = 0; k < strEachList.Length; k++)
                    {
                        if (String.IsNullOrEmpty(strEachList[k]) == false)
                        {
                            string[] strEachListString = strEachList[k].Split('#');
                            String strVouchersNum = strEachListString[0];
                            Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + strVouchersNum + "'");
                            if (Model_tab_Shopping_Vouchers != null)
                            {
                                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                                EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID.toInt32());
                                if (Model_tab_Orderdetails == null)//说明不是在订单表中，可以清空购物车  已经进入订单列表了  。清空购物车就是无效操作了
                                {
                                    Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = 0;
                                    Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                                    BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                                }
                            }
                        }
                    }

                }
                #endregion
            }
        }

        public static void getGoodPrice(int intGoodType, int intGoodTypeId, int intbuyCount, string strGoodTypeIdBuyInfo, out string strReturnGoodPrice, out Decimal dec_Good_Money)
        {
            strReturnGoodPrice = "";
            dec_Good_Money = 0;
            if (intGoodType == 3)
            { ///微众筹
                EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                if (Model_tab_ZC_01Product_Support != null)
                {
                    strReturnGoodPrice = Model_tab_ZC_01Product_Support.SalesPrice.ToString();
                    dec_Good_Money = intbuyCount * Convert.ToDecimal(Model_tab_ZC_01Product_Support.SalesPrice);
                }
            }
            else if (intGoodType == 2)
            { ///微团购
                EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(intGoodTypeId);
                if (Model_tab_TuanGou != null)
                {
                    strReturnGoodPrice = Model_tab_TuanGou.EachPeoplePrice.ToString();
                    dec_Good_Money = intbuyCount * Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice);
                }
            }
            else if (intGoodType == 1)
            { //微砍价用户

                EggsoftWX.BLL.tab_WeiKanJia_Master BLL_tab_WeiKanJia_Master = new EggsoftWX.BLL.tab_WeiKanJia_Master();
                EggsoftWX.Model.tab_WeiKanJia_Master Model_tab_WeiKanJia_Master = BLL_tab_WeiKanJia_Master.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                if (Model_tab_WeiKanJia_Master != null)
                {
                    strReturnGoodPrice = Model_tab_WeiKanJia_Master.NowPrice.ToString();
                    dec_Good_Money = intbuyCount * Convert.ToDecimal(Model_tab_WeiKanJia_Master.NowPrice);
                }
                else
                {
                    EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                    EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel(intGoodTypeId);

                    if (Model_tab_WeiKanJia != null)
                    {
                        strReturnGoodPrice = Model_tab_WeiKanJia.StartPrice.ToString();
                        dec_Good_Money = intbuyCount * Convert.ToDecimal(Model_tab_WeiKanJia.StartPrice);

                    }
                }
            }
        }

        /// <summary>
        /// 用户需要支付的现金 包含已有现金   计算代理所得的钱  从订单详细表中
        /// </summary>
        /// <param name="OrderdetailsID"></param>
        /// <returns></returns>
        public static Decimal CountCur_Will_Pay_PriceFromtab_OrderdetailsID(Int32 OrderdetailsID)
        {
            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(OrderdetailsID);
            Decimal DecimalMoney = Convert.ToDecimal(Model_tab_Orderdetails.GoodPrice) * Convert.ToInt32(Model_tab_Orderdetails.OrderCount) - Convert.ToDecimal(Model_tab_Orderdetails.MoneyWeBuy8Credits) - Convert.ToDecimal(Model_tab_Orderdetails.WealthMoney);

            Decimal allVouchersMoneyMoney = 0;
            if (String.IsNullOrEmpty(Model_tab_Orderdetails.VouchersNum_List) == false)
            {
                string[] strEachList = Model_tab_Orderdetails.VouchersNum_List.Split(',');
                for (int k = 0; k < strEachList.Length; k++)
                {
                    if (String.IsNullOrEmpty(strEachList[k]) == false)
                    {
                        string[] strEachListString = strEachList[k].Split('#');
                        String strVouchersMoney = strEachListString[1];
                        allVouchersMoneyMoney += Decimal.Parse(strVouchersMoney);
                    }
                }
            }
            DecimalMoney -= allVouchersMoneyMoney;
            return DecimalMoney;
        }

        //
        /// <summary>
        ///   ///本详细订单 用户需要支付的现金（已扣除购物券+现金） 
        /// </summary>
        /// <param name="intUserID"></param>
        /// <param name="strGoodID"></param>
        /// <param name="str_buyCount"></param>
        /// <param name="strMultiBuyType"></param>
        /// <param name="strMoneyCredits"></param>
        /// <param name="strVouchersNum_List"></param>
        /// <param name="strMoneyWeBuy8Credits"></param>
        /// <param name="strMoneyWealth"></param>
        /// <param name="strBeans"></param>
        /// <param name="strReturnGoodPrice"></param>
        /// <param name="outDecimal_My_Freight"></param>
        /// <param name="strShowYunFei"></param>
        /// <param name="boolmyProductIDListFirstDoFreight"></param>
        /// <param name="strMultiBuyTypeName"></param>
        /// <param name="boolCartSameGood">bool boolCartSameGood = false;///处理购物车 同种商品 满足免运费的条件   购车车 才处理 不是购物车不处理 值是false</param>
        /// <returns></returns>
        public static Decimal CountCur_Will_Pay_Price(int intUserID, string strGoodID, string str_buyCount, string strMultiBuyType, string strMoneyCredits, string strVouchersNum_List, string strMoneyWeBuy8Credits, String strMoneyWealth, String strBeans, out String strReturnGoodPrice, out Decimal outDecimal_My_Freight, out string strShowYunFei, bool boolmyProductIDListFirstDoFreight, out string strMultiBuyTypeName, bool boolCartSameGood, out AllcartYunFeiList myAllcartYunFeiList, int intGoodType, int intGoodTypeId, string strGoodTypeIdBuyInfo)
        {
            //int intAgentMyStocknum = 0;
            #region 处理 价格问题  代理商 价格 可能不一样
            Decimal Decimal__Agent_ProductPrice = 0;
            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + intUserID + " and ProductID=" + strGoodID + " and Empowered=1");
            if (Model_tab_ShopClient_Agent__ProductClassID != null)
            {
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intUserID + "   and IsDeleted=0 and Empowered=1");

                if (Model_tab_ShopClient_Agent_ != null)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32() + " and ProductID=" + strGoodID);
                    if (Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID != null)
                    {
                        Decimal__Agent_ProductPrice = Model_tab_ShopClient_Agent_Level_ProductInfo_ParentID.ProductPrice.toDecimal();
                    }
                }
            }
            int intbuyCount = Int32.Parse(str_buyCount);///this  willpay

            if (strBeans == "0") strBeans = "";//这里清空 有利于后面检查
            if (strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
            if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
            if (strMoneyWealth == "0.00") strMoneyWealth = "";//这里清空 有利于后面检查

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

            my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Convert.ToInt32(strGoodID));

            bool boolIFSecondBuy = false;
            EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = Eggsoft_Public_CL.GoodP.GetSecondBuyInfoPrice(Int32.Parse(strGoodID), out boolIFSecondBuy);
            string strGoodPrice = "";
            strReturnGoodPrice = "";
            strMultiBuyTypeName = "";
            Decimal dec_Good_Money = 0;
            Decimal dec_Good_kg = 0;

            if ((boolIFSecondBuy) && (Model_View_SecondSalesGoodList != null) && intGoodType == 0)//秒杀 并在期限内
            {
                dec_Good_kg = Model_View_SecondSalesGoodList.kg * intbuyCount;
                strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Model_View_SecondSalesGoodList.LimitTimerBuy_TimePrice);
                dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                strReturnGoodPrice = strGoodPrice;
                if ((strMultiBuyType != "0") && (strMultiBuyType != ""))//multi price
                {
                    EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
                    EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = BLL_tab_Goods_MultiSelectTypePrice.GetModel(Int32.Parse(strMultiBuyType));

                    strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_Goods_MultiSelectTypePrice.GoodPrice);
                    strMultiBuyTypeName = Model_tab_Goods_MultiSelectTypePrice.GoodMultiName;
                    //strGoodName = strGoodName + Model_tab_Goods_MultiSelectTypePrice.GoodMultiName;
                    dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;

                    strReturnGoodPrice = strGoodPrice;
                }
            }
            else
            {
                if ((strMultiBuyType != "0") && (strMultiBuyType != ""))//multi price
                {
                    EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
                    EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = BLL_tab_Goods_MultiSelectTypePrice.GetModel(Int32.Parse(strMultiBuyType));
                    if (Model_tab_Goods_MultiSelectTypePrice != null) strMultiBuyTypeName = Model_tab_Goods_MultiSelectTypePrice.GoodMultiName;
                    if (Decimal__Agent_ProductPrice > 0)///存在代理商价格
                    {
                        ///看看 剩下的购物券是否够用
                        ///
                        //Decimal goodTotalCreditsMoney_Vouchers = 0;
                        //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(intUserID, out goodTotalCreditsMoney_Vouchers);  //
                        //if (goodTotalCreditsMoney_Vouchers < Decimal__Agent_ProductPrice * intbuyCount)///至少得有买一个的钱 否则 价格升高
                        //{
                        //    strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        //    dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        //    dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        //    strReturnGoodPrice = strGoodPrice;
                        //}
                        //else
                        //{
                        strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Decimal__Agent_ProductPrice);
                        dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        strReturnGoodPrice = strGoodPrice;
                        //}
                    }
                    else if (Model_tab_Goods_MultiSelectTypePrice != null)//该分分类可能已被删除
                    {
                        strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_Goods_MultiSelectTypePrice.GoodPrice);

                        //strGoodName = strGoodName + Model_tab_Goods_MultiSelectTypePrice.GoodMultiName;
                        dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        strReturnGoodPrice = strGoodPrice;
                    }
                    else
                    {

                        strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        strReturnGoodPrice = strGoodPrice;

                    }
                }
                else
                {
                    if (Decimal__Agent_ProductPrice > 0)///存在代理商价格
                    {
                        ///看看 剩下的购物券是否够用
                        ///
                        //Decimal goodTotalCreditsMoney_Vouchers = 0;
                        //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(intUserID, out goodTotalCreditsMoney_Vouchers);
                        //
                        //if (goodTotalCreditsMoney_Vouchers < Decimal__Agent_ProductPrice * intbuyCount)///至少得有买一个的钱 否则 价格升高
                        //{
                        //    strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        //    dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        //    dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        //    strReturnGoodPrice = strGoodPrice;
                        //}
                        //else
                        //{
                        strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Decimal__Agent_ProductPrice);
                        dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        strReturnGoodPrice = strGoodPrice;
                        //}
                    }
                    else
                    {
                        strGoodPrice = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Goods.PromotePrice));
                        dec_Good_Money = Decimal.Parse(strGoodPrice) * intbuyCount;
                        dec_Good_kg = Convert.ToDecimal(my_Model_tab_Goods.kg) * intbuyCount;
                        strReturnGoodPrice = strGoodPrice;
                    }
                }
            }
            #endregion
            ///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
            if ((intGoodType == 1) || (intGoodType == 2) || (intGoodType == 3))
            {

                getGoodPrice(intGoodType, intGoodTypeId, intbuyCount, strGoodTypeIdBuyInfo, out strReturnGoodPrice, out dec_Good_Money);
            }



            #region //处理运费问题


            #region 处理默认运费
            outDecimal_My_Freight = 0;//处理运费问题
            string strFromToWhere = "";


            Decimal tMAXKgNoFright = 0;
            Decimal tMAXMoneyNoFright = 0;
            int tMAXIntNoFright = 0;

            Decimal iniAllGoodPrice = dec_Good_Money;///便于下面查找免运费条件
            int iniAllGoodCount = intbuyCount;///便于下面查找免运费条件
            Decimal iniAllKg = dec_Good_kg;///便于下面查找免运费条件


            string strYunFeiText = ""; strShowYunFei = "";
            int intFreightTemplate_ID = Convert.ToInt32(my_Model_tab_Goods.FreightTemplate_ID);
            if ((intFreightTemplate_ID != 0))
            {
                EggsoftWX.BLL.tab_FreightTemplate BLL_tab_FreightTemplate = new EggsoftWX.BLL.tab_FreightTemplate();
                EggsoftWX.Model.tab_FreightTemplate Model_tab_FreightTemplate = BLL_tab_FreightTemplate.GetModel(intFreightTemplate_ID);
                Decimal Decimal_My_Freight = 0;
                if (Model_tab_FreightTemplate != null)
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                    string strToSheng = Model_tab_User.Sheng;

                    #region 当前用户地区  "从" + strShopProvince + "发货" + "送至" + strToSheng

                    string strShopProvince = Model_tab_FreightTemplate.Province;

                    if (String.IsNullOrEmpty(strShopProvince))
                    {
                        if (String.IsNullOrEmpty(strToSheng))
                        {
                            strFromToWhere = "";
                        }
                        else
                        {
                            strFromToWhere = "送至" + strToSheng;
                        }
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
                        EggsoftWX.Model.tab_PE_Region Model_tab_PE_Region = BLL_tab_PE_Region.GetModel(Int32.Parse(strShopProvince));

                        if (String.IsNullOrEmpty(strToSheng))
                        {
                            strFromToWhere = "" + Eggsoft.Common.StringNum.MaxLengthString(Model_tab_PE_Region.Province, 4) + "发货";
                        }
                        else
                        {
                            strFromToWhere = "" + Eggsoft.Common.StringNum.MaxLengthString(Model_tab_PE_Region.Province, 4) + "发货" + "至" + Eggsoft.Common.StringNum.MaxLengthString(strToSheng, 4);
                        }
                    }
                    #endregion

                    #region 检查区域运费



                    #region 检查运费模板是否设置了定制地区的运费
                    #region 取静态化字符串 有利于前台调用 增强性能    //反序列化...........
                    Eggsoft.Common.ArrayListHelper mmmArrayListHelper = new Eggsoft.Common.ArrayListHelper();
                    string strXML_Sheng_ID_NamePubList = Eggsoft_Public_CL.XML_Sheng_ID_Name.strXML_Sheng_ID_NamePub;
                    Type[] extra2 = new Type[1];
                    extra2[0] = typeof(Eggsoft_Public_CL.XML_Sheng_ID_Name);
                    ArrayList laArrayListHelper = mmmArrayListHelper.DeserializeArrayList(strXML_Sheng_ID_NamePubList, typeof(ArrayList), extra2);
                    #endregion  取静态化字符串 有利于前台调用 增强性能
                    int intShengID = 0;
                    for (int kk = 0; kk < laArrayListHelper.Count; kk++)
                    {
                        Eggsoft_Public_CL.XML_Sheng_ID_Name myXML_Sheng_ID_Name = (Eggsoft_Public_CL.XML_Sheng_ID_Name)laArrayListHelper[kk];
                        if (myXML_Sheng_ID_Name.ShengName.Contains(strToSheng) || strToSheng.Contains(myXML_Sheng_ID_Name.ShengName))
                        {
                            intShengID = myXML_Sheng_ID_Name.ShengID;
                            break;
                        }
                    }
                    string strtab_FreightTemplate_AreaID = "";


                    #region 处理购物车 同种商品 满足免运费的条件  bool boolCartSameGood = false;///   购车车 才处理 不是购物车不处理 值是false

                    if (boolCartSameGood)
                    {

                        string strboolCartSameGood = "";
                        strboolCartSameGood += "SELECT  SUM(View_19999.GoodPrice_Multi";
                        strboolCartSameGood += "   * View_19999.GoodIDCount) AS allGoodPrice_Multi ,";
                        strboolCartSameGood += " SUM(View_19999.GoodIDCount) AS AllGoodIDCount ,";
                        strboolCartSameGood += " GoodID";
                        strboolCartSameGood += " FROM    ( SELECT DISTINCT TOP ( 100 ) PERCENT";
                        strboolCartSameGood += "                 ISNULL(tab_Goods_MultiSelectTypePrice.GoodPrice,";
                        strboolCartSameGood += "                       tab_Goods.PromotePrice) AS GoodPrice_Multi ,";
                        strboolCartSameGood += "                tab_Order_ShopingCart.UserID ,";
                        strboolCartSameGood += "                tab_Order_ShopingCart.UpdateTime ,";
                        strboolCartSameGood += "                tab_Order_ShopingCart.GoodID ,";
                        strboolCartSameGood += "               tab_Order_ShopingCart.GoodIDCount";
                        strboolCartSameGood += "      FROM      tab_Order_ShopingCart ";
                        strboolCartSameGood += "               LEFT OUTER JOIN tab_Goods ON tab_Order_ShopingCart.GoodID = tab_Goods.ID";
                        strboolCartSameGood += "               LEFT OUTER JOIN tab_Goods_MultiSelectTypePrice ON tab_Order_ShopingCart.GoodID = tab_Goods_MultiSelectTypePrice.GoodID";
                        strboolCartSameGood += "                                                         AND tab_Order_ShopingCart.MultiBuyType = tab_Goods_MultiSelectTypePrice.ID";
                        strboolCartSameGood += "   where tab_Order_ShopingCart.IsDeleted<>1 ) AS View_19999";
                        strboolCartSameGood += " WHERE   View_19999.userid = " + intUserID + " and  View_19999.GoodID=" + strGoodID;
                        strboolCartSameGood += " GROUP BY View_19999.GoodID ";

                        System.Data.DataTable myDataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataTable(strboolCartSameGood);
                        if (myDataTable.Rows.Count > 0)
                        {
                            iniAllGoodPrice = Decimal.Parse(myDataTable.Rows[0]["allGoodPrice_Multi"].ToString());
                            iniAllGoodCount = Int32.Parse(myDataTable.Rows[0]["AllGoodIDCount"].ToString());
                            iniAllKg = iniAllGoodCount * Convert.ToDecimal(my_Model_tab_Goods.kg);
                        }
                    }
                    /****** Script for SelectTopNRows command from SSMS  ******/
                    #endregion

                    if (intShengID > 0)
                    {
                        EggsoftWX.BLL.tab_FreightTemplate_Area BLL_tab_FreightTemplate_Area = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                        System.Data.DataTable Data_DataTable = BLL_tab_FreightTemplate_Area.GetList("FreightTemplate_ID=" + intFreightTemplate_ID).Tables[0];
                        for (int pp = 0; pp < Data_DataTable.Rows.Count; pp++)
                        {
                            string strAreaList = Data_DataTable.Rows[pp]["AreaList"].ToString();
                            string strTemp_tab_FreightTemplate_AreaID = Data_DataTable.Rows[pp]["ID"].ToString();
                            if (String.IsNullOrEmpty(strAreaList) == false)
                            {
                                string[] strShengIDList = strAreaList.Split(',');
                                for (int mm = 0; mm < strShengIDList.Length; mm++)
                                {
                                    if (strShengIDList[mm] == intShengID.ToString())
                                    {
                                        strtab_FreightTemplate_AreaID = strTemp_tab_FreightTemplate_AreaID;
                                        pp = 9999;///上一个循环也结束
                                        break;///找到了 跳出去
                                    }
                                }
                            }
                        }
                        ////找到了区域运费情况 。这时就不用默认的啦

                        if (string.IsNullOrEmpty(strtab_FreightTemplate_AreaID) == false)
                        {
                            #region 找到了区域运费情况 。这时就不用默认的啦
                            EggsoftWX.Model.tab_FreightTemplate_Area Model_tab_FreightTemplate_Area = BLL_tab_FreightTemplate_Area.GetModel(Int32.Parse(strtab_FreightTemplate_AreaID));
                            if (boolmyProductIDListFirstDoFreight)///计算首件商品的                                                         
                            {
                                Decimal_My_Freight = Model_tab_FreightTemplate_Area.Freight + Model_tab_FreightTemplate_Area.FreightMore * (intbuyCount - 1);
                            }
                            else
                            {
                                Decimal_My_Freight = Model_tab_FreightTemplate_Area.FreightMore * (intbuyCount);///购物车列表中 已经 表示是首件 运费 这里 只是不同的颜色表示
                            }
                            ///全局比较运费用
                            tMAXKgNoFright = Model_tab_FreightTemplate_Area.HowkgNoFreight;
                            tMAXMoneyNoFright = Model_tab_FreightTemplate_Area.HowmuchNoFreight;
                            tMAXIntNoFright = Model_tab_FreightTemplate_Area.HowmanysNoFreight;



                            Decimal argDecimal_My_Freight = 0; string strargYunFeiText = "";
                            getYunFeiText_YunFeiText(Model_tab_FreightTemplate_Area.HowkgNoFreight, Model_tab_FreightTemplate_Area.HowmuchNoFreight, Model_tab_FreightTemplate_Area.HowmanysNoFreight, iniAllKg, iniAllGoodPrice, iniAllGoodCount, out argDecimal_My_Freight, out strargYunFeiText);
                            if (argDecimal_My_Freight > 99999999)///说明 没有满足免运费条件
                            {
                                argDecimal_My_Freight = Decimal_My_Freight;///没有满足包邮条件  就取实际的值
                            }
                            else if (argDecimal_My_Freight == 0)
                            {
                                argDecimal_My_Freight = 0;
                            }

                            if (argDecimal_My_Freight == 0) Decimal_My_Freight = 0;
                            if (String.IsNullOrEmpty(strargYunFeiText) == false) strYunFeiText = strargYunFeiText;
                            outDecimal_My_Freight = argDecimal_My_Freight;//页面上输出运费
                            dec_Good_Money += argDecimal_My_Freight;
                            #endregion
                        }
                        #endregion

                    }
                    #endregion
                    #region 默认地区 没有找到区域运费
                    if (string.IsNullOrEmpty(strtab_FreightTemplate_AreaID) == true)////确认没有区域运费
                    {
                        if (boolmyProductIDListFirstDoFreight)///计算首件商品的                                                         
                        {
                            Decimal_My_Freight = Model_tab_FreightTemplate.Freight + Model_tab_FreightTemplate.FreightMore * (intbuyCount - 1);
                        }
                        else
                        {
                            Decimal_My_Freight = Model_tab_FreightTemplate.FreightMore * (intbuyCount);///购物车列表中 已经 表示是首件 运费 这里 只是不同的颜色表示
                        }
                        ///免费政策。如果设置大于0  表示使用该项设置，不使用 不填写任何值。
                        ///如果填写 那就是必要条件。三者都填写，三者都达到才免费。
                        /// 重量   钱  件数  
                        /// 

                        ///全局比较运费用
                        tMAXKgNoFright = Model_tab_FreightTemplate.HowkgNoFreight;
                        tMAXMoneyNoFright = Model_tab_FreightTemplate.HowmuchNoFreight;
                        tMAXIntNoFright = Model_tab_FreightTemplate.HowmanysNoFreight;

                        Decimal argDecimal_My_Freight = 0; string strargYunFeiText = "";
                        getYunFeiText_YunFeiText(Model_tab_FreightTemplate.HowkgNoFreight, Model_tab_FreightTemplate.HowmuchNoFreight, Model_tab_FreightTemplate.HowmanysNoFreight, iniAllKg, iniAllGoodPrice, iniAllGoodCount, out argDecimal_My_Freight, out strargYunFeiText);

                        if (argDecimal_My_Freight > 99999999)///说明 没有满足免运费条件
                        {
                            argDecimal_My_Freight = Decimal_My_Freight;///没有满足包邮条件  就取实际的值
                        }
                        else if (argDecimal_My_Freight == 0)
                        {
                            argDecimal_My_Freight = 0;
                        }

                        if (String.IsNullOrEmpty(strargYunFeiText) == false) strYunFeiText = strargYunFeiText;

                        outDecimal_My_Freight = argDecimal_My_Freight;//页面上输出运费
                        dec_Good_Money += argDecimal_My_Freight;
                    }
                    #endregion
                }
                strShowYunFei = strFromToWhere + ",运费:¥" + Eggsoft_Public_CL.Pub.getBankPubMoney(outDecimal_My_Freight) + "" + strYunFeiText;
            }


            #endregion



            #endregion

            //---计算购物券
            Decimal allMoneyMoney = 0;
            Decimal allVouchersMoneyMoney = 0;
            Decimal allMoneyWealth = 0;//财富积分

            if (String.IsNullOrEmpty(strMoneyCredits) == false)
            {
                allMoneyMoney = Decimal.Parse(strMoneyCredits);
            }
            if (String.IsNullOrEmpty(strMoneyWealth) == false)
            {
                allMoneyWealth = Decimal.Parse(strMoneyWealth);
            }


            if (String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
            {
                allVouchersMoneyMoney = Decimal.Parse(strMoneyWeBuy8Credits);
            }
            else if (String.IsNullOrEmpty(strBeans) == false)
            {
                allVouchersMoneyMoney = Decimal.Multiply(Decimal.Parse(strBeans), (Decimal)0.01);
            }
            else if (String.IsNullOrEmpty(strVouchersNum_List) == false)
            {
                string[] strEachList = strVouchersNum_List.Split(',');
                for (int k = 0; k < strEachList.Length; k++)
                {
                    if (String.IsNullOrEmpty(strEachList[k]) == false)
                    {
                        string[] strEachListString = strEachList[k].Split('#');
                        String strVouchersMoney = strEachListString[1];
                        allVouchersMoneyMoney += Decimal.Parse(strVouchersMoney);
                    }
                }
            }
            dec_Good_Money = dec_Good_Money - allVouchersMoneyMoney - allMoneyMoney - allMoneyWealth;

            myAllcartYunFeiList = new AllcartYunFeiList();
            myAllcartYunFeiList.stryunfeiText = strYunFeiText;
            myAllcartYunFeiList.intGoodID = Convert.ToInt32(strGoodID);
            myAllcartYunFeiList.GoodCount = intbuyCount;
            myAllcartYunFeiList.DecimalAllkg = dec_Good_kg;
            myAllcartYunFeiList.DecimalGoodPrice = Decimal.Parse(strReturnGoodPrice);
            myAllcartYunFeiList.DecimalAllGoodPrice = dec_Good_Money;
            myAllcartYunFeiList.DecimalAllFright = outDecimal_My_Freight;
            myAllcartYunFeiList.FreightTempletID = intFreightTemplate_ID;
            myAllcartYunFeiList.MAXKgNoFright = tMAXKgNoFright;
            myAllcartYunFeiList.MAXMoneyNoFright = tMAXMoneyNoFright;
            myAllcartYunFeiList.MAXIntNoFright = tMAXIntNoFright;
            return dec_Good_Money;

        }


        public static void getYunFeiText_YunFeiText(Decimal HowkgNoFreight, Decimal HowmuchNoFreight, int HowmanysNoFreight, Decimal iniAllKg, Decimal iniAllGoodPrice, int iniAllGoodCount, out Decimal Decimal_My_Freight, out string strYunFeiText)
        {
            strYunFeiText = "";
            Decimal_My_Freight = 999999999;//
            if ((HowkgNoFreight > 0) && (HowmuchNoFreight > 0) && (HowmanysNoFreight > 0))///启用免费政策
            {
                if ((HowmanysNoFreight <= iniAllGoodCount) && (HowmuchNoFreight <= iniAllGoodPrice) && (HowkgNoFreight <= iniAllKg))
                {
                    Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",已满" + strkgShow;
                    strYunFeiText += ",已满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元";
                    strYunFeiText += ",已满" + HowmanysNoFreight + "件";
                    strYunFeiText += "免运费";
                }
                else if ((HowkgNoFreight > iniAllKg))
                {
                    //    Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",满" + strkgShow;
                    strYunFeiText += "免运费";
                }
                else if ((HowmuchNoFreight > iniAllGoodPrice))
                {
                    //  Decimal_My_Freight = 0;//
                    strYunFeiText += ",满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元免运费";
                }
                else if ((HowmanysNoFreight > iniAllGoodCount))
                {
                    //Decimal_My_Freight = 0;//
                    strYunFeiText += ",满" + HowmanysNoFreight + "件免运费";
                }
            }
            else if ((HowkgNoFreight > 0) && (HowmuchNoFreight > 0))///启用免费政策   重量   钱   
            {
                if ((HowkgNoFreight <= iniAllKg) && (HowmuchNoFreight <= iniAllGoodPrice))
                {
                    Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",已满" + strkgShow;
                    strYunFeiText += ",已满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元";
                    strYunFeiText += "免运费";
                }
                else if ((HowkgNoFreight > iniAllKg))
                {
                    //Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",满" + strkgShow;
                    strYunFeiText += "免运费";
                }
                else if ((HowmuchNoFreight > iniAllGoodPrice))
                {
                    Decimal_My_Freight = 0;//
                    strYunFeiText += ",已满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元免运费";
                }
            }
            else if ((HowkgNoFreight > 0) && (HowmanysNoFreight > 0))///启用免费政策   重量     件数 
            {
                if ((HowkgNoFreight <= iniAllKg) && (HowmanysNoFreight <= iniAllGoodCount))
                {
                    Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",已满" + strkgShow;
                    strYunFeiText += ",已满" + HowmanysNoFreight + "件";

                    strYunFeiText += "免运费";
                }
                else if ((HowkgNoFreight > iniAllKg))
                {
                    //Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",满" + strkgShow;
                    strYunFeiText += "运费";
                }
                else if ((HowmanysNoFreight > iniAllGoodCount))
                {
                    //Decimal_My_Freight = 0;//
                    strYunFeiText += ",满" + HowmanysNoFreight + "件免运费";
                }
            }
            else if (HowkgNoFreight > 0)
            {
                if ((HowkgNoFreight <= iniAllKg))
                {
                    Decimal_My_Freight = 0;//
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",已满" + strkgShow;
                    strYunFeiText += "免运费";
                }
                else if ((HowkgNoFreight > iniAllKg))
                {
                    string strkgShow = HowkgNoFreight > 1 ? Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight)) + "公斤" : Eggsoft_Public_CL.Pub.getBankPubMoney((HowkgNoFreight * 1000)) + "克";
                    strYunFeiText += ",满" + strkgShow;
                    strYunFeiText += "免运费";
                }
            }
            else if (HowmuchNoFreight > 0)
            {
                if ((HowmuchNoFreight <= iniAllGoodPrice))
                {
                    Decimal_My_Freight = 0;//
                    strYunFeiText += ",已满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元";
                    strYunFeiText += "免运费";
                }
                else if ((HowmuchNoFreight > iniAllGoodPrice))
                {
                    strYunFeiText += ",满" + Eggsoft_Public_CL.Pub.getBankPubMoney(HowmuchNoFreight) + "元免运费";
                }
            }
            else if ((HowmanysNoFreight > 0))///启用免费政策   重量     件数 
            {
                if ((HowmanysNoFreight <= iniAllGoodCount))
                {
                    Decimal_My_Freight = 0;//
                    strYunFeiText += ",已满" + HowmanysNoFreight + "件";
                    strYunFeiText += "免运费";
                }
                else if ((HowmanysNoFreight > iniAllGoodCount))
                {
                    strYunFeiText += ",满" + HowmanysNoFreight + "件免运费";
                }
            }


        }


        /// <summary>
        /// 取当前商品包邮条件
        /// </summary>
        /// <param name="myDecimal"></param>
        /// <param name="myCount"></param>
        public static void getMaxJianshuAndMaxMoney(int intFreightTemplate_ID, int pub_Int_Session_CurUserID, out int intHowmanysNoFreight, out Decimal HowmuchNoFreight)
        {
            intHowmanysNoFreight = 0;///默认赋值 0表示不启用  
            HowmuchNoFreight = 0;//默认赋值 0表示不启用


            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
            string strToSheng = Model_tab_User.Sheng;


            #region 检查区域运费
            #region 检查运费模板是否设置了定制地区的运费
            #region 取静态化字符串 有利于前台调用 增强性能    //反序列化...........
            Eggsoft.Common.ArrayListHelper mmmArrayListHelper = new Eggsoft.Common.ArrayListHelper();
            string strXML_Sheng_ID_NamePubList = Eggsoft_Public_CL.XML_Sheng_ID_Name.strXML_Sheng_ID_NamePub;
            Type[] extra2 = new Type[1];
            extra2[0] = typeof(Eggsoft_Public_CL.XML_Sheng_ID_Name);
            ArrayList laArrayListHelper = mmmArrayListHelper.DeserializeArrayList(strXML_Sheng_ID_NamePubList, typeof(ArrayList), extra2);
            #endregion  取静态化字符串 有利于前台调用 增强性能
            int intShengID = 0;
            for (int kk = 0; kk < laArrayListHelper.Count; kk++)
            {
                Eggsoft_Public_CL.XML_Sheng_ID_Name myXML_Sheng_ID_Name = (Eggsoft_Public_CL.XML_Sheng_ID_Name)laArrayListHelper[kk];
                if (myXML_Sheng_ID_Name.ShengName.Contains(strToSheng) || strToSheng.Contains(myXML_Sheng_ID_Name.ShengName))
                {
                    intShengID = myXML_Sheng_ID_Name.ShengID;
                    break;
                }
            }
            string strtab_FreightTemplate_AreaID = "";

            if (intShengID > 0)
            {
                EggsoftWX.BLL.tab_FreightTemplate_Area BLL_tab_FreightTemplate_Area = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                System.Data.DataTable Data_DataTable = BLL_tab_FreightTemplate_Area.GetList("FreightTemplate_ID=" + intFreightTemplate_ID).Tables[0];
                for (int pp = 0; pp < Data_DataTable.Rows.Count; pp++)
                {
                    string strAreaList = Data_DataTable.Rows[pp]["AreaList"].ToString();
                    string strTemp_tab_FreightTemplate_AreaID = Data_DataTable.Rows[pp]["ID"].ToString();
                    if (String.IsNullOrEmpty(strAreaList) == false)
                    {
                        string[] strShengIDList = strAreaList.Split(',');
                        for (int mm = 0; mm < strShengIDList.Length; mm++)
                        {
                            if (strShengIDList[mm] == intShengID.ToString())
                            {
                                strtab_FreightTemplate_AreaID = strTemp_tab_FreightTemplate_AreaID;
                                pp = 9999;///上一个循环也结束
                                break;///找到了 跳出去
                            }
                        }
                    }
                }
                ////找到了区域运费情况 。这时就不用默认的啦

                if (string.IsNullOrEmpty(strtab_FreightTemplate_AreaID) == false)
                {
                    #region 找到了区域运费情况 。这时就不用默认的啦
                    EggsoftWX.Model.tab_FreightTemplate_Area Model_tab_FreightTemplate_Area = BLL_tab_FreightTemplate_Area.GetModel(Int32.Parse(strtab_FreightTemplate_AreaID));


                    //if ((Model_tab_FreightTemplate_Area.HowmanysNoFreight > 0) || (Model_tab_FreightTemplate_Area.HowmuchNoFreight > 0))///启用免费政策
                    //{
                    intHowmanysNoFreight = Model_tab_FreightTemplate_Area.HowmanysNoFreight;
                    HowmuchNoFreight = Model_tab_FreightTemplate_Area.HowmuchNoFreight;
                    //}

                    #endregion
                }
                #endregion

            }
            #endregion
            #region 默认地区
            if (string.IsNullOrEmpty(strtab_FreightTemplate_AreaID) == true)////确认没有区域运费
            {
                EggsoftWX.BLL.tab_FreightTemplate BLL_tab_FreightTemplate = new EggsoftWX.BLL.tab_FreightTemplate();
                EggsoftWX.Model.tab_FreightTemplate Model_tab_FreightTemplate = BLL_tab_FreightTemplate.GetModel(intFreightTemplate_ID);

                intHowmanysNoFreight = Model_tab_FreightTemplate.HowmanysNoFreight;
                HowmuchNoFreight = Model_tab_FreightTemplate.HowmuchNoFreight;
            }
            #endregion

        }

        public static void GetShoppingCart_PriceCount(out Decimal myDecimal, out int myCount)
        {
            myDecimal = 0;
            myCount = 0;
            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

            DataTable dt = GetShoppingCart();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int intCur = Int32.Parse(dt.Rows[i]["Count"].ToString());
                int intProductId = Int32.Parse(dt.Rows[i]["ProductId"].ToString());
                if (intProductId < 1) continue;
                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(intProductId);
                if (my_Model_tab_Goods != null)
                {
                    Decimal DecimalCur = Convert.ToDecimal(intCur * my_Model_tab_Goods.Price);
                    myCount = myCount + intCur;
                    myDecimal = myDecimal + DecimalCur;
                }
            }
            //return doubleLiteral_AllMoney;
        }

        public static DataTable GetShoppingCart()
        {
            DataTable dt = new DataTable();
            int userID = Pub_GetOpenID_And_.getUserIDFromCookies();
            EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
            DataTable dtmy_tab_Order_ShopingCart = my_tab_Order_ShopingCart.GetList("UserID=" + userID + " and IsDeleted<>1").Tables[0];

            //Eggsoft.Common.JsUtil.ShowMsg("dtmy_tab_Order_ShopingCart.Rows.Count=" + dtmy_tab_Order_ShopingCart.Rows.Count.ToString());



            if (dtmy_tab_Order_ShopingCart.Rows.Count > 0)
            {

                DataColumn column;
                DataRow row;

                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.    
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "ProductId";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                dt.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Count";
                column.AutoIncrement = false;
                column.Caption = "Count";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "ID";
                column.AutoIncrement = false;
                column.Caption = "ID";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "UserID";
                column.AutoIncrement = false;
                column.Caption = "UserID";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "ParentID";
                column.AutoIncrement = false;
                column.Caption = "ParentID";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);
                //string newCookie = "";



                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "MultiBuyType";
                column.AutoIncrement = false;
                column.Caption = "MultiBuyType";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);
                //string newCookie = "";

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "VouchersNum_List";
                column.AutoIncrement = false;
                column.Caption = "VouchersNum_List";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                dt.Columns.Add(column);
                //string newCookie = "";

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Beans";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                dt.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "MoneyCredits";
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                dt.Columns.Add(column);





                for (int i = 0; i < dtmy_tab_Order_ShopingCart.Rows.Count; i++)
                {
                    row = dt.NewRow();
                    row["ProductId"] = dtmy_tab_Order_ShopingCart.Rows[i]["GoodID"].ToString();
                    row["Count"] = dtmy_tab_Order_ShopingCart.Rows[i]["GoodIDCount"].ToString();
                    row["id"] = dtmy_tab_Order_ShopingCart.Rows[i]["ID"].ToString();
                    row["UserID"] = dtmy_tab_Order_ShopingCart.Rows[i]["UserID"].ToString();
                    row["ParentID"] = dtmy_tab_Order_ShopingCart.Rows[i]["ParentID"].ToString();
                    row["MultiBuyType"] = dtmy_tab_Order_ShopingCart.Rows[i]["MultiBuyType"].ToString();
                    row["VouchersNum_List"] = dtmy_tab_Order_ShopingCart.Rows[i]["VouchersNum_List"].ToString();



                    dt.Rows.Add(row);                //}
                }
            }
            else
            {
            }
            return dt;

        }



    }

}