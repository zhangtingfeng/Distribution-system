using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _11WA_ClientShop.AddFunction._01Shake_Parter.data
{
    /// <summary>
    /// WService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        /// <summary>
        /// 校验用户名 密码 
        /// </summary>
        /// <returns></returns>
        public String doac_search999___()
        {
            string str = "";
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("调用频度检测doverify", "05XianChangHuoDong");

                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strscene_id = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["scene_id"]);
                String strpassword = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["password"]);


                str = "{\"ret\":0,\"data\":1";
                str += "}";
                //if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                //{
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                Response.ContentType = "application/json";
                string callback = Request["jsonp"];
                //Response.Write(callback + "(" + str + ")");
                Response.Write(str);
                Response.End();//结束后续的操作，直接返回所需要的字符串
                //}

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
            return str;
            //return intErrorCode.ToString();
        }

    }
}
