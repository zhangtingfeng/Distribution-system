using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// MyWeBuy_AddGouWUQuanDuiHuanEtc 的摘要说明
    /// </summary>
    public class MyWeBuy_AddGouWUQuanDuiHuanEtc : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {

            string pGood_AgentText = "";

            try
            {
                string strUserID = context.Request.QueryString["strUserID"];
                //
                //string strContext=
                int pIntUserID = 0;
                int.TryParse(strUserID, out pIntUserID);


                int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);

                EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc BLLtab_GouWuQuan2XianJInEtc = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                bool boolGouWuQuan2XianJInEtc = BLLtab_GouWuQuan2XianJInEtc.Exists("ShopClientID=" + intShopClientID + " and IsDeleted=0");
                if (boolGouWuQuan2XianJInEtc)
                {
                    string str_Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pIntUserID);

                    pGood_AgentText += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 100%; border-left: none; border-right: none;\">\n";
                    pGood_AgentText += "    <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_gouwuquanchange.aspx', '_self');\">\n";
                    pGood_AgentText += "       <td align=\"center\" width=\"18%\">\n";
                    pGood_AgentText += "           <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/07.jpg\">\n";
                    pGood_AgentText += "       </td>\n";
                    pGood_AgentText += "       <td align=\"left\" width=\"33%\">\n";
                    pGood_AgentText += "           <span class=\"shouyi\">" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + "兑换</span>\n";
                    pGood_AgentText += "      </td>\n";
                    pGood_AgentText += "       <td align=\"left\" width=\"35%\">\n";
                    pGood_AgentText += "           <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    pGood_AgentText += "      </td>\n";
                    pGood_AgentText += "   </tr>\n";
                    pGood_AgentText += "           <tr>\n";
                    pGood_AgentText += "   <td style=\"border:1px solid #E3E3E3;border-top:none;\" colspan=\"3\"></td>\n";
                    pGood_AgentText += "   </tr>\n";
                    pGood_AgentText += "    </table>\n";
                }


                //string strVisitUserListImgeAndName = Eggsoft_Public_CL.GoodP_MakeHtml.VisitUserListImgeAndName(pIntGoodID);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {


            }
            context.Response.Write(pGood_AgentText);
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