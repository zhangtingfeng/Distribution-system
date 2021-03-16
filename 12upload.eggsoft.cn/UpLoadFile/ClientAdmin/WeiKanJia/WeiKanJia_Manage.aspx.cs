using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WeiKanJia
{
    public partial class WeiKanJia_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strTextbox_Timer1_Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_WeiKanJia bll = new EggsoftWX.BLL.tab_WeiKanJia();

                    //EggsoftWX.Model.tab_WeiKanJia Model = bll.GetModel(Int32.Parse(ID));//删除文件

                    bll.Delete(Int32.Parse(ID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/24WeiKanJian/" + strCallBackUrl);
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

                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_WeiKanJia bll = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia Model = bll.GetModel(Int32.Parse(ID));


                if (Model.GoodID >= 0)
                {
                    DropDownList_Goods.SelectedValue = Model.GoodID.ToString();
                }

              



                txtNameTopic.Text = Model.Topic;
                Textbox_StartPrice.Text = ((Decimal)(Model.StartPrice)).ToString("###0.00");
                TextboxEndPrice.Text = ((Decimal)(Model.EndPrice)).ToString("###0.00");
                TextboxEachAction_HighPrice.Text = ((Decimal)(Model.EachAction_HighPrice)).ToString("###0.00");
                TextboxEachAction_LowPrice.Text = ((Decimal)(Model.EachAction_LowPrice)).ToString("###0.00");

                CheckBox_MustSubscribe_Master.Checked = (bool)Model.MustSubscribe_Master;
                CheckBox_MustSubscribe_Helper.Checked = (bool)Model.MustSubscribe_Helper;
                CheckBox_MustAddress_Master.Checked = (bool)Model.MustAddress_Master;

              
                CheckBox_Agent.Checked = (bool)Model.MustSubscribe_Agent;

                if (Model.isSaled == true)
                    RadioButtonList_IsSaled.SelectedValue = "1";
                else
                    RadioButtonList_IsSaled.SelectedValue = "0";

                TextBox_KanJiaTopicDescContent.Text = Server.HtmlDecode(Model.KanJiaTopicDescContent);
                TextBox_Content_KanJiaRule.Text = Server.HtmlDecode(Model.KanJiaRule);


                try
                {


                    strTextbox_Timer1_Text = ((DateTime)Model.EndTime).ToString("yyyy-MM-dd HH:mm:ss");

                }
                catch
                { }
                finally { }
                //}
                // Textbox_ContactMan.Text=Model.ContactMan;


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

                Decimal StartPrice = 0;
                Decimal EndPrice = 0;
                Decimal.TryParse(Textbox_StartPrice.Text, out StartPrice);
                Decimal.TryParse(TextboxEndPrice.Text, out EndPrice);
                if (EndPrice > StartPrice)
                {
                    JsUtil.ShowMsg("最低成交价格不能超过起始价格", "javascript:history.back();");
                    return;
                }

                Decimal HighPrice = 0;
                Decimal LowPrice = 0;
                Decimal.TryParse(TextboxEachAction_HighPrice.Text, out HighPrice);
                Decimal.TryParse(TextboxEachAction_LowPrice.Text, out LowPrice);
                if (LowPrice > HighPrice)
                {
                    JsUtil.ShowMsg("每次砍价随机的最低价格不能超过每次砍价随机的最高价格", "javascript:history.back();");
                    return;
                }

                //TextBox dddTextBox_ChangePicList = Upload_MultiSeclect2.FindControl("TextBox_txtReturnValue") as TextBox;
                //if (string.IsNullOrEmpty(dddTextBox_ChangePicList.Text))
                //{
                //    JsUtil.ShowMsg("商品图片必须选择", "javascript:history.back();");
                //    return;
                //}

                if (String.IsNullOrEmpty(TextBox_KanJiaTopicDescContent.Text))
                {
                    JsUtil.ShowMsg("砍价商品详细描述必须填写", "javascript:history.back();");
                    return;
                }
                if (String.IsNullOrEmpty(TextBox_Content_KanJiaRule.Text))
                {
                    JsUtil.ShowMsg("砍价发起及参与规则详细描述必须填写", "javascript:history.back();");
                    return;
                }


                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_WeiKanJia bll = new EggsoftWX.BLL.tab_WeiKanJia();
                    EggsoftWX.Model.tab_WeiKanJia Model = bll.GetModel(Int32.Parse(ID));

                    if (saveModel(Model) == false)
                    {
                        return;
                    }
                    //Model.ChangePicList = dddTextBox_ChangePicList.Text;
                    bll.Update(Model);

                    //Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(Model.ChangePicList, Int16.Parse(str_Pub_ShopClientID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/24WeiKanJian/" + strCallBackUrl);
                }
                else
                    if (type.ToLower() == "add")
                    {
                        EggsoftWX.BLL.tab_WeiKanJia bll = new EggsoftWX.BLL.tab_WeiKanJia();
                        EggsoftWX.Model.tab_WeiKanJia model = new EggsoftWX.Model.tab_WeiKanJia();


                        if (saveModel(model) == false)
                        {
                            return;
                        }
                        //model.ChangePicList = dddTextBox_ChangePicList.Text;//轮播图片
                        int inttab_WeiKanJiaID = bll.Add(model);

                        //Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(model.ChangePicList, Int16.Parse(str_Pub_ShopClientID));

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        if (String.IsNullOrEmpty(strCallBackUrl) == true)
                        {
                            strCallBackUrl = "Board_WeiKanJian.aspx";
                        }
                        else
                        {
                            strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        }
                        JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/24WeiKanJian/" + strCallBackUrl);
                    }
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog("saveModel:" + Exceptione.ToString());
            }
            finally
            {

            }
        }


        private bool saveModel(EggsoftWX.Model.tab_WeiKanJia Model)
        {
            bool boolsaveModel = false;
            try
            {

                Model.Topic = txtNameTopic.Text.Trim();
                Model.StartPrice = Convert.ToDecimal(Textbox_StartPrice.Text);
                Model.EndPrice = Convert.ToDecimal(TextboxEndPrice.Text);
                Model.EachAction_LowPrice = Convert.ToDecimal(TextboxEachAction_LowPrice.Text);
                Model.EachAction_HighPrice = Convert.ToDecimal(TextboxEachAction_HighPrice.Text);

                Model.MustSubscribe_Master = CheckBox_MustSubscribe_Master.Checked;
                Model.MustSubscribe_Helper = CheckBox_MustSubscribe_Helper.Checked;
                Model.MustAddress_Master = CheckBox_MustAddress_Master.Checked;
                Model.MustSubscribe_Agent = CheckBox_Agent.Checked;

                Model.isSaled = (RadioButtonList_IsSaled.SelectedValue.ToString() == "1");

                Model.KanJiaTopicDescContent = Server.HtmlEncode(TextBox_KanJiaTopicDescContent.Text);
                Model.KanJiaRule = Server.HtmlEncode(TextBox_Content_KanJiaRule.Text);
                Model.Sort = Int32.Parse(txtMenuPos.Text);
                Model.UpdateTime = DateTime.Now;


                if ((DropDownList_Goods.SelectedIndex != -1) && (DropDownList_Goods.SelectedIndex != null))
                {
                    if (string.IsNullOrEmpty(DropDownList_Goods.SelectedValue) == false)
                    {
                        Model.GoodID = Convert.ToInt32(DropDownList_Goods.SelectedValue.ToString());
                    }
                }

                string strINC = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model.ShopClientID = Int32.Parse(strINC);


                #region 终止时间

                string strText_SecondBuyEnd = Request.Form["Text-SecondBuyEnd"];
                DateTime my2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                Model.EndTime = my2datetime;
                #endregion

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

        protected void txtContent_InitTopicDescContent(object sender, EventArgs e)
        {
            //txtContent_ShortInfo.Height = 347;
            TextBox_KanJiaTopicDescContent.Height = 547;
        }

        protected void txtContent_KanJiaRule(object sender, EventArgs e)
        {
            //txtContent_ShortInfo.Height = 347;
            TextBox_Content_KanJiaRule.Height = 547;
        }
    }
}