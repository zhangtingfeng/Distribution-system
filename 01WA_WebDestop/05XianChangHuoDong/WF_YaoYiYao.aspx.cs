using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace _01WA_WebDestop._05XianChangHuoDong
{
    public partial class WF_YaoYiYao : System.Web.UI.Page
    {

        public int intCountHowMany = 0; public int intLongShakeTime = 0; public int intMaxTracks = 0;
        public string strBackground_PIC_BigScreen = ""; public string strBackground_SoundPath = "";
        public string strShopClientID = ""; public string strXianChangHuoDongID = ""; public string strXianChangHuoDongNumberbyShopClientID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            //string strSession_05XianChangHuoDong = "Session_05XianChangHuoDong" + strShopClientID;
            //string sSession_05XianChangHuoDong = Eggsoft.Common.Session.Read(strSession_05XianChangHuoDong);
            //Response.Write(sSession_05XianChangHuoDong);
            //if (string.IsNullOrEmpty(sSession_05XianChangHuoDong)){
            //    Eggsoft.Common.Session.Add(strSession_05XianChangHuoDong, strSession_05XianChangHuoDong);
            //}
            if (!(IsPostBack))
            {
                iniStateVar();
            }
        }


       
      

        #region 重置数据  和 主机的编号 同步就是

        private static Object isEnableLock_CopyData = new Object();

        [WebMethod]
        /// <summary>
        /// 重置数据  和 主机的编号 同步就是
        /// </summary>
        /// <returns></returns>
        private String doResetAllDataForUni(string strArgintShopClientID, string strArgXianChangHuoDongNumberbyShopClientID)
        {///当前现场 活动 是否开始  0表示 尚未开始 1表示现场区域开发，但是 摇奖尚未开始 2表示正在进行 3 表示 当前 已结束

            String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(strArgintShopClientID);
            String strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(strArgXianChangHuoDongNumberbyShopClientID);


            string strReturn = "-1";

            lock (isEnableLock_CopyData)
            {
                try
                {
                    ////数据库中 应该 只存在 一个 是1  或者 0  的 和 本编号 相同的 数据 。其他 的 都让他们 结束
                    EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                    string strUpdateWhere = "ShopClientID=" + strShopClientID + " and (XianChangHuoDongStatus=1 or XianChangHuoDongStatus=0)";
                    EggsoftWX.Model.b017Help_01XianChangHuoDong_Main Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel(strUpdateWhere);
                    if (Convert.ToInt32(Model_tab_ShopClient_XianChangHuoDong.XianChangHuoDongNum) == Convert.ToInt32(strSceenXianChangHuoDongNumber))
                    {
                        ////对了  什么事 都不做
                    }
                    else
                    {
                        Model_tab_ShopClient_XianChangHuoDong.XianChangHuoDongStatus = 4;////终结它
                        Model_tab_ShopClient_XianChangHuoDong.UpdateTime = DateTime.Now;
                        BLL_tab_ShopClient_XianChangHuoDong.Update(Model_tab_ShopClient_XianChangHuoDong);
                    }
                    strReturn = "0";
                }
                catch (System.Threading.ThreadAbortException e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
            return strReturn;
            //return intErrorCode.ToString();
        }


        #endregion 重置数据  和 主机的编号 同步就是


        private void iniStateVar()
        {
            strXianChangHuoDongID = HttpContext.Current.Request["XianChangHuoDongID"];//是不是访问代理的网页；
            strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            //TextShopClientID.Value = strShopClientID;
            //ServiceServicesURLID.Value = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL();


            HyperLinkWF_YaoYiYao.NavigateUrl = "./WF_YaoYiYao-" + strShopClientID + "-" + strXianChangHuoDongID + ".aspx";
            HyperLink_ChouJiang.NavigateUrl = "./WF_ChouJiang-" + strShopClientID + "-" + strXianChangHuoDongID + ".aspx";

            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
            EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ShopClientID=" + strShopClientID + " and id=" + strXianChangHuoDongID + " and ActivityState=1");
            if (Model_tab_ShopClient_XianChangHuoDong != null)
            {
                intCountHowMany = Convert.ToInt32(Model_tab_ShopClient_XianChangHuoDong.CountHowMany);
                intLongShakeTime = Convert.ToInt32(Model_tab_ShopClient_XianChangHuoDong.LongShakeTime);
                intMaxTracks = Convert.ToInt32(Model_tab_ShopClient_XianChangHuoDong.MaxTracks);


                strBackground_PIC_BigScreen = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + Model_tab_ShopClient_XianChangHuoDong.Background_PIC_BigScreen;
                if (String.IsNullOrEmpty(strBackground_PIC_BigScreen) || Eggsoft.Common.FileFolder.RemoteFileExists(strBackground_PIC_BigScreen) == false) strBackground_PIC_BigScreen = "./Images/2.jpg";
                strBackground_SoundPath = System.Configuration.ConfigurationManager.AppSettings["UpLoadResourceURL"] + Model_tab_ShopClient_XianChangHuoDong.Background_SoundPath;
                if (String.IsNullOrEmpty(strBackground_SoundPath) || Eggsoft.Common.FileFolder.RemoteFileExists(strBackground_SoundPath) == false) strBackground_SoundPath = "./Mp3/Blank_NoAnySound.mp3";


                //TextShopClientID.Value = strShopClientID;
                #region 读取 主机的编号 。
                //string urlasmx = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"] + "/Pub/doWeiXianChang.asmx";
                //string[] args = new string[1];
                //args[0] = strShopClientID;// "/UpLoad/images/";
                //object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "doGetHuoDongNumberStatusIsTrue", args);
                //string strReturn = result.ToString();
                string strReturn =PubClass.Pub.doGetHuoDongNumberStatusIsTrue(strShopClientID);
                strXianChangHuoDongNumberbyShopClientID = strReturn.Split('#')[0];
                this.Title = Model_tab_ShopClient_XianChangHuoDong.ActivityName + "现场活动，by" + Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(Int32.Parse(strShopClientID)) + " 技术支持：微云基石";
                LiteralWeiXinHao.Text = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "WeiXinHao");//微信号   微现场可用：
                #endregion

                #region 重置所有手机端的 Help_01XianChangHuoDong_Main 编号
                doResetAllDataForUni(strShopClientID, strXianChangHuoDongNumberbyShopClientID);
                //string urlasmxdoResetAllDataForUni = System.Configuration.ConfigurationManager.AppSettings["ServicesURL_HelpMachine"] + "/01XianChangHuoDong/doWS_01XianChangHuoDong.asmx";
                //string[] argsdoResetAllDataForUni = new string[2];
                //argsdoResetAllDataForUni[0] = strShopClientID;// "/UpLoad/images/";
                //argsdoResetAllDataForUni[1] = strXianChangHuoDongNumberbyShopClientID;// "/UpLoad/images/";
                //object resultdoResetAllDataForUni = WebServiceHelper.WsCaller.InvokeWebService(urlasmxdoResetAllDataForUni, "doResetAllDataForUni", argsdoResetAllDataForUni);
                #endregion
                #region  logo  and  erweima  推广二维码


                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClientID));

                Literal_ShopClientName.Text = Model_tab_ShopClient.ShopClientName + "--";


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
                            Literal_Agent.Text = Model_tab_ShopClient.ShopClientName;

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
                            Literal_Agent.Text = strAgentTextName + ":" + Model_tab_ShopClient_Agent_.ShopName;

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