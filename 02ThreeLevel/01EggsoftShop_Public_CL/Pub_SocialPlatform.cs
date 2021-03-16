using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;



namespace Eggsoft_Public_CL
{

    //public class Pub_Default_Status_User_AllLeader
    //{
    //    private ArrayList pub_ArrayList = null;
    //    private Int32 IDAllFenXiaoMoney_my_AND_myAllParent = 0;
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="intShopClient">0  表示 所有的  有传值表示 指定的</param>

    //    public Pub_Default_Status_User_AllLeader(int intShopClient = 0)
    //    {

    //        try
    //        {
    //            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();


    //            pub_ArrayList = new ArrayList();
    //            string strSQL = "delete from tab_ShopClient_Status_User_AllLeader";
    //            if (intShopClient > 0) strSQL += " where ShopClientID=" + intShopClient;
    //            pub_ArrayList.Add(strSQL);

    //            IDAllFenXiaoMoney_my_AND_myAllParent = 0;
    //            if (intShopClient > 0)
    //            {
    //                doThisShopClient(intShopClient);
    //            }
    //            else
    //            {
    //                System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "1=1 order by id").Tables[0];////仅仅测试使用
    //                for (int ppppp = 0; ppppp < DataTable_tab_ShopClient.Rows.Count; ppppp++)
    //                {
    //                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");

    //                    string strShopClientID = DataTable_tab_ShopClient.Rows[ppppp]["ID"].ToString();
    //                    doThisShopClient(Int32.Parse(strShopClientID));
    //                }
    //            }
    //            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(pub_ArrayList);

    //        }
    //        catch (Exception ex)
    //        {
    //            Eggsoft.Common.debug_Log.Call_WriteLog(ex);
    //        }
    //        finally
    //        {

    //        }

    //        //
    //        //TODO: 在此处添加构造函数逻辑
    //        //
    //    }


    //    private void doThisShopClient(int intShopClient)
    //    {
    //        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
    //        EggsoftWX.BLL.View_ShopClient_Agent private_BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();


    //        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
    //        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient);
    //        if (Model_tab_ShopClient.AuthorTime < DateTime.Now) return;////停止服务

    //        System.Data.DataTable DataTable_tab_User = BLL_tab_User.GetList("ID,ParentID", "ShopClientID=" + intShopClient + " order by id asc").Tables[0];
    //        for (int ooooo = 0; ooooo < DataTable_tab_User.Rows.Count; ooooo++)
    //        {
    //            string strUserID = DataTable_tab_User.Rows[ooooo]["ID"].ToString();
    //            ArrayList myAgentIntArrayList = new ArrayList();
    //            string strUserMe = strUserID;

    //            #region 一个人可以加自己的销量 但是不能是自己的父亲 如果本人是代理 加本人一个销量
    //            //string strSQLString = String.Format(strAdd, strUserID);

    //            //Eggsoft.Common.debug_Log.Call_WriteLog("commandText=" + strSQLString);
    //            //System.Threading.Thread.Sleep(100);
    //            //bool boooAgent = BLL_tab_User.SelectList(strSQLString).Tables[0].Rows.Count > 0;

    //            bool boooAgent = private_BLL_View_ShopClient_Agent.Exists("UserID=" + strUserID + " and Empowered=1 and ShopClientID=" + intShopClient);
    //            if (boooAgent)
    //            {
    //                IDAllFenXiaoMoney_my_AND_myAllParent++;
    //                string strInsertSQL = "insert into tab_ShopClient_Status_User_AllLeader (id,UserID,LeaderUserID,UserIDByLeader,ShopClientID)  values (" + IDAllFenXiaoMoney_my_AND_myAllParent + "," + strUserMe + "," + strUserID + "," + strUserID + "," + intShopClient + ")";
    //                pub_ArrayList.Add(strInsertSQL);
    //            }
    //            #endregion




    //            string strParentID = DataTable_tab_User.Rows[ooooo]["ParentID"].ToString();
    //            int intParentID = 0;
    //            int.TryParse(strParentID, out intParentID);
    //            if (intParentID == 0)
    //            {
    //                //System.Data.DataTable DataTable__View_ShopClient_AgentTemp = BLL_tab_User.SelectList(String.Format(strAdd, strUserID)).Tables[0];
    //                System.Data.DataTable DataTable__View_ShopClient_AgentTemp = private_BLL_View_ShopClient_Agent.GetList("ParentID", "UserID=" + strUserID + " and ShopClientID=" + intShopClient + " order by id").Tables[0];

    //                //System.Data.DataTable DataTable__View_ShopClient_AgentTemp = private_BLL_View_ShopClient_Agent.GetList("ParentID", "UserID=" + strUserID + " and ShopClientID=" + intShopClient + " order by id").Tables[0];
    //                if (DataTable__View_ShopClient_AgentTemp.Rows.Count > 0)
    //                {
    //                    strParentID = DataTable__View_ShopClient_AgentTemp.Rows[0]["ParentID"].ToString();
    //                    int.TryParse(strParentID, out intParentID);


    //                }
    //            }


    //            while (intParentID > 0)///找出本人的所有父系  为他们 加分
    //            {
    //                if (myAgentIntArrayList.Contains(intParentID))
    //                {
    //                    break; ///防止死循环
    //                }
    //                myAgentIntArrayList.Add(intParentID);
    //                IDAllFenXiaoMoney_my_AND_myAllParent++;
    //                string strInsertSQL = "insert into tab_ShopClient_Status_User_AllLeader (id,UserID,LeaderUserID,UserIDByLeader,ShopClientID)  values (" + IDAllFenXiaoMoney_my_AND_myAllParent + "," + strUserMe + "," + intParentID + "," + strUserID + "," + intShopClient + ")";
    //                pub_ArrayList.Add(strInsertSQL);
    //                try
    //                {
    //                    System.Data.DataTable DataTable__View_ShopClient_Agent = private_BLL_View_ShopClient_Agent.GetList("ParentID", "UserID=" + intParentID + " and ShopClientID=" + intShopClient + " order by id").Tables[0];

    //                    //System.Data.DataTable DataTable__View_ShopClient_Agent = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataTable(String.Format(strAdd, intParentID));
    //                    strUserID = intParentID.ToString();///记录现在的子
    //                    intParentID = 0;
    //                    if (DataTable__View_ShopClient_Agent.Rows.Count > 0)
    //                    {
    //                        strParentID = DataTable__View_ShopClient_Agent.Rows[0]["ParentID"].ToString();
    //                        int.TryParse(strParentID, out intParentID);
    //                    }
    //                }
    //                catch (Exception eeee)
    //                {
    //                    Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "找出本人的所有父系程序报错");
    //                }
    //                finally
    //                {

    //                }
    //            }
    //        }

    //    }
    //}

    //public class Pub_Default_DoOrderShow
    //{

    //    private EggsoftWX.BLL.tab_ShopClient_Agent_ private_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
    //    private EggsoftWX.BLL.View_ShopClient_Agent private_BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();

    //    private int IDAllFenXiaoMoney_my_AND_myAllSon = 0;
    //    private ArrayList myGetSQLMySonMoneyArrayList = new ArrayList();
    //    private ArrayList pub_ArrayList = null;

    //    private ArrayList myStaticsMeAndMySonSelfPage_Load_DoArrayList = new ArrayList();///防止递归循环


    //    public Pub_Default_DoOrderShow(int intShopClient = 0)
    //    {

    //        try
    //        {
    //            pub_ArrayList = new ArrayList();
    //            string strSQL = "delete from tab_ShopClient_Agent_AllFenXiaoMoney_my_AND_myAllSon";
    //            pub_ArrayList.Add(strSQL);

    //            string strDelete = "delete from tab_tab_ShopClient_myGetFenXiaoMoneyHistory";
    //            myGetSQLMySonMoneyArrayList.Add(strDelete);


    //            if (intShopClient > 0)
    //            {
    //                StaticsMySelfPage_Load(intShopClient);//先循环每个会员的分销收入  把订单中 有我的  都重新猎取一下 。并 放到 tab_ShopClient_Agent_ AllFenXiaoMoney
    //                StaticsMeAndMySonSelfPage_Load_Do(intShopClient);///递归循环  把所有分销的钱 加给他的上线 放到  tab_ShopClient_Agent_AllFenXiaoMoney_my_AND_myAllSon

    //            }
    //            else
    //            {
    //                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
    //                //System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "1=1 order by id").Tables[0];
    //                System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "1=1 order by id").Tables[0];////仅仅测试使用
    //                for (int ppppp = 0; ppppp < DataTable_tab_ShopClient.Rows.Count; ppppp++)
    //                {
    //                    //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");

