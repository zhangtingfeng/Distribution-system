using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///Subscribe 的摘要说明
    /// </summary>
    public class WX_Text
    {

        //public static string Call_Image(string strXML)
        //{

        //    String strResponseText = "";

        //    //先是否是关键词 然后做对话
        //    try
        //    {
        //        WX_Model.WX_Model_Image myWX_Model = new WX_Model.WX_Model_Image();
        //        myWX_Model = myWX_Model.GetWX_Model_Image(strXML);

        //        EggsoftWX.BLL.tab_TransMessage_In_WeinXinPlatform BLL_tab_TransMessage_In_WeinXinPlatform = new EggsoftWX.BLL.tab_TransMessage_In_WeinXinPlatform();
        //        EggsoftWX.Model.tab_TransMessage_In_WeinXinPlatform Model_tab_TransMessage_In_WeinXinPlatform = new EggsoftWX.Model.tab_TransMessage_In_WeinXinPlatform();


        //        WX_Model.WX_Model_Parent myWX_Parent = WX_Parent.Call_Parent(strXML);
        //        string strUserID = Pub.GetUserIDFromOpenID(myWX_Parent.FromUserName);

        //        //没有任何回复 的 我们试试客服消息
        //        //检查48小时内 最大的ID
        //        string strSQL = "datediff(hh,UpdateTime,getdate())<= 48";

        //        string struseSQL = "FromUserId=" + strUserID + " and " + strSQL + " and IFClosedTalked=0";
        //        string strToUserID = "";
        //        bool mybool = BLL_tab_TransMessage_In_WeinXinPlatform.Exists(struseSQL);
        //        if (mybool)
        //        {
        //            struseSQL = struseSQL + " order by UpdateTime desc";
        //            System.Data.DataTable myDataTable = BLL_tab_TransMessage_In_WeinXinPlatform.GetList(struseSQL).Tables[0];
        //            strToUserID = myDataTable.Rows[0]["ToUserId"].ToString();

        //            // strResponseText = "小神马在此恭候多时了，微店已记录您的信息!";

        //            //string strToContent = strKeyList + "\n\n";
        //            //strToContent += "以上是" + Pub.GetNickName(strUserID) + "向您发送的消息。回复" + strUserID + "#文字内容，可以直接与其对话！。\n只要你在48小时内访问微店的公众平台，你都能得到我们的消息回复！" + "\n";

        //            Pub_GetOpenID_And_.SendTextWinXinMessage_Image(Int32.Parse(strToUserID), Int32.Parse(strUserID), myWX_Model.MediaId, tab_System_And_.getTab_System("CityName") + "微店转发信息");

        //            //                   strResponseText = Pub_DeMode.Get_Message_(strXML, "31", "0");
        //            strResponseText = Pub_DeMode.Get_Message_(strXML, "31", "0");

        //        }
        //    }
        //    catch
        //    {
        //        strResponseText = "";
        //    }
        //    finally
        //    {
        //    }



        //    return strResponseText;

        //}


        //public static string Call_Voice(string strXML)
        //{

        //    String strResponseText = "";

        //    //先是否是关键词 然后做对话
        //    try
        //    {
        //        WX_Model.WX_Model_Voice myWX_Model = new WX_Model.WX_Model_Voice();
        //        myWX_Model = myWX_Model.Get_Model_Voice(strXML);

        //        EggsoftWX.BLL.tab_TransMessage_In_WeinXinPlatform BLL_tab_TransMessage_In_WeinXinPlatform = new EggsoftWX.BLL.tab_TransMessage_In_WeinXinPlatform();
        //        EggsoftWX.Model.tab_TransMessage_In_WeinXinPlatform Model_tab_TransMessage_In_WeinXinPlatform = new EggsoftWX.Model.tab_TransMessage_In_WeinXinPlatform();


        //        WX_Model.WX_Model_Parent myWX_Parent = WX_Parent.Call_Parent(strXML);
        //        string strUserID = Pub.GetUserIDFromOpenID(myWX_Parent.FromUserName);

        //        //没有任何回复 的 我们试试客服消息
        //        //检查48小时内 最大的ID
        //        string strSQL = "datediff(hh,UpdateTime,getdate())<= 48";

        //        string struseSQL = "FromUserId=" + strUserID + " and " + strSQL + " and IFClosedTalked=0";
        //        string strToUserID = "";
        //        bool mybool = BLL_tab_TransMessage_In_WeinXinPlatform.Exists(struseSQL);
        //        if (mybool)
        //        {
        //            struseSQL = struseSQL + " order by UpdateTime desc";
        //            System.Data.DataTable myDataTable = BLL_tab_TransMessage_In_WeinXinPlatform.GetList(struseSQL).Tables[0];
        //            strToUserID = myDataTable.Rows[0]["ToUserId"].ToString();

        //            Pub_GetOpenID_And_.SendTextWinXinMessage_Voice(Int32.Parse(strToUserID), Int32.Parse(strUserID), myWX_Model.MediaId, tab_System_And_.getTab_System("CityName") + "微店转发信息");

        //            strResponseText = Pub_DeMode.Get_Message_(strXML, "31", "0");
        //        }
        //    }
        //    catch
        //    {
        //        strResponseText = "";
        //    }
        //    finally
        //    {
        //    }



        //    return strResponseText;

        //}





        public static string Call_Text(string strShopClientID, string strXML)
        {

            String strResponseText = "";

            //先是否是关键词 然后做对话
            try
            {

                WX_Model.WX_Model_Text myWX_Model = new WX_Model.WX_Model_Text();
                myWX_Model = myWX_Model.GeWX_Model_Text(strXML);

                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

                bool bool_Like_Or_Same = Model_tab_ShopClient_EngineerMode.RadioButtonList_Like_Or_Same;

                String strWherekey = "";
                if (bool_Like_Or_Same)//精确匹配
                {
                    strWherekey = "Marker='" + myWX_Model.Content + "'";
                }
                else
                {
                    strWherekey = "Marker like '%" + myWX_Model.Content + "%'";
                }
                EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer myBLLBLL_tab_ShopClient_EngineerMode_KeyAnswer = new EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer();
                bool exitKey = myBLLBLL_tab_ShopClient_EngineerMode_KeyAnswer.Exists(strWherekey);
                if (exitKey)
                {
                    String strMarkerContent = myBLLBLL_tab_ShopClient_EngineerMode_KeyAnswer.GetList("markerContent", strWherekey).Tables[0].Rows[0][0].ToString();
                    string[] strList = strMarkerContent.Split(new char[5] { '#', '$', '#', '$', '#' }, StringSplitOptions.RemoveEmptyEntries);
                    string strType = strList[0];
                    string strResourceID = strList[1];
                    strResponseText = Pub_DeMode.Get_Message_(strXML, strType, strResourceID, false, Int32.Parse(strShopClientID));
                }
                else
                {
                    strResponseText = Eggsoft_Public_CL.WX_Subscribe.Call_KeyAnswer_Default(Int32.Parse(strShopClientID), strXML);
                }
                Eggsoft.Common.debug_Log.Call_WriteLog(strResponseText,"微信用户交互");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);

                strResponseText = "免费代理微店，0投资0风险，不用囤货，不用发货，公司帮你一切搞定。。\n" + tab_System_And_.getTab_System("CityName") + "微店，每天24小时，每年365天，任何时候都在为客户服务。";
            }
            finally
            {
            }



            return strResponseText;

        }

    }
}