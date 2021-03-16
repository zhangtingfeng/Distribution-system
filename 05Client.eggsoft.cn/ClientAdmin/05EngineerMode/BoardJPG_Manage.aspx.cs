using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode._05EngineerMode
{
    public partial class BoardJPG_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strFilePath = "";

        //EggsoftWX.BLL.Net_BigClass bll = new EggsoftWX.BLL.Net_BigClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
            string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);

            strFilePath = strRemoveUpload;// "/upload/" + "images/";

            if (!IsPostBack)
            {

                MyInit();
            }
        }

        private void MyInit()
        {


            string type = Request.QueryString["type"];

            if (type.ToLower() == "delete")
            {
                string FileName = Request.QueryString["FileName"];

                Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(strFilePath + FileName));//删除文件


                string UploadMarker = Eggsoft.Common.Session.Read("UploadMarker");
                if (UploadMarker != "")
                {
                    JsUtil.ShowMsg("删除成功!", "BoardJPG.aspx?type=" + Eggsoft.Common.Session.Read("type"));
                }
                else
                {
                    JsUtil.ShowMsg("删除成功!", "BoardJPG.aspx");
                }
            }
        }

    }
}