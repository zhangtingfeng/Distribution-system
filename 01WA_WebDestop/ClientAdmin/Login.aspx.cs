using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin
{
    public partial class Login : System.Web.UI.Page
    {
        EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        EggsoftWX.BLL.tab_ShopClient_AdminUser bll_tab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string type = Request.QueryString["Act"];
                    if (type != null)
                    {
                        if (type.ToString() == "gotouserFrom_Admin")
                        {
                            //return;////暂时关闭该功能
                            string stringUserID = Request.QueryString["UserID"];
                            stringUserID = stringUserID.Replace("oliver", "");///admin login
                            //Eggsoft.Common.CommAuthen.Client_SetAuthenAdmin(stringUserID);
                            pubLogin(stringUserID, "gotouserFrom_Adminoliver");
                            Eggsoft.Common.JsUtil.LocationNewHref("default.aspx");
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "pubLogin");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string strcom_VCode = Eggsoft.Common.Session.Read("m._ShangHaiDianzi__VCode");
                string strtxtValidCode = txtValidCode.Text.Trim();

                if (strcom_VCode != strtxtValidCode)
                    Eggsoft.Common.JsUtil.ShowMsg("验证码错误!", "Login.aspx");
                AdminLogin(txtUserID, txtUserPass);

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "btnLogin_Click", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "btnLogin_Click");
            }
        }

        public void AdminLogin(TextBox txtUserID, TextBox txtUserPass)
        {
            try
            {
                string SourUserID = txtUserID.Text.Trim();
                string SourUserPass = txtUserPass.Text.Trim();

                if (SourUserID == "liu" && SourUserPass == "liu000") {
                    pubLogin("34", "liu");
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "pubLogin");
            }
        }
        protected void btnReg_Click(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("/ShopReg/ShopReg.aspx");
        }



        private bool ifFunctionCanContinus(int intShopClientID, out string infoMoneyTipInfo)
        {

            infoMoneyTipInfo = "";

            try
            {

                string strShopClientIDShow = "ShopClient" + intShopClientID.toString();

                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                string strReadjson1 = Eggsoft.Common.JsonHelper.GetFileJson("~/ClientAdmin/FunctionCanContinus.json");
                dynamic modelDy = js.Deserialize<dynamic>(strReadjson1);
                bool boolExsit = ((IDictionary<string, object>)modelDy).ContainsKey(strShopClientIDShow);
                if (boolExsit)////说明存在限制条件
                {
                    string strState = modelDy[strShopClientIDShow]["State"];
                    DateTime? DateTimeTimeStart = ((String)modelDy[strShopClientIDShow]["TimeStart"]).toDateTime();
                    DateTime? DateTimeTimeEnd = ((String)modelDy[strShopClientIDShow]["TimeEnd"]).toDateTime();
                    String strInfo = modelDy[strShopClientIDShow]["Info"];
                    infoMoneyTipInfo = strInfo + " 开始时间:" + modelDy[strShopClientIDShow]["TimeStart"] + " 结束时间:" + modelDy[strShopClientIDShow]["TimeEnd"];

                    if (strState == "Pause")///维护期间，该时间段暂停提现，请注意商城公告
                    {////Normal  表示不暂停

                        if ((DateTimeTimeStart <= DateTime.Now && DateTimeTimeEnd >= DateTime.Now))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (strState == "Normal")
                    {////表示这个时间段才能提现
                        if (!(DateTimeTimeStart <= DateTime.Now && DateTimeTimeEnd >= DateTime.Now))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd, "提现文件读取错误");
                infoMoneyTipInfo = "出现传输错误，请稍后重拾";
                return false;
            }
            return true;
        }

        private void pubLogin(string strShopClientID, string strAdminAccount)
        {
            try
            {
                DateTime? out_AuthorTime = DateTime.MinValue;
                string strAlertDayScript = "";
                int span_Days = Eggsoft_Public_CL.ClassP.CheckAuthorTime(strShopClientID, out strAlertDayScript, out out_AuthorTime);



                if (span_Days >= 0)
                {
                    string strAlertContinusScript = "";
                    bool boolifFunctionCanContinus = ifFunctionCanContinus(strShopClientID.toInt32(), out strAlertContinusScript);
                    if (boolifFunctionCanContinus)
                    {
                        Eggsoft.Common.CommAuthen.Client_SetAuthenAdmin(strShopClientID);
                        Eggsoft.Common.CommAuthen.Client_SetAuthenAdmin_ClientUserAccount(strAdminAccount);
                        string strBackUrl = Request.QueryString["BackUrl"];
                        if (String.IsNullOrEmpty(strBackUrl) == false)
                        {
                            strBackUrl = "default.aspx?BackUrl=" + strBackUrl;
                        }
                        else
                        {
                            strBackUrl = "default.aspx";
                        }
                        Eggsoft.Common.debug_Log.Call_WriteLog("商户登陆成功", "商户登陆成功strShopClientID=" + strShopClientID, strAdminAccount);
                        Eggsoft.Common.JsUtil.ShowMsg("商户登陆成功!" + strAlertDayScript, strBackUrl);
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg(strAlertContinusScript + "登陆失败,系统维护中,感谢你的支持,谢谢!!", -1);
                    }
                }

                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("登陆失败,请数据维护中,感谢你的支持", "合约商户失效登陆strShopClientID=" + strShopClientID, strAdminAccount);

                    Eggsoft.Common.JsUtil.ShowMsg("登陆失败,请数据维护中,感谢你的支持,谢谢!!", -1);
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "pubLogin", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "pubLogin");
            }
        }
    }




}