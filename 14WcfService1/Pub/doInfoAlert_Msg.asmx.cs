using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doInfoAlert_Msg 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doInfoAlert_Msg : System.Web.Services.WebService
    {

        [WebMethod]
        public string doInfoAlert_MsgAction()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strdoInfoAlert_MsgAction = "";

            try
            {
                string strpub_Int_Session_CurUserID = context.QueryString["strpub_Int_Session_CurUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);

                if (pub_Int_Session_CurUserID > 0)
                {
                    EggsoftWX.BLL.tab_User_Message_NeedShow BLL_tab_User_Message_NeedShow = new EggsoftWX.BLL.tab_User_Message_NeedShow();
                    EggsoftWX.Model.tab_User_Message_NeedShow Model_tab_User_Message_NeedShow = BLL_tab_User_Message_NeedShow.GetModel("UserID=" + pub_Int_Session_CurUserID + " and isnull(IFShowed,0)=0 and Isdeleted=0");

                    if (Model_tab_User_Message_NeedShow != null)
                    {
                        strdoInfoAlert_MsgAction = Model_tab_User_Message_NeedShow.InfoNeedShow;
                        Model_tab_User_Message_NeedShow.IFShowed = true;
                        Model_tab_User_Message_NeedShow.UpdateBy = "被用户读取服务";
                        Model_tab_User_Message_NeedShow.UpdateTime = DateTime.Now;
                        BLL_tab_User_Message_NeedShow.Update(Model_tab_User_Message_NeedShow);
                    }
                }


                string str = "{\"InfoMsg\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(strdoInfoAlert_MsgAction) + "\"}";
                Eggsoft.Common.debug_Log.Call_WriteLog(str, "消息提醒功能", "服务返回消息");

                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "doInfoAlert_Msg", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doInfoAlert_Msg", "程序报错");
            }
            finally
            {

            }
            return strdoInfoAlert_MsgAction;
        }
    }
}
