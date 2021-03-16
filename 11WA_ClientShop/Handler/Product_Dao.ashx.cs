using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Product_Dao 的摘要说明
    /// </summary>
    public class Product_Dao : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {

            try
            {
                context.Response.ContentType = "text/plain";
                string strGoodID = context.Request.QueryString["strGoodID"];
                string strpub_Int_Session_CurUserID = context.Request.QueryString["pub_Int_Session_CurUserID"];
                string strInt_ShopClientID = context.Request.QueryString["Int_ShopClientID"];
                //
                //string strContext=
                int pIntGoodID = 0;
                int.TryParse(strGoodID, out pIntGoodID);
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strInt_ShopClientID, out pub_Int_ShopClientID);

                //string strVisitUserListImgeAndName = Eggsoft_Public_CL.GoodP_MakeHtml.VisitUserListImgeAndName(pIntGoodID);

                //#region mp3  多媒体购物   和         ###DABao###
                ////多媒体购物
                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);


                #region  是否绑定的微信号访问
                String strDA = "";


                if (String.IsNullOrEmpty(Model_ShopClient.XML) == false)
                {
                    try
                    {
                        bool bollChackAuthor = false;

                        Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_ShopClient.XML, System.Text.Encoding.UTF8);
                        if (String.IsNullOrEmpty(XML__Class_Shop_Client.WeiXinRalationUserIDList) == false)
                        {
                            string[] strlist = XML__Class_Shop_Client.WeiXinRalationUserIDList.Split(',');
                            for (int i = 0; i < strlist.Length; i++)
                            {
                                if (strlist[i] == pub_Int_Session_CurUserID.ToString())
                                {
                                    bollChackAuthor = true;
                                }
                            }
                        }



                        if (bollChackAuthor == true)
                        {

                            String[] stringPowerList = Eggsoft_Public_CL.Pub.GetstringPowerList(Model_ShopClient.XML);
                            if (stringPowerList[1] == "1")
                            {

                                strDA = "<div class=\"lrbtn_Good_Height\"><table id=\"ID_TableShowHistory\" border=\"0\" style=\"width:100%;table-layout:fixed;\">\n";
                                strDA += "<tr id=\"TableRow1\">\n";
                                strDA += "	<td id=\"TableCell1\" class=\"p5\">序号</td><td id=\"TableCell2\" class=\"p35\">访问人</td><td id=\"TableCell3\" class=\"p35\">分享人</td><td id=\"TableCell4\" class=\"p5\">次数</td><td id=\"TableCell5\"class=\"p15\">时间</td>";
                                strDA += "</tr>\n";

                                EggsoftWX.BLL.tab_User_Good_History BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();

                                System.Data.DataTable Data_DataTable = BLL_tab_User_Good_History.GetList("GoodID=" + pIntGoodID + " order by Count_Visit").Tables[0];
                                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                                {
                                    string strUserID = Data_DataTable.Rows[i]["UserID"].ToString();
                                    string strParent_UserID = Data_DataTable.Rows[i]["Parent_UserID"].ToString();
                                    string strCount_Visit = Data_DataTable.Rows[i]["Count_Visit"].ToString();
                                    string strUpdateTime = Data_DataTable.Rows[i]["UpdateTime"].ToString();

                                    strDA += "<tr id=\"TableRow1\">\n";
                                    strDA += "	<td>" + i.ToString() + "</td><td>" + Eggsoft_Public_CL.Pub.GetNickName(strUserID) + "</td><td>" + Eggsoft_Public_CL.Pub.GetNickName(strParent_UserID) + "</td><td>" + strCount_Visit + "</td><td>" + strUpdateTime + "</td>";
                                    strDA += "</tr>\n";
                                }
                                strDA += "</table></div>\n";

                            }

                        }

                    }
                    catch (Exception Exceptione)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                    }
                    finally
                    {


                    }

                }
                context.Response.Write(strDA);

                ///strargBody = strargBody.Replace("###DABao###", strDA);
                #endregion


            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            // #endregion





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