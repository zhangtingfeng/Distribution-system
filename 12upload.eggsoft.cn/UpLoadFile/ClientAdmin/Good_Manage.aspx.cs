using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin
{
    public partial class Good_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin//System.Web.UI.Page//
    {
        public string strTextbox_Timer0_Text = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
        public string strTextbox_Timer1_Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ini_DropDownList_MinSalesCount();
                ini_DropDownList_Timer_MaxSalesCount();
                InitClass1();
                Init_DropDownList_FreightTemplet();

                Init_DropDownList_tab_Unit();
                ini_DropDownList_MaxSalesCount();

                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

                    EggsoftWX.Model.tab_Goods Model = bll.GetModel(Int32.Parse(ID));//删除文件
                    //Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.Icon));//删除文件

                    bll.Delete(Int32.Parse(ID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/" + strCallBackUrl);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {

                    SetClass();

                }
            }
        }




        private void ini_DropDownList_MinSalesCount()
        {
            for (int i = 1; i <= 999; i++)
            {
                ListItem ListItemNew = new ListItem((i).ToString(), (i).ToString());
                DropDownList_MinSalesCount.Items.Add(ListItemNew);

            }
        }

        private void ini_DropDownList_MaxSalesCount()
        {
            for (int i = 1; i <= 999; i++)
            {
                ListItem ListItemNew = new ListItem((i).ToString(), (i).ToString());
                DropDownList_MaxSalesCount.Items.Add(ListItemNew);

            }
        }


        private void ini_DropDownList_Timer_MaxSalesCount()
        {
            for (int i = 1; i <= 999; i++)
            {
                ListItem ListItemNew = new ListItem((i).ToString(), (i).ToString());
                DropDownList_Timer_MaxSalesCount.Items.Add(ListItemNew);

            }
        }



        private void InitClass1()
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            DropDownList_Class1.DataSource = new EggsoftWX.BLL.tab_Class1().GetDataTable("100", "ID,ClassName", "ShopClientID=" + strShopClient_ID + " order by Sort asc,ID asc");
            DropDownList_Class1.DataTextField = "ClassName";
            DropDownList_Class1.DataValueField = "ID";
            DropDownList_Class1.DataBind();
            DropDownList_Class1.Items.Insert(0, new ListItem("请选择商品分类", "0"));


        }

        private void InitClass2_DataSource()
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (DropDownList_Class1.SelectedValue.IndexOf("请选择") == -1)
            {
                DropDownList_Class2.DataSource = new EggsoftWX.BLL.tab_Class2().GetDataTable("100", "ID,ClassName", " and Class1_ID=" + DropDownList_Class1.SelectedValue + " and ShopClientID=" + strShopClient_ID + "  order by Sort asc,ID asc");
                DropDownList_Class2.DataTextField = "ClassName";
                DropDownList_Class2.DataValueField = "ID";
                DropDownList_Class2.DataBind();
            }
            DropDownList_Class2.Items.Insert(0, new ListItem("请选择商品二级分类", "0"));
        }

        private void InitClass3_DataSource()
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (DropDownList_Class2.SelectedValue.IndexOf("请选择") == -1)
            {
                DropDownList_Class3.DataSource = new EggsoftWX.BLL.tab_Class3().GetDataTable("100", "ID,ClassName", " and Class2_ID=" + DropDownList_Class2.SelectedValue + " and ShopClientID=" + strShopClient_ID + "  order by Sort asc,ID asc");
                DropDownList_Class3.DataTextField = "ClassName";
                DropDownList_Class3.DataValueField = "ID";
                DropDownList_Class3.DataBind();
            }
            DropDownList_Class3.Items.Insert(0, new ListItem("请选择商品三级分类", "0"));
        }


        private void Init_DropDownList_FreightTemplet()
        {

            EggsoftWX.BLL.tab_FreightTemplate myBLL_tab_FreightTemplate = new EggsoftWX.BLL.tab_FreightTemplate();

            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (myBLL_tab_FreightTemplate.Exists("ShopClient_ID=" + strShopClient_ID))
            {



                DropDownList_FreightTemplet.DataSource = myBLL_tab_FreightTemplate.GetDataTable("100", "ID,Name", " and ShopClient_ID=" + strShopClient_ID + " order by ID asc");

                DropDownList_FreightTemplet.DataTextField = "Name";
                DropDownList_FreightTemplet.DataValueField = "ID";
                DropDownList_FreightTemplet.DataBind();


                DropDownList_FreightTemplet.Items.Insert(0, new ListItem("包邮", "0"));


            }
            else
            {
                DropDownList_FreightTemplet.Items.Insert(0, new ListItem("包邮", "0"));


            }
        }

        //private void Init_DropDownList_tab_Goods_Class()
        //{

        //    EggsoftWX.BLL.tab_Goods_Class myBLL_tab_Goods_Class = new EggsoftWX.BLL.tab_Goods_Class();

        //    string strUserID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
        //    if (myBLL_tab_Goods_Class.Exists("userID=" + strUserID))
        //    {
        //        bool boolShowChild = Eggsoft_Public_CL.Pub.boolShowPower(strUserID, "GoodChildClass");
        //        string strPID = " and PID=0";

        //        string strSQLWhere = "";
        //        if (boolShowChild)
        //        {
        //            strPID = "";

        //            strSQLWhere += "SELECT ID, ";
        //            strSQLWhere += "RTRIM(LTRIM(ISNULL ([Expr3] , '')+' '+ISNULL ( [Expr2] , '')+' '+ISNULL ( [Expr1] , '')+ISNULL ( [ClassName] , ''))) as ClassName from( SELECT  tab_Goods_Class.ID,tab_Goods_Class.userID,tab_Goods_Class.ClassName, ";
        //            strSQLWhere += "tab_Goods_Class_1.ClassName AS Expr1, ";
        //            strSQLWhere += "tab_Goods_Class_2.ClassName AS Expr2, ";
        //            strSQLWhere += "tab_Goods_Class_3.ClassName AS Expr3 ";
        //            strSQLWhere += "FROM tab_Goods_Class AS tab_Goods_Class_2 ";
        //            strSQLWhere += "LEFT OUTER JOIN tab_Goods_Class AS tab_Goods_Class_3 ON tab_Goods_Class_2.PID = tab_Goods_Class_3.ID ";
        //            strSQLWhere += "RIGHT OUTER JOIN tab_Goods_Class AS tab_Goods_Class_1 ON tab_Goods_Class_2.ID = tab_Goods_Class_1.PID ";
        //            strSQLWhere += "RIGHT OUTER JOIN tab_Goods_Class ON tab_Goods_Class_1.ID = tab_Goods_Class.PID  where tab_Goods_Class.userID=" + strUserID + ") AS t5";


        //            //strSQLWhere += "SELECT     tab_Goods_Class.*, tab_Goods_Class_1.ClassName AS Expr1, tab_Goods_Class_2.ClassName AS Expr2, tab_Goods_Class_3.ClassName AS Expr3 ";
        //            //strSQLWhere += "FROM         tab_Goods_Class AS tab_Goods_Class_2 LEFT OUTER JOIN ";
        //            //strSQLWhere += "                      tab_Goods_Class AS tab_Goods_Class_3 ON tab_Goods_Class_2.PID = tab_Goods_Class_3.ID RIGHT OUTER JOIN ";
        //            //strSQLWhere += "       tab_Goods_Class AS tab_Goods_Class_1 ON tab_Goods_Class_2.ID = tab_Goods_Class_1.PID RIGHT OUTER JOIN ";
        //            //strSQLWhere += "      tab_Goods_Class ON tab_Goods_Class_1.ID = tab_Goods_Class.PID ";
        //            //strSQLWhere += "";
        //            DropDownList_tab_Goods_Class.DataSource = myBLL_tab_Goods_Class.SelectList(strSQLWhere);

        //        }
        //        else
        //        {
        //            DropDownList_tab_Goods_Class.DataSource = myBLL_tab_Goods_Class.GetDataTable("100", "ID,ClassName", " and userID=" + strUserID + strPID + " order by Sort asc,ID asc");

        //        }


        //        DropDownList_tab_Goods_Class.DataTextField = "ClassName";
        //        DropDownList_tab_Goods_Class.DataValueField = "ID";
        //        DropDownList_tab_Goods_Class.DataBind();

        //    }
        //    else
        //    {

        //    }
        //}
        private void Init_DropDownList_tab_Unit()
        {
            DropDownList_Unit.DataSource = new EggsoftWX.BLL.tab_Goods_Unit().GetDataTable("100", "ID,Unit", "1=1 order by ID asc");
            DropDownList_Unit.DataTextField = "Unit";
            DropDownList_Unit.DataValueField = "ID";
            DropDownList_Unit.DataBind();

        }



        private void SetClass()
        {
            string type = Request.QueryString["type"];

            DropDownList_MinSalesCount.SelectedValue = "1";
            DropDownList_MaxSalesCount.SelectedValue = "99";

            #region 是否启用购物券功能
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(Int32.Parse(strINCID));
            Panel_GouWuQuan.Visible = Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers);
            Panel_GouWuQuan.Enabled = Panel_GouWuQuan.Visible;

            Panel_CaiFuJiFen.Visible = Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32());
            Panel_CaiFuJiFen.Enabled = Panel_CaiFuJiFen.Visible;

            Panel_Send_Vouchers_IfBuy.Visible = Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers);
            Panel_Send_Vouchers_IfBuy.Enabled = Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers);


            #endregion
            if (type.ToLower() == "modify")
            {

                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model = bll.GetModel(Int32.Parse(ID));




                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                Upload_MultiSeclect2.OnInit(Model.Icon, upLoadpath);

                txtName.Text = Model.Name;
                Textbox_Price.Text = Convert.ToDecimal(Model.Price).ToString("###0.00");
                Textbox_PromotePrice.Text = Convert.ToDecimal(Model.PromotePrice).ToString("###0.00");
                Textbox_AgentPercentMoney.Text = Convert.ToDecimal(Model.AgentPercent).ToString("###0.00");
                Textbox_Total_Vouchers_Consume_Or_Recharge.Text = Convert.ToDecimal(Model.Shopping_Vouchers_Percent).ToString("###0.00");
                Textbox_Total_CaiFuJiFen_Consume_Or_Recharge.Text = Convert.ToDecimal(Model.WealthMoney).ToString("###0.00");//财富积分最大允许金额

                Textbox_Send_Money_IfBuy.Text = Convert.ToDecimal(Model.Send_Money_IfBuy).ToString("###0.00");
                Textbox_Send_Vouchers_IfBuy.Text = Convert.ToDecimal(Model.Send_Vouchers_IfBuy).ToString("###0.00");


                DropDownList_Unit.SelectedValue = Model.Unit;
                //DropDownList_tab_Goods_Class.SelectedValue = Model.Good_Class.ToString();

                if (Model.FreightTemplate_ID >= 0)
                {
                    DropDownList_FreightTemplet.SelectedValue = Model.FreightTemplate_ID.ToString();
                }
                DropDownList_Class1.SelectedValue = Model.Class1_ID.ToString();

                if (Model.Class2_ID > 0)
                {
                    DropDownList_Class2.Visible = true;
                    InitClass2_DataSource();
                    DropDownList_Class2.SelectedValue = Model.Class2_ID.ToString();
                }
                else
                {
                    DropDownList_Class2.Visible = false;
                }
                if (Model.Class3_ID > 0)
                {
                    DropDownList_Class3.Visible = true;
                    InitClass3_DataSource();
                    DropDownList_Class3.SelectedValue = Model.Class3_ID.ToString();
                }
                else
                {
                    DropDownList_Class3.Visible = false;
                }
                if (Convert.ToBoolean(Model.isSaled))
                    RadioButtonList_IsSaled.SelectedValue = "1";
                else
                    RadioButtonList_IsSaled.SelectedValue = "0";

                //txtContent_ShortInfo.Text = Server.HtmlDecode(Model.ShortInfo);
                txtContent_LongInfo.Text = Server.HtmlDecode(Model.LongInfo);
                Textbox_KuCunLiang.Text = Model.KuCunCount.ToString();
                Textbox_GoodKg.Text = Model.kg.ToString();
                //string LimitTimerBuy = Model.TimerBuyXML;

                try
                {
                    CheckBox_DoTimer.Checked = Convert.ToBoolean(Model.LimitTimerBuy_Bool);
                    CheckBox_WeiBai_RedMoney.Checked = Convert.ToBoolean(Model.CheckBox_WeiBai_RedMoney);

                    if (CheckBox_DoTimer.Checked) Panel_CheckBox_DoTimer.Visible = true;

                    strTextbox_Timer0_Text = Convert.ToDateTime(Model.LimitTimerBuy_StartTime).ToString("yyyy-MM-dd HH:mm:ss");
                    strTextbox_Timer1_Text = Convert.ToDateTime(Model.LimitTimerBuy_EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                    Textbox_Price_Timer.Text = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model.LimitTimerBuy_TimePrice));
                    DropDownList_Timer_MaxSalesCount.SelectedValue = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToInt32(Model.LimitTimerBuy_MaxSalesCount));
                }
                catch
                { }
                finally { }
                //}
                txtMenuPos.Text = Model.Sort.ToString();
                TextBox_ShotInfo.Text = Server.HtmlDecode(Model.ShortInfo);
                DropDownList_MinSalesCount.SelectedValue = Model.MinOrderNum.ToString();
                DropDownList_MaxSalesCount.SelectedValue = Model.MaxOrderNum.ToString();


                string strXML = Model.XML;
                if (string.IsNullOrEmpty(strXML) == false)
                {
                    Eggsoft_Public_CL.XML__Class_Shop_Goods myGoodsXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Goods>(strXML, System.Text.Encoding.UTF8);
                    HyperLink__Mp3path.NavigateUrl = myGoodsXML.Mp3path;
                    HyperLink__Mp3path.Text = myGoodsXML.Mp3path;

                }

                readMultiPrice(Int32.Parse(ID));

                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {

                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                string webFilePath = System.Web.HttpContext.Current.Server.MapPath(upLoadpath);
                Eggsoft.Common.FileFolder.makeFolder(webFilePath);
                Upload_MultiSeclect2.OnInit("", upLoadpath);

            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int intMinSalesCount = Int32.Parse(DropDownList_MinSalesCount.SelectedValue);
                int intMaxSalesCount = Int32.Parse(DropDownList_MaxSalesCount.SelectedValue);
                if (intMinSalesCount > intMaxSalesCount)
                {
                    JsUtil.ShowMsg("购买数量限制范围设置错误！!", "javascript:history.back();");
                    return;
                }

                string strkg = Textbox_GoodKg.Text;
                if (String.IsNullOrEmpty(strkg)) strkg = "0";
                Decimal Decimalkg = Decimal.Parse(strkg);
                if (Decimalkg < 0)
                {
                    JsUtil.ShowMsg("商品重量错误！!", "javascript:history.back();");
                    return;
                }

                int intTextbox_KuCunLiang = Int32.Parse(Textbox_KuCunLiang.Text);
                if ((intTextbox_KuCunLiang != 0) && (intMinSalesCount > intTextbox_KuCunLiang))
                {
                    JsUtil.ShowMsg("起卖数量，库存数量范围设置错误！!", "javascript:history.back();");
                    return;
                }

                TextBox dddTextBox = Upload_MultiSeclect2.FindControl("TextBox_txtReturnValue") as TextBox;
                if (string.IsNullOrEmpty(dddTextBox.Text))
                {
                    JsUtil.ShowMsg("商品图片必须选择", "javascript:history.back();");
                    return;
                }

                Decimal PromotePrice = 0;
                Decimal AgentPercent = 0;
                Decimal Shopping_Vouchers_Percent = 0;
                Decimal CaiFuJiFen_Consume_Or_Recharge = 0;
                Decimal.TryParse(Textbox_PromotePrice.Text, out PromotePrice);///财富积分最大允许金额
                Decimal.TryParse(Textbox_AgentPercentMoney.Text, out AgentPercent);
                Decimal.TryParse(Textbox_Total_Vouchers_Consume_Or_Recharge.Text, out Shopping_Vouchers_Percent);
                Decimal.TryParse(Textbox_Total_CaiFuJiFen_Consume_Or_Recharge.Text, out CaiFuJiFen_Consume_Or_Recharge);///财富积分最大允许金额

                if (AgentPercent > PromotePrice)
                {
                    JsUtil.ShowMsg("代理利润不能超过商品价格", "javascript:history.back();");
                    return;
                }
                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                Model_tab_ShopClient = bll_tab_ShopClient.GetModel(Int32.Parse(strINCID));
                if (Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers))
                {
                    if (Shopping_Vouchers_Percent > (PromotePrice))////2016 /3/12 蓝梦游艇 修改
                    {
                        JsUtil.ShowMsg("你已启用购物券功能,购物券允许数值必须设置正确,必须不大于实际销售价格（打折价格）", "javascript:history.back();");
                        return;
                    }
                }
                if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                {
                    if (CaiFuJiFen_Consume_Or_Recharge > (PromotePrice))////2016 /3/12 蓝梦游艇 修改
                    {
                        JsUtil.ShowMsg("你已启用财富积分购买功能,财富积分允许数值必须设置正确,必须不大于实际销售价格（打折价格）", "javascript:history.back();");
                        return;
                    }
                }
                if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()) && Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers))
                {
                    if ((CaiFuJiFen_Consume_Or_Recharge + Shopping_Vouchers_Percent) > (PromotePrice))////2016 /3/12 蓝梦游艇 修改
                    {
                        JsUtil.ShowMsg("你已启用财富积分购买功能+购物券功能,之和允许数值必须设置正确,必须不大于实际销售价格（打折价格）", "javascript:history.back();");
                        return;
                    }
                }



                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model = bll.GetModel(Int32.Parse(ID));
                    Model = saveModel(Model);
                    Model = saveXMLModel(Model);
                    if (Model == null)
                    {
                        return;
                    }
                    Model.Icon = dddTextBox.Text;
                    int intSavesaveMultiPrice = saveMultiPrice(Int32.Parse(ID), Shopping_Vouchers_Percent, CaiFuJiFen_Consume_Or_Recharge, AgentPercent, Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers));

                    if (intSavesaveMultiPrice == -4)
                    {
                        JsUtil.ShowMsg("你已启用财富积分+购物券功能。种类价格必须在允许的范围内，必须不小于之和数值", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -3)
                    {
                        JsUtil.ShowMsg("你已启用财富积分功能。种类价格必须在允许的范围内，必须不小于财富积分数值", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -2)
                    {
                        JsUtil.ShowMsg("你已启用购物券功能。种类价格必须在允许的范围内，必须不小于购物券数值。", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -1)
                    {
                        JsUtil.ShowMsg("种类价格必须在允许的范围内，必须不小于代理分销商利润。", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == 0)
                    {
                        //continues
                    }
                    ;
                    bll.Update(Model);
                    Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(Model.Icon, Int16.Parse(strINCID));
                    System.Threading.Tasks.Task.Factory.StartNew(() =>///自动更新代理商商品
                    {
                        if (Convert.ToBoolean(Model_tab_ShopClient.AutoMidifyAgentGoods)) Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(ID));
                    });



                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/" + strCallBackUrl);


                }
                else
                    if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods model = new EggsoftWX.Model.tab_Goods();

                    model = saveModel(model);
                    model = saveXMLModel(model);

                    if (model == null)
                    {
                        return;
                    }
                    model.Icon = dddTextBox.Text;
                    int intGoodID = bll.Add(model);
                    int intSavesaveMultiPrice = saveMultiPrice(intGoodID, Shopping_Vouchers_Percent, CaiFuJiFen_Consume_Or_Recharge, AgentPercent, Convert.ToBoolean(Model_tab_ShopClient.Shopping_Vouchers));

                    if (intSavesaveMultiPrice == -4)
                    {
                        JsUtil.ShowMsg("你已启用财富积分+购物券功能。种类价格必须在允许的范围内，必须不小于之和数值", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -3)
                    {
                        JsUtil.ShowMsg("你已启用财富积分功能。种类价格必须在允许的范围内，必须不小于财富积分数值", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -2)
                    {
                        JsUtil.ShowMsg("你已启用购物券功能。种类价格必须在允许的范围内，必须不小于购物券数值", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == -1)
                    {
                        JsUtil.ShowMsg("种类价格必须在允许的范围内，必须不小于代理分销商利润。", "javascript:history.back();");
                        return;
                    }
                    else if (intSavesaveMultiPrice == 0)
                    {
                        //continues
                    };

                    Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(model.Icon, Int16.Parse(strINCID));

                    if (Convert.ToBoolean(Model_tab_ShopClient.AutoMidifyAgentGoods)) Eggsoft_Public_CL.GoodP.updateAllAgentPercent(intGoodID);

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    if (String.IsNullOrEmpty(strCallBackUrl) == true)
                    {
                        strCallBackUrl = "Board_Good.aspx";
                    }
                    else
                    {
                        strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    }
                    JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/" + strCallBackUrl);
                }
            }

            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "更新商品", "线程异常");
            }

            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione, "更新商品");

            }
            finally
            {

            }
        }

        private void readMultiPrice(int intGoodID)
        {
            EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
            EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = new EggsoftWX.Model.tab_Goods_MultiSelectTypePrice();



            System.Data.DataTable myDataTable = BLL_tab_Goods_MultiSelectTypePrice.GetList("GoodId=" + intGoodID + " order by id asc").Tables[0];

            string multi_Price_Line = "";

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string GoodMultiName = myDataTable.Rows[i]["GoodMultiName"].ToString();
                string Price_Num = myDataTable.Rows[i]["GoodPrice"].ToString();

                Price_Num = Decimal.Parse(Price_Num).ToString("###0.00");
                multi_Price_Line += "<tr><td>选择种类<input name=\"Text_Choice_Name" + i + "\" id=\"Text_Choice_Name" + i + "\" value=\"" + GoodMultiName + "\" type=\"text\"> 价格<input name=\"Text_Price_Num" + i + "\" value=\"" + Price_Num + "\" id=\"Text_Price_Num" + i + "\" type=\"text\"></td><td></td></tr> \n";

            }
            Literalmulti_Price_Line.Text = multi_Price_Line;


        }
        /// <summary>
        /// 多种商品分类的功能
        /// </summary>
        /// <param name="intGoodID"></param>
        /// <param name="DecimalShopping_Vouchers_Percent"></param>
        /// <param name="CaiFuJiFen_Consume_Or_Recharge"></param>
        /// <param name="DecimalAgentPercent"></param>
        /// <param name="boolShopClient_Shopping_Vouchers"></param>
        /// <returns></returns>
        private int saveMultiPrice(int intGoodID, Decimal DecimalShopping_Vouchers_Percent, Decimal CaiFuJiFen_Consume_Or_Recharge, Decimal DecimalAgentPercent, bool boolShopClient_Shopping_Vouchers)
        {
            EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice BLL_tab_Goods_MultiSelectTypePrice = new EggsoftWX.BLL.tab_Goods_MultiSelectTypePrice();
            EggsoftWX.Model.tab_Goods_MultiSelectTypePrice Model_tab_Goods_MultiSelectTypePrice = new EggsoftWX.Model.tab_Goods_MultiSelectTypePrice();

            BLL_tab_Goods_MultiSelectTypePrice.Delete("GoodID=" + intGoodID);
            string strRowNumCount = Request.Form["RowLength"];
            int intRowNumCount = Int32.Parse(strRowNumCount);

            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            bool boolExsitMode_OperationCenter = (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strShopClient_ID.toInt32()));

            for (int i = 0; i < intRowNumCount; i++)
            {
                string strText_Choice_Name = Request.Form["Text_Choice_Name" + i];
                string strText_Price_Num = Request.Form["Text_Price_Num" + i];
                Decimal Price_Num = 0;
                Decimal.TryParse(strText_Price_Num, out Price_Num);

                if (String.IsNullOrEmpty(strText_Choice_Name)) continue;

                Decimal AgentPercent = 0;
                Decimal.TryParse(Textbox_AgentPercentMoney.Text, out AgentPercent);
                if (Price_Num < AgentPercent)
                {
                    return -1;
                }

                if (boolShopClient_Shopping_Vouchers)
                {///你已启用购物券功能,购物券允许数值必须设置正确,必须不大于实际销售价格（打折价格）
                    if (DecimalShopping_Vouchers_Percent > (Price_Num))
                    {
                        return -2;
                    }
                }
                if (boolExsitMode_OperationCenter)
                {///你已启用财富积分功能,购物券允许数值必须设置正确,必须不大于实际销售价格（打折价格）
                    if (CaiFuJiFen_Consume_Or_Recharge > (Price_Num))
                    {
                        return -3;
                    }
                }
                if ((boolShopClient_Shopping_Vouchers) && (boolExsitMode_OperationCenter))
                {
                    if (DecimalShopping_Vouchers_Percent + CaiFuJiFen_Consume_Or_Recharge > Price_Num)
                    {
                        return -4;
                    }
                }



                Model_tab_Goods_MultiSelectTypePrice.GoodID = intGoodID;
                Model_tab_Goods_MultiSelectTypePrice.GoodMultiName = strText_Choice_Name;
                Model_tab_Goods_MultiSelectTypePrice.GoodPrice = Price_Num;
                BLL_tab_Goods_MultiSelectTypePrice.Add(Model_tab_Goods_MultiSelectTypePrice);
            }

            return 0;
        }


        private EggsoftWX.Model.tab_Goods saveXMLModel(EggsoftWX.Model.tab_Goods Model)
        {
            try
            {
                string MP3upLoadpath = "";
                if (FileUpload_Mp3.HasFile == true)
                {
                    string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                    MP3upLoadpath = Eggsoft.Common.FileFolder.btnFileUpload(FileUpload_Mp3, (upLoadpath));


                    Eggsoft_Public_CL.XML__Class_Shop_Goods myShopGoodsXML;
                    if (String.IsNullOrEmpty(Model.XML) == false)
                    {
                        myShopGoodsXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Goods>(Model.XML, System.Text.Encoding.UTF8);
                    }
                    else
                    {
                        myShopGoodsXML = new Eggsoft_Public_CL.XML__Class_Shop_Goods();
                    }
                    myShopGoodsXML.Mp3path = MP3upLoadpath;
                    string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myShopGoodsXML, System.Text.Encoding.UTF8);
                    Model.XML = strXML;

                }

            }
            catch
            { }

            finally { }

            return Model;

        }

        private EggsoftWX.Model.tab_Goods saveModel(EggsoftWX.Model.tab_Goods Model)
        {
            try
            {
                Model.Name = txtName.Text.Trim();
                Model.Price = Convert.ToDecimal(Textbox_Price.Text);
                Model.Send_Money_IfBuy = Convert.ToDecimal(Textbox_Send_Money_IfBuy.Text);
                Model.Send_Vouchers_IfBuy = Convert.ToDecimal(Textbox_Send_Vouchers_IfBuy.Text);

                Model.PromotePrice = Convert.ToDecimal(Textbox_PromotePrice.Text);
                Model.AgentPercent = Convert.ToDecimal(Textbox_AgentPercentMoney.Text);

                Decimal Decimalmy = 0;
                Decimal.TryParse(Textbox_Total_Vouchers_Consume_Or_Recharge.Text, out Decimalmy);
                Model.Shopping_Vouchers_Percent = Decimalmy;
                Model.WealthMoney = Textbox_Total_CaiFuJiFen_Consume_Or_Recharge.Text.toDecimal();//财富积分最大允许金额


                Model.Unit = DropDownList_Unit.Text;

                if ((DropDownList_FreightTemplet.SelectedIndex != -1) && (DropDownList_FreightTemplet.SelectedIndex != null))
                {
                    if (string.IsNullOrEmpty(DropDownList_FreightTemplet.SelectedValue) == false)
                    {
                        Model.FreightTemplate_ID = Convert.ToInt32(DropDownList_FreightTemplet.SelectedValue.ToString());
                    }
                }

                if ((DropDownList_Class1.SelectedIndex != 0) && (DropDownList_Class1.SelectedIndex != -1))
                {
                    if (string.IsNullOrEmpty(DropDownList_Class1.SelectedValue) == false)
                    {
                        Model.Class1_ID = Convert.ToInt32(DropDownList_Class1.SelectedValue.ToString());
                    }
                    else
                    {
                        Model.Class1_ID = 0;
                    }
                }
                else
                {
                    Model.Class1_ID = 0;
                }
                if ((DropDownList_Class2.SelectedIndex != 0) && (DropDownList_Class2.SelectedIndex != -1))
                {
                    if (string.IsNullOrEmpty(DropDownList_Class2.SelectedValue) == false)
                    {
                        Model.Class2_ID = Convert.ToInt32(DropDownList_Class2.SelectedValue.ToString());
                    }
                    else
                    {
                        Model.Class2_ID = 0;
                    }
                }
                else
                {
                    Model.Class2_ID = 0;
                }

                if ((DropDownList_Class3.SelectedIndex != 0) && (DropDownList_Class3.SelectedIndex != -1))
                {
                    if (string.IsNullOrEmpty(DropDownList_Class3.SelectedValue) == false)
                    {
                        Model.Class3_ID = Convert.ToInt32(DropDownList_Class3.SelectedValue.ToString());
                    }
                    else
                    {
                        Model.Class3_ID = 0;
                    }
                }
                else
                {
                    Model.Class3_ID = 0;
                }

                Model.isSaled = (RadioButtonList_IsSaled.SelectedValue.ToString() == "1");
                Model.LongInfo = Server.HtmlEncode(txtContent_LongInfo.Text);
                Model.Sort = Int32.Parse(txtMenuPos.Text);
                Model.UpdateTime = DateTime.Now;

                string strINC = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model.ShopClient_ID = Int32.Parse(strINC);
                Model.ShortInfo = Server.HtmlEncode(TextBox_ShotInfo.Text);

                Model.KuCunCount = Int32.Parse(Textbox_KuCunLiang.Text);
                string strkg = Textbox_GoodKg.Text;
                if (String.IsNullOrEmpty(strkg)) strkg = "0";
                Model.kg = Decimal.Parse(strkg);
                Model.MinOrderNum = Int32.Parse(DropDownList_MinSalesCount.SelectedValue);
                Model.MaxOrderNum = Int32.Parse(DropDownList_MaxSalesCount.SelectedValue);

                Model.IS_Admin_check = true;//现在不需要审核

                //Model.IS_Admin_check = false;//保存成功 请等待审核


                #region 秒杀时间
                Model.LimitTimerBuy_Bool = CheckBox_DoTimer.Checked;
                Model.CheckBox_WeiBai_RedMoney = CheckBox_WeiBai_RedMoney.Checked;

                string strText_SecondBuyStart = Request.Form["Text-SecondBuyStart"];
                string strText_SecondBuyEnd = Request.Form["Text-SecondBuyEnd"];

                DateTime my1datetime = DateTime.ParseExact(strText_SecondBuyStart, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime my2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                Model.LimitTimerBuy_StartTime = my1datetime;
                Model.LimitTimerBuy_EndTime = my2datetime;


                if (Convert.ToDateTime(Model.LimitTimerBuy_StartTime).Ticks > Convert.ToDateTime(Model.LimitTimerBuy_EndTime).Ticks)
                {
                    JsUtil.ShowMsg("亲 ，开始时间设置不对吧,开始时间“" + Model.LimitTimerBuy_StartTime + "”大于结束时间“" + Model.LimitTimerBuy_EndTime + "”！！！");
                    Model.LimitTimerBuy_Bool = false;
                    return null;
                }
                Decimal DecimalLimitTimerBuy_TimePrice = 0;
                Decimal.TryParse(Textbox_Price_Timer.Text, out DecimalLimitTimerBuy_TimePrice);
                Model.LimitTimerBuy_TimePrice = DecimalLimitTimerBuy_TimePrice;

                Model.LimitTimerBuy_MaxSalesCount = Int32.Parse(DropDownList_Timer_MaxSalesCount.SelectedValue);
                if (CheckBox_DoTimer.Checked)
                {
                    if (Model.LimitTimerBuy_TimePrice < (Decimal)0.01)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("亲 ，秒杀价格设置不对吧！！！");
                        return null;
                    }
                    else if (Model.LimitTimerBuy_TimePrice > Model.PromotePrice)
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("同学 ，秒杀价格设置不对吧,比促销价格还高？！！！");
                        return null;
                    }
                }
                //}
                #endregion


            }

            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "更新商品", "线程异常");
            }

            catch (Exception e)
            {
                debug_Log.Call_WriteLog(e, "更新商品");
                debug_Log.Call_WriteLog("saveModel:" + e.ToString(), "更新商品");
            }
            finally
            {

            }

            return Model;
        }


        protected void DropDownList_Class1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList_Class2.Visible = false;
            DropDownList_Class3.Visible = false;

            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (DropDownList_Class1.SelectedIndex > 0)
            {
                DropDownList_Class2.DataSource = new EggsoftWX.BLL.tab_Class2().GetDataTable("100", "ID,ClassName", " and Class1_ID=" + DropDownList_Class1.SelectedValue + " and ShopClientID=" + strShopClient_ID);
                DropDownList_Class2.DataTextField = "ClassName";
                DropDownList_Class2.DataValueField = "ID";
                DropDownList_Class2.DataBind();
                if (DropDownList_Class2.Items.Count > 0)
                {
                    DropDownList_Class2.Visible = true;
                    DropDownList_Class2.Items.Insert(0, new ListItem("请选择商品二级分类", "0"));
                }
            }
        }

        protected void DropDownList_Class2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            DropDownList_Class3.Visible = false;

            DropDownList_Class3.DataSource = new EggsoftWX.BLL.tab_Class3().GetDataTable("100", "ID,ClassName", " and Class2_ID=" + DropDownList_Class2.SelectedValue + " and ShopClientID=" + strShopClient_ID);
            DropDownList_Class3.DataTextField = "ClassName";
            DropDownList_Class3.DataValueField = "ID";
            DropDownList_Class3.DataBind();
            if (DropDownList_Class3.Items.Count > 0)
            {
                DropDownList_Class3.Visible = true;

                DropDownList_Class3.Items.Insert(0, new ListItem("请选择商品三级分类", "0"));
            }
        }
        protected void txtContent_Init(object sender, EventArgs e)
        {
            //txtContent_ShortInfo.Height = 347;
            txtContent_LongInfo.Height = 547;
        }


        protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            AjaxControlToolkit.AsyncFileUpload fileUploader = sender as AjaxControlToolkit.AsyncFileUpload;
            if (fileUploader != null && fileUploader.HasFile)
            {
                string ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();// 修改ID
                Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath("/upload/TempUpload/" + ID + "_" + fileUploader.FileName));
                fileUploader.SaveAs(Server.MapPath("/upload/TempUpload/" + ID + "_" + fileUploader.FileName));
                Eggsoft_Public_CL.GoodP.ScaleBMP("/upload/TempUpload/" + ID + "_" + fileUploader.FileName, 800, 600, "NoreW");
            }
        }


        public String DisPlay(string strintMenu)
        {
            string strDisPlay = "";
            string strNotDisPlay = " style=\"DISPLAY: none\"";

            string UserName = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + UserName + "");

            String[] stringPowerList = Eggsoft_Public_CL.Pub.GetstringPowerList(Model_tab_ShopClient.XML);



            if (strintMenu == "Good_Voice")
            {
                //if (stringPowerList.Length > 0)
                //{
                //    if (stringPowerList[0] == "1")
                //    {
                //    }
                //    else
                //    {
                strDisPlay = strNotDisPlay;
                //    }
                //}
                //return strDisPlay;
            }
            else if (strintMenu == "Shopping_Vouchers")
            {

                strDisPlay = strNotDisPlay;///购物券都不用

            }
            else if (strintMenu == "ShopClient_ProductClass")
            {

                strDisPlay = strNotDisPlay;///购物券都不用          

            }


            return strDisPlay;
        }



    }
}