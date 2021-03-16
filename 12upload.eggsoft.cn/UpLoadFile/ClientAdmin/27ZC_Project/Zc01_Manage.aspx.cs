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
    public partial class Zc01_Manage1 : Eggsoft.Common.DotAdminPage_ClientAdmin
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
                    string tab_ZC_01ProductID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(tab_ZC_01ProductID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                    blltab_ZC_01Product.Delete(Int32.Parse(tab_ZC_01ProductID));
                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/27ZC_Project/" + strCallBackUrl);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    Init_DropDownList_GoodList();
                    SetClass();
                }
            }
        }

        private void Init_DropDownList_GoodList()
        {

            EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();

            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (myBLL_tab_Goods.Exists(" isSaled=1 and IsDeleted=0 and  ShopClient_ID=" + strShopClient_ID))
            {
                DropDownList_Goods.DataSource = myBLL_tab_Goods.GetDataTable("1000", "ID,Name", " and ShopClient_ID=" + strShopClient_ID + " and isSaled=1 and IsDeleted=0 order by Sort asc,ID asc");
                DropDownList_Goods.DataTextField = "Name";
                DropDownList_Goods.DataValueField = "ID";
                DropDownList_Goods.DataBind();
            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsg("至少先添加一个相关商品", -2);
            }
        }

        private void SetClass()
        {
            string type = Request.QueryString["type"];
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (type.ToLower() == "modify")
            {

                string IDblltab_ZC_01Product = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                EggsoftWX.Model.tab_ZC_01Product Model = blltab_ZC_01Product.GetModel(Int32.Parse(IDblltab_ZC_01Product));


                if (Model.SourceGoodID >= 0)
                {
                    DropDownList_Goods.SelectedValue = Model.SourceGoodID.ToString();
                }

                Textbox_DestinationMoney.Text = ((Decimal)(Model.DestinationPrice)).ToString("###0.00");




                if (Model.IsSales == true)
                    RadioButtonList_IsSaled.SelectedValue = "1";
                else
                    RadioButtonList_IsSaled.SelectedValue = "0";

                TextBox_content_ZCReason.Text = Server.HtmlDecode(Model.ZCReason);
                TextBox_content_ZCPromiseAndReturn.Text = Server.HtmlDecode(Model.ZCPromiseAndReturn);
                TextBox_content_ZCDescribe.Text = Server.HtmlDecode(Model.ZCDescribe);

                txtMenuPos.Text = ((int)(Model.Sort)).ToString();

                try
                {
                    strTextbox_Timer1_Text = ((DateTime)Model.WhenEndAllGroup).ToString("yyyy-MM-dd HH:mm:ss");

                }
                catch
                {
                }
                finally { }


                btnAdd.Text = "保 存";

                //RequiredFieldValidator_GoodIcon.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {

                //string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/images/";
                //string webFilePath = System.Web.HttpContext.Current.Server.MapPath(upLoadpath);
                //Eggsoft.Common.FileFolder.makeFolder(webFilePath);
                //Upload_MultiSeclect2.OnInit("", upLoadpath);

            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                Decimal DestinationMoney = 0;
                Decimal.TryParse(Textbox_DestinationMoney.Text, out DestinationMoney);
                if (DestinationMoney < 0)
                {
                    JsUtil.ShowMsg("众筹金额不能为0", "javascript:history.back();");
                    return;
                }



                if (String.IsNullOrEmpty(TextBox_content_ZCReason.Text))
                {
                    JsUtil.ShowMsg("众筹原因必须填写", "javascript:history.back();");
                    return;
                }

                if (String.IsNullOrEmpty(TextBox_content_ZCPromiseAndReturn.Text))
                {
                    JsUtil.ShowMsg("承诺与回报必须填写", "javascript:history.back();");
                    return;
                }




                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ZC_01ProductID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                    EggsoftWX.Model.tab_ZC_01Product Modeltab_ZC_01Product = blltab_ZC_01Product.GetModel(Int32.Parse(ZC_01ProductID));

                    if (saveModel(Modeltab_ZC_01Product) == false)
                    {
                        return;
                    }

                    blltab_ZC_01Product.Update(Modeltab_ZC_01Product);

                    string strUpdateSourceGoodIDExecuteSql = "update tab_ZC_01Product_Support set SourceGoodID=" + Modeltab_ZC_01Product.SourceGoodID + " where  ShopClientID=" + str_Pub_ShopClientID + " and ZC_01ProductID=" + ZC_01ProductID;
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdateSourceGoodIDExecuteSql);
                    //Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(Model.ChangePicList, Int16.Parse(str_Pub_ShopClientID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/27ZC_Project/" + strCallBackUrl);
                }
                else
                    if (type.ToLower() == "add")
                    {
                        EggsoftWX.BLL.tab_ZC_01Product blltab_ZC_01Product = new EggsoftWX.BLL.tab_ZC_01Product();
                        EggsoftWX.Model.tab_ZC_01Product modeltab_ZC_01Product = new EggsoftWX.Model.tab_ZC_01Product();


                        if (saveModel(modeltab_ZC_01Product) == false)
                        {
                            return;
                        }
                        //model.ChangePicList = dddTextBox_ChangePicList.Text;//轮播图片
                        int inttab_WeiKanJiaID = blltab_ZC_01Product.Add(modeltab_ZC_01Product);

                        //Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(model.ChangePicList, Int16.Parse(str_Pub_ShopClientID));

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        if (String.IsNullOrEmpty(strCallBackUrl) == true)
                        {
                            strCallBackUrl = "Board_01ZC_Project.aspx";
                        }
                        else
                        {
                            strCallBackUrl = strCallBackUrl.Replace("*", "?");
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


        private bool saveModel(EggsoftWX.Model.tab_ZC_01Product Model)
        {
            bool boolsaveModel = false;
            try
            {
                if ((DropDownList_Goods.SelectedIndex != -1) && (DropDownList_Goods.SelectedIndex != null))
                {
                    if (string.IsNullOrEmpty(DropDownList_Goods.SelectedValue) == false)
                    {
                        Model.SourceGoodID = Convert.ToInt32(DropDownList_Goods.SelectedValue.ToString());
                    }
                }
                Model.DestinationPrice = Convert.ToDecimal(Textbox_DestinationMoney.Text);
                #region 终止时间
                string strText_SecondBuyEnd = Request.Form["Text_SecondBuyEndWhenEndAllGroup"];
                DateTime myWhenEndAllGroup2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                Model.WhenEndAllGroup = myWhenEndAllGroup2datetime;
                #endregion

                Model.IsSales = (RadioButtonList_IsSaled.SelectedValue.ToString() == "1");

                Model.ZCReason = Server.HtmlEncode(TextBox_content_ZCReason.Text);
                Model.ZCPromiseAndReturn = Server.HtmlEncode(TextBox_content_ZCPromiseAndReturn.Text);
                Model.ZCDescribe = Server.HtmlEncode(TextBox_content_ZCDescribe.Text);
                Model.Sort = Int32.Parse(txtMenuPos.Text);
                Model.UpdateTime = DateTime.Now;

                string strINC = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model.ShopClientID = Int32.Parse(strINC);




                boolsaveModel = true;
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
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

        protected void TextBox_content_ZCReason_Init(object sender, EventArgs e)
        {
            TextBox_content_ZCReason.Height = 547;
        }

        protected void TextBox_content_ZCPromiseAndReturn_Init(object sender, EventArgs e)
        {
            TextBox_content_ZCPromiseAndReturn.Height = 547;
        }

        protected void TextBox_content_ZCDescribe_Init(object sender, EventArgs e)
        {
            TextBox_content_ZCDescribe.Height = 547;
        }
    }
}