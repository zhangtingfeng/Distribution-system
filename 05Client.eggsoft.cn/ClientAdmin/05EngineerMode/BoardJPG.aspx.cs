using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class BoardJPG : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String strMange = "";
        List<Eggsoft.Common.FileSort_GetFileList> custormerList = new List<Eggsoft.Common.FileSort_GetFileList>();
        public String SessionReturnURL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_BoardJPG")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            ViewState["IMGpageIndex"] = Request.QueryString["IMGpageIndex"];
            if (!IsPostBack)
            {
                //String strNeedMedia = Request.QueryString["NeedMedia"];
                //if (strNeedMedia != null)
                //{
                //    //String strpubGet_ACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(0,Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform());
                //    //Eggsoft.Common.Session.Add("strpubGet_ACCESS_TOKEN", strpubGet_ACCESS_TOKEN);
                //}
                InitGoPage();
            }
        }
        private static object MyObjectInitGoPage = new object();
        private void InitGoPage()
        {
            lock (MyObjectInitGoPage)
            {
                string strJPGType = Request.QueryString["type"];
                if (strJPGType == "AddNew")///新建图形
                {
                    string UploadMarker = Eggsoft.Common.Session.Read("UploadMarker");
                    if ((UploadMarker == null) || (UploadMarker == ""))
                    {
                        Eggsoft.Common.Session.Add("UploadMarker", "1");

                        String strReturnURL = Eggsoft.Common.Session.Read("ReturnURL");
                        Eggsoft.Common.Session.Add("type", strJPGType);
                        SessionReturnURL = strReturnURL;
                    }
                    else///上传文件的返回 已读过了
                    {
                        SessionReturnURL = Eggsoft.Common.Session.Read("ReturnURL");
                    }
                    Literal_Choice.Text = "<th scope=\"col\" style=\"width:12%;\">选择</th>";
                }
                else if (strJPGType == "ModifySelectThisJPG")///新建图形
                {


                    string UploadMarker = Eggsoft.Common.Session.Read("UploadMarker");
                    if ((UploadMarker == null) || (UploadMarker == ""))
                    {
                        Eggsoft.Common.Session.Add("UploadMarker", "1");

                        String strResourceID = Request.Form["ResourceID"];
                        String strReturnURL = Request.Form["ReturnURL"];
                        Eggsoft.Common.Session.Add("ReturnURL", strReturnURL);
                        Eggsoft.Common.Session.Add("ResourceID", strResourceID);
                        Eggsoft.Common.Session.Add("type", strJPGType);
                        SessionReturnURL = strReturnURL;
                    }
                    else
                    {
                        SessionReturnURL = Eggsoft.Common.Session.Read("ReturnURL");
                    }

                    Literal_Choice.Text = "<th scope=\"col\" style=\"width:12%;\">选择</th>";
                }

                doGetFileList();
            }

        }



        private void doGetFileList()
        {
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
            //string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);


            string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_GetFileList.asmx";
            string[] args = new string[1];
            args[0] = upLoadpath;// "/UpLoad/images/";
            object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "APPCODE_getFileList", args);
            string strresult = result.toString();
            custormerList = Eggsoft.Common.XmlHelper.XmlDeserialize<List<Eggsoft.Common.FileSort_GetFileList>>(strresult, System.Text.Encoding.UTF8);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";


            if (FileUpload1.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                upLoadpath = upLoadpath + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - "/upload/".Length - saveName.Length);
                upl.UploadFile(FileUpload1.FileBytes, saveName, strRemoveUpload, ref eMsg, "");

                // upLoadpath = Eggsoft.Common.FileFolder.btnFileUpload(FileUpload1, "/upload/images/");
            }

            Eggsoft.Common.JsUtil.LocationNewHref(Request.RawUrl);//重新载入本页
        }


        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            int intIMGpageIndex = 0;
            if (ViewState["IMGpageIndex"] != null)
            {
                string strIMGpageIndex = ViewState["IMGpageIndex"].ToString();
                intIMGpageIndex = Convert.ToInt32(strIMGpageIndex);
            }

            #region dogetList()
            AspNetPager1.UrlRewritePattern = "BoardJPG.aspx?IMGpageIndex={0}";

            if (string.IsNullOrEmpty(Request.QueryString["type"]) == false)
            {
                AspNetPager1.UrlRewritePattern += "&type=" + Request.QueryString["type"];
            }
            if (string.IsNullOrEmpty(Request.QueryString["ResourceID"]) == false)
            {
                AspNetPager1.UrlRewritePattern += "&ResourceID=" + Request.QueryString["ResourceID"];
            }
            #endregion
            // custormerList = (Eggsoft.Common.FileFolder.FileSort_GetFileList)strresult;
            //        return result.ToString();


            AspNetPager1.RecordCount = custormerList.Count;
            AspNetPager1.CurrentPageIndex = intIMGpageIndex;

            //int iNUm = 0;
            //string strFilePath = "/upload/" + "images/";
            //string strUpLoadFilePath = "upload\\" + "images\\";
            //string fpath = Server.MapPath(strFilePath);
            int intpageSize = AspNetPager1.PageSize;

            int intStart = 0;
            int intEnd = 0;

            string url = HttpContext.Current.Request.Url.Host;
            intStart = 0 + (AspNetPager1.CurrentPageIndex - 1) * intpageSize;
            intEnd = 0 + (AspNetPager1.CurrentPageIndex) * intpageSize;
            if (intEnd > custormerList.Count) intEnd = custormerList.Count;

            string ResourceID = Request.QueryString["ResourceID"];
            string NeedMedia = Request.QueryString["NeedMedia"];//=INeedPHPUpload

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();



            for (int i = intStart; i < intEnd; i++)
            {
                Eggsoft.Common.FileSort_GetFileList FS = custormerList[i];
                string fname = FS.FileName;

                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                string strfname = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + upLoadpath + fname;

                int sourceWidth = FS.File_Width;
                int sourceHeight = FS.File_Height;
                string strIMG = "";
                try
                {
                    if ((fname.ToLower().IndexOf(".jpg") != -1) || (fname.ToLower().IndexOf(".jpeg") != -1) || (fname.ToLower().IndexOf(".bmp") != -1) || (fname.ToLower().IndexOf(".gif") != -1))
                    {
                        //System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(strfname);
                        //sourceWidth = imgPhoto.Width;
                        //sourceHeight = imgPhoto.Height;
                        strIMG = "<img title=\"" + strfname + "\" id=\"SelectIMG" + (i + 1) + "\" src=\"" + strfname + "\" height=40 />";
                    }
                }
                finally
                {

                }
                //iNUm++;
                strMange += " <tr >\n";
                strMange += "                <td class=\"centerAuto\">" + (i + 1).ToString() + "</td>\n";
                strMange += "                <td class=\"centerAuto\"><a title=\"" + strfname + "\" id=\"FilenameAHref" + (i + 1) + "\" target=\"_blank\" href=\"" + strfname + "\">" + fname + "</a></td>\n";
                strMange += "                <td class=\"centerAuto\">" + FS.FileNameDate + "</td>\n";
                strMange += "                <td class=\"centerAuto\">" + sourceWidth + "*" + sourceHeight + "</td>\n";
                strMange += "                <td class=\"centerAuto\">" + FS.FileSize / 1024 + "K" + "</td>\n";
                strMange += "                <td class=\"centerAuto\">" + strIMG + "</td>\n";
                // strMange += "                <td class=\"centerAuto\"><a href=\"BoardJPG_Manage.aspx?type=Delete&FileName=" + fname + "\" onclick=\"return confirm('删除可能会导致相关的错误，确定删除吗?')\">删除</a></td>\n";

                if ((ResourceID != null) && (ResourceID != ""))
                {
                    if ((NeedMedia != null) && (NeedMedia != ""))
                    {
                        strMange += "                <td class=\"centerAuto\"><IMG title=\"选择并上传到微信服务器," + fname + "" + ResourceID + "\" style=\" width:50px;height:50px; ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='upThisMedia(" + (i + 1) + ")' src=\"Images/SelectJPG.png\"></td>\n";
                    }
                    else
                    {
                        strMange += "                <td class=\"centerAuto\"><IMG title=\"选择并保存," + fname + "" + ResourceID + "\" style=\" width:50px;height:50px; ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='SelectThisJPG(" + (i + 1) + ")' src=\"Images/SelectJPG.png\"></td>\n";
                    }
                }
                strMange += "            </tr>\n";

            }
        }
    }
}