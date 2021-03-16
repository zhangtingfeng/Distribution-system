using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// QueryExp 的摘要说明
    /// </summary>
    public class QueryExp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            try
            {
                string strOrderID = context.Request.QueryString["strOrderID"];
                string strShopClientID = context.Request.QueryString["strShopClientID"];
                int intOrderID = 0;
                int.TryParse(strOrderID, out intOrderID);

                Eggsoft_Public_CL.GoodP.ReadHTTPjuheWuLiu(Int32.Parse(strOrderID), Int32.Parse(strShopClientID));////执行查询


                string strKuaidiQuery = "暂不支持查询或查询失败";
                EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery blltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery();
                EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery Modeltab_ShopClient_Order_KuaiDiQuery = blltab_ShopClient_Order_KuaiDiQuery.GetModel("OrderID=" + strOrderID + " and ShopClient_ID=" + strShopClientID);
                if (Modeltab_ShopClient_Order_KuaiDiQuery != null)
                {
                    if (Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount > 0)///===-99999 属于查询 失败 不能在查了
                    {
                        string strJuHeResultList = Modeltab_ShopClient_Order_KuaiDiQuery.JuHeResultList;

                        try
                        {


                            Eggsoft_Public_CL.FahuoDanJsonExp.resultList[] mmmFahuoDanJsonExp = Eggsoft_Public_CL.JsonHelper.JsonDeserialize<Eggsoft_Public_CL.FahuoDanJsonExp.resultList[]>(strJuHeResultList);

                            strKuaidiQuery = "<div class=\"KuaidiOneLIineTitle\">";
                            strKuaidiQuery += "<div class=\"KuaidiOneLIineid1\">序号</div>";
                            strKuaidiQuery += "<div class=\"KuaidiOneLIineid2\">时间</div>";
                            strKuaidiQuery += "<div class=\"KuaidiOneLIineid3\">内容</div>";
                            strKuaidiQuery += "</div>";

                            //Table_QueryKuaidi.Visible = true;
                            for (int intii = 0; intii < mmmFahuoDanJsonExp.Length; intii++)
                            {
                                strKuaidiQuery += "<div class=\"KuaidiOneLIine\">";
                                strKuaidiQuery += "<div class=\"KuaidiOneLIineid1\">" + (intii + 1).ToString() + "</div>";
                                strKuaidiQuery += "<div class=\"KuaidiOneLIineid2\">" + mmmFahuoDanJsonExp[intii].datetime + "</div>";
                                strKuaidiQuery += "<div class=\"KuaidiOneLIineid3\">" + mmmFahuoDanJsonExp[intii].remark + "</div>";
                                strKuaidiQuery += "</div>";

                            }
                        }
                        catch (Exception eeee)
                        {
                            //Table_QueryKuaidi.Visible = false;///属于查询失败的
                            Eggsoft.Common.debug_Log.Call_WriteLog(strJuHeResultList);
                            Eggsoft.Common.debug_Log.Call_WriteLog(eeee);
                        }
                    }
                }

                context.Response.Write(strKuaidiQuery);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
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