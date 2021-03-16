using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doVisitStatistics1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doVisitStatistics1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string doVisitStatistics_DescContent()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strstrUseid = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUseid"]);
                int intUseid = 0;
                int.TryParse(strstrUseid, out intUseid);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int intShopClientID = 0;
                int.TryParse(strShopClientID, out intShopClientID);
                if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strstrUseid) != intShopClientID)
                {
                    return "strShopClientID 非法访问";
                }
                str = "{\"ErrorCode\":0";

                #region  取本人的
                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                System.Data.DataTable myDataTableUser = BLL_tab_TotalCredits_Consume_Or_Recharge.GetList("creattime,AllSalesMoney_my_AND_myAllSon,ThisMonth_SalesMoney_my_AND_myAllSon,LastMonthSales_my_AND_myAllSon", "UserID=" + strstrUseid + " and ShopClientID=" + strShopClientID).Tables[0];
                if (myDataTableUser.Rows.Count > 0)
                {

                    str += ",\"AllSalesMoney_my_AND_myAllSon\":\"" + myDataTableUser.Rows[0]["AllSalesMoney_my_AND_myAllSon"] + "\"";
                    str += ",\"ThisMonth_SalesMoney_my_AND_myAllSon\":\"" + myDataTableUser.Rows[0]["ThisMonth_SalesMoney_my_AND_myAllSon"] + "\"";
                    str += ",\"LastMonthSales_my_AND_myAllSon\":\"" + myDataTableUser.Rows[0]["LastMonthSales_my_AND_myAllSon"] + "\"";

                    string strCreatTime = myDataTableUser.Rows[0]["creattime"].ToString();
                    if (String.IsNullOrEmpty(strCreatTime))
                    {
                        strCreatTime = DateTime.Now.ToString();
                    }
                    string strCreatTimeReturn = DateTime.Parse(strCreatTime).ToString("d日h时");
                    str += ",\"UpdateTime\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strCreatTimeReturn) + "\"";
                }
                else
                {
                    str += ",\"AllSalesMoney_my_AND_myAllSon\":\"0\"";
                    str += ",\"ThisMonth_SalesMoney_my_AND_myAllSon\":\"0\"";
                    str += ",\"LastMonthSales_my_AND_myAllSon\":\"0\"";
                    str += ",\"UpdateTime\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent("暂无数据") + "\"";

                }
                #region make strPub_Agent_Path  如果 本人不是代理 就看看 他的父亲是不是
                string parentagentadid = "0";
                string parentagentid = "0";

                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intUseid + " and ShopClientID=" + intShopClientID + " and Empowered=1 and IsDeleted=0");
                if (Model_tab_ShopClient_Agent_ != null)
                {

                    if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                    {
                        parentagentadid = intUseid.ToString();///还是高级代理那
                    }
                    else
                    {
                        parentagentid = intUseid.ToString();
                    }

                }
                else
                {
                    ///就看看 他的父亲是不是
                    int intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intUseid);
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ParentID = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intParentID + " and Empowered=1 and IsDeleted=0");
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        if (Model_tab_ShopClient_Agent_.AgentLevelSelect > 0)
                        {
                            parentagentadid = intParentID.ToString();///还是高级代理那
                        }
                        else
                        {
                            parentagentid = intParentID.ToString();
                        }
                    }
                }
                #endregion
                str += ",\"parentagentadid\":\"" + parentagentadid + "\"";
                str += ",\"parentagentid\":\"" + parentagentid + "\"";
              


                #endregion

                #region  ThisMonth_SalesMoney_my_AND_myAllSon
                String strThisMonth_SalesMoney_my_AND_myAllSonWhere = @"SELECT   TOP (3) SUM(tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney) AS sumMoney, 
                tab_TotalCredits_Consume_Or_Recharge.UserID, (isnull(tab_ShopClient_Agent_.ShopName, '') + isnull(tab_User.NickName, '')) as ShopName 
FROM tab_TotalCredits_Consume_Or_Recharge LEFT OUTER JOIN
           tab_User ON tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_User.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_User.ID LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON
                tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_ShopClient_Agent_.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_ShopClient_Agent_.UserID
WHERE(tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = @ShopClient_ID) and (tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=10 or tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=12) and tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge=1 and  datediff(month,[tab_TotalCredits_Consume_Or_Recharge.UpdateTime],getdate())=0
GROUP BY tab_TotalCredits_Consume_Or_Recharge.UserID, tab_ShopClient_Agent_.ShopName, 
                tab_User.NickName
ORDER BY sumMoney DESC";

                System.Data.DataTable myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList(strThisMonth_SalesMoney_my_AND_myAllSonWhere, intShopClientID).Tables[0];
                if (myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon.Rows.Count > 0)
                {
                    string strThisMonth_SalesMoney_my_AND_myAllSon = "";
                    for (int kkk = 0; kkk < myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon.Rows.Count; kkk++)
                    {
                        if (kkk > 0) strThisMonth_SalesMoney_my_AND_myAllSon += ",";
                        strThisMonth_SalesMoney_my_AND_myAllSon += "{";
                        strThisMonth_SalesMoney_my_AND_myAllSon += "\"ID\":" + (kkk + 1) + "";
                        strThisMonth_SalesMoney_my_AND_myAllSon += ",\"userid\":" + myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon.Rows[kkk]["UserID"] + "";
                        strThisMonth_SalesMoney_my_AND_myAllSon += ",\"ShopName\":" + "\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon.Rows[kkk]["ShopName"]) + "\"" + "";
                        strThisMonth_SalesMoney_my_AND_myAllSon += ",\"SalesMoney\":" + "\"" + myDataTable_ThisMonth_SalesMoney_my_AND_myAllSon.Rows[kkk]["sumMoney"] + "\"";
                        strThisMonth_SalesMoney_my_AND_myAllSon += "}";
                    }
                    str += ",\"ThisMonth_SalesMoney_my_AND_myAllSonList\":[" + strThisMonth_SalesMoney_my_AND_myAllSon + "]";
                }
                #endregion

                #region  LastMonthSales_my_AND_myAllSon  上个月
                String strLastMonthSales_my_AND_myAllSonWhere = @"SELECT   TOP (3) SUM(tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney) AS sumMoney, 
                tab_TotalCredits_Consume_Or_Recharge.UserID, (isnull(tab_ShopClient_Agent_.ShopName, '') + isnull(tab_User.NickName, '')) as ShopName 
