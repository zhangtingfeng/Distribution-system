using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace _14WcfS.User
{
    /// <summary>
    /// WS_Agent_ChoiceGoods1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_Agent_ChoiceGoods1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private static object ojb_Service_Agent_Save = new object();

        [WebMethod]
        /// <summary>
        /// 代理管理 
        /// </summary>
        /// <param name="strGoodID">代理管理  </param>
        /// <param name="strParentID">分享人</param>
        /// <returns></returns>
        public String _Service_Agent_Save()
        {

            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            String strShopClientID = context.QueryString["ShopClientID"];
            String strUserID = context.QueryString["UserID"];
            String strParentID = context.QueryString["ParentID"];
            String strShopName = System.Web.HttpUtility.UrlDecode(context.QueryString["ShopName"], System.Text.UTF8Encoding.UTF8);
            String strContactName = System.Web.HttpUtility.UrlDecode(context.QueryString["ContactName"], System.Text.UTF8Encoding.UTF8);
            String strContactMobile = context.QueryString["ContactMobile"];
            String strAlipayOrWeiXinPay = System.Web.HttpUtility.UrlDecode(context.QueryString["AlipayOrWeiXinPay"], System.Text.UTF8Encoding.UTF8);
            String strChoiceGoodList = context.QueryString["ChoiceGoodList"];
            String strAgentAdLevel = context.QueryString["AgentAdLevel"];
            String strAddExp0 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp0"], System.Text.UTF8Encoding.UTF8);
            String strAddExp1 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp1"], System.Text.UTF8Encoding.UTF8);
            String strAddExp2 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp2"], System.Text.UTF8Encoding.UTF8);
            String strAddExp3 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp3"], System.Text.UTF8Encoding.UTF8);
            String strAddExp4 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp4"], System.Text.UTF8Encoding.UTF8);
            String strAddExp5 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp5"], System.Text.UTF8Encoding.UTF8);
            String strAddExp6 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp6"], System.Text.UTF8Encoding.UTF8);
            String strAddExp7 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp7"], System.Text.UTF8Encoding.UTF8);
            String strAddExp8 = System.Web.HttpUtility.UrlDecode(context.QueryString["AddExp8"], System.Text.UTF8Encoding.UTF8);

            strUserID = Eggsoft.Common.CommUtil.SafeFilter(strUserID);
            strParentID = Eggsoft.Common.CommUtil.SafeFilter(strParentID);
            strShopName = Eggsoft.Common.CommUtil.SafeFilter(strShopName);
            strContactName = Eggsoft.Common.CommUtil.SafeFilter(strContactName);
            strContactMobile = Eggsoft.Common.CommUtil.SafeFilter(strContactMobile);
            strAlipayOrWeiXinPay = Eggsoft.Common.CommUtil.SafeFilter(strAlipayOrWeiXinPay);
            strAgentAdLevel = Eggsoft.Common.CommUtil.SafeFilter(strAgentAdLevel);
            strAddExp0 = Eggsoft.Common.CommUtil.SafeFilter(strAddExp0);
            strAddExp1 = Eggsoft.Common.CommUtil.SafeFilter(strAddExp1);
            strAddExp2 = Eggsoft.Common.CommUtil.SafeFilter(strAddExp2);

        
            Eggsoft.Common.debug_Log.Call_WriteLog("进行授权处理strUserID=" + strUserID);

            int intErrorCode = 0;


            try
            {
                lock (ojb_Service_Agent_Save)
                {
                    int intAgentAdLevel = 0;
                    int.TryParse(strAgentAdLevel, out intAgentAdLevel);


                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent = new EggsoftWX.Model.tab_ShopClient_Agent_();

                    bool bool_AgentUserID = BLL_tab_ShopClient_Agent_.Exists("UserID=" + strUserID);


                    //用户按了保存
                    bool boolEveryOneAutoAgentOnlyIsAngel = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "EveryOneAutoAgentOnlyIsAngel");


                    if (bool_AgentUserID == false)//还没有开店
                    {
                        if (strParentID == strUserID) strParentID = "0";//一个人的父亲不可能是自己 处理 上级(NAAN--Anna) 的问题
                        Model_tab_ShopClient_Agent = new EggsoftWX.Model.tab_ShopClient_Agent_();
                        Model_tab_ShopClient_Agent.UserID = Int32.Parse(strUserID);
                        if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID) == Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strParentID))
                        {
                            Model_tab_ShopClient_Agent.ParentID = Int32.Parse(strParentID);
                        }
                        else
                        {

                        }
                        Model_tab_ShopClient_Agent.ShopClientID = strShopClientID.toInt32();
                        Model_tab_ShopClient_Agent.ShopName = strShopName;
                        Model_tab_ShopClient_Agent.CreatTime = DateTime.Now;
                        Model_tab_ShopClient_Agent.CreateBy = "用户申请";
                        Model_tab_ShopClient_Agent.AgentLevelSelect = intAgentAdLevel;
                        if (String.IsNullOrEmpty(strAddExp0) == false) Model_tab_ShopClient_Agent.AddExp0 = strAddExp0;
                        if (String.IsNullOrEmpty(strAddExp1) == false) Model_tab_ShopClient_Agent.AddExp1 = strAddExp1;
                        if (String.IsNullOrEmpty(strAddExp2) == false) Model_tab_ShopClient_Agent.AddExp2 = strAddExp2;
                        if (String.IsNullOrEmpty(strAddExp3) == false) Model_tab_ShopClient_Agent.AddExp3 = strAddExp3;
                        if (String.IsNullOrEmpty(strAddExp4) == false) Model_tab_ShopClient_Agent.AddExp4 = strAddExp4;
                        if (String.IsNullOrEmpty(strAddExp5) == false) Model_tab_ShopClient_Agent.AddExp5 = strAddExp5;
                        if (String.IsNullOrEmpty(strAddExp6) == false) Model_tab_ShopClient_Agent.AddExp6 = strAddExp6;
                        if (String.IsNullOrEmpty(strAddExp7) == false) Model_tab_ShopClient_Agent.AddExp7 = strAddExp7;
                        if (String.IsNullOrEmpty(strAddExp8) == false) Model_tab_ShopClient_Agent.AddExp8 = strAddExp8;

                        BLL_tab_ShopClient_Agent_.Add(Model_tab_ShopClient_Agent);
                        intErrorCode = 3;

                        #region weixin通知店主上级 下级要开店
                        try
                        {
                            string strintAgentAdLevelname = "代理";
                            if (intAgentAdLevel > 0)
                            {
                                EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                                EggsoftWX.Model.tab_ShopClient_Agent_Level Modeltab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel(intAgentAdLevel);
                                if (Modeltab_ShopClient_Agent_Level != null)
                                {
                                    strintAgentAdLevelname = Modeltab_ShopClient_Agent_Level.AgentLevelName;
                                }
                            }

                            int pub_Int_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);

                            EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(strUserID);
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserID);
                            string strTitle = "店主亲,";
                            strTitle += "昵称" + strUserNickName + " 店铺名" + strShopName + " 联系人" + strContactName + " 联系方式" + strContactMobile + "申请" + strintAgentAdLevelname;
                            if (Int32.Parse(strParentID) > 0) strTitle += " ,上级是" + Eggsoft_Public_CL.Pub.GetNickName(strParentID);
                            if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                            {
                                string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                                for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), 0, strTitle + ",Email已同步发送");
                                }
                            }
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strParentID), 0, strTitle + "请联系厂家协调你的下线开店事宜");




                        }
                        catch (Exception Exceptione)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                        }
                        finally
                        {

                        }

                        #endregion
                    }
                    else
                    { //已有数据 已经开过店   或者是天使转过来的 或者删除过的
                        Model_tab_ShopClient_Agent = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID);
                        if (Model_tab_ShopClient_Agent.Empowered == false) Model_tab_ShopClient_Agent.AgentLevelSelect = intAgentAdLevel;///没有授权就能改，授权 了  就不能改
                        Model_tab_ShopClient_Agent.ShopName = strShopName;
                        Model_tab_ShopClient_Agent.UpdateTime = DateTime.Now;
                        if (Model_tab_ShopClient_Agent.IsDeleted != 0) {
                            Model_tab_ShopClient_Agent.IsDeleted = 0;
                            Model_tab_ShopClient_Agent.UpdateBy = "恢复删除";
                        }
                        if (String.IsNullOrEmpty(strAddExp0) == false) Model_tab_ShopClient_Agent.AddExp0 = strAddExp0;
                        if (String.IsNullOrEmpty(strAddExp1) == false) Model_tab_ShopClient_Agent.AddExp1 = strAddExp1;
                        if (String.IsNullOrEmpty(strAddExp2) == false) Model_tab_ShopClient_Agent.AddExp2 = strAddExp2;
                        if (String.IsNullOrEmpty(strAddExp3) == false) Model_tab_ShopClient_Agent.AddExp3 = strAddExp3;
                        if (String.IsNullOrEmpty(strAddExp4) == false) Model_tab_ShopClient_Agent.AddExp4 = strAddExp4;
                        if (String.IsNullOrEmpty(strAddExp5) == false) Model_tab_ShopClient_Agent.AddExp5 = strAddExp5;
                        if (String.IsNullOrEmpty(strAddExp6) == false) Model_tab_ShopClient_Agent.AddExp6 = strAddExp6;
                        if (String.IsNullOrEmpty(strAddExp7) == false) Model_tab_ShopClient_Agent.AddExp7 = strAddExp7;
                        if (String.IsNullOrEmpty(strAddExp8) == false) Model_tab_ShopClient_Agent.AddExp8 = strAddExp8;

                         if (boolEveryOneAutoAgentOnlyIsAngel) Model_tab_ShopClient_Agent.OnlyIsAngel = false;
                        BLL_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent);
                        intErrorCode = 4;
                    }
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                    Model_tab_User.UserRealName = strContactName;
                    Model_tab_User.ContactPhone = strContactMobile;
                    Model_tab_User.AlipayNumOrWeiXinPay = strAlipayOrWeiXinPay;
                    BLL_tab_User.Update(Model_tab_User);
                    //保存选择的商品  先全部删除 然后加进去所选择的
                    EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                    string[] mystrChoiceGoodList = strChoiceGoodList.Split(',');
                    if (mystrChoiceGoodList.Length == 0)
                    {
                        intErrorCode = -1;
                        Eggsoft.Common.debug_Log.Call_WriteLog("保存错误,列表为空 context=" + context);

                    }
                    else
                    {

                        #region save
                        #region //先删除掉 不再代理的的
                        System.Data.DataSet myds = BLL_tab_ShopClient_Agent__ProductClassID.GetList("ProductID,id", " UserID=" + strUserID + "  order by id asc");
                        for (int i = 0; i < (myds.Tables[0].Rows.Count); i++)
                        {
                            string strProductID = myds.Tables[0].Rows[i]["ProductID"].ToString();
                            string strID = myds.Tables[0].Rows[i]["ID"].ToString();
                            bool boolEsit = false;

                            boolEsit = mystrChoiceGoodList.Contains(strProductID);//C#判断某元素是否存在数组中
                            if (boolEsit == false)
                            {
                                BLL_tab_ShopClient_Agent__ProductClassID.Delete(Int32.Parse(strID));//人家不在代理聊  删除掉
                            }                          
                        }
                        #endregion
                        ArrayList ArrayListSQL = new ArrayList();

                        for (int i = 0; i < mystrChoiceGoodList.Length; i++)
                        {
                            string strChoiceProductID = mystrChoiceGoodList[i];
                            bool boolExsit_Agent = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + strUserID + " and ProductID=" + strChoiceProductID);
                            if (boolExsit_Agent)
                            {
                                //nothing
                            }
                            else
                            {
                                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();
                                Model_tab_ShopClient_Agent__ProductClassID.UserID = Int32.Parse(strUserID);
                                Model_tab_ShopClient_Agent__ProductClassID.ProductID = Int32.Parse(mystrChoiceGoodList[i]);
                                Model_tab_ShopClient_Agent__ProductClassID.UpdateTime = DateTime.Now;
                                Model_tab_ShopClient_Agent__ProductClassID.Empowered = true;//详细商品自动审批把
                                Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel = false;//既然详细商品自动审批把 那天使也自动去掉吧
                                //Model_tab_ShopClient_Agent__ProductClassID.Price_Percent = 0;


                                StringBuilder strSql = new StringBuilder();
                                strSql.Append("INSERT INTO  [tab_ShopClient_Agent__ProductClassID] (ShopClientID,OnlyIsAngel,UserID,ProductID,UpdateTime,Empowered,StockNum_MeHavebuyNum,ProductRightNum,ProductPrice)  values ");
                                strSql.Append("(" + strShopClientID + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel.toInt32() + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.UserID + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductID + ",");
                                strSql.Append("'" + Convert.ToDateTime(Model_tab_ShopClient_Agent__ProductClassID.UpdateTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                                strSql.Append((Model_tab_ShopClient_Agent__ProductClassID.Empowered.toInt32()) + ",");
                                 strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum.toInt32() + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductRightNum.toInt32() + ",");
                                strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductPrice.toDecimal() + ")");
                                ArrayListSQL.Add(strSql.ToString());
                                                              
                                intErrorCode = 5;
                            }
                        }
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);

                        if ((intErrorCode == 3) || (intErrorCode == 5))//发送email 通知
                        {
                            int intShopClientID = Convert.ToInt32(Model_tab_User.ShopClientID);
                            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intShopClientID);
                            if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
                            {

                                Eggsoft_Public_CL.XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                                if (myXML__Class_Shop_Client.CheckEmail == true)
                                {
                                    string strTo = myXML__Class_Shop_Client.Email;
                                    string strSubject = my_Model_tab_ShopClient.ShopClientName + "，代理店铺名：" + strShopName + ",用户昵称：" + Model_tab_User.NickName + "，微店号：" + Model_tab_User.ShopUserID + " 用户代理操作通知！";
                                    string strBody = "你好，我们给你发信，是因为" + "微店" + "用户操作代理通知引起！" + "\n";

                                    string strClientAdminURL = System.Configuration.ConfigurationManager.AppSettings["ClientAdminURL"];

                                    strBody += "请点击如下的连接进行授权处理。如果不能点击，请复制如下连接到浏览器地址栏！" + "\n";
                                    strBody += strClientAdminURL + "/ClientAdmin/07AgentChecked/Board_AgentChecked.aspx";
                                    strBody += "\n" + strSubject;
                                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName, strTo, strSubject, strBody);

                                }
                                else
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog("商家Email尚未验证=" + myXML__Class_Shop_Client.Email);

                                }
                            }

                            #region 是否自动给予分销权
                            if (intErrorCode == 5)
                            {

                                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                                EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                                if (tab_ShopClient_ShopPar_Model != null)
                                {
                                    if (tab_ShopClient_ShopPar_Model.AskAgentAuto.toBoolean())
                                    {
                                        Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Int32.Parse(strUserID));
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion save
                    }
                }
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                intErrorCode = -1;
            }
            finally
            {

            }
            Eggsoft.Common.debug_Log.Call_WriteLog(strUserID, "代理申请处理");

            string str = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return str;

            //return intErrorCode.ToString();
        }

    }
}
