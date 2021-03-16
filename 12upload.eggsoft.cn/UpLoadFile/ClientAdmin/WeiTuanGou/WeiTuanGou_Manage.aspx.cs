using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WeiTuanGou
{
    public partial class WeiTuanGou_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
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
                    string TuanGouID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(TuanGouID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_TuanGou blltab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();


                    blltab_TuanGou.Delete(Int32.Parse(TuanGouID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/26WeiTuanGou/" + strCallBackUrl);
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

                string IDTuanGou = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_TuanGou blltab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou Model = blltab_TuanGou.GetModel(Int32.Parse(IDTuanGou));


                if (Model.SourceGoodID >= 0)
                {
                    DropDownList_Goods.SelectedValue = Model.SourceGoodID.ToString();
                }

                //string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/images/";
                //Upload_MultiSeclect2.OnInit(Model.ChangePicList, upLoadpath);




                //txtNameTopic.Text = Model.Topic;
                Textbox_EachPeoplePrice.Text = ((Decimal)(Model.EachPeoplePrice)).ToString("###0.00");
                TextboxAgentPrice.Text = ((Decimal)(Model.AgentPrice)).ToString("###0.00");

                try
                {
                    CheckBox_TuanZhang_AgentGet.Checked = (bool)Model.TuanZhangBonus_AgentGet;
                    Textbox1_TuanZhang_GouWuQuan.Text = ((Decimal)(Model.TuanZhangBonus_GouWuQuan)).ToString("###0.00");
                    Textbox2_TuanZhang_Money.Text = ((Decimal)(Model.TuanZhangBonus_Money)).ToString("###0.00");
                }
                catch { 
                    
                }
                Textbox_HowManyPeople.Text = ((int)(Model.HowManyPeople)).ToString();

                CheckBox_MustSubscribe_Master.Checked = (bool)Model.MustSubscribe_Master;
                CheckBox_MustSubscribe_Helper.Checked = (bool)Model.MustSubscribe_Helper;
                CheckBox_MustAddress_Master.Checked = (bool)Model.MustAddress_Master;
                CheckBox_Agent.Checked = (bool)Model.MustAgent_Master;
                CheckBox_BuyMultiOnlyOneAccount.Checked = (bool)Model.BuyMultiOnlyOneAccount;

                CheckBox_HowmanyHoursEnd.Checked = (bool)Model.ChoiceMaxTimeLengthDoGroup;
                CheckBoxWhenEndAllGroup.Checked = (bool)Model.ChoiceWhenEndAllGroup;
                Textbox_HowmanyHoursEnd.Text = Model.MaxTimeLengthDoGroup.ToString();


                if (Model.IsSales == true)
                    RadioButtonList_IsSaled.SelectedValue = "1";
                else
                    RadioButtonList_IsSaled.SelectedValue = "0";

                TextBox_Content_TuanFouRule.Text = Server.HtmlDecode(Model.TuanFouRule);

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

                Decimal EachPeoplePrice = 0;
                Decimal AgentPrice = 0;
                Decimal.TryParse(Textbox_EachPeoplePrice.Text, out EachPeoplePrice);
                Decimal.TryParse(TextboxAgentPrice.Text, out AgentPrice);
                if (AgentPrice > EachPeoplePrice)
                {
                    JsUtil.ShowMsg("最低成交价格不能小于代理商利润", "javascript:history.back();");
                    return;
                }

                Decimal HowmanyHoursEnd = 0;

                Decimal.TryParse(Textbox_HowmanyHoursEnd.Text, out HowmanyHoursEnd);

                if (HowmanyHoursEnd < 1)
                {
                    JsUtil.ShowMsg("开团多少小时后自动结束不能低于一小时", "javascript:history.back();");
                    return;
                }




                if (String.IsNullOrEmpty(TextBox_Content_TuanFouRule.Text))
                {
                    JsUtil.ShowMsg("团购描述必须填写", "javascript:history.back();");
                    return;
                }



                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_TuanGou bll = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou Model = bll.GetModel(Int32.Parse(ID));

                    if (saveModel(Model) == false)
                    {
                        return;
                    }
                    //Model.ChangePicList = dddTextBox_ChangePicList.Text;
                    bll.Update(Model);

                    //Eggsoft_Public_CL.GoodP.APPCODE_saveOtherImage_Force(Model.ChangePicList, Int16.Parse(str_Pub_ShopClientID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/26WeiTuanGou/" + strCallBackUrl);
                }
                else
                    if (type.ToLower() == "add")
                    {
                        EggsoftWX.BLL.tab_TuanGou bll = new EggsoftWX.BLL.tab_TuanGou();
                        EggsoftWX.Model.tab_TuanGou model = new EggsoftWX.Model.tab_TuanGou();


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
                            strCallBackUrl = "Board_WeiTuanGou.aspx";
                        }
                        else
                        {
                            strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        }
                        JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/26WeiTuanGou/" + strCallBackUrl);
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


        private bool saveModel(EggsoftWX.Model.tab_TuanGou Model)
        {
            bool boolsaveModel = false;
            try
            {

                Model.EachPeoplePrice = Convert.ToDecimal(Textbox_EachPeoplePrice.Text);
                Model.AgentPrice = Convert.ToDecimal(TextboxAgentPrice.Text);

                Model.TuanZhangBonus_AgentGet = CheckBox_TuanZhang_AgentGet.Checked;
                Model.TuanZhangBonus_GouWuQuan = Convert.ToDecimal(Textbox1_TuanZhang_GouWuQuan.Text);
                Model.TuanZhangBonus_Money = Convert.ToDecimal(Textbox2_TuanZhang_Money.Text);


                Model.HowManyPeople = Convert.ToInt32(Textbox_HowManyPeople.Text);
                //Model.EachAction_HighPrice = Convert.ToDecimal(TextboxEachAction_HighPrice.Text);

                Model.MustSubscribe_Master = CheckBox_MustSubscribe_Master.Checked;
                Model.MustSubscribe_Helper = CheckBox_MustSubscribe_Helper.Checked;
                Model.MustAddress_Master = CheckBox_MustAddress_Master.Checked;
                Model.MustAgent_Master = CheckBox_Agent.Checked;
                Model.ChoiceMaxTimeLengthDoGroup = CheckBox_HowmanyHoursEnd.Checked;
                Model.ChoiceWhenEndAllGroup = CheckBoxWhenEndAllGroup.Checked;
                Model.BuyMultiOnlyOneAccount = CheckBox_BuyMultiOnlyOneAccount.Checked;

                Model.MaxTimeLengthDoGroup = Int32.Parse(Textbox_HowmanyHoursEnd.Text);


                Model.IsSales = (RadioButtonList_IsSaled.SelectedValue.ToString() == "1");

                Model.TuanFouRule = Server.HtmlEncode(TextBox_Content_TuanFouRule.Text);
                //Model.KanJiaRule = Server.HtmlEncode(TextBox_Content_KanJiaRule.Text);
                Model.Sort = Int32.Parse(txtMenuPos.Text);
                Model.UpdateTime = DateTime.Now;


                if ((DropDownList_Goods.SelectedIndex != -1) && (DropDownList_Goods.SelectedIndex != null))
                {
                    if (string.IsNullOrEmpty(DropDownList_Goods.SelectedValue) == false)
                    {
                        Model.SourceGoodID = Convert.ToInt32(DropDownList_Goods.SelectedValue.ToString());
                    }
                }

                string strINC = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Model.ShopClientID = Int32.Parse(strINC);


                #region 终止时间
                string strText_SecondBuyEnd = Request.Form["Text_SecondBuyEndWhenEndAllGroup"];
                DateTime myWhenEndAllGroup2datetime = DateTime.ParseExact(strText_SecondBuyEnd, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                Model.WhenEndAllGroup = myWhenEndAllGroup2datetime;
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

        protected void txtContent_TuanFouRule(object sender, EventArgs e)
        {
            //txtContent_ShortInfo.Height = 347;
            TextBox_Content_TuanFouRule.Height = 547;
        }

        protected void TextBox_Content_TuanFouRule_TextChanged(object sender, EventArgs e)
        {

        }

        //protected void txtContent_KanJiaRule(object sender, EventArgs e)
        //{
        //    //txtContent_ShortInfo.Height = 347;
        //    TextBox_Content_KanJiaRule.Height = 547;
        //}
    }
}