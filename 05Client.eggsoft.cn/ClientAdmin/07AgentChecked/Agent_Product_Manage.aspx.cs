using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Agent_Product_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected string pub_RightSLevelGoods = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                    string type = Request.QueryString["type"];

                    // Link0.Text = "/default.html";
                    if (type.ToLower() == "delete")
                    {
                        string strUserID = Request.QueryString["UserID"];
                        if (!CommUtil.IsNumStr(strUserID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID bll = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                        bll.Delete("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);

                        EggsoftWX.BLL.tab_ShopClient_Agent_ tab_ShopClient_Agent_bll1 = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        tab_ShopClient_Agent_bll1.Update("IsDeleted=1,UpdateTime=getdate(),UpdateBy=@UpdateBy", "UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID, strwebuy8_ClientAdmin_Users_ClientUserAccount);

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        JsUtil.ShowMsg("删除成功!", strCallBackUrl);
                    }
                    else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        inti_pub_RightSLevelGoods();
                        read_ShopClient_TeamParentID();
                        SetClass();
                    }


                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }
        }

        private string inti_pub_RightSLevelGoods()
        {
            string strinti_pub_RightSLevelGoods = "";
            try
            {
                string strAgentLevelSelect = Request.QueryString["AgentLevelSelect"];
                EggsoftWX.BLL.tab_ShopClient_Agent_Level bll_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = bll_tab_ShopClient_Agent_Level.GetModel(Int32.Parse(strAgentLevelSelect));
                pub_RightSLevelGoods = Model_tab_ShopClient_Agent_Level.AgentLevelName + " " + Model_tab_ShopClient_Agent_Level.AgentlevelMemo;


                Label_GouWuQuanGoodPrice.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_Agent_Level.GouWuQuanGoodPrice.toDecimal()) + "¥";
                //TextBox_Vouchers_Consume_Or_Recharge.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_Agent_Level.GouWuQuanGoodPrice);
                strinti_pub_RightSLevelGoods = Model_tab_ShopClient_Agent_Level.AgentLevelName;

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }

            return strinti_pub_RightSLevelGoods;

        }


        private void read_ShopClient_TeamParentID()
        {

            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                int intUserID = Request.QueryString["UserID"].toInt32();
                string strthisPID = "0";



                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intUserID);
                if (Model_tab_ShopClient_Agent_ != null) strthisPID = Model_tab_ShopClient_Agent_.TeamParentID.ToString();
                string strSQL = @"SELECT   tab_User.ContactMan, tab_User.ContactPhone, tab_User.NickName, tab_User.UserRealName, 
                tab_User.ShopUserID, tab_ShopClient_Agent_.*
