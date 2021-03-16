using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class1_Modify : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        string str_Pub_ShopClientID = "";

        EggsoftWX.BLL.tab_Class1 bll = new EggsoftWX.BLL.tab_Class1();
        protected void Page_Load(object sender, EventArgs e)
        {
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            if (!IsPostBack)
            {
                MyInit();
            }
        }

        private void MyInit()
        {
            string strUpLoadURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"];

            int BigClassID = Int32.Parse(Request.QueryString["BigClassID"].ToString());
            EggsoftWX.Model.tab_Class1 bc = bll.GetModel(BigClassID);
            txtBigClassName.Text = bc.ClassName;
            txtBigClassPos.Text = bc.Sort.ToString();

            Image_Nav_PIC_Small.ImageUrl = strUpLoadURL + bc.ClassIcon;
            Image_Nav_PIC_Big.ImageUrl = strUpLoadURL + bc.BigPicpath;

            lblBigClassID.Text = BigClassID.ToString();

        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            int BigClassID = Int32.Parse(Request.QueryString["BigClassID"].ToString());
            EggsoftWX.Model.tab_Class1 bc = bll.GetModel(BigClassID);

            bc.ID = Int32.Parse(lblBigClassID.Text);
            bc.ClassName = txtBigClassName.Text.Trim();
            bc.Sort = Int32.Parse(txtBigClassPos.Text.Trim());

            bc.IsShow = cbIsShow.Checked;
            //判断设为版主的会员帐号是否存在

            bc.Sort = Int32.Parse(txtBigClassPos.Text.Trim());


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
                bc.ClassIcon = strFileImage_Nav_PIC_Small;
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
                bc.BigPicpath = strFileUpload_Nav_PIC_Big;
            }



            bll.Update(bc);
            JsUtil.ShowMsg("修改成功!", "tab_Class_BoardSet.aspx");


        }
    }
}