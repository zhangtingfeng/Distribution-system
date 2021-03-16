using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;



namespace Eggsoft_Public_CL
{

    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class Pub_PC
    {
        public Pub_PC()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public static void setAllNeedIDPCGetSession()
        {
            if (string.IsNullOrEmpty(Eggsoft.Common.Session.Read("ShopClientID")))
            {
                string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing("PCYuMing");
                Eggsoft.Common.Session.Add("ShopClientID", strShopClientID);
            }
            if (string.IsNullOrEmpty(Eggsoft.Common.Session.Read("pub_Int_Session_CurUserID")))
            {///检查前端是否因为扫描传来 PCOpenID
                string strPCOpenID = HttpContext.Current.Request.QueryString["PCOpenID"];///js 检测到了 并调用
                Eggsoft.Common.Session.Add("PCOpenID", strPCOpenID);
                //if (string.IsNullOrEmpty(strPCOpenID) == false)
                //{
                string strUserID = Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(strPCOpenID);
                Eggsoft.Common.Session.Add("pub_Int_Session_CurUserID", strUserID);

                int intShopClientID = Int32.Parse(Eggsoft.Common.Session.Read("ShopClientID"));
                int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(intShopClientID);
                Eggsoft.Common.Session.Add("pInt_QueryString_ParentID", pInt_QueryString_ParentID.ToString());
                string pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(Int32.Parse(strUserID), intShopClientID);
                Eggsoft.Common.Session.Add("pub_GetAgentShopName_From_Visit__", pub_GetAgentShopName_From_Visit__);
                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(Int32.Parse(strUserID));
                Eggsoft.Common.Session.Add("Pub_Agent_Path", Pub_Agent_Path);
                Eggsoft.Common.Session.Add("pub_Int_Session_CurUserID", strUserID);
            }
            else
            { ///判断是否访问别人的店铺。。。。根据网址判断
                string strPCparentagentid = HttpContext.Current.Request.QueryString["parentagentid"];///js 检测到了 并调用
                int intPCparentagentid = 0;
                int.TryParse(strPCparentagentid, out intPCparentagentid);
                if (intPCparentagentid > 0)///确实是别人的店铺  自己是游客
                {
                    if (string.IsNullOrEmpty(Eggsoft.Common.Session.Read("pub_GetAgentShopName_From_Visit__")))
                    {
                        int intShopClientID = Int32.Parse(Eggsoft.Common.Session.Read("ShopClientID").ToString());
                        Eggsoft.Common.Session.Add("pInt_QueryString_ParentID", strPCparentagentid);
                        string pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(intPCparentagentid, intShopClientID);
                        string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intPCparentagentid);
                        Eggsoft.Common.Session.Add("pub_GetAgentShopName_From_Visit__", pub_GetAgentShopName_From_Visit__);
                        Eggsoft.Common.Session.Add("Pub_Agent_Path", Pub_Agent_Path);
                    }
                }
            }


        }

        public static String getShopIcon(int intpub_Int_ShopClientID)
        {
            string strreturnPub_ShopClientWeiXinPingTaiErWeiMa = "";
            string CacheKey = "getShopIcon" + intpub_Int_ShopClientID;
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = bll_tab_ShopClient.GetModel(intpub_Int_ShopClientID);
                    Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);
                    string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;
                    string strPub_ShopClientWeiXinPingTaiErWeiMa = XML__Class_Shop_Client.WeiXinGongZhongPingTaiErWeiMaIMG;
                    strreturnPub_ShopClientWeiXinPingTaiErWeiMa = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strPub_ShopClientWeiXinPingTaiErWeiMa;
                    Eggsoft.Common.DataCache.SetCache(CacheKey, strreturnPub_ShopClientWeiXinPingTaiErWeiMa);// 写入缓存   
                }
                catch { }
            }
            else
            {
                strreturnPub_ShopClientWeiXinPingTaiErWeiMa = (string)objType;
            }
            return strreturnPub_ShopClientWeiXinPingTaiErWeiMa;
        }
    }
}