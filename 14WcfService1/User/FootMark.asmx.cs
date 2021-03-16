using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.User
{
    /// <summary>
    /// FootMark 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class FootMark : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        /// <summary>
        /// 用户足迹管理器 
        /// </summary>

        /// <returns></returns>
        public String _FootMark_Html5_Save()
        {
            string str = "";
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strUserID = context.QueryString["userid"];
                String strlongitude = context.QueryString["longitude"];
                String strlatitude = context.QueryString["latitude"];
                String strneedSetVisitInfo = context.QueryString["needSetVisitInfo"];
                strneedSetVisitInfo = HttpContext.Current.Server.UrlDecode(strneedSetVisitInfo);
                Eggsoft.Common.debug_Log.Call_WriteLog("_FootMark_Html5_Save" + strneedSetVisitInfo + " strUserID=" + strUserID + " strlongitude=" + strlongitude + " strlatitude=" + strlatitude, "longlat");


                string getBaiDugpsX = "";
                string getBaiDugpsY = "";

                Eggsoft.Common.GPS.getBaiDugps(strlatitude, strlongitude, out getBaiDugpsX, out getBaiDugpsY);
                string strrenderReverse = "https://api.map.baidu.com/geocoder/v2/?ak=D115c637a1d10e58c7ed20711db00cca&callback=renderReverse&location=" + getBaiDugpsX + "," + getBaiDugpsY + "&output=json&pois=1";
                string str_json_OpenID_userInfo = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strrenderReverse);
                int indexLeftPos = str_json_OpenID_userInfo.IndexOf('(');
                int indexRightPos = str_json_OpenID_userInfo.Length - 1;
                str_json_OpenID_userInfo = str_json_OpenID_userInfo.Substring(indexLeftPos + 1, indexRightPos - indexLeftPos - 1);
                Eggsoft_Public_CL.Pub_GetOpenID_And_.JsonCity myjson_api_map_baidu_com_geocoder = JsonHelper.JsonDeserialize<Eggsoft_Public_CL.Pub_GetOpenID_And_.JsonCity>(str_json_OpenID_userInfo);


                #region write DB
                EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat BLL_tab_ShopClient_User_Lng_Lat = new EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat();
                EggsoftWX.Model.tab_ShopClient_User_Lng_Lat Model_tab_ShopClient_User_Lng_Lat = new EggsoftWX.Model.tab_ShopClient_User_Lng_Lat();

                bool boolOneHour = BLL_tab_ShopClient_User_Lng_Lat.Exists("UserID=" + strUserID + " and lat='" + strlatitude + "' and lng='" + strlongitude + "' and aspxLocation='" + strneedSetVisitInfo + "'  and DateDiff(hh,UpdateTime,getDate())<=1");///1小时的不存盘
                //Eggsoft.Common.debug_Log.Call_WriteLog("boolOneHour" + boolOneHour, "longlat");

                if (false == boolOneHour)
                {


                    Model_tab_ShopClient_User_Lng_Lat.UserID = Int32.Parse(strUserID);
                    Model_tab_ShopClient_User_Lng_Lat.lat = strlatitude;
                    Model_tab_ShopClient_User_Lng_Lat.lng = strlongitude;
                    Model_tab_ShopClient_User_Lng_Lat.BaiDulat = getBaiDugpsX;
                    Model_tab_ShopClient_User_Lng_Lat.BaiDulng = getBaiDugpsY;
                    Model_tab_ShopClient_User_Lng_Lat.aspxDescription = strneedSetVisitInfo;

                    string strBaiDuAdress = "";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.addressComponent.province + " ";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.addressComponent.city + " ";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.addressComponent.district + " ";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.addressComponent.street + " ";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.business + " ";
                    strBaiDuAdress += myjson_api_map_baidu_com_geocoder.result.formatted_address;

                    //strBaiDuAdress+=myjson_api_map_baidu_com_geocoder.result.addressComponent.streetNumber+" ";

                    Model_tab_ShopClient_User_Lng_Lat.BaiDuAdress = strBaiDuAdress;
                    Model_tab_ShopClient_User_Lng_Lat.BaiDuAllAdress = str_json_OpenID_userInfo;

                    BLL_tab_ShopClient_User_Lng_Lat.Add(Model_tab_ShopClient_User_Lng_Lat);
                }
                #endregion write DB

                str = "{\"ErrorCode\":\"" + 1 + "\",\"BaiDugpsX\":\"" + getBaiDugpsX + "\",\"BaiDugpsY\":\"" + getBaiDugpsY + "\"";
                str += ",\"province\":\"" + myjson_api_map_baidu_com_geocoder.result.addressComponent.province + "\"";
                str += ",\"city\":\"" + myjson_api_map_baidu_com_geocoder.result.addressComponent.city + "\"";
                str += ",\"district\":\"" + myjson_api_map_baidu_com_geocoder.result.addressComponent.district + "\"";
                str += ",\"business\":\"" + myjson_api_map_baidu_com_geocoder.result.business + "\"";
                str += ",\"formatted_address\":\"" + myjson_api_map_baidu_com_geocoder.result.formatted_address + "\"";
                str += "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }

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
