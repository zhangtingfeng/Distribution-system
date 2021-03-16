using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._03AnnouncePic
{
    public partial class AnnouncePic_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
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
                    EggsoftWX.BLL.tab_AnnouncePic bll = new EggsoftWX.BLL.tab_AnnouncePic();

                    EggsoftWX.Model.tab_AnnouncePic Model = bll.GetModel(Int32.Parse(ID));//删除文件
                    Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.PicUrl));//删除文件

                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "BoardAnnouncePic.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void SetClass()
        {

            string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_AnnouncePic bll = new EggsoftWX.BLL.tab_AnnouncePic();
                EggsoftWX.Model.tab_AnnouncePic Model = bll.GetModel(Int32.Parse(ID));

                txtTitle.Text = Model.ShowText;
                Link0.Text = Model.LinkURL;

                ImageButton.Visible = true;
                ImageButton.ImageUrl = strUpLoadURL + Model.PicUrl.Trim();
                txtClassPos.Text = Model.Pos.ToString();
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strID));
                txtTitle.Text = Model.ShopClientName;///默认一个数值
                Link0.Text = "https://" + Model.ErJiYuMing;
            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
            String strFileUpload_Button = "";

            if (FileUpload_Button.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileUpload_Button = upLoadpath + saveName;
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                upl.UploadFile(FileUpload_Button.FileBytes, saveName, strRemoveUpload, ref eMsg, "");

                // if (String.IsNullOrEmpty(strFileUpload_Button) == false) tab_ShopClient_Model.ShopButton = strFileUpload_Button;
            }
            int intpos = 0;
            int.TryParse(txtClassPos.Text, out intpos);

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_AnnouncePic bll = new EggsoftWX.BLL.tab_AnnouncePic();
                EggsoftWX.Model.tab_AnnouncePic Model = bll.GetModel(Int32.Parse(ID));


                //Model.Writer = txtWriter.Text.Trim();
                if (strFileUpload_Button != "")
                {
                    //Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.PicUrl));
                    Model.PicUrl = strFileUpload_Button;
                }
                Model.ShowText = txtTitle.Text.Trim();
                Model.LinkURL = Link0.Text;
                Model.UpdateTime = DateTime.Now;

                Model.Pos = intpos;
                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "BoardAnnouncePic.aspx");

            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_AnnouncePic bll = new EggsoftWX.BLL.tab_AnnouncePic();
                    EggsoftWX.Model.tab_AnnouncePic Announce = new EggsoftWX.Model.tab_AnnouncePic();




                    //Announce.Writer = txtWriter.Text.Trim();
                    Announce.PicUrl = strFileUpload_Button;
                    Announce.ShowText = txtTitle.Text.Trim();
                    Announce.LinkURL = Link0.Text;
                    Announce.UpdateTime = DateTime.Now;
                    Announce.UserID = Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                    Announce.Pos = intpos;

                    bll.Add(Announce);
                    JsUtil.ShowMsg("添加成功!", "BoardAnnouncePic.aspx");

                }



        }
    }
}