    //                    string strShopClientID = DataTable_tab_ShopClient.Rows[ppppp]["ID"].ToString();
    //                    int intstrShopClientID = Int32.Parse(strShopClientID);
    //                    Eggsoft.Common.debug_Log.Call_WriteLog("mmPub_Default_DoOrderShow开始执行 现在是客户intstrShopClientID=" + intstrShopClientID, "每天更新", "客户intstrShopClientID = " + intstrShopClientID);



    //                    StaticsMySelfPage_Load(intstrShopClientID);//先循环每个会员的分销收入  把订单中 有我的  都重新猎取一下 。并 放到 tab_ShopClient_Agent_ AllFenXiaoMoney
    //                    StaticsMeAndMySonSelfPage_Load_Do(intstrShopClientID);///递归循环  把所有分销的钱 加给他的上线 放到  tab_ShopClient_Agent_AllFenXiaoMoney_my_AND_myAllSon
    //                }

    //            }



    //            for (int n = 0; n < pub_ArrayList.Count; n++)
    //            {
    //                string strsql = pub_ArrayList[n].ToString();
    //            }
    //            for (int n = 0; n < myGetSQLMySonMoneyArrayList.Count; n++)
    //            {
    //                string strsql = myGetSQLMySonMoneyArrayList[n].ToString();
    //            }
    //            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(pub_ArrayList);
    //            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(myGetSQLMySonMoneyArrayList);

    //            Eggsoft.Common.debug_Log.Call_WriteLog("Default_DoOrderShow", "每天更新", "Default_DoOrderShow");
    //        }
    //        catch (Exception ex)
    //        {
    //            Eggsoft.Common.debug_Log.Call_WriteLog(ex);
    //        }
    //        finally
    //        {

    //        }

    //        //
    //        //TODO: 在此处添加构造函数逻辑
    //        //
    //    }


    //    /// <summary>
    //    /// 递归循环  把所有分销的钱 加给他的上线
    //    /// </summary>
    //    /// <param name="intShopClientID"></param>
    //    protected void StaticsMeAndMySonSelfPage_Load_Do(int intShopClientID)
    //    {
    //        int intMuSonSum = 0;


    //        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
    //        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();

    //        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
    //        if (Model_tab_ShopClient.AuthorTime < DateTime.Now) return;////停止服务

    //        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
    //        EggsoftWX.BLL.View_ShopClient_Agent BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();


    //        System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "id=" + intShopClientID + " order by id").Tables[0];
    //        for (int ppppp = 0; ppppp < DataTable_tab_ShopClient.Rows.Count; ppppp++)
    //        {
    //            string strShopClientID = DataTable_tab_ShopClient.Rows[ppppp]["ID"].ToString();
    //            System.Data.DataTable DataTable_Agent = BLL_View_ShopClient_Agent.GetList("UserID", "ShopClientID=" + strShopClientID + " order by id").Tables[0];
    //            for (int oooo = 0; oooo < DataTable_Agent.Rows.Count; oooo++)
    //            {
    //                string strintUserID = DataTable_Agent.Rows[oooo]["UserID"].ToString();
    //                myStaticsMeAndMySonSelfPage_Load_DoArrayList = new ArrayList();///防止递归循环
    //                Decimal allNeedShowMoney = StaticsDiGuiAllMySinFromMySelfPage_Load(Int32.Parse(strintUserID), out intMuSonSum, intShopClientID);
    //            }
    //        }


    //        #region print errlog

    //        #endregion

    //    }

    //    /// <summary>
    //    /// 递归循环  把所有分销的钱 加给他的上线
    //    /// </summary>
    //    /// <param name="intargUserID"></param>
    //    /// <param name="intOutMuSonSum"></param>
    //    /// <param name="intShopClientID"></param>
    //    /// <returns></returns>
    //    protected Decimal StaticsDiGuiAllMySinFromMySelfPage_Load(int intargUserID, out int intOutMuSonSum, int intShopClientID)
    //    {

    //        #region 防止递归循环
    //        if (myStaticsMeAndMySonSelfPage_Load_DoArrayList.Contains(intargUserID))
    //        {
    //            intOutMuSonSum = 0;
    //            return 0;
    //        }
    //        else
    //        {
    //            myStaticsMeAndMySonSelfPage_Load_DoArrayList.Add(intargUserID);
    //        }
    //        #endregion


    //        System.Data.DataTable DataTable__ShopClient_Agent = private_BLL_View_ShopClient_Agent.GetList("ID,UserID", "ParentID=" + intargUserID + " and ShopClientID=" + intShopClientID + " order by id").Tables[0];
    //        int intLength = DataTable__ShopClient_Agent.Rows.Count;

    //        if (intLength > 0)
    //        {
    //            Decimal mySonDecimal = 0;
    //            int intForMySonSum = 0;
    //            for (int iiii = 0; iiii < intLength; iiii++)
    //            {
    //                string intID = DataTable__ShopClient_Agent.Rows[iiii]["ID"].ToString();
    //                string strintUserID = DataTable__ShopClient_Agent.Rows[iiii]["UserID"].ToString();
    //                int intUserID = 0;
    //                int.TryParse(strintUserID, out intUserID);
    //                if ((intUserID > 0) && (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strintUserID) == intShopClientID))
    //                {
    //                    int intCurMuSonSum = 0;
    //                    mySonDecimal += StaticsDiGuiAllMySinFromMySelfPage_Load(intUserID, out intCurMuSonSum, intShopClientID);
    //                    intForMySonSum += intCurMuSonSum;

    //                    IDAllFenXiaoMoney_my_AND_myAllSon++;
    //                    string strInsertSQL = "insert into tab_ShopClient_Agent_AllFenXiaoMoney_my_AND_myAllSon (id,UpdateTime,MoneyHistoryFrom,GetMoney,UserID,ShopClientID)  values (" + IDAllFenXiaoMoney_my_AND_myAllSon + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + intCurMuSonSum + "'," + mySonDecimal + "," + intUserID + "," + intShopClientID + ")";
    //                    pub_ArrayList.Add(strInsertSQL);

    //                }
    //            }
    //            #region 本人的也要处理
    //            Decimal DecimalMeAndMySonMoney = 0;
    //            if (intargUserID != 0)
    //            {//处理本人的
    //                System.Data.DataTable DataTable__ShopClient_Agent_CurThis = private_BLL_tab_ShopClient_Agent_.GetList("ID,UserID,AllFenXiaoMoney", "UserID=" + intargUserID + " order by id").Tables[0];
    //                string strAllFenXiaoMoney = DataTable__ShopClient_Agent_CurThis.Rows[0]["AllFenXiaoMoney"].ToString();
    //                Decimal myOutDecimal = 0;
    //                Decimal.TryParse(strAllFenXiaoMoney, out myOutDecimal);
    //                intOutMuSonSum = intForMySonSum + 1;
    //                DecimalMeAndMySonMoney = myOutDecimal + mySonDecimal;
    //                string strSQL = "update tab_ShopClient_Agent_ set AllFenXiaoMoney_my_AND_myAllSon=" + DecimalMeAndMySonMoney + ",myAgentSonSum=" + intOutMuSonSum + "  where UserID=" + intargUserID;
    //                pub_ArrayList.Add(strSQL);
    //            }
    //            else
    //            {
    //                intOutMuSonSum = intForMySonSum;
    //                DecimalMeAndMySonMoney = mySonDecimal;
    //            }
    //            #endregion

    //            return DecimalMeAndMySonMoney;
    //        }
    //        else//没有下级了
    //        {
    //            ///本人 是最低级
    //            ///
    //            System.Data.DataTable DataTable__ShopClient_Agent_CurThis = private_BLL_tab_ShopClient_Agent_.GetList("ID,UserID,AllFenXiaoMoney", "UserID=" + intargUserID + " order by id").Tables[0];
    //            string strAllFenXiaoMoney = DataTable__ShopClient_Agent_CurThis.Rows[0]["AllFenXiaoMoney"].ToString();
    //            Decimal myOutDecimal = 0;
    //            Decimal.TryParse(strAllFenXiaoMoney, out myOutDecimal);
    //            string strSQL = "update tab_ShopClient_Agent_ set AllFenXiaoMoney_my_AND_myAllSon=" + myOutDecimal + ",myAgentSonSum=1  where UserID=" + intargUserID;

