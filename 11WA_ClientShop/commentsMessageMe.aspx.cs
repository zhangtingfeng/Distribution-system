using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class commentsMessageMe : System.Web.UI.Page
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

                    string strreadSHOWaLLMessage = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/commentsMessageMe_Templet.html");
                    strreadSHOWaLLMessage = strreadSHOWaLLMessage.Replace("###SAgentPath###", Pub_Agent_Path);
                    strreadSHOWaLLMessage = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strreadSHOWaLLMessage, "ShareShopFunction");//微信分享代码
                    strreadSHOWaLLMessage = strreadSHOWaLLMessage.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "留言板"));


                    //string setHeaderBody = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/header_Templet.html");
                    //setHeaderBody = setHeaderBody.Replace("###SAgentPath###", Pub_Agent_Path);
                    strreadSHOWaLLMessage = strreadSHOWaLLMessage.Replace("###Header###", "");

                    strreadSHOWaLLMessage = strreadSHOWaLLMessage.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));





                    string SHOWaLLMessage = readMessage(strreadSHOWaLLMessage);


                    Response.Write(SHOWaLLMessage);

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

        private String getFromID(Int32 Int32Check)
        {
            EggsoftWX.BLL.tab_User_Question bll_tab_User_Question = new EggsoftWX.BLL.tab_User_Question();
            EggsoftWX.Model.tab_User_Question Model_tab_User_Question = new EggsoftWX.Model.tab_User_Question();
            Model_tab_User_Question = bll_tab_User_Question.GetModel(Int32Check);
            return Model_tab_User_Question.UserID;

        }


        private string readMessage(string strreadSHOWaLLMessage)
        {



            string TouserID = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["TouserID"]);
            string strContent = "";


            string InfoAlert_TalkMe_Page = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/InfoAlert_TalkMe_Templet.html");
            InfoAlert_TalkMe_Page = InfoAlert_TalkMe_Page.Replace("###SAgentPath###", Pub_Agent_Path);
            InfoAlert_TalkMe_Page = InfoAlert_TalkMe_Page.Replace("###ToUserID###", TouserID);
            //strTemplet = strTemplet.Replace("###InfoAlert_TalkMe_Page###", InfoAlert_TalkMe_Page);
            strContent += InfoAlert_TalkMe_Page;


            strContent = strreadSHOWaLLMessage.Replace("###commentsMessageMe###", strContent);

            return strContent;
        }
    }
}