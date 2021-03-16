using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _06ThirdplatForm.eggsoft.cn.wxurl
{
    public partial class ModeD : System.Web.UI.Page
    {
        private static object ojbLock = new object();
        private string strPubAppID = "";  //你的token
        private string strPubToken = "";  //你的token
        private string strEncodingAESKey = "";  //你的token
        protected void Page_Load(object sender, EventArgs e)
        {
            //            string strDebug = @"<xml><ToUserName><![CDATA[gh_788c731cfeda]]></ToUserName>
            //<FromUserName><![CDATA[oPCaiwi6hegLWjQRxb83AZLkAgM0]]></FromUserName>
            //<CreateTime>1499814453</CreateTime>
            //<MsgType><![CDATA[event]]></MsgType>
            //<Event><![CDATA[subscribe]]></Event>
            //<EventKey><![CDATA[]]></EventKey>
            //</xml>";
            //            getResponseMsg(strDebug);
            //            return;
            if (!IsPostBack)
            {

                try
                {
                    String strShopClientID = Request.QueryString["ShopClientID"];
                    Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID=" + strShopClientID, "微信交互");


                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                    Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

                    if (Model_tab_ShopClient_EngineerMode != null)
                    {
                        strPubToken = Model_tab_ShopClient_EngineerMode.Token;
                        strPubAppID = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                        strEncodingAESKey = Model_tab_ShopClient_EngineerMode.EncodingAESKey;
                    }
                    string postStr = "";
                    string strOutValidQQToolCheck = "";
                    if (CheckSignature())
                    {
                        if (ValidQQToolCheck(out strOutValidQQToolCheck) == true)//不是接口验证  是的话 已经停止 向下执行。
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("httpFullUrl_BeforeUrlRewriting=" + Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting());
                        }
                        else
                        {
                            if (Request.HttpMethod.ToLower() == "post")//post提交的都是消息推送
                            {
                                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                                byte[] b = new byte[s.Length];
                                s.Read(b, 0, (int)s.Length);
                                postStr = Encoding.UTF8.GetString(b);
                                if (!string.IsNullOrEmpty(postStr))
                                {
                                    //EggsoftWX.BLL.tab_ShopClient_EngineerMode
                                    //公众平台上开发者设置的token, appID, EncodingAESKey
                                    string url = HttpContext.Current.Request.Url.Host; //获取 域名：
                                    string strgetResponseMsg = "";
                                    if (url == "localhost")
                                    {
                                        strgetResponseMsg = getResponseMsg(postStr);
                                        Response.Write(strgetResponseMsg);
                                    }
                                    else
                                    {
                                        string sToken = strPubToken;
                                        string sAppID = strPubAppID;
                                        string sEncodingAESKey = strEncodingAESKey;

                                        Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
                                        string sReqMsgSig = Request.QueryString["msg_signature"];
                                        string sReqTimeStamp = Request.QueryString["timestamp"];
                                        string sReqNonce = Request.QueryString["nonce"];
                                        string sReqData = postStr;
                                        string sMsg = "";  //解析之后的明文
                                        int ret = 0;
                                        ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);
                                        if (ret != 0)
                                        {
                                            return;
                                        }

                                        strgetResponseMsg = getResponseMsg(sMsg);

                                        //string sRespData = strgetResponseMsg;
                                        //"<xml><ToUserName><![CDATA[mycreate]]></ToUserName><FromUserName><![CDATA[wx582测试一下中文的情况，消息长度是按字节来算的396d3bd56c7]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[this is a test]]></Content><MsgId>1234567890123456</MsgId></xml>";
                                        string sEncryptMsg = ""; //xml格式的密文
                                        ret = wxcpt.EncryptMsg(strgetResponseMsg, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
                                        Response.Write(sEncryptMsg);

                                    }


                                }
                            }

                        }
                        //}
                    }
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "微信模式交互认证", "线程异常");
                }
                catch (Exception ee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ee, "微信模式交互认证");
                }
                finally { }
            }
        }
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string url = HttpContext.Current.Request.Url.Host; //获取 域名：
            if (url == "localhost") return true;//跳过验证 

            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            if (String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(timestamp) || String.IsNullOrEmpty(nonce)) return false;

            string[] ArrTmp = { strPubToken, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();



            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidQQToolCheck(out string strOutValidQQToolCheck)//接口验证
        {
            bool mybool = false;
            strOutValidQQToolCheck = "";

            string url = HttpContext.Current.Request.Url.Host; //获取 域名：
            if (url == "localhost") return false;//跳过接口验证 


            try
            {
                string strstrValidQQToolCheck = "";
                string QueryStringechoStr = Request.QueryString["echoStr"];
                if (QueryStringechoStr != null)//是进行接口验证
                {
                    string echoStr = QueryStringechoStr.ToString();

                    if (!string.IsNullOrEmpty(echoStr))
                    {
                        mybool = true;
                        strstrValidQQToolCheck = (echoStr);
                        strOutValidQQToolCheck = strstrValidQQToolCheck;
                        Response.Write(strOutValidQQToolCheck);
                    }
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }
            return mybool;
        }





        /// <summary>
        /// 返回信息结果(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private string getResponseMsg(string weixinXML)
        {
            string strgetResponseMsg = "";

            //lock (ojbLock)
            //{
            Eggsoft.Common.debug_Log.Call_WriteLog("WeiXinResponseMsg0001" + weixinXML);

            try
            {
                String strShopClientID = Request.QueryString["ShopClientID"];
                if (String.IsNullOrEmpty(strShopClientID)) strShopClientID = "0";
                //String strDefaultResponseText = Eggsoft_Public_CL.Pub_DeMode.Get_Message_(weixinXML, "31", "0");

                //回复消息的部分:你的代码写在这里strWeiXinINC_User_ID
                WX_Model.WX_Model_Parent myWX_Parent = WX_Parent.Call_Parent(weixinXML);

                String strMsgType = myWX_Parent.MsgType;
                string strResponse = "";
                Eggsoft.Common.debug_Log.Call_WriteLog("WeiXinResponseMsg98459248strMsgType=" + strMsgType);
                switch (strMsgType)
                {
                    case null: Console.WriteLine("strMsgType==null"); break;
                    case "text":

                        strResponse = Eggsoft_Public_CL.WX_Text.Call_Text(strShopClientID, weixinXML);

                        strgetResponseMsg = (strResponse);

                        Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息

                        break;
                    case "voice":
                        strgetResponseMsg = Eggsoft_Public_CL.WX_Subscribe.Call_KeyAnswer_Default(Int32.Parse(strShopClientID), weixinXML);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息
                        break;
                    case "image":
                        strgetResponseMsg = Eggsoft_Public_CL.WX_Subscribe.Call_KeyAnswer_Default(Int32.Parse(strShopClientID), weixinXML);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息
                        break;
                    case "location":
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.DoBaiDuAPILocation(weixinXML, myWX_Parent.FromUserName); ///调用一次 do location信息
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息
                        break;
                    case "event":
                        WX_Model.WX_Model_Event myWX_Event = WX_Event.Call_Event(weixinXML);
                        string strEventToLower = myWX_Event.Event.ToLower();
                        Eggsoft.Common.debug_Log.Call_WriteLog("weixinXML1:" + "" + strEventToLower);
                        switch (strEventToLower)
                        {
                            case "scan":
                                WX_Model.WX_Model_EventKey my_SCAN_CLICK_WX_Model = new WX_Model.WX_Model_EventKey();
                                my_SCAN_CLICK_WX_Model = my_SCAN_CLICK_WX_Model.GetWX_Model_EventKey(weixinXML);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Pub_Do_SCAN_And_ShopClient_Good(my_SCAN_CLICK_WX_Model.EventKey, my_SCAN_CLICK_WX_Model.FromUserName);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息
                                break;
                            case "location":
                                ///调用一次 do location信息 太长 这里写不下
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.DoBaiDuAPILocation(weixinXML, myWX_Parent.FromUserName); ///调用一次 do location信息
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息

                                break;
                            case "subscribe":

                                EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();

                                string strWhere = "OpenID='" + myWX_Event.FromUserName + "'";
                                strWhere += " and ShopClientID=" + strShopClientID;


                                if (bll_tab_User.Exists(strWhere) == false)
                                {


                                    Model_tab_User.ShopClientID = Int32.Parse(strShopClientID);
                                    Model_tab_User.OpenID = myWX_Event.FromUserName;
                                    Model_tab_User.Subscribe = true;
                                    Model_tab_User.SocialPlatform = Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform();
                                    Model_tab_User.InsertTime = DateTime.Now;
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    Model_tab_User.UpdateBy = "用户新增关注";
                                    Int32 IntUserID = bll_tab_User.Add(Model_tab_User);
                                    Model_tab_User = bll_tab_User.GetModel(IntUserID);
                                    //Model_tab_User.TeamID =Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(IntUserID.toString());

                                    //Model_tab_User.ParentID=

                                    #region 给没有送过购物红包的 用户送购物红包
                                    Eggsoft_Public_CL.Pub.SendYouhuiquan100Dollor(IntUserID);
                                    #endregion

                                }
                                else
                                {
                                    Model_tab_User = bll_tab_User.GetModel(strWhere);
                                    Model_tab_User.Subscribe = true;
                                    Model_tab_User.SocialPlatform = Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform();
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    Model_tab_User.UpdateBy = "用户更新关注";
                                    bll_tab_User.Update(Model_tab_User);


                                    ///Model_tab_User.ParentID
                                }
                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_User.toJsonString(), "关注", "UserID=" + Model_tab_User.ID);
                                Eggsoft.Common.debug_Log.Call_WriteLog(bll_tab_User.GetModel(strWhere).toJsonString(), "关注", "复核UserID=" + Model_tab_User.ID);


                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息


                                strResponse = Eggsoft_Public_CL.WX_Subscribe.Call_Subscribe(Int32.Parse(strShopClientID), weixinXML);

                                if (weixinXML.IndexOf("qrscene_") != 1)//商户扫描二维码事件 没有关注的情况下, （现在已经关注了，发送消息）发送消息。
                                {
                                    WX_Model.WX_Model_EventKey my_Subscribe_CLICK_WX_Model = new WX_Model.WX_Model_EventKey();
                                    my_Subscribe_CLICK_WX_Model = my_Subscribe_CLICK_WX_Model.GetWX_Model_EventKey(weixinXML);

                                    if (!string.IsNullOrEmpty(my_Subscribe_CLICK_WX_Model.EventKey))
                                    {
                                        String strKey = my_Subscribe_CLICK_WX_Model.EventKey.Substring("qrscene_".Length, my_Subscribe_CLICK_WX_Model.EventKey.Length - "qrscene_".Length);
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.Pub_Do_SCAN_And_ShopClient_Good(strKey, my_Subscribe_CLICK_WX_Model.FromUserName);
                                    }
                                }



                                strgetResponseMsg = (strResponse);
                                break;
                            case "unsubscribe":
                                //Eggsoft_Public_CL.WXdbMsghelp.addUser_Chat_InfoToMDF(Int32.Parse(Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(myWX_Parent.FromUserName)), 0, "用户消息_WeiXin", strEvent, weixinXML, myWX_Parent.FromUserName, myWX_Parent.ToUserName);
                                ///在关注者与公众号产生消息交互后，更新 关注状态
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息

                                #region 设置关注状态
                                EggsoftWX.BLL.tab_User bll_tab_Userunsubscribe = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User Model_tab_Userunsubscribe = bll_tab_Userunsubscribe.GetModel("OpenID='" + myWX_Event.FromUserName + "'");
                                if (Model_tab_Userunsubscribe != null)
                                {
                                    Model_tab_Userunsubscribe.Api_Authorize = false;
                                    Model_tab_Userunsubscribe.Subscribe = false;
                                    Model_tab_Userunsubscribe.UpdateBy = "用户取消关注";
                                    Model_tab_Userunsubscribe.Updatetime = DateTime.Now;
                                    bll_tab_Userunsubscribe.Update(Model_tab_Userunsubscribe);
                                    Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_Userunsubscribe.toJsonString(), "取消关注", "UserID=" + Model_tab_Userunsubscribe.ID);
                                }
                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_Userunsubscribe.toJsonString(), "取消关注", myWX_Event.FromUserName);
                                #endregion
                                /*不再删除
                          EggsoftWX.BLL.tab_User bll_Delete_tab_User = new EggsoftWX.BLL.tab_User();
                          String strHeadImageURL = bll_Delete_tab_User.GetModel("OpenID='" + myWX_Event.FromUserName + "'").HeadImageUrl;
                          Eggsoft.Common.FileFolder.DeleteFile(System.Web.HttpContext.Current.Server.MapPath(strHeadImageURL));
                          bll_Delete_tab_User.Delete("OpenID='" + myWX_Event.FromUserName + "'");
                          */
                                EggsoftWX.BLL.tab_System_XML_Message bll_Delete_tab_System_XML_Message = new EggsoftWX.BLL.tab_System_XML_Message();
                                bll_Delete_tab_System_XML_Message.Delete("FromUserName='" + myWX_Event.FromUserName + "'");


                                break;
                            case "click":

                                strResponse = Eggsoft_Public_CL.WX_EventKey.Call_EventKey(weixinXML, Int32.Parse(strShopClientID));
                                strgetResponseMsg = (strResponse);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(Int32.Parse(strShopClientID), myWX_Parent.FromUserName); ///调用一次 do location信息

                                break;
                        }
                        break;
                    default: Console.WriteLine("strMsgType==Unknown"); break;
                }


            }

            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee, "微信交互", "程序报错");
            }

            finally { }
            //}
            return strgetResponseMsg;
        }
    }
}