    //            IDAllFenXiaoMoney_my_AND_myAllSon++;
    //            string strInsertSQL = "insert into tab_ShopClient_Agent_AllFenXiaoMoney_my_AND_myAllSon (id,UpdateTime,MoneyHistoryFrom,GetMoney,UserID,ShopClientID)  values (" + IDAllFenXiaoMoney_my_AND_myAllSon + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','1'," + myOutDecimal + "," + intargUserID + "," + intShopClientID + ")";
    //            pub_ArrayList.Add(strInsertSQL);

    //            pub_ArrayList.Add(strSQL);
    //            intOutMuSonSum = 1;
    //            return myOutDecimal;
    //        }



    //    }

    //    /// <summary>
    //    /// 先循环每个会员的分销收入  把订单中 有我的  都重新猎取一下 。并 放到 tab_ShopClient_Agent_ AllFenXiaoMoney
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    protected void StaticsMySelfPage_Load(int intShopClientID)
    //    {
    //        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
    //        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
    //        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();

    //        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
    //        if (Model_tab_ShopClient.AuthorTime < DateTime.Now) return;////停止服务

    //        ArrayList myStringArrayList = new ArrayList();


    //        System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "id=" + intShopClientID + " order by id").Tables[0];
    //        for (int ppppp = 0; ppppp < DataTable_tab_ShopClient.Rows.Count; ppppp++)
    //        {
    //            string strShopClientID = DataTable_tab_ShopClient.Rows[ppppp]["ID"].ToString();


    //            System.Data.DataTable DataTable_Agent = private_BLL_View_ShopClient_Agent.GetList("UserID", "ShopClientID=" + strShopClientID + " order by id").Tables[0];
    //            for (int oooo = 0; oooo < DataTable_Agent.Rows.Count; oooo++)
    //            {
    //                string strintUserID = DataTable_Agent.Rows[oooo]["UserID"].ToString();
    //                int intUserID = 0;
    //                int.TryParse(strintUserID, out intUserID);
    //                Decimal mySonemoney = 0;
    //                Eggsoft_Public_CL.Pub_FenXiao.GetCountMySonMoney(intUserID, out mySonemoney, myGetSQLMySonMoneyArrayList, true);
    //                myStringArrayList.Add("update tab_ShopClient_Agent_ set AllFenXiaoMoney=" + mySonemoney + ",AllFenXiaoMoney_Updatetime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where UserID=" + intUserID);
    //            }
    //        }
    //        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(myStringArrayList);
    //    }
    //}

    public class Pub_Default_DoYunYingZhongXin28EveryDay
    {
        public Pub_Default_DoYunYingZhongXin28EveryDay(int intShopClient = 0)
        {
            try
            {

                if (intShopClient > 0)
                {
                    doYunYinZhongXin28Action(intShopClient);
                }
                else
                {
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    System.Data.DataTable DataTable_tab_ShopClient = BLL_tab_ShopClient.GetList("ID", "1=1 order by id").Tables[0];////仅仅测试使用
                    for (int ppppp = 0; ppppp < DataTable_tab_ShopClient.Rows.Count; ppppp++)
                    {
                        string strShopClientID = DataTable_tab_ShopClient.Rows[ppppp]["ID"].ToString();
                        doYunYinZhongXin28Action(strShopClientID.toInt32());
                    }

                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "每天运营中心加权分红开始执行");
            }
        }



        private void doYunYinZhongXin28Action(int intShopClient)
        {
            string strFilePath = "~/File/" + intShopClient.toString() + "do" + DateTime.Now.ToString("yyyyMMdd") + "YunYinZhongXin28Action.txt";

            ///string strFilePath = "~/File/" +DateTime.Now.ToString("yyyyMMdd")+ intShopClient.toString() + "doYunYinZhongXin28Action.txt";
            StringBuilder builderFilePath = new StringBuilder(DateTime.Now.ToLongTimeString() + "分红");


            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient);
            if (Model_tab_ShopClient.AuthorTime < DateTime.Now) return;////停止服务
            if (!Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(intShopClient)) return;////停止服务不需要运营中心的

            ////按照运营商品的情况进行分红。每个商品都不相同 可以多个 。每个商品都有自己的分红方式
            string ThisDay = DateTime.Now.ToString("yyyy-MM-dd");
            EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay my_BLL_b007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney BLL_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney = new EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney();
            System.Data.DataTable DataOperationGoodsDataTable = BLL_b004_OperationGoods.GetList("HowToReturnMoneyA,HowToReturnMoneyB,ID,GoodID,ReturnConsumerWealth", "ShopClient_ID=@ShopClient_ID and IsDeleted=0 and RunningStatus=1", intShopClient).Tables[0];
            #region 截至分红 0.2扣税使用
            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.BLL.tab_User_Message_NeedShow BLL_tab_User_Message_NeedShow = new EggsoftWX.BLL.tab_User_Message_NeedShow();
            EggsoftWX.Model.tab_User_Message_NeedShow Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();


            EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();

