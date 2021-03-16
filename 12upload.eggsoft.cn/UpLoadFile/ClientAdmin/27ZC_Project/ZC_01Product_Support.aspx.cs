using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin._27ZC_Project
{
    public partial class ZC_01Product_Support : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextbox_Timer1_Text = DateTime.Now.AddDays(60).ToString("yyyy-MM-dd HH:mm:ss");
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strtab_ZC_01Product_Support = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strtab_ZC_01Product_Support))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ZC_01Product_Support blltab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    blltab_ZC_01Product_Support.Delete(Int32.Parse(strtab_ZC_01Product_Support));
                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?").Replace("^", "&");
                    JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/27ZC_Project/" + strCallBackUrl);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }



        private void SetClass()
        {
            string type = Request.QueryString["type"];
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (type.ToLower() == "modify")
            {

                string IDblltab_ZC_01Product = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ZC_01Product_Support blltab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                EggsoftWX.Model.tab_ZC_01Product_Support Modeltab_ZC_01Product_Support = blltab_ZC_01Product_Support.GetModel(Int32.Parse(IDblltab_ZC_01Product));




                TextboxName.Text = (Modeltab_ZC_01Product_Support.Name);
                TextboxAgentPrice.Text = ((Decimal)(Modeltab_ZC_01Product_Support.AgentPrice)).ToString("###0.00");
                TextboxSalesPrice.Text = ((Decimal)(Modeltab_ZC_01Product_Support.SalesPrice)).ToString("###0.00");
                RadioButtonList_SupportWay.SelectedValue = ((int)(Modeltab_ZC_01Product_Support.SupportWay)).ToString();
                TextboxSalesLimit.Text = ((int)(Modeltab_ZC_01Product_Support.SalesLimit)).ToString();
                TextboxSalesPricePromiseAndReturn.Text = (Modeltab_ZC_01Product_Support.SalesPricePromiseAndReturn);
                Textbox_Sort.Text = ((int)(Modeltab_ZC_01Product_Support.Sort)).ToString();


                RadioButtonList_SupportWay.SelectedValue = Modeltab_ZC_01Product_Support.SupportWay.ToString();

                TextboxSupportHowMany.Text = ((int)(Modeltab_ZC_01Product_Support.SupportHowMany)).ToString();
                CheckBox_MustSubscribe.Checked = (bool)Modeltab_ZC_01Product_Support.MustSubscribe;
                CheckBox_MustAddress.Checked = (bool)Modeltab_ZC_01Product_Support.MustAddress;
                CheckBoxOnlyBuyOneOnlyOneAccount.Checked = (bool)Modeltab_ZC_01Product_Support.OnlyBuyOneOnlyOneAccount;



                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                Decimal SalesPrice = 0;
                Decimal.TryParse(TextboxSalesPrice.Text, out SalesPrice);
                if (SalesPrice < 0)
                {
                    JsUtil.ShowMsg("档位支付金额不能为0", "javascript:history.back();");
                    return;
                }

                Decimal AgentPrice = 0;
                Decimal.TryParse(TextboxAgentPrice.Text, out AgentPrice);
                if (SalesPrice < AgentPrice)
                {
                    JsUtil.ShowMsg("代理商金额太少", "javascript:history.back();");
                    return;
                }


                if (String.IsNullOrEmpty(TextboxName.Text))
                {
                    JsUtil.ShowMsg("档位名称不能为空", "javascript:history.back();");
                    return;
                }
              
                int intSupportWay = Int32.Parse(RadioButtonList_SupportWay.SelectedValue.ToString());
                int intTextboxSupportHowMany = 0;
                int.TryParse(TextboxSupportHowMany.Text, out intTextboxSupportHowMany);
                if (intTextboxSupportHowMany < 1 && (intSupportWay == 1 || intSupportWay == 2))
                {
                    JsUtil.ShowMsg("已选中抽奖发货,每满足多少个抽奖不能小于1", "javascript:history.back();");
                    return;
                }

                if (String.IsNullOrEmpty(TextboxSalesPricePromiseAndReturn.Text))
                {
                    JsUtil.ShowMsg("当前档位承诺或回报不能为空", "javascript:history.back();");
                    return;
                }

                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string IDtab_ZC_01Product_Support = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_ZC_01Product_Support blltab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Modeltab_ZC_01Product_Support = blltab_ZC_01Product_Support.GetModel(Int32.Parse(IDtab_ZC_01Product_Support));

                    if (saveModel(Modeltab_ZC_01Product_Support) == false)
                    {
                        return;
                    }
                    //Model.ChangePicList = dddTextBox_ChangePicList.Text;
                    blltab_ZC_01Product_Support.Update(Modeltab_ZC_01Product_Support);


                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?").Replace("^", "&");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/27ZC_Project/" + strCallBackUrl);
                }
                else
                    if (type.ToLower() == "add")
                    {
                           string IDblltab_ZC_01ProductZCID = Request.QueryString["ZCID"];// 修改ID
                     
                        EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                        EggsoftWX.Model.tab_ZC_01Product modeltab_ZC_01Product = blltab_ZC_01Product.GetModel(Int32.Parse(IDblltab_ZC_01ProductZCID));
                      

                        EggsoftWX.BLL.tab_ZC_01Product_Support blltab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                        EggsoftWX.Model.tab_ZC_01Product_Support modeltab_ZC_01Product_Support = new EggsoftWX.Model.tab_ZC_01Product_Support();
                        modeltab_ZC_01Product_Support.ZC_01ProductID = Int32.Parse(IDblltab_ZC_01ProductZCID);
                        if (saveModel(modeltab_ZC_01Product_Support) == false)
                        {
                            return;
                        }
                        modeltab_ZC_01Product_Support.SourceGoodID = modeltab_ZC_01Product.SourceGoodID;
                        int inttab_WeiKanJiaID = blltab_ZC_01Product_Support.Add(modeltab_ZC_01Product_Support);

                     
                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        if (String.IsNullOrEmpty(strCallBackUrl) == true)
                        {
                            strCallBackUrl = "Board_01ZC_ProjectSupport.aspx?ZCID=" + IDblltab_ZC_01ProductZCID;
                        }
                        else
                        {
                            strCallBackUrl = strCallBackUrl.Replace("*", "?").Replace("^", "&");
                        }
                        JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/27ZC_Project/" + strCallBackUrl);
                    }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog("saveModel:" + Exceptione.ToString());
            }
            finally
            {

            }
        }


        private bool saveModel(EggsoftWX.Model.tab_ZC_01Product_Support Model)
        {
            bool boolsaveModel = false;
            try
            {
                Model.Name = Eggsoft.Common.StringNum.MaxLengthString(TextboxName.Text, 10);

                Model.AgentPrice = Convert.ToDecimal(TextboxAgentPrice.Text);
                Model.SalesPrice = Convert.ToDecimal(TextboxSalesPrice.Text);

                Model.SalesLimit = Int32.Parse(TextboxSalesLimit.Text);
                Model.IsSales = (RadioButtonList_IsSaled.SelectedValue.ToString() == "1");
                Model.SalesPricePromiseAndReturn = Eggsoft.Common.StringNum.MaxLengthString(TextboxSalesPricePromiseAndReturn.Text, 250);
                Model.SupportWay = Int32.Parse(RadioButtonList_SupportWay.SelectedValue.ToString());
                Model.SupportHowMany = Int32.Parse(TextboxSupportHowMany.Text);


                Model.MustSubscribe = CheckBox_MustSubscribe.Checked;
                Model.MustAddress = CheckBox_MustAddress.Checked;
                Model.OnlyBuyOneOnlyOneAccount = CheckBoxOnlyBuyOneOnlyOneAccount.Checked;

                Model.UpdateTime = DateTime.Now;

                string strINC = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model.ShopClientID = Int32.Parse(strINC);

                Model.Sort = Int32.Parse(Textbox_Sort.Text);



                boolsaveModel = true;
            }
            catch (Exception e)
            {
                boolsaveModel = false;
                debug_Log.Call_WriteLog(e);
            }
            finally
            {

            }

            return boolsaveModel;
        }

    }
}