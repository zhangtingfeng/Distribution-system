using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// ProductSearchGoods 的摘要说明
    /// </summary>
    public class ProductSearchGoods : IHttpHandler
    {

        public string Pub_Agent_Path = "";


        public void ProcessRequest(HttpContext context)
        {
            string strBody = "";

            //string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("ProductSearchAgentGoods_Blank.txt");
            //strtableWhereSQL = Eggsoft.Common.DESCrypt.Crypt(strtableWhereSQL);
            //string strtableWhereSQL1 = Eggsoft.Common.FileFolder.ReadFile("ProductSearchAgentGoods_SQL.txt");
            //strtableWhereSQL = Eggsoft.Common.DESCrypt.Crypt(strtableWhereSQL1);
            //string strtableWhereSQL2 = Eggsoft.Common.FileFolder.ReadFile("ProductSearchGoods_SQL.txt");
            //strtableWhereSQL = Eggsoft.Common.DESCrypt.Crypt(strtableWhereSQL2);


            try
            {
                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.Request.QueryString["strUserID"]);
                string strpage = Eggsoft.Common.CommUtil.SafeFilter(context.Request.Form["page"]);
                string strSearchKey = (context.Request.QueryString["SearchKey"]);
                strSearchKey = strSearchKey.Replace("搜索商品名/分类/种类/描述/全部", "");
                strSearchKey = strSearchKey.Replace("全部", "");
                strSearchKey = Eggsoft.Common.CommUtil.SafeFilter(strSearchKey);
                string strPageSize = Eggsoft.Common.CommUtil.SafeFilter(context.Request.Form["pagesize"]);
                //strUserID = "8568";
                //strpage = "1";
                //strSearchKey = "微";

                //
                //string strContext=
                int pIntUserID = 0;
                int.TryParse(strUserID, out pIntUserID);

                int pIntShowpages = 1;
                int.TryParse(strpage, out pIntShowpages);


                int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pIntUserID.ToString());
                Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pIntUserID);


                string strList = "{\"list\":[";


                int intPageSize = 10;
                int.TryParse(strPageSize, out intPageSize);

                #region getlist

                strList += GetMyList(strSearchKey, pIntUserID, intShopClientID, pIntShowpages, intPageSize);
                #endregion

                strList += "]}";
                strBody = strList;
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {


            }



            context.Response.Write(strBody);
        }

        private System.Data.DataSet GetMyList_NoAgent_DataSet(string strSearchKey, int intLoadingPageNum, int intPageSize, int intShopClientID)
        {

            System.Data.DataSet myds = null;
            if (String.IsNullOrEmpty(strSearchKey))////空字符串  的话 搜索全部商品
            {

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("sort", "asc");//第一排序字段  
                sql.addOrderField("updatetime", "desc");//第一排序字段  
                sql.table = "tab_Goods";
                sql.outfields = "Name,ShortInfo,ID,sort,icon,Price,PromotePrice,updatetime";
                sql.nowPageIndex = intLoadingPageNum;

                sql.pagesize = intPageSize;
                sql.where = "IsDeleted=0 and isSaled=1 and ShopClient_ID=" + intShopClientID;

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();


                System.Text.StringBuilder strCountSql = new System.Text.StringBuilder();
                strCountSql.Append("select count(*) from tab_Goods " + " where IsDeleted=0 and isSaled=1 and ShopClient_ID=" + intShopClientID);
                object obj = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strCountSql.ToString());
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = Int32.Parse(obj.ToString());
                }

                int intAllCountsNum = cmdresult;
                string strSql = sql.getSQL(intAllCountsNum);
                myds = BLL_tab_Goods.SelectList(strSql);
            }
            else
            {
                #region 关键字 搜索
                string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("ProductSearchGoods_SQL.txt");
                //strtableWhereSQL = Eggsoft.Common.DESCrypt.DeCrypt(strtableWhereSQL);

                strtableWhereSQL = String.Format(strtableWhereSQL, strSearchKey, intShopClientID.ToString());

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("MutltiOrder", "desc");//第一排序字段  
                sql.addOrderField("Sort", "asc");//第一排序字段  
                sql.table = "(" + strtableWhereSQL + ") as tableWhereSQL";
                sql.outfields = "Name,ShortInfo,ID,MutltiOrder,sort,icon,Price,PromotePrice";
                sql.nowPageIndex = intLoadingPageNum;

                sql.pagesize = intPageSize;
                sql.where = "";

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();


                System.Text.StringBuilder strCountSql = new System.Text.StringBuilder();
                strCountSql.Append("select count(*) from (" + strtableWhereSQL + ") as tableWhereSQL ");
                object obj = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strCountSql.ToString());
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = Int32.Parse(obj.ToString());
                }

                int intAllCountsNum = cmdresult;
                string strSql = sql.getSQL(intAllCountsNum);
                myds = BLL_tab_Goods.SelectList(strSql);
                #endregion
            }
            return myds;
        }


        private System.Data.DataSet GetMyList_Agent_DataSet(string strSearchKey, int intLoadingPageNum, int intPageSize, int intShopClientID, int intAgentID)
        {
            System.Data.DataSet myds = null;
            //string str1tableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("ProductSearchGoods_SQL.txt");
            //str1tableWhereSQL=Eggsoft.Common.DESCrypt.DeCrypt(str1tableWhereSQL);程序报错
            if (String.IsNullOrEmpty(strSearchKey))////空字符串  的话 搜索全部商品
            {
                string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("ProductSearchAgentGoods_Blank.txt");
                //strtableWhereSQL = Eggsoft.Common.DESCrypt.DeCrypt(strtableWhereSQL);

                strtableWhereSQL = String.Format(strtableWhereSQL, intShopClientID.ToString(), intAgentID.ToString());

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("Sort", "asc");//第一排序字段  
                sql.addOrderField("updatetime", "desc");//第一排序字段  
                sql.table = "(" + strtableWhereSQL + ") as tableWhereSQL";
                sql.outfields = "Name,ShortInfo,ID,sort,icon,Price,PromotePrice,updatetime";
                sql.nowPageIndex = intLoadingPageNum;

                sql.pagesize = intPageSize;
                sql.where = "";

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();


                System.Text.StringBuilder strCountSql = new System.Text.StringBuilder();
                strCountSql.Append("select count(*) from (" + strtableWhereSQL + ") as tableWhereSQL ");
                object obj = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strCountSql.ToString());
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = Int32.Parse(obj.ToString());
                }

                int intAllCountsNum = cmdresult;
                string strSql = sql.getSQL(intAllCountsNum);
                myds = BLL_tab_Goods.SelectList(strSql);
            }
            else
            {
                #region 关键字 搜索
                string strtableWhereSQL = Eggsoft.Common.FileFolder.ReadFile("ProductSearchAgentGoods_SQL.txt");
                //strtableWhereSQL = Eggsoft.Common.DESCrypt.DeCrypt(strtableWhereSQL);

                strtableWhereSQL = String.Format(strtableWhereSQL, strSearchKey, intShopClientID.ToString(), intAgentID.ToString());

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("MutltiOrder", "desc");//第一排序字段  
                sql.addOrderField("Sort", "asc");//第一排序字段  
                sql.table = "(" + strtableWhereSQL + ") as tableWhereSQL";
                sql.outfields = "Name,ShortInfo,ID,MutltiOrder,sort,icon,Price,PromotePrice";
                sql.nowPageIndex = intLoadingPageNum;

                sql.pagesize = intPageSize;
                sql.where = "";

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();


                System.Text.StringBuilder strCountSql = new System.Text.StringBuilder();
                strCountSql.Append("select count(*) from (" + strtableWhereSQL + ") as tableWhereSQL ");
                object obj = EggsoftWX.SQLServerDAL.DbHelperSQL.GetSingle(strCountSql.ToString());
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = Int32.Parse(obj.ToString());
                }

                int intAllCountsNum = cmdresult;
                string strSql = sql.getSQL(intAllCountsNum);
                myds = BLL_tab_Goods.SelectList(strSql);
                #endregion
            }
            return myds;
        }


        private String GetMyList(string strSearchKey, int pIntUserID, int intShopClientID, int intLoadingPageNum, int intPageSize)
        {
            System.Data.DataSet myds = null;
            //检查是否是代理
            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pIntUserID + " and Empowered=1" + "   and IsDeleted=0 and ShopClientID=" + intShopClientID);///有代理啊
            if (boolAgent)
            {
                #region 代理商品搜索
                myds = GetMyList_Agent_DataSet(strSearchKey, intLoadingPageNum, intPageSize, intShopClientID, pIntUserID);
                #endregion
            }
            else
            {
                int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pIntUserID);
                if (pInt_QueryString_ParentID > 0) //是访问别人代理店铺；
                {
                    #region 代理商品搜索
                    myds = GetMyList_Agent_DataSet(strSearchKey, intLoadingPageNum, intPageSize, intShopClientID, pInt_QueryString_ParentID);
                    #endregion

                    if (myds.Tables[0].Rows.Count == 0)//父代理不存在了
                    {
                        Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_DeleteCookies();
                        #region 原商品搜索
                        myds = GetMyList_NoAgent_DataSet(strSearchKey, intLoadingPageNum, intPageSize, intShopClientID);
                        #endregion
                    }
                }
                else
                {
                    #region 原商品搜索
                    myds = GetMyList_NoAgent_DataSet(strSearchKey, intLoadingPageNum, intPageSize, intShopClientID);
                    #endregion
                }
            }


            string strStrList = "";
            if (myds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < myds.Tables[0].Rows.Count; i++)
                {
                    string strGoodName = myds.Tables[0].Rows[i]["Name"].ToString();


                    string strShortInfo = myds.Tables[0].Rows[i]["ShortInfo"].ToString();
                    string strGoodID = myds.Tables[0].Rows[i]["ID"].ToString();
                    string strGoodIcon = myds.Tables[0].Rows[i]["Icon"].ToString();
                    string strPromotePrice = myds.Tables[0].Rows[i]["PromotePrice"].ToString();
                    string strPrice = myds.Tables[0].Rows[i]["Price"].ToString();

                    string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(strGoodIcon, 200);
                    string strZheKou = Eggsoft_Public_CL.Pub.getPubPromotePrice_ZheKou(Decimal.Parse(strPromotePrice), Decimal.Parse(strPrice));
                    int intByHitCount = Eggsoft_Public_CL.GoodP.ByHitCount(Int32.Parse(strGoodID));


                    String strCurList = "";


                    strCurList += "\"strPathAgent\":" + "\"" + Pub_Agent_Path + "\",";
                    strCurList += "\"strGoodID\":" + "\"" + strGoodID + "\",";
                    strCurList += "\"strGoodName\":" + "\"" + strGoodName + "\",";
                    strCurList += "\"strShortInfo\":" + "\"" + strShortInfo + "\",";
                    strCurList += "\"strGoodID\":" + "\"" + strGoodID + "\",";
                    strCurList += "\"strGoodIcon\":" + "\"" + GoodIcon + "\",";
                    strCurList += "\"strPromotePrice\":" + "\"" + strPromotePrice + "\",";
                    strCurList += "\"strPrice\":" + "\"" + strPrice + "\",";
                    strCurList += "\"strZheKou\":" + "\"" + strZheKou + "\",";
                    strCurList += "\"strByHitCount\":" + "\"" + intByHitCount + "\",";


                    strStrList += "{" + strCurList + "\"id\":\"" + i.ToString() + "\",\"Name\":\"8888\",\"time\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\"}";
                    if (i != myds.Tables[0].Rows.Count - 1) strStrList += ",";
                }
            }
            else
            {
                //strBody = "暂无数据";
            }

            return strStrList;
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}