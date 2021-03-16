using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Eggsoft_Public_CL
{


    /// <summary>
    ///Pub_GetOpenID_And_ 的摘要说明
    /// </summary>
    public class Pub_GetOpenID_And_
    {
        public Pub_GetOpenID_And_()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 发送消息 返回码
        /// </summary>
        public class SendMessageReturn_Json
        {///  ///{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [ZDQ00568age1]"}   豪哥
         ///{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [awiJsa0661age7]"}

            public String errcode { get; set; }
            public String errmsg { get; set; }
        }




        public class ticket_Json
        {
            public String ticket { get; set; }
            public String expire_seconds { get; set; }
        }


        public class InputMoneyTempletMessage
        {
            public String LookURL { get; set; }
            public String UserAccount { get; set; }
            public Decimal InputMoney { get; set; }
            public Decimal TotalMoney { get; set; }
            public Decimal OldMoney { get; set; }
            public String ContactPhone { get; set; }


        }

        public class AccountNoticeTempletMessage
        {
            public Int32 intUserID { get; set; }

            public String LookURL { get; set; }
            public String UserAccount { get; set; }
            public Decimal GetMoney { get; set; }
            public Decimal TotalMoney { get; set; }
            public Decimal OldMoney { get; set; }

            public String Des { get; set; }

        }

        public static String MakeOpenIDBitmap(int intShopClientID, String strScene_Name, bool boolexpire_seconds)
        {

            readstrGet_ACCESS_TOKEN:
            string strBitmapURL = "";

            try
            {
                string strScend_ID = "1";

                //标号start
                #region count scend id
                EggsoftWX.BLL.tab_WeiXin_Scene_ID BLL_tab_WeiXin_Scene_ID = new EggsoftWX.BLL.tab_WeiXin_Scene_ID();
                EggsoftWX.Model.tab_WeiXin_Scene_ID Model_tab_WeiXin_Scene_ID = new EggsoftWX.Model.tab_WeiXin_Scene_ID();


                #region ///删除昨天以前的临时二维码  数据库记录太多 性能会下降 所以物理删除
                string strLikePC_SessionID_ = "RelationShopClientPC_SessionID_";
                string strWhere = "Scene_ActionName like '" + strLikePC_SessionID_ + "%'  and datediff(day,[UpdateTime],getdate())>0";
                BLL_tab_WeiXin_Scene_ID.Delete(strWhere);
                #endregion

                #region ///删除昨天以前的临时二维码  数据库记录太多 性能会下降 所以物理删除
                string strLikeRelation_PC_Sagent_SessionID_ = "Relation_PC_S_agent_";///pc  访问别人的店铺的  别人店铺的临时二维码
                strWhere = "Scene_ActionName like '" + strLikeRelation_PC_Sagent_SessionID_ + "%'  and datediff(day,[UpdateTime],getdate())>0";
                BLL_tab_WeiXin_Scene_ID.Delete(strWhere);
                #endregion


                bool boolScene_ActionName = BLL_tab_WeiXin_Scene_ID.Exists("Scene_ActionName='" + strScene_Name + "'");
                if (boolScene_ActionName)
                {
                    Model_tab_WeiXin_Scene_ID = BLL_tab_WeiXin_Scene_ID.GetModel("Scene_ActionName='" + strScene_Name + "'");
                    strScend_ID = Model_tab_WeiXin_Scene_ID.ID.ToString();

                    if (strScene_Name.IndexOf(strLikePC_SessionID_) != -1)////pC  登陆二维码才这么干 马上退出
                    {
                        return Model_tab_WeiXin_Scene_ID.Scene_Memo;
                    }
                    else if (strScene_Name.IndexOf(strLikeRelation_PC_Sagent_SessionID_) != -1)////pC  登陆别人的临时二维码才这么干 马上退出
                    {
                        return Model_tab_WeiXin_Scene_ID.Scene_Memo;
                    }

                    string strLike_SAgent_ = "Sagent_";///这时永久二维码
                    if (strScene_Name.IndexOf(strLike_SAgent_) != -1)////pC  登陆二维码才这么干 马上退出
                    {
                        if (String.IsNullOrEmpty(Model_tab_WeiXin_Scene_ID.Scene_Memo) == false)///说明这里面有值  可以马上返回
                        {
                            return Model_tab_WeiXin_Scene_ID.Scene_Memo;
                        }
                    }

                }
                else
                {
                    Model_tab_WeiXin_Scene_ID.Scene_ActionName = strScene_Name;
                    strScend_ID = BLL_tab_WeiXin_Scene_ID.Add(Model_tab_WeiXin_Scene_ID).ToString();
                }

                #endregion

                String strGet_ACCESS_TOKEN = Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID);

                string str_ticket_URL = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + strGet_ACCESS_TOKEN;

                string str_json_URL = "";
                if (boolexpire_seconds)
                {
                    int intOneDay = 60 * 60 * 24;///产生一天的临时二维码
                    str_json_URL = "{\"expire_seconds\": " + intOneDay + ", \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\":" + strScend_ID + "}}}";//临时二维码请求说明
                }
                else
                {
                    str_json_URL = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + strScend_ID + "}}}";
                }
                string str_ticket_Json = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(str_ticket_URL, str_json_URL);
                ticket_Json my_ticket_Json = JsonHelper.JsonDeserialize<ticket_Json>(str_ticket_Json);

                if ((str_ticket_Json.IndexOf("access_token expired") != -1) || (str_ticket_Json.IndexOf("40001") != -1))
                {
                    Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, true);
                    goto readstrGet_ACCESS_TOKEN;
                }
                strBitmapURL = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + System.Web.HttpUtility.UrlEncode(my_ticket_Json.ticket);

                Model_tab_WeiXin_Scene_ID.Scene_Memo = strBitmapURL;
                BLL_tab_WeiXin_Scene_ID.Update(Model_tab_WeiXin_Scene_ID);


            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e);
            }
            finally
            {

            }
            return strBitmapURL;
            //strBitmapURL;
        }



        public static string CheckSubscribe(int intUserID)
        {

            string strCheckSubscribe = "";

            Eggsoft.Common.debug_Log.Call_WriteLog("CheckSubscribe(int intUserID)=" + intUserID);

            #region CheckSubscribe
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
            Model_tab_User = BLL_tab_User.GetModel(intUserID);
            if (Model_tab_User.Subscribe == false)
            {
                #region 不要 反复 调用 该接口
                if (Model_tab_User.Updatetime < DateTime.Now.AddHours(-24))////没有交互的情况下 24小时 只去 交互一次接口
                {
                    bool boolfaleSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CaiLiangIFSsubscribe(Convert.ToInt32(Model_tab_User.ShopClientID), Model_tab_User.OpenID);
                    Model_tab_User.Subscribe = boolfaleSubscribe;
                    Model_tab_User.Updatetime = DateTime.Now;
                    BLL_tab_User.Update(Model_tab_User);
                }
                #endregion
                strCheckSubscribe = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/InfoAlert_PleaseSubscribeMe_Templet.html");
                strCheckSubscribe = strCheckSubscribe.Replace("###Get_GuideSubscribePageFromWeiXinD_ShopClientID_###", Eggsoft_Public_CL.Pub_DeMode.Get_GuideSubscribePageFromWeiXinD_ShopClientID_());


                int intShopId = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + intShopId);
                string strSubscribeTipInfo = "";
                if (tab_ShopClient_ShopPar_Model != null)
                {
                    strSubscribeTipInfo = tab_ShopClient_ShopPar_Model.SubscribeTipInfo;
                }


                String strShowCheckSubscribe = "";
                if (String.IsNullOrEmpty(strSubscribeTipInfo))
                {
                    strShowCheckSubscribe = "亲,您还没有关注我们,点这里开始关注.";
                }
                else
                {
                    strShowCheckSubscribe = strSubscribeTipInfo;
                }
                strCheckSubscribe = strCheckSubscribe.Replace("###CheckSubscribe###", strShowCheckSubscribe);
            }

            #endregion
            return strCheckSubscribe;
        }

        public static void Pub_Do_SCAN_And_ShopClient_Good(String strSceneID, String strOpenID)
        {
            try
            {

                int intSceneID = Int32.Parse(strSceneID);

                EggsoftWX.BLL.tab_WeiXin_Scene_ID BLL_tab_WeiXin_Scene_ID = new EggsoftWX.BLL.tab_WeiXin_Scene_ID();
                EggsoftWX.Model.tab_WeiXin_Scene_ID Model_tab_WeiXin_Scene_ID = new EggsoftWX.Model.tab_WeiXin_Scene_ID();

                bool bool_intSceneID = BLL_tab_WeiXin_Scene_ID.Exists(intSceneID);



                if (bool_intSceneID)
                {



                    Model_tab_WeiXin_Scene_ID = BLL_tab_WeiXin_Scene_ID.GetModel(intSceneID);
                    string strScene_ActionName = Model_tab_WeiXin_Scene_ID.Scene_ActionName.ToLower();
                    Eggsoft.Common.debug_Log.Call_WriteLog("strScene_ActionName=" + strScene_ActionName);

                    if (strScene_ActionName.IndexOf("good_") != -1)
                    {
                        #region good_
                        string strGoodID = strScene_ActionName.Substring("good_".Length);

                        EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        EggsoftWX.Model.tab_Goods Model_tab_Goods = bll_tab_Goods.GetModel(Int32.Parse(strGoodID));

                        EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(Convert.ToInt32(Model_tab_Goods.ShopClient_ID));

                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        //实例化几个WeiXinTuWen类对象  
                        string strTitle = Pub.GetNickName(Pub.GetUserIDFromOpenID(strOpenID)) + "你好！请点击微店商家" + Model_ShopClient.ShopClientName + "向你推荐的" + Model_tab_Goods.Name;

                        // string strImage = Eggsoft.Common.Application.AppUrl + GoodP.APPCODE_getImage(GoodP.APPCODE_getFirstImage(Model_tab_Goods.Icon), 640);
                        string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(Model_tab_Goods.Icon, 640);

                        string strDescription = "";
                        strDescription += Model_tab_Goods.ShortInfo + "\n";
                        //strDescription += "商家微店号：" + Pub.GetUserIDFromShopClientID(Model_tab_Goods.ShopClient_ID) + "。输入" + Pub.GetUserIDFromShopClientID(Model_tab_Goods.ShopClient_ID).ToString() + "#文字内容，可与商家直接对话！" + DateTime.Now + "\n";
                        //strDescription += "卖家微店商铺号：S" + Model_tab_Goods.ShopClient_ID + "。输入s" + Model_tab_Goods.ShopClient_ID + "#文字内容，可与卖家直接对话！其中s的格式是小写，半角" + DateTime.Now + "\n";
                        strDescription += DateTime.Now + "\n";

                        string strURL = Eggsoft.Common.Application.httpUrl + "/product-" + strGoodID + ".aspx";
                        ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                        WeiXinTuWens_ArrayList.Add(First);
                        Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(Pub.GetUserIDFromOpenID(strOpenID)), 0, WeiXinTuWens_ArrayList);
                        #endregion
                        // strOpenID

                    }
                    else if (strScene_ActionName.IndexOf("relationshopclientpc_sessionid_") != -1)
                    {
                        #region PC 用户扫描 关联
                        string strsessionid = strScene_ActionName.Substring("relationshopclientpc_sessionid_".Length);
                        //用户扫描 关联

                        string struserID = Pub.GetUserIDFromOpenID(strOpenID);
                        //struserID = "1";
                        string url = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL() + "/PCService/WS_User.asmx/UserSaoYiSaoErWeiMa?strargUserOpenID=" + strOpenID + "&strargUserSessionID=" + strsessionid;


                        Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(url);

                        Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, "PC扫描登录消息");
                        #endregion 用户扫描 关联
                    }
                    else if (strScene_ActionName.IndexOf("relation_pc_s_agent_") != -1)////可以扫描这个上线的PC临时二维码
                    {
                        string struserID = Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(strOpenID);


                        string strS_agentsessionid = strScene_ActionName.Substring("relation_pc_s_agent_".Length);
                        string[] strS_agentsessionidList = strS_agentsessionid.Split('_');
                        if (strS_agentsessionidList.Length == 2)
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(struserID.toInt32());
                            if (Model_tab_User.ID.ToString() == strS_agentsessionidList[0])
                            { ///自己扫描自己的  什么也不改写

                            }
                            else if (bll_tab_ShopClient_Agent_.Exists("UserID=" + struserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1"))////自己是代理 所以什么也不做
                            { ///自己扫描自己的微信代理二维码 什么也不干

                            }
                            else
                            {


                                int intIFModifyParent = 0;///分销所得优先给予第一人还是给予最新的转发人
                                String strShopClientID = HttpContext.Current.Request.QueryString["ShopClientID"];
                                //分销所得优先给予第一人还是给予最新的转发人。（举例：A转发给B，2天后C也转发给B。然后B购买了商品，A的上线是A还是C？选择表示上线是A，不选择表示上线是C。）
                                bool boolDoCheckIfPayMoney = Eggsoft_Public_CL.Pub_FenXiao.DoCheckIfPayedMoney(Model_tab_User.ShopClientID.toInt32(), struserID.toInt32());//.getp Eggsoft_Public_CL.Pub.boolShowPower(Model_tab_User.ShopClientID.ToString(), "ShareFirstManORLastMan");


                                // bool boolShareFirstManORLastMan = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShareFirstManORLastMan");
                                if ((boolDoCheckIfPayMoney) && (1 == 1)////并且当前用户也不是代理才可以改写
                                    && ((Model_tab_User.ParentID == null) || (Model_tab_User.ParentID < 1)))
                                {


                                    Model_tab_User.ParentID = Int32.Parse(strS_agentsessionidList[0]);
                                    //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                    Model_tab_User.Updatetime = DateTime.Now;
                                    Model_tab_User.UpdateBy = "支付过 改写上级";
                                    BLL_tab_User.Update(Model_tab_User);

                                    intIFModifyParent = Model_tab_User.ParentID.toInt32();
                                }
                                else
                                { ///可以给最新的人
                                    Model_tab_User.ParentID = Int32.Parse(strS_agentsessionidList[0]);
                                    //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                    Model_tab_User.Updatetime = DateTime.Now;
                                    Model_tab_User.UpdateBy = "没支付过支付过 改写上级";
                                    BLL_tab_User.Update(Model_tab_User);
                                    intIFModifyParent = Model_tab_User.ParentID.toInt32();
                                }

                                if (intIFModifyParent > 0)
                                {
                                    #region 增加直推未处理信息
                                    if (Model_tab_User.ParentID > 0)
                                    {
                                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "Pub_Do_SCAN";
                                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                        Model_b011_InfoAlertMessage.InfoTip = "改写上级PC二维码";
                                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                        Model_b011_InfoAlertMessage.UserID = intIFModifyParent;
                                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                        Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    }
                                    #endregion 增加直推未处理信息  

                                    #region 增加间推未处理信息
                                    if (Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent) > 0)
                                    {
                                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = "Pub_Do_SCAN";
                                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                        Model_b011_InfoAlertMessage.InfoTip = "改写上上级PC二维码";
                                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                        Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent);
                                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                        Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                        Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    }
                                    #endregion 增加直推未处理信息  

                                }

                            }
                            string url = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL() + "/PCService/WS_User.asmx/UserSaoYiSaoErWeiMa?strargUserOpenID=" + strOpenID + "&strargUserSessionID=" + strS_agentsessionidList[1];
                            Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(url);

                            Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, "PC扫描" + Eggsoft_Public_CL.Pub.GetNickName(strS_agentsessionidList[0]) + "登录消息");
                        }

                    }
                    else if (strScene_ActionName.IndexOf("relationshopclient_") != -1)
                    {
                        #region relationshopclient_
                        string strShopClientID = strScene_ActionName.Substring("relationshopclient_".Length);
                        //商户后台扫描关联

                        string struserID = Pub.GetUserIDFromOpenID(strOpenID);
                        //struserID = "1";



                        string strWeiXinRalationUserIDList = "";
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClientID));

                        #region
                        Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, Model_tab_ShopClient.ShopClientName + "商户关联消息,已增加2000元购物红包,点击我即可查看,购物红包只能购物使用,额度请到商品后台设置.微信上的使用方法1是到商品详情页->购物红包开关即可.方法2是到我->分享购物红包->让你朋友圈抢你的购物红包,你朋友抢到后即可用方法1使用,进行购物");
                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = 2000;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "关联增加";
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(struserID);
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                        #region 增加购物券未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物券未处理信息 
                        #endregion

                        XML__Class_Shop_Client XML__Class_Shop_Client;
                        string strXML = Model_tab_ShopClient.XML;
                        if (string.IsNullOrEmpty(strXML) == false)
                        {
                            XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);

                            // Eggsoft.Common.debug_Log.Call_WriteLog("商户后台扫描关联XML__Class_Shop_Client.WeiXinRalationUserIDList=" + XML__Class_Shop_Client.WeiXinRalationUserIDList);

                            if (String.IsNullOrEmpty(XML__Class_Shop_Client.WeiXinRalationUserIDList) == false)///真的有数据 就读出
                            {
                                strWeiXinRalationUserIDList = XML__Class_Shop_Client.WeiXinRalationUserIDList;
                            }

                            if (strWeiXinRalationUserIDList == "")
                            {
                                strWeiXinRalationUserIDList = struserID;
                            }
                            else
                            {
                                //检查是否关联过
                                bool boolCheck = false;
                                string[] strList = strWeiXinRalationUserIDList.Split(',');
                                for (int i = 0; i < strList.Length; i++)
                                {
                                    if (strList[i] == struserID)
                                    {
                                        boolCheck = true;
                                        return;
                                    }
                                }
                                if (boolCheck == false)///
                                {
                                    strWeiXinRalationUserIDList = strWeiXinRalationUserIDList + "," + struserID;

                                }
                            }
                            XML__Class_Shop_Client.WeiXinRalationUserIDList = strWeiXinRalationUserIDList;
                            // Eggsoft.Common.debug_Log.Call_WriteLog("商户后台扫描关联strWeiXinRalationUserIDList=" + strWeiXinRalationUserIDList);
                        }
                        else
                        {
                            XML__Class_Shop_Client = new XML__Class_Shop_Client();
                            XML__Class_Shop_Client.WeiXinRalationUserIDList = struserID;
                        }
                        Model_tab_ShopClient.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.Encoding.UTF8);
                        BLL_tab_ShopClient.Update(Model_tab_ShopClient);

                        Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, Model_tab_ShopClient.ShopClientName + "商户关联消息");


                        #endregion relationshopclient_

                    }
                    else if (strScene_ActionName.IndexOf("relationshop_o2o_client") != -1)
                    {
                        #region relationshop_o2o_client
                        string strShopClientID = strScene_ActionName.Substring("relationshop_o2o_client".Length);
                        //商户后台扫描关联

                        string struserID = Pub.GetUserIDFromOpenID(strOpenID);
                        //struserID = "1";



                        string strWeiXinRalationUserIDList = "";
                        EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                        EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Int32.Parse(strShopClientID));


                        XML__Class_Shop_O2o XML__Class_Shop_O2o;
                        string strXML = Model_tab_ShopClient_O2O_ShopInfo.XML;
                        if (string.IsNullOrEmpty(strXML) == false)
                        {
                            XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);

                            // Eggsoft.Common.debug_Log.Call_WriteLog("商户后台扫描关联XML__Class_Shop_Client.WeiXinRalationUserIDList=" + XML__Class_Shop_Client.WeiXinRalationUserIDList);

                            if (String.IsNullOrEmpty(XML__Class_Shop_O2o.WeiXinRalationUserIDList) == false)///真的有数据 就读出
                            {
                                strWeiXinRalationUserIDList = XML__Class_Shop_O2o.WeiXinRalationUserIDList;
                            }

                            if (strWeiXinRalationUserIDList == "")
                            {
                                strWeiXinRalationUserIDList = struserID;
                            }
                            else
                            {
                                //检查是否关联过
                                bool boolCheck = false;
                                string[] strList = strWeiXinRalationUserIDList.Split(',');
                                for (int i = 0; i < strList.Length; i++)
                                {
                                    if (strList[i] == struserID)
                                    {
                                        boolCheck = true;
                                        return;
                                    }
                                }
                                if (boolCheck == false)///
                                {
                                    strWeiXinRalationUserIDList = strWeiXinRalationUserIDList + "," + struserID;

                                }
                            }
                            XML__Class_Shop_O2o.WeiXinRalationUserIDList = strWeiXinRalationUserIDList;
                            // Eggsoft.Common.debug_Log.Call_WriteLog("商户后台扫描关联strWeiXinRalationUserIDList=" + strWeiXinRalationUserIDList);
                        }
                        else
                        {
                            XML__Class_Shop_O2o = new XML__Class_Shop_O2o();
                            XML__Class_Shop_O2o.WeiXinRalationUserIDList = struserID;
                        }
                        Model_tab_ShopClient_O2O_ShopInfo.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_O2o, System.Text.Encoding.UTF8);
                        BLL_tab_ShopClient_O2O_ShopInfo.Update(Model_tab_ShopClient_O2O_ShopInfo);

                        Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, "o2o商户关联消息");


                        #endregion relationshopclient_

                    }
                    else if (strScene_ActionName.IndexOf("sagent_") != -1)///代理证书
                    {
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                        #region 代理证书
                        String strShopClientID = HttpContext.Current.Request.QueryString["ShopClientID"];
                        if (String.IsNullOrEmpty(strShopClientID)) return;
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClientID));
                        if (Model_tab_ShopClient == null) return;
                        string struserID = Pub.GetUserIDFromOpenID(strOpenID);

                        string strParentAgentID = strScene_ActionName.Substring("sagent_".Length);
                        string strTitle = Pub.GetNickName(struserID) + "你好！欢迎访问" + Model_tab_ShopClient.ShopClientName + ",你扫描二维码";
                        strTitle += Pub.GetNickName(strParentAgentID) + "的证书成功.";

                        #region 推送优惠券
                        bool boolParentAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strParentAgentID + " and ShopClientID=" + strShopClientID + "   and IsDeleted=0 and isnull(Empowered,0)=1");///有代理啊
                        string strSendYouHuiQuanID = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "SendYouHuiQuanID");
                        if (strSendYouHuiQuanID.toInt32() > 0)
                        {
                            string strAgent = "";
                            if (boolParentAgent)
                            {
                                strAgent = "/sagent-" + strParentAgentID;
                            }
                            string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                            strErJiYuMing = "https://" + strErJiYuMing + strAgent + "/addfunction/06coupons/indexlist.aspx?gotogetvouchersnumid=" + strSendYouHuiQuanID;

                            string strImage = "https://upload.eggsoft.cn/Upload/images/timg.jpg";
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, "平台发优惠券,立即领取", strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);
                            Pub_GetOpenID_And_.SendTextWinXinMessageImage(struserID.toInt32(), strParentAgentID.toInt32(), WeiXinTuWens_ArrayList);
                        }
                        else
                        {
                            Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(struserID), 0, strTitle);
                        }
                        #endregion 推送优惠券



                        Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strParentAgentID), 0, "微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(struserID) + "," + Pub.GetNickName(struserID) + "扫描你的专属二维码");

                        //分销所得优先给予第一人还是给予最新的转发人。（举例：A转发给B，2天后C也转发给B。然后B购买了商品，A的上线是A还是C？选择表示上线是A，不选择表示上线是C。）
                        //bool boolShareFirstManORLastMan = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShareFirstManORLastMan");
                        bool boolDoCheckIfPayMoney = Eggsoft_Public_CL.Pub_FenXiao.DoCheckIfPayedMoney(strShopClientID.toInt32(), struserID.toInt32());//.getp Eggsoft_Public_CL.Pub.boolShowPower(Model_tab_User.ShopClientID.ToString(), "ShareFirstManORLastMan");


                        EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(Int32.Parse(struserID));
                        int intIFModifyParent = 0;//是否修改上级

                        if (boolDoCheckIfPayMoney)
                        {
                            bool boolMeUserAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + struserID + " and ShopClientID=" + strShopClientID + "   and IsDeleted=0 and isnull(Empowered,0)=1");///有代理啊

                            if (!boolMeUserAgent &&////当前用户不是代理才能改写
                                ((Model_tab_User.ParentID == null) || (Model_tab_User.ParentID < 1)) && (Model_tab_User.ID != Int32.Parse(strParentAgentID)))
                            {
                                if (Model_tab_User.ShopClientID == Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strParentAgentID))
                                {
                                    Model_tab_User.ParentID = Int32.Parse(strParentAgentID);
                                    //Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    Int32 Int32TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                    if (Int32TeamID > 0) Model_tab_User.TeamID = Int32TeamID;

                                    Model_tab_User.UpdateBy = "当前用户不是代理才能改写";
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    intIFModifyParent = Model_tab_User.ParentID.toInt32();
                                    bll_tab_User.Update(Model_tab_User);
                                }
                                else
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog("别人商铺的二维码strAgentID=" + strParentAgentID, "非同一商铺");
                                }
                            }
                        }
                        else if (Model_tab_User.ID != Int32.Parse(strParentAgentID))
                        {
                            if (Model_tab_User.ShopClientID == Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strParentAgentID))
                            {
                                Model_tab_User.ParentID = Int32.Parse(strParentAgentID);
                                Model_tab_User.TeamID = Eggsoft_Public_CL.Pub.GetMyOrganizationTeamIDFromUserID(Model_tab_User.ParentID.toString());
                                Model_tab_User.UpdateBy = "没有支付过才能改写";
                                Model_tab_User.Updatetime = DateTime.Now;
                                bll_tab_User.Update(Model_tab_User);
                                intIFModifyParent = Model_tab_User.ParentID.toInt32();
                            }
                            else
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog("别人商铺的二维码strAgentID=" + strParentAgentID, "程序报错", "非同一商铺");
                            }
                        }

                        if (intIFModifyParent > 0)
                        {
                            #region 增加直推未处理信息
                            if (Model_tab_User != null & Model_tab_User.ParentID > 0)
                            {
                                string strwebuy8_ClientAdmin_Users_ClientUserAccount = "别人商铺的二维码";
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = "改写ParentID代理证书";
                                Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UserID = intIFModifyParent;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            }
                            #endregion 增加直推未处理信息  

                            #region 增加间推未处理信息
                            if (Model_tab_User != null && Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent) > 0)
                            {
                                string strwebuy8_ClientAdmin_Users_ClientUserAccount = "别人商铺的二维码";
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = "改写ParentID代理证书";
                                Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                                Model_b011_InfoAlertMessage.UserID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Database_AbountHistory(intIFModifyParent);
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_User.ShopClientID;
                                Model_b011_InfoAlertMessage.Type = "Info_MySonmember";///增加直推
                                Model_b011_InfoAlertMessage.TypeTableID = Model_tab_User.ID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            }
                            #endregion 增加直推未处理信息  

                        }

                        #endregion 代理证书

                        #region 加入被转发人的运营中心数据
                        System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClientID.toInt32()))
                            {
                                Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, strShopClientID.toInt32(), Model_tab_User.ParentID.toInt32());
                            }
                        });
                        #endregion 初始化所有运营中心数据

                        #region 给没有扫描赠送   购物红包的 用户送购物红包  或者 购物券
                        Eggsoft_Public_CL.Pub.SendYouhuiquan100DollorScanAgentErWeiMa(Int32.Parse(struserID));
                        #endregion 扫描赠送
                    }

                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
                Eggsoft.Common.debug_Log.Call_WriteLog("strSceneID=" + strSceneID + "," + "strOpenID=" + strOpenID);
            }
            finally
            {
            }
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public static void SendTextTipInfoMessage(Int32 intToUserID, string strSendMessage)
        {
            EggsoftWX.BLL.tab_User_Message_NeedShow BLL_tab_User_Message_NeedShow = new EggsoftWX.BLL.tab_User_Message_NeedShow();
            EggsoftWX.Model.tab_User_Message_NeedShow Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
            Model_tab_User_Message_NeedShow.UserID = intToUserID;
            Model_tab_User_Message_NeedShow.InfoNeedShow = Eggsoft.Common.CommUtil.getShortText(strSendMessage, 255);
            Model_tab_User_Message_NeedShow.CreateBy = "系统预定义执行";
            Model_tab_User_Message_NeedShow.UpdateBy = "系统预定义执行";
            BLL_tab_User_Message_NeedShow.Add(Model_tab_User_Message_NeedShow);
        }


        /// <summary>
        /// 微信模板消息通知     成功付款通知
        /// </summary>
        /// <param name="intToUserID"></param>
        /// <param name="intFromUserID"></param>
        /// <param name="WeiXinTuWens_ArrayList">适用只有图文一个消息的地方</param>
        public static String SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)
        {
            String strOK = "";
            try
            {
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "TempletPayMessage");
                string strTemplePayMessage = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "TemplePayMessage");
                if ((boolTempletPayMessage == false) || (String.IsNullOrEmpty(strTemplePayMessage))) return "No Message";
                string strACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, false);
                String strSendURL = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + strACCESS_TOKEN;
                ////适用只有图文一个消息的地方  

                //string strTitle = stu.Title;
                //string strSendMessage = stu.Description;
                //string strImage = stu.Image;
                //string strClickURL = stu.Url;

                string strJSON = "";
                strJSON += "{\n";
                strJSON += "\"touser\":\"" + Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
                strJSON += "\"template_id\":\"" + strTemplePayMessage + "\",\n";
                strJSON += "\"url\":\"" + strClickURL + "\",\n";
                strJSON += "\"topcolor\":\"#FF0000\",\n";
                strJSON += "\"data\":{\n";
                strJSON += "\"first\": {\n";
                strJSON += "\"value\":\"" + strTitle + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword1\":{\n";
                strJSON += "\"value\":\"" + stringOrderNum + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword2\":{\n";
                strJSON += "\"value\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword3\":{\n";
                strJSON += "\"value\":\"" + stringOrderNumTotalMoney + "\",\n";
                strJSON += "\"color\":\"#FF0000\"\n";
                strJSON += "},\n";
                strJSON += "\"remark\":{\n";
                strJSON += "\"value\":\"" + strremark + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "}\n";
                strJSON += "}\n";
                strJSON += "}\n";

                Eggsoft.Common.debug_Log.Call_WriteLog(strJSON, "微信支付", "发送消息");
                strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                Eggsoft.Common.debug_Log.Call_WriteLog(strOK, "微信支付", "返回消息");
                //{{first.DATA}}
                //订单号码：{{keyword1.DATA}}
                //交易时间：{{keyword2.DATA}}
                //交易金额：{{keyword3.DATA}}
                //{{remark.DATA}}
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd);
            }
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);
            return my_SendMessage_Json.errcode;
        }

        /// <summary>
        /// 微信模板消息通知   
        /// </summary>
        /// <param name="intToUserID"></param>
        /// <param name="intFromUserID"></param>
        /// <param name="WeiXinTuWens_ArrayList">适用只有图文一个消息的地方</param>
        public static String SendTempletWinXinMessage(Int32 intToUserID, Int32 intShopClientID, System.Collections.ArrayList WeiXinTuWens_ArrayList)
        {
            if (IFCanSendWXget_IF_LocalhostTest_()) return "LocalNoSend";

            String strOK = "";
            try
            {
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "TempletVisitMessage");
                string strTempleVisitMessage = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "TempleVisitMessage");
                string strTempleWisdomVisitMessage = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "TempleWisdomVisitMessage");
                if ((boolTempletVisitMessage == false) || (String.IsNullOrEmpty(strTempleVisitMessage) && String.IsNullOrEmpty(strTempleWisdomVisitMessage))) return "No Message";
                string strACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, false);
                String strSendURL = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + strACCESS_TOKEN;
                ////适用只有图文一个消息的地方  
                ClassP.WeiXinTuWen stu = WeiXinTuWens_ArrayList[0] as ClassP.WeiXinTuWen;
                string strTitle = stu.Title.Replace("\"", "");
                string strSendMessage = stu.Description.Replace("\\", "").Replace("\"", "");
                string strImage = stu.Image;
                string strClickURL = stu.Url;

                string strJSON = "";

                if (!string.IsNullOrEmpty(strTempleVisitMessage))
                {
                    strJSON += "        {\n";
                    strJSON += "\"touser\":\"" + Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
                    strJSON += "\"template_id\":\"" + strTempleVisitMessage + "\",\n";
                    strJSON += "\"url\":\"" + strClickURL + "\",\n";
                    strJSON += "\"topcolor\":\"#000000\",\n";
                    strJSON += "\"data\":{\n";
                    strJSON += "\"first\": {\n";
                    strJSON += "\"value\":\"" + strTitle + "\",\n";
                    strJSON += "\"color\":\"#FF0000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"keynote1\":{\n";
                    strJSON += "\"value\":\"" + Eggsoft_Public_CL.Pub.GetNickName(intToUserID.ToString()) + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"keynote2\":{\n";
                    strJSON += "\"value\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"remark\":{\n";
                    strJSON += "\"value\":\"" + strSendMessage + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "}\n";
                    strJSON += "}\n";
                    strJSON += "}\n";
                }
                else if (!string.IsNullOrEmpty(strTempleWisdomVisitMessage))
                {
                    strJSON += "        {\n";
                    strJSON += "\"touser\":\"" + Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
                    strJSON += "\"template_id\":\"" + strTempleWisdomVisitMessage + "\",\n";
                    strJSON += "\"url\":\"" + strClickURL + "\",\n";
                    strJSON += "\"topcolor\":\"#000000\",\n";
                    strJSON += "\"data\":{\n";
                    strJSON += "\"first\": {\n";
                    strJSON += "\"value\":\"" + strTitle + "\",\n";
                    strJSON += "\"color\":\"#FF0000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"keyword1\":{\n";
                    strJSON += "\"value\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"keyword2\":{\n";
                    strJSON += "\"value\":\"" + Eggsoft_Public_CL.Pub.GetNickName(intToUserID.ToString()) + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "},\n";
                    strJSON += "\"remark\":{\n";
                    strJSON += "\"value\":\"" + strSendMessage + "\",\n";
                    strJSON += "\"color\":\"#000000\"\n";
                    strJSON += "}\n";
                    strJSON += "}\n";
                    strJSON += "}\n";

                }
                if (!String.IsNullOrEmpty(strJSON))
                {
                    strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                }
                //                {{first.DATA}}
                //消息来自：{{keynote1.DATA}}           
                //发送时间：{{keynote2.DATA}}
                //{{remark.DATA}}
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd);
            }
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);
            return my_SendMessage_Json.errcode;
        }

        /// <summary>
        /// 会员充值微信模板消息通知   
        /// </summary>
        /// <param name="intToUserID"></param>
        /// <param name="intFromUserID"></param>
        /// <param name="argInputMoneyTempletMessage"></param>
        public static String SendTemplenputMoneyMessage(Int32 intToUserID, Int32 intShopClientID, InputMoneyTempletMessage argInputMoneyTempletMessage)
        {
            Eggsoft.Common.debug_Log.Call_WriteLog(Eggsoft.Common.JsonHelper.JsonSerializer<InputMoneyTempletMessage>(argInputMoneyTempletMessage), "会员充值微信模板消息通知", "收到消息" + "intToUserID=" + intToUserID + ", intShopClientID=" + intShopClientID);

            if (IFCanSendWXget_IF_LocalhostTest_()) return "LocalNoSend";

            String strOK = "";
            try
            {
                string strTempleInputMoneyMessage = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "TempleInputMoneyMessage");
                string strACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, false);
                String strSendURL = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + strACCESS_TOKEN;
                ////适用只有图文一个消息的地方  
                //ClassP.WeiXinTuWen stu = WeiXinTuWens_ArrayList[0] as ClassP.WeiXinTuWen;
                string strTitle = "您好，您已成功进行会员卡充值。";
                //string strSendMessage = stu.Description.Replace("\\", "").Replace("\"", "");
                //string strImage = stu.Image;
                //string strClickURL = stu.Url;



                //{ { first.DATA} }

                //{ { accountType.DATA} }：{ { account.DATA} }
                //充值金额：{ { amount.DATA} }
                //充值状态：{ { result.DATA} }
                //{ { remark.DATA} }
                //                您好，您已成功进行会员卡充值。

                //会员卡号：11912345678
                //充值金额：50元
                //充值状态：充值成功
                //备注：如有疑问，请致电13912345678联系我们。

                string strJSON = "";
                strJSON += "        {\n";
                strJSON += "\"touser\":\"" + Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
                strJSON += "\"template_id\":\"" + strTempleInputMoneyMessage + "\",\n";
                strJSON += "\"url\":\"" + argInputMoneyTempletMessage.LookURL + "\",\n";
                strJSON += "\"topcolor\":\"#000000\",\n";
                strJSON += "\"data\":{\n";
                strJSON += "\"first\": {\n";
                strJSON += "\"value\":\"" + strTitle + "\",\n";
                strJSON += "\"color\":\"#FF0000\"\n";
                strJSON += "},\n";
                strJSON += "\"accountType\":{\n";
                strJSON += "\"value\":\"" + "会员卡号" + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"account\":{\n";
                strJSON += "\"value\":\"" + argInputMoneyTempletMessage.UserAccount + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"amount\":{\n";///充值金额
                strJSON += "\"value\":\"" + argInputMoneyTempletMessage.InputMoney + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"result\":{\n";
                strJSON += "\"value\":\"" + "您的原账户余额是" + argInputMoneyTempletMessage.OldMoney + "。成功充值，您的账户余额是" + argInputMoneyTempletMessage.TotalMoney + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"remark\":{\n";
                strJSON += "\"value\":\"" + "如有疑问，请致电" + argInputMoneyTempletMessage.ContactPhone + "联系我们" + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "}\n";
                strJSON += "}\n";
                strJSON += "}\n";
                Eggsoft.Common.debug_Log.Call_WriteLog(strJSON, "会员充值微信模板消息通知", "发送消息");
                strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                Eggsoft.Common.debug_Log.Call_WriteLog(strOK, "会员充值微信模板消息通知", "返回消息");

                //                {{first.DATA}}
                //消息来自：{{keynote1.DATA}}           
                //发送时间：{{keynote2.DATA}}
                //{{remark.DATA}}
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd);
            }
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);
            Eggsoft.Common.debug_Log.Call_WriteLog(Eggsoft.Common.JsonHelper.JsonSerializer<SendMessageReturn_Json>(my_SendMessage_Json), "会员充值功能", "返回消息");

            return my_SendMessage_Json.errcode;
        }

        /// <summary>
        /// 到账通知
        /// </summary>
        public static String SendTempleAccountNotice(Int32 intToUserID, Int32 intShopClientID, AccountNoticeTempletMessage argAccountNoticeTempletMessage)
        {
            Eggsoft.Common.debug_Log.Call_WriteLog(Eggsoft.Common.JsonHelper.JsonSerializer<AccountNoticeTempletMessage>(argAccountNoticeTempletMessage), "到账微信模板消息通知", "收到消息" + "intToUserID=" + intToUserID + ", intShopClientID=" + intShopClientID);

            //if (intToUserID != 52336) return "Debug";

            if (IFCanSendWXget_IF_LocalhostTest_()) return "LocalNoSend";

            String strOK = "";
            try
            {
                string strTempleAccountNotice = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "TempleAccountNotice");
                if (string.IsNullOrEmpty(strTempleAccountNotice.toString())) return "";
                string strACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, false);
                String strSendURL = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + strACCESS_TOKEN;
                ////适用只有图文一个消息的地方  
                //ClassP.WeiXinTuWen stu = WeiXinTuWens_ArrayList[0] as ClassP.WeiXinTuWen;
                string strTitle = "您好，您有到账通知。";
                //               { { first.DATA} }
                //               账户名：{ { keyword1.DATA} }
                //               数量：{ { keyword2.DATA} }
                //               时间：{ { keyword3.DATA} }
                //               { { remark.DATA} }

                //               您好，您的任务收益已到账
                //账户名称：张三
                //数量：1000元
                //时间：2016年5月11日
                //作业完成及时，书写工整

                string strJSON = "";
                strJSON += "        {\n";
                strJSON += "\"touser\":\"" + Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
                strJSON += "\"template_id\":\"" + strTempleAccountNotice + "\",\n";
                strJSON += "\"url\":\"" + argAccountNoticeTempletMessage.LookURL + "\",\n";
                strJSON += "\"topcolor\":\"#000000\",\n";
                strJSON += "\"data\":{\n";
                strJSON += "\"first\": {\n";
                strJSON += "\"value\":\"" + strTitle + "\",\n";
                strJSON += "\"color\":\"#FF0000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword1\":{\n";
                strJSON += "\"value\":\"" + "ID:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intToUserID.toString()) + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword2\":{\n";
                strJSON += "\"value\":\"" +Eggsoft_Public_CL.Pub.getBankPubMoney(argAccountNoticeTempletMessage.GetMoney) + "元\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"keyword3\":{\n";///充值金额
                strJSON += "\"value\":\"" + DateTime.Now.ToString() + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "},\n";
                strJSON += "\"remark\":{\n";
                strJSON += "\"value\":\"" + "" + argAccountNoticeTempletMessage.Des + "" + "\",\n";
                strJSON += "\"color\":\"#000000\"\n";
                strJSON += "}\n";
                strJSON += "}\n";
                strJSON += "}\n";
                Eggsoft.Common.debug_Log.Call_WriteLog(strJSON, "到账微信模板消息通知", "发送消息");
                strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
                Eggsoft.Common.debug_Log.Call_WriteLog(strOK, "到账微信模板消息通知", "返回消息");

                //                {{first.DATA}}
                //消息来自：{{keynote1.DATA}}           
                //发送时间：{{keynote2.DATA}}
                //{{remark.DATA}}
            }
            catch (Exception ddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddd);
            }
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);
            Eggsoft.Common.debug_Log.Call_WriteLog(Eggsoft.Common.JsonHelper.JsonSerializer<SendMessageReturn_Json>(my_SendMessage_Json), "会员充值功能", "返回消息");

            return my_SendMessage_Json.errcode;
        }


        /// <summary>
        /// 模板消息的
        /// </summary>
        /// <param name="intToUserID"></param>
        /// <param name="intFromUserID"></param>
        /// <param name="WeiXinTuWens_ArrayList"></param>
        /// <returns></returns>
        public static String SendTextWinXinMessageImage(Int32 intToUserID, Int32 intFromUserID, System.Collections.ArrayList WeiXinTuWens_ArrayList)
        {
            if (IFCanSendWXget_IF_LocalhostTest_()) return "LocalNoSend";
            //return "LocalNoSend";
            if (intToUserID == 0)///发给总店主的 就不发料
            {
                return "Error intToUserID=0" + intToUserID;
            }

            int intShopClientID = Eggsoft_Public_CL.Pub_DeMode.Get_ShopClientID_From_UserID_(intToUserID);

            string strJSON = ""; ;
            //1
            strJSON += "{\n";
            strJSON += "\"touser\":\"" + Pub_GetOpenID_And_.Getuser_OpenID(intToUserID) + "\",\n";
            strJSON += "\"msgtype\":\"news\",\n";
            strJSON += "\"news\":{\n";
            strJSON += "    \"articles\": [\n";
            for (int i = 0; i < WeiXinTuWens_ArrayList.Count; ++i)
            {
                //读取单个元素,因为存入ArrayList中的元素会变为Object类型,   
                ClassP.WeiXinTuWen stu = WeiXinTuWens_ArrayList[i] as ClassP.WeiXinTuWen;
                string strTitle = stu.Title;
                string strSendMessage = stu.Description;
                string strImage = stu.Image;
                string strClickURL = stu.Url;

                strJSON += "     {\n";
                strJSON += "         \"title\":\"" + strTitle + "\",\n";
                strJSON += "        \"description\":\"" + strSendMessage + "\",\n";
                strJSON += "        \"url\":\"" + strClickURL + "\",\n";
                strJSON += "        \"picurl\":\"" + strImage + "\"\n";
                if (i == WeiXinTuWens_ArrayList.Count - 1)
                {
                    strJSON += "    }\n";
                }
                else
                {
                    strJSON += "    },\n";
                }
            }

            strJSON += "    ]\n";
            strJSON += "}\n";
            strJSON += "}\n";
            string strSendURL = "";
            strSendURL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID) + "";

            String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);
            ///{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [ZDQ00568age1]"}   豪哥
            ///{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [awiJsa0661age7]"}
            Eggsoft.Common.debug_Log.Call_WriteLog("intShopClientID=" + intShopClientID + "  strOK=" + strOK + "  strSendURL" + strSendURL + "  strJSON" + strJSON, "SendTextWinXinMessageImage", "intToUserID=" + intToUserID);
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);
            return my_SendMessage_Json.errcode;


            //Eggsoft_Public_CL.Pub.SendWeiXinMessage_AddTask(strSendURL, strJSON);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intToUserID"></param>
        /// <param name="intFromUserID"></param>
        /// <param name="strSendMessage"></param>
        /// <returns>45015   or  ok</returns>
        public static String SendTextWinXinMessage(Int32 intToUserID, Int32 intFromUserID, string strSendMessage)
        {
            if (IFCanSendWXget_IF_LocalhostTest_()) return "LocalNoSend";

            int intShopClientID = Eggsoft_Public_CL.Pub_DeMode.Get_ShopClientID_From_UserID_(intToUserID);

            EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User myModel_tab_User = new EggsoftWX.Model.tab_User();
            myModel_tab_User = myBLL_tab_User.GetModel("id=" + intToUserID);


            if (myModel_tab_User == null)
            {
                //Eggsoft.Common.JsUtil.ShowMsg("微店已收到你的信息，请等待处理！。可能商户没有绑定微信号！");
                return "intToUserID!=" + intToUserID;
            }
            string strSendURL = "";

            strSendURL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID) + "";


            string strJSON = ""; ;

            strJSON = "{\n";
            strJSON += "\"touser\":\"" + myModel_tab_User.OpenID + "\",\n";
            strJSON += "\"msgtype\":\"text\",\n";
            strJSON += "\"text\":\n";
            strJSON += "{\n";
            strJSON += "     \"content\":\"" + strSendMessage + "\"\n";
            strJSON += "}\n";
            strJSON += "}\n";


            String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);

            Eggsoft.Common.debug_Log.Call_WriteLog("intShopClientID= " + intShopClientID + " strOK=" + strOK + "  strSendURL" + strSendURL + "  strJSON" + strJSON, "SendTextWinXinMessage");
            SendMessageReturn_Json my_SendMessage_Json = JsonHelper.JsonDeserialize<SendMessageReturn_Json>(strOK);

            return my_SendMessage_Json.errcode;
        }


        public static bool SendTextWinXinMessage_Voice(Int32 intToUserID, Int32 intFromUserID, string strSendMediaId, String strMsgShopCliengORSystem)
        {
            if (IFCanSendWXget_IF_LocalhostTest_()) return false;
            int intShopClientID = Eggsoft_Public_CL.Pub_DeMode.Get_ShopClientID_From_UserID_(intToUserID);


            //if (intToUserID == 0)
            //{
            //    if (Pub_SocialPlatform.Check_SocialPlatform() == "WeiXin")
            //    {
            //        intToUserID = 13;
            //    }
            //    else if (Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
            //    {
            //        intToUserID = 5540;
            //    }
            //}
            EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User myModel_ToUserID_tab_User;
            EggsoftWX.Model.tab_User myModel_FromUserID_tab_User;

            myModel_ToUserID_tab_User = myBLL_tab_User.GetModel("id=" + intToUserID);
            myModel_FromUserID_tab_User = myBLL_tab_User.GetModel("id=" + intFromUserID);

            if (myModel_ToUserID_tab_User.SocialPlatform != myModel_FromUserID_tab_User.SocialPlatform)
            {
                return false;//不是一个平台 发送失败
            }


            string strSendURL = "";
            string strYiXin = Pub_SocialPlatform.Check_SocialPlatform(intToUserID);

            strSendURL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID) + "";

            string strJSON = ""; ;

            strJSON = "{\n";
            strJSON += "\"touser\":\"" + myModel_ToUserID_tab_User.OpenID + "\",\n";
            strJSON += "\"msgtype\":\"voice\",\n";
            strJSON += "\"voice\":\n";
            strJSON += "{\n";
            strJSON += "     \"media_id\":\"" + strSendMediaId + "\"\n";
            strJSON += "}\n";
            strJSON += "}\n";




            String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);

            bool SendTextWinXinMessage = false;
            if (strOK.ToLower().IndexOf("ok") != -1)
            {
                SendTextWinXinMessage = true;
                //WXdbMsghelp.addUser_Chat_InfoToMDF(intToUserID, intFromUserID, strMsgShopCliengORSystem, "语音消息", strJSON, myModel_ToUserID_tab_User.OpenID, "");
            }
            return SendTextWinXinMessage;
        }


        public static bool SendTextWinXinMessage_Image(Int32 intToUserID, Int32 intFromUserID, string strSendMediaId, String strMsgShopCliengORSystem)
        {
            if (IFCanSendWXget_IF_LocalhostTest_()) return false;

            //EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            //EggsoftWX.Model.tab_User myModel_tab_User = new EggsoftWX.Model.tab_User();
            //myModel_tab_User = myBLL_tab_User.GetModel("id=" + intToUserID);
            int intShopClientID = Eggsoft_Public_CL.Pub_DeMode.Get_ShopClientID_From_UserID_(intToUserID);
            //if (intToUserID == 0)
            //{
            //    if (Pub_SocialPlatform.Check_SocialPlatform() == "WeiXin")
            //    {
            //        intToUserID = 13;
            //    }
            //    else if (Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
            //    {
            //        intToUserID = 5540;
            //    }
            //}

            EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User myModel_ToUserID_tab_User;
            EggsoftWX.Model.tab_User myModel_FromUserID_tab_User;

            myModel_ToUserID_tab_User = myBLL_tab_User.GetModel("id=" + intToUserID);
            myModel_FromUserID_tab_User = myBLL_tab_User.GetModel("id=" + intFromUserID);

            if (myModel_ToUserID_tab_User.SocialPlatform != myModel_FromUserID_tab_User.SocialPlatform)
            {
                return false;//不是一个平台 发送失败
            }


            string strSendURL = "";
            string strYiXin = Pub_SocialPlatform.Check_SocialPlatform(intToUserID);


            strSendURL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID) + "";
            //string strSendURL = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN() + "";
            string strJSON = ""; ;

            strJSON = "{\n";
            strJSON += "\"touser\":\"" + myModel_ToUserID_tab_User.OpenID + "\",\n";
            strJSON += "\"msgtype\":\"image\",\n";
            strJSON += "\"image\":\n";
            strJSON += "{\n";
            strJSON += "     \"media_id\":\"" + strSendMediaId + "\"\n";
            strJSON += "}\n";
            strJSON += "}\n";



            String strOK = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strSendURL, strJSON);

            bool SendTextWinXinMessage = false;
            if (strOK.ToLower().IndexOf("ok") != -1)
            {
                SendTextWinXinMessage = true;
                WXdbMsghelp.addUser_Chat_InfoToMDF(intToUserID, intFromUserID, strMsgShopCliengORSystem, "语音消息", strJSON, myModel_ToUserID_tab_User.OpenID, "");
            }
            return SendTextWinXinMessage;
        }

        public static int getUserIDFromCookiesPC()
        {/// 用户授权并获取code
         /// 
            String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();
            String strU_D = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName + "_USID");//sesion 不能太长
            //9999///
            /*本地才有效 防止误上传*/
            string localhosturl = Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting();// HttpContext.Current.Request.Url.Host;
            bool bool1 = localhosturl.IndexOf("local__host") != -1;


            int getUserIDFromCookies = 0;
            int.TryParse(strU_D, out getUserIDFromCookies);
            if (getUserIDFromCookies > 0)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("Index2 getUserIDFromCookies=" + getUserIDFromCookies);

                return getUserIDFromCookies;
            }
            else
            {

                return 0;
            }
        }


        public static int getUserIDFromCookies()
        {/// 用户授权并获取code
         /// 
            int intReturnUser32 = 0;

            String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();
            String strU_D = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName + "_USID");//sesion 不能太长
            //9999///
            /*本地才有效 防止误上传*/
            string localhosturl = Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting();// HttpContext.Current.Request.Url.Host;
            bool bool1 = localhosturl.IndexOf("local__host") != -1;
            if (bool1) strU_D = "151";

            String strLocalHostDebug_UseID_LocalHostDebug = System.Configuration.ConfigurationManager.AppSettings["LocalHostDebug_UseID_LocalHostDebug"];
            int int_UserID = 0;
            int.TryParse(strLocalHostDebug_UseID_LocalHostDebug, out int_UserID);
            if (int_UserID > 0)
            {
                strU_D = strLocalHostDebug_UseID_LocalHostDebug;
            }

            int getUserIDFromCookies = 0;
            int.TryParse(strU_D, out getUserIDFromCookies);
            if (getUserIDFromCookies > 0)
            {

                intReturnUser32 = getUserIDFromCookies;
            }
            else
            {
                string strOpenIDCode = get_OpenIDCode_SnsApi_Base();////微信open 入口

                EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User myModel_tab_User = new EggsoftWX.Model.tab_User();
                int intstrID = 0;
                if (myBLL_tab_User.Exists("OpenID='" + strOpenIDCode + "'") == true)
                {
                    myModel_tab_User = myBLL_tab_User.GetModel("OpenID='" + strOpenIDCode + "'");

                    intstrID = myModel_tab_User.ID;
                    Eggsoft.Common.debug_Log.Call_WriteLog("记录初始的转发路径 有利于证据核对 strID=" + intstrID, "转发路径证据核对");////记录初始的转发路径 有利于证据核对
                }
                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName + "_USID", intstrID.ToString());///决定如何检查网页授权的东东
                intReturnUser32 = intstrID;
            }



            return intReturnUser32;
        }

        public static bool IFCanSendWXget_IF_LocalhostTest_()
        {
            bool bool_IFsend = false;

            string localhosturl = Eggsoft.Common.Application.httpFullUrl();// HttpContext.Current.Request.Url.Host;
            string Md5localhosturl = Eggsoft.Common.DESCrypt.GetMd5Str32(localhosturl);

            string CacheKey = "IF_Can_SendWXget_IF_LocalhostTest_" + Md5localhosturl + "_";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {


                    bool bool1 = localhosturl.IndexOf("localhost:28199") != -1;
                    bool bool41 = localhosturl.IndexOf("localhost:58970") != -1;
                    bool bool2 = localhosturl.IndexOf("localhost/14WcfS") != -1;
                    bool bool3 = localhosturl.ToLower().IndexOf(((String)"noline.eggsoft.cn").ToLower()) != -1;
                    bool bool4 = localhosturl.ToLower().IndexOf(((String)"Test.CWB.wap.eggsoft.cn").ToLower()) != -1;
                    bool bool5 = localhosturl.ToLower().IndexOf(((String)"Test.ZJL.wap.eggsoft.cn").ToLower()) != -1;
                    bool bool6 = localhosturl.ToLower().IndexOf(((String)"test.eggsoft.cn").ToLower()) != -1;
                    bool bool7 = localhosturl.ToLower().IndexOf(((String)"Test.MMK.wap.eggsoft.cn").ToLower()) != -1;

                    bool bool10 = localhosturl.IndexOf("localhost:1587") != -1;
                    bool bool11 = localhosturl.IndexOf("localhost:1585") != -1;

                    bool bool16 = localhosturl.ToLower().IndexOf(((String)"testyixin.eggsoft.cn").ToLower()) != -1;


                    System.Web.Configuration.CompilationSection ds = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
                    bool isDebugEnable = ds.Debug;
                    //isDebugEnable = false;///肯定发送   发布要注释掉

                    bool bool_IFCanSendWXget_IF_LocalhostTest_ = bool1 || bool41 || bool2 || bool3 || bool4 || bool5 || bool6 || bool7 || bool10 || bool16 || bool11;
                    bool_IFsend = !bool_IFCanSendWXget_IF_LocalhostTest_;
                    if (isDebugEnable == false) bool_IFsend = true;


                    Eggsoft.Common.DataCache.SetCache(CacheKey, bool_IFsend);// 写入缓存   
                }
                catch (System.Threading.ThreadAbortException e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
                }
                catch (Exception eee)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(eee);
                }
            }
            else
            {
                bool_IFsend = (bool)objType;
            }

            bool bool_IFNotsend = !(bool_IFsend);
            return bool_IFNotsend;
        }



        public static String get_OpenIDCode_SnsApi_Base()
        {/// 用户授权并获取code
         /// 
            int intShopClientID = 0;
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            int.TryParse(strShopClientID, out intShopClientID);
            String strApplicationCheckName = Eggsoft_Public_CL.Pub.GetAppConfiug_ApplicationCheckName();

            String strOpenIDCode = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName);//sesion 不能太长

            string localhosturl = Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting();// HttpContext.Current.Request.Url.Host;
            bool bool1 = localhosturl.IndexOf("localhost:28199") != -1;
            bool bool41 = localhosturl.IndexOf("local__host:58970") != -1;
            bool bool42 = localhosturl.ToLower().IndexOf(((String)"csoliver.eggsoft.cn").ToLower()) != -1;//张廷锋 微商技术  id=2  还要设置店铺的地方  店铺默认是shopClientID=1
            bool bool43 = localhosturl.ToLower().IndexOf(((String)"192.168.0.108").ToLower()) != -1;//张廷锋 微商技术  id=2  还要设置店铺的地方  店铺默认是shopClientID=1
            bool bool4 = localhosturl.ToLower().IndexOf(((String)"TestCWB.eggsoft.cn").ToLower()) != -1;
            bool bool5 = localhosturl.ToLower().IndexOf(((String)"Test.ZJL.wap.eggsoft.cn").ToLower()) != -1;
            bool bool6 = localhosturl.ToLower().IndexOf(((String)"test.eggsoft.cn").ToLower()) != -1;
            bool bool7 = localhosturl.ToLower().IndexOf(((String)"Test.MMK.wap.eggsoft.cn").ToLower()) != -1;

            bool bool10 = localhosturl.IndexOf("localhost:1587") != -1;
            bool bool16 = localhosturl.ToLower().IndexOf(((String)"testyixin.eggsoft.cn").ToLower()) != -1;

            //Eggsoft.Common.debug_Log.Call_WriteLog("3112");

            if ((bool1) || (bool41))
            {
                // strOpenIDCode = "oHUlduM0ISGy8gTO3lFRbxndm_A0";///张廷锋 微商技术  id=11 naan 测试   
                //strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///张廷锋 微商技术  id=2   时仪电子的号 
                ///
                strOpenIDCode = "och_MjsZaKTyq6ciyE5yfhTBNc7U";///张廷锋 微商技术  id=14 o2o 测试   
                //strOpenIDCode = "och_MjgB6idnDO_y-TAmDyUmfqgk";///王录 id=15 测试   
                //strOpenIDCode = "och_Mjof3yYIs0WUyLhf7jr5K2OE";///微云基石CEO号 id=47 o2o 测试
                //strOpenIDCode = "och_MjlaMDjyHd9QLbKrJOd-BeMg";///豪哥 id=98 o2o 测试

                //strOpenIDCode = "oFif2jsRiJRHzTIu1WAJVqYVk-ww";///微云基石CEO号  id=5     //2个切换 测试代理使用
                //strOpenIDCode = "och_MjhYzDiNuVUzHK8izIC0yZR8";///王录  id=34     //2个切换 测试代理使用

                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if ((bool42))
            {
                // strOpenIDCode = "oHUlduM0ISGy8gTO3lFRbxndm_A0";///张廷锋 微商技术  id=11 naan 测试   
                ////strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///张廷锋 微商技术  id=2   
                ///              
                strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///张廷锋 微商技术  id=14 o2o 测试   
                //strOpenIDCode = "och_MjgB6idnDO_y-TAmDyUmfqgk";///王录 id=15 测试   
                //strOpenIDCode = "oRDXqjko3kyIL_tHZkM5FrqiGVyY";///张廷锋 微商技术  id=19 o2o 测试     id=18998



                //strOpenIDCode = "oFif2jsRiJRHzTIu1WAJVqYVk-ww";///微云基石CEO号  id=5     //2个切换 测试代理使用
                //strOpenIDCode = "och_MjhYzDiNuVUzHK8izIC0yZR8";///王录  id=34     //2个切换 测试代理使用

                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if ((bool43))
            {
                // strOpenIDCode = "oHUlduM0ISGy8gTO3lFRbxndm_A0";///张廷锋 微商技术  id=11 naan 测试   
                ////strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///张廷锋 微商技术  id=2   
                ///
                //strOpenIDCode = "och_MjsZaKTyq6ciyE5yfhTBNc7U";///张廷锋 微商技术  id=14 o2o 测试   
                strOpenIDCode = "o4DJXuJTZ0mcL0RlZELcUBeZa_VM";///张廷锋 微商技术  id=1461 1461 优生活 测试   



                //strOpenIDCode = "oFif2jsRiJRHzTIu1WAJVqYVk-ww";///微云基石CEO号  id=5     //2个切换 测试代理使用
                //strOpenIDCode = "och_MjhYzDiNuVUzHK8izIC0yZR8";///王录  id=34     //2个切换 测试代理使用

                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if (bool4)
            {
                strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///本地调试用  CWB 1366
                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if (bool6)
            {
                strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///本地调试用  CNN
                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if (bool1 || bool4 || bool5 || bool6 || bool7)
            {   //oIcdQuGfm942Mp0_W8sZM_sEUzJg   oIcdQuHFHXYo6uM2dHihP3tUhqHI      
                //婷子    oIcdQuKL-6WmrlxGtDxxa79p94LE  
                //张俊林 oIcdQuPw3Ks5ER15_WehSvC_zfX0
                //查理 oIcdQuK3g810UhihHMz6-dc2MU08
                //增初选 oIcdQuLUrLbzjJfOFQRfhc1fNbSQ
                //oIcdQuNDVNtEcr4Bl0gZOMs2xUo8  陈王斌
                //张朝晖 oIcdQuGfm942Mp0_W8sZM_sEUzJg
                strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";/// //我的oIcdQuGfm942Mp0_W8sZM_sEUzJg
                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            else if ((bool10) || (bool16))
            {
                strOpenIDCode = "oFif2jnbW87ScolQ8wi7EOnO_D9k";///本地调试用  我手机的易信  18917905147
                Eggsoft.Common.Cookie.Add(strApplicationCheckName, strApplicationCheckName, strOpenIDCode);
                return strOpenIDCode;
            }
            //HttpContext.Current.Response.Write("strOpenIDCode=" + strOpenIDCode);
            //HttpContext.Current.Response.End();
            //Eggsoft.Common.debug_Log.Call_WriteLog("strOpenIDCode=" + strOpenIDCode, "未关注");
            /*完美的做法是snsapi_base  首次 发起 看有用户信息否 如果没有 则请求一次授权   就看 nickname是否存在发起请求*/
            if (String.IsNullOrEmpty(strOpenIDCode))
            {
                try
                {

                    #region 首次加载中 snsapi_base
                    string strAspxCallBackURL = HttpUtility.UrlEncode(localhosturl.Replace("http://", "https://"));
                    Eggsoft.Common.JsUtil.LocationNewHref("/User/Html5LocalStorge.aspx?AspxCallBackURL=" + strAspxCallBackURL);////现在没有本地化存储信息 跳至目标
                    HttpContext.Current.Response.End();
                    return null;
                    #endregion
                }
                catch (System.Threading.ThreadAbortException e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }

                finally { }

                return null;
            }
            else//这是授权之后跳回来了
            {
                String strScopeAccess_token = "";//sesion 不能太长
                String strscope = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName + "_Scope");//sesion 不能太长
                if (strscope == "snsapi_userinfo") strScopeAccess_token = Eggsoft.Common.Cookie.Read(strApplicationCheckName, strApplicationCheckName + "_S_A_t");//sesion 不能太长

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                if (BLL_tab_User.Exists("OpenID='" + strOpenIDCode + "' and Subscribe=1  and ShopClientID=" + intShopClientID))//存在并关注过 snsapi_userinfo不必再调用
                {
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info(intShopClientID, strOpenIDCode, false); ///用户可能是网页授权 这样也可以获取信息//  调用一次 do location信息
                    return strOpenIDCode;
                }
                else
                {

                    try
                    {
                        if (BLL_tab_User.Exists("OpenID='" + strOpenIDCode + "' and ShopClientID=" + intShopClientID))//存在记录 但是可能没网页授权过
                        {
                            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("OpenID='" + strOpenIDCode + "' and ShopClientID=" + intShopClientID);
                            if ((strscope == "snsapi_base"))
                            {
                                string strRedirect_uri = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/WxURL/myOauth1-" + intShopClientID + ".aspx";
                                strRedirect_uri = strRedirect_uri.ToLower();

                                if (localhosturl.ToLower().IndexOf("03custompay.aspx") != -1)
                                {
                                }
                                else
                                {
                                    #region 再次发起请求 snsapi_userinfo 相同语句

                                    string strWeiXinAppId = "";
                                    if (intShopClientID == 0)
                                    {
                                        strWeiXinAppId = tab_System_And_.getTab_System("WeiXinAppId");
                                    }
                                    else
                                    {
                                        EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                                        EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                                        Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                                        if (Model_tab_ShopClient_EngineerMode != null)
                                        {
                                            strWeiXinAppId = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                                        }
                                    }


                                    #region tab_ShopClient_WeiXin_stateurl   转换到一个整数
                                    EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl BLL_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl();
                                    bool boolstrstate = BLL_tab_ShopClient_WeiXin_Stateurl.Exists("UrlFrom='" + localhosturl + "' and ShopClientID=" + intShopClientID);
                                    Int32 int32State = 0;
                                    if (boolstrstate)
                                    {
                                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = BLL_tab_ShopClient_WeiXin_Stateurl.GetModel("UrlFrom='" + localhosturl + "'");
                                        Model_tab_ShopClient_WeiXin_Stateurl.updateTime = DateTime.Now;
                                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount += 1;
                                        BLL_tab_ShopClient_WeiXin_Stateurl.Update(Model_tab_ShopClient_WeiXin_Stateurl);
                                        int32State = Model_tab_ShopClient_WeiXin_Stateurl.ID;
                                    }
                                    else
                                    {
                                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl();
                                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount = 1;
                                        Model_tab_ShopClient_WeiXin_Stateurl.ShopClientID = intShopClientID;
                                        Model_tab_ShopClient_WeiXin_Stateurl.UrlFrom = Eggsoft.Common.StringNum.MaxLengthString(localhosturl,400);
                                        int32State = BLL_tab_ShopClient_WeiXin_Stateurl.Add(Model_tab_ShopClient_WeiXin_Stateurl);
                                    }
                                    #endregion

                                    string strWXRedirect_uri = System.Web.HttpContext.Current.Server.UrlEncode(strRedirect_uri);
                                    string strmyOauth1URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strWeiXinAppId + "&redirect_uri=" + strWXRedirect_uri + "&response_type=code&scope=snsapi_userinfo&state=" + int32State + "#wechat_redirect";

                                    Eggsoft.Common.JsUtil.TipAndRedirect("亲,首次加载中", strmyOauth1URL, 1);
                                    HttpContext.Current.Response.End();
                                    return null;
                                    #endregion 再次发起请求
                                }


                            }
                            else if (strscope == "snsapi_userinfo")//更新一下信息
                            {

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info_scope_snsapi_userinfo(intShopClientID, strOpenIDCode, strScopeAccess_token); ///用户可能是网页授权 这样也可以获取信息//  调用一次 do location信息
                            }
                        }
                        else
                        {//还没有记录

                            if (strscope == "snsapi_base")
                            {


                                #region 再次发起请求 snsapi_userinfo 相同语句
                                string strRedirect_uri = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/WxURL/myOauth1-" + intShopClientID + ".aspx";
                                if (localhosturl.ToLower().IndexOf("03custompay.aspx") != -1)
                                {
                                    #region ///这是授权之后跳回来了
                                    EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                                    Model_tab_User.OpenID = strOpenIDCode;
                                    Model_tab_User.SocialPlatform = Pub_SocialPlatform.Check_SocialPlatform();
                                    Model_tab_User.ShopClientID = intShopClientID;
                                    Model_tab_User.Api_Authorize = true;
                                    Model_tab_User.NickName = "自助支付类型";
                                    Model_tab_User.InsertTime = DateTime.Now;
                                    Model_tab_User.Updatetime = DateTime.Now;
                                    //Model_tab_User.TeamID=;
                                    Int32 myint = BLL_tab_User.Add(Model_tab_User);
                                    #endregion

                                    #region 更新b005_UserID_Operation_ID

                                    #endregion 更新b005_UserID_Operation_ID
                                }
                                else
                                {
                                    #region 必须 授权的


                                    #region =tab_ShopClient_WeiXin_stateurl   转换到一个整数
                                    EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl BLL_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl();
                                    bool boolstrstate = BLL_tab_ShopClient_WeiXin_Stateurl.Exists("UrlFrom='" + localhosturl + "' and ShopClientID=" + intShopClientID);
                                    Int32 int32State = 0;
                                    if (boolstrstate)
                                    {
                                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = BLL_tab_ShopClient_WeiXin_Stateurl.GetModel("UrlFrom='" + localhosturl + "'");
                                        Model_tab_ShopClient_WeiXin_Stateurl.updateTime = DateTime.Now;
                                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount += 1;
                                        BLL_tab_ShopClient_WeiXin_Stateurl.Update(Model_tab_ShopClient_WeiXin_Stateurl);
                                        int32State = Model_tab_ShopClient_WeiXin_Stateurl.ID;
                                    }
                                    else
                                    {
                                        EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl();
                                        Model_tab_ShopClient_WeiXin_Stateurl.intFromCount = 1;
                                        Model_tab_ShopClient_WeiXin_Stateurl.UrlFrom = Eggsoft.Common.StringNum.MaxLengthString(localhosturl,400);
                                        Model_tab_ShopClient_WeiXin_Stateurl.ShopClientID = intShopClientID;
                                        int32State = BLL_tab_ShopClient_WeiXin_Stateurl.Add(Model_tab_ShopClient_WeiXin_Stateurl);
                                    }
                                    #endregion


                                    string strWeiXinAppId = "";

                                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                                    Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                                    if (Model_tab_ShopClient_EngineerMode != null)
                                    {
                                        strWeiXinAppId = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                                    }


                                    string strWXRedirect_uri = System.Web.HttpContext.Current.Server.UrlEncode(strRedirect_uri);
                                    string strmyOauth1URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + strWeiXinAppId + "&redirect_uri=" + strWXRedirect_uri + "&response_type=code&scope=snsapi_userinfo&state=" + int32State + "#wechat_redirect";

                                    Eggsoft.Common.JsUtil.TipAndRedirect("亲,加载中", strmyOauth1URL, 1);
                                    HttpContext.Current.Response.End();
                                    return strOpenIDCode;
                                    #endregion 必须 授权的

                                }
                                #endregion 再次发起请求
                            }
                            else if (strscope == "snsapi_userinfo")
                            {
                                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                                Model_tab_User.OpenID = strOpenIDCode;
                                //Model_tab_User.Api_Authorize = true;//由于发现路光用的机器 授权后不能访问 这里直接授权 而不是重新发起访问 myOauth1.aspx
                                Model_tab_User.SocialPlatform = Pub_SocialPlatform.Check_SocialPlatform();
                                Model_tab_User.ShopClientID = intShopClientID;


                                ////废弃以下项目
                                //#region 处理菜单过来的链接 肯定是关注过的才能点击进来  补充已经运行过的一段时间的公众平台
                                //string strSubscribe = HttpContext.Current.Request["subscribe"];
                                //bool boolSubscribe = false;
                                //bool.TryParse(strSubscribe, out boolSubscribe);
                                //if (boolSubscribe) Model_tab_User.Subscribe = true;
                                //#endregion

                                Model_tab_User.InsertTime = DateTime.Now;
                                Model_tab_User.Updatetime = DateTime.Now;

                                Int32 myint = BLL_tab_User.Add(Model_tab_User);

                                #region 给首次访问的用户送购物红包
                                Eggsoft_Public_CL.Pub.SendYouhuiquan100Dollor_ToFirstVisit(myint);
                                #endregion
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.Getuser_info_scope_snsapi_userinfo(intShopClientID, strOpenIDCode, strScopeAccess_token); ///用户可能是网页授权 这样也可以获取信息//  调用一次 do location信息
                            }
                        }
                        return strOpenIDCode;//破机器 不管了  由于发现路光用的机器 授权后不能访问 这里直接返回  这里直接授权 而不是重新发起访问 myOauth1.aspx
                    }
                    catch (System.Threading.ThreadAbortException e)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(e, "线程异常");
                    }
                    catch (Exception eee)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(eee);
                        HttpContext.Current.Response.End();
                    }
                    return null;
                }

            }
        }





        public class json_OBJ_OpenID_userInfo
        {///
            //       {
            //    "subscribe": 1, 
            //    "openid": "o6_bmjrPTlm6_2sgVt7hMZOPfL2M", 
            //    "nickname": "Band", 
            //    "sex": 1, 
            //    "language": "zh_CN", 
            //    "city": "广州", 
            //    "province": "广东", 
            //    "country": "中国", 
            //    "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0", 
            //   "subscribe_time": 1382694957
            //}
            public bool subscribe { get; set; }
            public string nickname { get; set; }
            public string sex { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string headimgurl { get; set; }
            public string openid { get; set; }
        }


        public class json_OBJ_OpenID_userInfo_snsapi_userinfo
        {

            //2015-2-5 19:24:40:str_json_OpenID_userInfo:{"openid":"oFif2jnbW87ScolQ8wi7EOnO_D9k","nickname":"张廷锋 微商技术","sex":1,"language":"zh_CN","city":"闵行","province":"上海","country":"中国","headimgurl":"http:\/\/wx.qlogo.cn\/mmopen\/Tw5eoIxYxFBTTYVpgVxVNshl5SV1sSyJ0Bl6IwgJ60Oq6zrxu4CRJRCAsmzxeeo91zdDZXtamzVMXGFxtJfNyEXQNHiatiaxme\/0","privilege":[]}

            //          "openid":" OPENID",
            //" nickname": NICKNAME,
            //"sex":"1",
            //"province":"PROVINCE"
            //"city":"CITY",
            //"country":"COUNTRY",
            // "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46", 
            // "privilege":[
            // "PRIVILEGE1"
            // "PRIVILEGE2"
            // ]
            // "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            public string openid { get; set; }
            public string nickname { get; set; }
            public string sex { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string headimgurl { get; set; }
            public string subscribe { get; set; }
            /// <summary>
            /// 统一微信ID  非常重要
            /// </summary>
            public string unionid { get; set; }

        }


        public static String Getuser_OpenID(int intUserID)
        {
            EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User myModel_tab_User = myBLL_tab_User.GetModel(intUserID); ///关注时产生的 肯定存在
            if (myModel_tab_User != null)
            {                                                                                 ///
                return myModel_tab_User.OpenID;
            }
            else
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("Getuser_OpenID(int intUserID)=" + intUserID);
                return "";
            }
        }

        public static void Getuser_info(int intShopClientID, string strOpenID, bool boolWeiXinVisit = true)
        {
            try
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog("intShopClientID=" + intShopClientID);
                //Eggsoft.Common.debug_Log.Call_WriteLog("strOpenID=" + strOpenID);



                EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User myModel_tab_User = myBLL_tab_User.GetModel("OpenID='" + strOpenID + "'  and ShopClientID=" + intShopClientID); ///关注时产生的 肯定存在
                if (myModel_tab_User == null)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("AAAAA警告 该SQL不存在 " + "OpenID='" + strOpenID + "'  and ShopClientID=" + intShopClientID);
                    return;
                }
                ///调用一次 do location信息
                ///
                string strXin = Pub_SocialPlatform.Check_SocialPlatform(myModel_tab_User.ID);
                String strGet_ACCESS_TOKEN = Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID);
                //Eggsoft.Common.debug_Log.Call_WriteLog("strGet_ACCESS_TOKEN=" + strGet_ACCESS_TOKEN);
                string strrenderReverse = "";

                //String strSocialPlatform = myModel_tab_User.SocialPlatform;

                //if (myModel_tab_User.SocialPlatform == "WeiXin")
                //{
                strrenderReverse = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + strGet_ACCESS_TOKEN + "&openid=" + strOpenID + "&lang=zh_CN";
                string str_json_OpenID_userInfo = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strrenderReverse);

                json_OBJ_OpenID_userInfo json_OpenID_userInfo = JsonHelper.JsonDeserialize<json_OBJ_OpenID_userInfo>(str_json_OpenID_userInfo);
                myModel_tab_User.Subscribe = json_OpenID_userInfo.subscribe;
                if (string.IsNullOrEmpty(json_OpenID_userInfo.nickname) == false) myModel_tab_User.NickName = Eggsoft.Common.CommUtil.SafeFilter(json_OpenID_userInfo.nickname);
                if (string.IsNullOrEmpty(json_OpenID_userInfo.headimgurl) == false)
                {
                    myModel_tab_User.HeadImageUrl = json_OpenID_userInfo.headimgurl;
                    string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClientID);
                    string strHead_User_Image = strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(myModel_tab_User.ID, 6) + ".jpg";
                    Eggsoft_Public_CL.GoodP.DownLoadFile_Service_ScaleBMP(myModel_tab_User.HeadImageUrl, strHead_User_Image, 80, 80, "HW");

                    Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopClientID);

                }
                if (string.IsNullOrEmpty(json_OpenID_userInfo.country) == false)
                {
                    if (json_OpenID_userInfo.country == "中国")
                    {
                        myModel_tab_User.Country = json_OpenID_userInfo.country;
                        if (string.IsNullOrEmpty(json_OpenID_userInfo.province) == false) myModel_tab_User.Sheng = json_OpenID_userInfo.province;
                        if (string.IsNullOrEmpty(json_OpenID_userInfo.city) == false) myModel_tab_User.City = json_OpenID_userInfo.city;
                    }
                }
                if (boolWeiXinVisit) myModel_tab_User.Updatetime = DateTime.Now;
                myBLL_tab_User.Update(myModel_tab_User);
            }

            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }

            finally { }
        }

        /// <summary>
        /// 商城注册关注完成后还是显示请关注公众号的提示，这个问题好久了，看看是什么原因
        /// </summary>
        /// <param name="intShopClientID"></param>
        /// <param name="strOpenID"></param>
        /// <returns></returns>

        public static bool CaiLiangIFSsubscribe(int intShopClientID, string strOpenID)
        {
            #region 技巧性处理 用于去是否关注
            String strGet_ACCESS_TOKEN = Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID);
            string strSsubscribeReverse = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + strGet_ACCESS_TOKEN + "&openid=" + strOpenID + "&lang=zh_CN";
            string str_json_OpenID_userInfo_Ssubscribe = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strSsubscribeReverse);
            json_OBJ_OpenID_userInfo_snsapi_userinfo json_OpenID_userInfo_Ssubscribe = JsonHelper.JsonDeserialize<json_OBJ_OpenID_userInfo_snsapi_userinfo>(str_json_OpenID_userInfo_Ssubscribe);
            string strSsubscribe = json_OpenID_userInfo_Ssubscribe.subscribe;
            bool boolSsubscribe = (strSsubscribe == "1" ? true : false);///同时就判断是否关注
            #endregion

            return boolSsubscribe;
        }
        public static void Getuser_info_scope_snsapi_userinfo(int intShopClientID, string strOpenID, string straccess_token)
        {
            try
            {
                EggsoftWX.BLL.tab_User myBLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User myModel_tab_User = myBLL_tab_User.GetModel("OpenID='" + strOpenID + "'  and ShopClientID=" + intShopClientID); ///关注时产生的 肯定存在

                ///调用一次 do location信息
                ///
                string strrenderReverse = "";

                String strSocialPlatform = myModel_tab_User.SocialPlatform;

                strrenderReverse = "https://api.weixin.qq.com/sns/userinfo?access_token=" + straccess_token + "&openid=" + strOpenID + "&lang=zh_CN";
                string str_json_OpenID_userInfo = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strrenderReverse);
                Eggsoft.Common.debug_Log.Call_WriteLog(str_json_OpenID_userInfo, "统一UnionID", "微信小程序与微信公众号同一用户登录问题");
                json_OBJ_OpenID_userInfo_snsapi_userinfo json_OpenID_userInfo = JsonHelper.JsonDeserialize<json_OBJ_OpenID_userInfo_snsapi_userinfo>(str_json_OpenID_userInfo);

                bool boolSsubscribe = CaiLiangIFSsubscribe(intShopClientID, strOpenID);

                if (string.IsNullOrEmpty(json_OpenID_userInfo.nickname) == false) myModel_tab_User.NickName = Eggsoft.Common.CommUtil.SafeFilter(json_OpenID_userInfo.nickname);
                if (string.IsNullOrEmpty(json_OpenID_userInfo.headimgurl) == false)
                {
                    myModel_tab_User.HeadImageUrl = json_OpenID_userInfo.headimgurl;

                    string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClientID);

                    string strHead_User_Image = strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(myModel_tab_User.ID, 6) + ".jpg";
                    Eggsoft_Public_CL.GoodP.DownLoadFile_Service_ScaleBMP(myModel_tab_User.HeadImageUrl, strHead_User_Image, 80, 80, "HW");
                    Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(intShopClientID);
                }
                if (string.IsNullOrEmpty(json_OpenID_userInfo.country) == false) myModel_tab_User.Country = json_OpenID_userInfo.country;
                if (string.IsNullOrEmpty(json_OpenID_userInfo.province) == false) myModel_tab_User.Sheng = json_OpenID_userInfo.province;
                if (string.IsNullOrEmpty(json_OpenID_userInfo.city) == false) myModel_tab_User.City = json_OpenID_userInfo.city;
                if (string.IsNullOrEmpty(json_OpenID_userInfo.unionid) == false) myModel_tab_User.unionID = json_OpenID_userInfo.unionid;

                myModel_tab_User.Subscribe = boolSsubscribe;
                myModel_tab_User.Api_Authorize = true;///这是网页授权
                myBLL_tab_User.Update(myModel_tab_User);
            }

            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }

            finally { }
        }


        #region json baidu API  dizhi
        public class JsonCity
        {
            public JsonCity()
            {

            }
            public string status { get; set; }
            public Result result { get; set; }

        }
        public class Result
        {
            public Location location { get; set; }
            public string business { get; set; }
            public string formatted_address { get; set; }
            public AddressComponent addressComponent { get; set; }


        }
        public class Location
        {
            public string lng { get; set; }
            public string lat { get; set; }
        }
        public class AddressComponent
        {
            public string city { get; set; }
            public string district { get; set; }
            public string province { get; set; }
            public string street { get; set; }
            public string street_number { get; set; }

        }

        public static void DoBaiDuAPILocation(string strweixinXML, string strOpenID)
        {
            #region baidu API  dizhi
            string str_UserID = Eggsoft_Public_CL.Pub.GetUserIDFromOpenID(strOpenID);

            try
            {


                EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat BLL_tab_ShopClient_User_Lng_Lat = new EggsoftWX.BLL.tab_ShopClient_User_Lng_Lat();
                EggsoftWX.Model.tab_ShopClient_User_Lng_Lat Model_tab_ShopClient_User_Lng_Lat = new EggsoftWX.Model.tab_ShopClient_User_Lng_Lat();
                //if (BLL_tab_User.Exists("OpenID='" + strOpenID + "'"))
                //{
                WX_Model.WX_Model_EventLOCATION my_WX_Model_EventLOCATION = new WX_Model.WX_Model_EventLOCATION();
                WX_Model.WX_Model_EventLOCATION ddEventLOCATION = my_WX_Model_EventLOCATION.GetWX_Model_EventLOCATION(strweixinXML);

                string Latitude = ddEventLOCATION.EventLatitude;
                string Longitude = ddEventLOCATION.EventLongitude;

                string getBaiDugpsX = "";
                string getBaiDugpsY = "";

                Eggsoft.Common.GPS.getBaiDugps(Latitude, Longitude, out getBaiDugpsX, out getBaiDugpsY);
                //int LatitudeLeft = strweixinXML.IndexOf("<Latitude>");
                //int LatitudeRight = strweixinXML.IndexOf("</Latitude>");
                //int LongitudeLeft = strweixinXML.IndexOf("<Longitude>");
                //int LongitudeRight = strweixinXML.IndexOf("</Longitude>");
                //string Latitude = strweixinXML.Substring(LatitudeLeft + "<Latitude>".Length, LatitudeRight - (LatitudeLeft + "<Latitude>".Length));
                //string Longitude = strweixinXML.Substring(LongitudeLeft + "<Longitude>".Length, LongitudeRight - (LongitudeLeft + "<Longitude>".Length));




                ///调用一次 do location信息
                ///
                string strrenderReverse = "https://api.map.baidu.com/geocoder/v2/?ak=D115c637a1d10e58c7ed20711db00cca&callback=renderReverse&location=" + getBaiDugpsX + "," + getBaiDugpsY + "&output=json&pois=1";

                string str_json_OpenID_userInfo = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strrenderReverse);
                int indexLeftPos = str_json_OpenID_userInfo.IndexOf('(');
                int indexRightPos = str_json_OpenID_userInfo.Length - 1;

                str_json_OpenID_userInfo = str_json_OpenID_userInfo.Substring(indexLeftPos + 1, indexRightPos - indexLeftPos - 1);
                JsonCity myjson_api_map_baidu_com_geocoder = JsonHelper.JsonDeserialize<JsonCity>(str_json_OpenID_userInfo);

                Model_tab_ShopClient_User_Lng_Lat.UserID = Int32.Parse(str_UserID);
                Model_tab_ShopClient_User_Lng_Lat.lat = Latitude;
                Model_tab_ShopClient_User_Lng_Lat.lng = Longitude;
                Model_tab_ShopClient_User_Lng_Lat.BaiDulat = getBaiDugpsX;
                Model_tab_ShopClient_User_Lng_Lat.BaiDulng = getBaiDugpsY;
                Model_tab_ShopClient_User_Lng_Lat.aspxDescription = "微信公众平台地理位置";

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
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }

            finally { }
            //Model_tab_User = BLL_tab_User.GetModel("OpenID='" + strOpenID + "'");
            //if (string.IsNullOrEmpty(Model_tab_User.City) == true) Model_tab_User.City = myjson_api_map_baidu_com_geocoder.result.addressComponent.city;
            //if (string.IsNullOrEmpty(Model_tab_User.Sheng) == true) Model_tab_User.Sheng = myjson_api_map_baidu_com_geocoder.result.addressComponent.province;

            //BLL_tab_User.Update(Model_tab_User);


            // <?xml version="1.0" encoding="utf-8" ?> 
            //<GeocoderSearchResponse> 
            //    <status>0</status>
            //    <result>
            //                    <location>
            //                <lat>31.169362832725</lat>
            //                <lng>121.3429180069</lng>
            //            </location>
            //            <formatted_address>上海市闵行区沪星路299弄-65号</formatted_address>
            //            <business>七宝,航华</business>
            //            <addressComponent>
            //                <streetNumber></streetNumber>
            //                <street>沪星路</street>
            //                <district>闵行区</district>
            //                <city>上海市</city>
            //                <province>上海市</province>
            //            </addressComponent>
            //                            <cityCode>289</cityCode>
            //                                        <pois>
            //                            </pois>
            //            </result>	
            //</GeocoderSearchResponse>

            #endregion baidu API  dizhi

            //renderReverse&&renderReverse({"status":0,"result":{"location":{"lng":121.3429180069,"lat":31.169362832725},"formatted_address":"上海市闵行区沪星路299弄-65号","business":"七宝,航华","addressComponent":{"city":"上海市","district":"闵行区","province":"上海市","street":"沪星路","street_number":"299弄-65号"},"pois":[],"cityCode":289}})    

        }
        #endregion json baidu API  dizhi


    }
}