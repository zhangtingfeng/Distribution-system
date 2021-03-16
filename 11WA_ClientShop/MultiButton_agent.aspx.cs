using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_agent : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    setAllNeedID();
                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_Agent_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    //strTemplet = InitOpenShopAsk(strTemplet);

                    ////strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "我的代理"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    string strLookmySonID = Request.QueryString["LookmySonID"];
                    int intLookmySonID = 0;
                    int.TryParse(strLookmySonID, out intLookmySonID);
                    if (intLookmySonID > 0)
                    {
                        strTemplet = strTemplet.Replace("###strpub_Int_Session_CurUserID###", strLookmySonID);
                        strTemplet = strTemplet.Replace("###strpub_LevelShowNum###", "1");///只看下级的
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###strpub_Int_Session_CurUserID###", pub_Int_Session_CurUserID.ToString());
                        string strLevelShow = Request.QueryString["LevelShow"];
                        strTemplet = strTemplet.Replace("###strpub_LevelShowNum###", strLevelShow);
                    }

                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));

                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                    Response.Write(strTemplet);

                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private String InitOpenShopAsk(string strTemplet)
        {

            Decimal mySonemoney = 0;
            string strLookmySonID = Request.QueryString["LookmySonID"];
            int intLookmySonID = 0;
            int.TryParse(strLookmySonID, out intLookmySonID);

            string strAgentLevelNum = Request.QueryString["AgentLevelNum"];
            int intAgentLevelNum = 0;
            int.TryParse(strAgentLevelNum, out intAgentLevelNum);

            int intGetMySon_AgentMoney = pub_Int_Session_CurUserID;
            if (intLookmySonID > 0)
            {
                intGetMySon_AgentMoney = intLookmySonID;
            }

            //string strBody = Eggsoft_Public_CL.Pub_Agent.GetMySon_AgentMoneyLoadingByPage(intGetMySon_AgentMoney, out mySonemoney,1,intAgentLevelNum);//intAgentLevelNum可选参数  标志显示多少级别
            //        string strBody = Eggsoft_Public_CL.Pub_Agent.GetMySon_AgentMoney(intGetMySon_AgentMoney, out mySonemoney, intAgentLevelNum);//intAgentLevelNum可选参数  标志显示多少级别
            //strTemplet = strTemplet.Replace("###AllMyMoney###", "" + Eggsoft_Public_CL.Pub.getPubMoney(mySonemoney));
            //strTemplet = strTemplet.Replace("###Agents_list###", strBody);

            return strTemplet;

        }
    }
}