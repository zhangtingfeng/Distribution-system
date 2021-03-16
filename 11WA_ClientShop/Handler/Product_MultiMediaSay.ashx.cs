using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Product_MultiMediaSay 的摘要说明
    /// </summary>
    public class Product_MultiMediaSay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string strGoodID = context.Request.QueryString["strGoodID"];
                string strInt_ShopClientID = context.Request.QueryString["Int_ShopClientID"];
                //
                //string strContext=
                int pIntGoodID = 0;
                int.TryParse(strGoodID, out pIntGoodID);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strInt_ShopClientID, out pub_Int_ShopClientID);

                //string strVisitUserListImgeAndName = Eggsoft_Public_CL.GoodP_MakeHtml.VisitUserListImgeAndName(pIntGoodID);

                #region mp3  多媒体购物   和         ###DABao###
                //多媒体购物
                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);



                String strMedia = "";
                String[] stringPowerList;
                try
                {

                    stringPowerList = Eggsoft_Public_CL.Pub.GetstringPowerList(Model_ShopClient.XML);
                    if (stringPowerList.Length > 0)
                    {
                        if (stringPowerList[0] == "1")
                        {

                            string strXML = my_Model_tab_Goods.XML;
                            if (string.IsNullOrEmpty(strXML) == false)
                            {
                                Eggsoft_Public_CL.XML__Class_Shop_Goods myGoodsXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Goods>(strXML, System.Text.Encoding.UTF8);
                                //strMedia = "<audio id=\"car_audio\" controls preload=\"preload\" autoplay><source src=\"" + myGoodsXML.Mp3path + "\" type=\"audio/mpeg\"></audio>";
                                strMedia += "<audio  style=\"position:absolute;left:-1000px; display:none;\" id=\"bgaudio\" controls=\"controls\">\n";
                                strMedia += "<source src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + myGoodsXML.Mp3path + "\" type=\"audio/mpeg\" />\n";
                                strMedia += "Your browser does not support the audio element.\n";
                                strMedia += "</audio>\n";

                                strMedia += "<script type=\"text/javascript\">\n";
                                strMedia += "document.getElementById('bgaudio').play();\n";
                                strMedia += "document.getElementById('bgaudio').play();\n";
                                strMedia += "document.getElementById('bgaudio').style.display=\"none\";\n";
                                strMedia += "</script>\n";
                            }
                        }
                    }
                }
                catch { }
                finally { }
                #endregion

                context.Response.Write(strMedia);
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