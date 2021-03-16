using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.Game
{
    public partial class DoGameUserID : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string pub_UserSafeCode = "";
        protected string Pub_Agent_Path = "";
        protected int Pub_DB_ParentID = 0;
        protected string Pub_GetShopClientName = "";
        protected string Pub_GetNickName = "";
        protected string Pub_GetUserHeadImage = "";
        protected string Pub_Get_MyDisk_HeadImage = "";
        protected string Pub_WeiXinAuthorstrGameCallBackURl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    if (pub_Int_Session_CurUserID > 0)
                    {
                        string strhttpURL = Request.QueryString["GameCallBackURl"];
                        if (String.IsNullOrEmpty(strhttpURL))
                        {
                            string strBefore = "";
                            if (Request.UrlReferrer != null)
                                strBefore = Request.UrlReferrer.AbsoluteUri;
                            //Eggsoft.Common.JsUtil.ShowMsg("超级错误，失去聊strhttpURL  GameCallBackURl" + strhttpURL + " strBefore=" + strBefore);
                        }
                        strhttpURL = HttpUtility.UrlDecode(strhttpURL);

                        if (strhttpURL != null && string.IsNullOrEmpty(strhttpURL) == false)
                        {
                            Pub_WeiXinAuthorstrGameCallBackURl = strhttpURL.ToLower();
                            if (Pub_DB_ParentID > 0)
                            {
                                if (Pub_WeiXinAuthorstrGameCallBackURl.IndexOf("parentagentid") == -1)
                                {
                                    if (Pub_WeiXinAuthorstrGameCallBackURl.IndexOf("?") == -1)
                                    {
                                        Pub_WeiXinAuthorstrGameCallBackURl += "?parentagentid=" + Pub_DB_ParentID;
                                    }
                                    else
                                    {
                                        Pub_WeiXinAuthorstrGameCallBackURl += "&parentagentid=" + Pub_DB_ParentID;
                                    }
                                }
                            }
                        }


                        Pub_GetShopClientName = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(pub_Int_ShopClientID);
                        Pub_GetNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                        Pub_GetUserHeadImage = Eggsoft_Public_CL.Pub.Get_HeadImage(pub_Int_Session_CurUserID.ToString());
                        pub_UserSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2((new EggsoftWX.BLL.tab_User().GetModel(pub_Int_Session_CurUserID)).SafeCode);

                        Pub_Get_MyDisk_HeadImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(pub_Int_Session_CurUserID);

                    }
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception ee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ee);
                }
                finally { }
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

            Pub_DB_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
        }
    }
}