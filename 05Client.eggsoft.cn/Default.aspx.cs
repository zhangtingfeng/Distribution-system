using Eggsoft.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace _05Client.eggsoft.cn
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int intddd = Int32.Parse("7777");
            //String stringmyWXRed_Debug = "<xml>\n<return_code><![CDATA[SUCCESS]]></return_code>\n<return_msg><![CDATA[参数错误:单号必须为33位以下的数字或字母]]></return_msg>\n<result_code><![CDATA[FAIL]]></result_code>\n<err_code><![CDATA[PARAM_ERROR]]></err_code>\n<err_code_des><![CDATA[参数错误:单号必须为33位以下的数字或字母]]></err_code_des>\n</xml>";
            //var fddddd = stringmyWXRed_Debug.XMLtoJsonDynamicObject();
            Eggsoft_Public_CL.GoodP.ReadHTTPjuheWuLiu(10293, 21);


            Eggsoft.Common.JsUtil.LocationNewHref("/ClientAdmin/default.aspx");
        }
    }
}