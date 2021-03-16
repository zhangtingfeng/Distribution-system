using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Other._01XianChangHuoDong
{
    /// <summary>
    /// doWS_01XianChangHuoDong 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doWS_01XianChangHuoDong : System.Web.Services.WebService
    {
        private static string isEnableLock_CopyData = "isEnableLock_CopyData20160505";////复制 数据期间 不能 检测编号


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]

        public String doDelete_Help_01XianChangHuoDong_Main(string strintShopClientID)
        {

            lock (isEnableLock_CopyData)
            {
                ///服务器端修改了  大屏幕信息 这里 删除空白的编号 调用 信息
                ///
                EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();


                EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
                System.Data.DataTable DataTable_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetList("ShopClientID=" + strintShopClientID + " and (XianChangHuoDongNum_BeginTime is null or XianChangHuoDongNum_EndTime is null or XianChangHuoDongStatus=1)").Tables[0];
                for (int i = 0; i < DataTable_01XianChangHuoDong_Main.Rows.Count; i++)
                {
                    string strID = DataTable_01XianChangHuoDong_Main.Rows[i]["ID"].ToString();
                    string strXianChangHuoDongNum = DataTable_01XianChangHuoDong_Main.Rows[i]["XianChangHuoDongNum"].ToString();


                    BLL_Help_01XianChangHuoDong_Main.Delete(int.Parse(strID));
                    BLL_Help_01XianChangHuoDong_UserShake.Delete("ShopClientID=" + strintShopClientID + " and XianChangHuoDongNum=" + strXianChangHuoDongNum);
                }
            }


            return "1";
        }


      
    }
}
