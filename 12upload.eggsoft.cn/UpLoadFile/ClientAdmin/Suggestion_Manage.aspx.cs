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
    public partial class Suggestion_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin//System.Web.UI.Page//
    {
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





        private void SetClass()
        {
            string type = Request.QueryString["type"];


            #region 是否启用购物券功能
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
            Model_tab_ShopClient = bll_tab_ShopClient.GetModel(Int32.Parse(strINCID));

            #endregion
            if (type.ToLower() == "modify")
            {

                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model = bll.GetModel(Int32.Parse(ID));




                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

                txtName.Text = Model.Name;




                //txtContent_ShortInfo.Text = Server.HtmlDecode(Model.ShortInfo);
                txtContent_LongInfo.Text = Server.HtmlDecode(Model.LongInfo);


                btnAdd.Text = "保 存";

                //RequiredFieldValidator_GoodIcon.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {

                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                string webFilePath = System.Web.HttpContext.Current.Server.MapPath(upLoadpath);
                Eggsoft.Common.FileFolder.makeFolder(webFilePath);

            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model = bll.GetModel(Int32.Parse(ID));
                    //Model.Webuy8_DistributionMoney_Value = Int32.Parse(DropDownList_FenXiaoMoney.SelectedValue);

                    Model = saveModel(Model);


                    //Model = saveXMLModel(Model);
                    if (Model == null)
                    {
                        return;
                    }

                    bll.Update(Model);
                    //Eggsoft_Public_CL.GoodP.tell_Admin_Need_Watch_Good_ByEmail_Thread(Int16.Parse(ID));



                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                    JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/" + strCallBackUrl);


                }
                else
                    if (type.ToLower() == "add")
                    {
                        EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();
                        EggsoftWX.Model.tab_Goods model = new EggsoftWX.Model.tab_Goods();

                        //debug_Log.Call_WriteLog("model = saveModel(model)");
                        //model.Webuy8_DistributionMoney_Value = Int32.Parse(DropDownList_FenXiaoMoney.SelectedValue);

                        model = saveModel(model);
                        //debug_Log.Call_WriteLog("model = saveXMLModel(model)");
                        //model = saveXMLModel(model);
                        //debug_Log.Call_WriteLog("After model = saveXMLModel(model)");

                        if (model == null)
                        {
                            return;
                        }

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
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog("saveModel:" + Exceptione.ToString());
            }
            finally
            {

            }
        }


        private EggsoftWX.Model.tab_Goods saveModel(EggsoftWX.Model.tab_Goods Model)
        {
            try
            {

                //  string strContent = Request.Form["Text1"].ToString();


                Model.Name = txtName.Text.Trim();

                Model.LongInfo = Server.HtmlEncode(txtContent_LongInfo.Text);

                Model.UpdateTime = DateTime.Now;



            }
            catch (Exception e)
            {
                debug_Log.Call_WriteLog("saveModel:" + e.ToString());
            }
            finally
            {

            }

            return Model;
        }


        protected void txtContent_Init(object sender, EventArgs e)
        {
            //txtContent_ShortInfo.Height = 347;
            txtContent_LongInfo.Height = 547;
        }
    }
}