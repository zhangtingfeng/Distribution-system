using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.SSQ_3D
{
    public partial class DoTask_Services_930_Everyday : System.Web.UI.Page
    {
        public class pReturnObj
        {
            public string Name { get; set; }
            public string HaoMa { get; set; }
            public string QiShu { get; set; }
            public string BonusDay { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string urlasmx = "http://Function.o2o10000.cn/01GetShuangSeQiu/do01GetSSQWS.asmx";

            Eggsoft.Common.debug_Log.Call_WriteLog("开始执行", "一元云购程序");
            try
            {
                EggsoftWX.BLL.tab_HelpSSQ_3D BLL_tab_HelpSSQ_3D = new EggsoftWX.BLL.tab_HelpSSQ_3D();
                string strSSQWhere = "Convert(varchar(10),[CreateTime],120) = Convert(varchar(10),getDate(),120) and Name='双色球' and HaoMa is not null and QiShu is not null";
                string str3DWhere = "Convert(varchar(10),[CreateTime],120) = Convert(varchar(10),getDate(),120) and Name='3D' and HaoMa is not null and QiShu is not null";
                if (BLL_tab_HelpSSQ_3D.Exists(strSSQWhere) == false || BLL_tab_HelpSSQ_3D.Exists(str3DWhere) == false)
                {
                    string[] args = new string[2];

                    try
                    {
                        args[0] = "Oliver";// "/UpLoad/images/";
                        args[1] = "33";// "/UpLoad/images/";
                        object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "do_01GetShuangSeQiu", args);
                        string strresult = result.toString();

                        //#region 推送微信短消息
                        //string strServicesURL = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"];

                        //string urlSendMessageasmx = strServicesURL + "/Pub/doWS_GetWeiXinSign.asmx";
                        //string strPrivateKey = "546354683465907u34u6938465938746905834756";
                        //String md5DESCrypt = Eggsoft.Common.DESCrypt.GetMd5Str32(strresult + strPrivateKey);

                        //string[] argsSendMessage = new string[2];
                        //argsSendMessage[0] = strresult;// "/UpLoad/images/";
                        //argsSendMessage[1] = md5DESCrypt;// "/UpLoad/images/";
                        //WebServiceHelper.WsCaller.InvokeWebService(urlSendMessageasmx, "PubSendWeiXinMessage", argsSendMessage);
                        //Eggsoft.Common.debug_Log.Call_WriteLog("发送微信通知", "一元云购程序");
                        //#endregion

                    }
                    catch (Exception eeee)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "一元云购程序");
                        System.Threading.Thread.Sleep(60000);
                    }




                    #region 双色球

                    if (BLL_tab_HelpSSQ_3D.Exists(strSSQWhere) == false)
                    {
                        args[0] = "Oliver";// "/UpLoad/images/";
                        args[1] = "31";// "/UpLoad/images/";
                        object resultSSQ = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "do_01GetShuangSeQiu", args);
                        string strresultSSQ = resultSSQ.ToString();
                        pReturnObj mypReturnObjSSQ = Eggsoft.Common.JsonHelper.JsonDeserialize<pReturnObj>(strresultSSQ);
                       
                        if (String.IsNullOrEmpty(mypReturnObjSSQ.HaoMa) == false && String.IsNullOrEmpty(mypReturnObjSSQ.QiShu) == false)
                        {
                            EggsoftWX.Model.tab_HelpSSQ_3D Model_tab_HelpSSQ_3D = new EggsoftWX.Model.tab_HelpSSQ_3D();
                            Model_tab_HelpSSQ_3D.Name = mypReturnObjSSQ.Name;
                            Model_tab_HelpSSQ_3D.HaoMa = mypReturnObjSSQ.HaoMa;
                            Model_tab_HelpSSQ_3D.QiShu = mypReturnObjSSQ.QiShu;
                            Model_tab_HelpSSQ_3D.BonusDay = mypReturnObjSSQ.BonusDay;
                            BLL_tab_HelpSSQ_3D.Add(Model_tab_HelpSSQ_3D);
                        }
                    }
                    #endregion

                    #region 3D
                    if (BLL_tab_HelpSSQ_3D.Exists(str3DWhere) == false)
                    {
                        args[1] = "32";// "/UpLoad/images/";
                        object result3D = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "do_01GetShuangSeQiu", args);
                        string strresult3D = result3D.ToString();
                        pReturnObj mypReturnObj3D = Eggsoft.Common.JsonHelper.JsonDeserialize<pReturnObj>(strresult3D);

                        if (String.IsNullOrEmpty(mypReturnObj3D.HaoMa) == false && String.IsNullOrEmpty(mypReturnObj3D.QiShu) == false)
                        {
                            EggsoftWX.Model.tab_HelpSSQ_3D Model_tab_HelpSSQ_3D = new EggsoftWX.Model.tab_HelpSSQ_3D();
                            Model_tab_HelpSSQ_3D = new EggsoftWX.Model.tab_HelpSSQ_3D();
                            Model_tab_HelpSSQ_3D.Name = mypReturnObj3D.Name;
                            Model_tab_HelpSSQ_3D.HaoMa = mypReturnObj3D.HaoMa;
                            Model_tab_HelpSSQ_3D.QiShu = mypReturnObj3D.QiShu;
                            Model_tab_HelpSSQ_3D.BonusDay = mypReturnObj3D.BonusDay;
                            BLL_tab_HelpSSQ_3D.Add(Model_tab_HelpSSQ_3D);
                        }
                    }
                    #endregion

                    Eggsoft.Common.debug_Log.Call_WriteLog("处理结束", "一元云购程序");
                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "一元云购程序");
            }
        }
    }
}





















//#region 推送微信短消息
//string strServicesURL = System.Configuration.ConfigurationManager.AppSettings["ServicesURL"];

//string urlSendMessageasmx = strServicesURL + "/Pub/doWS_GetWeiXinSign.asmx";
//string strPrivateKey = "546354683465907u34u6938465938746905834756";
//String md5DESCrypt = Eggsoft.Common.DESCrypt.GetMd5Str32(strresult + strPrivateKey);

//string[] argsSendMessage = new string[2];
//argsSendMessage[0] = strresult;// "/UpLoad/images/";
//argsSendMessage[1] = md5DESCrypt;// "/UpLoad/images/";
//WebServiceHelper.WsCaller.InvokeWebService(urlSendMessageasmx, "PubSendWeiXinMessage", argsSendMessage);
//Eggsoft.Common.debug_Log.Call_WriteLog("发送微信通知", "一元云购程序");
//#endregion