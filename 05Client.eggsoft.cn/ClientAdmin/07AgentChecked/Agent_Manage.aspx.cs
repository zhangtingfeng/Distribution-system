using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Agent_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    string type = Request.QueryString["type"];

                    // Link0.Text = "/default.html";
                    if (type.ToLower() == "delete")
                    {
                        string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                        string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                        string strUserID = Request.QueryString["UserID"];
                        Eggsoft.Common.debug_Log.Call_WriteLog("删除代理分销商strUserID=" + strUserID, "Temp_tab_ShopClient_Agent_");
                        if (!CommUtil.IsNumStr(strUserID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID blltab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                        blltab_ShopClient_Agent__ProductClassID.Delete("UserID=" + strUserID + " and ShopClientID=" + str_Pub_ShopClientID);


                        bool boolEveryOneAutoAgentOnlyIsAngel = Eggsoft_Public_CL.Pub.boolShowPower(str_Pub_ShopClientID, "EveryOneAutoAgentOnlyIsAngel");
                        EggsoftWX.BLL.tab_ShopClient_Agent_ tab_ShopClient_Agent_bll1 = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        //tab_ShopClient_Agent_bll1.Delete("UserID=" + strUserID + " and ShopClientID=" + str_Pub_ShopClientID);
                        tab_ShopClient_Agent_bll1.Update("IsDeleted=1,UpdateTime=getdate(),UpdateBy=@UpdateBy", "UserID=" + strUserID + " and ShopClientID=" + str_Pub_ShopClientID, strwebuy8_ClientAdmin_Users_ClientUserAccount);
                        #region 如果是高级代理 就要去除所有权限
                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        EggsoftWX.Model.tab_ShopClient_Agent_ Modelold = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and ShopClientID=" + str_Pub_ShopClientID);
                        if (Modelold.AgentLevelSelect > 0)
                        {
                            Modelold.AgentLevelSelect = 0;
                            bll_tab_ShopClient_Agent_.Update(Modelold);
                            string strTeamID = Eggsoft_Public_CL.Pub.stringShowPower(str_Pub_ShopClientID, "ConsumptionCapital_OperationCenterID");///
                            EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=@ShopClientID and ShopTeamID=@ShopTeamID and isnull(IsDeleted,0)=0", str_Pub_ShopClientID, strTeamID.toInt32());
                            if (my_Model_tab_ShopClient_Agent_ != null)
                            {
                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                my_BLL_tab_User.Update("TeamID=@TeamID,updatetime=getdate(),updateBy=@updateBy", "TeamID=@OldTeamID", my_Model_tab_ShopClient_Agent_.ID, strwebuy8_ClientAdmin_Users_ClientUserAccount + "代理商删除操作", Modelold.ID);
                            }
                        }
                        #endregion 如果是高级代理 就要去除所有权限

                        if (boolEveryOneAutoAgentOnlyIsAngel)
                        {
                        }
                        else
                        {
                            int intDeleteParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(strUserID.toInt32());
                            EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                            blltab_User.Update("ParentID=" + intDeleteParentID, "ParentID=" + strUserID);///不再设置为代理
                        }

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        JsUtil.ShowMsg("删除成功!", strCallBackUrl);
                    }
                    else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass();
                    }


                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }
        private void read_ShopClient_Agent__ProductClassID()
        {
            try
            {
                string strUserID = Request.QueryString["UserID"];
                #region 顶级代理模式
                //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID));
                //bool boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                #endregion

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();

                ///复杂性 比较高  啊
                /*
                 每款商品 检查是否有父级代理。有的话 拆分资金奖励 。按照 20%拆分
         
                 */
                //
                //检查 父级代理
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ meModel = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                int intParentID = meModel.ParentID.toInt32();
                int intGrandParentID = 0;
                int[] mf1_ParentID_ProductID_List = null;
                int[] mf1_GrandParentID_ProductID_List = null;//爷爷级别的代理
                string strParentIDShopName = "";
                string strGrandarentIDShopName = "";
                if (intParentID > 0)
                {//整理父亲代理的列表
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_intParentID = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intParentID + " and ShopClientID=" + strShopClient_ID);
                    if (Model_intParentID != null)
                    {
                        strParentIDShopName = Model_intParentID.ShopName;

                        System.Data.DataTable myDataTable2 = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + intParentID + " and ShopClientID=" + strShopClient_ID + " and Empowered=1 order by id asc").Tables[0];
                        mf1_ParentID_ProductID_List = new int[myDataTable2.Rows.Count]; //注意初始化数组的范围,或者指定初值;

                        for (int i = 0; i < myDataTable2.Rows.Count; i++)
                        {
                            string strProductID = myDataTable2.Rows[i]["ProductID"].ToString();
                            mf1_ParentID_ProductID_List[i] = Int32.Parse(strProductID);
                        }

                        intGrandParentID = Model_intParentID.ParentID.toInt32();
                        if (intGrandParentID > 0)
                        {
                            EggsoftWX.Model.tab_ShopClient_Agent_ Model_intGrandParentID = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intGrandParentID + " and ShopClientID=" + strShopClient_ID);
                            if (Model_intGrandParentID != null)
                            {
                                strGrandarentIDShopName = Model_intGrandParentID.ShopName;
                                myDataTable2 = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + intGrandParentID + " and ShopClientID=" + strShopClient_ID + " and Empowered=1 order by id asc").Tables[0];
                                mf1_GrandParentID_ProductID_List = new int[myDataTable2.Rows.Count]; //注意初始化数组的范围,或者指定初值;

                                for (int i = 0; i < myDataTable2.Rows.Count; i++)
                                {
                                    string strProductID = myDataTable2.Rows[i]["ProductID"].ToString();
                                    mf1_GrandParentID_ProductID_List[i] = Int32.Parse(strProductID);
                                }
                            }
                        }
                    }
                    else
                    {
                        meModel.ParentID = 0;

                        bll_tab_ShopClient_Agent_.Update(meModel);
                    }
                }

                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                int intPub_ShopClientID = Int32.Parse(str_Pub_ShopClientID);


                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();

                System.Data.DataTable myDataTable = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID + " order by id asc").Tables[0];

                string multi_Price_Line = "";

                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string ProductID = myDataTable.Rows[i]["ProductID"].ToString();
                    string Empowered = myDataTable.Rows[i]["Empowered"].ToString();
                    //string strPrice_Percent = myDataTable.Rows[i]["Price_Percent"].ToString();
                    //string strPrice_Percent1 = myDataTable.Rows[i]["Price_Percent1"].ToString();
                    //string strPrice_Percent2 = myDataTable.Rows[i]["Price_Percent2"].ToString();
                    string strEmpowered = myDataTable.Rows[i]["Empowered"].ToString();

                    bool boolEmpowered = false;
                    bool.TryParse(strEmpowered, out boolEmpowered);

                    string strPowerint1Or0 = boolEmpowered ? "checked" : "";
                    Model_tab_Goods = BLL_tab_Goods.GetModel(Int32.Parse(ProductID));
                    if ((Model_tab_Goods.IsDeleted == true)) continue;///是否删除
                    //Decimal Decimal_Price_Percent = 0;
                    //Decimal.TryParse(strPrice_Percent, out Decimal_Price_Percent);

                    //Decimal Decimal_Price_Percent1 = 0;
                    //Decimal.TryParse(strPrice_Percent1, out Decimal_Price_Percent1);

                    //Decimal Decimal_Price_Percent2 = 0;
                    //Decimal.TryParse(strPrice_Percent2, out Decimal_Price_Percent2);

                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intPub_ShopClientID, strUserID.toInt32(), ProductID.toInt32());
                    int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();

                    int idExsit_ProductID_Parent = 0;
                    if (intLength > 1)///2级分销  才看父亲的情况
                    {
                        if (mf1_GrandParentID_ProductID_List != null)
                        {
                            idExsit_ProductID_Parent = Array.IndexOf(mf1_GrandParentID_ProductID_List, Int32.Parse(ProductID)); ///父父数组代理的是否有此商品
                        }
                    }
                    int idExsit_ProductID_GrandParent = 0;
                    if (intLength > 2)///3级分销  才看爷爷的情况
                    {
                        if (mf1_GrandParentID_ProductID_List != null)
                        {
                            idExsit_ProductID_GrandParent = Array.IndexOf(mf1_GrandParentID_ProductID_List, Int32.Parse(ProductID)); ///父父数组代理的是否有此商品
                        }
                    }
                   
                }
                Literal_Agent_Percent_Line.Text = multi_Price_Line;
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }

        private void SetClass()
        {
            try
            {
                read_ShopClient_Agent__ProductClassID();

                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string strUserID = Request.QueryString["UserID"];
                    EggsoftWX.BLL.View_ShopClient_Agent bll = new EggsoftWX.BLL.View_ShopClient_Agent();
                    EggsoftWX.Model.View_ShopClient_Agent Model = bll.GetModel("UserID=" + strUserID);

                    CheckBox_Agent.Checked = Model.Empowered.toBoolean();
                    Label_ContactMan.Text = Model.UserRealName;
                    Label_ShopClientName.Text = Model.ShopName;
                    btnAdd.Text = "保 存";
                }
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            try
            {
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                string strUserID = Request.QueryString["UserID"];
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Modelold = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                Modelold.Empowered = CheckBox_Agent.Checked;
                Modelold.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount + "保存普通代理商";
                if (Modelold.AgentLevelSelect > 0)
                {
                    Modelold.AgentLevelSelect = 0;
                    string strTeamID = Eggsoft_Public_CL.Pub.stringShowPower(strShopClient_ID, "ConsumptionCapital_OperationCenterID");///
                    EggsoftWX.BLL.tab_ShopClient_Agent_ my_BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ my_Model_tab_ShopClient_Agent_ = my_BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=@ShopClientID and ShopTeamID=@ShopTeamID and isnull(IsDeleted,0)=0", strShopClient_ID, strTeamID.toInt32());
                    if (my_Model_tab_ShopClient_Agent_ != null)
                    {
                        EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        my_BLL_tab_User.Update("TeamID=@TeamID,updatetime=getdate(),updateBy=@updateBy", "TeamID=@OldTeamID", my_Model_tab_ShopClient_Agent_.ID, strwebuy8_ClientAdmin_Users_ClientUserAccount + "代理商改为分销商", Modelold.ID);
                    }
                }

                bll_tab_ShopClient_Agent_.Update(Modelold);

                //bll_tab_ShopClient_Agent_.Update(Model);

                saveMultiProductAgent();
                //return;

                string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_UserAgentCertification.asmx";
                string[] args = new string[1];
                args[0] = strUserID;// "/UpLoad/images/";
                object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_UserAgentCertification", args);
                string strresult = result.ToString();

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = "已授权，并为您制作代理资格证。";
                string strDescription = "请点击完成代理环境配置。一键微店，万家同源，不用发货，公司帮你一切搞定。";

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID));
                string strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/showagentbookmark.aspx?type=ResetAgentHuanJing";



                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strresult, strDescription, strClickURL);///Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strresult
                WeiXinTuWens_ArrayList.Add(First);
                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strUserID), 0, WeiXinTuWens_ArrayList);


                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                //if (String.IsNullOrEmpty(strCallBackUrl) == false)
                //{
                strCallBackUrl = strCallBackUrl.Replace("*", "?");
                //}

                JsUtil.ShowMsg("授权成功!", strCallBackUrl);

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }

        }

        private void saveMultiProductAgent()
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            try
            {
                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();


                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                string strUserID = Request.QueryString["UserID"];
                System.Data.DataTable myDataTable = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID + " order by id asc").Tables[0];




                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string ProductID = myDataTable.Rows[i]["ProductID"].ToString();

                    string strcheckbox_Empowered_ = Request.Form["checkbox_Empowered_Name" + ProductID];
                    //string strText_Price_Percent = Request.Form["Text_Price_Percent" + ProductID];
                    //string strText_Price_Percent1 = Request.Form["Text_Price_Percent_Parent" + ProductID];
                    //string strText_Price_Percent2 = Request.Form["Text_Price_Percent_GrandParent" + ProductID];

                    //if (String.IsNullOrEmpty(strcheckbox_Empowered_) == false)
                    //{
                    //    if (strcheckbox_Empowered_.IndexOf("on") > -1) strcheckbox_Empowered_ = "true";
                    //}
                    //bool bool_Empowered = false;
                    //bool.TryParse(strcheckbox_Empowered_, out bool_Empowered);

                    //Decimal Decimal_Price_Percent = 0;
                    //Decimal.TryParse(strText_Price_Percent, out Decimal_Price_Percent);

                    //Decimal Decimal_Price_Percent1 = 0;
                    //Decimal.TryParse(strText_Price_Percent1, out Decimal_Price_Percent1);

                    //Decimal Decimal_Price_Percent2 = 0;
                    //Decimal.TryParse(strText_Price_Percent2, out Decimal_Price_Percent2);

                    Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("ProductID=" + ProductID + " and UserID=" + strUserID);
                    Model_tab_ShopClient_Agent__ProductClassID.Empowered = CheckBox_Agent.Checked;
                    //Model_tab_ShopClient_Agent__ProductClassID.Price_Percent = Decimal_Price_Percent;
                    //Model_tab_ShopClient_Agent__ProductClassID.Price_Percent1 = Decimal_Price_Percent1;
                    //Model_tab_ShopClient_Agent__ProductClassID.Price_Percent2 = Decimal_Price_Percent2;
                    BLL_tab_ShopClient_Agent__ProductClassID.Update(Model_tab_ShopClient_Agent__ProductClassID);


                }

            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }

    }
}