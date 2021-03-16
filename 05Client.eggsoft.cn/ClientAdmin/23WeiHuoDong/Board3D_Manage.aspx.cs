using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong
{
    public partial class Board3D_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strFilePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            strFilePath = "/Upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";

            if (!IsPostBack)
            {

                MyInit();
            }
        }

        private void MyInit()
        {


            string type = Request.QueryString["Type"];

            if (type.ToLower() == "delete")
            {
                string FileName = Request.QueryString["FileName"];
                EggsoftWX.BLL.tab_ShopClient_Net_3D bll = new EggsoftWX.BLL.tab_ShopClient_Net_3D();
                EggsoftWX.Model.tab_ShopClient_Net_3D Model = bll.GetModel("Filename='" + strFilePath + FileName + "'");

                Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.Filename));//删除文件
                Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.SMALL150PX));//删除文件

                bll.Delete("Filename='" + strFilePath + FileName + "'");


                JsUtil.ShowMsg("删除成功!", "Board3D.aspx");
            }
            else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
            {
                SetClass();
            }
        }

        private void SetClass()
        {

            string FileName = Request.QueryString["FileName"];

            string type = Request.QueryString["Type"];
            if (type.ToLower() == "modify")
            {
                EggsoftWX.BLL.tab_ShopClient_Net_3D bll = new EggsoftWX.BLL.tab_ShopClient_Net_3D();
                EggsoftWX.Model.tab_ShopClient_Net_3D Model = bll.GetModel("Filename='" + strFilePath + FileName + "'");


                cbIsShow.Checked = Model.selectIF;
                txtPos.Text = Model.ShowPos.ToString();
                //txt3D_Info.Text = Model.ShortInfo.ToString();
                btnModify.Text = "保 存";

                //RequiredFieldValidatorFileUpload3D.Enabled = false;

            }
        }




        protected void btnModify_Click(object sender, EventArgs e)
        {
            string FileName = Request.QueryString["FileName"];
            EggsoftWX.BLL.tab_ShopClient_Net_3D bll = new EggsoftWX.BLL.tab_ShopClient_Net_3D();
            EggsoftWX.Model.tab_ShopClient_Net_3D Model = bll.GetModel("Filename='" + strFilePath + FileName + "'");

            if (cbIsShow.Checked)
            {
                try
                {

                    Model.selectIF = true;

                    string str150px = Eggsoft.Common.FileFolder.WriteFile150px(strFilePath + FileName);
                    Model.SMALL150PX = str150px;
                }
                catch
                {
                    Eggsoft.Common.JsUtil.ShowMsg("错误代码201403041137，可能是文件不存在！");
                }
                finally
                {
                }
            }
            else
            {
                Model.selectIF = false;
            }

            if (Convert.ToInt64(txtPos.Text) > 10000)
            {
                JsUtil.ShowMsg("太大的数字吧，请确认!", "javascript:history.back()");
            }
            else

                Model.ShowPos = Convert.ToInt32(txtPos.Text);

            bll.Update(Model);
            JsUtil.ShowMsg("修改成功!", "Board3D.aspx");



        }
    }
}