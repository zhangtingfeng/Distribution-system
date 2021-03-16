using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class Cart_SaySelf : System.Web.UI.Page
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


                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        if (type == "saywrite")
                        {
                            string name_jianyi = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["name_jianyi"]);
                            string ToShopCilentIDname = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["ToShopCilentIDname"]);
                            if (String.IsNullOrEmpty(ToShopCilentIDname))
                            {
                                ToShopCilentIDname = "s0";
                            }
                                                  
                            Eggsoft_Public_CL.Pub.insert_tab_User_Question(pub_Int_Session_CurUserID.ToString(), ToShopCilentIDname, Server.HtmlEncode(name_jianyi), "投诉建议");



                            if (ToShopCilentIDname != "s0")
                                Eggsoft_Public_CL.Pub.SendEmail_AddTask("微店", Eggsoft_Public_CL.Pub.GetEmailFromShopClientID(Int16.Parse(ToShopCilentIDname)), "投诉建议", Eggsoft.Common.CommUtil.GetMainContent(name_jianyi));

                            string strToContent = "用户投诉消息！\n";
                            strToContent += Eggsoft.Common.CommUtil.GetMainContent(name_jianyi) + "\n";
                            strToContent += "！\n只要你在48小时内访问微店的公众平台，你都能得到微店的消息回复！" + "\n";

                            if (ToShopCilentIDname.ToLower().IndexOf("0") != -1)
                            {
                                string strpShopClient_ID = ToShopCilentIDname.Remove(0, 1);
                                EggsoftWX.Model.tab_ShopClient Model_ShopClient = new EggsoftWX.BLL.tab_ShopClient().GetModel(Int32.Parse(strpShopClient_ID));
                                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(Int32.Parse(strpShopClient_ID), "WinXinLookGoods"))
                                {
                                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                                    {
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, strToContent);
                                    }
                                }
                            }
                            Eggsoft.Common.JsUtil.ShowMsg("提交成功！感谢您的申请！我们会尽快处理！", "javascript:history.back();");
                        }
                    }
                    else
                    {

                        string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/Cart_SaySelf_jianyi_Templet.html");
                        strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                        strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码

                        string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                        strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                        strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "投诉建议"));

                        strTemplet = strTemplet.Replace("###Header###", "");

                        if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "PC")
                        {
                            strTemplet = strTemplet.Replace("###Webuy8Footer###", "");
                        }
                        else
                        {
                            strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                        }
                        strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

                        string ToShopCilentID = Request.QueryString["ShopCilentID"];
                        strTemplet = strTemplet.Replace("###ToShopCilentID###", ToShopCilentID);

                        Response.Write(strTemplet);
                    }
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

    }
}