using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class DotConfig
    {
        public static string GetAppSettingsValue(string key)
        {
            string retValue="";
            retValue = ConfigurationManager.AppSettings["Eggsoft_" + key];
            if (retValue == "")
                throw new Exception("Web.config 错误!");
            return retValue;
        }

    }
}
