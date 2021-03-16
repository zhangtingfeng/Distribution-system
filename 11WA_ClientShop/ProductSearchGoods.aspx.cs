using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class ProductSearchGoods : System.Web.UI.Page
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

                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                    Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                    int Pub_IntStyle_Model = Convert.ToInt32(Model_tab_ShopClient.Style_Model);

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/ProductSearchGoods_Model" + Pub_IntStyle_Model.ToString() + "_Templet.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = strTemplet.Replace("###_PulicChageWeiXin###", "");
                    //strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = strTemplet.Replace("###Header###", "");

                    strTemplet = strTemplet.Replace("###strpub_Int_Session_CurUserID###", pub_Int_Session_CurUserID.ToString());
                    string strSearchKey = Request.QueryString["SearchKey"];
                    strSearchKey = Eggsoft.Common.CommUtil.SafeFilter(strSearchKey);
                    strTemplet = strTemplet.Replace("###SearchKey###", strSearchKey);///
                    if (string.IsNullOrEmpty(strSearchKey) == false)
                    {
                        strTemplet = strTemplet.Replace("###全部商品###", strSearchKey + pub_GetAgentShopName_From_Visit__);///set html title             
                    }
                    else
                    {
                        strTemplet = strTemplet.Replace("###全部商品###", "所有商品" + pub_GetAgentShopName_From_Visit__);///    set html title               
                    }
                    strTemplet = strTemplet.Replace("###SearchKey###", strSearchKey);///    set html title               

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

    }
}