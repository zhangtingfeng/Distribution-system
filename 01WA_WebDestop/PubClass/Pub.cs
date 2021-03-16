using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _01WA_WebDestop.PubClass
{
    public class Pub
    {
        private static Object doGetHuoDongNumberStatusIsTrueLock = new Object();

        /// <summary>
        /// HelpMachine 当前 可以 进行的活动的编号   前端  可以 2秒调用一次   用户一旦得到编号 就去调用 活动 状态聊
        /// </summary>
        /// <returns></returns>
        public static String doGetHuoDongNumberStatusIsTrue(string strintShopClientID)
        {
            string strIP = HttpContext.Current.Request.UserHostAddress;
            string strReturn = "-1";
            try
            {
                lock (doGetHuoDongNumberStatusIsTrueLock)
                {
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel("ActivityState=1 and ShopClientID=" + strintShopClientID);



                    if (Model_tab_ShopClient_XianChangHuoDong == null)
                    {
                        strReturn = "-1";
                    }
                    else
                    {
                        string strXianChangHuoDongID = "";
                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                        EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel("IsDoing=1 and ShopClientID=" + strintShopClientID);
                        if (Model_tab_ShopClient_XianChangHuoDong_Number == null)
                        {
                            Model_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number();
                            Model_tab_ShopClient_XianChangHuoDong_Number.ShopClientID = Int32.Parse(strintShopClientID);
                            Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong.ID;
                            Model_tab_ShopClient_XianChangHuoDong_Number.IsDoing = 1;
                            int intID = BLL_tab_ShopClient_XianChangHuoDong_Number.Add(Model_tab_ShopClient_XianChangHuoDong_Number);

                            Model_tab_ShopClient_XianChangHuoDong_Number = BLL_tab_ShopClient_XianChangHuoDong_Number.GetModel(intID);
                            strXianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongNumberbyShopClientID.ToString();/// 可以登录
                        }
                        else
                        {
                            strXianChangHuoDongID = Model_tab_ShopClient_XianChangHuoDong_Number.XianChangHuoDongNumberbyShopClientID.ToString();/// 可以登录
                        }
                        strReturn = strXianChangHuoDongID + "#" + Model_tab_ShopClient_XianChangHuoDong.CountHowMany + "#" + Model_tab_ShopClient_XianChangHuoDong.LongShakeTime + "#" + Model_tab_ShopClient_XianChangHuoDong.MaxTracks;
                        Eggsoft.Common.debug_Log.Call_WriteLog("strReturn=" + strReturn + " strIP=" + strIP, "doGetHuoDongNumberStatusIsTrue");
                    }
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
            return strReturn;
            //return intErrorCode.ToString();
        }

    }
}