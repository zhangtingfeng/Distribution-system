using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver._03WAWapShop_Oliver
{
    public partial class WebForm_FenQian_ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (false)////暂不上线
            {
                //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                //EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();

            }
                //#region 今天是否推送过同类型消息
                //bool myTodayInfo_ZhangHuYuE = bll_b011_InfoAlertMessage.Exists("UserID=@UserID and ShopClient_ID=@ShopClient_ID and Type='Info_ZhangHuYuE' and DateDiff(dd,CreatTime,getdate())=0", 41192,21);
                //if (!myTodayInfo_ZhangHuYuE)
                //{
                //    Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                //    Model_b011_InfoAlertMessage.InfoTip = "Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge";
                //    Model_b011_InfoAlertMessage.CreateBy = "系统分红";
                //    Model_b011_InfoAlertMessage.UpdateBy = "系统分红";
                //    Model_b011_InfoAlertMessage.UserID = 41192;
                //    Model_b011_InfoAlertMessage.ShopClient_ID = 21;
                //    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                //    Model_b011_InfoAlertMessage.TypeTableID = 4545;
                //    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                //}
                //#endregion 今天是否推送过同类型消息


                //#region 每天运营中心加权分红
                //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
                //Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(21);
                //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");

                //string strFilePath = "~/File/21do" + DateTime.Now.ToString("yyyyMMdd") + "YunYinZhongXin28Action.txt";
                //Response.Write(Eggsoft.Common.FileFolder.ReadFile((strFilePath)));

                //#endregion 每天运营中心加权分红

                /*
                 21:31:16分红出现负值，请手动处理追回strUserID=41210 OrderDetailID9859出现负值，请手动处理追回strUserID=43113 OrderDetailID9871出现负值，请手动处理追回strUserID=42985 OrderDetailID9878出现负值，请手动处理追回strUserID=42985 OrderDetailID9893出现负值，请手动处理追回strUserID=43120 OrderDetailID9901出现负值，请手动处理追回strUserID=43215 OrderDetailID9946出现负值，请手动处理追回strUserID=43209 OrderDetailID9947出现负值，请手动处理追回strUserID=42985 OrderDetailID9980出现负值，请手动处理追回strUserID=42985 OrderDetailID9985出现负值，请手动处理追回strUserID=42985 OrderDetailID10000出现负值，请手动处理追回strUserID=41192 OrderDetailID10015出现负值，请手动处理追回strUserID=42985 OrderDetailID10025出现负值，请手动处理追回strUserID=42985 OrderDetailID10030出现负值，请手动处理追回strUserID=43327 OrderDetailID10094出现负值，请手动处理追回strUserID=42985 OrderDetailID10106出现负值，请手动处理追回strUserID=43209 OrderDetailID10124出现负值，请手动处理追回strUserID=43209 OrderDetailID10133出现负值，请手动处理追回strUserID=43120 OrderDetailID10216出现负值，请手动处理追回strUserID=43217 OrderDetailID10238出现负值，请手动处理追回strUserID=46162 OrderDetailID11020出现负值，请手动处理追回strUserID=43327 OrderDetailID11080出现负值，请手动处理追回strUserID=46259 OrderDetailID11105出现负值，请手动处理追回strUserID=41192 OrderDetailID11123出现负值，请手动处理追回strUserID=44323 OrderDetailID11506出现负值，请手动处理追回strUserID=46259 OrderDetailID11787

                 */


            }
    }
}