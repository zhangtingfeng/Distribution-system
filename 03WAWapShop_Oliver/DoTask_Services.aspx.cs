using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver
{
    public partial class DoTask_Services : System.Web.UI.Page
    {
        private static Object thisLock = new Object();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lock (thisLock)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog("本页面5分钟被执行一次", "每5分钟更新");
                        EggsoftWX.BLL.tab_DoTask_Services BLL_tab_DoTask_Services = new EggsoftWX.BLL.tab_DoTask_Services();
                        EggsoftWX.Model.tab_DoTask_Services Model_tab_DoTask_Services = BLL_tab_DoTask_Services.GetModel("TaskType='MornitorIfServiceWorking'");
                        Label_Count.Text = "“本页面5分钟被执行一次”" + Model_tab_DoTask_Services.TaskMemo;
                        Label_UpdateTime.Text = Model_tab_DoTask_Services.DoTime.ToString();


                        Model_tab_DoTask_Services.DoTime = DateTime.Now;
                        Model_tab_DoTask_Services.TaskMemo = (Int32.Parse(Model_tab_DoTask_Services.TaskMemo) + 1).ToString();
                        BLL_tab_DoTask_Services.Update(Model_tab_DoTask_Services);


                        #region sendEmail
                        System.Data.DataTable Data_DataTableEmail = BLL_tab_DoTask_Services.GetList("ID,TaskXML", "TaskType='SendEmail' and TaskIfDone=0 order by id asc").Tables[0];
                        for (int i = 0; i < Data_DataTableEmail.Rows.Count; i++)
                        {
                            string strID = Data_DataTableEmail.Rows[i]["ID"].ToString();
                            string strTaskXML = Data_DataTableEmail.Rows[i]["TaskXML"].ToString();

                            Eggsoft.Common.ClassEmail_Task myClassEmail_Task = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft.Common.ClassEmail_Task>(strTaskXML, System.Text.Encoding.UTF8);
                            Eggsoft.Common.OurSendEmail.SendEmail(myClassEmail_Task.Email_FromCityName, myClassEmail_Task.Email_To, myClassEmail_Task.Email_Subject, myClassEmail_Task.Email_Body);
                            BLL_tab_DoTask_Services.Update("TaskIfDone=1,DoTime='" + DateTime.Now + "'", "ID=" + strID);
                        }
                        #endregion


                        #region SendWeiXin_Template
                        EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();

                        System.Data.DataTable Data_DataTableWeiXin_Template = BLL_tab_DoTask_Services.GetList("ID,TaskXML,TaskMemo", "TaskType='SendWeiXin_Template' and TaskIfDone=0 order by id asc").Tables[0];
                        for (int i = 0; i < Data_DataTableWeiXin_Template.Rows.Count; i++)
                        {
                            string strID = Data_DataTableWeiXin_Template.Rows[i]["ID"].ToString();
                            string strTaskXML = Data_DataTableWeiXin_Template.Rows[i]["TaskXML"].ToString();
                            string strArrayList = Data_DataTableWeiXin_Template.Rows[i]["TaskMemo"].ToString();

                            string[] strUserList = strArrayList.Split(',');

                            Eggsoft.Common.ClassWeiXin_Task myClassWeiXin_Task = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft.Common.ClassWeiXin_Task>(strTaskXML, System.Text.Encoding.UTF8);

                            string strSendURL = myClassWeiXin_Task.SendURL;
                            string strJSON = myClassWeiXin_Task.JSON;

                            for (int j = 0; j < strUserList.Length; j++)
                            {
                                String strUserID = strUserList[j];
                                Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                                strJSON = strJSON.Replace("###openid###", Model_tab_User.OpenID).Replace("###NickName###", Model_tab_User.NickName).Replace("###userid###", strUserID);
                                String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                            }

                            BLL_tab_DoTask_Services.Update("TaskIfDone=1,DoTime='" + DateTime.Now + "'", "ID=" + strID);
                        }
                        #endregion


                        #region sendWeiXin
                        System.Data.DataTable Data_DataTableWeiXin = BLL_tab_DoTask_Services.GetList("ID,TaskXML", "TaskType='SendWeiXin' and TaskIfDone=0 order by id asc").Tables[0];
                        for (int i = 0; i < Data_DataTableWeiXin.Rows.Count; i++)
                        {
                            string strID = Data_DataTableWeiXin.Rows[i]["ID"].ToString();
                            string strTaskXML = Data_DataTableWeiXin.Rows[i]["TaskXML"].ToString();

                            Eggsoft.Common.ClassWeiXin_Task myClassWeiXin_Task = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft.Common.ClassWeiXin_Task>(strTaskXML, System.Text.Encoding.UTF8);
                            String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(myClassWeiXin_Task.SendURL, myClassWeiXin_Task.JSON);
                            if (strOK.ToLower().IndexOf("ok") != -1)
                            {
                            }
                            else if (strOK.ToLower().IndexOf("45015") != -1)
                            {

                                Eggsoft.Common.debug_Log.Call_WriteLog("亲，可能48小时内用户没有和微店交互！微信消息发送不成功" + myClassWeiXin_Task.JSON + "");
                            }
                            BLL_tab_DoTask_Services.Update("TaskIfDone=1,DoTime='" + DateTime.Now + "'", "ID=" + strID);
                        }
                        #endregion


                        #region  检查团购订单的 有效性
                        try
                        {
                            EggsoftWX.BLL.tab_TuanGou_Number BLL_tab_TuanGou_Number = new EggsoftWX.BLL.tab_TuanGou_Number();
                            EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                            EggsoftWX.BLL.tab_TuanGou_Partner BLL_tab_TuanGou_Partner = new EggsoftWX.BLL.tab_TuanGou_Partner();

                            System.Data.DataTable DataTableTuanGou_Number = BLL_tab_TuanGou_Number.GetList("IFFinshedCurMemberShip=0 and Efficacy=1 order by id asc").Tables[0];
                            for (int i = 0; i < DataTableTuanGou_Number.Rows.Count; i++)
                            {
                                string strNumberID = DataTableTuanGou_Number.Rows[i]["ID"].ToString();
                                string strShopClientID = DataTableTuanGou_Number.Rows[i]["ShopClientID"].ToString();
                                string strTuanGouID = DataTableTuanGou_Number.Rows[i]["TuanGouID"].ToString();
                                EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strTuanGouID));
                                #region 检查团的终止时间
                                if ((Model_tab_TuanGou.ChoiceMaxTimeLengthDoGroup == true) || (Model_tab_TuanGou.ChoiceWhenEndAllGroup == true))///
                                {

                                    bool boolMakeITdisefficacy = false;
                                    if (Model_tab_TuanGou.ChoiceWhenEndAllGroup == true)
                                    {
                                        TimeSpan span = (TimeSpan)(Model_tab_TuanGou.WhenEndAllGroup - DateTime.Now);
                                        if (span.TotalSeconds <= 0)
                                        {
                                            boolMakeITdisefficacy = true;
                                        }
                                    }
                                    if (Model_tab_TuanGou.ChoiceMaxTimeLengthDoGroup == true && boolMakeITdisefficacy == false)
                                    {
                                        #region  找到开团时间
                                        DateTime DateTimeDoGroupTime = DateTime.MinValue;

                                        System.Data.DataTable DataTableTuanGou_Number_Parter = BLL_tab_TuanGou_Partner.GetDataTable("1", "CreateTime", "and TuanGouIDNumber=" + strNumberID + " order by CreateTime desc");
                                        if (DataTableTuanGou_Number_Parter.Rows.Count > 0)
                                        {
                                            string strPayTime = DataTableTuanGou_Number_Parter.Rows[0]["CreateTime"].ToString();
                                            DateTime.TryParse(strPayTime, out DateTimeDoGroupTime);
                                        }
                                        #endregion
                                        Double intWillAddHour = double.Parse(Model_tab_TuanGou.MaxTimeLengthDoGroup.ToString());
                                        DateTimeDoGroupTime = DateTimeDoGroupTime.AddHours(intWillAddHour);

                                        TimeSpan span = (TimeSpan)(DateTimeDoGroupTime - DateTime.Now);
                                        if (span.TotalSeconds <= 0)
                                        {
                                            boolMakeITdisefficacy = true;
                                        }
                                    }
                                    if (boolMakeITdisefficacy == true)////失效的团
                                    {
                                        EggsoftWX.Model.tab_TuanGou_Number Model_tab_TuanGou_Number = BLL_tab_TuanGou_Number.GetModel(Int32.Parse(strNumberID));
                                        Model_tab_TuanGou_Number.UpdateTime = DateTime.Now;
                                        Model_tab_TuanGou_Number.Efficacy = false;
                                        BLL_tab_TuanGou_Number.Update(Model_tab_TuanGou_Number);

                                        #region sendweixin
                                        System.Data.DataTable Data_DataTableParter = BLL_tab_TuanGou_Partner.GetList("ID,OrderID,UserID", " TuanGouIDNumber=" + strNumberID + " and ShopClientID=" + strShopClientID + "  and TuanGouID=" + strTuanGouID + " order by id desc").Tables[0];
                                        for (int j = 0; j < Data_DataTableParter.Rows.Count; j++)
                                        {
                                            string strParterID = Data_DataTableParter.Rows[j]["ID"].ToString();
                                            string strOrderID = Data_DataTableParter.Rows[j]["OrderID"].ToString();
                                            string strUserID = Data_DataTableParter.Rows[j]["UserID"].ToString();

                                            GoodP_TuanGou.tellShopClientID_UserPayMoney_ByWeiXin_TuanGou_DisEfficacy(strUserID, Int32.Parse(strTuanGouID), Int32.Parse(strNumberID));
                                            GoodP_TuanGou.tellShopClientID_o2o_UserPayMoney_ByWeiXin_TuanGou_DisEfficacy(strUserID, Int32.Parse(strTuanGouID), Int32.Parse(strNumberID));
                                            GoodP_TuanGou.tell_UserIGetMoneyPayMoney_ByWeiXin_TuanGou_DisEfficacy(strUserID, Int32.Parse(strTuanGouID), Int32.Parse(strNumberID));
                                            GoodP_TuanGou.tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_TuanGou_DisEfficacy(Int32.Parse(strOrderID), strUserID, Int32.Parse(strTuanGouID), Int32.Parse(strNumberID));

                                        }


                                        #endregion
                                        //string strUpdate 
                                    }


                                }
                                #endregion 检查团的终止时间
                            }
                        }
                        catch (Exception eeee)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(eeee);
                        }
                        finally { }

                        #endregion


                        #region 执行订单的结算任务
                        ///这里 一天只执行一次
                        ///
                        DateTime myDateTime = DateTime.Now;

                        int[] intDoTimeList = { 3, 10 };//这样写是3点10分  结合任务 5分钟执行一次    //int[] intDoTimeList = { 4, 10 };//这样写是3点52分


                        if ((myDateTime.Hour > (intDoTimeList[0] - 1)) && (myDateTime.Hour <= intDoTimeList[0]))///这样写是3点钟
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("start", "Default_DoOrderShow");
                            int[] intTimeList = new int[2];
                            intTimeList[0] = intDoTimeList[1];
                            intTimeList[1] = intTimeList[0] + 4;

                            if ((myDateTime.Minute >= intTimeList[0]) && (myDateTime.Minute <= intTimeList[1]))    ///[0  4]    [38  42]             
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog("startTrue", "Default_DoOrderShow");
                                Response.Redirect("/Status/Default_DoOrderShow.aspx");
                            }
                        }

                        #endregion
                    }
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception euuuu)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(euuuu, "每5分钟更新");

                }
                finally { }
            }
        }
    }
}