using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class2_Modify : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        string str_Pub_ShopClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (!IsPostBack)
            {
                int SmallClassID = Int32.Parse(Request.QueryString["SmallClassID"].ToString());
                EggsoftWX.BLL.tab_Class2 bll = new EggsoftWX.BLL.tab_Class2();
                EggsoftWX.Model.tab_Class2 sc = bll.GetModel(SmallClassID);

                lblID.Text = SmallClassID.ToString();
                txtSmallClassName.Text = sc.ClassName;
                txtSmallClassPos.Text = sc.Sort.ToString();
                //cbIsShow.Checked = sc.IsShow;


                string strUpLoadURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"];
                Image_Nav_PIC_Small.ImageUrl = strUpLoadURL + sc.ClassIcon;
                Image_Nav_PIC_Big.ImageUrl = strUpLoadURL + sc.BigPicpath;

                InitBigClass(sc.Class1_ID);
            }
        }

        private void InitBigClass(int? p)
        {
            EggsoftWX.BLL.tab_Class1 bll = new EggsoftWX.BLL.tab_Class1();
            ddlBigClass.DataTextField = "ClassName";
            ddlBigClass.DataValueField = "ID";
            ddlBigClass.DataSource = bll.GetList("*", "ShopClientID=" + str_Pub_ShopClientID + " order by Sort asc");
            ddlBigClass.DataBind();
            ddlBigClass.SelectedValue = p.ToString();
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            int SmallClassID = Int32.Parse(Request.QueryString["SmallClassID"].ToString());
            EggsoftWX.BLL.tab_Class2 bll = new EggsoftWX.BLL.tab_Class2();
            EggsoftWX.Model.tab_Class2 sc = bll.GetModel(SmallClassID);

            sc.Class1_ID = Int32.Parse(ddlBigClass.SelectedValue);
            sc.ClassName = txtSmallClassName.Text.Trim();
            sc.Sort = Int32.Parse(txtSmallClassPos.Text.Trim());

            sc.IsShow = cbIsShow.Checked;
            sc.IsLock = cbIsLock.Checked;
            sc.ID = Int32.Parse(lblID.Text);
            string upLoadpathimages = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/images/";
            String strFileImage_Nav_PIC_Small = "";
            if (FileUpload_Nav_PIC_Small.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileImage_Nav_PIC_Small = upLoadpathimages + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpathimages.Substring("/upload/".Length, upLoadpathimages.Length - 1 - "/upload/".Length);
                upl.UploadFile(FileUpload_Nav_PIC_Small.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
                sc.ClassIcon = strFileImage_Nav_PIC_Small;
            }

            String strFileUpload_Nav_PIC_Big = "";
            if (FileUpload_Nav_PIC_Big.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileUpload_Nav_PIC_Big = upLoadpathimages + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpathimages.Substring("/upload/".Length, upLoadpathimages.Length - 1 - "/upload/".Length);
                upl.UploadFile(FileUpload_Nav_PIC_Big.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
                sc.BigPicpath = strFileUpload_Nav_PIC_Big;
            }

            bll.Update(sc);
            JsUtil.ShowMsg("修改成功!", "tab_Class_BoardSet.aspx");
        }
    }
}