using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class GoodP_YouHuiQuan
    {
        public GoodP_YouHuiQuan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private static object myGetOneYouHuiQuan = new object();

        /// <summary>
        /// 用户领用一个线上发放的购物券。返回 ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="pub_Int_ShopClientID"></param>
        /// <param name="myVouchersSchemeInt32"></param>
        /// <param name="DecimalMoney"></param>
        /// <param name="ValidateStartTime"></param>
        /// <param name="ValidateEndTime"></param>
        public static Int32 GetOneYouHuiQuan(int UserID, int pub_Int_ShopClientID, int myVouchersSchemeInt32, Decimal DecimalMoney, DateTime ValidateStartTime, DateTime ValidateEndTime)
        {
            Int32 ThisDetailID = 0;

            try
            {
                #region 运行存储过程
                //if (Model_Shopping_VouchersScheme.HowToGet == 1)////线下发放才实际生成
                {
                    EggsoftWX.BLL.tab_Shopping_Vouchers_RunProcedure bll_tab_Shopping_Vouchers_RunProcedure = new EggsoftWX.BLL.tab_Shopping_Vouchers_RunProcedure();
                    //              @return_value = [dbo].[RunProcedure_DoShopping_Vouchers]
                    //      @ShopClientID = 1,
                    //@Scheme_ID = 11,
                    //@Money = 12,
                    //@AllNum = 11111,
                    //@DoFinshed = @DoFinshed OUTPUT

                    lock (myGetOneYouHuiQuan)
                    {
                        string[] strList = { pub_Int_ShopClientID.toString(), myVouchersSchemeInt32.toString(), DecimalMoney.toString(), "1", ValidateStartTime.ToString(), ValidateEndTime.ToString() };
                        //my1datetime.ToString();
                        bool mybool = bll_tab_Shopping_Vouchers_RunProcedure.AddAllNum(strList);

                        EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLLtab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                        EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Modeltab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                        System.Data.DataTable Data_DataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.Query("select max(id)from tab_ShopClient_Shopping_VouchersScheme_Detail where Scheme_ID =" + myVouchersSchemeInt32).Tables[0];
                        if (Data_DataTable.Rows.Count > 0)
                        {
                           

                            Modeltab_ShopClient_Shopping_VouchersScheme_Detail = BLLtab_ShopClient_Shopping_VouchersScheme_Detail.GetModel(Data_DataTable.Rows[0][0].toInt32());
                            Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID = UserID;
                            Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UpdateTime = DateTime.Now;

                            #region 是否有多少天过期的事情
                            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLLtab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                            EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme ModelVouchersScheme = BLLtab_ShopClient_Shopping_VouchersScheme.GetModel(myVouchersSchemeInt32);

                            if (ModelVouchersScheme.ValidateTypeRelativeCheck.toBoolean())
                            {
                                DateTime tMinValidateDate = DateTime.Now.AddDays(ModelVouchersScheme.ValidateDateTypeRelative.toInt32());
                                if (tMinValidateDate < ModelVouchersScheme.ValidateEndTime)
                                {
                                    Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime = tMinValidateDate;////领用后多少天过期
                                }
                            }
                            #endregion 是否有多少天过期的事情


                            BLLtab_ShopClient_Shopping_VouchersScheme_Detail.Update(Modeltab_ShopClient_Shopping_VouchersScheme_Detail);
                            ThisDetailID = Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ID;
                        }
                    }

                }
                #endregion 运行存储过程


            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "优惠券");
            }
            finally
            {

            }
            return ThisDetailID;
        }



        public static Int32 GetMyCanUseCountYouHuiQuan(int UserID, int pub_Int_ShopClientID)
        {
            EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail bll_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();



            string strSELECTWhere = @"SELECT   count(*) as CanUseCount
                  FROM      tab_ShopClient_Shopping_VouchersScheme RIGHT OUTER JOIN
                tab_ShopClient_Shopping_VouchersScheme_Detail ON 
                tab_ShopClient_Shopping_VouchersScheme.ID = tab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID";


            strSELECTWhere += " where isnull(tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID,0)=0 and tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime>getdate() ";


            strSELECTWhere += " and tab_ShopClient_Shopping_VouchersScheme_Detail.ShopClientID = " + pub_Int_ShopClientID + " and tab_ShopClient_Shopping_VouchersScheme_Detail.UserID = " + UserID;

            return bll_VouchersScheme_Detail.SelectList(strSELECTWhere).Tables[0].Rows[0][0].toInt32();
        }



        #region ShowYouHuiQuanSearchByGoodID
        public class ShowYouHuiQuanSearchBy
        {
            public int? Scheme_ID { get; set; }
            public string VouchersNum { get; set; }
            public int? GuWuCheIDOrOrderDetailID { get; set; }
            public Decimal? Money { get; set; }
            public DateTime? ValidateEndTime { get; set; }
            public int? HowToUse { get; set; }
            public Decimal? HowToUseLimitMaxMoney { get; set; }
            public string Vouchers_Title { get; set; }
        }


        /// <summary>
        /// 是否有优惠券的项目  如果有返回1   如果本人有 返回2，out 出金额和名称列表     什么都没有 返回-1；没有本商品的,也没有可以领用的，返回0   
        /// </summary>
        /// <param name="Int32GoodID"></param>
        /// <returns></returns>
        public static Int32 ShowYouHuiQuanSearchByGoodID(Int32 Int32GoodID, Int32 Int32ShopClientID, Int32 Int32UserID, out List<ShowYouHuiQuanSearchBy> ListShowYouHuiQuanSearchBy)
        {
            Int32 Int32ShowYouHuiQuanSearchByGoodID = 0;
            ListShowYouHuiQuanSearchBy = null;
            try
            {


                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLL_tab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                if (BLL_tab_ShopClient_Shopping_VouchersScheme.Exists("ValidateEndTime>getdate() and ShopClientID=" + Int32ShopClientID))
                {
                    String strSQL = " select GoodList as GoodList from [tab_ShopClient_Shopping_VouchersScheme] where [ShopClientID]=" + Int32ShopClientID + " for xml path('oneLine'); ";
                    System.Data.DataTable dddd_VouchersScheme = BLL_tab_ShopClient_Shopping_VouchersScheme.SelectList(strSQL).Tables[0];

                    if (dddd_VouchersScheme.Rows.Count > 0)
                    {
                        string strContainsGood = dddd_VouchersScheme.Rows[0][0].toString();
                        if (strContainsGood.Contains(Int32GoodID.toString()))
                        {
                            Int32ShowYouHuiQuanSearchByGoodID = 1;

                            #region 得到本商品能用的购物券列表
                            string strWhere = @"   SELECT tab_ShopClient_Shopping_VouchersScheme_Detail.ID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.VouchersNum, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ShopClientID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.UserID, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.Money, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.MoneyUsed, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.CreatTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.UpdateTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateStartTime, 
                tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime, 
                tab_ShopClient_Shopping_VouchersScheme.HowToUse, 
                tab_ShopClient_Shopping_VouchersScheme.HowToUseLimitMaxMoney,
                tab_ShopClient_Shopping_VouchersScheme.Vouchers_Title 
FROM      tab_ShopClient_Shopping_VouchersScheme_Detail LEFT OUTER JOIN
                tab_ShopClient_Shopping_VouchersScheme ON
                tab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID = tab_ShopClient_Shopping_VouchersScheme.ID
WHERE( tab_ShopClient_Shopping_VouchersScheme_Detail.ShopClientID={0} and tab_ShopClient_Shopping_VouchersScheme_Detail.UserID={1} and  isnull(tab_ShopClient_Shopping_VouchersScheme_Detail.GuWuCheIDOrOrderDetailID,0)=0 and tab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime > GETDATE() and (tab_ShopClient_Shopping_VouchersScheme.GoodList='{2}' or tab_ShopClient_Shopping_VouchersScheme.GoodList like '%{2},%' or tab_ShopClient_Shopping_VouchersScheme.GoodList like '%,{2}%')) order by tab_ShopClient_Shopping_VouchersScheme_Detail.id desc";
                            strWhere = string.Format(strWhere, Int32ShopClientID, Int32UserID, Int32GoodID);
                            System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_Shopping_VouchersScheme.SelectList(strWhere).Tables[0];


                            ListShowYouHuiQuanSearchBy = new List<ShowYouHuiQuanSearchBy>();
                            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                            {
                                ListShowYouHuiQuanSearchBy.Add(new ShowYouHuiQuanSearchBy
                                {
                                    Scheme_ID = Data_DataTable.Rows[i]["Scheme_ID"].toInt32(),
                                    VouchersNum = Data_DataTable.Rows[i]["VouchersNum"].toString(),
                                    GuWuCheIDOrOrderDetailID = Data_DataTable.Rows[i]["GuWuCheIDOrOrderDetailID"].toInt32(),
                                    Money = Data_DataTable.Rows[i]["Money"].toDecimal(),
                                    ValidateEndTime = Data_DataTable.Rows[i]["ValidateEndTime"].toDateTime(),
                                    HowToUse = Data_DataTable.Rows[i]["HowToUse"].toInt32(),
                                    HowToUseLimitMaxMoney = Data_DataTable.Rows[i]["HowToUseLimitMaxMoney"].toDecimal(),
                                    Vouchers_Title = Data_DataTable.Rows[i]["Vouchers_Title"].toString(),

                                });
                            }
                            if (Data_DataTable.Rows.Count > 0)///本用户有本商品的购物券
                            {
                                Int32ShowYouHuiQuanSearchByGoodID = 2;
                            }
                            #endregion 得到本商品能用的购物券列表

                        }
                        else
                        {
                            ////商城没有本商品的购物券
                            Int32ShowYouHuiQuanSearchByGoodID = 0;
                        }
                    }
                    else
                    {
                        Int32ShowYouHuiQuanSearchByGoodID = 0;
                    }
                }
                else
                {////商城没有购物券
                    Int32ShowYouHuiQuanSearchByGoodID = -1;
                }


            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "前端检查优惠券情况");
            }

            return Int32ShowYouHuiQuanSearchByGoodID;
        }

        #endregion ShowYouHuiQuanSearchByGoodID
    }
}