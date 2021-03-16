using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._01Shopping_Vouchers
{
    public partial class Shopping_Vouchers_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String DisPlayStatus_New_None = "";
        public String strText_Shopping_Vouchers_Start = "";
        public String strText_Shopping_Vouchers_End = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];

                //Link0.Text = "./Default" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + ".aspx";

                if (type.ToLower() == "shixiao")
                {
                    string strScheme_ID = Request.QueryString["Scheme_ID"];
                    if (!CommUtil.IsNumStr(strScheme_ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme blltab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                    blltab_ShopClient_Shopping_VouchersScheme.Update("ValidateEndTime=@ValidateEndTime", "ID=@ID", DateTime.Now.AddDays(-1), strScheme_ID);

                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail blltab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    blltab_ShopClient_Shopping_VouchersScheme_Detail.Update("ValidateEndTime=@ValidateEndTime", "Scheme_ID=@Scheme_ID", DateTime.Now.AddDays(-1), strScheme_ID);
                    JsUtil.ShowMsg("失效成功!", "Shopping_Vouchers_Board.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }




        private void SetClass()
        {
            #region 绑定商品
            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            System.Data.DataSet myDataSet = BLL_tab_Goods.GetList("Name,ID", " ShopClient_ID=" + strShopClientID + " and Shopping_Vouchers_Percent>0 and isSaled=1 and IsDeleted=0 order by Sort asc,id desc");
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                CheckBoxList_GoodList.DataSource = myDataSet;
                CheckBoxList_GoodList.DataBind();
            }
            else
            {
                LabelGoodListMarkerShow.Visible = true;
                btnAdd.Visible = false;
            }
            #endregion 绑定商品

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strScheme_ID = Request.QueryString["Scheme_ID"];// 修改ID

                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme blltab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme Model = blltab_ShopClient_Shopping_VouchersScheme.GetModel("ID=" + strScheme_ID + "");



                strText_Shopping_Vouchers_Start = Convert.ToDateTime(Model.ValidateStartTime).ToString("yyyy-MM-dd HH:mm:ss");
                strText_Shopping_Vouchers_End = Convert.ToDateTime(Model.ValidateEndTime).ToString("yyyy-MM-dd HH:mm:ss");




                RadioButtonList2HowToGet.SelectedValue = Model.HowToGet.toString();
                DropDownList1LimitHowMany.SelectedValue = Model.LimitHowMany.toString();
                CheckBox1ValidateTypeAbsoluteCheck.Checked = Model.ValidateTypeAbsoluteCheck.toBoolean();
                CheckBox2ValidateTypeRelativeCheck.Checked = Model.ValidateTypeRelativeCheck.toBoolean();
                Textbox1ValidateDateTypeRelative.Text = Model.ValidateDateTypeRelative.toString();

                RadioButtonList3HowToUse.SelectedValue = Model.HowToUse.toString();
                TextBox_HowmanyMoneyCanUse.Text = Model.HowToUseLimitMaxMoney.toString();
                Textbox_HowManyNum.Text = Model.AllCount.toString();
                Textbox_Money.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.Money.toDecimal());

                RadioButtonList2HowToGet.Enabled = false;
                DropDownList1LimitHowMany.Enabled = false;
                RadioButtonList3HowToUse.Enabled = false;
                TextBox_HowmanyMoneyCanUse.Enabled = false;
                Textbox_HowManyNum.Enabled = false;
                Textbox_Money.Enabled = false;
                CheckBox1ValidateTypeAbsoluteCheck.Enabled = false;
                CheckBox2ValidateTypeRelativeCheck.Enabled = false;
                Textbox1ValidateDateTypeRelative.Enabled = false;

                CheckBoxList_GoodList.Enabled = false;
                #region  对应商品
                //RadioButtonList1GoodList.SelectedValue = (Model.GoodList == "0" ? "0" : "1");
                //if (RadioButtonList1GoodList.SelectedValue == "1")
                //{
                string[] strGoodList = Model.GoodList.Split(',');
                for (int i = 0; i < strGoodList.Length; i++)
                {
                    for (int j = 0; j < CheckBoxList_GoodList.Items.Count; j++)
                    {
                        if (strGoodList[i].toString() == CheckBoxList_GoodList.Items[j].Value.toString())
                        {
                            CheckBoxList_GoodList.Items[j].Selected = true;
                        }
                    }
                }
                //}
                #endregion  对应商品



                Vouchers_Title.Text = Model.Vouchers_Title;


                btnAdd.Text = "保 存";

                DisPlayStatus_New_None = "";
            }
            else
            {
                strText_Shopping_Vouchers_Start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strText_Shopping_Vouchers_End = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss");

                DisPlayStatus_New_None = "display:none;";

                for (int j = 0; j < CheckBoxList_GoodList.Items.Count; j++)
                {
                    CheckBoxList_GoodList.Items[j].Selected = true;
                }

            }





        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"];


            #region 商品列表
            bool ifChoiceGood = false;

            string strCheckBoxList_GoodList = "";
            for (int j = 0; j < CheckBoxList_GoodList.Items.Count; j++)
            {
                if (CheckBoxList_GoodList.Items[j].Selected == true)
                {
                    ifChoiceGood = true;
                    strCheckBoxList_GoodList += CheckBoxList_GoodList.Items[j].Value + ",";
                }
            }
            if (ifChoiceGood == false)
            {
                JsUtil.ShowMsg("不能保存，没有相应的商品可以使用优惠券!", -1);
            }
            if (string.IsNullOrEmpty(strCheckBoxList_GoodList) == false) strCheckBoxList_GoodList = strCheckBoxList_GoodList.Remove(strCheckBoxList_GoodList.Length - 1, 1);
            #endregion 商品列表

            #region 购物券时间
            string strText_SecondBuyStart = Request.Form["Text_Shopping_Vouchers_Start"];
            string strText_SecondBuyEnd = Request.Form["Text_Shopping_Vouchers_End"];
            DateTime my1datetime = DateTime.ParseExact(strText_SecondBuyStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime my2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            #endregion 购物券时间


            if (type.ToLower() == "modify")
            {
                string strScheme_ID = Request.QueryString["Scheme_ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme bll = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme Model = bll.GetModel(strScheme_ID.toInt32());

                Model.Vouchers_Title = Vouchers_Title.Text;
                Model.ValidateStartTime = my1datetime;
                Model.ValidateEndTime = my2datetime;
                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);

                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail blltab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                blltab_ShopClient_Shopping_VouchersScheme_Detail.Update("ValidateStartTime=@ValidateStartTime,ValidateEndTime=@ValidateEndTime", "Scheme_ID=@Scheme_ID", Model.ValidateStartTime, Model.ValidateEndTime, strScheme_ID.toInt32());


                JsUtil.ShowMsg("修改成功!", "Shopping_Vouchers_Board.aspx");
            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail bll_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                //#region 获取起始号码
                int intHowMany = Int32.Parse(Textbox_HowManyNum.Text);
                #region 生成系列购物券         
                if (intHowMany > 3000)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("最多3000张", "javascript:history.back()");
                    return;
                }

                if (Textbox1ValidateDateTypeRelative.Text.toInt32() < 1 || Textbox1ValidateDateTypeRelative.Text.toInt32() > 1000)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("领用后多少天过期必须在1到1000之间", "javascript:history.back()");
                    return;
                }

                if (RadioButtonList3HowToUse.SelectedValue.toInt32() == 1 && TextBox_HowmanyMoneyCanUse.Text.toDecimal() == 0)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("满足多少金额才能使用不能为0", "javascript:history.back()");
                    return;
                }
                if (CheckBox1ValidateTypeAbsoluteCheck.Checked == false && CheckBox2ValidateTypeRelativeCheck.Checked == false)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("过期方式至少选中一个", "javascript:history.back()");
                    return;
                }
                if (RadioButtonList2HowToGet.Text.toInt32() == 1 && CheckBox1ValidateTypeAbsoluteCheck.Checked == false)////线下发放才实际生成
                {
                    Eggsoft.Common.JsUtil.ShowMsg("线下发放,指定领取,必须选中有效起始日期", "javascript:history.back()");
                    return;
                }



                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLLtab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme Model_Shopping_VouchersScheme = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme();
                Model_Shopping_VouchersScheme.Vouchers_Title = Vouchers_Title.Text;
                Model_Shopping_VouchersScheme.Money = Textbox_Money.Text.toDecimal();
                Model_Shopping_VouchersScheme.AllCount = Textbox_HowManyNum.Text.toInt32();
                Model_Shopping_VouchersScheme.ShopClientID = strShopClientID.toInt32();
                Model_Shopping_VouchersScheme.GoodList = strCheckBoxList_GoodList;

                Model_Shopping_VouchersScheme.ValidateTypeAbsoluteCheck = CheckBox1ValidateTypeAbsoluteCheck.Checked;
                Model_Shopping_VouchersScheme.ValidateTypeRelativeCheck = CheckBox2ValidateTypeRelativeCheck.Checked;
                Model_Shopping_VouchersScheme.ValidateDateTypeRelative = Textbox1ValidateDateTypeRelative.Text.toInt32();


                Model_Shopping_VouchersScheme.HowToUse = RadioButtonList3HowToUse.SelectedValue.toInt32();
                Model_Shopping_VouchersScheme.HowToUseLimitMaxMoney = TextBox_HowmanyMoneyCanUse.Text.toDecimal();
                Model_Shopping_VouchersScheme.LimitHowMany = DropDownList1LimitHowMany.SelectedValue.toInt32();
                Model_Shopping_VouchersScheme.ValidateStartTime = my1datetime;
                Model_Shopping_VouchersScheme.ValidateEndTime = my2datetime;
                Model_Shopping_VouchersScheme.HowToGet = RadioButtonList2HowToGet.Text.toInt32();
                Int32 myVouchersSchemeInt32 = BLLtab_ShopClient_Shopping_VouchersScheme.Add(Model_Shopping_VouchersScheme);


                #region 运行存储过程
                if (Model_Shopping_VouchersScheme.HowToGet == 1)////线下发放才实际生成
                {
                    EggsoftWX.BLL.tab_Shopping_Vouchers_RunProcedure bll_tab_Shopping_Vouchers_RunProcedure = new EggsoftWX.BLL.tab_Shopping_Vouchers_RunProcedure();
                    //              @return_value = [dbo].[RunProcedure_DoShopping_Vouchers]
                    //      @ShopClientID = 1,
                    //@Scheme_ID = 11,
                    //@Money = 12,
                    //@AllNum = 11111,
                    //@DoFinshed = @DoFinshed OUTPUT
                    string[] strList = { strShopClientID, myVouchersSchemeInt32.toString(), Textbox_Money.Text, intHowMany.ToString(), my1datetime.ToString(), my2datetime.ToString() };
                    //my1datetime.ToString();
                    bool mybool = bll_tab_Shopping_Vouchers_RunProcedure.AddAllNum(strList);
                }
                #endregion 运行存储过程

                #endregion

                JsUtil.ShowMsg("添加成功!", "Shopping_Vouchers_Board.aspx");

            }



        }




        //protected void RadioButtonList1GoodList_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    if (RadioButtonList1GoodList.SelectedValue == "1")
        //    {
        //        UpdatePanel1GoodList.Visible = false;
        //    }
        //    else if (RadioButtonList1GoodList.SelectedValue == "0")
        //    {
        //        UpdatePanel1GoodList.Visible = true;
        //    }
        //}
    }
}