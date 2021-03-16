using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.Huodong.LightAppCN
{
    public partial class D : System.Web.UI.Page
    {
        public String strTitleName = "", strDescription = "";
        public String strLightApp_ID_Pic_First = "";
        public String strLightApp_ID_Mp3Path = "";

        public String strLightApp_ID = "";
        public String strINC_User_ID = "";
        public string strintContent = "";

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected int pInt_QueryString_ParentID = 0;
        protected string _Pub_03Footer_html = "";

        EggsoftWX.BLL.tab_ShopClient_LightApp BLL_tab_LightAppR = new EggsoftWX.BLL.tab_ShopClient_LightApp();
        EggsoftWX.Model.tab_ShopClient_LightApp Model_tab_LightApp_Model = new EggsoftWX.Model.tab_ShopClient_LightApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    InitLight();
                    intContent();
                    InitWeiXinShareLink();
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
            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private void InitLight()
        {
            strLightApp_ID = Request.QueryString["LightAppID"];
            strINC_User_ID = Request.QueryString["INC_User_ID"];

            // INC_User_ID=1&MenuID=2

            Model_tab_LightApp_Model = BLL_tab_LightAppR.GetModel(Int32.Parse(strLightApp_ID));

            strDescription = Model_tab_LightApp_Model.Description;
            strTitleName = Model_tab_LightApp_Model.Title;
            //strLightApp_ID_PicList = Model_tab_LightApp_Model.PicList;
            strLightApp_ID_Mp3Path = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Model_tab_LightApp_Model.Mp3Path;


        }



        private void intContent()
        {
            strintContent = "";

            //String[] StringmePicList = strLightApp_ID_PicList.Split(';');

            //ArrayList alStringPicList = new ArrayList();
            //for (int i = 0; i < StringmePicList.Length; i++)
            //{
            //    if (String.IsNullOrEmpty(StringmePicList[i]) == false)
            //    {
            //        if (Eggsoft.Common.FileFolder.File_Exists(StringmePicList[i]))
            //        {
            //            alStringPicList.Add(StringmePicList[i]);
            //        }
            //    }
            //}
            strLightApp_ID = Request.QueryString["LightAppID"];

            EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage BLL_tab_LightApp_EachPage = new EggsoftWX.BLL.tab_ShopClient_LightApp_EachPage();
            System.Data.DataTable Data_DataTable = BLL_tab_LightApp_EachPage.GetList("LightApp_ID=" + strLightApp_ID + " order by ShowPos asc").Tables[0];


            int intPicLength = -1;
            for (int k = 0; k < Data_DataTable.Rows.Count; k++)
            {

                string strPicPath = Data_DataTable.Rows[k]["PicPath"].ToString();
                string strNavName = Data_DataTable.Rows[k]["NavName"].ToString();
                string strNavPath = Data_DataTable.Rows[k]["NavPath"].ToString();
                string strShowNav = Data_DataTable.Rows[k]["ShowNav"].ToString();
                bool bool_ShowNav = bool.Parse(strShowNav);


                if (k == 0)
                {
                    string[] strPicPathList = strPicPath.Split(';');////是多组件上传 可能一次上传了多个
                    if (strPicPathList.Length > 0)
                    {


                        for (int h = 0; h < strPicPathList.Length; h++)
                        {
                            intPicLength++;
                            if (intPicLength == 0)
                            {
                                strLightApp_ID_Pic_First = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicPathList[h];
                                strintContent += "	<!--index " + k.ToString() + h.ToString() + "--><br />\n";
                                strintContent += "<section data-page=\"" + (k).ToString() + h.ToString() + "\" class=\"m-page m-page" + k.ToString() + h.ToString() + "\" data-id='" + (12000 + k * 100 + h).ToString() + "' data-type='info_pic2'>\n";
                                strintContent += "		<div class=\"m-img m-img01\" style=\"background:url(" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicPathList[h] + ") center no-repeat; background-size:cover;\"></div>\n";
                                strintContent += "		<img data-share-logo=\"\"  src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicPathList[h] + "\"  style=\"display:none;\">\n";

                                if (bool_ShowNav == true)
                                {
                                    strintContent += "	<section class=\"lastPage_u-arrow2\"><a class=\"button orange\" target=\"_blank\" href=\"" + strNavPath + "\">" + strNavName + "</a></section>\n";
                                }
                                strintContent += "	</section>\n";
                                strintContent += "<!--index " + k + " end-->\n";
                                strintContent += "\n";
                            }
                            else
                            {
                                strintContent += " <!--全屏大图-->\n";
                                strintContent += "<section data-page=\"" + k.ToString() + h.ToString() + "\" class=\"m-page m-page2 hide\" data-id='" + (12000 + k * 100 + h).ToString() + "' data-type='info_nomore'>\n";
                                strintContent += "<div class=\"m-img m-img01 lazy-bk\" data-bk= '" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicPathList[h] + "' \n";
                                strintContent += "	style=\"background:#ddd center no-repeat; background-size:cover;\">\n";

                                if (bool_ShowNav == true)
                                {
                                    strintContent += "	<section class=\"lastPage_u-arrow2\"><a class=\"button orange\" target=\"_blank\" href=\"" + strNavPath + "\">" + strNavName + "</a></section>\n";
                                }

                                strintContent += "</div>\n";
                                strintContent += "		</section>\n";
                                strintContent += "<!--全屏大图end-->\n";
                                strintContent += "\n";

                            }
                        }


                    }

                }
                else
                {
                    string[] strPicPathList = strPicPath.Split(';');////是多组件上传 可能一次上传了多个
                    if (strPicPathList.Length > 0)
                    {
                        for (int h = 0; h < strPicPathList.Length; h++)
                        {
                            strintContent += " <!--全屏大图-->\n";
                            strintContent += "<section data-page=\"" + k.ToString() + h.ToString() + "\" class=\"m-page m-page2 hide\" data-id='" + (12000 + k * 100 + h).ToString() + "' data-type='info_nomore'>\n";
                            strintContent += "<div class=\"m-img m-img01 lazy-bk\" data-bk= '" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPicPathList[h] + "' \n";
                            strintContent += "	style=\"background:#ddd center no-repeat; background-size:cover;\">\n";

                            if (bool_ShowNav == true)
                            {
                                strintContent += "	<section class=\"lastPage_u-arrow2\"><a class=\"button orange\" target=\"_blank\" href=\"" + strNavPath + "\">" + strNavName + "</a></section>\n";
                            }

                            strintContent += "</div>\n";
                            strintContent += "		</section>\n";
                            strintContent += "<!--全屏大图end-->\n";
                            strintContent += "\n";
                        }
                    }
                }
            }
        }

        private void InitWeiXinShareLink()
        {
            string strgetwebHttp = Eggsoft.Common.Application.getwebHttp();
            string strFirstImageFullName = strLightApp_ID_Pic_First;

            string pub_strURL = Eggsoft.Common.Application.getwebHttp() + Pub_Agent_Path + "/huodong/lightappcn/d-" + strLightApp_ID + ".aspx";
            Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对


            string cssFrageMent = "";
            cssFrageMent += " <script type=\"text/javascript\">\n";
            cssFrageMent += "var WeiXin_imgAllPageUrl = '" + strFirstImageFullName + "';\n";
            cssFrageMent += "var WeiXin_lineAllPageLink = '" + pub_strURL + "';\n";
            cssFrageMent += "var WeiXin_descAppPageContent = '" + Eggsoft.Common.CommUtil.getShortText(strDescription, 80) + "';\n";
            cssFrageMent += "var WeiXin_shareAppAllPageTitle = '" + strTitleName + "';\n";
            cssFrageMent += "var WeiXin_appidAllPage = '';\n";
            cssFrageMent += "var path=WeiXin_lineAllPageLink;\n";
            cssFrageMent += "</script>\n";
            cssFrageMent += "###_PulicChageWeiXin###\n";
            cssFrageMent = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(cssFrageMent, "ShareShopFunction");


            //string cssFrageMent = Pub.GetshareFriendString(strTitleName, strFirstImageFullName, Eggsoft.Common.Application.getwebHttp() + "/LightAppCN/D-" + strINC_User_ID + "-" + strLightApp_ID + ".aspx", Eggsoft.Common.CommUtil.getShortText(strDescription, 80));




            Literal_ShareFriend.Text = cssFrageMent;
            //LiteralControl include = new LiteralControl(cssFrageMent);
            //this.Page.Header.Controls.Add(include);
        }
    }
}