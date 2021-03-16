using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{

    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class Pub_Agent
    {
        public Pub_Agent()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 一键给予省级等代理商资格.   运营中心的批准需要这个资格
        public static int add_AdvanceAgent_Default_OnlyOneKey(int intUseID, int IntShopClientID)
        {
            int intBootIntLevel = Eggsoft_Public_CL.Pub.stringShowPower(IntShopClientID.toString(), "YunYingZhongXin_AdvanceAgentID").toInt32();
            if (intBootIntLevel <= 0) return -1;

            int intadd_AdvanceAgent_Default_OnlyOneKey = -1;
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUseID);
            try
            {
                lock (myadd_Agent_Default_OnlyOneKey)
                {


                    #region ///已经是代理了  就不调用这个了
                    ///
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolZhuan_TrueAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + intUseID + " and IsDeleted=0  and (Empowered=1) and isnull(AgentLevelSelect,0)>0 and ShopClientID=" + Model_tab_User.ShopClientID);
                    if (boolZhuan_TrueAgent == true)
                    {
                        SendWS_UserAgentCertification(intUseID);
                        intadd_AdvanceAgent_Default_OnlyOneKey = 1;///已经是代理了  直接返回
                    }

                    #endregion
                    if (intadd_AdvanceAgent_Default_OnlyOneKey != 1)
                    {
                        #region 顶级代理模式
                        //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                        //bool boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                        #endregion
                        EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                        BLL_tab_ShopClient_Agent__ProductClassID.Delete("UserID=" + intUseID + " and ShopClientID=" + Model_tab_User.ShopClientID);
                        if (Model_tab_User.ShopClientID != Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User.ParentID.ToString()))
                        {///  取得默认的TeamID

                            Eggsoft.Common.debug_Log.Call_WriteLog("模拟异常 运营中心默认配置编号", "ShopClientID不一致", "程序报错UserID=" + intUseID + " IntShopClientID=" + IntShopClientID);
                            Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID("0");
                            if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;
                            Model_tab_User.ParentID = 0;
                            Model_tab_User.UpdateBy = "ShopClientID不一致" + Model_tab_User.ShopClientID.toString() + " " + Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User.ParentID.ToString()).toString();
                            Model_tab_User.Updatetime = DateTime.Now;
                        }
                        else
                        {
                            Model_tab_User.UpdateBy = "改写ParentID";
                            Model_tab_User.Updatetime = DateTime.Now;
                            Model_tab_User.ParentID = Model_tab_User.ParentID == intUseID ? 0 : Model_tab_User.ParentID;
                            Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());


                            #region 增加直推未处理信息
                            if (Model_tab_User.ParentID > 0)
                            {
                                string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = "改写上级一键给予省级等代理商资格";
                                Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_User.ParentID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            }
                            #endregion 增加直推未处理信息  

                            #region 增加间推未处理信息
                            if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32()) > 0)
                            {
                                string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = "改写上上级一键给予省级等代理商资格";
                                Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32());
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            }
                            #endregion 增加直推未处理信息  

                        }

                        #region tab_ShopClient_Agent_
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=" + IntShopClientID + " and UserID=" + intUseID);
                        if (Model_tab_ShopClient_Agent_ == null)
                        {
                            Model_tab_ShopClient_Agent_ = new EggsoftWX.Model.tab_ShopClient_Agent_();
                            Model_tab_ShopClient_Agent_.ShopName = Model_tab_User.NickName;
                            Model_tab_ShopClient_Agent_.UserID = intUseID;
                            Model_tab_ShopClient_Agent_.ShopClientID = Model_tab_User.ShopClientID;
                            Model_tab_ShopClient_Agent_.ParentID = Convert.ToInt32(Model_tab_User.ParentID == intUseID ? 0 : Model_tab_User.ParentID);

                            Model_tab_ShopClient_Agent_.OnlyIsAngel = false;
                            Model_tab_ShopClient_Agent_.Empowered = true;
                            Model_tab_ShopClient_Agent_.AgentLevelSelect = intBootIntLevel;

                            BLL_tab_ShopClient_Agent_.Add(Model_tab_ShopClient_Agent_);
                        }
                        else
                        {
                            if (Model_tab_ShopClient_Agent_.IsDeleted != 0)
                            {
                                Model_tab_ShopClient_Agent_.IsDeleted = 0;
                                Model_tab_ShopClient_Agent_.UpdateBy = "一键给予省级等代理商资格恢复删除";
                            }
                            Model_tab_ShopClient_Agent_.ParentID = Convert.ToInt32(Model_tab_User.ParentID == intUseID ? 0 : Model_tab_User.ParentID);
                            Model_tab_ShopClient_Agent_.OnlyIsAngel = false;
                            Model_tab_ShopClient_Agent_.Empowered = true;
                            Model_tab_ShopClient_Agent_.AgentLevelSelect = intBootIntLevel;
                            Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                            BLL_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);
                        }
                        BLL_tab_User.Update(Model_tab_User);///出现了 14=14  的情况 可能只有本机会出现吧

                        #endregion tab_ShopClient_Agent_
                        #region 商品挑选 父数组
                        ArrayList ArrayListSQL = new ArrayList();
                        int[] mf1_ParentID_ProductID_List = null;///父数组代理的是否有此商品
                        System.Data.DataTable myAgentDataTable2 = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + Model_tab_User.ParentID + " and (Empowered=1 or OnlyIsAngel=1) order by id asc").Tables[0];
                        mf1_ParentID_ProductID_List = new int[myAgentDataTable2.Rows.Count]; //注意初始化数组的范围,或者指定初值;
                        System.Data.DataSet myds_tab_Goods = null;
                        EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();

                        for (int i = 0; i < myAgentDataTable2.Rows.Count; i++)
                        {
                            mf1_ParentID_ProductID_List[i] = myAgentDataTable2.Rows[i]["ProductID"].toInt32();
                        }
                        if (mf1_ParentID_ProductID_List == null || mf1_ParentID_ProductID_List.Length == 0)
                        {
                            BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            myds_tab_Goods = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value", " ShopClient_ID=" + Model_tab_User.ShopClientID + " and isSaled=1 and IsDeleted=0 order by Sort,id asc");
                            mf1_ParentID_ProductID_List = new int[myds_tab_Goods.Tables[0].Rows.Count]; //注意初始化数组的范围,或者指定初值;

                            for (int i = 0; i < (myds_tab_Goods.Tables[0].Rows.Count); i++)
                            {
                                mf1_ParentID_ProductID_List[i] = myds_tab_Goods.Tables[0].Rows[i]["ID"].toInt32();
                            }
                        }
                        Array.Sort(mf1_ParentID_ProductID_List);
                        #endregion 父数组

                        #region 有父亲  看 是几级代理
                        EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Model_tab_User.ShopClientID.toInt32(), intUseID, 0);
                        int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();

                        //Decimal[] myAgentFenXiaoList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Convert.ToInt32(Model_tab_User.ShopClientID), 0);



                        //都是总店铺挑选
                        myds_tab_Goods = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value", " ShopClient_ID=" + Model_tab_User.ShopClientID + " and isSaled=1 and IsDeleted=0 order by Sort,id asc");
                        //}
                        for (int i = 0; i < (myds_tab_Goods.Tables[0].Rows.Count); i++)
                        {
                            string strGood = myds_tab_Goods.Tables[0].Rows[i]["ID"].ToString();
                            //string strPrice_Percent = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent"].ToString();
                            //string strPrice_Percent1 = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent1"].ToString();
                            //string strPrice_Percent2 = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent2"].ToString();

                            bool boolParentAgent = (mf1_ParentID_ProductID_List != null) && (mf1_ParentID_ProductID_List.Length != 0) && (Array.BinarySearch(mf1_ParentID_ProductID_List, Int32.Parse(strGood)) != -1);///父是否代理
                            if (boolParentAgent)
                            {
                                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();

                                Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel = false;
                                Model_tab_ShopClient_Agent__ProductClassID.Empowered = true;

                                Model_tab_ShopClient_Agent__ProductClassID.UserID = intUseID;
                                Model_tab_ShopClient_Agent__ProductClassID.ProductID = Int32.Parse(strGood);
                                Model_tab_ShopClient_Agent__ProductClassID.ShopClientID = Model_tab_User.ShopClientID;
                               

                                StringBuilder strSql = new StringBuilder();
                                strSql.Append("INSERT INTO  [tab_ShopClient_Agent__ProductClassID] (UserID,ShopClientID,ProductID,UpdateTime,Empowered,OnlyIsAngel,StockNum_MeHavebuyNum,ProductRightNum,ProductPrice)  values ");
                                strSql.Append("(" + Model_tab_ShopClient_Agent__ProductClassID.UserID + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ShopClientID + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductID + ",");
                                strSql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                                strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.Empowered.toInt32()) + ",");
                                strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel.toInt32()) + ",");
                                   strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum.toDecimal() + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductRightNum.toDecimal() + ",");
                                //strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.Full_Vouchers_.toInt32()) + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductPrice.toDecimal() + ")");
                                ArrayListSQL.Add(strSql.ToString());
                                Eggsoft.Common.debug_Log.Call_WriteLog("ArrayListSQL.Add(strSql.ToString())=" + strSql.ToString());
                            }
                        }
                        #endregion
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);
                        SendWS_UserAgentCertification(intUseID);
                    }
                    else
                    {
                        intadd_AdvanceAgent_Default_OnlyOneKey = 2;
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "一键给予省级等代理商资格");
            }
            finally
            {

            }
            return intadd_AdvanceAgent_Default_OnlyOneKey;
        }
        #endregion 一键给予省级等代理商资格


        private static Object myadd_Agent_Default_OnlyOneKey = new object();
        /// <summary>
        /// 一键成为代理商  分销商
        /// </summary>
        /// <returns></returns>
        public static void add_Agent_Default_OnlyOneKey(int intUseID, bool isGiveOnlyIsAngel = false)
        {
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUseID);
            try
            {
                lock (myadd_Agent_Default_OnlyOneKey)
                {


                    #region ///已经是代理了  就不调用这个了
                    ///
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolZhuan_TrueAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + intUseID + " and (Empowered=1)" + "  and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID);
                    bool boolZhuanOnlyIsAngel = BLL_tab_ShopClient_Agent_.Exists("UserID=" + intUseID + " and (OnlyIsAngel=1)" + "  and IsDeleted=0  and ShopClientID=" + Model_tab_User.ShopClientID);
                    if (boolZhuan_TrueAgent == true)
                    {
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_Cur = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intUseID + " and (Empowered=1)" + "  and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID);
                        if (Model_tab_ShopClient_Agent_Cur.IsDeleted != 0)
                        {
                            Model_tab_ShopClient_Agent_Cur.IsDeleted = 0;
                            Model_tab_ShopClient_Agent_Cur.UpdateBy = "一键成为代理商  分销商 恢复删除";
                            Model_tab_ShopClient_Agent_Cur.UpdateTime = DateTime.Now;
                            BLL_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_Cur);
                        }


                        SendWS_UserAgentCertification(intUseID);
                        return;///已经是代理了  直接返回
                    }
                    if (boolZhuanOnlyIsAngel == true && isGiveOnlyIsAngel)
                    {
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_Cur = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intUseID + " and (OnlyIsAngel=1)" + "  and IsDeleted=0  and ShopClientID=" + Model_tab_User.ShopClientID);
                        if (Model_tab_ShopClient_Agent_Cur.IsDeleted != 0)
                        {
                            Model_tab_ShopClient_Agent_Cur.IsDeleted = 0;
                            Model_tab_ShopClient_Agent_Cur.UpdateBy = "一键成为代理商已经是天使代理了  分销商 恢复删除";
                            Model_tab_ShopClient_Agent_Cur.UpdateTime = DateTime.Now;
                            BLL_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_Cur);
                        }

                        return;///已经是天使代理了  直接返回
                    }
                    #endregion
                    #region 顶级代理模式
                    //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                    //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                    //bool boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                    #endregion
                    EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();

                    BLL_tab_ShopClient_Agent_.Delete("UserID=" + intUseID + " and ShopClientID=" + Model_tab_User.ShopClientID);
                    BLL_tab_ShopClient_Agent__ProductClassID.Delete("UserID=" + intUseID + " and ShopClientID=" + Model_tab_User.ShopClientID);

                    #region tab_ShopClient_Agent_
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = new EggsoftWX.Model.tab_ShopClient_Agent_();
                    Model_tab_ShopClient_Agent_.ShopName = Model_tab_User.NickName;
                    Model_tab_ShopClient_Agent_.UserID = intUseID;
                    Model_tab_ShopClient_Agent_.ShopClientID = Model_tab_User.ShopClientID;
                    Model_tab_ShopClient_Agent_.ParentID = Convert.ToInt32(Model_tab_User.ParentID == intUseID ? 0 : Model_tab_User.ParentID);

                    if (Model_tab_User.ShopClientID != Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User.ParentID.ToString()))
                    {
                        Model_tab_User.ParentID = 0;
                        Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID("0");
                    }
                    else
                    {
                        Model_tab_User.ParentID = Model_tab_User.ParentID == intUseID ? 0 : Model_tab_User.ParentID;
                        Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());


                        #region 增加直推未处理信息
                        if (Model_tab_User.ParentID > 0)
                        {
                            string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "改写上级一键成为代理商";
                            Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UserID = Model_tab_User.ParentID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                            Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                            Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        }
                        #endregion 增加直推未处理信息  

                        #region 增加间推未处理信息
                        if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32()) > 0)
                        {
                            string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "改写上上级一键成为代理商";
                            Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32());
                            Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                            Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                            Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        }
                        #endregion 增加直推未处理信息  

                    }
                    BLL_tab_User.Update(Model_tab_User);///出现了 14=14  的情况 可能只有本机会出现吧
                    if (isGiveOnlyIsAngel)
                    {
                        Model_tab_ShopClient_Agent_.OnlyIsAngel = true;
                    }
                    else
                    {
                        Model_tab_ShopClient_Agent_.OnlyIsAngel = false;
                        Model_tab_ShopClient_Agent_.Empowered = true;
                    }

                    BLL_tab_ShopClient_Agent_.Add(Model_tab_ShopClient_Agent_);
                    #endregion tab_ShopClient_Agent_
                    #region 父数组
                    ArrayList ArrayListSQL = new ArrayList();
                    int[] mf1_ParentID_ProductID_List = null;///父数组代理的是否有此商品
                    System.Data.DataTable myAgentDataTable2 = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + Model_tab_User.ParentID + " and (Empowered=1 or OnlyIsAngel=1) order by id asc").Tables[0];
                    mf1_ParentID_ProductID_List = new int[myAgentDataTable2.Rows.Count]; //注意初始化数组的范围,或者指定初值;
                    System.Data.DataSet myds_tab_Goods = null;
                    EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();

                    for (int i = 0; i < myAgentDataTable2.Rows.Count; i++)
                    {
                        mf1_ParentID_ProductID_List[i] = myAgentDataTable2.Rows[i]["ProductID"].toInt32();
                    }
                    if (mf1_ParentID_ProductID_List == null || mf1_ParentID_ProductID_List.Length == 0)
                    {
                        BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        myds_tab_Goods = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value", " ShopClient_ID=" + Model_tab_User.ShopClientID + " and isSaled=1 and IsDeleted=0 order by Sort,id asc");
                        mf1_ParentID_ProductID_List = new int[myds_tab_Goods.Tables[0].Rows.Count]; //注意初始化数组的范围,或者指定初值;

                        for (int i = 0; i < (myds_tab_Goods.Tables[0].Rows.Count); i++)
                        {
                            mf1_ParentID_ProductID_List[i] = myds_tab_Goods.Tables[0].Rows[i]["ID"].toInt32();
                        }
                    }
                    Array.Sort(mf1_ParentID_ProductID_List);
                    #endregion 父数组

                    #region 有父亲  看 是几级代理
                    // Decimal[] myAgentFenXiaoList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Convert.ToInt32(Model_tab_User.ShopClientID), 0);
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Model_tab_User.ShopClientID.toInt32(), intUseID, 0);
                    int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();



                    //都是总店铺挑选
                    myds_tab_Goods = BLL_tab_Goods.GetList("ID,Name,Webuy8_DistributionMoney_Value", " ShopClient_ID=" + Model_tab_User.ShopClientID + " and isSaled=1 and IsDeleted=0 order by Sort,id asc");
                    //}
                    for (int i = 0; i < (myds_tab_Goods.Tables[0].Rows.Count); i++)
                    {
                        string strGood = myds_tab_Goods.Tables[0].Rows[i]["ID"].ToString();
                        //string strPrice_Percent = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent"].ToString();
                        //string strPrice_Percent1 = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent1"].ToString();
                        //string strPrice_Percent2 = myds_tab_Goods.Tables[0].Rows[i]["Price_Percent2"].ToString();

                        bool boolParentAgent = (mf1_ParentID_ProductID_List != null) && (mf1_ParentID_ProductID_List.Length != 0) && (Array.BinarySearch(mf1_ParentID_ProductID_List, Int32.Parse(strGood)) != -1);///父是否代理
                        if (boolParentAgent)
                        {
                            EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();
                            if (isGiveOnlyIsAngel)
                            {
                                Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel = true;
                            }
                            else
                            {
                                Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel = false;
                                Model_tab_ShopClient_Agent__ProductClassID.Empowered = true;
                            }
                            Model_tab_ShopClient_Agent__ProductClassID.UserID = intUseID;
                            Model_tab_ShopClient_Agent__ProductClassID.ProductID = Int32.Parse(strGood);
                            Model_tab_ShopClient_Agent__ProductClassID.ShopClientID = Model_tab_User.ShopClientID;
                            


                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("INSERT INTO  [tab_ShopClient_Agent__ProductClassID] (UserID,ShopClientID,ProductID,UpdateTime,Empowered,OnlyIsAngel,StockNum_MeHavebuyNum,ProductRightNum,ProductPrice)  values ");
                            strSql.Append("(" + Model_tab_ShopClient_Agent__ProductClassID.UserID + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ShopClientID + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductID + ",");
                            strSql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                            strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.Empowered.toInt32()) + ",");
                            strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel.toInt32()) + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent.toDecimal() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent1.toDecimal() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent2.toDecimal() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum.toDecimal() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductRightNum.toDecimal() + ",");
                            //strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.Full_Vouchers_.toInt32()) + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductPrice.toDecimal() + ")");
                            ArrayListSQL.Add(strSql.ToString());
                            Eggsoft.Common.debug_Log.Call_WriteLog("ArrayListSQL.Add(strSql.ToString())=" + strSql.ToString());
                        }
                    }
                    #endregion
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);
                    if (isGiveOnlyIsAngel == false)///不是天使才给证书
                    {
                        SendWS_UserAgentCertification(intUseID);
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "一键成为代理商");
            }
            finally
            {

            }
        }
        /// <summary>
        /// 给用户代理证书
        /// </summary>
        /// <returns></returns>
        private static void SendWS_UserAgentCertification(int intUseID)
        {
            #region 给予老用户代理证书
            string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_UserAgentCertification.asmx";
            string[] args = new string[1];
            args[0] = intUseID.ToString();//;// "/UpLoad/images/";
            object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_UserAgentCertification", args);
            string strresult = result.toString();
            if (string.IsNullOrEmpty(strresult)) return;///可能是服务失败


            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
            //实例化几个WeiXinTuWen类对象  
            string strTitle = "已授权，并为您制作代理资格证。";
            string strDescription = "一键微店，万家同源，不用发货，公司帮你一切搞定。";

            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strresult, strDescription, Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strresult);
            WeiXinTuWens_ArrayList.Add(First);
            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(intUseID, 0, WeiXinTuWens_ArrayList);
            #endregion

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intShopClientID"></param>
        /// <param name="Int32UserID"></param>
        /// <param name="Int32GoodID">可以传0  表示判断级别之类的</param>
        /// <returns></returns>
        public static EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(int intShopClientID, Int32 Int32UserID, Int32 Int32GoodID)
        {

            string strSelectSQL = @"
SELECT  top 1 b019_tab_ShopClient_MultiFenXiaoLevel.OperationGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.OperationParentGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.OperationGrandParentGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.ChildGet, b019_tab_ShopClient_MultiFenXiaoLevel.GrandsonGet, 
                b019_tab_ShopClient_MultiFenXiaoLevel.GreatsonGet, b019_tab_ShopClient_MultiFenXiaoLevel.Sort, 
                b019_tab_ShopClient_MultiFenXiaoLevel.Name, b019_tab_ShopClient_MultiFenXiaoLevel.ShopClient_ID, 
                b019_tab_ShopClient_MultiFenXiaoLevel.ID, tab_ShopClient_Agent_Level_ProductInfo.ProductID, 
                tab_User.ID AS UserID
FROM      tab_ShopClient_Agent_ LEFT OUTER JOIN
                tab_User ON tab_ShopClient_Agent_.ShopClientID = tab_User.ShopClientID AND 
                tab_ShopClient_Agent_.ID = tab_User.TeamID
RIGHT OUTER JOIN
                tab_ShopClient_Agent_Level_ProductInfo ON 
                tab_ShopClient_Agent_.ShopClientID = tab_ShopClient_Agent_Level_ProductInfo.ShopClientID AND 
                tab_ShopClient_Agent_.AgentLevelSelect = tab_ShopClient_Agent_Level_ProductInfo.ShopClient_Agent_Level_ID
                 RIGHT OUTER JOIN
                b019_tab_ShopClient_MultiFenXiaoLevel ON 
                tab_ShopClient_Agent_Level_ProductInfo.b019_tab_ShopClient_MultiFenXiaoLevelID = b019_tab_ShopClient_MultiFenXiaoLevel.ID
                 AND 
                tab_ShopClient_Agent_Level_ProductInfo.ShopClientID = b019_tab_ShopClient_MultiFenXiaoLevel.ShopClient_ID

WHERE  (b019_tab_ShopClient_MultiFenXiaoLevel.ShopClient_ID = @ShopClient_ID) 

";
            System.Data.DataTable Data_DataTable = null;
            EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel myModel = new EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel();
            EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel BLL_b019_tab_ShopClient_MultiFenXiaoLevel = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();



            if (Int32UserID > 0 && Int32GoodID > 0)
            {
                strSelectSQL += " AND  (tab_User.ID = @tab_UserID)  AND (tab_ShopClient_Agent_Level_ProductInfo.ProductID = @tab_ShopClient_Agent_Level_ProductInfo_ProductID)";
                Data_DataTable = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.SelectList(strSelectSQL, intShopClientID, Int32UserID, Int32GoodID).Tables[0];
            }
            else if (Int32GoodID > 0)
            {
                strSelectSQL += " AND (tab_ShopClient_Agent_Level_ProductInfo.ProductID = @tab_ShopClient_Agent_Level_ProductInfo_ProductID)";
                Data_DataTable = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.SelectList(strSelectSQL, intShopClientID, Int32GoodID).Tables[0];
            }
            else if (Int32UserID > 0)
            {
                strSelectSQL += " AND  (tab_User.ID = @tab_UserID)";
                Data_DataTable = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.SelectList(strSelectSQL, intShopClientID, Int32UserID).Tables[0];
            }
            else
            {
                Data_DataTable = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.SelectList(strSelectSQL, intShopClientID).Tables[0];
            }


            if (Data_DataTable.Rows.Count > 0)
            {
                myModel = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.GetModel(Data_DataTable.Rows[0]["ID"].toInt32());
            }

            return myModel;
            //System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_FenXiaoLevel.GetList("ShopClient_ID=" + intShopClientID + " order by sort asc,id asc").Tables[0];
            //string allLevelPercent = BLL_tab_ShopClient_FenXiaoLevel.SelectList("select sum(LevelPercent) as allPercent from tab_ShopClient_FenXiaoLevel where ShopClient_ID=" + intShopClientID).Tables[0].Rows[0][0].ToString();


            //if (Data_DataTable.Rows.Count > 0)
            //{
            //    Decimal[] Pub_AgentList = new Decimal[Data_DataTable.Rows.Count];
            //    for (int i = Data_DataTable.Rows.Count - 1; i > -1; i--)
            //    {
            //        string strLevelPercent = Data_DataTable.Rows[i]["LevelPercent"].ToString();
            //        Pub_AgentList[i] = Decimal.Parse(strLevelPercent) / Decimal.Parse(allLevelPercent);
            //    }
            //    return Pub_AgentList;
            //}
            //else
            //{
            //Decimal[] Pub_AgentList = new Decimal[2];
            //Pub_AgentList[0] = (Decimal)0.75;//me
            //Pub_AgentList[1] = (Decimal)0.25;///parent
            //return Pub_AgentList;
            //}
        }


        /// <summary>
        /// 启用代理系统后 原来的叫分销 。怎么检查一下这个名字
        /// </summary>
        /// <returns></returns>
        public static String Pub_Agent_Default_GetAgent_FenXiaoOrDaili(int stringShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
            bool boolExsit = BLL_tab_ShopClient_Agent_Level.Exists("ShopClientID=" + stringShopClientID + "");
            string strReturn = "";
            if (boolExsit)
            {
                strReturn = "分销";
            }
            else
            {
                strReturn = "代理";
            }
            return strReturn;
        }

        /// <summary>
        /// 取UserID的微信推广二维码 http地址 七牛的
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="stringHttp"></param>
        /// <returns></returns>
        public static String Pub_Agent_GetAgent_WeiXinErWeiMaPath(int UserID, out string stringHttp)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolExsitAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + UserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1");

            if (boolExsitAgent)
            {
                int intShopClientID = Pub.GetShopClientIDFromUserID(UserID.ToString());
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

                string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/SAgent_01_" + UserID + ".jpg";
                stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + ls_Des_fileName;
                return ls_Des_fileName;
            }
            else
            {
                stringHttp = "";
                return "";
            }
        }

        /// <summary>
        /// 取UserID得推广证书 http地址 七牛的
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="stringHttp"></param>
        /// <returns></returns>
        public static String Pub_Agent_GetAgent_BookMarkerPath(int UserID, out string stringHttp)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolExsitAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + UserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1");

            if (boolExsitAgent)
            {
                int intShopClientID = Pub.GetShopClientIDFromUserID(UserID.ToString());
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

                string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/SAgent" + UserID + "Certifcation01.jpg";
                stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + ls_Des_fileName;
                return ls_Des_fileName;
            }
            else
            {
                stringHttp = "";
                return "";
            }
        }


        public static String Pub_Agent_GetAgent_PosterPath(int UserID, out string stringHttp)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolExsitAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + UserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1");

            if (boolExsitAgent)
            {
                int intShopClientID = Pub.GetShopClientIDFromUserID(UserID.ToString());
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

                string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/SAgent" + UserID + "Poster.jpg";
                stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + ls_Des_fileName;
                return ls_Des_fileName;
            }
            else
            {///天使海报
                int intShopClientID = Pub.GetShopClientIDFromUserID(UserID.ToString());
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);

                string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/Normal" + UserID + "Poster.jpg";
                stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + ls_Des_fileName;
                return ls_Des_fileName;
                //stringHttp = "";
                //return "";
            }
        }

        public static String Pub_Agent_Default_GetAgent()
        {
            string strAgentMustRead = "";
            strAgentMustRead += " <h2 class=\"tip-means-title\"><span>温馨提示</span><i class=\"icon-close\" onclick=\"tip_means_close(this)\"></i></h2>\n";
            strAgentMustRead += "    <div class=\"tip-means-c\">\n";
            strAgentMustRead += "  <span>亲，您的代理佣金由您的微店销售所得：</span>\n";
            strAgentMustRead += "         <ol class=\"tip-means-ol\">\n";
            strAgentMustRead += "             <li>销售的商品，我所获得佣金（即本店代理销售佣金）。</li>\n";
            strAgentMustRead += "             <li>每款销售商品，客户无异议，无退货后，佣金自动转入你的账户（一般T+7）。</li>\n";
            strAgentMustRead += "             <li>您的账户的现金可随时提现。</li>\n";
            strAgentMustRead += "             <li>本系统挑选商品后即可一键生成您的微店。</li>\n";
            strAgentMustRead += "             <li>下级分店发展的分店所销售的商品，我所获得的佣金（即一级分店佣金）。</li>\n";
            strAgentMustRead += "         </ol>\n";
            strAgentMustRead += " </div>\n";
            return strAgentMustRead;
        }

        public static String Pub_Agent_Default_GetAgentAd()
        {
            string Server_Str = "";
            Server_Str += "   <div style=\"text-align: center;\">\n";
            Server_Str += " 	<span style=\"font-size:14px;\"><strong>代理政策</strong></span></div>\n";
            Server_Str += " <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%;\">\n";
            Server_Str += " 	<colgroup>\n";
            Server_Str += " 		<col span=\"3\" />\n";
            Server_Str += " 	</colgroup>\n";
            Server_Str += " 	<tbody>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				代理级别</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				订货金额</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				代理金额</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				省级代理</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				10万</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				135</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				市级代理</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				48000</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				145</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				皇冠代理</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				20800</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				160</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				铂金代理</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				6840</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				180</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 		<tr height=\"72\">\n";
            Server_Str += " 			<td height=\"72\" style=\"height:72px;width:30%;\">\n";
            Server_Str += " 				天使代理</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				1090</td>\n";
            Server_Str += " 			<td style=\"width:30%;\">\n";
            Server_Str += " 				218</td>\n";
            Server_Str += " 		</tr>\n";
            Server_Str += " 	</tbody>\n";
            Server_Str += " </table>\n";
            Server_Str += " <br />\n";
            return Server_Str;
        }


        public static String GetMySon_AgentMoneyLoadingByPage(int intArgUserID, int loadingPageNum, int intAgentLevelNum = 0)
        {
            int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intArgUserID.ToString());

            EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intShopClientID, intArgUserID, 0);
            int intLevevNum = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();

            //int intLevevNum = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intShopClientID, 0).Length;
            bool boolIfShowMuSonMoney = true;//是否显示
            if (intAgentLevelNum >= intLevevNum)
            {
                boolIfShowMuSonMoney = false;
            }

            string strList = "{\"list\":[";

            //Decimal CountmyArgMoney = 0;
            int intPageSize = 3;

            if (intAgentLevelNum == 1)
            {
                #region 一级代理分销商
                string strWhereSQL = "ParentID=" + intArgUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))";
                strList += GetMyAgentSonList(strWhereSQL, loadingPageNum, intPageSize, boolIfShowMuSonMoney, intAgentLevelNum, intShopClientID);
                #endregion 一级代理分销商
            }
            else if (intAgentLevelNum == 2)
            {
                #region 二级代理分销商
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                //int intOneLevelTableCount = BLLtab_ShopClient_Agent_.ExistsCount("ParentID=" + intArgUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))");
                System.Data.DataTable OneLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + intArgUserID + "  and IsDeleted=0  and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                ArrayList myTwoLevelArrayList = new ArrayList();
                for (int i = 0; i < OneLevelTable.Rows.Count; i++)
                {
                    String strTwoUserID = OneLevelTable.Rows[i]["UserID"].ToString();
                    System.Data.DataTable TwoLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + strTwoUserID + "  and IsDeleted=0 and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                    for (int j = 0; j < TwoLevelTable.Rows.Count; j++)
                    {
                        String strThreeUserID = TwoLevelTable.Rows[j]["UserID"].ToString();
                        myTwoLevelArrayList.Add(strThreeUserID);
                    }
                }
                int intMAXRow = (loadingPageNum) * intPageSize > myTwoLevelArrayList.Count ? myTwoLevelArrayList.Count : (loadingPageNum) * intPageSize;///数组 不要超出
                for (int j = (loadingPageNum - 1) * intPageSize; j < intMAXRow; j++)
                {
                    string strUserID = myTwoLevelArrayList[j].ToString();
                    string strWhereSQL = "UserID=" + strUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))";
                    strList += GetMyAgentSonList(strWhereSQL, 1, intPageSize, boolIfShowMuSonMoney, intAgentLevelNum, intShopClientID);
                    if (j != intMAXRow - 1) strList += ",";
                }
                #endregion 二级代理分销商
            }
            else if (intAgentLevelNum == 3)
            {
                #region 二级代理分销商
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                //int intOneLevelTableCount = BLLtab_ShopClient_Agent_.ExistsCount("ParentID=" + intArgUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))");
                System.Data.DataTable OneLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + intArgUserID + "  and IsDeleted=0 and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                ArrayList myThreeLevelArrayList = new ArrayList();
                for (int i = 0; i < OneLevelTable.Rows.Count; i++)
                {
                    String strTwoUserID = OneLevelTable.Rows[i]["UserID"].ToString();
                    System.Data.DataTable TwoLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + strTwoUserID + "  and IsDeleted=0 and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                    for (int j = 0; j < TwoLevelTable.Rows.Count; j++)
                    {
                        String strThreeUserID = TwoLevelTable.Rows[j]["UserID"].ToString();
                        System.Data.DataTable ThreeLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + strThreeUserID + "  and IsDeleted=0 and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                        for (int k = 0; k < ThreeLevelTable.Rows.Count; k++)
                        {
                            String strFourUserID = ThreeLevelTable.Rows[k]["UserID"].ToString();
                            myThreeLevelArrayList.Add(strFourUserID);
                        }
                    }
                }
                int intMAXRow = (loadingPageNum) * intPageSize > myThreeLevelArrayList.Count ? myThreeLevelArrayList.Count : (loadingPageNum) * intPageSize;///数组 不要超出
                for (int j = (loadingPageNum - 1) * intPageSize; j < intMAXRow; j++)
                {
                    string strUserID = myThreeLevelArrayList[j].ToString();
                    string strWhereSQL = "UserID=" + strUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))";
                    strList += GetMyAgentSonList(strWhereSQL, 1, intPageSize, boolIfShowMuSonMoney, intAgentLevelNum, intShopClientID);
                    if (j != intMAXRow - 1) strList += ",";
                }
                #endregion 二级代理分销商
            }
            strList += "]}";
            return strList;

            //myArgMoney = CountmyArgMoney;
        }

        public static String GetMyAgentSonList(string strWhereSQL, int intLoadingPageNum, int intPageSize, bool boolIfShowMuSonMoney, int intAgentLevelNum, int intShopClientID)
        {
            web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
            //sql.addOrderField("tab_ShopClient_Agent_.myAgentSonSum", "desc");//第一排序字段  
            sql.addOrderField("tab_ShopClient_Agent_.id", "asc");//第二排序字段  
            sql.table = "tab_ShopClient_Agent_";
            sql.outfields = "tab_ShopClient_Agent_.UserID,tab_ShopClient_Agent_.ShopName,tab_ShopClient_Agent_.ID,tab_ShopClient_Agent_.UpdateTime";
            sql.nowPageIndex = intLoadingPageNum;

            sql.pagesize = intPageSize;
            sql.where = strWhereSQL;

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();


            int intAllCountsNum = BLLtab_ShopClient_Agent_.ExistsCount(strWhereSQL);
            string strSql = sql.getSQL(intAllCountsNum);
            System.Data.DataTable myDataTable = BLLtab_ShopClient_Agent_.SelectList(strSql).Tables[0];
            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.Model.tab_User();
            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLLtab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            //string strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intArgUserID);
            String strPub_Agent_Default_GetAgent_FenXiaoOrDaili = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDaili(intShopClientID);

            string strStrList = "";
            if (myDataTable.Rows.Count > 0)
            {
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strUserID = myDataTable.Rows[i]["UserID"].ToString();


                    string strShopName = myDataTable.Rows[i]["ShopName"].ToString();
                    string strID = myDataTable.Rows[i]["ID"].ToString();
                    string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();

                    string strHeadIMG = Eggsoft_Public_CL.Pub.Get_HeadImage(strUserID);
                    string strNickname = Eggsoft_Public_CL.Pub.GetNickName(strUserID);


                    Modeltab_User = BLLtab_User.GetModel(Int32.Parse(strUserID));
                    if (Modeltab_User == null) continue;

                    Decimal myDecimal = 0;

                    int myAgentSales = 0;

                    System.Data.DataTable myDataTable66 = BLLtab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and Empowered=1  order by id asc").Tables[0];
                    for (int j = 0; j < myDataTable66.Rows.Count; j++)
                    {
                        string strProductID = myDataTable66.Rows[j]["ProductID"].ToString();
                       
                        System.Data.DataTable myDataTable77 = BLLtab_Orderdetails.GetList("OrderCount", "GoodID=" + strProductID + " and ParentID=" + strUserID + " and Over7DaysToBeans=1 and isdeleted=0").Tables[0];
                        for (int m = 0; m < myDataTable77.Rows.Count; m++)
                        {
                            string strOrderCount = myDataTable77.Rows[m]["OrderCount"].ToString();

                            myAgentSales += Int32.Parse(strOrderCount);
                        }
                     }
                    String strCurList = "";
                    strCurList += "\"strHeadIMG\":" + "\"" + strHeadIMG + "\",";
                    strCurList += "\"strNickname\":" + "\"" + strNickname + "\",";
                    strCurList += "\"strShopName\":" + "\"" + strShopName + "\",";
                    strCurList += "\"UserRealName\":" + "\"" + Modeltab_User.UserRealName + "\",";
                    strCurList += "\"tel\":" + "\"" + Modeltab_User.ContactPhone + "\",";
                    strCurList += "\"GoodsCounts\":" + "\"" + myDataTable66.Rows.Count + "\",";
                    strCurList += "\"SalesGoodsCounts\":" + "\"" + myAgentSales + "\",";
                    //strCurList += "\"GetSalesMoney\":" + "\"" + Pub.getPubMoney(myDecimal) + "\",";

                    int intFenXiaoOrDailiCount = BLLtab_ShopClient_Agent_.ExistsCount("ParentID=" + strUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))");
                    Decimal mySonemoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.GetCountMySonMoney(Int32.Parse(strUserID), out mySonemoney);

                    strCurList += "\"GetmySalesMoney\":" + "\"" + Eggsoft_Public_CL.Pub.getPubMoney(mySonemoney) + "\",";
                    strCurList += "\"intFenXiaoOrDailiCount\":" + "\"" + intFenXiaoOrDailiCount + "\",";
                    strCurList += "\"_GetAgent_FenXiaoOrDaili\":" + "\"" + strPub_Agent_Default_GetAgent_FenXiaoOrDaili + "\",";
                    strCurList += "\"_GetAgent_UserID\":" + "\"" + strUserID + "\",";
                    strCurList += "\"boolIfShowMuSonMoney\":" + "\"" + boolIfShowMuSonMoney + "\",";
                    strCurList += "\"strPub_Agent_Path\":" + "\"" + "" + "\",";
                    strCurList += "\"intAgentLevelNum\":" + "\"" + intAgentLevelNum + "\",";


                    strStrList += "{" + strCurList + "\"id\":\"" + i.ToString() + "\",\"Name\":\"8888\",\"time\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\"}";
                    if (i != myDataTable.Rows.Count - 1) strStrList += ",";
                }
            }
            else
            {
                //strBody = "暂无数据";
            }

            return strStrList;
        }


        public static String GetMySon_AgentMoney(int intArgUserID, out Decimal myArgMoney, int intAgentLevelNum = 0)
        {
            int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intArgUserID.ToString());

            EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intShopClientID, intArgUserID, 0);
            int intLevevNum = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();

            // int intLevevNum = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intShopClientID, 0).Length;
            bool boolIfShowMuSonMoney = true;//是否显示
            if (intAgentLevelNum + 1 >= intLevevNum)
            {
                boolIfShowMuSonMoney = false;
            }
            string strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intArgUserID);
            String strPub_Agent_Default_GetAgent_FenXiaoOrDaili = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDaili(intShopClientID);

            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.Model.tab_User();

            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

            EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLLtab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
            String strBody = "";
            Decimal CountmyArgMoney = 0;
            System.Data.DataTable myDataTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + intArgUserID + "  and IsDeleted=0 and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))  order by id asc").Tables[0];

            if (myDataTable.Rows.Count > 0)
            {
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                    string strShopName = myDataTable.Rows[i]["ShopName"].ToString();
                    string strID = myDataTable.Rows[i]["ID"].ToString();
                    string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();

                    string strHeadIMG = Eggsoft_Public_CL.Pub.Get_HeadImage(strUserID);
                    string strNickname = Eggsoft_Public_CL.Pub.GetNickName(strUserID);


                    Modeltab_User = BLLtab_User.GetModel(Int32.Parse(strUserID));


                    Decimal myDecimal = 0;

                    int myAgentSales = 0;

                    System.Data.DataTable myDataTable66 = BLLtab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and Empowered=1  order by id asc").Tables[0];
                    for (int j = 0; j < myDataTable66.Rows.Count; j++)
                    {
                        string strProductID = myDataTable66.Rows[j]["ProductID"].ToString();
                        //string strPrice_Percent1 = myDataTable66.Rows[j]["Price_Percent1"].ToString();///存在 这个 就是我的收入
                        ///

                        ////Decimal Decimal_ProductID1 = 0;
                        ////Decimal.TryParse(strPrice_Percent1, out Decimal_ProductID1);
                        //if (Decimal_ProductID1 > 0)
                        //{
                        //    System.Data.DataTable myDataTable77 = BLLtab_Orderdetails.GetList("GoodID=" + strProductID + " and ParentID=" + strUserID + " and Over7DaysToBeans=1 and isdeleted=0").Tables[0];
                        //    for (int m = 0; m < myDataTable77.Rows.Count; m++)
                        //    {
                        //        string strGoodPrice = myDataTable77.Rows[m]["GoodPrice"].ToString();
                        //        string strOrderCount = myDataTable77.Rows[m]["OrderCount"].ToString();
                        //        myAgentSales += Int32.Parse(strOrderCount);
                        //        Decimal DecimalGoodPrice = 0;
                        //        Decimal.TryParse(strGoodPrice, out DecimalGoodPrice);
                        //        myDecimal += decimal.Multiply(decimal.Multiply(decimal.Multiply(DecimalGoodPrice, Int32.Parse(strOrderCount)), Decimal_ProductID1), (decimal)0.01);
                        //    }
                        //}

                    }

                    CountmyArgMoney += myDecimal;

                    strBody += "<li ms-repeat=\"items\">\n";
                    strBody += "			<div class=\"ul_li\">\n";
                    strBody += "				<div class=\"ul_li_div_img\">\n";
                    strBody += "					<div class=\"div_img_center\"><img alt=\"图片\" width=\"90px\" height=\"90px\"\n";
                    strBody += "						src=\"" + strHeadIMG + "\"></div>\n";
                    strBody += "					<div class=\"ul_li_div_name\">" + strNickname + "</div>\n";

                    strBody += "				</div>\n";

                    strBody += "				<div class=\"ul_li_trade\"><ul class=\"OliverModi\">\n";
                    strBody += "					<li>代理店铺名:" + strShopName + "</li>\n";

                    strBody += "						<li>联系人:" + Modeltab_User.UserRealName + "</li>\n";
                    strBody += "						<li>电话:<a href=\"tel:" + Modeltab_User.ContactPhone + "\">" + Modeltab_User.ContactPhone + "</a></li>\n";
                    //strBody += "						<li>店铺更新时间:" + strUpdateTime + "</li>\n";
                    strBody += "						<li>代理商品总数:" + myDataTable66.Rows.Count + "</li>\n";
                    //strBody += "				<div class=\"ul_li_money_Percent\"><ul class=\"OliverModi\">\n";
                    strBody += "						<li>代理销售商品数:" + myAgentSales + "%</li>\n";
                    strBody += "						<li>所得收入:" + Pub.getPubMoney(myDecimal) + "￥(T+7)</li>\n";
                    // EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    int intFenXiaoOrDailiCount = BLLtab_ShopClient_Agent_.ExistsCount("ParentID=" + strUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))");
                    Decimal mySonemoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.GetCountMySonMoney(Int32.Parse(strUserID), out mySonemoney);



                    if (boolIfShowMuSonMoney)
                    {
                        strBody += "<li><a style=\"color:blue;\" href='" + strPub_Agent_Path + "/multibutton_customer.aspx?lookmysonid=" + strUserID + "&agentlevelnum=" + (intAgentLevelNum + 1).ToString() + "'>直接收入(" + Eggsoft_Public_CL.Pub.getPubMoney(mySonemoney) + "￥)</a></li>";
                        strBody += "<li><a style=\"color:blue;\" href='" + strPub_Agent_Path + "/multibutton_agent.aspx?lookmysonid=" + strUserID + "&agentlevelnum=" + (intAgentLevelNum + 1).ToString() + "'>下级" + strPub_Agent_Default_GetAgent_FenXiaoOrDaili + "商(" + intFenXiaoOrDailiCount.ToString() + "个)</a></li>";
                    }
                    else
                    {
                        strBody += "<li>直接收入(" + Eggsoft_Public_CL.Pub.getPubMoney(mySonemoney) + "￥)</li>";
                        strBody += "<li>下级" + strPub_Agent_Default_GetAgent_FenXiaoOrDaili + "商(" + intFenXiaoOrDailiCount.ToString() + "个)</li>";
                    }
                    strBody += "				</ul></div>\n";

                    strBody += "			</div>\n";
                    strBody += "		</li>\n";
                }
            }
            else
            {
                strBody = "暂无数据";
            }

            myArgMoney = CountmyArgMoney;
            return strBody;
        }

        public class shorturl_Json
        {
            public String errcode { get; set; }
            public String errmsg { get; set; }
            public String short_url { get; set; }
        }


        public static string LongUrlToShortUrl(int intShopClientID, string strLongUrl)
        {
            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
            String strGet_ACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID);
            string str_URL = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token=" + strGet_ACCESS_TOKEN;
            string str_json_URL = "";
            string strLongURL = strLongUrl;
            str_json_URL = "{\"action\":\"long2short\",\"long_url\":\"" + strLongURL + "\"}";
            string str_Json = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(str_URL, str_json_URL);
            shorturl_Json my_Json = JsonHelper.JsonDeserialize<shorturl_Json>(str_Json);


            return my_Json.short_url;

        }






        public static string strGet_SAgent_ProductGoodClass(int Int_Session_CurUserID, int intpShopClientID, string strPub_Agent_Path, int intClassGoodType, int intpClassID)
        {

            string strReturn = "";

            ///先识别身份。  总是有用的 任何一页都是如此啊
            //int pInt_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            //检查是否是代理
            ///
            System.Data.DataTable myds__Goods_Class = null;

            if (intClassGoodType == 0)
            {
                EggsoftWX.BLL.tab_Class1 bll_tab_Class1 = new EggsoftWX.BLL.tab_Class1();
                myds__Goods_Class = bll_tab_Class1.GetList("ShopClientID=" + intpShopClientID + " order by sort asc,id asc").Tables[0];
            }
            else if (intClassGoodType == 1)
            {
                EggsoftWX.BLL.tab_Class2 bll_tab_Class2 = new EggsoftWX.BLL.tab_Class2();
                myds__Goods_Class = bll_tab_Class2.GetList("ShopClientID=" + intpShopClientID + " and Class1_ID=" + intpClassID + " order by sort asc,id asc").Tables[0];
            }
            else if (intClassGoodType == 2)
            {
                EggsoftWX.BLL.tab_Class3 bll_tab_Class3 = new EggsoftWX.BLL.tab_Class3();
                myds__Goods_Class = bll_tab_Class3.GetList("ShopClientID=" + intpShopClientID + " and Class2_ID=" + intpClassID + " order by sort asc,id asc").Tables[0];
            }
            else if (intClassGoodType == 3)
            {
                EggsoftWX.BLL.tab_Class2 bll_tab_Class2 = new EggsoftWX.BLL.tab_Class2();
                myds__Goods_Class = bll_tab_Class2.GetList("ShopClientID=" + intpShopClientID + " and Class1_ID=" + intpClassID + " order by sort asc,id asc").Tables[0];
            }

            string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(Int_Session_CurUserID);
            string strSQLView_Goods_Agent = @"SELECT   tab_ShopClient_Agent__ProductClassID.UserID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, 
                tab_Goods.Class2_ID, tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, 
                tab_Goods.Icon, tab_Goods.ShortInfo, tab_Goods.KuCunCount, 
                tab_Goods.Unit, tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, 
                tab_Goods.IsCommend, tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, 
                tab_Goods.UpdateTime, tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, 
                tab_Goods.Good_Class, tab_Goods.SalesCount, tab_Goods.LimitTimerBuy_StartTime, 
                tab_Goods.LimitTimerBuy_EndTime, tab_Goods.LimitTimerBuy_TimePrice, 
                tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, tab_Goods.MaxOrderNum, 
                tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, tab_Goods.IS_Admin_check, 
                tab_Goods.CheckBox_WeiBai_RedMoney, tab_Goods.Webuy8_DistributionMoney_Value, 
                tab_Goods.FreightTemplate_ID, tab_Goods.ID, 
                tab_ShopClient_Agent__ProductClassID.Empowered
FROM      tab_Goods RIGHT OUTER JOIN
                tab_ShopClient_Agent__ProductClassID ON 
                tab_Goods.ID = tab_ShopClient_Agent__ProductClassID.ProductID and tab_Goods.ShopClient_ID = tab_ShopClient_Agent__ProductClassID.ShopClientID  where tab_ShopClient_Agent__ProductClassID.UserID={0} and tab_Goods.ShopClient_ID={1} {2} and tab_Goods.isSaled=1 and tab_Goods.IsDeleted=0 and tab_Goods.IS_Admin_check=1 {3} order by tab_Goods.sort asc,tab_Goods.updatetime desc		
";



            if (myds__Goods_Class.Rows.Count > 0)
            {
                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                Model_tab_ShopClient = bll_tab_ShopClient.GetModel(intpShopClientID);
                int intPub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);


                if (intPub_IntStyle_Model == 0)
                {
                    strReturn += "        <div class=\"jx_map_bd WX_cat_list\">\n";
                    for (int i = 0; i < myds__Goods_Class.Rows.Count; i++)
                    {
                        string strSort = myds__Goods_Class.Rows[i]["sort"].ToString();
                        string strID = myds__Goods_Class.Rows[i]["ID"].ToString();
                        string ShowMenuName = myds__Goods_Class.Rows[i]["ClassName"].ToString();
                        strReturn += "<a href=\"" + strPub_Agent_Path + "/productclass.aspx?pclassgoodtype=" + (intClassGoodType + 1) + "&pclassid=" + strID + "\" class=\"J_ytag WX_cat_sp\">" + ShowMenuName + "</a>";
                    }
                    strReturn += "        </div>\n";
                }
                else if (intPub_IntStyle_Model == 1)
                {
                    string _NewstrGoodBodyest = " <div class=\"NewStyle3jx_map_bd\">\n";
                    for (int i = 0; i < myds__Goods_Class.Rows.Count; i++)
                    {
                        string strSort = myds__Goods_Class.Rows[i]["sort"].ToString();
                        string strstrClass123_ID = myds__Goods_Class.Rows[i]["ID"].ToString();
                        string ShowMenuName = myds__Goods_Class.Rows[i]["ClassName"].ToString();
                        string strA = strPub_Agent_Path + "/productclass.aspx?pclassgoodtype=" + (intClassGoodType + 1) + "&pclassid=" + strstrClass123_ID;
                        _NewstrGoodBodyest += "<div onclick=\"javascript:window.location.href='" + strA + "'\" class=\"ClassClickStyle\"><a href=\"" + strA + "\" class=\"Classname\">" + ShowMenuName;
                        _NewstrGoodBodyest += "<span class=\"More\">更多〉</span>";
                        _NewstrGoodBodyest += "</a></div>";

                        #region 获取本分类的商品
                        #region bll获取本分类的商品
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + Int_Session_CurUserID + "  and IsDeleted=0 and (Empowered=1 or OnlyIsAngel=1)");///有代理啊

                        string strWhereClass = "";
                        if (intClassGoodType == 0)///首页
                        {
                            strWhereClass = "and tab_Goods.Class1_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 1)///首页
                        {
                            strWhereClass = "and tab_Goods.Class2_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 2)///首页
                        {
                            strWhereClass = "and tab_Goods.Class3_ID=" + strstrClass123_ID + "";
                        }


                        System.Data.DataSet myds = null;
                        if (boolAgent)
                        {
                            myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, Int_Session_CurUserID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));

                            if (myds.Tables[0].Rows.Count == 0) {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }
                            //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                            //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + Int_Session_CurUserID + " and Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                        }
                        else
                        {
                            ///是不是别家的商品
                            ///

                            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(Int_Session_CurUserID);
                            if (pInt_QueryString_ParentID > 0) //是访问别人代理店铺；
                            {
                                myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pInt_QueryString_ParentID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));

                                //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();                              
                                //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + pInt_QueryString_ParentID + " and  Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                if (myds.Tables[0].Rows.Count == 0)//父代理不存在了
                                {
                                    Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_DeleteCookies();
                                    EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                    myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                }
                            }
                            else
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }

                        }
                        #endregion
                        int intShowCount = myds.Tables[0].Rows.Count;




                        #region for
                        _NewstrGoodBodyest += "<section id=\"search-content_Class\">\n";
                        _NewstrGoodBodyest += " <div class=\"i_wrap margin_auto rel hide\" id=\"item_classes_list_wrap\" style=\"display: block;\">\n";
                        _NewstrGoodBodyest += "     <ul class=\"i_ul rel\" id=\"hot_ul__\">\n";



                        for (int inti = 0; inti < (intShowCount > 2 ? 2 : intShowCount); inti++)
                        {
                            string str2GoodID = myds.Tables[0].Rows[inti]["ID"].ToString();
                            string str2GoodName = myds.Tables[0].Rows[inti]["Name"].ToString();
                            string str2GoodIconOOO = myds.Tables[0].Rows[inti]["Icon"].ToString();
                            string str2GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(str2GoodIconOOO, 200);


                            int int2SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(str2GoodID));
                            string str2Price = myds.Tables[0].Rows[inti]["Price"].ToString();
                            string str2PromotePrice = myds.Tables[0].Rows[inti]["PromotePrice"].ToString();
                            string str2ShortInfo = myds.Tables[0].Rows[inti]["ShortInfo"].ToString();
                            int int2intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(str2GoodID));
                            int int2ByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(str2GoodID));

                            _NewstrGoodBodyest += "  <li class=\"i_li left\">\n";
                            _NewstrGoodBodyest += " <a href=\"" + Pub_Agent_Path + "/product-" + str2GoodID + ".aspx\">\n";
                            _NewstrGoodBodyest += "<div  class=\"box\">\n";
                            _NewstrGoodBodyest += "<img  onload=\"AutoResizeImage(this)\" src=\"" + str2GoodIcon + "\">\n";
                            _NewstrGoodBodyest += "   </div>\n";
                            _NewstrGoodBodyest += "<p class=\"i_txt\">" + str2ShortInfo + " </p><p class=\"i_pri_wrap\"><span style=\"padding-right:2px;\" class=\"i_pri\">¥\n";
                            _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(str2PromotePrice));
                            _NewstrGoodBodyest += "</span>";

                            if (Decimal.Parse(str2PromotePrice) != Decimal.Parse(str2Price))
                            {
                                _NewstrGoodBodyest += "<span style=\"text-decoration:line-through;color: #666666;font-size: 12px;\" class=\"i_pri\">¥\n";
                                _NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(str2Price));
                                _NewstrGoodBodyest += "</span>";
                                _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;\" class=\"i_pri\">\n";
                                _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(str2GoodName, 5);
                                _NewstrGoodBodyest += "</span>";
                            }
                            else
                            {
                                _NewstrGoodBodyest += "<span style=\"color:#666666;font-size:12px;padding-left:0px;\" class=\"i_pri\">\n";
                                _NewstrGoodBodyest += Eggsoft.Common.StringNum.MaxLengthString(str2GoodName, 8);
                                _NewstrGoodBodyest += "</span>";
                            }

                            //_NewstrGoodBodyest += "<span style=\"text-decoration:line-through;color: #666666;font-size: 12px;\" class=\"i_pri\">¥\n";
                            //_NewstrGoodBodyest += Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(str2Price));
                            //_NewstrGoodBodyest += "</span>";



                            _NewstrGoodBodyest += "</p></a></li>\n";



                        }

                        _NewstrGoodBodyest += "</ul><div class=\"clear\"></div>\n";
                        _NewstrGoodBodyest += "</div>\n";
                        _NewstrGoodBodyest += " </section>\n";


                        #endregion for

                        #endregion
                    }
                    _NewstrGoodBodyest += "        </div>\n";

                    strReturn += _NewstrGoodBodyest;
                }
                else if (intPub_IntStyle_Model == 2)
                {
                    strReturn += " <div class=\"NewStyle3jx_map_bd\">\n";
                    for (int i = 0; i < myds__Goods_Class.Rows.Count; i++)
                    {
                        string strSort = myds__Goods_Class.Rows[i]["sort"].ToString();
                        string strstrClass123_ID = myds__Goods_Class.Rows[i]["ID"].ToString();
                        string ShowMenuName = myds__Goods_Class.Rows[i]["ClassName"].ToString();
                        string strA = strPub_Agent_Path + "/productclass.aspx?pclassgoodtype=" + (intClassGoodType + 1) + "&pclassid=" + strstrClass123_ID;
                        strReturn += "<div onclick=\"javascript:window.location.href='" + strA + "'\" class=\"ClassClickStyle\"><a href=\"" + strA + "\" class=\"Classname\">" + ShowMenuName;
                        strReturn += "<span class=\"More\">更多〉</span>";
                        strReturn += "</a></div>";

                        #region 获取本分类的商品
                        #region bll获取本分类的商品
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + Int_Session_CurUserID + " and IsDeleted=0 and Empowered=1");///有代理啊

                        string strWhereClass = "";
                        if (intClassGoodType == 0)///首页
                        {
                            strWhereClass = "and tab_Goods.Class1_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 1)///首页
                        {
                            strWhereClass = "and tab_Goods.Class2_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 2)///首页
                        {
                            strWhereClass = "and tab_Goods.Class3_ID=" + strstrClass123_ID + "";
                        }


                        System.Data.DataSet myds = null;
                        if (boolAgent)
                        {
                            myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, Int_Session_CurUserID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));
                            if (myds.Tables[0].Rows.Count == 0)
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }
                            //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                            //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + Int_Session_CurUserID + " and Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                        }
                        else
                        {
                            ///是不是别家的商品
                            ///

                            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(Int_Session_CurUserID);
                            if (pInt_QueryString_ParentID > 0) //是访问别人代理店铺；
                            {
                                myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pInt_QueryString_ParentID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));

                                //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                                //myds = bll_bll_View_Goods_Agent.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + pInt_QueryString_ParentID + " and  Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                if (myds.Tables[0].Rows.Count == 0)//父代理不存在了
                                {
                                    Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_DeleteCookies();
                                    EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                    myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                }
                            }
                            else
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }

                        }
                        #endregion
                        int intShowCount = myds.Tables[0].Rows.Count;


                        if (intShowCount > 0)
                        {
                            string GoodID = myds.Tables[0].Rows[0]["ID"].ToString();
                            string GoodName = myds.Tables[0].Rows[0]["Name"].ToString();
                            string GoodIconOOO = myds.Tables[0].Rows[0]["Icon"].ToString();
                            string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(GoodIconOOO, 200);


                            int SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                            string Price = myds.Tables[0].Rows[0]["Price"].ToString();
                            string PromotePrice = myds.Tables[0].Rows[0]["PromotePrice"].ToString();
                            string ShortInfo = myds.Tables[0].Rows[0]["ShortInfo"].ToString();
                            int intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(GoodID));
                            int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(GoodID));

                            strReturn += "<div class=\"classOneGood\" onclick=\"javascript:window.location.href='" + Pub_Agent_Path + "/product-" + GoodID + ".aspx'\">\n";
                            strReturn += " <div class=\"thisboard\">\n";
                            strReturn += "     <div class=\"box\">\n";
                            strReturn += "         <img alt=\"" + ShortInfo + "\" onload=\"AutoResizeImage(this)\" src=\"" + GoodIcon + "\" style=\"height: 110px; width: 110px; margin-left: 39px;\">\n";
                            strReturn += "     </div>\n";
                            strReturn += "     <div class=\"RightDataGoods\">\n";
                            strReturn += "      <div class=\"gameCenterthisBlock\">  <div class=\"gameCenter\">\n";
                            strReturn += "        <div class=\"RightDataGoods_GoodName\">\n";
                            strReturn += "             " + GoodName + "\n";
                            strReturn += "         </div>\n";
                            strReturn += "         </div></div>\n";
                            strReturn += "         <div class=\"RightDataGoods_Goodline01\">\n";
                            strReturn += "             <span class=\"RedPrice\">¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(PromotePrice)) + "\n";
                            strReturn += "            </span>\n";
                            strReturn += "           <span class=\"BlueSend\">" + Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(Decimal.Parse(PromotePrice), Decimal.Parse(Price)) + "折</span>\n";
                            strReturn += "            <span class=\"BlueSend\">\n";
                            strReturn += "            </span>\n";
                            strReturn += "           <span class=\"okImg\"></span>\n";
                            strReturn += "            <span class=\"okText\">" + intByHitCount + "</span>\n";
                            strReturn += "       </div>\n";
                            strReturn += "        <div class=\"RightDataGoods_Goodline01\">\n";
                            strReturn += "             <span class=\"oldPrice\">原价：¥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(Price)) + "\n";
                            strReturn += "           </span>\n";
                            strReturn += "             <span class=\"BtnBuyNow\">立即购买</span>\n";
                            strReturn += "         </div>\n";
                            strReturn += "     </div>\n";
                            strReturn += "   </div>\n";
                            strReturn += " </div>";
                        }

                        #endregion
                    }
                    strReturn += "        </div>\n";
                }
                else if (intPub_IntStyle_Model == 3)
                {
                    string ShopNavIconList = "<ul>";
                    string strGoodClassStyle4List = "";


                    //string _NewstrGoodBodyest = " <div class=\"NewStyle3jx_map_bd\">\n";
                    for (int i = 0; i < myds__Goods_Class.Rows.Count; i++)
                    {
                        string strSort = myds__Goods_Class.Rows[i]["sort"].ToString();
                        string strstrClass123_ID = myds__Goods_Class.Rows[i]["ID"].ToString();
                        string ShowMenuName = myds__Goods_Class.Rows[i]["ClassName"].ToString();
                        string strClassIcon = myds__Goods_Class.Rows[i]["ClassIcon"].ToString();
                        string strBigPicpath = myds__Goods_Class.Rows[i]["BigPicpath"].ToString();

                        string strClassA = strPub_Agent_Path + "/productclass.aspx?pclassgoodtype=" + (intClassGoodType + 1) + "&pclassid=" + strstrClass123_ID;
                        ShopNavIconList += "             <li><div onclick=\"javascript:window.location.href='" + strClassA + "'\" class=\"ShowNav\"><div class=\"ShowNavIconClass\"><img class=\"ShowNavIcon\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strClassIcon + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/></div><div class=\"ShowNavText\">" + ShowMenuName + "</div></div></li>\n";

                        strGoodClassStyle4List += "<div class=\"GoodClassStyle4_OneClass\">\n";
                        strGoodClassStyle4List += "<div onclick=\"javascript:window.location.href='" + strClassA + "'\" class=\"GoodClassStyle4Nav\">\n";
                        strGoodClassStyle4List += "                <span class=\"spanIcon\"><img src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strClassIcon + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/></span>\n";
                        strGoodClassStyle4List += "    <span class=\"spanTitleClassName\">" + ShowMenuName + "</span>\n";
                        strGoodClassStyle4List += " </div>\n";
                        strGoodClassStyle4List += "<div onclick=\"javascript:window.location.href='" + strClassA + "'\" class=\"GoodClassStyle4NavBigIcon\">\n";
                        strGoodClassStyle4List += "    <img src =\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strBigPicpath + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/>\n";
                        strGoodClassStyle4List += "</div>\n";
                        strGoodClassStyle4List += "<div class=\"GoodClassStyle4NavTwoGood\">\n";
                        strGoodClassStyle4List += "  <ul>\n";
                        #region bll获取本分类的2个商品
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + Int_Session_CurUserID + "  and IsDeleted=0 and Empowered=1");///有代理啊

                        string strWhereClass = "";
                        if (intClassGoodType == 0)///首页
                        {
                            strWhereClass = "and tab_Goods.Class1_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 1)///首页
                        {
                            strWhereClass = "and tab_Goods.Class2_ID=" + strstrClass123_ID + "";
                        }
                        else if (intClassGoodType == 2)///首页
                        {
                            strWhereClass = "and tab_Goods.Class3_ID=" + strstrClass123_ID + "";
                        }


                        System.Data.DataSet myds = null;
                        if (boolAgent)
                        {
                            myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, Int_Session_CurUserID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));
                            if (myds.Tables[0].Rows.Count == 0)
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }

                            //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                            //myds = bll_bll_View_Goods_Agent.GetList("top 2 ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + Int_Session_CurUserID + " and Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                        }
                        else
                        {
                            ///是不是别家的商品
                            ///

                            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(Int_Session_CurUserID);
                            if (pInt_QueryString_ParentID > 0) //是访问别人代理店铺；
                            {
                                myds = bll_tab_ShopClient_Agent_.SelectList(string.Format(strSQLView_Goods_Agent, pInt_QueryString_ParentID, intpShopClientID, " and tab_ShopClient_Agent__ProductClassID.Empowered=1 ", strWhereClass));

                                //EggsoftWX.BLL.View_Goods_Agent bll_bll_View_Goods_Agent = new EggsoftWX.BLL.View_Goods_Agent();
                                //myds = bll_bll_View_Goods_Agent.GetList("top 2 ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and UserID=" + pInt_QueryString_ParentID + " and  Empowered=1 and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                if (myds.Tables[0].Rows.Count == 0)//父代理不存在了
                                {
                                    Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_DeleteCookies();
                                    EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                    myds = bll_tab_Goods.GetList("top 2 ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                                }
                            }
                            else
                            {
                                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                myds = bll_tab_Goods.GetList("top 2 ID,Name,Icon,Price,PromotePrice,ShortInfo,sort", " ShopClient_ID=" + intpShopClientID + " and isSaled=1 and IsDeleted=0 and IS_Admin_check=1 " + strWhereClass + " order by sort asc,updatetime desc");
                            }
                        }
                        int intShowCount = myds.Tables[0].Rows.Count;
                        #endregion

                        #region 显示本分类的2个商品
                        for (int inti = 0; inti < (intShowCount > 2 ? 2 : intShowCount); inti++)
                        {
                            string str2GoodID = myds.Tables[0].Rows[inti]["ID"].ToString();
                            string str2GoodName = myds.Tables[0].Rows[inti]["Name"].ToString();
                            string str2GoodIconOOO = myds.Tables[0].Rows[inti]["Icon"].ToString();
                            string str2GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(str2GoodIconOOO, 400);


                            int int2SalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(str2GoodID));
                            string str2Price = myds.Tables[0].Rows[inti]["Price"].ToString();
                            string str2PromotePrice = myds.Tables[0].Rows[inti]["PromotePrice"].ToString();
                            string str2ShortInfo = myds.Tables[0].Rows[inti]["ShortInfo"].ToString();
                            int int2intSalesCount = Eggsoft_Public_CL.GoodP.BySalesCount(Int32.Parse(str2GoodID));
                            int int2ByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(str2GoodID));

                            string strGoodA = Pub_Agent_Path + "/product-" + str2GoodID + ".aspx";
                            strGoodClassStyle4List += "    <li>\n";
                            strGoodClassStyle4List += "       <div onclick=\"javascript:window.location.href='" + strGoodA + "'\" class=\"goodHotClass\">\n";
                            strGoodClassStyle4List += "           <div class=\"ImgParent\"><img onload=\"AutoResizeImage(this)\" onload=\"AutoResizeImage(this)\" src=\"" + str2GoodIcon + "\" onerror=\"javascript:this.src='/Templet/04StyleModel/Images/nopic_loading.gif'\"/></div>\n";
                            strGoodClassStyle4List += "          <div class=\"goodHotGoodName\">\n";
                            strGoodClassStyle4List += "             " + str2GoodName + "\n";
                            strGoodClassStyle4List += "         </div>\n";
                            strGoodClassStyle4List += "        <div class=\"goodHotGoodPriceAndBuy\">\n";
                            strGoodClassStyle4List += "            <div class=\"leftPrice\">\n";
                            strGoodClassStyle4List += "                 惊爆价<span class=\"Moneypay\">￥" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(str2PromotePrice)) + "</span><br />\n";
                            strGoodClassStyle4List += "                <span class=\"MarketMoney\">市场价 " + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(str2Price)) + "</span>\n";
                            strGoodClassStyle4List += "             </div>\n";
                            strGoodClassStyle4List += "              <div class=\"rightButton\">立即购买</div>\n";
                            strGoodClassStyle4List += "         </div>\n";
                            strGoodClassStyle4List += "       </div>\n";
                            strGoodClassStyle4List += "   </li>\n";
                        }
                        #endregion 显示本分类的2个商品             
                        strGoodClassStyle4List += "    </ul>\n";
                        strGoodClassStyle4List += "</div>\n";
                        strGoodClassStyle4List += "</div>\n";


                    }
                    ShopNavIconList += "</ul>";
                    //_NewstrGoodBodyest += "        </div>\n";

                    strReturn += ShopNavIconList + "######" + strGoodClassStyle4List;
                }
            }
            return strReturn;
        }

        public static string strGetMyAgentFooter(int intCurUserID, int intShopClient, string strPub_Agent_Path)
        {
            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();
            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClient);

            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/Html/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.ReadRemoteTempleToCacheKey_ShopClientID(strGetAppConfiugUplaod + STR_03Footer_html, intShopClient);
            string str_Pub_03Footer_html = strFooter.Replace("###SAgentPath###", strPub_Agent_Path);
            str_Pub_03Footer_html = str_Pub_03Footer_html.Replace("申请代理", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(intCurUserID));
            str_Pub_03Footer_html = str_Pub_03Footer_html.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(intCurUserID));


            return str_Pub_03Footer_html;
        }

        public static string Pub_ShowAgent_SubMix_Text(int int_Session_CurUserID)
        {
            //检查是否是代理
            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + "  and IsDeleted=0 and Empowered=1");///有代理啊
            string strText = "";
            if (boolAgent)
            {
                strText = "管理代理";
            }
            else
            {
                strText = "申请代理";
            }
            return strText;
        }

        public static string Pub_Agent_Path(int int_Session_CurUserID)
        {
            string strPub_Agent_Path = "";

            int intShareParnetID = 0;
            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + int_Session_CurUserID + "  and IsDeleted=0 and (Empowered=1 or OnlyIsAngel=1)");
            if (Model_tab_ShopClient_Agent_ != null)
            {

                if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                {
                    strPub_Agent_Path = "/sagent." + int_Session_CurUserID;///还是高级代理那
                }
                else
                {
                    strPub_Agent_Path = "/sagent-" + int_Session_CurUserID;
                }

            }
            else
            {
                intShareParnetID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(int_Session_CurUserID);
                if (intShareParnetID > 0)
                {
                    Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intShareParnetID + " and Empowered=1");
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                        {
                            strPub_Agent_Path = "/sagent." + intShareParnetID;///还是高级代理那
                        }
                        else
                        {
                            strPub_Agent_Path = "/sagent-" + intShareParnetID;
                        }
                    }
                }
                //strPub_Agent_Path = "/sagent-" + intShareParnetID;
            }
            return strPub_Agent_Path;
        }

        public static void GetParentID_Agent_From_Request_DeleteCookies()
        {
            String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();
            String strApplicationCheckName_PAID = strApplicationCheckName + "_PAID";
            Eggsoft.Common.Cookie.Delete(strApplicationCheckName, strApplicationCheckName_PAID);
        }

        /// <summary>
        /// 有代理资格的才能称为父亲  代理身份  是天使用 3  分销用5   高级代理用7 什么都不是用-1
        /// </summary>
        /// <param name="int_UserID"></param>
        /// <returns>是天使用 3  分销用5   高级代理用7  什么都不是用-1</returns>
        public static Int32 IF_Agent_From_Database_(int int_UserID)
        {
            Int32 gIF_Agent_From_Database = -1;
            EggsoftWX.BLL.View_ShopClient_Agent BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
            EggsoftWX.Model.View_ShopClient_Agent Model_View_ShopClient_Agent = BLL_View_ShopClient_Agent.GetModel("UserID=" + int_UserID);
            if (Model_View_ShopClient_Agent == null)
            {
                gIF_Agent_From_Database = -1;
            }
            else if (Model_View_ShopClient_Agent.OnlyIsAngel == true)
            {
                gIF_Agent_From_Database = 3;
            }
            else if (Model_View_ShopClient_Agent.AgentLevelSelect > 0 && Model_View_ShopClient_Agent.Empowered == true)
            {
                gIF_Agent_From_Database = 7;
            }
            else if (Model_View_ShopClient_Agent.Empowered == true)
            {
                gIF_Agent_From_Database = 5;
            }
            return gIF_Agent_From_Database;
        }
        /// <summary>
        /// 得到父ID
        /// </summary>
        /// <param name="int_UserID"></param>
        /// <returns></returns>
        public static Int32 GetGrandParentID_Agent_From_Database_(int int_UserID)
        {
            if (int_UserID == 0) return 0;
            EggsoftWX.BLL.View_ShopClient_Agent BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
            EggsoftWX.Model.View_ShopClient_Agent Model_View_ShopClient_Agent = BLL_View_ShopClient_Agent.GetModel("UserID=" + int_UserID);


            Int32 intGetGrandParentID_Agent_From_Database_ = 0;


            if (Model_View_ShopClient_Agent != null)
            {
                intGetGrandParentID_Agent_From_Database_ = Model_View_ShopClient_Agent.ParentID.toInt32();
            }
            return intGetGrandParentID_Agent_From_Database_;
        }

        /// <summary>
        /// 找到一个人的上级 先看代理 如果没有 就看useid表
        /// </summary>
        /// <param name="int_UserID"></param>
        /// <returns></returns>
        public static Int32 GetParentID_Agent_From_Database_AbountHistory(Int32 int_UserID)
        {
            if (int_UserID == 0) return 0;
            Int32 intParentID = GetGrandParentID_Agent_From_Database_(int_UserID);
            if (intParentID == int_UserID) intParentID = 0;
            if (intParentID == 0)
            {
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(int_UserID);
                if (Model_tab_User != null && Model_tab_User.ParentID > 0 && Model_tab_User.ParentID != int_UserID)
                {
                    intParentID = Model_tab_User.ParentID.toInt32();
                }
            }
            return intParentID;
        }


        public static Int32 GetParentID_Agent_From_Request_(int int_Session_CurUserID)
        {

            // 先要检查 1 parentagentadid  从网页地址来的   
            //          2 没有的话 读parentagentid   
            //          3 有没有已经存在的父亲  ///各种优先级设定

            var intGetParentID_Agent_From_Request_ = 0;
            try
            {
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(int_Session_CurUserID);
                if (Model_tab_User == null) return 0;
                String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();
                String strApplicationCheckName_PAID = strApplicationCheckName + "_PAID";
                if (HttpContext.Current != null)
                {
                    string strParentAgentADID = HttpContext.Current.Request["parentagentadid"];//是不是访问代理的网页；
                    int.TryParse(strParentAgentADID, out intGetParentID_Agent_From_Request_);
                }

                #region 访问的是其他代理的网页 并且我是代理
                int meAgent = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(int_Session_CurUserID);
                if (meAgent > 0) intGetParentID_Agent_From_Request_ = 0;
                #endregion


                int? intIFModifyParent = 0;///分销所得优先给予第一人还是给予最新的转发人

                if (intGetParentID_Agent_From_Request_ > 0)///代理的网页  是固定的
                {
                    Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName_PAID, intGetParentID_Agent_From_Request_.ToString());///记忆父Cookies
                    if ((Model_tab_User.ParentID == null) || (Model_tab_User.ParentID < 1))
                    {
                        if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intGetParentID_Agent_From_Request_.ToString()) == Model_tab_User.ShopClientID)
                        {///保证是同一个店铺的转发
                            if (intGetParentID_Agent_From_Request_ != int_Session_CurUserID)
                            {
                                Model_tab_User.ParentID = intGetParentID_Agent_From_Request_;
                                Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;
                                Model_tab_User.TeamID = Int32TeamID;

                                Model_tab_User.Updatetime = DateTime.Now;
                                BLL_tab_User.Update(Model_tab_User);



                                intIFModifyParent = Model_tab_User.ParentID;

                            }
                        }
                    }
                }
                else
                {
                    int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(int_Session_CurUserID.ToString());
                    EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                    EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + intShopClientID);
                    //bool bool_BuyMySelfIfGetMoney = Model_tab_ShopClient_ShopPar.BuyMySelfIfGetMoney.toBoolean();
                    //if (bool_BuyMySelfIfGetMoney)//自己购买商品是否享受分销提成  父亲可能是自己
                    //{
                    //    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    //    bool boolEmpowered = BLL_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + "  and IsDeleted=0 and (Empowered=1)");///上级代理可能会被取消权限
                    //   // bool boolEmpoweredOnlyIsAngel = BLL_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + " and (OnlyIsAngel=1)");///上级代理可能会被取消权限
                    //    if (boolEmpowered)
                    //    {
                    //        intGetParentID_Agent_From_Request_ = int_Session_CurUserID;
                    //    }
                    //    else
                    //    {

                    //        intGetParentID_Agent_From_Request_ = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(int_Session_CurUserID);

                    //    }
                    //}
                    //else
                    //{
                    intGetParentID_Agent_From_Request_ = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(int_Session_CurUserID);
                    //}
                    if (intGetParentID_Agent_From_Request_ < 1)
                    {


                        if (HttpContext.Current != null)
                        {
                            string strParentAgentID = HttpContext.Current.Request["parentagentid"];//是不是访问分销网页；
                            int.TryParse(strParentAgentID, out intGetParentID_Agent_From_Request_);
                            if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strParentAgentID) != intShopClientID) intGetParentID_Agent_From_Request_ = 0;///保证同源
                        }
                        if (Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(int_Session_CurUserID) > 0) intGetParentID_Agent_From_Request_ = 0;///本人是代理 没有上级
                    }
                    //分销所得优先给予第一人还是给予最新的转发人。（举例：A转发给B，2天后C也转发给B。然后B购买了商品，A的上线是A还是C？选择表示上线是A，不选择表示上线是C。）
                    //bool boolShareFirstManORLastMan = 
                    bool boolDoCheckIfPayMoney = Eggsoft_Public_CL.Pub_FenXiao.DoCheckIfPayedMoney(Model_tab_User.ShopClientID.toInt32(), int_Session_CurUserID);//.getp Eggsoft_Public_CL.Pub.boolShowPower(Model_tab_User.ShopClientID.ToString(), "ShareFirstManORLastMan");
                    if (intGetParentID_Agent_From_Request_ > 0)
                    {
                        Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName_PAID, intGetParentID_Agent_From_Request_.ToString());///记忆父Cookies
                        if (boolDoCheckIfPayMoney)
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            bool boolAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + "  and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID + " and isnull(Empowered,0)=1");///有代理啊
                                                                                                                                                                                                                      ///不是代理才改写
                            if (!boolAgent && (Model_tab_User.ParentID == null) || (Model_tab_User.ParentID < 1))///改写上家的资料 20150818  
                            {
                                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intGetParentID_Agent_From_Request_.ToString()) == Model_tab_User.ShopClientID)
                                {///保证是同一个店铺的转发
                                 ///
                                    if (intGetParentID_Agent_From_Request_ != int_Session_CurUserID)
                                    {
                                        Model_tab_User.ParentID = intGetParentID_Agent_From_Request_;
                                       
                                        Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                        if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                        Model_tab_User.UpdateBy = "支付过的用户有了上级";
                                        Model_tab_User.Updatetime = DateTime.Now;
                                        BLL_tab_User.Update(Model_tab_User);
                                        intIFModifyParent = Model_tab_User.ParentID;
                                    }
                                }
                            }
                        }
                        else////总是给最新的人
                        {
                            if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intGetParentID_Agent_From_Request_.ToString()) == Model_tab_User.ShopClientID)
                            {///保证是同一个店铺的转发
                                if (intGetParentID_Agent_From_Request_ != int_Session_CurUserID)
                                {
                                    Model_tab_User.ParentID = intGetParentID_Agent_From_Request_;
                                    Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    Model_tab_User.UpdateBy = "没有支付过的用户有了新上级";
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    BLL_tab_User.Update(Model_tab_User);
                                    intIFModifyParent = Model_tab_User.ParentID;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Model_tab_User.ParentID > 0)///优先级高
                        {
                            intGetParentID_Agent_From_Request_ = Convert.ToInt32(Model_tab_User.ParentID);
                        }
                        else
                        {///看 cookies的
                            String strAgent_PAID = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName_PAID);//sesion 不能太长
                            int.TryParse(strAgent_PAID, out intGetParentID_Agent_From_Request_);///看看session  是否存在

                            if ((intGetParentID_Agent_From_Request_ > 0) && (Model_tab_User.ParentID == 0))
                            {

                                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                                bool boolAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + "  and IsDeleted=0 and ShopClientID=" + Model_tab_User.ShopClientID + " and isnull(Empowered,0)=1");///有代理啊
                                                                                                                                                                                                                          ///不是代理才改写
                                if (!boolAgent && Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intGetParentID_Agent_From_Request_.ToString()) == Model_tab_User.ShopClientID)
                                {///保证是同一个店铺的转发
                                    if (intGetParentID_Agent_From_Request_ != int_Session_CurUserID)
                                    {
                                        Model_tab_User.ParentID = intGetParentID_Agent_From_Request_;

                                        Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                        if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;
                                      
                                        BLL_tab_User.Update(Model_tab_User);


                                        intIFModifyParent = Model_tab_User.ParentID;
                                    }
                                }
                            }
                        }                                                     /// }
                    }//要是数据库的代理分销表 有这个，其他地方就别判定了
                }
                if (intGetParentID_Agent_From_Request_ > 0)//有可能上家取消代理资格聊
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolEmpowered = BLL_tab_ShopClient_Agent_.Exists("UserID=" + intGetParentID_Agent_From_Request_ + "  and IsDeleted=0 and (Empowered=1 or OnlyIsAngel=1)");///上级代理可能会被取消权限
                    if (boolEmpowered == false) intGetParentID_Agent_From_Request_ = 0;
                    //Eggsoft.Common.debug_Log.Call_WriteLog("intGetParentID_Agent_From_Request_2217=" + intGetParentID_Agent_From_Request_);
                }

                #region 设置天使权限
                int intShopClientIDGet = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(int_Session_CurUserID.ToString());
                bool boolEveryOneAutoAgentOnlyIsAngel = Eggsoft_Public_CL.Pub.boolShowPower(intShopClientIDGet.toString(), "EveryOneAutoAgentOnlyIsAngel");
                if (boolEveryOneAutoAgentOnlyIsAngel)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_ myBLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    if (myBLL_tab_ShopClient_Agent_.Exists("UserID=" + int_Session_CurUserID + " and IsDeleted=0 ") == false)
                    {
                        Model_tab_User = BLL_tab_User.GetModel("ID=" + int_Session_CurUserID + " and (Subscribe=1 or Api_Authorize=1)");///只有关注过或者授权过的才给予天使资格
                        if (Model_tab_User != null)///是粉丝才给权限 过客不给
                        {///给予天使资格
                            System.Threading.Tasks.Task.Factory.StartNew(() =>
                            {
                                Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(int_Session_CurUserID, true);
                            });

                            intGetParentID_Agent_From_Request_ = int_Session_CurUserID;///开启天使功能
                        }
                    }
                    else
                    {
                        intGetParentID_Agent_From_Request_ = int_Session_CurUserID;///开启天使功能
                    }
                }
                #endregion 设置天使权限


                if (intIFModifyParent > 0)
                {
                    #region 增加直推未处理信息

                    if (Model_tab_User.ParentID > 0)
                    {
                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "代理的网页是固定的";
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "改写上级打开代理的网页";
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = intIFModifyParent;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intIFModifyParent.toString()); ;
                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                        Model_b011_InfoAlertMessage.TypeTableID = intIFModifyParent;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    }
                    #endregion 增加直推未处理信息  

                    #region 增加间推未处理信息
                    if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent.toInt32()) > 0)
                    {
                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "代理的网页是固定的";
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "改写上上级打开代理的网页";
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent.toInt32());
                        Model_b011_InfoAlertMessage.ShopClient_ID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intIFModifyParent.toString());
                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                        Model_b011_InfoAlertMessage.TypeTableID = intIFModifyParent;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    }
                    #endregion 增加直推未处理信息  
                }
                #region 加入被转发人的运营中心数据
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(Model_tab_User.ShopClientID.toInt32()))
                    {
                        Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, Model_tab_User.ShopClientID.toInt32(), Model_tab_User.ParentID.toInt32());
                    }
                });
                #endregion 初始化所有运营中心数据

            }
            catch (Exception Exceptione)
            {

                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "页面获取上级", "程序报错 int_Session_CurUserID=" + int_Session_CurUserID);
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "页面获取上级");
            }
            finally
            {

            }



            return intGetParentID_Agent_From_Request_;
        }
        public static string GetAgentShopName_From_Visit__(int pInt_Session_CurUserID, int intShopClientID)
        {
            string strShopName = "";
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = new EggsoftWX.Model.tab_ShopClient_Agent_();


            string strParentAgentID = HttpContext.Current.Request["parentagentid"];//是不是访问别人网页；

            ///先识别身份。  总是有用的 任何一页都是如此啊
            //判断自己是不是代理
            Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pInt_Session_CurUserID + "  and IsDeleted=0 and (Empowered=1 or OnlyIsAngel=1)");//存在并得到授权
            if (Model_tab_ShopClient_Agent_ != null)//自己就是代理
            {
                strShopName = Model_tab_ShopClient_Agent_.ShopName;              //  httpc
            }
            else if (String.IsNullOrEmpty(strParentAgentID) == false) //是访问别人代理店铺；
            {
                Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strParentAgentID + " and (Empowered=1 or OnlyIsAngel=1)");
                if (Model_tab_ShopClient_Agent_ != null) strShopName = Model_tab_ShopClient_Agent_.ShopName;              //  httpc
            }
            else
            {
                int intParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pInt_Session_CurUserID);//扫描证书时  只有数据库中 有记录
                if (intParentID > 0)
                {
                    Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intParentID + " and (Empowered=1 or OnlyIsAngel=1)");
                    if (Model_tab_ShopClient_Agent_ != null) strShopName = Model_tab_ShopClient_Agent_.ShopName;
                }
                else //排除以上2者  那就显示店铺的真实店铺名字
                {
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

                    Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + intShopClientID);
                    strShopName = Model_tab_ShopClient.ShopClientName;              //  httpc
                }
            }

            return strShopName;
        }



        //asp.net读取模板生成HTML
        /// <summary>
        /// 根据域名确定当前的商户ID
        /// </summary>
        /// <param name="strCheckErJiYuMing">可选参数  传表示检查那个字段  PC端检查的和微信端检查的不一样</param>
        /// <returns></returns>
        public static string GetShopClientID_ErJiYuMing(String strCheckErJiYuMing = "ErJiYuMing")
        {
            string strShopClientID = "0";

            //string CacheKey = "GetShopClientID_ErJiYuMing4999674";
            //object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            //if (objType == null)
            //{
            try
            {
                string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();

                if (strgetwebHttp.ToLower().Contains("https://"))
                {
                    strgetwebHttp = strgetwebHttp.Remove(0, 8);
                }
                else if (strgetwebHttp.ToLower().Contains("http://"))
                {
                    strgetwebHttp = strgetwebHttp.Remove(0, 7);
                }
                strgetwebHttp = strgetwebHttp.ToLower();


                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(strCheckErJiYuMing + "='" + strgetwebHttp + "'");

                String strLocalHostDebug_ShopClientID_LocalHostDebug = ConfigurationManager.AppSettings["LocalHostDebug_ShopClientID_LocalHostDebug"];
                int int_ShopClientID = 0;
                int.TryParse(strLocalHostDebug_ShopClientID_LocalHostDebug, out int_ShopClientID);
                if (int_ShopClientID > 0)
                {
                    strShopClientID = strLocalHostDebug_ShopClientID_LocalHostDebug;
                }
                else if (Model_tab_ShopClient != null)
                {
                    strShopClientID = Model_tab_ShopClient.ID.ToString();
                }
                else//本地访问
                {
                    if ((strgetwebHttp.IndexOf("58970") > -1))
                    {
                        strShopClientID = "1";///仅仅是为了本地方便调试 时仪电子的号  
                        //strShopClientID = "2";///仅仅是为了本地方便调试 naan的号  
                        strShopClientID = "7";///王录的 o2o
                    }
                    else if ((strgetwebHttp.IndexOf(("csoliver.eggsoft.cn").ToLower()) > -1))
                    {
                        strShopClientID = "2";///仅仅是为了本地方便调试 时仪电子的号  
                        //strShopClientID = "2";///仅仅是为了本地方便调试 naan的号  
                        //strShopClientID = "19";///优生活网
                    }
                    else if ((strgetwebHttp.IndexOf(("192.168.0.6:8001").ToLower()) > -1))
                    {
                        strShopClientID = "1";///仅仅是为了本地方便调试 时仪电子的号  
                        //strShopClientID = "2";///仅仅是为了本地方便调试 naan的号  
                        strShopClientID = "5";///王录的 o2o
                    }
                    Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID本地访问=" + strShopClientID);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return strShopClientID;
        }

        /// <summary>
        /// 线下录单 可以调用这个修改上级
        /// </summary>
        /// <param name="intUserID"></param>
        /// <param name="intParentID"></param>
        /// <param name="intShopClient_ID"></param>
        /// <returns></returns>
        public static bool modifyUserParent(int intUserID, int intParentID, int intShopClient_ID)
        {
            #region 判断上级是否存在

            EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User ParentModel_tab_User = bll_tab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", intParentID, intShopClient_ID);
            if (ParentModel_tab_User == null) return false;
            #endregion 判断上级是否存在

            EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", intUserID, intShopClient_ID);
            if (Model_tab_User != null)
            {
                if (Model_tab_User.ParentID != intParentID)
                {
                    Model_tab_User.ParentID = intParentID;
                    //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                    Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                    if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                    Model_tab_User.Updatetime = DateTime.Now;
                    bll_tab_User.Update(Model_tab_User);


                    #region 增加直推未处理信息
                    if (Model_tab_User.ParentID > 0)
                    {
                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "改写上级";
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_User.ParentID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                        Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    }
                    #endregion 增加直推未处理信息  

                    #region 增加间推未处理信息  
                    if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32()) > 0)
                    {
                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = "改写上上级";
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(Model_tab_User.ParentID.toInt32());
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                        Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    }
                    #endregion 增加直推未处理信息  
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 线下录单 可以调用这个修改上级
        /// </summary>
        /// <param name="intUserID"></param>
        /// <param name="intParentID"></param>
        /// <param name="intShopClient_ID"></param>
        /// <returns></returns>
        public static bool modifyUserAgentParent(int intUserID, int intParentID, int intShopClient_ID)
        {
            #region 判断上级是否存在

            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ ParentModel_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=@UserID  and IsDeleted=0 and ShopClientID=@ShopClientID", intParentID, intShopClient_ID);
            if (ParentModel_tab_ShopClient_Agent_ == null) return false;
            #endregion 判断上级是否存在


            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", intUserID, intShopClient_ID);
            if (Model_tab_ShopClient_Agent_ != null)
            {
                if (Model_tab_ShopClient_Agent_.ParentID != intParentID)
                {
                    Model_tab_ShopClient_Agent_.ParentID = intParentID;
                    Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                    bll_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}