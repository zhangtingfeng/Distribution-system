using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eggsoft_Public_CL;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _1313OperationCenter_OrderManage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strPubBoard = "";
        private string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
        //public string strTextBox_PayTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_13OperationCenter_OrderManage")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {
                strPubBoard = Request.QueryString["CallBackUrl"].toString();
                if (String.IsNullOrEmpty(strPubBoard)) strPubBoard = "12OrderDetailEveryDay.aspx";


                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string OrderID = Request.QueryString["OrderID"];
                    if (!CommUtil.IsNumStr(OrderID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                    blltab_Order.Delete(Int32.Parse(OrderID));////触发器会自动删除下属订单祥表


                    JsUtil.ShowMsg("删除成功!", strPubBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    if (Request.QueryString["OrderDatailID"].toString().toInt32() > 0)
                    {
                        string strSQL = @"SELECT  tab_Order.ShopClient_ID,tab_Orderdetails.ID,tab_Orderdetails.GoodType, 
               tab_Orderdetails.GoodTypeId,tab_Orderdetails.GoodTypeIdBuyInfo, 
               tab_Orderdetails.ShopClient_ID AS Expr1,tab_Order.DeliveryText,tab_Order.UserID, 
               tab_Orderdetails.ParentID,tab_Orderdetails.GrandParentID,tab_Orderdetails.GreatParentID, 
               tab_Orderdetails.Over7DaysToBeans,tab_Orderdetails.GoodPrice,tab_Order.PayStatus,tab_Orderdetails.OrderCount,tab_Orderdetails.OrderCount,tab_Order.PayDateTime,tab_Order.PaywayOrderNum,tab_Order.PayWay    
FROM     tab_Order LEFT OUTER JOIN
               tab_Orderdetails ON tab_Order.ID =tab_Orderdetails.OrderID AND 
               tab_Order.ShopClient_ID =tab_Orderdetails.ShopClient_ID
WHERE   (tab_Orderdetails.ID = " + Request.QueryString["OrderDatailID"].toString().toInt32() + ")";

                        EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                        System.Data.DataTable Data_DataTable = blltab_Order.SelectList(strSQL).Tables[0];
                        if (Data_DataTable.Rows.Count > 0)
                        {
                            txtUserID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(Data_DataTable.Rows[0]["UserID"].toString()).toString();

                            #region 用户真实姓名
                            EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                            EggsoftWX.Model.tab_User Modeltab_User = blltab_User.GetModel(Data_DataTable.Rows[0]["UserID"].toInt32());
                            if (Modeltab_User != null)
                            {
                                if (string.IsNullOrEmpty(Modeltab_User.UserRealName) == false)
                                {
                                    TextBoxUserReaname.Text = Modeltab_User.UserRealName;
                                }
                                else if (string.IsNullOrEmpty(Modeltab_User.ContactMan) == false)
                                {
                                    TextBoxUserReaname.Text = Modeltab_User.ContactMan;
                                }
                            }
                            #endregion 用户真实姓名

                            TextBoxb004_OperationGoodsID.Text = Data_DataTable.Rows[0]["GoodTypeIdBuyInfo"].toString();
                            TextBox2OrderCount.Text = Data_DataTable.Rows[0]["OrderCount"].toString();
                            TextBox3ParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(Data_DataTable.Rows[0]["ParentID"].toString()).toString();
                            TextBox4GrandParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(Data_DataTable.Rows[0]["GrandParentID"].toString()).toString();
                            //TextBox4GrandParentID.ReadOnly = true;
                            TextBox3_PaySerialNumber.Text = Data_DataTable.Rows[0]["PaywayOrderNum"].toString();
                            TextBox3_PaySerialNumber_TextChanged(sender, e);
                            if (Data_DataTable.Rows[0]["PayWay"].toString() == "Tenpay")
                            {
                                TextBox3_PaySerialNumber.ReadOnly = true;
                            }
                            String strTextBox_PayTime = Convert.ToDateTime(Data_DataTable.Rows[0]["PayDateTime"].toDateTime()).ToString("yyyy-MM-dd HH:mm:ss");
                            TextBox_PayTime.Text = strTextBox_PayTime;

                            TextBox5b002_OperationCenterID.Text = Data_DataTable.Rows[0]["GoodTypeId"].toString();

                            //Panel1Modify.Visible = false;
                        }
                        btnAdd.Text = " 保 存 ";
                    }
                    else
                    {
                        String strTextBox_PayTime = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss");
                        TextBox_PayTime.Text = strTextBox_PayTime;
                    }
                }
                else if ((type == "CopyFromYunYingZhongXin"))////运营中心录单数据
                {
                    string strb013_WriteOrderByOperationID = Request.QueryString["ID"].toString();
                    string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    EggsoftWX.BLL.b013_WriteOrderByOperation BLL_013_WriteOrderByOperation = new EggsoftWX.BLL.b013_WriteOrderByOperation();
                    EggsoftWX.Model.b013_WriteOrderByOperation Model_013_WriteOrderByOperation = BLL_013_WriteOrderByOperation.GetModel("ID=@ID and shopClient_ID=@hopClient_ID", strb013_WriteOrderByOperationID.toInt32(), str_Pub_ShopClientID.toInt32());

                    txtUserID.Text = Model_013_WriteOrderByOperation.BuyOrderShopUserID.toString();

                    #region 用户真实姓名
                    //EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    //EggsoftWX.Model.tab_User Modeltab_User = blltab_User.GetModel(Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(Model_013_WriteOrderByOperation.BuyOrderShopUserID.toInt32(), str_Pub_ShopClientID.toInt32()));
                    //if(Modeltab_User != null)
                    //{
                    //    if(string.IsNullOrEmpty(Modeltab_User.UserRealName) == false)
                    //    {
                    //        TextBoxUserReaname.Text = Modeltab_User.UserRealName;
                    //    }
                    //    else if(string.IsNullOrEmpty(Modeltab_User.ContactMan) == false)
                    //    {
                    TextBoxUserReaname.Text = Model_013_WriteOrderByOperation.BuyOrderShopUserIDRealName;
                    //    }
                    //}
                    #endregion 用户真实姓名

                    TextBoxb004_OperationGoodsID.Text = Model_013_WriteOrderByOperation.BuyGoodID.toString();
                    TextBox2OrderCount.Text = Model_013_WriteOrderByOperation.BuyOrderCount.toString();
                    TextBox3ParentID.Text = Model_013_WriteOrderByOperation.BuyParentShopUserID.toString();

                    #region 得到上上级
                    Int32 strParentID_userID = Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(Model_013_WriteOrderByOperation.BuyParentShopUserID.toInt32(), str_Pub_ShopClientID.toInt32());
                    Int32 strParentID_userID_ParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(strParentID_userID);
                    TextBox4GrandParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(strParentID_userID_ParentID.toString()).toString();
                    #endregion 得到上上级
                    TextBox4GrandParentID.ReadOnly = true;
                    TextBox3_PaySerialNumber.Text = Model_013_WriteOrderByOperation.PaySerialNumber;
                    TextBox3_PaySerialNumber_TextChanged(sender, e);

                    TextBox5b002_OperationCenterID.Text = Model_013_WriteOrderByOperation.OperationCenterID.toString();

                    String strTextBox_PayTime = Convert.ToDateTime(Model_013_WriteOrderByOperation.OrderPayTime).ToString("yyyy-MM-dd HH:mm:ss");
                    TextBox_PayTime.Text = strTextBox_PayTime;

                    //Panel1Modify.Visible = false;

                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            strPubBoard = Request.QueryString["CallBackUrl"].toString();
            try
            {
                string ID = Request.QueryString["ID"];// 修改ID
                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string type = Request.QueryString["type"];

                EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();


                #region 前端录入合法性检查
                EggsoftWX.Model.tab_User Model_tab_User = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", txtUserID.Text.toInt32(), strINCID);
                if (Model_tab_User == null)
                {
                    JsUtil.ShowMsg("添加失败，当前支付用户微店ID不存在!", -1);
                    return;
                }
                EggsoftWX.Model.tab_User Model_tab_UserParentID = blltab_User.GetModel("ShopUserID=@ParentID and ShopClientID=@ShopClientID", TextBox3ParentID.Text.toInt32(), strINCID);
                if (Model_tab_UserParentID == null && TextBox3ParentID.Text.toInt32() > 0)
                {
                    JsUtil.ShowMsg("添加失败，上级用户微店ID不存在!", -1);
                    return;
                }
                EggsoftWX.Model.tab_User Model_tab_UserGrandParentID = blltab_User.GetModel("ShopUserID=@GrandParentID and ShopClientID=@ShopClientID", TextBox4GrandParentID.Text.toInt32(), strINCID);
                if (Model_tab_UserGrandParentID == null && TextBox4GrandParentID.Text.toInt32() > 0)
                {
                    JsUtil.ShowMsg("添加失败，上上级用户微店ID不存在!", -1);
                    return;
                }
                if (TextBox2OrderCount.Text.toInt32() < 1 || TextBox2OrderCount.Text.toInt32() > 999999)
                {
                    JsUtil.ShowMsg("添加失败，购买数量非法!", -1);
                    return;
                }
                EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bllb004_OperationGoods.GetModel("ID=@OperationGoodsID and ShopClient_ID=@ShopClientID", TextBoxb004_OperationGoodsID.Text.toInt32(), strINCID);
                if (Model_b004_OperationGoods == null)
                {
                    JsUtil.ShowMsg("添加失败，支付运营商品ID不存在!", -1);
                    return;
                }
                EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bllb002_OperationCenter.GetModel("ID=@OperationCenterID and ShopClient_ID=@ShopClientID", TextBox5b002_OperationCenterID.Text.toInt32(), strINCID);
                if (Model_b002_OperationCenter == null)
                {
                    JsUtil.ShowMsg("添加失败，运营中心ID不存在!", -1);
                    return;
                }
                DateTime paydatetime = DateTime.MinValue;
                //if (type.ToLower() == "add")
                //{
                String strTextBox_PayTime = TextBox_PayTime.Text;// Request.Form["TextBox_PayTime"];
                paydatetime = Convert.ToDateTime(strTextBox_PayTime.toDateTime());
                if (paydatetime <= DateTime.MinValue || paydatetime >= DateTime.MaxValue)
                {
                    JsUtil.ShowMsg("添加失败，支付时间处理失败，可请求技术支持处理!", -1);
                    return;
                }
                //}
                #endregion 前端录入合法性检查


                #region 用户真实姓名
                if (Model_tab_User != null && String.IsNullOrEmpty(TextBoxUserReaname.Text) == false)
                {
                    Model_tab_User.ContactMan = TextBoxUserReaname.Text.Trim();
                    Model_tab_User.UserRealName = TextBoxUserReaname.Text.Trim();
                    blltab_User.Update(Model_tab_User);
                }
                #endregion 用户真实姓名


                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                if (type.ToLower() == "modify")
                {
                    //#region 是否已经完全出局 完全出局也不能买
                    //Eggsoft_Public_CL.GoodP_YunYingZhongXin myGoodP_YunYingZhongXin = new GoodP_YunYingZhongXin();
                    //Boolean boolHaveRunAway = myGoodP_YunYingZhongXin.checkHaveQuitUser(Model_tab_User.ID, strINCID.toInt32(), TextBoxb004_OperationGoodsID.Text.toInt32());
                    //if(boolHaveRunAway)
                    //{
                    //    JsUtil.ShowMsg("已经完全出局，该ID号无权再下单!", -1);
                    //    return;
                    //}
                    //#endregion 本月已有购买限制

                    EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel("ID=" + Model_b004_OperationGoods.GoodID);
                    EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
                    EggsoftWX.Model.tab_Orderdetails Modeltab_Orderdetails = blltab_Orderdetails.GetModel(Request.QueryString["OrderDatailID"].toString().toInt32());
                    Modeltab_Orderdetails.ParentID = (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID);
                    Modeltab_Orderdetails.GrandParentID = (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID);
                    Modeltab_Orderdetails.GoodTypeId = TextBox5b002_OperationCenterID.Text.toInt32();
                    Modeltab_Orderdetails.GoodTypeIdBuyInfo = TextBoxb004_OperationGoodsID.Text.toInt32().toString();
                    Modeltab_Orderdetails.OrderCount = TextBox2OrderCount.Text.toInt32();
                    Modeltab_Orderdetails.UpdateDateTime = DateTime.Now;
                    blltab_Orderdetails.Update(Modeltab_Orderdetails);
                    blltab_Orderdetails.Update("Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + Modeltab_Orderdetails.ID);

                    Modeltab_Order = blltab_Order.GetModel(Modeltab_Orderdetails.OrderID.toInt32());
                    Modeltab_Order.PayDateTime = paydatetime;
                    Modeltab_Order.OrderName = "干预" + Modeltab_Goods.Name;
                    Modeltab_Order.UpdateDateTime = DateTime.Now;
                    Modeltab_Order.UserID = Model_tab_User.ID;
                    Modeltab_Order.DeliveryText = TextBox2DeliveryText.Text.toString().Trim();
                    Modeltab_Order.TotalMoney = Modeltab_Goods.PromotePrice * TextBox2OrderCount.Text.toInt32();
                    if (Modeltab_Order.PayWay != "Tenpay")
                    {////微信支付的 不要去改
                        Modeltab_Order.PaywayOrderNum = TextBox3_PaySerialNumber.Text;
                    }
                    blltab_Order.Update(Modeltab_Order);
                    blltab_Order.Update("Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + Modeltab_Orderdetails.OrderID.toInt32());

                    #region 增加运营中心的订单消息通知
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "干预订单,待确认";
                    Model_b011_InfoAlertMessage.CreateBy = "干预订单通知";
                    Model_b011_InfoAlertMessage.UpdateBy = "干预订单通知";
                    Model_b011_InfoAlertMessage.UserID = Model_b002_OperationCenter.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Modeltab_Order.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_myYunYingOrder";
                    Model_b011_InfoAlertMessage.TypeTableID = Modeltab_Orderdetails.OrderID.toInt32();
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加运营中心的订单消息通知 


                    //JsUtil.ShowMsg("干预保存成功!", "12OrderDetailEveryDay.aspx");
                }
                else if (type.ToLower() == "add")
                {
                    #region 本月已有购买限制
                    int intOutMaxCount = 0;
                    int intNowCount = 0;
                    Eggsoft_Public_CL.GoodP_YunYingZhongXin myGoodP_YunYingZhongXin = new GoodP_YunYingZhongXin();
                    myGoodP_YunYingZhongXin.checkLimitBuyEveryMonth(Model_tab_User.ID, strINCID.toInt32(), TextBoxb004_OperationGoodsID.Text.toInt32(), out intOutMaxCount, out intNowCount);
                    if (intOutMaxCount > 0 && (TextBox2OrderCount.Text.toInt32() + intNowCount > intOutMaxCount))
                    {////@张廷锋沁家技术人员 前两天公司开会有说把一个月一个ID号限单20单的在这个17000盒完成前先取消。因为我这边有些会员的单子入不进去。丁总让我和你说一下，你核实下吧。
                        //JsUtil.ShowMsg("本月已有" + intNowCount + "单,最多可以下" + (intOutMaxCount - intNowCount) + "单!", -1);
                        //return;
                    }
                    #endregion 本月已有购买限制

                    //#region 是否已经完全出局 完全出局也不能买
                    //Boolean boolHaveRunAway = myGoodP_YunYingZhongXin.checkHaveQuitUser(Model_tab_User.ID, strINCID.toInt32(), TextBoxb004_OperationGoodsID.Text.toInt32());
                    //if(boolHaveRunAway)
                    //{
                    //    JsUtil.ShowMsg("已经完全出局，该ID号无权再下单!", -1);
                    //    return;
                    //}
                    //#endregion 本月已有购买限制




                    //{

                    //                intOutMaxCount = 0;/////
                    //                intNowCount = 0;/////


                    EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel("ID=" + Model_b004_OperationGoods.GoodID);
                    EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
                    Modeltab_Order.CreateDateTime = DateTime.Now;
                    Modeltab_Order.DeliveryText = TextBox2DeliveryText.Text.toString().Trim();
                    Modeltab_Order.OrderName = "线下录单" + Modeltab_Goods.Name;
                    Modeltab_Order.OrderNum = "1";
                    Modeltab_Order.PayDateTime = paydatetime;
                    Modeltab_Order.PayStatus = 1;
                    Modeltab_Order.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Order.UserID = Model_tab_User.ID;
                    Modeltab_Order.TotalMoney = Modeltab_Goods.PromotePrice * TextBox2OrderCount.Text.toInt32();
                    Modeltab_Order.PaywayOrderNum = TextBox3_PaySerialNumber.Text;
                    int intModeltab_OrderID = blltab_Order.Add(Modeltab_Order);
                    String strOrderNum = "offline" + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + intModeltab_OrderID.ToString();
                    blltab_Order.Update("OrderNum='" + strOrderNum + "',UpdateDateTime=getdate(),Createby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "',Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + intModeltab_OrderID);


                    EggsoftWX.Model.tab_Orderdetails Modeltab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                    Modeltab_Orderdetails.GoodID = Modeltab_Goods.ID;
                    Modeltab_Orderdetails.OrderCount = TextBox2OrderCount.Text.toInt32();
                    Modeltab_Orderdetails.OrderID = intModeltab_OrderID;
                    Modeltab_Orderdetails.Over7DaysToBeans = false;
                    Modeltab_Orderdetails.ParentID = (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID);
                    Modeltab_Orderdetails.GrandParentID = (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID);
                    Modeltab_Orderdetails.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Orderdetails.GoodName = "线下录单" + Modeltab_Goods.Name;
                    Modeltab_Orderdetails.GoodPrice = Modeltab_Goods.PromotePrice;
                    Modeltab_Orderdetails.GoodType = 6;
                    Modeltab_Orderdetails.GoodTypeId = TextBox5b002_OperationCenterID.Text.toInt32();
                    Modeltab_Orderdetails.GoodTypeIdBuyInfo = TextBoxb004_OperationGoodsID.Text.toInt32().toString();
                    int intInputID = blltab_Orderdetails.Add(Modeltab_Orderdetails);
                    blltab_Orderdetails.Update("UpdateDateTime=getdate(),Createby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "',Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + intInputID);

                    #region 增加运营中心的订单消息通知
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "线下录单";
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Model_b002_OperationCenter.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Modeltab_Order.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_myYunYingOrder";
                    Model_b011_InfoAlertMessage.TypeTableID = intModeltab_OrderID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加运营中心的订单消息通知 
                }
                else if (type == "CopyFromYunYingZhongXin") ////运营中心录单数据
                {
                    string strb013_WriteOrderByOperationID = Request.QueryString["ID"].toString();

                    EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel("ID=" + Model_b004_OperationGoods.GoodID);
                    EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
                    Modeltab_Order.CreateDateTime = DateTime.Now;
                    Modeltab_Order.DeliveryText = TextBox2DeliveryText.Text.toString().Trim();
                    Modeltab_Order.OrderName = "处理运营中心录单" + Modeltab_Goods.Name;
                    Modeltab_Order.OrderNum = "1";
                    Modeltab_Order.PayDateTime = paydatetime;
                    Modeltab_Order.PaywayOrderNum = TextBox3_PaySerialNumber.Text;
                    Modeltab_Order.PayStatus = 1;
                    Modeltab_Order.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Order.UserID = Model_tab_User.ID;
                    Modeltab_Order.TotalMoney = Modeltab_Goods.PromotePrice * TextBox2OrderCount.Text.toInt32();

                    int intModeltab_OrderID = blltab_Order.Add(Modeltab_Order);
                    String strOrderNum = strb013_WriteOrderByOperationID + "online" + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + intModeltab_OrderID.ToString();
                    blltab_Order.Update("OrderNum='" + strOrderNum + "',UpdateDateTime=getdate(),Createby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "',Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + intModeltab_OrderID);


                    EggsoftWX.Model.tab_Orderdetails Modeltab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                    Modeltab_Orderdetails.GoodID = Modeltab_Goods.ID;
                    Modeltab_Orderdetails.OrderCount = TextBox2OrderCount.Text.toInt32();
                    Modeltab_Orderdetails.OrderID = intModeltab_OrderID;
                    Modeltab_Orderdetails.Over7DaysToBeans = false;
                    Modeltab_Orderdetails.ParentID = (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID);
                    Modeltab_Orderdetails.GrandParentID = (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID);
                    Modeltab_Orderdetails.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Orderdetails.GoodName = "处理运营中心录单" + Modeltab_Goods.Name;
                    Modeltab_Orderdetails.GoodPrice = Modeltab_Goods.PromotePrice;
                    Modeltab_Orderdetails.GoodType = 6;
                    Modeltab_Orderdetails.GoodTypeId = TextBox5b002_OperationCenterID.Text.toInt32();
                    Modeltab_Orderdetails.GoodTypeIdBuyInfo = TextBoxb004_OperationGoodsID.Text.toInt32().toString();
                    int intInputID = blltab_Orderdetails.Add(Modeltab_Orderdetails);
                    blltab_Orderdetails.Update("UpdateDateTime=getdate(),Createby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "',Updateby='" + strwebuy8_ClientAdmin_Users_ClientUserAccount + "'", "ID=" + intInputID);




                    #region 增加运营中心的订单消息通知
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "运营中心申请录单";
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Model_b002_OperationCenter.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = Modeltab_Order.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_myYunYingOrder";
                    Model_b011_InfoAlertMessage.TypeTableID = intModeltab_OrderID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加运营中心的订单消息通知 

                    #region 发送微信消息
                    EggsoftWX.BLL.b013_WriteOrderByOperation bll_b013_WriteOrderByOperation = new EggsoftWX.BLL.b013_WriteOrderByOperation();
                    EggsoftWX.Model.b013_WriteOrderByOperation Model_b013_WriteOrderByOperation = bll_b013_WriteOrderByOperation.GetModel(strb013_WriteOrderByOperationID.toInt32());


                    string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(Model_b013_WriteOrderByOperation.OperationCenterUserID.toString());
                    string strTitle = strUserNickName + "你好。你的运营中心下单:已被通过\n";
                    strTitle += "下单人ID：" + Model_b013_WriteOrderByOperation.BuyOrderShopUserID + "\n";
                    strTitle += "上级ID:" + Model_b013_WriteOrderByOperation.BuyParentShopUserID + "\n";
                    strTitle += "支付流水号:" + Model_b013_WriteOrderByOperation.PaySerialNumber + "\n";
                    strTitle += "请进入运营中心订单查看\n";
                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Model_b013_WriteOrderByOperation.OperationCenterUserID.toInt32(), 0, strTitle);
                    #endregion 发送微信消息

                    #region 复制身份证等到user表
                    Model_tab_User.IDCard = Model_b013_WriteOrderByOperation.BuyOrderShopUserIDIDCard;
                    Model_tab_User.ContactPhone = Model_b013_WriteOrderByOperation.BuyOrderShopUserIDContactPhone;
                    blltab_User.Update(Model_tab_User);
                    #endregion 复制身份证等到user表

                    #region 发送邮件消息
                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(strINCID));


                    string strTo = Model_b013_WriteOrderByOperation.OperationCenterEmail;
                    if (string.IsNullOrWhiteSpace(strTo) == false)
                    {
                        string strSubject = tab_System_And_.getTab_System("CityName") + "微店" + " 你的运营中心下单！";
                        string strBody = "你好，我们给你发信，是因为" + tab_System_And_.getTab_System("CityName") + "微店" + "反馈运营中心下单通知引起！" + "\n";
                        strBody += strUserNickName + "你好。你的运营中心下单:已被通过\n";
                        strTitle += "下单人ID：" + Model_b013_WriteOrderByOperation.BuyOrderShopUserID + "\n";
                        strTitle += "上级ID:" + Model_b013_WriteOrderByOperation.BuyParentShopUserID + "\n";
                        strTitle += "支付流水号:" + Model_b013_WriteOrderByOperation.PaySerialNumber + "\n";
                        Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "微店", strTo, strSubject, strBody);
                    }
                    #endregion 发送邮件消息

                    #region 告知下单表 通过了
                    Model_b013_WriteOrderByOperation.FeedbackStatus = 1;///已处理
                    Model_b013_WriteOrderByOperation.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b013_WriteOrderByOperation.UpdateTime = DateTime.Now;
                    bll_b013_WriteOrderByOperation.Update(Model_b013_WriteOrderByOperation);
                    #endregion 告知下单表 通过了


                    #region 消除处理消息
                    Model_b011_InfoAlertMessage = bll_b011_InfoAlertMessage.GetModel("Type='Info_b013_WriteOrderByOperation' and TypeTableID=" + Model_b013_WriteOrderByOperation.ID + "");
                    if (Model_b011_InfoAlertMessage != null)
                    {
                        Model_b011_InfoAlertMessage.Done = true;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateTime = DateTime.Now;
                        bll_b011_InfoAlertMessage.Update(Model_b011_InfoAlertMessage);
                    }
                    #endregion 消除处理消息

                }
                #region 是否自动给予分销权  首次购买制作证书

                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + Model_tab_User.ID+ " and IsDeleted=0");
                if (Model_tab_ShopClient_Agent_ == null || Model_tab_ShopClient_Agent_.Empowered != true)////首次购买制作证书
                {
                    EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                    EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                    if (tab_ShopClient_ShopPar_Model != null)
                    {
                        if (tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy.toBoolean())
                        {

                            Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Convert.ToInt32(Model_tab_User.ID));
                        }
                    }
                }
                #endregion

                #region 初始化所有运营中心数据  粉丝数据
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                    {
                        #region 运营中心 修改及 修改上级
                        #region  子与父
                        Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), Model_b002_OperationCenter.ID, Model_b002_OperationCenter.UserID.toInt32());
                        Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), Model_b002_OperationCenter.ID);
                        #endregion  子与父
                        #region  父与爷爷
                        EggsoftWX.BLL.b005_UserID_Operation_ID bll_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                        EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = bll_b005_UserID_Operation_ID.GetModel("UserID=" + (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID) + " and ShopClientID=" + strINCID.toInt32());
                        if (Model_b005_UserID_Operation_ID != null && Model_tab_UserGrandParentID == null && Model_tab_UserGrandParentID.ID > 0)
                        {
                            Model_b005_UserID_Operation_ID.UserParentID = Model_tab_UserGrandParentID.ID;
                            Model_b005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                            Model_b005_UserID_Operation_ID.UpdateBy = "后台运营修改代理上级";
                            bll_b005_UserID_Operation_ID.Update(Model_b005_UserID_Operation_ID);
                        }
                        #endregion  父与爷爷
                        #endregion 运营中心 修改及 修改上级



                        #region 修改上级
                        Eggsoft_Public_CL.Pub_Agent.modifyUserParent(Model_tab_User.ID, (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), strINCID.toInt32());
                        //Eggsoft_Public_CL.Pub_Agent.modifyUserParent((Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID), strINCID.toInt32());
                        Eggsoft_Public_CL.Pub_Agent.modifyUserAgentParent(Model_tab_User.ID, (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), strINCID.toInt32());
                        Eggsoft_Public_CL.Pub_Agent.modifyUserAgentParent((Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID), (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID), strINCID.toInt32());
                        #endregion 修改上级
                    }
                });
                #endregion 初始化所有运营中心数据


                if (type.ToLower() == "modify")
                {
                    JsUtil.ShowMsg("干预保存成功!", "12OrderDetailEveryDay.aspx");
                }
                else if (type.ToLower() == "add")
                {
                    string strCallBackUrl = Request.QueryString["CallBackUrl"].toString();

                    if (string.IsNullOrEmpty(strCallBackUrl))
                    {
                        JsUtil.ShowMsg("添加成功,录单窗口将关闭!", -100);
                    }
                    else
                    {
                        JsUtil.ShowMsg("添加成功,进入待收货状态!", strCallBackUrl);
                    }
                }
                else if (type == "CopyFromYunYingZhongXin")
                {
                    string strCallBackUrl = Request.QueryString["CallBackUrl"].toString();

                    if (string.IsNullOrEmpty(strCallBackUrl))
                    {
                        JsUtil.ShowMsg("处理运营中心下单成功,录单窗口将关闭!", -100);
                    }
                    else
                    {
                        JsUtil.ShowMsg("处理运营中心下单成功,进入待收货状态!", strCallBackUrl);
                    }
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "后台运营中心", "线程异常");
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione, "后台运营中心");
            }
            finally
            {

            }
        }




        protected void txtUserID_TextChanged(object sender, EventArgs e)
        {
            changeIMGAndLABEL(txtUserID.Text.toInt32(), Label1UserID, Image1UserID);



            String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            #region 用户真实姓名
            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
            Model_tab_User = BLL_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", txtUserID.Text.toInt32(), strINCID.toInt32());

            if (Model_tab_User != null)
            {
                #region 是否已经完全出局 完全出局也不能买
                Eggsoft_Public_CL.GoodP_YunYingZhongXin myGoodP_YunYingZhongXin = new GoodP_YunYingZhongXin();
                Boolean boolHaveRunAway = myGoodP_YunYingZhongXin.checkHaveQuitUser(Model_tab_User.ID, strINCID.toInt32(), TextBoxb004_OperationGoodsID.Text.toInt32());
                if (boolHaveRunAway)
                {
                    Label1UserID.Text = Label1UserID.Text + "   已经完全出局，该ID号无权再下单.(后台可以处理，前端不能下单)!";
                }
                #endregion 本月已有购买限制


                if (string.IsNullOrEmpty(Model_tab_User.UserRealName) == false)
                {
                    TextBoxUserReaname.Text = Model_tab_User.UserRealName;
                }
                else if (string.IsNullOrEmpty(Model_tab_User.ContactMan) == false)
                {
                    TextBoxUserReaname.Text = Model_tab_User.ContactMan;
                }
                else
                {
                    TextBoxUserReaname.Text = "";
                }
            }
            else
            {
                TextBoxUserReaname.Text = "";
            }
            #endregion 用户真实姓名



            EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
            EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(txtUserID.Text.toInt32(), strINCID.toInt32()), strINCID.toInt32());
            if (Model_b005_UserID_Operation_ID != null)
            {
                TextBox3ParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_b005_UserID_Operation_ID.UserParentID.toString()).toString();
                TextBox3ParentID_TextChanged(sender, e);

                EggsoftWX.Model.b005_UserID_Operation_ID ParentModel_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(TextBox3ParentID.Text.toInt32(), strINCID.toInt32()), strINCID.toInt32());
                if (ParentModel_b005_UserID_Operation_ID != null)
                {
                    TextBox4GrandParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(ParentModel_b005_UserID_Operation_ID.UserParentID.toString()).toString();
                    TextBox4GrandParentID_TextChanged(sender, e);
                }
                else
                {
                    TextBox4GrandParentID.Text = "";
                    TextBox4GrandParentID_TextChanged(sender, e);
                }

                TextBox5b002_OperationCenterID.Text = Model_b005_UserID_Operation_ID.OperationCenterID.toString();
                TextBox5b002_OperationCenterID_TextChanged(sender, e);
            }
            else if (Model_tab_User != null)
            {
                TextBox3ParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_tab_User.ParentID.toString()).toString();
                TextBox3ParentID_TextChanged(sender, e);



                EggsoftWX.Model.tab_User ParentModel_tab_User = BLL_tab_User.GetModel(Model_tab_User.ParentID.toInt32());


                EggsoftWX.Model.b005_UserID_Operation_ID ParentModel_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", Eggsoft_Public_CL.Pub.GetMyUserIDFromShopUserID(TextBox3ParentID.Text.toInt32(), strINCID.toInt32()), strINCID.toInt32());
                if (ParentModel_b005_UserID_Operation_ID != null)
                {
                    TextBox4GrandParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(ParentModel_b005_UserID_Operation_ID.UserParentID.toString()).toString();
                    TextBox4GrandParentID_TextChanged(sender, e);


                    TextBox5b002_OperationCenterID.Text = ParentModel_b005_UserID_Operation_ID.OperationCenterID.toString();
                    TextBox5b002_OperationCenterID_TextChanged(sender, e);
                }
                else if (ParentModel_tab_User != null)
                {
                    TextBox4GrandParentID.Text = Eggsoft_Public_CL.Pub.GetMyShopUserID(ParentModel_tab_User.ParentID.toString()).toString();
                    TextBox4GrandParentID_TextChanged(sender, e);
                }
                else
                {
                    TextBox4GrandParentID.Text = "";
                    TextBox4GrandParentID_TextChanged(sender, e);
                }


            }
            else
            {
                TextBox3ParentID.Text = "";
                TextBox3ParentID_TextChanged(sender, e);
                TextBox4GrandParentID.Text = "";
                TextBox4GrandParentID_TextChanged(sender, e);
                TextBox5b002_OperationCenterID.Text = "";
                TextBox5b002_OperationCenterID_TextChanged(sender, e);

            }


        }


        private void changeIMGAndLABEL(int IntShopUserID, System.Web.UI.WebControls.Label LabelShow, System.Web.UI.WebControls.Image ImageShow)
        {
            String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
            Model_tab_User = BLL_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", IntShopUserID, strINCID.toInt32());
            if (Model_tab_User != null)
            {
                ImageShow.ImageUrl = Model_tab_User.HeadImageUrl;
                LabelShow.Text = Model_tab_User.NickName;
            }
            else
            {
                //TextBoxUserReaname.Text = "";
                ImageShow.ImageUrl = "";
                LabelShow.Text = "";
            }
        }

        /// <summary>
        /// 已有的订单中不能存在该支付流水号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox3_PaySerialNumber_TextChanged(object sender, EventArgs e)
        {
            String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strPaySerialNumber = TextBox3_PaySerialNumber.Text;
            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            Boolean BooleanSerialNumber = BLL_tab_Order.Exists("PaywayOrderNum='" + strPaySerialNumber + "' and isnull(IsDeleted,0)=0 and ShopClient_ID=" + strINCID);
            if (BooleanSerialNumber)
            {
                TextBox3_PaySerialNumber_TipInfo.Text = "订单中已存在该流水号,您可能会重复录单";
            }
            else
            {
                TextBox3_PaySerialNumber_TipInfo.Text = "";
            }
        }

        protected void TextBox3ParentID_TextChanged(object sender, EventArgs e)
        {
            changeIMGAndLABEL(TextBox3ParentID.Text.toInt32(), LabelParentID, Image1ParentID);
        }

        protected void TextBox4GrandParentID_TextChanged(object sender, EventArgs e)
        {
            changeIMGAndLABEL(TextBox4GrandParentID.Text.toInt32(), Label1GrandParentID, Image1GrandParentID);
        }

        protected void TextBox5b002_OperationCenterID_TextChanged(object sender, EventArgs e)
        {
            String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
            EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = new EggsoftWX.Model.b002_OperationCenter();
            Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("ID=@ShopUserID and ShopClient_ID=@ShopClientID", TextBox5b002_OperationCenterID.Text.toInt32(), strINCID.toInt32());
            if (Model_b002_OperationCenter != null)
            {
                changeIMGAndLABEL(Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_b002_OperationCenter.UserID.toString()), Label1OperationCenterID, Image1OperationCenterID);
            }
            else
            {
                Image1OperationCenterID.ImageUrl = "";
                Label1OperationCenterID.Text = "";
            }


        }
    }
}