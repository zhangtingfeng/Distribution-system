using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class ProductClassStyleModel3 : System.Web.UI.Page
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

        protected string _Pub_ProductGoodClass_ = "";
        protected string _Pub_ProductGood_ = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();

                    SearchThisGoods();
                    DoHTML();
                    setGoodChildClass();
                    _Pub_strstrFooter = Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path);
                    setLiteral_WeiXinShare();
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


        private void setGoodChildClass()
        {

            string strpClassGoodType = Request.QueryString["pClassGoodType"];
            string strpClassID = Request.QueryString["pClassID"];
            _Pub_ProductGoodClass_ = "pclassgoodtype=" + strpClassGoodType + "&" + "pclassid=" + strpClassID;///前台异步加载使用
            _Pub_ProductGood_ = _Pub_ProductGoodClass_;
            string strType = Request.QueryString["type"];
            if (String.IsNullOrEmpty(strType) == false)
                _Pub_ProductGood_ = _Pub_ProductGoodClass_ + "&type=" + strType;

        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

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
        protected void setLiteral_WeiXinShare()///微信转发首页
        {
            string strFirstImageFullName = "";
            string strURL = "";
            string strDESFullName = "";
            string strTitle = "";
            #region 先知道文件名是什么
            string strProductClassFilename = "";
            string strClassID = Request.QueryString["ClassID"];
            string strType = Request.QueryString["type"];
            if (String.IsNullOrEmpty(strType) == false)
            {
                strProductClassFilename = "productclass.aspx?type=" + strType;
            }
            else if (String.IsNullOrEmpty(strClassID) == false)
            {
                strProductClassFilename = "productclass-" + strClassID + ".aspx";
            }
            #endregion

            #region  strFirstImageFullName
            /*显示头像步骤 代理的头像 店铺的logo  店铺的轮播图片  店铺的商品 都没与 就默认*/
            //string strpShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            //int pInt_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + pub_Int_Session_CurUserID + "   and IsDeleted=0 and (isnull(Empowered, 0) = 1 or OnlyIsAngel=1)");///有代理啊
            if (boolAgent)
            {
                //EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                //EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(pInt_Session_CurUserID);
                strFirstImageFullName = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(pub_Int_Session_CurUserID);

                string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                strURL = strgetwebHttp + "/sagent-" + pub_Int_Session_CurUserID + "/" + strProductClassFilename;

                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                strTitle = _Pub_strGoodBody_Title + pub_GetAgentShopName_From_Visit__;
                if (strTitle.IndexOf(Model_tab_ShopClient.ShopClientName) < 0) strTitle = strTitle + " " + Model_tab_ShopClient.ShopClientName;//不知道是否包含啊

                strDESFullName += strTitle + ",微店推荐 ";
                EggsoftWX.BLL.View_Goods_Class_Agent bll_View_Goods_Class_Agent = new EggsoftWX.BLL.View_Goods_Class_Agent();
                DataSet myds = bll_View_Goods_Class_Agent.GetList("ClassName", " UserID=" + pub_Int_Session_CurUserID + "  order by sort asc,updatetime desc");
                for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                {
                    string GoodName = myds.Tables[0].Rows[i]["ClassName"].ToString();
                    strDESFullName += GoodName;
                }

            }
            else
            {
                ///是不是别家的商品
                ///
                string strParentAgentID = HttpContext.Current.Request["parentagentid"];//是不是访问别人网页；
                if (String.IsNullOrEmpty(strParentAgentID) == false) //是访问别人代理店铺；
                {
                    strFirstImageFullName = Eggsoft_Public_CL.Pub.Get_MyDisk_HeadImage(Int32.Parse(strParentAgentID));
                    string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                    strURL = strgetwebHttp + "/sagent-" + strParentAgentID + "/" + strProductClassFilename;

                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                    strTitle = _Pub_strGoodBody_Title + pub_GetAgentShopName_From_Visit__;
                    if (strTitle.IndexOf(Model_tab_ShopClient.ShopClientName) < 0) strTitle = strTitle + " " + Model_tab_ShopClient.ShopClientName;//不知道是否包含啊

                    strDESFullName += strTitle + ",微店推荐 ";
                    EggsoftWX.BLL.View_Goods_Class_Agent bll_View_Goods_Class_Agent = new EggsoftWX.BLL.View_Goods_Class_Agent();
                    DataSet myds = bll_View_Goods_Class_Agent.GetList("ClassName", " UserID=" + Int32.Parse(strParentAgentID) + "  order by sort asc,updatetime desc");
                    for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                    {
                        string GoodName = myds.Tables[0].Rows[i]["ClassName"].ToString();
                        strDESFullName += GoodName;
                    }
                }
                else
                {

                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);
                    string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                    string strShopButtonImage = Model_tab_ShopClient.ShopButton;

                    if (String.IsNullOrEmpty(strShopLogoImage) == false)
                    {
                        strFirstImageFullName = strShopLogoImage;
                    }
                    else if (String.IsNullOrEmpty(strShopButtonImage) == false)
                    {
                        strFirstImageFullName = strShopButtonImage;
                    }
                    else
                    {
                        strFirstImageFullName = "/UpLoad/images/lb20141228103044118533376.jpg";//默认的
                    }

                    string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
                    strURL = strgetwebHttp + "/" + strProductClassFilename;

                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(pub_Int_Session_CurUserID);


                    strTitle = _Pub_strGoodBody_Title + Model_tab_User.NickName + " " + pub_GetAgentShopName_From_Visit__;


                    strDESFullName += strTitle + ",微店推荐 ";
                    //EggsoftWX.BLL.tab_Goods_Class tab_Goods_Class = new EggsoftWX.BLL.tab_Goods_Class();
                    //DataSet myds = tab_Goods_Class.GetList("ClassName", " ShopClient_ID=" + pub_Int_ShopClientID + "  order by sort asc,updatetime desc");
                    //for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                    //{
                    //    string GoodName = myds.Tables[0].Rows[i]["ClassName"].ToString();
                    //    strDESFullName += GoodName;
                    //}
                }
            }
            strFirstImageFullName = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strFirstImageFullName;
            #endregion

            Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 strURL=" + strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

            string cssFrageMent = "";

            cssFrageMent += " <script type=\"text/javascript\">\n";
            cssFrageMent += "var WeiXin_imgAllPageUrl = '" + strFirstImageFullName + "';\n";
            cssFrageMent += "var WeiXin_lineAllPageLink = '" + strURL + "';\n";
            cssFrageMent += "var WeiXin_descAppPageContent = '" + Eggsoft.Common.CommUtil.getShortText(strDESFullName, 80) + "';\n";
            cssFrageMent += "var WeiXin_shareAppAllPageTitle = '" + strTitle + "';\n";
            cssFrageMent += "var WeiXin_appidAllPage = '';\n";
            cssFrageMent += "var path=WeiXin_lineAllPageLink;\n";
            cssFrageMent += "</script>\n";
            cssFrageMent += "###_PulicChageWeiXin###\n";
            cssFrageMent = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(cssFrageMent, "ShareShopFunction");

            Literal_WeiXinShare.Text = cssFrageMent;
        }
        private void SearchThisGoods()
        {
            _Pub_strGoodBodyest = "";
            string strClassID = Request.QueryString["pClassID"];
            string strType = Request.QueryString["type"];


            _Pub_strGoodBodyest = "暂无商品上传！";

            if (String.IsNullOrEmpty(strType) == false)
            {
                strType = strType.ToLower();
                if (strType == "newest")
                {

                    _Pub_strGoodBody_Title = "最新上架";
                    _Pub_dBody_Title = _Pub_strGoodBody_Title;//分销商名称

                }
                else if (strType == "salsed")
                {
                    _Pub_strGoodBody_Title = "精选商品";
                }
                else if (strType == "cheapest")
                {
                    _Pub_strGoodBody_Title = "特惠商品";
                }


            }
            else if (String.IsNullOrEmpty(strClassID) == false)
            {

                string strpClassGoodType = Request.QueryString["pClassGoodType"];
                string strpClassID = Request.QueryString["pClassID"];


                string strWhere = "";

                strWhere = " ShopClient_ID=" + pub_Int_ShopClientID;
                EggsoftWX.BLL.tab_Class1 bll_tab_Class1 = new EggsoftWX.BLL.tab_Class1();
                EggsoftWX.BLL.tab_Class2 bll_tab_Class2 = new EggsoftWX.BLL.tab_Class2();
                EggsoftWX.BLL.tab_Class3 bll_tab_Class3 = new EggsoftWX.BLL.tab_Class3();
                int intClassGoodType = 0;
                int.TryParse(strpClassGoodType, out intClassGoodType);
                int intpClassID = 0;
                int.TryParse(strpClassID, out intpClassID);


                EggsoftWX.Model.tab_Class1 Model_tab_Class1 = new EggsoftWX.Model.tab_Class1();
                EggsoftWX.Model.tab_Class2 Model_tab_Class2 = new EggsoftWX.Model.tab_Class2();
                EggsoftWX.Model.tab_Class3 Model_tab_Class3 = new EggsoftWX.Model.tab_Class3();
                if (intClassGoodType == 1)
                {
                    Model_tab_Class1 = bll_tab_Class1.GetModel(intpClassID);
                    _Pub_strGoodBody_Title = Model_tab_Class1.ClassName;
                    _Pub_dBody_Title = _Pub_strGoodBody_Title;
                }
                else if (intClassGoodType == 2)
                {
                    Model_tab_Class2 = bll_tab_Class2.GetModel(intpClassID);
                    _Pub_strGoodBody_Title = Model_tab_Class2.ClassName;
                    _Pub_dBody_Title = _Pub_strGoodBody_Title;
                }
                else if (intClassGoodType == 3)
                {
                    Model_tab_Class3 = bll_tab_Class3.GetModel(intpClassID);
                    _Pub_strGoodBody_Title = Model_tab_Class3.ClassName;
                    _Pub_dBody_Title = _Pub_strGoodBody_Title;
                }
            }

            pub_WeiXin__o2o_FootMarker_Location___ = Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, _Pub_strGoodBody_Title);

        }
    }
}