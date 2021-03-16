using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class Cart_Good2_o2o_Book : System.Web.UI.Page
    {
        protected string pub_WeiXin__o2o_FootMarker_Location___ = "";

        protected string _Pub_strGoodBodyest = "";
        protected string _Pub_strGoodBody_Title = "";
        protected string _Pub_dBody_Title = "";
        protected string _Pub_strstrFooter = "";
        protected string pub_GetAgentShopName_From_Visit__ = "";
        private int pInt_QueryString_ParentID = 0;//；

        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";

        protected string _Pub_02Topbar_html = "";
        protected string _Pub_03Footer_html = "";

        protected string _Pub_Orderid_ = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    string strType = Request.QueryString["type"];
                    if (strType.ToLower() == "takegood")
                    {
                        Image_MarkerErWeiMaThisBook();
                    }
                    else if (strType.ToLower() == "givegood")
                    {
                        GiveGoodsAction();
                    }
                    DoHTML();
                    //_Pub_ProductGoodClass_ = Eggsoft_Public_CL.Pub_Agent.strGet_SAgent_ProductGoodClass(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    _Pub_strstrFooter = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);


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

        /// <summary>
        /// 店主 给货物
        /// 1  验证店主 身份 必须是o2o店主的微信
        /// 2 设置 该订单为已发货状态
        /// </summary>
        private void GiveGoodsAction()
        {
            string strorderid = Request.QueryString["orderid"];

            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = BLL_tab_Order.GetModel(Int32.Parse(strorderid));


            EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
            EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.GetModel(Convert.ToInt32(Model_tab_Order.O2OTakedID));
            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID);

            bool boolIFO2OTakedID = false;
            if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
            {
                string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                {
                    if (pub_Int_Session_CurUserID == Int32.Parse(stringWeiXinRalationUserIDList[k]))
                    {
                        boolIFO2OTakedID = true;
                        break;
                    }
                }
            }


            if (boolIFO2OTakedID == false)
            {
                Eggsoft.Common.JsUtil.ShowMsg("必须是" + Model_tab_ShopClient_O2O_ShopInfo.ShopName + "才有权限扫描发货", "/" + Pub_Agent_Path);
            }
            else
            {
                Model_tab_Order.DeliveryText = "已上门自取";
                BLL_tab_Order.Update(Model_tab_Order);
                //Model_tab_Order.isReceipt = true;//已收获
                Eggsoft.Common.JsUtil.ShowMsg("上门自取发货成功", Eggsoft.Common.Application.getwebHttp().ToLower() + "" + Pub_Agent_Path + "/");
            }

        }
        private void Image_MarkerErWeiMaThisBook()
        {
            _Pub_Orderid_ = Request.QueryString["orderid"];

            string strPath = "";
            Eggsoft_Public_CL.FahuoDan.Pub_Take_Goods_Path(Int32.Parse(_Pub_Orderid_), out strPath);

            if (Eggsoft.Common.FileFolder.RemoteFileExists(strPath))
            {
                Image_MarkerErWeiMa.ImageUrl = strPath;

            }
            else
            {
                string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/Pub_Take_Goods_Path.asmx";
                string[] args = new string[1];
                args[0] = _Pub_Orderid_;// "/UpLoad/images/";
                object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_Take_Goods_Path", args);
                string strresult = result.ToString();
                //if (Eggsoft.Common.FileFolder.RemoteFileExists(strPath))
                //{
                Image_MarkerErWeiMa.ImageUrl = strPath;
                //}
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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);

        }

        protected void DoHTML()
        {

            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";



            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);
            _Pub_03Footer_html = strFooter.Replace("###SAgentPath###", Pub_Agent_Path);
            _Pub_03Footer_html = _Pub_03Footer_html.Replace("background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">申请代理</li>", "background-image: url(/Templet/02ShiYi/skin/images/h02.jpg);\">" + Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID) + "</li>");

        }

    }
}