FROM      tab_ShopClient_Agent_ LEFT OUTER JOIN
                tab_User ON tab_ShopClient_Agent_.ShopClientID = tab_User.ShopClientID AND 
                tab_ShopClient_Agent_.UserID = tab_User.ID where tab_ShopClient_Agent_.ShopClientID={0} and tab_ShopClient_Agent_.AgentLevelSelect>0 and tab_ShopClient_Agent_.ID<>{1} and tab_ShopClient_Agent_.IsDeleted<>1 order by tab_ShopClient_Agent_.id asc";
                strSQL = string.Format(strSQL, strShopClientID, Model_tab_ShopClient_Agent_.ID);
                System.Data.DataTable myDataTable2 = bll_tab_ShopClient_Agent_.SelectList(strSQL).Tables[0];

                ListItem myThisListItem = new ListItem("直接访问，无上级", "0");
                DropDownListChoiceTeamParentIDList.Items.Add(myThisListItem);

                for (int i = 0; i < myDataTable2.Rows.Count; i++)
                {
                    string strShopTeamID = myDataTable2.Rows[i]["ShopTeamID"].ToString();
                    string strID = myDataTable2.Rows[i]["ID"].ToString();
                    string strParentID = myDataTable2.Rows[i]["userID"].ToString();
                    string strShopUserID = myDataTable2.Rows[i]["ShopUserID"].ToString();
                    string strUserRealName = myDataTable2.Rows[i]["UserRealName"].ToString();
                    string strContactPhone = myDataTable2.Rows[i]["ContactPhone"].ToString();
                    string strNickName = myDataTable2.Rows[i]["NickName"].ToString();


                    myThisListItem = new ListItem("团队ID：" + strShopTeamID + " " + "用户ID：" + strShopUserID + " " + strNickName + " " + strUserRealName + " " + strContactPhone, strID);
                    DropDownListChoiceTeamParentIDList.Items.Add(myThisListItem);
                }
                DropDownListChoiceTeamParentIDList.SelectedValue = strthisPID;
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "团队设置", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "团队设置");
            }

        }

        private void read_ShopClient_Agent_Stock_ProductClassID()
        {
            try
            {
                string strUserID = Request.QueryString["UserID"];
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();

                ///复杂性 比较高  啊
                /*
                 每款商品 检查是否有父级代理。有的话 拆分资金奖励 。按照 20%拆分
         
                 */
                //
                //检查 父级代理


                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                int intParentID = Model.ParentID.toInt32();
                //int[] mf1_ParentID_ProductID_List = null;
                //string strParentIDShopName = "";
                string strAgentLevelSelect = Request.QueryString["AgentLevelSelect"];

                //int intParentID = Model_tab_ShopClient_Agent.ParentID;
                if (intParentID > 0)
                {
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_PID = bll_tab_ShopClient_Agent_.GetModel("UserID=" + intParentID + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_ShopClient_Agent_PID != null)
                    {
                        if (Model_tab_ShopClient_Agent_PID.AgentLevelSelect > 0)
                        {
                            EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                            EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel("ID=" + Model_tab_ShopClient_Agent_PID.AgentLevelSelect + " and ShopClientID=" + strShopClient_ID);
                            if (Model_tab_ShopClient_Agent_PID != null)
                            {
                                //Literal_ParentID_Show.Text = "<br />上级代理" + Eggsoft_Public_CL.Pub.GetNickName(intParentID.ToString()) + " <span style=\"color:red;\">" + Model_tab_ShopClient_Agent_Level.AgentLevelName + "</span>";
                                //Literal_ParentID_Show.Text += ",如批准下级代理的权限，其购物券将按照上级代理商的价格<span style=\"color:red;\">" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_Agent_Level.GouWuQuanGoodPrice) + "元</span>等值进行折算,从上级代理那里取走购物券，等值折算给本代理商";
                                // Literal_ParentID_Show.Text
                            }

                        }
                    }
                    //      EggsoftWX.BLL.tab_ShopClient_Agent_Level bll_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                }

                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();
                EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();

                System.Data.DataTable myDataTable = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID + " order by id asc").Tables[0];

                string multi_Price_Line = "";

                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string ProductID = myDataTable.Rows[i]["ProductID"].ToString();
                    string strProductPrice = myDataTable.Rows[i]["ProductPrice"].ToString();
                    string strEmpowered = myDataTable.Rows[i]["Empowered"].ToString();
                    string strFull_Vouchers_ = "";

                    bool boolEmpowered = false;
                    bool.TryParse(strEmpowered, out boolEmpowered);
                    string strPowerint1Or0 = boolEmpowered ? "checked" : "";

                    //bool boolFull_Vouchers_ = false;
                    //bool.TryParse(strFull_Vouchers_, out boolFull_Vouchers_);
                    //string strPowerint1Or0_Full_Vouchers_ = boolFull_Vouchers_ ? "checked" : "";

                    Model_tab_Goods = BLL_tab_Goods.GetModel(Int32.Parse(ProductID));
                    if ((Model_tab_Goods.IsDeleted == true)) continue;///是否删除
                    if ((Model_tab_Goods.isSaled == false)) continue;///是否删除

                    //Decimal Decimal_ProductPrice = 0;
                    //Decimal.TryParse(strProductPrice, out Decimal_ProductPrice);

                    EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("ShopClient_Agent_Level_ID=" + strAgentLevelSelect + " and ProductID=" + ProductID + " and ShopClientID=" + strShopClient_ID);
                    if (Model_tab_ShopClient_Agent_Level_ProductInfo != null)
                    {
                        strProductPrice = Model_tab_ShopClient_Agent_Level_ProductInfo.ProductPrice.toDecimal().toString();
                        strFull_Vouchers_ = Model_tab_ShopClient_Agent_Level_ProductInfo.MaxGouWuQuan.toDecimal().toString();
                    }




                    //string Text_Decimal_ProductPrice = strProductPrice.toDecimal().ToString("###0.00");
                    //string Text_Price_RightNum = Decimal_Price_Percent1.ToString("###0");
                    multi_Price_Line += "<tr><td  width=\"200px;\">商品名称：" + Model_tab_Goods.Name + "</td>";
                    multi_Price_Line += "<td>打折价格：" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_Goods.PromotePrice)) + "元</td>";
                    multi_Price_Line += "<td>是否授权代理<input type=\"checkbox\" name=\"checkbox_Empowered_Name" + ProductID + "\" id=\"checkbox_Empowered_Name" + ProductID + "\" " + strPowerint1Or0 + "></td>";
                    multi_Price_Line += "<td>授权代理价格" + strProductPrice.toDecimal().ToString("###0.00") + "￥</td>";
                    multi_Price_Line += "<td>最大购物券允许金额" + strFull_Vouchers_.toDecimal().ToString("###0.00") + "￥</td>";
                    //multi_Price_Line += "<td>最大购物券允许金额<input type=\"text\" name=\"text_Empowered_Full_Vouchers_" + ProductID + "\" id=\"text_Empowered_Full_Vouchers_" + ProductID + "\" " + strFull_Vouchers_.toDecimal() + "></td>";

                    multi_Price_Line += "</td>";



                    multi_Price_Line += "</tr>\n";
                }
                Literal_Agent_Percent_Line.Text = multi_Price_Line;

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }
        }

        private void SetClass()
        {
            try
            {
                read_ShopClient_Agent_Stock_ProductClassID();

                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string strUserID = Request.QueryString["UserID"];
                    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                    string strSQL = @"SELECT DISTINCT 
                tab_User.ShopClientID, tab_User.ContactPhone, tab_User.NickName, 
                tab_ShopClient_Agent_.ShopName, tab_ShopClient_Agent_.Empowered, tab_User.UserRealName, 
                tab_User.AlipayNumOrWeiXinPay, tab_ShopClient_Agent_.UpdateTime, tab_ShopClient_Agent_.ID, 
                tab_ShopClient_Agent_.UserID, tab_ShopClient_Agent_.AgentLevelSelect, tab_ShopClient_Agent_.ParentID,tab_ShopClient_Agent_.ShopTeamID, 
                tab_User.ShopUserID, tab_ShopClient_Agent_.OnlyIsAngel
FROM      tab_ShopClient_Agent_ LEFT OUTER JOIN
                tab_User ON tab_ShopClient_Agent_.ShopClientID = tab_User.ShopClientID AND 
                tab_ShopClient_Agent_.UserID = tab_User.ID where tab_ShopClient_Agent_.UserID =@UserID and tab_ShopClient_Agent_.ShopClientID=@ShopClientID";


                    EggsoftWX.BLL.tab_ShopClient blltab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    System.Data.DataTable Data_DataTable = blltab_ShopClient.SelectList(strSQL, strUserID.toInt32(), strShopClient_ID.toInt32()).Tables[0];
                    if (Data_DataTable.Rows.Count > 0) {
                        Label_ContactMan.Text = Data_DataTable.Rows[0]["UserRealName"].toString();
                        Label_ShopClientName.Text = Data_DataTable.Rows[0]["ShopName"].toString();
                        LabelShopTeamID.Text = Data_DataTable.Rows[0]["ShopTeamID"].toString();

                        //Label_ShopClientName.Text = Model.ShopName;
                        //LabelShopTeamID.Text = Model.s

                    }
                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(strUserID), out myCountMoney_Vouchers);
                    //strargBody = strargBody.Replace("###GouWuHongBao###", Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers));
                    Label_GouWuQuanYuE.Text = "<a target=\"_blank\" href=\"../09System_Status/UserStatus_Quan.aspx?userId=" + strUserID + "\">" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "¥</a>";
                    TextBox_Vouchers_Consume_Or_Recharge.ToolTip = "现有购物券为" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers) + "¥";
                    //TextBox_Vouchers_Consume_Or_Recharge.p

                    btnAdd.Text = "保 存";


                    //RequiredFieldValidator3.Enabled = false;
                }


            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }



        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                bool boolSusess = false;
                try
                {
                    string strChoiceTeamParentID = DropDownListChoiceTeamParentIDList.SelectedValue;

                    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    string strUserID = Request.QueryString["UserID"];
                    string strAgentLevelSelect = Request.QueryString["AgentLevelSelect"];
                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                    Model_tab_ShopClient_Agent_.Empowered = CheckBox_Agent.Checked;
                    ///Model.ParentID = 0;//这种代理 没有上级 就别发消息了  从源头上拒绝了  产品页自己设置 其他页还是要的 只是购买的时候产品页设为没有就可以了。
                    int intAgentLevelSelect = 0;
                    int.TryParse(strAgentLevelSelect, out intAgentLevelSelect);
                    Model_tab_ShopClient_Agent_.AgentLevelSelect = intAgentLevelSelect;
                    Model_tab_ShopClient_Agent_.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_tab_ShopClient_Agent_.UpdateTime = DateTime.Now;
                    Model_tab_ShopClient_Agent_.TeamParentID = strChoiceTeamParentID.toInt32();
                    bll_tab_ShopClient_Agent_.Update(Model_tab_ShopClient_Agent_);


                    #region 把本人的团队改成本人的
                    EggsoftWX.BLL.tab_User bll_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = bll_tab_User.GetModel(strUserID.toInt32());
                    Model_tab_User.TeamID = Model_tab_ShopClient_Agent_.ID;
                    Model_tab_User.Updatetime = DateTime.Now;
                    Model_tab_User.UpdateBy = "后台授权高级代理";
                    bll_tab_User.Update(Model_tab_User);
                    #endregion

                    //#region 初始化所有运营中心数据  粉丝数据
                    //System.Threading.Tasks.Task.Factory.StartNew(() =>
                    //{
                    //    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClient_ID.toInt32()))
                    //    {
                    //        Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(strUserID.toInt32(), strShopClient_ID.toInt32());
                    //    }
                    //});
                    //#endregion 初始化所有运营中心数据

                    #region 增加或者减少购物券
                    string strGetstringShowPower_ShopName = Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(strShopClient_ID);

                    Decimal Decimal_Total_Vouchers_ = 0;
                    Decimal.TryParse(TextBox_Vouchers_Consume_Or_Recharge.Text, out Decimal_Total_Vouchers_);
                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                    if (Decimal.Round(Decimal_Total_Vouchers_, 2) > 0)
                    {
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal_Total_Vouchers_;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "批准代理增加";
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strUserID);
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount; ;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Int32.Parse(strUserID);
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUserID), 0, "商家批准代理消息,已增加" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_Total_Vouchers_) + "元" + strGetstringShowPower_ShopName + "," + strGetstringShowPower_ShopName + "只能购物使用");
                    }
                    else if (Decimal_Total_Vouchers_ < 0)//冲红使用
                    {
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = -Decimal_Total_Vouchers_;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "批准代理减少";
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strUserID);
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                        BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUserID), 0, "商家批准代理消息,已减少" + Eggsoft_Public_CL.Pub.getPubMoney(-Decimal_Total_Vouchers_) + "元" + strGetstringShowPower_ShopName + "," + strGetstringShowPower_ShopName + "只能购物使用");
                    }
                    #endregion 增加或者减少购物券


                    saveMultiProductAgent();
                    //return;

                    string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_UserAgentCertification.asmx";
                    string[] args = new string[1];
                    args[0] = strUserID;// "/UpLoad/images/";
                    object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_UserAgentCertification", args);
                    string strresult = result.toString();



                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                    //实例化几个WeiXinTuWen类对象  
                    string strTitle = inti_pub_RightSLevelGoods() + "已授权，并为您制作代理资格证。";
                    string strDescription = "一键微店，公司帮你一切搞定。";

                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strresult, strDescription, Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + strresult);
                    WeiXinTuWens_ArrayList.Add(First);


                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strUserID), 0, WeiXinTuWens_ArrayList);


                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    //if (String.IsNullOrEmpty(strCallBackUrl) == false)
                    //{
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    //}
                    boolSusess = true;
                    JsUtil.ShowMsg("授权成功!", strCallBackUrl);

                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
                }
                catch (Exception ex)
                {
                    boolSusess = false;
                    Eggsoft.Common.debug_Log.Call_WriteLog(ex, "操作每个代理");
                }

                finally
                {

                }

                if (boolSusess == false)
                {
                    string strCallBackUrl88 = Request.QueryString["CallBackUrl"];
                    strCallBackUrl88 = strCallBackUrl88.Replace("*", "?");
                    JsUtil.ShowMsg("授权失败!", strCallBackUrl88);
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }
        }

        private void saveMultiProductAgent()
        {
            try
            {
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();

                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                string strUserID = Request.QueryString["UserID"];
                System.Data.DataTable myDataTable = BLL_tab_ShopClient_Agent__ProductClassID.GetList("UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID + " order by id asc").Tables[0];




                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string ProductID = myDataTable.Rows[i]["ProductID"].ToString();

                    //string strcheckbox_Empowered_ = Request.Form["checkbox_Empowered_Name" + ProductID];
                    //string strcheckbox_Empowered_Full_Vouchers_ = Request.Form["checkbox_Empowered_Full_Vouchers_" + ProductID];

                    //if (String.IsNullOrEmpty(strcheckbox_Empowered_) == false)
                    //{
                    //    if (strcheckbox_Empowered_.IndexOf("on") > -1) strcheckbox_Empowered_ = "true";
                    //}
                    //bool bool_Empowered = false;
                    //bool.TryParse(strcheckbox_Empowered_, out bool_Empowered);

                    //if (String.IsNullOrEmpty(strcheckbox_Empowered_Full_Vouchers_) == false)
                    //{
                    //    if (strcheckbox_Empowered_Full_Vouchers_.IndexOf("on") > -1) strcheckbox_Empowered_Full_Vouchers_ = "true";
                    //}
                    //bool bool_Full_Vouchers_ = false;
                    //bool.TryParse(strcheckbox_Empowered_Full_Vouchers_, out bool_Full_Vouchers_);




                    Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("ProductID=" + ProductID + " and UserID=" + strUserID + " and ShopClientID=" + strShopClient_ID);
                    Model_tab_ShopClient_Agent__ProductClassID.Empowered = CheckBox_Agent.Checked;
                    Model_tab_ShopClient_Agent__ProductClassID.UpdateTime = DateTime.Now;
                    Model_tab_ShopClient_Agent__ProductClassID.Updateby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    BLL_tab_ShopClient_Agent__ProductClassID.Update(Model_tab_ShopClient_Agent__ProductClassID);


                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "操作每个代理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "操作每个代理");
            }

        }
    }
}