            #endregion 截至分红 0.2扣税使用
            for (int j = 0; j < DataOperationGoodsDataTable.Rows.Count; j++)
            {


                #region 开始这款商品的分红
                #region 截至分红 0.2扣税使用
                string strGoodID = DataOperationGoodsDataTable.Rows[j]["GoodID"].toString();
                string strReturnConsumerWealth = DataOperationGoodsDataTable.Rows[j]["ReturnConsumerWealth"].toString();
                Model_tab_Goods = BLL_tab_Goods.GetModel("ID=" + strGoodID + " and ShopClient_ID=" + intShopClient);
                Decimal UnitDecimal_5994_20171021 = Model_tab_Goods.PromotePrice.toDecimal() * strReturnConsumerWealth.toDecimal();
                #endregion 截至分红 0.2扣税使用

                string strOperationGoodsIDID = DataOperationGoodsDataTable.Rows[j]["ID"].toString();
                Decimal DecimalHowToReturnMoneyA = DataOperationGoodsDataTable.Rows[j]["HowToReturnMoneyA"].toDecimal();///股东类  每天如何归还 钱 。--1 表示不归还   0表示按照系统产生的订单自动归还。  其他>0的表示按照这个值进行加权分红
                Decimal DecimalHowToReturnMoneyB = DataOperationGoodsDataTable.Rows[j]["HowToReturnMoneyB"].toDecimal();///非股东类  每天如何归还 钱 。--1 表示不归还   0表示按照系统产生的订单自动归还。  其他>0的表示按照这个值进行加权分红

                #region   取今天的 28%是多少 如果固定额度分红 这个值后面是不用的
                string str7ThisDay = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                string strWhere = "select sum(DecimalReturnMoney) from b009_EveryGetOrderIDDetailIDWillActiveReturnMoney where OperationGoodsID=@OperationGoodsID and ShopClientID=@ShopClientID and ThisDay=@ThisDay";//// 取今天的 28%是多少 如果固定额度分红 这个值后面是不用的
                Decimal this7DayWillReturn = BLL_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.SelectList(strWhere, strOperationGoodsIDID, intShopClient, str7ThisDay).Tables[0].Rows[0][0].toDecimal();
                #endregion 

                EggsoftWX.Model.b007_OperationReturnMoneyEveryDay my_Model_b007_OperationReturnMoneyEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();
                my_Model_b007_OperationReturnMoneyEveryDay = my_BLL_b007_OperationReturnMoneyEveryDay.GetModel("ThisDay=@ThisDay and ShopClient_ID=@ShopClient_ID AND b004_OperationGoodsID=@b004_OperationGoodsID", ThisDay, intShopClient, strOperationGoodsIDID.toInt32());

                #region 测试分红 就打开这个
                //my_Model_b007_OperationReturnMoneyEveryDay = null;
                //DecimalHowToReturnMoney = 1000000;
                #endregion 测试分红 就打开这个

                if (my_Model_b007_OperationReturnMoneyEveryDay == null)///是否有今天分红记录
                {
                    my_Model_b007_OperationReturnMoneyEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();
                    my_Model_b007_OperationReturnMoneyEveryDay.ShopClient_ID = intShopClient;
                    my_Model_b007_OperationReturnMoneyEveryDay.b004_OperationGoodsID = strOperationGoodsIDID.toInt32();
                    my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyAuto = this7DayWillReturn;////如果不是采用实际分红 后面会复写这个数字
                    my_Model_b007_OperationReturnMoneyEveryDay.ThisDay = ThisDay;
                    my_BLL_b007_OperationReturnMoneyEveryDay.Add(my_Model_b007_OperationReturnMoneyEveryDay);
                }
                else
                {

                    if (my_Model_b007_OperationReturnMoneyEveryDay.ThisDayReturnActual > 0)
                    {
                        Boolean BooleanCanRepeatEvery = true;// 可以调试使用 调试使用
                        BooleanCanRepeatEvery = false;
                        if (BooleanCanRepeatEvery)
                        {
                            string strAppend = "\r\n今天开始分钱A 可能是调试期间重复计算 intstrShopClientID=" + intShopClient;
                            builderFilePath.Append(strAppend);
                            Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天加钱分红", strAppend);
                            continue;
                        }
                        else
                        {
                            string strAppend = "\r\n今天已经分过了A 就不分了intstrShopClientID = " + intShopClient;
                            builderFilePath.Append(strAppend);
                            Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红", strAppend);
                            continue;
                        }
                    }

                }

                #region 只给没得到本金的
                string strA = @"SELECT 
      [b005_UserID_Operation_ID].[UserID]
  FROM[b005_UserID_Operation_ID] LEFT OUTER JOIN b002_OperationCenter
  on[b005_UserID_Operation_ID].OperationCenterID = b002_OperationCenter.id
  where isnull(b002_OperationCenter.ShareholderState,0)= 1 and isnull([b005_UserID_Operation_ID].ActiveAccount,0)= 1";

                string strB = @"SELECT 
      [b005_UserID_Operation_ID].[UserID]
  FROM[b005_UserID_Operation_ID] LEFT OUTER JOIN b002_OperationCenter
  on[b005_UserID_Operation_ID].OperationCenterID = b002_OperationCenter.id
  where isnull(b002_OperationCenter.ShareholderState,0)= 0 and isnull([b005_UserID_Operation_ID].ActiveAccount,0)= 1";

                string stsqlrWhereActiveOrderNumA = "OrderDetailID is not null and ActiveOrderNum> 0 and ((ActiveOrderNum * " + UnitDecimal_5994_20171021 + "  - ReturnMoneyUnit - ActiveOrderNum * " + UnitDecimal_5994_20171021 + " * 0.2) < ActiveOrderNum * " + Model_tab_Goods.PromotePrice.toDecimal() + ") and  b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID   and userID in (" + strA + ")";
                string stsqlrWhereActiveOrderNumB = "OrderDetailID is not null and ActiveOrderNum> 0 and ((ActiveOrderNum * " + UnitDecimal_5994_20171021 + "  - ReturnMoneyUnit - ActiveOrderNum * " + UnitDecimal_5994_20171021 + " * 0.2) < ActiveOrderNum * " + Model_tab_Goods.PromotePrice.toDecimal() + ") and  b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID   and userID in (" + strB + ")";
                string strWhereActiveOrderNumA = "SELECT sum(ActiveOrderNum)  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where  " + stsqlrWhereActiveOrderNumA;////没有拿到本金的消费者
                string strWhereActiveOrderNumB = "SELECT sum(ActiveOrderNum)  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where  " + stsqlrWhereActiveOrderNumB;////没有拿到本金的消费者

                ///"select sum(ActiveOrderNum) from b008_OpterationUserActiveReturnMoneyOrderNum where  OrderDetailID is not null and b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID and ActiveOrderNum>0"
                #endregion


                bool boolAExsit = false;//是否退出A类循环
                bool boolBExsit = false;///是否退出Blei
                #region 处理A类
                int IntAllCountA = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strWhereActiveOrderNumA, strOperationGoodsIDID.toInt32(), intShopClient).Tables[0].Rows[0][0].toInt32();
                if (IntAllCountA == 0)
                {
                    ////没有可分的订单
                    Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红A", "没有可分的订单A  intstrShopClientID=" + intShopClient);
                    //continue;
                }
                else
                {
                    Decimal DecimalThisA = DecimalHowToReturnMoneyA / (IntAllCountA);
                    #region 实现处理A类



                    #region 结算用户加权总额   下一步分权使用
                    if (DecimalHowToReturnMoneyA == -1)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红A", "-1表示不要分红  intstrShopClientID=" + intShopClient);
                        boolAExsit = true;
                    }
                    else if (DecimalHowToReturnMoneyA > 0)
                    {
                        my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = DecimalThisA;
                        my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = IntAllCountA;
                        my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                        my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = "开始老板介入分红A" + IntAllCountA.toInt32().toString();
                        my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss = DecimalHowToReturnMoneyA;
                        my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);
                    }
                    else if (DecimalHowToReturnMoneyA == 0)
                    {
                        string This7DaysBefore = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                        EggsoftWX.Model.b007_OperationReturnMoneyEveryDay my_7DaysEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();
                        my_7DaysEveryDay = my_BLL_b007_OperationReturnMoneyEveryDay.GetModel("ThisDay=@ThisDay and ShopClient_ID=@ShopClient_ID AND b004_OperationGoodsID=@b004_OperationGoodsID", This7DaysBefore, intShopClient, strOperationGoodsIDID.toInt32());
                        if (my_7DaysEveryDay != null)
                        {
                            DecimalHowToReturnMoneyA = my_7DaysEveryDay.ThisDayMoneyAuto.toDecimal();
                            my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss = DecimalHowToReturnMoneyA;
                            DecimalThisA = DecimalHowToReturnMoneyA / (IntAllCountA);
                            my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = DecimalThisA;

                            my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = IntAllCountA;
                            my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                            my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = "开始实际7天前收单情况分红A";
                            my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红A", "没有7天前的数据A  intstrShopClientID=" + intShopClient);
                            boolAExsit = true;
                        }
                        //my_Model_b007_OperationReturnMoneyEveryDay.
                        //DecimalMoneyConsumerAllOrder = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyAuto.toDecimal();///今天确实能分的钱
                    }