FROM tab_TotalCredits_Consume_Or_Recharge LEFT OUTER JOIN
           tab_User ON tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_User.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_User.ID LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON
                tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_ShopClient_Agent_.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_ShopClient_Agent_.UserID
WHERE(tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = @ShopClient_ID) and (tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=10 or tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=12) and tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge=1 and  datediff(month,[tab_TotalCredits_Consume_Or_Recharge.UpdateTime],getdate())=-1 
GROUP BY tab_TotalCredits_Consume_Or_Recharge.UserID, tab_ShopClient_Agent_.ShopName, 
                tab_User.NickName
ORDER BY sumMoney DESC";

                System.Data.DataTable myDataLastMonthSales_my_AND_myAllSon = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList(strLastMonthSales_my_AND_myAllSonWhere, intShopClientID).Tables[0];
                if (myDataLastMonthSales_my_AND_myAllSon.Rows.Count > 0)
                {
                    string strLastMonthSales_my_AND_myAllSon = "";
                    for (int kkk = 0; kkk < myDataLastMonthSales_my_AND_myAllSon.Rows.Count; kkk++)
                    {
                        if (kkk > 0) strLastMonthSales_my_AND_myAllSon += ",";
                        strLastMonthSales_my_AND_myAllSon += "{";
                        strLastMonthSales_my_AND_myAllSon += "\"ID\":" + (kkk + 1) + "";
                        strLastMonthSales_my_AND_myAllSon += ",\"userid\":" + myDataLastMonthSales_my_AND_myAllSon.Rows[kkk]["UserID"] + "";
                        strLastMonthSales_my_AND_myAllSon += ",\"ShopName\":" + "\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(myDataLastMonthSales_my_AND_myAllSon.Rows[kkk]["ShopName"]) + "\"" + "";
                        strLastMonthSales_my_AND_myAllSon += ",\"SalesMoney\":" + "\"" + myDataLastMonthSales_my_AND_myAllSon.Rows[kkk]["sumMoney"] + "\"";
                        strLastMonthSales_my_AND_myAllSon += "}";
                    }
                    str += ",\"LastMonthSales_my_AND_myAllSonList\":[" + strLastMonthSales_my_AND_myAllSon + "]";
                }
                #endregion

                #region  AllSalesMoney_my_AND_myAllSon
                String strAllSalesMoney_my_AND_myAllSonWhere = @"SELECT   TOP (3) SUM(tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney) AS sumMoney, 
                tab_TotalCredits_Consume_Or_Recharge.UserID, (isnull(tab_ShopClient_Agent_.ShopName, '') + isnull(tab_User.NickName, '')) as ShopName 
FROM tab_TotalCredits_Consume_Or_Recharge LEFT OUTER JOIN
           tab_User ON tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_User.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_User.ID LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON
                tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = tab_ShopClient_Agent_.ShopClientID AND
                tab_TotalCredits_Consume_Or_Recharge.UserID = tab_ShopClient_Agent_.UserID
WHERE(tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = @ShopClient_ID) and (tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=10 or tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType=12) and tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge=1 
GROUP BY tab_TotalCredits_Consume_Or_Recharge.UserID, tab_ShopClient_Agent_.ShopName, 
                tab_User.NickName
ORDER BY sumMoney DESC";
                System.Data.DataTable myDataTableAllSalesMoney_my_AND_myAllSon = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList(strAllSalesMoney_my_AND_myAllSonWhere, intShopClientID).Tables[0];
                if (myDataTableAllSalesMoney_my_AND_myAllSon.Rows.Count > 0)
                {
                    string strAllSalesMoney_my_AND_myAllSon = "";
                    for (int kkk = 0; kkk < myDataTableAllSalesMoney_my_AND_myAllSon.Rows.Count; kkk++)
                    {
                        if (kkk > 0) strAllSalesMoney_my_AND_myAllSon += ",";
                        strAllSalesMoney_my_AND_myAllSon += "{";
                        strAllSalesMoney_my_AND_myAllSon += "\"ID\":" + (kkk + 1) + "";
                        strAllSalesMoney_my_AND_myAllSon += ",\"userid\":" + myDataTableAllSalesMoney_my_AND_myAllSon.Rows[kkk]["userid"] + "";
                        strAllSalesMoney_my_AND_myAllSon += ",\"ShopName\":" + "\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(myDataTableAllSalesMoney_my_AND_myAllSon.Rows[kkk]["ShopName"]) + "\"" + "";
                        strAllSalesMoney_my_AND_myAllSon += ",\"SalesMoney\":" + "\"" + myDataTableAllSalesMoney_my_AND_myAllSon.Rows[kkk]["sumMoney"] + "\"";
                        strAllSalesMoney_my_AND_myAllSon += "}";
                    }
                    str += ",\"AllSalesMoney_my_AND_myAllSonList\":[" + strAllSalesMoney_my_AND_myAllSon + "]";
                }
                str += "}";
                #endregion


                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "排行榜");
            }
            finally
            {

            }
            return str;
        }

    }
}
