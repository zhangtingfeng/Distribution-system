using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _01WA_WebDestop._05XianChangHuoDong
{
    public partial class WF_ChouJiang : System.Web.UI.Page
    {
        public string strBackground_PIC_BigScreen = ""; public string strBackground_SoundPath = "";
        public string strShopClientID = ""; public string strXianChangHuoDongID = ""; public string strXianChangHuoDongNumberbyShopClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(IsPostBack))
            {
                #region 加载奖项等级
                //DropDownList_LevelList.Items

                ListItem list1 = new ListItem("纪念奖", "111");
                ListItem list2 = new ListItem("四等奖", "222");
                ListItem list3 = new ListItem("三等奖", "333");
                ListItem list4 = new ListItem("二等奖", "444");
                ListItem list5 = new ListItem("一等奖", "555");
                ListItem list6 = new ListItem("特等奖", "666");

                this.DropDownList_LevelList.Items.Add(list1);
                this.DropDownList_LevelList.Items.Add(list2);
                this.DropDownList_LevelList.Items.Add(list3);
                this.DropDownList_LevelList.Items.Add(list4);
                this.DropDownList_LevelList.Items.Add(list5);
                this.DropDownList_LevelList.Items.Add(list6);
                #endregion
            }

            iniStateVar();
        }


        private void iniStateVar()
        {
            strXianChangHuoDongID = HttpContext.Current.Request["XianChangHuoDongID"];//是不是访问代理的网页；
            strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            string strIFEndThisChouJiang = HttpContext.Current.Request["IFEndThisChouJiang"];//是不是访问代理的网页；



            //TextShopClientID.Value = strShopClientID;
            //ServiceServicesURLID.Value = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL();


            HyperLinkWF_YaoYiYao.NavigateUrl = "./WF_YaoYiYao-" + strShopClientID + "-" + strXianChangHuoDongID + ".aspx";
            HyperLink_ChouJiang.NavigateUrl = "./WF_ChouJiang-" + strShopClientID + "-" + strXianChangHuoDongID + ".aspx";


            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
            EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ShopClientID=" + strShopClientID + " and id=" + strXianChangHuoDongID + " and ActivityState=1");
            if (Model_tab_ShopClient_XianChangHuoDong != null)
            {

                strBackground_PIC_BigScreen = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + Model_tab_ShopClient_XianChangHuoDong.Background_PIC_BigScreen;
                if (String.IsNullOrEmpty(strBackground_PIC_BigScreen) || Eggsoft.Common.FileFolder.RemoteFileExists(strBackground_PIC_BigScreen) == false) strBackground_PIC_BigScreen = "./Images/4.jpg";
                strBackground_SoundPath = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + Model_tab_ShopClient_XianChangHuoDong.Background_SoundPath;
                if (String.IsNullOrEmpty(strBackground_SoundPath) || Eggsoft.Common.FileFolder.RemoteFileExists(strBackground_SoundPath) == false) strBackground_SoundPath = "./Mp3/Blank_NoAnySound.mp3";



                //string urlasmx = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/Pub/doWeiXianChang.asmx";
                //string[] args = new string[1];
                //args[0] = strShopClientID;// "/UpLoad/images/";
                //object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "doGetHuoDongNumberStatusIsTrue", args);
                //string strReturn = result.ToString();
                string strReturn = PubClass.Pub.doGetHuoDongNumberStatusIsTrue(strShopClientID);

                strXianChangHuoDongNumberbyShopClientID = strReturn.Split('#')[0];
                //TextSceenXianChangHuoDongNumber.Value = strXianChangHuoDongNumberbyShopClientID;
                this.Title = Model_tab_ShopClient_XianChangHuoDong.ActivityName + "现场活动，by" + Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Int32.Parse(strShopClientID)) + " 技术支持：微云基石";
                Literal_ShopClientName.Text = Model_tab_ShopClient_XianChangHuoDong.ActivityName;
                //LiteralWeiXinHao.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "WeiXinHao");//微信号   微现场可用：
                #region  logo  and  erweima  推广二维码


                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClientID));

                //Literal_ShopClientName.Text = Model_tab_ShopClient.ShopClientName + "--";


                if (Model_tab_ShopClient != null)///说明有权限
                {

                    string strXML = Model_tab_ShopClient.XML;
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                    string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                    string strWeiXinErWeiMaIMG = XML__Class_Shop_Client.WeiXinErWeiMaIMG;

                    string strImageUrl = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + strWeiXinErWeiMaIMG;
                    if (Eggsoft.Common.FileFolder.RemoteFileExists(strImageUrl))
                    {
                        Image_logo.ImageUrl = strImageUrl;
                    }
                    else
                    {
                        Image_logo.ImageUrl = "./Images/3c95a5df3dceb9334c4e522c483634de_640_1181.jpg";
                    }
                    activity_logo.ImageUrl = Image_logo.ImageUrl;

                    #region 推广二维码
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong bll = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Modeltab_ShopClient_XianChangHuoDong = bll.GetModel("ActivityState=1 and ShopClientID=" + strShopClientID);
                    if (Modeltab_ShopClient_XianChangHuoDong != null)
                    {
                        int intAgentErWeiMa = Convert.ToInt32(Modeltab_ShopClient_XianChangHuoDong.ShowAgentErWeiMa_UserID_ByAgent);

                        if (intAgentErWeiMa == 0)
                        {
                            //Literal_Agent.Text = Model_tab_ShopClient.ShopClientName;

                            string str_ErWeiMa = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_Client.WeiXinGongZhongPingTaiErWeiMaIMG;
                            Image1_ErWeiMa.ToolTip = str_ErWeiMa;
                            Image1_ErWeiMa.ImageUrl = str_ErWeiMa;
                            Image_codeImg.ToolTip = str_ErWeiMa;
                            Image_codeImg.ImageUrl = str_ErWeiMa;
                        }
                        else
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + intAgentErWeiMa+ "  and IsDeleted=0 ");
                            String strAgentTextName = Eggsoft_Public_CL.Pub.GetstringShowPower_AgentShopTextDesc(strShopClientID.ToString());// "代理店铺:";
                            //  Literal_Agent.Text = strAgentTextName + ":" + Model_tab_ShopClient_Agent_.ShopName;

                            String strHttpImage1_ErWeiMa = "";
                            Eggsoft_Public_CL.Pub_Agent.Pub_Agent_GetAgent_WeiXinErWeiMaPath(intAgentErWeiMa, out strHttpImage1_ErWeiMa);
                            Image1_ErWeiMa.ImageUrl = strHttpImage1_ErWeiMa;
                            Image_codeImg.ImageUrl = strHttpImage1_ErWeiMa;
                        }
                    }

                    #endregion
                }
                #endregion



            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsgNew("现场活动已无效", "http://client.eggsoft.cn");
            }
        }
    }
}