                    #endregion 结算用户加权总额
                    if (boolAExsit == false)
                    {
                        System.Data.DataTable DataTableUserWillGetMoney_b008A = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetList("UserID,ActiveOrderNum,b004_OperationGoodsID,ReturnMoneyUnit,OrderDetailID", stsqlrWhereActiveOrderNumA, strOperationGoodsIDID.toInt32(), intShopClient).Tables[0];


                        Dictionary<string, ArrayList> openWithUserListA = new Dictionary<string, ArrayList>();/// Userid  您的消费寄售优惠已完成


                        if (IntAllCountA > 0 && DecimalHowToReturnMoneyA > 0)////可以分红
                        {
                            for (int ppppp = 0; ppppp < DataTableUserWillGetMoney_b008A.Rows.Count; ppppp++)
                            {
                                string strUserID = DataTableUserWillGetMoney_b008A.Rows[ppppp]["UserID"].ToString();
                                string strOrderDetailID = DataTableUserWillGetMoney_b008A.Rows[ppppp]["OrderDetailID"].ToString();
                                int intActiveOrderNum = DataTableUserWillGetMoney_b008A.Rows[ppppp]["ActiveOrderNum"].toInt32();
                                int intb004_OperationGoodsID = DataTableUserWillGetMoney_b008A.Rows[ppppp]["b004_OperationGoodsID"].toInt32();
                                Decimal DecimalReturnMoneyUnit = DataTableUserWillGetMoney_b008A.Rows[ppppp]["ReturnMoneyUnit"].toDecimal();////本订单 还剩下多少钱


                                #region 暂不上线 检查下级是否下过单 没有下线下过单的 直接忽略 今天的分红
                                if (false)////暂不上线
                                {
                                    try
                                    {
                                        Decimal DecimalHaveReturnMoney = UnitDecimal_5994_20171021 - DecimalReturnMoneyUnit / intActiveOrderNum;//单位已经还过的钱
                                        if (Model_tab_Goods.PromotePrice.toDecimal() < DecimalHaveReturnMoney)////归还的钱 已经超过本金
                                        {
                                            string strSQL = @"SELECT count(1)
  FROM [tab_Order] where PayStatus = 1 and IsDeleted = 0 and UserID in(SELECT id
   FROM [tab_User]
        where ParentID =" + strUserID + @" )";
                                            Object myObject = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strSQL);
                                            int intNum = myObject.toInt32();

                                            if (intNum == 0)////暂停发放
                                            {
                                                string strInfoAlert = @"小沁温馨提示（A类）：首先感谢您的信任和支持，我们真诚的邀请您和正在努力的沁加人一起让自然源健康进入千万家庭！由于您目前还未进行有效的市场调查推广，请一个月内进行推广，否则我们可能暂停您的市场调查推广费的发放。";

                                                Boolean BooleanMessage_NeedShow = BLL_tab_User_Message_NeedShow.Exists("userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                                if (!BooleanMessage_NeedShow)
                                                {
                                                    #region 通知消息  增加未处理信息
                                                    Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                                                    Model_tab_User_Message_NeedShow.CreateBy = "系统定时执行A" + (DecimalThisA * intActiveOrderNum).toString();
                                                    Model_tab_User_Message_NeedShow.UpdateBy = "系统定时执行A";
                                                    Model_tab_User_Message_NeedShow.UserID = strUserID.toInt32();
                                                    Model_tab_User_Message_NeedShow.InfoNeedShow = strInfoAlert;
                                                    Model_tab_User_Message_NeedShow.InfoType = "PauseGiveMoneyForNoExtension";
                                                    BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                                                    #endregion 通知消息 增加未处理信息
                                                }
                                                ///continue;//////要继续发放
                                            }
                                            else
                                            {
                                                Boolean BooleanMessage_NeedShow = BLL_tab_User_Message_NeedShow.Exists("userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                                if (BooleanMessage_NeedShow)
                                                {
                                                    BLL_tab_User_Message_NeedShow.Update("[Isdeleted]=1,updatetime=getdate(),updateby='下单A类了，就不提示了'", "userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                                }
                                            }


                                        }


                                    }
                                    catch (Exception eeee)
                                    {
                                        Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "系统执行没有下线下过单的直接忽略");

                                    }
                                }

                                #endregion 检查下级是否下过单 没有下过单的 直接忽略 


                                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                                Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                                Model_b006_TotalWealth_OperationUser.ShopClient_ID = intShopClient;
                                Model_b006_TotalWealth_OperationUser.OrderDetailID = strOrderDetailID.toInt32();

                                //Decimal myCountTotalWealth_ = 0;
                                //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(strUserID.toInt32(), out myCountTotalWealth_);
                                #region 得到 5994  截至分红 0.2扣税使用
                                Decimal activeMoney = DecimalReturnMoneyUnit;// - (Decimal)(UnitDecimal_5994_20171021 * (Decimal)0.2 * intActiveOrderNum);

                                #endregion

                                if (activeMoney > DecimalThisA * intActiveOrderNum)
                                {
                                    #region 表示正常给

                                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = DecimalReturnMoneyUnit - DecimalThisA * intActiveOrderNum;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,给差额";
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                    #region 表示正常给
                                    string strOUT = "\r\n表示正常给strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                    builderFilePath.Append(strOUT);
                                    #endregion  表示正常给

                                    #endregion 表示正常给

                                    #region 记录是否需要 退出 优惠警告
                                    if (!openWithUserListA.Keys.Contains(strUserID))
                                    {
                                        openWithUserListA.Add(strUserID, new ArrayList());
                                    }
                                    openWithUserListA[strUserID].Add(1);///存在继续的
                                    #endregion 记录是否需要 退出 优惠警告


                                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalThisA * intActiveOrderNum;
                                }
                                else if (activeMoney <= 0)
                                {
                                    #region 负值 应该不会出现这个 表示给完了
                                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                    string strOUT = "\r\n出现负值，请手动处理追回strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                    builderFilePath.Append(strOUT);
                                    Eggsoft.Common.debug_Log.Call_WriteLog(Model_b008_OpterationUserActiveReturnMoneyOrderNum.toJsonString(), "每日分红出现问题", strOUT);
                                    #region  一次性减去财富值

                                    if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit > 0)
                                    {
                                        Decimal myCountTotalWealth_ = 0;
                                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(strUserID.toInt32(), out myCountTotalWealth_);
                                        if (myCountTotalWealth_ >= Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit)
                                        {

                                            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                            Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;
                                            Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                                            Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = intShopClient;
                                            Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit;
                                            Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "负值 出现问题 一次性减去财富值OrderDetailID" + strOrderDetailID;



                                            my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                                        }
                                        else
                                        {
                                            Eggsoft.Common.debug_Log.Call_WriteLog("strUserID=" + strUserID + "  myCountTotalWealth_=" + myCountTotalWealth_ + "  " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.toJsonString(), "负值 出现问题 财富值不够扣除", "程序报错");
                                        }

                                    }
                                    #endregion 一次性减去财富值



                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + " 负值 出现问题 一次性减去财富值 " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toString();
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = 0;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32() + Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum.toInt32();////这个顺序不能反掉
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "钱给完了,跳过当前用户";
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                    #endregion 表示给完了负值 应该不会出现这个 表示给完了


                                    continue;//////跳过当前用户
                                }
                                else
                                {
                                    #region 已有的值小于要给的 表示给完了

                                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + " 已有的值小于要给的 表示给完了 " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toString();


                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = 0;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32();////这个顺序不能反掉
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,钱给完了,给差额";
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                    #region 已有的值小于要给的 表示给完了
                                    string strOUT = "\r\n减增进入现金,钱给完了,给差额strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                    builderFilePath.Append(strOUT);
                                    #endregion  已有的值小于要给的 表示给完了



                                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = activeMoney;
                                    #endregion 已有的值小于要给的 表示给完了


                                    #region 表示给完了沁加币补贴扣税方式：  5994 / 单 * 0.2 = 1198.80 * 0.135 = 162个沁加币／单



                                    #region 记录是否需要 退出 优惠警告
                                    if (!openWithUserListA.Keys.Contains(strUserID))
                                    {
                                        openWithUserListA.Add(strUserID, new ArrayList());
                                    }
                                    openWithUserListA[strUserID].Add(0);///存在终止的
                                    #endregion 记录是否需要 退出 优惠警告


                                    #endregion
                                }

                                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "市场调查推广费发放(" + (DecimalHowToReturnMoneyA * 100).toInt32().toString() + "00" + intActiveOrderNum + "00" + IntAllCountA + ")";
                                int intReturnID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                                #region 增加未处理信息
                                #region 今天是否推送过同类型消息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                bool myTodayInfo_TotalWealth = bll_b011_InfoAlertMessage.Exists("UserID=@UserID and ShopClient_ID=@ShopClient_ID and Type='Info_TotalWealth' and DateDiff(dd,CreatTime,getdate())=0", strUserID.toInt32(), intShopClient);
                                if (!myTodayInfo_TotalWealth)
                                {

                                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage.InfoTip = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.CreateBy = "系统分红";
                                    Model_b011_InfoAlertMessage.UpdateBy = "系统分红";
                                    Model_b011_InfoAlertMessage.UserID = strUserID.toInt32();
                                    Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient;
                                    Model_b011_InfoAlertMessage.Type = "Info_TotalWealth";
                                    Model_b011_InfoAlertMessage.TypeTableID = intReturnID;
                                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                }
                                #endregion 今天是否推送过同类型消息
                                #endregion 增加未处理信息



                                if (intReturnID > 0)
                                {
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 92;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClient;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "市场调查推广费发放WealthID=" + intReturnID;
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    int intTableReturnID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                    #region 增加未处理信息
                                    #region 今天是否推送过同类型消息
                                    bool myTodayInfo_ZhangHuYuE = bll_b011_InfoAlertMessage.Exists("UserID=@UserID and ShopClient_ID=@ShopClient_ID and Type='Info_ZhangHuYuE' and DateDiff(dd,CreatTime,getdate())=0", strUserID.toInt32(), intShopClient);
                                    if (!myTodayInfo_TotalWealth)
                                    {
                                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                        Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                        Model_b011_InfoAlertMessage.CreateBy = "系统分红";
                                        Model_b011_InfoAlertMessage.UpdateBy = "系统分红";
                                        Model_b011_InfoAlertMessage.UserID = strUserID.toInt32();
                                        Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient;
                                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                        Model_b011_InfoAlertMessage.TypeTableID = intTableReturnID;
                                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    }
                                    #endregion 今天是否推送过同类型消息
                                    #endregion 增加未处理信息
                                }
                                my_Model_b007_OperationReturnMoneyEveryDay.ThisDayReturnActual = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayReturnActual.toDecimal() + Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth.toDecimal();
                            }
                        }
                        my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                        my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = "分红完毕";
                        my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);////更新今天实际返还的钱
                        Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红", "今天分钱成功intstrShopClientID=" + intShopClient);


                        #region 循环检查那些人需要警告通知
                        foreach (KeyValuePair<string, ArrayList> kvp in openWithUserListA)
                        {
                            string strWillQuitUserID = kvp.Key;
                            bool exstt0 = ((IList)openWithUserListA[strWillQuitUserID]).Contains(0);///存在终止的
                            bool exstt1 = ((IList)openWithUserListA[strWillQuitUserID]).Contains(1);///存在继续的
                            if ((exstt0) && (!exstt1))
                            {
                                #region 通知消息  增加未处理信息
                                Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                                Model_tab_User_Message_NeedShow.CreateBy = "已经在沁加完成了消费寄售优惠";
                                Model_tab_User_Message_NeedShow.UserID = strWillQuitUserID.toInt32();
                                Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜您已经在沁加完成了消费寄售优惠。您可以在商城直接续购获得消费寄售优惠权，您可以通过以下三个渠道了解公司最新优惠政策：1.介绍您购买的朋友；2.您运营中心负责人；3.公司客服电话400-649-3199！";///我们扣除的20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分，同时为了感谢您的信任和支持已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金）。
                                Model_tab_User_Message_NeedShow.InfoType = "补贴扣税方式C";
                                BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                                #endregion 通知消息 增加未处理信息
                            }
                            else if (exstt0)
                            {
                                #region 通知消息  增加未处理信息
                                Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                                Model_tab_User_Message_NeedShow.CreateBy = "有订单完成了消费寄售优惠";
                                //Model_tab_User_Message_NeedShow.UpdateBy = "系统定时执行";
                                Model_tab_User_Message_NeedShow.UserID = strWillQuitUserID.toInt32();
                                //Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜发财，大吉大利！小沁友情提醒：您的消费寄售优惠已完成。很抱歉的通知您，由于在享受此优惠期间小沁识别出您没有为我们伟大的自然健康事业做出市场推广贡献,系统已终止了您享受沁加优惠方案产品的购买权限，您不能继续购买消费寄售优惠，也不能享受市场调查劳务费权限。由于系统在您提现时没有及时扣除您寄售所得部分应缴20 % 手续费，手续费13.5 % 部分我们已经补贴沁加币，您可以使用沁加币根据权限享受商城购买1：1现金抵用！";
                                Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜发财。您有订单完成了消费寄售优惠。您可以通过以下三个渠道了解公司最新优惠政策：1.介绍您购买的朋友；2.您运营中心负责人；3.公司客服电话400-649-3199！";///我们已经扣除了20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分，同时已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金），您可以在沁加商城使用沁加币进行优惠购买。
                                Model_tab_User_Message_NeedShow.InfoType = "补贴扣税方式A";
                                BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                                #endregion 通知消息 增加未处理信息
                            }
                        }
                        #endregion 循环检查那些人需要警告通知





                    }



