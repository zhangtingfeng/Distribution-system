using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop.Huodong
{
    public partial class _05OlineInfo : System.Web.UI.Page
    {

        public String strMenuName = "", strINCFullName = "", strINCHalfName = "", strOnlineContent = "", strINCIcon = "";
        public String strOnlineID_ID = "";

        public String GetDisplayPassVlideTime = "";
        public String Pub_IFShow_Cus_Item_List = "";

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_CurParentID = 0;
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected int pInt_QueryString_ParentID = 0;
        protected string _Pub_03Footer_html = "";


        EggsoftWX.BLL.tab_ShopClient_OnlineRegistration OnlineRegistration = new EggsoftWX.BLL.tab_ShopClient_OnlineRegistration();
        EggsoftWX.Model.tab_ShopClient_OnlineRegistration RModel = new EggsoftWX.Model.tab_ShopClient_OnlineRegistration();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                    InitOnline();
                    InitWeiXinShareLink();
                    BindRpt();
                    DoFooterHTML();
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

        protected void DoFooterHTML()
        {
            string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod();
            string strPub_Agent_Path = Pub_Agent_Path;

            string STR_tab_ShopClient_ModelUpLoadPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(pub_Int_ShopClientID);
            STR_tab_ShopClient_ModelUpLoadPath += "/Html";
            string STR_03Footer_html = STR_tab_ShopClient_ModelUpLoadPath + "/03Footer.html";
            string strFooter = Eggsoft.Common.FileFolder.Read_Remote_File(strGetAppConfiugUplaod + STR_03Footer_html);
            _Pub_03Footer_html = strFooter.Replace("###SAgentPath###", strPub_Agent_Path);
            _Pub_03Footer_html = _Pub_03Footer_html.Replace("###Pub_ShowAgent_SubMix_Text###", Eggsoft_Public_CL.Pub_Agent.Pub_ShowAgent_SubMix_Text(pub_Int_Session_CurUserID));
        }
        private void BindRpt()
        {
            strOnlineID_ID = Request.QueryString["OnlineID"];
            if (strOnlineID_ID.IndexOf(',') > 0)
            {
                strOnlineID_ID = strOnlineID_ID.Substring(0, strOnlineID_ID.IndexOf(','));
            }


            EggsoftWX.BLL.tab_ShopClient_OlineContent OnlineROlineContent = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
            EggsoftWX.Model.tab_ShopClient_OlineContent RModelOlineContent = new EggsoftWX.Model.tab_ShopClient_OlineContent();
            RModelOlineContent = OnlineROlineContent.GetModel(Int32.Parse(strOnlineID_ID));


            string strNeedCheckWhere = " ";
            try
            {
                Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Online>(RModelOlineContent.XML, System.Text.Encoding.UTF8);
                if (myXML.BoolNeedCheck)
                {
                    strNeedCheckWhere = " and Valid=1 ";
                }

                if (DateTime.Now > myXML.NeedWrite_DeadlineTime)
                {
                    GetDisplayPassVlideTime = "style=\"display:none\"";


                    strOnlineContent += "<br /><br />本次活动已结束！<br /><br />";
                }

            }
            catch
            { }
            finally { }

            DataTable dt = OnlineRegistration.GetList(" isnull(IsDeleted,0)=0 and OnlineID=" + strOnlineID_ID + " " + strNeedCheckWhere + "order by id asc").Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }


        private void InitWeiXinShareLink()
        {
            strOnlineID_ID = Request.QueryString["OnlineID"];

            if (strOnlineID_ID.IndexOf(',') > 0)
            {
                strOnlineID_ID = strOnlineID_ID.Substring(0, strOnlineID_ID.IndexOf(','));
            }

            string strDESFullName = Eggsoft.Common.CommUtil.GetMainContent(strOnlineContent); ;

            string strFirstImageFullName = Eggsoft.Common.Image.GetFirstHtmlImageUrl(strOnlineContent);
            if (strFirstImageFullName == "")
            {
                strFirstImageFullName = strINCIcon;
            }

            if (strFirstImageFullName.ToLower().IndexOf("http") == -1)
            {
                if (String.IsNullOrEmpty(strFirstImageFullName)) strFirstImageFullName = "/Images/OnLineGiveName.gif";

                strFirstImageFullName = Eggsoft.Common.Application.getwebHttp() + strFirstImageFullName;
            }
            string pub_strURL = Eggsoft.Common.Application.getwebHttp() + Pub_Agent_Path + "/05olineinfo-" + strOnlineID_ID + ".aspx";
            Eggsoft.Common.debug_Log.Call_WriteLog("是直接访问 pub_strURL=" + pub_strURL, "转发路径证据核对");////记录初始的转发路径 有利于证据核对

            string cssFrageMent = "";
            cssFrageMent += " <script type=\"text/javascript\">\n";
            cssFrageMent += "var WeiXin_imgAllPageUrl = '" + strFirstImageFullName + "';\n";
            cssFrageMent += "var WeiXin_lineAllPageLink = '" + pub_strURL + "';\n";
            cssFrageMent += "var WeiXin_descAppPageContent = '" + Eggsoft.Common.CommUtil.getShortText(strDESFullName, 80) + "';\n";
            cssFrageMent += "var WeiXin_shareAppAllPageTitle = '" + strMenuName + "';\n";
            cssFrageMent += "var WeiXin_appidAllPage = '';\n";
            cssFrageMent += "var path=WeiXin_lineAllPageLink;\n";
            cssFrageMent += "</script>\n";
            cssFrageMent += "###_PulicChageWeiXin###\n";
            cssFrageMent = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(cssFrageMent, "ShareShopFunction");

            Literal_ShareFriend.Text = cssFrageMent;
        }

        private bool getbool_NeedWrite_NotChoiceShengFeng()
        {
            strOnlineID_ID = Request.QueryString["OnlineID"];
            if (strOnlineID_ID.IndexOf(',') > 0)
            {
                strOnlineID_ID = strOnlineID_ID.Substring(0, strOnlineID_ID.IndexOf(','));
            }

            EggsoftWX.BLL.tab_ShopClient_OlineContent OnlineROlineContent = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
            EggsoftWX.Model.tab_ShopClient_OlineContent RModelOlineContent = new EggsoftWX.Model.tab_ShopClient_OlineContent();
            RModelOlineContent = OnlineROlineContent.GetModel(Int32.Parse(strOnlineID_ID));

            //strOnlineID_ID = Request.QueryString["OnlineID"];

            Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Online>(RModelOlineContent.XML, System.Text.Encoding.UTF8);
            bool bool_NeedWrite_NotChoiceShengFeng = myXML.NeedWrite_NotChoiceShengFeng;
            return bool_NeedWrite_NotChoiceShengFeng;

        }

        public string displayLiuOrShengOrData(string strdisplayLiuOrShengOrData)
        {
            String strReturn = "";

            bool bool_NeedWrite_NotChoiceShengFeng = getbool_NeedWrite_NotChoiceShengFeng();


            if (strdisplayLiuOrShengOrData == "XiangXiDizhi")
            {
                if (bool_NeedWrite_NotChoiceShengFeng)
                {
                    //strReturn = "style=\"display:inline\"";
                }
                else
                {
                    strReturn = "style=\"display:none;\"";
                }
            }
            else if (strdisplayLiuOrShengOrData == "ShengFen")
            {
                if (bool_NeedWrite_NotChoiceShengFeng)
                {
                    strReturn = "style=\"display:none;\"";
                }
                else
                {
                    //strReturn = "style=\"display:inline\"";
                }
            }

            return strReturn;
        }



        private void Init_InitExpListText(string strAddExpListText)
        {
            if ((String.IsNullOrEmpty(strAddExpListText) == false))
            {
                string[] strAddExpListTextList = strAddExpListText.Split('#');
                for (int i = 0; i < strAddExpListTextList.Length; i++)
                {
                    Pub_IFShow_Cus_Item_List += " <tr>\n";
                    Pub_IFShow_Cus_Item_List += "                    <td class=\"td_right\">" + strAddExpListTextList[i] + "：<br />\n";
                    Pub_IFShow_Cus_Item_List += "                        <input style=\"width:98%;\" name=\"txtAddExp" + (i + 1).ToString() + "\" type=\"text\" id=\"txtAddExp" + (i + 1).ToString() + "\" size=\"40\" />\n";
                    Pub_IFShow_Cus_Item_List += "                    </td>\n";
                    Pub_IFShow_Cus_Item_List += "                 </tr>\n";
                }
            }
        }

        private void InitOnline()
        {
            strOnlineID_ID = Request.QueryString["OnlineID"];
            if (strOnlineID_ID.IndexOf(',') > 0)
            {
                strOnlineID_ID = strOnlineID_ID.Substring(0, strOnlineID_ID.IndexOf(','));
            }


            // INC_User_ID=1&MenuID=2

            EggsoftWX.BLL.tab_ShopClient_OlineContent tab_OlineContent_bll = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
            EggsoftWX.Model.tab_ShopClient_OlineContent tab_OlineContent_Model = new EggsoftWX.Model.tab_ShopClient_OlineContent();
            tab_OlineContent_Model = tab_OlineContent_bll.GetModel(Int32.Parse(strOnlineID_ID));

            strOnlineContent = Server.HtmlDecode(tab_OlineContent_Model.Oline_Content);
            /*正则表达式替换求助
       如何将HTML图片路径中的src中不含http:的路径替换成绝对路径
       例如：<img src="logo.jpg">替换成<img src="http://www.xxx.com/logo.jpg">*/
            strOnlineContent = System.Text.RegularExpressions.Regex.Replace(strOnlineContent, "<img([^<]*)src=\"(?!(http))(.*?)\"([^<]*)>", "<img src=\"$3\">");
            string strSearch = "src=\"/upload/";
            strOnlineContent = strOnlineContent.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");
            strSearch = "src=\"/Upload/";
            strOnlineContent = strOnlineContent.Replace(strSearch, "src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "/upload/");





            strMenuName = tab_OlineContent_Model.Title;
            if (tab_OlineContent_Model.AddExpListTextShow.toBoolean())
            {
                Init_InitExpListText(tab_OlineContent_Model.AddExpListText);//自定义字段
            }
            EggsoftWX.BLL.tab_ShopClient INC_User_bll = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient INC_User_Model = new EggsoftWX.Model.tab_ShopClient();
            INC_User_Model = INC_User_bll.GetModel(pub_Int_ShopClientID);

            strINCFullName = INC_User_Model.ShopClientName;
            //strINCWeiXinNum = INC_User_Model.WeiXinNum;

            #region  得到图标  路径  真麻烦啊
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(pub_Int_ShopClientID);
            Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);
            string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
            string strShopButtonImage = Model_tab_ShopClient.ShopButton;

            if (String.IsNullOrEmpty(strShopLogoImage) == false)
            {
                strINCIcon = strShopLogoImage;
            }
            else if (String.IsNullOrEmpty(strShopButtonImage) == false)
            {
                strINCIcon = strShopButtonImage;
            }
            else
            {
                strINCIcon = "/UpLoad/images/lb20141228103044118533376.jpg";//默认微云基石的
            }
            strINCIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strINCIcon;
            #endregion

            strINCHalfName = INC_User_Model.ShopClientName;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            strOnlineID_ID = Request.QueryString["OnlineID"];
            if (strOnlineID_ID.IndexOf(',') > 0)
            {
                strOnlineID_ID = strOnlineID_ID.Substring(0, strOnlineID_ID.IndexOf(','));
            }
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            RModel.Name = txtName.Value;
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            RModel.ShopClient_ID = Int32.Parse(strShopClientID);
            RModel.Createby = pub_Int_Session_CurUserID.toString();
            RModel.Updateby = pub_Int_Session_CurUserID.toString();
            RModel.UserID = pub_Int_Session_CurUserID;
            RModel.LocalCall = txtLocalCall.Value;
            bool bool_OnlineR_Exists = OnlineRegistration.Exists("OnlineID=" + strOnlineID_ID + " and isnull(IsDeleted,0)=0  and LocalCall='" + txtLocalCall.Value + "'");
            if (bool_OnlineR_Exists)
            {
                Eggsoft.Common.JsUtil.ShowMsg("该手机号码已参加过！", -1);
                return;
            }

            bool bool_NeedWrite_NotChoiceShengFeng = getbool_NeedWrite_NotChoiceShengFeng();
            if (bool_NeedWrite_NotChoiceShengFeng)
            {
                if (String.IsNullOrEmpty(Text_Address_New_By_LiuZong.Value))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("地址必须输入！", -1);
                    return;
                }
                else
                {
                    RModel.Address = Text_Address_New_By_LiuZong.Value;
                }
            }
            else
            {
                if (hidprovince.Value == "请选择省份")
                {
                    Eggsoft.Common.JsUtil.ShowMsg("省份必须选择！", -1);
                    return;
                }
                else
                {
                    RModel.Address = hidprovince.Value;
                }
            }
            RModel.OnlineID = Int32.Parse(strOnlineID_ID);


            EggsoftWX.BLL.tab_ShopClient_OlineContent tab_OlineContent_bll = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
            EggsoftWX.Model.tab_ShopClient_OlineContent tab_OlineContent_Model = new EggsoftWX.Model.tab_ShopClient_OlineContent();
            tab_OlineContent_Model = tab_OlineContent_bll.GetModel(Int32.Parse(strOnlineID_ID));
            if (tab_OlineContent_Model.AddExpListTextShow.toBoolean())//自定义字段
            {
                string strAddExpListText = tab_OlineContent_Model.AddExpListText;
                if ((String.IsNullOrEmpty(strAddExpListText) == false))
                {
                    string[] strAddExpListTextList = strAddExpListText.Split('#');
                    for (int i = 0; i < strAddExpListTextList.Length; i++)
                    {
                        String strAddStringAdd = "txtAddExp" + (i + 1).ToString();
                        string strtxtAddExp = Request.Form[strAddStringAdd];

                        switch (i + 1)
                        {
                            case 1:
                                RModel.AddExp1 = strtxtAddExp;
                                break;
                            case 2:
                                RModel.AddExp2 = strtxtAddExp;
                                break;
                            case 3:
                                RModel.AddExp3 = strtxtAddExp;
                                break;
                            case 4:
                                RModel.AddExp4 = strtxtAddExp;
                                break;
                            case 5:
                                RModel.AddExp5 = strtxtAddExp;
                                break;
                            case 6:
                                RModel.AddExp6 = strtxtAddExp;
                                break;
                            case 7:
                                RModel.AddExp7 = strtxtAddExp;
                                break;
                            case 8:
                                RModel.AddExp8 = strtxtAddExp;
                                break;
                            case 9:
                                RModel.AddExp9 = strtxtAddExp;
                                break;
                            case 10:
                                RModel.AddExp10 = strtxtAddExp;
                                break;
                            case 11:
                                RModel.AddExp11 = strtxtAddExp;
                                break;
                            case 12:
                                RModel.AddExp12 = strtxtAddExp;
                                break;
                            case 13:
                                RModel.AddExp13 = strtxtAddExp;
                                break;
                            case 14:
                                RModel.AddExp14 = strtxtAddExp;
                                break;
                            case 15:
                                RModel.AddExp15 = strtxtAddExp;
                                break;
                            case 16:
                                RModel.AddExp16 = strtxtAddExp;
                                break;
                            case 17:
                                RModel.AddExp17 = strtxtAddExp;
                                break;
                            case 18:
                                RModel.AddExp18 = strtxtAddExp;
                                break;
                            case 19:
                                RModel.AddExp19 = strtxtAddExp;
                                break;
                            case 20:
                                RModel.AddExp20 = strtxtAddExp;
                                break;
                            default:

                                break;
                        }
                    }
                }
            }

            if (OnlineRegistration.Add(RModel) > 0)
            {
                Eggsoft.Common.JsUtil.ShowMsg("报名成功！", Pub_Agent_Path + "/Huodong/05olineinfo-" + strOnlineID_ID + ".aspx");
       
            }
        }
    }
}