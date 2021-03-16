using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.SSQ_3D
{
    public partial class DoTask_Services_10_3DEveryday : System.Web.UI.Page
    {
        private static Object myLock3D = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            bool boolIfAddMeAgain = false;////需要开奖人数 多。重新 调用本程序。
            //            1、福彩3D没有电视直播的，3D开奖直播是广播《中国之声》频道20点30分开始。
            //2、双色球电视直播开奖是《中国教育频道》周二、四、日晚上9点30开始。
            try
            {
                lock (myLock3D)
                {
                    EggsoftWX.BLL.tab_HelpSSQ_3D BLL_tab_HelpSSQ_3D = new EggsoftWX.BLL.tab_HelpSSQ_3D();
                    string strSSQWhere = "Convert(varchar(10),[CreateTime],120) = Convert(varchar(10),getDate(),120) and Name='3D' and HaoMa is not null and QiShu is not null and (ISUsedEveryDay=0 or ISUsedEveryDay is null)";


                    if (BLL_tab_HelpSSQ_3D.Exists(strSSQWhere))
                    {
                        EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus BLL_tab_ZC_01Product_Support_GetBonus = new EggsoftWX.BLL.tab_ZC_01Product_Support_GetBonus();
                        EggsoftWX.BLL.tab_ZC_01Product_PartnerList BLL_tab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();


                        EggsoftWX.Model.tab_HelpSSQ_3D Model_tab_HelpSSQ_3D = BLL_tab_HelpSSQ_3D.GetModel(strSSQWhere);
                        String strHaoMa = Model_tab_HelpSSQ_3D.HaoMa;
                        String strQiShu = Model_tab_HelpSSQ_3D.QiShu;

                        #region 可以开奖
                        int intISUsedEveryDay = 0;

                        string str_ZC_01Product_Support = "select ID,SupportHowMany,ZC_01ProductID,ShopClientID,SourceGoodID  from tab_ZC_01Product_Support where SupportWay=2";
                        System.Data.DataTable DataTableSSQ = BLL_tab_HelpSSQ_3D.SelectList(str_ZC_01Product_Support).Tables[0];
                        for (int i = 0; i < DataTableSSQ.Rows.Count; i++)
                        {
                            string strSupportID = DataTableSSQ.Rows[i]["ID"].ToString();
                            string strSupportHowMany = DataTableSSQ.Rows[i]["SupportHowMany"].ToString();
                            string strZC_01ProductID = DataTableSSQ.Rows[i]["ZC_01ProductID"].ToString();
                            string strShopClientID = DataTableSSQ.Rows[i]["ShopClientID"].ToString();
                            string strSourceGoodID = DataTableSSQ.Rows[i]["SourceGoodID"].ToString();

                            string strWherePartnerList = "SELECT  TOP (100) PERCENT  tab_ZC_01Product_PartnerList.ID, tab_ZC_01Product_PartnerList.ZC_01ProductID, ";
                            strWherePartnerList += " tab_ZC_01Product_PartnerList.SupportID, tab_ZC_01Product_PartnerList.PayPrice, ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.ZCBuysSay, tab_ZC_01Product_PartnerList.Ispay,  ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.PayTime, tab_ZC_01Product_PartnerList.OrderID,  ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.IsCanSendGoods, tab_ZC_01Product_PartnerList.GetBonusID,  ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.ShopClientID, tab_ZC_01Product_PartnerList.IsDeleted,  ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.CreateTime, tab_ZC_01Product_PartnerList.UpdateTime,  ";
                            strWherePartnerList += "  tab_ZC_01Product_PartnerList.UserID, tab_ZC_01Product_PartnerList.GetGoodsAddress,  ";
                            strWherePartnerList += "   tab_ZC_01Product_Support.SupportWay ";
                            strWherePartnerList += "  FROM      tab_ZC_01Product_PartnerList LEFT OUTER JOIN ";
                            strWherePartnerList += "   tab_ZC_01Product_Support ON tab_ZC_01Product_PartnerList.SupportID = tab_ZC_01Product_Support.ID";
                            strWherePartnerList += " WHERE   ((tab_ZC_01Product_Support.SupportWay = 2) and (tab_ZC_01Product_PartnerList.GetBonusID is null) and (tab_ZC_01Product_PartnerList.SupportID=" + strSupportID + "))";
                            strWherePartnerList += "  ORDER BY dbo.tab_ZC_01Product_PartnerList.ID asc";

                            System.Data.DataTable PartnerListDataTable = BLL_tab_HelpSSQ_3D.SelectList(strWherePartnerList).Tables[0];
                            int intRowCount = PartnerListDataTable.Rows.Count;
                            if (intRowCount == 0)
                            {///今天 没有数据 开什么奖
                                if (intISUsedEveryDay == 0) intISUsedEveryDay = 2;
                            }
                            else if (intRowCount >= (Int32.Parse(strSupportHowMany)))
                            {
                                #region 开奖结果公布
                                if (intISUsedEveryDay == 0) intISUsedEveryDay = 1;///可以开奖
                                ///
                                long Int64HaoMa = Int64.Parse(strHaoMa);
                                ///取大数的 余数
                                ///
                                long intAllRenshu = intRowCount;
                                long intGetBonusRows = Int64HaoMa % intAllRenshu;
                                int intSmallINT = (int)intGetBonusRows;

                                string strGetBonusPartnerID = PartnerListDataTable.Rows[intSmallINT]["ID"].ToString();
                                string strGetBonusUserID = PartnerListDataTable.Rows[intSmallINT]["UserID"].ToString();
                                string strGetBonusUserCreateTime = PartnerListDataTable.Rows[intSmallINT]["CreateTime"].ToString();

                                string strBonusNumber = DateTime.Parse(strGetBonusUserCreateTime).ToString("yyyyMM") + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strGetBonusPartnerID), 3);

                                #region 插入中奖情况


                                EggsoftWX.Model.tab_ZC_01Product_Support_GetBonus Model_tab_ZC_01Product_Support_GetBonus = new EggsoftWX.Model.tab_ZC_01Product_Support_GetBonus();
                                Model_tab_ZC_01Product_Support_GetBonus.GoodID = Int32.Parse(strSourceGoodID);
                                Model_tab_ZC_01Product_Support_GetBonus.ShopClientID = Int32.Parse(strShopClientID);
                                Model_tab_ZC_01Product_Support_GetBonus.SupportID = Int32.Parse(strSupportID);
                                Model_tab_ZC_01Product_Support_GetBonus.ZC_01ProductID = Int32.Parse(strZC_01ProductID);
                                int intGetBonusID = BLL_tab_ZC_01Product_Support_GetBonus.Add(Model_tab_ZC_01Product_Support_GetBonus);

                                string strBonusShow = "";
                                strBonusShow += "微商城开奖期数" + DateTime.Now.ToString("yyyy") + Eggsoft.Common.StringNum.Add000000Num(intGetBonusID, 3) + ",中国福利彩票3D" + strQiShu + "期开奖结果" + strHaoMa;
                                strBonusShow += ",取余数" + intAllRenshu + "结果为" + intSmallINT;
                                strBonusShow += ",中奖编号为" + strBonusNumber + " 抽奖序号为(" + intSmallINT + "+1)即" + (intSmallINT + 1);
                                strBonusShow += ",中奖人为" + Eggsoft_Public_CL.Pub.GetNickName(strGetBonusUserID);
                                Model_tab_ZC_01Product_Support_GetBonus = BLL_tab_ZC_01Product_Support_GetBonus.GetModel(intGetBonusID);
                                Model_tab_ZC_01Product_Support_GetBonus.BonusContent = strBonusShow;
                                BLL_tab_ZC_01Product_Support_GetBonus.Update(Model_tab_ZC_01Product_Support_GetBonus);
                                #endregion 插入中奖情况

                                for (int j = 0; j < Int32.Parse(strSupportHowMany); j++)
                                {
                                    string strSendMsg = "";
                                    string strUpdatePartnerID = PartnerListDataTable.Rows[j]["ID"].ToString();
                                    string strUpdatePartnerUserID = PartnerListDataTable.Rows[j]["UserID"].ToString();
                                    if (strUpdatePartnerID != strGetBonusPartnerID)
                                    {
                                        BLL_tab_ZC_01Product_PartnerList.Update("IsCanSendGoods=0,UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',GetBonusID=" + intGetBonusID, "ID=" + strUpdatePartnerID);
                                        strSendMsg = strBonusShow + "。下次再来";
                                    }
                                    else
                                    {
                                        BLL_tab_ZC_01Product_PartnerList.Update("IsCanSendGoods=1,UpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',GetBonusID=" + intGetBonusID, "ID=" + strUpdatePartnerID);
                                        strSendMsg = strBonusShow + "。中奖";
                                    }

                                    #region 推送微信消息
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUpdatePartnerUserID), 0, strSendMsg);
                                    #endregion
                                }
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(8568, 0, "系统管理员大人：" + strBonusShow);

                                if (intRowCount > (Int32.Parse(strSupportHowMany))) boolIfAddMeAgain = true;///需要开奖人数 多。重新 调用本程序。
                                #endregion 开奖结果公布
                            }
                            else
                            {
                                if (intISUsedEveryDay == 0) intISUsedEveryDay = 3;///有数据 但是 不满足开奖条件
                            }
                        }
                        #endregion 可以开奖
                        if (Model_tab_HelpSSQ_3D.ISUsedEveryDay != 1)////可能 多次 开奖聊
                        {
                            Model_tab_HelpSSQ_3D.ISUsedEveryDay = intISUsedEveryDay;
                        }
                        Model_tab_HelpSSQ_3D.UpdateTime = DateTime.Now;
                        BLL_tab_HelpSSQ_3D.Update(Model_tab_HelpSSQ_3D);
                    }
                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "一元云购程序");
            };



            if (boolIfAddMeAgain)////需要开奖人数 多。重新 调用本程序。
            {
                Response.Redirect("DoTask_Services_10_3DEveryday.aspx");
            }


        }

    }
}