                    #endregion 实现处理A类
                }
                #endregion 处理A类

                #region 处理B类
                int IntAllCountB = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strWhereActiveOrderNumB, strOperationGoodsIDID.toInt32(), intShopClient).Tables[0].Rows[0][0].toInt32();
                if (IntAllCountB == 0)
                {
                    ////没有可分的订单
                    Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红B", "没有可分的订单B  intstrShopClientID=" + intShopClient);
                    if (boolAExsit) continue;
                }
                else
                {
                    Decimal DecimalThisB = DecimalHowToReturnMoneyB / (IntAllCountB);
                    #region 实现处理B类





                    #region 结算用户加权总额   下一步分权使用
                    if (boolBExsit == false)
                    {
                        if (DecimalHowToReturnMoneyB == -1)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红B", "-1表示不要分红  intstrShopClientID=" + intShopClient);
                            if (boolAExsit) continue; ;////两个都退出 这个也退出
                        }
                        else if (DecimalHowToReturnMoneyB > 0)
                        {
                            my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet.toDecimal() + DecimalThisB;
                            my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder.toInt32() + IntAllCountB;
                            my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                            my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy.toString() + "开始老板介入分红B" + IntAllCountB.toInt32().toString();
                            my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss = DecimalHowToReturnMoneyB;
                            my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);
                        }
                        else if (DecimalHowToReturnMoneyB == 0)
                        {
                            string This7DaysBefore = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                            EggsoftWX.Model.b007_OperationReturnMoneyEveryDay my_7DaysEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();
                            my_7DaysEveryDay = my_BLL_b007_OperationReturnMoneyEveryDay.GetModel("ThisDay=@ThisDay and ShopClient_ID=@ShopClient_ID AND b004_OperationGoodsID=@b004_OperationGoodsID", This7DaysBefore, intShopClient, strOperationGoodsIDID.toInt32());
                            if (my_7DaysEveryDay != null)
                            {
                                DecimalHowToReturnMoneyB = my_7DaysEveryDay.ThisDayMoneyAuto.toDecimal();
                                my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss.toDecimal() + DecimalHowToReturnMoneyB;
                                DecimalThisB = DecimalHowToReturnMoneyB / (IntAllCountB);
                                my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = my_Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet.toDecimal() + DecimalThisB;

                                my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder.toInt32() + IntAllCountB;
                                my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                                my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy.toString() + "开始实际7天前收单情况分红B" + IntAllCountB.toInt32().toString();
                                my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);
                            }
                            else
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红", "没有7天前的数据  intstrShopClientID=" + intShopClient);
                                if (boolAExsit) continue; ;
                            }
                            //my_Model_b007_OperationReturnMoneyEveryDay.
                            //DecimalMoneyConsumerAllOrder = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyAuto.toDecimal();///今天确实能分的钱
                        }



                    }
                    #endregion 结算用户加权总额
                    System.Data.DataTable DataTableUserWillGetMoney_b008B = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetList("UserID,ActiveOrderNum,b004_OperationGoodsID,ReturnMoneyUnit,OrderDetailID", stsqlrWhereActiveOrderNumB, strOperationGoodsIDID.toInt32(), intShopClient).Tables[0];


                    Dictionary<string, ArrayList> openWithUserListB = new Dictionary<string, ArrayList>();/// Userid  您的消费寄售优惠已完成


                    if (IntAllCountB > 0 && DecimalHowToReturnMoneyB > 0)////可以分红
                    {
                        for (int ppppp = 0; ppppp < DataTableUserWillGetMoney_b008B.Rows.Count; ppppp++)
                        {
                            string strUserID = DataTableUserWillGetMoney_b008B.Rows[ppppp]["UserID"].ToString();
                            string strOrderDetailID = DataTableUserWillGetMoney_b008B.Rows[ppppp]["OrderDetailID"].ToString();
                            int intActiveOrderNum = DataTableUserWillGetMoney_b008B.Rows[ppppp]["ActiveOrderNum"].toInt32();
                            int intb004_OperationGoodsID = DataTableUserWillGetMoney_b008B.Rows[ppppp]["b004_OperationGoodsID"].toInt32();
                            Decimal DecimalReturnMoneyUnit = DataTableUserWillGetMoney_b008B.Rows[ppppp]["ReturnMoneyUnit"].toDecimal();////本订单 还剩下多少钱


                            #region 暂不上线 检查下级是否下过单 没有下线下过单的 直接忽略 今天的分红
                            if (false)////
                            {
                                try
                                {
                                    Decimal DecimalHaveReturnMoney = UnitDecimal_5994_20171021 - DecimalReturnMoneyUnit / intActiveOrderNum;//单位已经还过的钱
                                    if (Model_tab_Goods.PromotePrice.toDecimal() < DecimalHaveReturnMoney)////归还的钱 已经超过本金
                                    {
                                        string strSQL = @"SELECT count(1)
  FROM [tab_Order] where PayStatus = 1 and IsDeleted = 0 and UserID in(SELECT id
   FROM [tab_User]
        where ParentID =" + strUserID + @" )";
                                        Object myObject = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strSQL);
                                        int intNum = myObject.toInt32();

                                        if (intNum == 0)////暂停发放
                                        {
                                            string strInfoAlert = @"小沁温馨提示：首先感谢您的信任和支持，我们真诚的邀请您和正在努力的沁加人一起让自然源健康进入千万家庭！由于您目前还未进行有效的市场调查推广，请一个月内进行推广，否则我们可能暂停您的市场调查推广费的发放。";

                                            Boolean BooleanMessage_NeedShow = BLL_tab_User_Message_NeedShow.Exists("userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                            if (!BooleanMessage_NeedShow)
                                            {
                                                #region 通知消息  增加未处理信息
                                                Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                                                Model_tab_User_Message_NeedShow.CreateBy = "系统定时执行" + (DecimalThisB * intActiveOrderNum).toString();
                                                Model_tab_User_Message_NeedShow.UpdateBy = "系统定时执行";
                                                Model_tab_User_Message_NeedShow.UserID = strUserID.toInt32();
                                                Model_tab_User_Message_NeedShow.InfoNeedShow = strInfoAlert;
                                                Model_tab_User_Message_NeedShow.InfoType = "PauseGiveMoneyForNoExtension";
                                                BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                                                #endregion 通知消息 增加未处理信息
                                            }
                                            ///continue;//////要继续发放
                                        }
                                        else
                                        {
                                            Boolean BooleanMessage_NeedShow = BLL_tab_User_Message_NeedShow.Exists("userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                            if (BooleanMessage_NeedShow)
                                            {
                                                BLL_tab_User_Message_NeedShow.Update("[Isdeleted]=1,updatetime=getdate(),updateby='下单了，就不提示了'", "userID=@userID and IFshowed=0 and InfoType='PauseGiveMoneyForNoExtension' and isdeleted=0", strUserID.toInt32());
                                            }
                                        }


                                    }


                                }
                                catch (Exception eeee)
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "系统执行没有下线下过单的直接忽略");

                                }
                            }

                            #endregion 检查下级是否下过单 没有下过单的 直接忽略 


                            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                            Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                            Model_b006_TotalWealth_OperationUser.ShopClient_ID = intShopClient;
                            Model_b006_TotalWealth_OperationUser.OrderDetailID = strOrderDetailID.toInt32();

                            //Decimal myCountTotalWealth_ = 0;
                            //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(strUserID.toInt32(), out myCountTotalWealth_);
                            #region 得到 5994  截至分红 0.2扣税使用
                            Decimal activeMoney = DecimalReturnMoneyUnit;// - (Decimal)(UnitDecimal_5994_20171021 * (Decimal)0.2 * intActiveOrderNum);

                            #endregion

                            if (activeMoney > DecimalThisB * intActiveOrderNum)
                            {
                                #region 表示正常给

                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = DecimalReturnMoneyUnit - DecimalThisB * intActiveOrderNum;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,给差额";
                                BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                #region 表示正常给
                                string strOUT = "\r\n表示正常给strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                builderFilePath.Append(strOUT);
                                #endregion  表示正常给

                                #endregion 表示正常给

                                #region 记录是否需要 退出 优惠警告
                                if (!openWithUserListB.Keys.Contains(strUserID))
                                {
                                    openWithUserListB.Add(strUserID, new ArrayList());
                                }
                                openWithUserListB[strUserID].Add(1);///存在继续的
                                #endregion 记录是否需要 退出 优惠警告


                                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalThisB * intActiveOrderNum;
                            }
                            else if (activeMoney <= 0)
                            {
                                #region 负值 应该不会出现这个 表示给完了
                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                string strOUT = "\r\n出现负值，请手动处理追回strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                builderFilePath.Append(strOUT);
                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_b008_OpterationUserActiveReturnMoneyOrderNum.toJsonString(), "每日分红出现问题", strOUT);
                                #region  一次性减去财富值

                                if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit > 0)
                                {
                                    Decimal myCountTotalWealth_ = 0;
                                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(strUserID.toInt32(), out myCountTotalWealth_);
                                    if (myCountTotalWealth_ >= Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit)
                                    {

                                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                        Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;
                                        Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                                        Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = intShopClient;
                                        Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit;
                                        Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "负值 出现问题 一次性减去财富值OrderDetailID" + strOrderDetailID;



                                        my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                                    }
                                    else
                                    {
                                        Eggsoft.Common.debug_Log.Call_WriteLog("strUserID=" + strUserID + "  myCountTotalWealth_=" + myCountTotalWealth_ + "  " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.toJsonString(), "负值 出现问题 财富值不够扣除", "程序报错");
                                    }

                                }
                                #endregion 一次性减去财富值



                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + " 负值 出现问题 一次性减去财富值 " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toString();
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = 0;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32() + Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum.toInt32();////这个顺序不能反掉
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "钱给完了,跳过当前用户";
                                BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                #endregion 表示给完了负值 应该不会出现这个 表示给完了


                                continue;//////跳过当前用户
                            }
                            else
                            {
                                #region 已有的值小于要给的 表示给完了

                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), intShopClient, intb004_OperationGoodsID, strOrderDetailID);
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + " 已有的值小于要给的 表示给完了 " + Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toString();


                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = 0;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32();////这个顺序不能反掉
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,钱给完了,给差额";
                                BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                #region 已有的值小于要给的 表示给完了
                                string strOUT = "\r\n减增进入现金,钱给完了,给差额strUserID=" + strUserID + "  OrderDetailID" + strOrderDetailID;
                                builderFilePath.Append(strOUT);
                                #endregion  已有的值小于要给的 表示给完了


                                //#region  一次性减去财富值
                                //EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                //Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;

                                //Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                                //Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = intShopClient;
                                //Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = (Decimal)(UnitDecimal_5994 * (Decimal)0.2 * intActiveOrderNum);
                                //Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "一次性减去财富值OrderDetailID" + strOrderDetailID;
                                //my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                                //#endregion 一次性减去财富值
                                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = activeMoney;
                                #endregion 已有的值小于要给的 表示给完了


                                #region 表示给完了沁加币补贴扣税方式：  5994 / 单 * 0.2 = 1198.80 * 0.135 = 162个沁加币／单
                                //EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                //Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                //Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                                //Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClient;
                                //Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = UnitDecimal_5994 * (Decimal)0.2 * (Decimal)0.135 * intActiveOrderNum;
                                //Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "补贴扣税方式OrderDetailID" + strOrderDetailID;
                                //int intTableReturn__ID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                                //#region 增加未处理信息
                                //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage01 = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                //EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage01 = new EggsoftWX.Model.b011_InfoAlertMessage();
                                //Model_b011_InfoAlertMessage01.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                //Model_b011_InfoAlertMessage01.CreateBy = "补贴扣税方式";
                                //Model_b011_InfoAlertMessage01.UpdateBy = "补贴扣税方式";
                                //Model_b011_InfoAlertMessage01.UserID = strUserID.toInt32();
                                //Model_b011_InfoAlertMessage01.ShopClient_ID = intShopClient;
                                //Model_b011_InfoAlertMessage01.Type = "Info_GouWuHongBao";
                                //Model_b011_InfoAlertMessage01.TypeTableID = intTableReturn__ID;
                                //bll_b011_InfoAlertMessage01.Add(Model_b011_InfoAlertMessage01);
                                //#endregion 增加未处理信息



                                #region 记录是否需要 退出 优惠警告
                                if (!openWithUserListB.Keys.Contains(strUserID))
                                {
                                    openWithUserListB.Add(strUserID, new ArrayList());
                                }
                                openWithUserListB[strUserID].Add(0);///存在终止的
                                #endregion 记录是否需要 退出 优惠警告


                                #endregion region 表示给完了沁加币补贴扣税方式
                            }

                            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "市场调查推广费发放(" + (DecimalHowToReturnMoneyB * 100).toInt32().toString() + "00" + intActiveOrderNum + "00" + IntAllCountB + ")";
                            int intReturnID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                            #region 增加未处理信息
                            #region 今天是否推送过同类型消息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            bool myTodayInfo_TotalWealth = bll_b011_InfoAlertMessage.Exists("UserID=@UserID and ShopClient_ID=@ShopClient_ID and Type='Info_TotalWealth' and DateDiff(dd,CreatTime,getdate())=0", strUserID.toInt32(), intShopClient);
                            if (!myTodayInfo_TotalWealth)
                            {

                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = "系统分红";
                                Model_b011_InfoAlertMessage.UpdateBy = "系统分红";
                                Model_b011_InfoAlertMessage.UserID = strUserID.toInt32();
                                Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient;
                                Model_b011_InfoAlertMessage.Type = "Info_TotalWealth";
                                Model_b011_InfoAlertMessage.TypeTableID = intReturnID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            }
                            #endregion 今天是否推送过同类型消息
                            #endregion 增加未处理信息



                            if (intReturnID > 0)
                            {
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 92;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClient;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "市场调查推广费发放WealthID=" + intReturnID;
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                int intTableReturnID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加未处理信息
                                #region 今天是否推送过同类型消息
                                bool myTodayInfo_ZhangHuYuE = bll_b011_InfoAlertMessage.Exists("UserID=@UserID and ShopClient_ID=@ShopClient_ID and Type='Info_ZhangHuYuE' and DateDiff(dd,CreatTime,getdate())=0", strUserID.toInt32(), intShopClient);
                                if (!myTodayInfo_TotalWealth)
                                {
                                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.CreateBy = "系统分红";
                                    Model_b011_InfoAlertMessage.UpdateBy = "系统分红";
                                    Model_b011_InfoAlertMessage.UserID = strUserID.toInt32();
                                    Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient;
                                    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                    Model_b011_InfoAlertMessage.TypeTableID = intTableReturnID;
                                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                }
                                #endregion 今天是否推送过同类型消息
                                #endregion 增加未处理信息
                            }
                            my_Model_b007_OperationReturnMoneyEveryDay.ThisDayReturnActual = my_Model_b007_OperationReturnMoneyEveryDay.ThisDayReturnActual.toDecimal() + Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth.toDecimal();
                        }
                    }
                    my_Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                    my_Model_b007_OperationReturnMoneyEveryDay.UpdateBy = "分红完毕";
                    my_BLL_b007_OperationReturnMoneyEveryDay.Update(my_Model_b007_OperationReturnMoneyEveryDay);////更新今天实际返还的钱
                    Eggsoft.Common.debug_Log.Call_WriteLog(my_Model_b007_OperationReturnMoneyEveryDay.toJsonString(), "每天更新加钱分红", "今天分钱成功intstrShopClientID=" + intShopClient);



                    #region 循环检查那些人需要警告通知
                    foreach (KeyValuePair<string, ArrayList> kvp in openWithUserListB)
                    {
                        string strWillQuitUserID = kvp.Key;
                        bool exstt0 = ((IList)openWithUserListB[strWillQuitUserID]).Contains(0);///存在终止的
                        bool exstt1 = ((IList)openWithUserListB[strWillQuitUserID]).Contains(1);///存在继续的
                        if ((exstt0) && (!exstt1))
                        {
                            #region 通知消息  增加未处理信息
                            Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                            Model_tab_User_Message_NeedShow.CreateBy = "已经在沁加完成了消费寄售优惠";
                            Model_tab_User_Message_NeedShow.UserID = strWillQuitUserID.toInt32();
                            Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜您已经在沁加完成了消费寄售优惠。您可以在商城直接续购获得消费寄售优惠权，您可以通过以下三个渠道了解公司最新优惠政策：1.介绍您购买的朋友；2.您运营中心负责人；3.公司客服电话400-649-3199！";///我们扣除的20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分，同时为了感谢您的信任和支持已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金）。
                            Model_tab_User_Message_NeedShow.InfoType = "补贴扣税方式C";
                            BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                            #endregion 通知消息 增加未处理信息
                        }
                        else if (exstt0)
                        {
                            #region 通知消息  增加未处理信息
                            Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                            Model_tab_User_Message_NeedShow.CreateBy = "有订单完成了消费寄售优惠";
                            //Model_tab_User_Message_NeedShow.UpdateBy = "系统定时执行";
                            Model_tab_User_Message_NeedShow.UserID = strWillQuitUserID.toInt32();
                            //Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜发财，大吉大利！小沁友情提醒：您的消费寄售优惠已完成。很抱歉的通知您，由于在享受此优惠期间小沁识别出您没有为我们伟大的自然健康事业做出市场推广贡献,系统已终止了您享受沁加优惠方案产品的购买权限，您不能继续购买消费寄售优惠，也不能享受市场调查劳务费权限。由于系统在您提现时没有及时扣除您寄售所得部分应缴20 % 手续费，手续费13.5 % 部分我们已经补贴沁加币，您可以使用沁加币根据权限享受商城购买1：1现金抵用！";
                            Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜发财。您有订单完成了消费寄售优惠。您可以通过以下三个渠道了解公司最新优惠政策：1.介绍您购买的朋友；2.您运营中心负责人；3.公司客服电话400-649-3199！";///我们已经扣除了20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分，同时已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金），您可以在沁加商城使用沁加币进行优惠购买。
                            Model_tab_User_Message_NeedShow.InfoType = "补贴扣税方式A";
                            BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                            #endregion 通知消息 增加未处理信息
                        }
                    }
                    #endregion 循环检查那些人需要警告通知



                    #endregion 实现处理B类
                }
                #endregion 处理B类
                #region 用户的财富 少于 1800单元 发出警告  
                string strSql = "select UserID,sum(ActiveOrderNum) as allActiveOrderNum,sum(ReturnMoneyUnit) as AllReturnMoneyUnit ,sum(ReturnMoneyUnit)/sum(ActiveOrderNum) as AllReturnMoneyUnitEveryOrder from b008_OpterationUserActiveReturnMoneyOrderNum where ActiveOrderNum>0 and b004_OperationGoodsID=" + strOperationGoodsIDID + " and ShopClient_ID=" + intShopClient + " group by UserID ";
                System.Data.DataTable DataTableUserAlert_b008 = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSql).Tables[0];
                for (int i = 0; i < DataTableUserAlert_b008.Rows.Count; i++)
                {
                    Decimal UserAllReturnMoneyUnitEveryOrder = DataTableUserAlert_b008.Rows[i]["AllReturnMoneyUnitEveryOrder"].toDecimal();
                    String strUserID = DataTableUserAlert_b008.Rows[i]["UserID"].toString();


                    if (UserAllReturnMoneyUnitEveryOrder < (UnitDecimal_5994_20171021 * (Decimal)0.3))/// && (UserAllReturnMoneyUnitEveryOrder >= (UnitDecimal_5994_20171021 * (Decimal)0.2)
                    {                                    //  1800                                                                               1200
                        string strInfoType = "NeedByNewOrder" + strGoodID;

                        bool boolIFExsit = BLL_tab_User_Message_NeedShow.Exists("InfoType=@InfoType and UserID=@UserID and Isdeleted=0", strInfoType, strUserID.toInt32());
                        if (boolIFExsit == false)
                        {
                            Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                            Model_tab_User_Message_NeedShow.CreateBy = "系统定时执行";
                            Model_tab_User_Message_NeedShow.UpdateBy = "系统定时执行";
                            Model_tab_User_Message_NeedShow.UserID = strUserID.toInt32();
                            Model_tab_User_Message_NeedShow.InfoNeedShow = "恭喜发财。小沁友情提醒，您的消费寄售优惠已接近尾声，如要继续享受市场调查员待遇，请提前下单购买并分享朋友圈，进行推广，推广成功您将获得15%税前销售奖金和间接5%税前销售奖金！";///我们扣除的20%寄售服务费中有依法为您代缴税部分，另外为感谢您的支持和信任，我们将会馈赠13.5%的沁加币到您的账户
                            Model_tab_User_Message_NeedShow.InfoType = strInfoType;
                            BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
                        }
                    }
                }

                #endregion 用户的财富 少于 1800单元 发出警告  

                #endregion 开始这款商品的分红
            }






            try
            {
                Eggsoft.Common.FileFolder.WriteFile((strFilePath), builderFilePath.ToString());
            }
            catch (Exception eee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(strFilePath, "写出日志", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(eee, "写出日志", "程序报错");
            }

        }
    }

    /// <summary>
    ///Pub_GetOpenID_And_ 的摘要说明
    /// </summary>
    public class Pub_SocialPlatform
    {
        public Pub_SocialPlatform()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static String Check_SocialPlatform()
        {
            string strCheck_SocialPlatform = "WeiXin";

            string localhosturl = Eggsoft.Common.Application.httpFullUrl().ToLower();// HttpContext.Current.Request.Url.Host;




            bool bool17 = localhosturl.IndexOf("localhost:2426") != -1;
            bool bool16 = localhosturl.IndexOf("localhost:1585") != -1;
            bool bool1 = localhosturl.IndexOf("localhost:28199") != -1;

            bool bool10 = localhosturl.IndexOf("localhost:1587") != -1;
            bool bool11 = localhosturl.IndexOf("yixin.eggsoft.cn") != -1;
            bool bool12 = localhosturl.IndexOf("www.webuy8.net") != -1;
            bool bool13 = localhosturl.IndexOf("noline.eggsoft.cn") != -1;
            bool bool14 = localhosturl.IndexOf("localhost:1588") != -1;

            bool bool23 = localhosturl.IndexOf("noline-weixin.eggsoft.cn") != -1;

            bool bool100 = localhosturl.IndexOf("apps.eggsoft.cn") != -1;
            bool bool101 = localhosturl.IndexOf("localhost:1581") != -1;



            if (bool100 || bool101)
            {
                strCheck_SocialPlatform = "PC";
            }
            else if (bool10 || bool11 || bool12 || bool13 || bool14)
            {
                strCheck_SocialPlatform = "YiXin";
            }
            else
            {
                strCheck_SocialPlatform = "WeiXin";
            }

            return strCheck_SocialPlatform;
        }



        public static String Check_SocialPlatform(Int32 intUserID)
        {

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
            string strstrSocialPlatform = Model_tab_User.SocialPlatform;
            if (String.IsNullOrEmpty(strstrSocialPlatform)) strstrSocialPlatform = "WeiXin";


            return strstrSocialPlatform;
        }


    }

}