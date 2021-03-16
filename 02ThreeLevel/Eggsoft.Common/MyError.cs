using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class MyError
    {
        public static void ThrowException(string msg)
        {
            throw new Exception(msg + "<br/>" + HttpContext.Current.Request.ServerVariables["path_translated"]);
        }
    }
}
