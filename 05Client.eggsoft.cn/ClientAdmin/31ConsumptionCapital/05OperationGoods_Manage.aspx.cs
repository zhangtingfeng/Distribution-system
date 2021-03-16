using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _05OperationGoods_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strPubBoard = "";
        private string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_04OperationGoods")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {
                strPubBoard = Request.QueryString["CallBackUrl"].toString();

                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.b004_OperationGoods bll = new EggsoftWX.BLL.b004_OperationGoods();
                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strPubBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }
        private void read_ShopClient_GoodID()
        {

            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
                int intb002_OperationCenterID = Request.QueryString["ID"].toInt32();
                string strthisGoodID = "0";
                string strDiscountGoodstrthisGoodID = "0";

                EggsoftWX.BLL.b004_OperationGoods bll_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bll_b004_OperationGoods.GetModel("ID=" + intb002_OperationCenterID);
                if (Model_b004_OperationGoods != null) strthisGoodID = Model_b004_OperationGoods.GoodID.ToString();
                if (Model_b004_OperationGoods != null) strDiscountGoodstrthisGoodID = Model_b004_OperationGoods.DiscountGoodID.ToString();
                string strSQL = @"SELECT   name,ID as GoodID ,ShortInfo from tab_Goods where ShopClient_ID={0} and IsDeleted=0 order by id desc";
                strSQL = string.Format(strSQL, strShopClientID, intb002_OperationCenterID);
                System.Data.DataTable myDataTable2 = bll_b004_OperationGoods.SelectList(strSQL).Tables[0];

                ListItem myThisListItem = null;
                ListItem myThisListItemDisCount = null;
                myThisListItemDisCount = new ListItem("无", "0");
                DropDownListDiscountGood.Items.Add(myThisListItemDisCount);

                for (int i = 0; i < myDataTable2.Rows.Count; i++)
                {
                    string strname = myDataTable2.Rows[i]["name"].ToString();
                    string strGoodID = myDataTable2.Rows[i]["GoodID"].ToString();

                    myThisListItem = new ListItem(strname, strGoodID);
                    myThisListItemDisCount = new ListItem(strname, strGoodID);

                    DropDownList1GoodID.Items.Add(myThisListItem);
                    DropDownListDiscountGood.Items.Add(myThisListItemDisCount);
                }
                DropDownList1GoodID.SelectedValue = strthisGoodID;
                DropDownListDiscountGood.SelectedValue = strDiscountGoodstrthisGoodID;
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "运营中心商品管理", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "运营中心商品管理");
            }

        }
        private void SetClass()
        {
            read_ShopClient_GoodID();
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.b004_OperationGoods bll = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.Model.b004_OperationGoods Model = bll.GetModel(Int32.Parse(ID));

                CheckBox_RunningStatus.Checked = Model.RunningStatus.toBoolean();
                Textbox_MoneyConsumerWeighting.Text = Model.MoneyConsumerWeighting.toString();
                Textbox1_ReturnMoneyShareA.Text = Model.ReturnMoneyShareA.toString();
                Textbox2_ReturnMoneyShareB.Text = Model.ReturnMoneyShareB.toString();
                Textbox3_ReturnMoneyOperationShareA.Text = Model.ReturnMoneyOperationShareA.toString();
                Textbox4_ReturnMoneyOperationShareB.Text = Model.ReturnMoneyOperationShareB.toString();
                Textbox5_ReturnMoneyToCompany.Text = Model.ReturnMoneyToCompany.toString();
                Textbox_Price_ReturnConsumerWealth.Text = Model.ReturnConsumerWealth.toInt32().toString();
                Textbox_LimitBuyEveryMonth.Text = Model.LimitBuyEveryMonth.toInt32().toString();

                Textbox_TextboxMoneyConsumerAllOrderA.Text = Model.HowToReturnMoneyA.toString();
                Textbox_TextboxMoneyConsumerAllOrderB.Text = Model.HowToReturnMoneyB.toString();
                CheckBox_ShowConsumerWealthAgreement.Checked = Model.ShowConsumerWealthAgreement.toBoolean();
                //txt_MoneyConsumerAllOrder.Text = Model.ConsumerAllOrder.toString();

                #region 编辑协议
                HyperLinkConsumerWealthAgreement.Enabled = true;
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
                string strUpLoadURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"];
                string strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClientID + "&GoToUrl=";
                string strNavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("07OperationGoods/ConsumerWealthAgreement.aspx?Usersb004_OperationGoodsID=" + ID);
                HyperLinkConsumerWealthAgreement.NavigateUrl = strNavigateUrl;
                #endregion 编辑协议

                #region 编辑提现显示的阅读须知
                HyperLink1ConsumerWealthDrawMoney.Enabled = true;
                //string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").toString();
                //string strUpLoadURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"];
                strHyperLink_MakeHtml = strUpLoadURL + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClientID + "&GoToUrl=";
                strNavigateUrl = strHyperLink_MakeHtml + Server.UrlEncode("07OperationGoods/ConsumerWealthDrawMoney.aspx?Usersb004_OperationGoodsID=" + ID);
                HyperLink1ConsumerWealthDrawMoney.NavigateUrl = strNavigateUrl;
                #endregion 编辑提现显示的阅读须知
                btnAdd.Text = "保 存";
            }

            #region  统计多少个要发放
            Decimal UnitDecimal_5994_20171021 = 1998 * 3;
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            string strA = @"SELECT 
      [b005_UserID_Operation_ID].[UserID]
  FROM[b005_UserID_Operation_ID] LEFT OUTER JOIN b002_OperationCenter
  on[b005_UserID_Operation_ID].OperationCenterID = b002_OperationCenter.id
  where isnull(b002_OperationCenter.ShareholderState,0)= 1 and isnull([b005_UserID_Operation_ID].ActiveAccount,0)= 1";

            string strB = @"SELECT 
      [b005_UserID_Operation_ID].[UserID]
  FROM[b005_UserID_Operation_ID] LEFT OUTER JOIN b002_OperationCenter
  on[b005_UserID_Operation_ID].OperationCenterID = b002_OperationCenter.id
  where isnull(b002_OperationCenter.ShareholderState,0)= 0 and isnull([b005_UserID_Operation_ID].ActiveAccount,0)= 1";

            string stsqlrWhereActiveOrderNumA = "OrderDetailID is not null and ActiveOrderNum> 0 and ((ActiveOrderNum * " + UnitDecimal_5994_20171021 + "  - ReturnMoneyUnit - ActiveOrderNum * " + UnitDecimal_5994_20171021 + " * 0.2) < ActiveOrderNum * " + 1998 + ") and  b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID   and userID in (" + strA + ")";
            string stsqlrWhereActiveOrderNumB = "OrderDetailID is not null and ActiveOrderNum> 0 and ((ActiveOrderNum * " + UnitDecimal_5994_20171021 + "  - ReturnMoneyUnit - ActiveOrderNum * " + UnitDecimal_5994_20171021 + " * 0.2) < ActiveOrderNum * " + 1998 + ") and  b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID   and userID in (" + strB + ")";
            string strWhereActiveOrderNumA = "SELECT sum(ActiveOrderNum)  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where  " + stsqlrWhereActiveOrderNumA;////没有拿到本金的消费者
            string strWhereActiveOrderNumB = "SELECT sum(ActiveOrderNum)  FROM [b008_OpterationUserActiveReturnMoneyOrderNum] where  " + stsqlrWhereActiveOrderNumB;////没有拿到本金的消费者

            int IntAllCountA = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strWhereActiveOrderNumA, 1, 21).Tables[0].Rows[0][0].toInt32();
            int IntAllCountB = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strWhereActiveOrderNumB, 1, 21).Tables[0].Rows[0][0].toInt32();
            Literal3ConsumerAllOrderA.Text = "发放个数参考值：" + IntAllCountA;
            Literal4ConsumerAllOrderB.Text = "发放个数参考值：" + IntAllCountB;

            #endregion  统计多少个要发放

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            decimal decimalMy = Textbox_MoneyConsumerWeighting.Text.toDecimal() +
                       Textbox1_ReturnMoneyShareA.Text.toDecimal() +
                       Textbox2_ReturnMoneyShareB.Text.toDecimal() +
        Textbox3_ReturnMoneyOperationShareA.Text.toDecimal() +
        Textbox4_ReturnMoneyOperationShareB.Text.toDecimal();
            decimal ReturnMoneyToCompany = Textbox5_ReturnMoneyToCompany.Text.toDecimal();
            if ((decimalMy + ReturnMoneyToCompany) != 100)
            {
                JsUtil.ShowMsg("保存失败，消费者加权平均直接返还+分享者A+分享者B+运营中心A+运营中心B+公司成本应该等于100%", -1);
            }

            if (Textbox_TextboxMoneyConsumerAllOrderA.Text.toDecimal() == 0 && Textbox_TextboxMoneyConsumerAllOrderB.Text.toDecimal() == 0)
            {
                JsUtil.ShowMsg("保存失败，本程序暂不支持都采用0分红，否侧将双倍返还。请联系系统程序员复核该问题", -1);
            }

            strPubBoard = Request.QueryString["CallBackUrl"].toString();
            try
            {
                string strID = Request.QueryString["ID"];// 修改ID
                Int32 intShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString().toInt32();
                string type = Request.QueryString["type"];
                string strGoodID = DropDownList1GoodID.SelectedValue;
                string strDiscountGood = DropDownListDiscountGood.SelectedValue;
                EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();

                if (type.ToLower() == "modify")
                {

                    #region 检测是否存在其他的 已选择这个商品的运营
                    string strWhere = "goodID=@goodID and ShopClient_ID=@ShopClient_ID and IsDeleted=0 and id<>@id";
                    bool boolgoodID = bllb004_OperationGoods.Exists(strWhere, strGoodID.toInt32(), intShopClient_ID, strID);
                    if (boolgoodID)
                    {
                        JsUtil.ShowMsg("添加失败，已经存在当前运营的商品，请改选其他商品!", -1);
                    }
                    #endregion 检测是否存在其他的 已选择这个商品的运营

                    EggsoftWX.Model.b004_OperationGoods Modelb004_OperationGoods = bllb004_OperationGoods.GetModel(Int32.Parse(strID));

                    Modelb004_OperationGoods.RunningStatus = CheckBox_RunningStatus.Checked;
                    Modelb004_OperationGoods.MoneyConsumerWeighting = Textbox_MoneyConsumerWeighting.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyShareA = Textbox1_ReturnMoneyShareA.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyShareB = Textbox2_ReturnMoneyShareB.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyOperationShareA = Textbox3_ReturnMoneyOperationShareA.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyOperationShareB = Textbox4_ReturnMoneyOperationShareB.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyToCompany = Textbox5_ReturnMoneyToCompany.Text.toDecimal();
                    Modelb004_OperationGoods.GoodID = strGoodID.toInt32();
                    Modelb004_OperationGoods.UpdateTime = DateTime.Now;
                    Modelb004_OperationGoods.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Modelb004_OperationGoods.ReturnConsumerWealth = Textbox_Price_ReturnConsumerWealth.Text.toInt32();
                    Modelb004_OperationGoods.LimitBuyEveryMonth = Textbox_LimitBuyEveryMonth.Text.toInt32();


                    Modelb004_OperationGoods.HowToReturnMoneyA = Textbox_TextboxMoneyConsumerAllOrderA.Text.toDecimal();
                    Modelb004_OperationGoods.HowToReturnMoneyB = Textbox_TextboxMoneyConsumerAllOrderB.Text.toDecimal();
                    Modelb004_OperationGoods.ShowConsumerWealthAgreement = CheckBox_ShowConsumerWealthAgreement.Checked;
                    Modelb004_OperationGoods.DiscountGoodID = strDiscountGood.toInt32();
                    Modelb004_OperationGoods.UpdateTime = DateTime.Now;

                    bllb004_OperationGoods.Update(Modelb004_OperationGoods);
                    JsUtil.ShowMsg("修改成功!", strPubBoard);

                }
                else if (type.ToLower() == "add")
                {
                    #region 检测是否存在其他的 已选择这个商品的运营
                    string strWhere = "goodID=@goodID and ShopClient_ID=@ShopClient_ID and IsDeleted=0";
                    bool boolgoodID = bllb004_OperationGoods.Exists(strWhere, strGoodID.toInt32(), intShopClient_ID);
                    if (boolgoodID)
                    {
                        JsUtil.ShowMsg("添加失败，已经存在当前运营的商品，请改选其他商品!", -1);
                    }
                    #endregion 检测是否存在其他的 已选择这个商品的运营

                    EggsoftWX.Model.b004_OperationGoods Modelb004_OperationGoods = new EggsoftWX.Model.b004_OperationGoods();

                    Modelb004_OperationGoods.ShopClient_ID = intShopClient_ID;
                    Modelb004_OperationGoods.RunningStatus = CheckBox_RunningStatus.Checked;
                    Modelb004_OperationGoods.MoneyConsumerWeighting = Textbox_MoneyConsumerWeighting.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyShareA = Textbox1_ReturnMoneyShareA.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyShareB = Textbox2_ReturnMoneyShareB.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyOperationShareA = Textbox3_ReturnMoneyOperationShareA.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyOperationShareB = Textbox4_ReturnMoneyOperationShareB.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnMoneyToCompany = Textbox5_ReturnMoneyToCompany.Text.toDecimal();
                    Modelb004_OperationGoods.ReturnConsumerWealth = Textbox_Price_ReturnConsumerWealth.Text.toInt32();
                    Modelb004_OperationGoods.LimitBuyEveryMonth = Textbox_LimitBuyEveryMonth.Text.toInt32();


                    Modelb004_OperationGoods.GoodID = strGoodID.toInt32();
                    Modelb004_OperationGoods.HowToReturnMoneyA = Textbox_TextboxMoneyConsumerAllOrderA.Text.toDecimal();
                    Modelb004_OperationGoods.HowToReturnMoneyB = Textbox_TextboxMoneyConsumerAllOrderB.Text.toDecimal();
                    Modelb004_OperationGoods.ShowConsumerWealthAgreement = CheckBox_ShowConsumerWealthAgreement.Checked;
                    Modelb004_OperationGoods.DiscountGoodID = strDiscountGood.toInt32();
                    Modelb004_OperationGoods.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Modelb004_OperationGoods.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;

                    bllb004_OperationGoods.Add(Modelb004_OperationGoods);
                    JsUtil.ShowMsg("添加成功!", strPubBoard);

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
    }
}