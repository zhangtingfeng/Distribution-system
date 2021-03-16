using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// MyWeBuy_AddAgentLevel 的摘要说明
    /// </summary>
    public class MyWeBuy_AddAgentLevel : IHttpHandler
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


                EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolIFAgent = BLLtab_ShopClient_Agent_.Exists("UserID=" + strUserID + " and Empowered=1   and IsDeleted=0 ");
                if (boolIFAgent)
                {
                    pGood_AgentText += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 100%; border-left: none; border-right: none;\">\n";
                    #region 是代理才显示  普通会员看不到 这个

                    #region 直接代理显示
                    System.Data.DataTable oneLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + strUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                    int intOneFenXiaoOrDailiCount = oneLevelTable.Rows.Count;
                    int intTwoFenXiaoOrDailiCount = 0;
                    int intThreeFenXiaoOrDailiCount = 0;

                    for (int i = 0; i < intOneFenXiaoOrDailiCount; i++)
                    {
                        String strTwoUserID = oneLevelTable.Rows[i]["UserID"].ToString();
                        System.Data.DataTable TwoLevelTable = BLLtab_ShopClient_Agent_.GetList("ParentID=" + strTwoUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))").Tables[0];
                        intTwoFenXiaoOrDailiCount += TwoLevelTable.Rows.Count;
                        for (int j = 0; j < TwoLevelTable.Rows.Count; j++)
                        {
                            String strThreeUserID = TwoLevelTable.Rows[j]["UserID"].ToString();
                            intThreeFenXiaoOrDailiCount += BLLtab_ShopClient_Agent_.ExistsCount("ParentID=" + strThreeUserID + " and Empowered=1 and ((AgentLevelSelect  is null) or (AgentLevelSelect=0))");


                        }

                    }



                    string str_FenXiaoOrDaili = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDaili(intShopClientID);
                    string str_Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pIntUserID);


                    pGood_AgentText += "    <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_customer.aspx', '_self');\">\n";
                    pGood_AgentText += "       <td align=\"center\" width=\"18%\">\n";
                    pGood_AgentText += "           <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/06.jpg\">\n";
                    pGood_AgentText += "       </td>\n";
                    pGood_AgentText += "       <td align=\"left\" width=\"33%\">\n";
                    pGood_AgentText += "           <span class=\"shouyi\">直接" + str_FenXiaoOrDaili + "收入</span>\n";
                    pGood_AgentText += "      </td>\n";
                    pGood_AgentText += "       <td align=\"left\" width=\"35%\">\n";
                    pGood_AgentText += "           <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                    pGood_AgentText += "      </td>\n";
                    pGood_AgentText += "   </tr>\n";
                    pGood_AgentText += "           <tr>\n";
                    pGood_AgentText += "   <td style=\"border:1px solid #E3E3E3;border-top:none;\" colspan=\"3\"></td>\n";
                    pGood_AgentText += "   </tr>\n";
                    #endregion 直接代理显示



                    //EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                    //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + intShopClientID);
                    //bool bool_BuyMySelfIfGetMoney = Model_tab_ShopClient_ShopPar.BuyMySelfIfGetMoney;
                    #region 显示三级分销的  下线的情况
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intShopClientID, strUserID.toInt32(), 0);

                    int intLevevNum = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();
                    //int intLevevNum = .Length;


                    if (intLevevNum == 1)
                    {
                        pGood_AgentText += "        <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=1', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">一级" + str_FenXiaoOrDaili + "商(" + intOneFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";

                    }
                    else if (intLevevNum == 2)
                    {
                        pGood_AgentText += "        <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=1', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">一级" + str_FenXiaoOrDaili + "商(" + intOneFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";
                        pGood_AgentText += "           <tr>\n";
                        pGood_AgentText += "   <td style=\"border:1px solid #E3E3E3;border-top:none;\" colspan=\"3\"></td>\n";
                        pGood_AgentText += "   </tr>\n";

                        pGood_AgentText += "        <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=2', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">二级" + str_FenXiaoOrDaili + "商(" + intTwoFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";
                    }
                    else if (intLevevNum == 3)
                    {
                        pGood_AgentText += "        <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=1', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">一级" + str_FenXiaoOrDaili + "商(" + intOneFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";
                        pGood_AgentText += "           <tr>\n";
                        pGood_AgentText += "   <td style=\"border:1px solid #E3E3E3;border-top:none;\" colspan=\"3\"></td>\n";
                        pGood_AgentText += "   </tr>\n";

                        pGood_AgentText += "        <tr class=\"DivLineTR\" onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=2', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">二级" + str_FenXiaoOrDaili + "商(" + intTwoFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";

                        pGood_AgentText += "           <tr>\n";
                        pGood_AgentText += "   <td style=\"border:1px solid #E3E3E3;border-top:none;\" colspan=\"3\"></td>\n";
                        pGood_AgentText += "   </tr>\n";

                        pGood_AgentText += "        <tr class=\"DivLineTR\"  onclick=\"window.open('" + str_Pub_Agent_Path + "/multibutton_agent.aspx?levelshow=3', '_self');\">\n";
                        pGood_AgentText += "            <td align=\"center\" width=\"18%\">\n";
                        pGood_AgentText += "                <img class=\"logo\" src=\"/Templet/02ShiYi/skin/images/02Button/05.jpg\">\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"33%\">\n";
                        pGood_AgentText += "                <span class=\"shouyi\">三级" + str_FenXiaoOrDaili + "商(" + intThreeFenXiaoOrDailiCount + "个)</span>\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "            <td align=\"left\" width=\"35%\">\n";
                        pGood_AgentText += "                <img src=\"/Templet/02ShiYi/skin/images/MyWeBuy8Images_q.png\" class=\"DivLineTR_Arrow\" />\n";
                        pGood_AgentText += "            </td>\n";
                        pGood_AgentText += "        </tr>\n";
                    }
                    #endregion 显示三级分销的  下线的情况

                    #endregion
                    pGood_AgentText += "    </table>\n";
                }








                //strTemplet = strTemplet.Replace("###Pub_Agent_Default_GetAgent_FenXiaoOrDaili###", );

                //' ''strargBody = strargBody.Replace("###LongInfo###", strpGood_LongText);


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