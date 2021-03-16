using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.Control.UploadControl
{
    public partial class Default : System.Web.UI.Page
    {
        private int Pages = 1;
        public string BaseUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //BaseUrl = "/upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";
            //Upload_Path=<%=public_Upload_Path%>
            BaseUrl = Request.QueryString["Upload_Path"];
            if (!IsPostBack)
            {
                ViewState["page"] = Pages;
                ViewState["pagesCount"] = 1;
                loadImglist(Pages);
            }
        }

        public String getPath(String strFilename)
        {
            BaseUrl = Request.QueryString["Upload_Path"];
            // string BaseUrl = "/upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";
            return BaseUrl + strFilename;
        }

        public void loadImglist(int CurrentPage)
        {
            try
            {
                string strServer_MapPath = Server.MapPath(BaseUrl);
                DirectoryInfo dirInfo = new DirectoryInfo(strServer_MapPath);
                FileInfo[] lstFile = dirInfo.GetFiles();
                Array.Sort(lstFile, new Eggsoft.Common.FileSort(Eggsoft.Common.FileOrder.LastAccessTime)); //按修改日期升序排列
                //foreach (FileInfo file in lstFile)
                //         Console.WriteLine(file.Name);

                int filecount = 0, countperpage = 40;
                if (lstFile != null && lstFile.Length > 0)
                {
                    filecount = lstFile.Length;
                    int pagesCount = 1;
                    int Count = 0;
                    int ArrayCount = 0;
                    if (filecount % countperpage == 0)
                    {
                        pagesCount = filecount / countperpage;
                    }
                    else
                    {
                        pagesCount = filecount / countperpage + 1;
                    }
                    Count = CurrentPage >= pagesCount ? filecount : CurrentPage * countperpage;
                    CurrentPage = CurrentPage >= pagesCount ? pagesCount : CurrentPage;
                    ViewState["page"] = CurrentPage;
                    ArrayCount = Count - ((CurrentPage - 1) * countperpage);
                    ViewState["pagesCount"] = pagesCount;
                    FileInfo[] pageFiles = new FileInfo[ArrayCount];
                    for (int j = (CurrentPage - 1) * countperpage; j < Count; j++)
                    {
                        pageFiles[j % countperpage] = lstFile[j];
                    }
                    DataList1.DataSource = pageFiles;
                    DataList1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally
            {

            }

            //DirectoryInfo imagesfile = new DirectoryInfo(Server.MapPath("/upload/images"));
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            loadImglist(Convert.ToInt32(ViewState["page"]));
            Timer1.Enabled = false;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            int pagesCount = Convert.ToInt32(ViewState["pagesCount"]) + 1;
            ViewState["page"] = pagesCount;
            loadImglist(pagesCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            ViewState["page"] = 1;
            loadImglist(1);
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int C_Pages = Convert.ToInt32(ViewState["page"]);
            int pagesCount = Convert.ToInt32(ViewState["pagesCount"]);
            if (C_Pages > 1)
            {
                C_Pages = C_Pages - 1;
            }
            ViewState["page"] = C_Pages;
            loadImglist(C_Pages);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            int C_Pages = Convert.ToInt32(ViewState["page"]);
            int pagesCount = Convert.ToInt32(ViewState["pagesCount"]);
            if (C_Pages < pagesCount)
            {
                C_Pages = C_Pages + 1;
            }
            ViewState["page"] = C_Pages;
            loadImglist(C_Pages);
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            int pagesCount = Convert.ToInt32(ViewState["pagesCount"]);
            ViewState["page"] = pagesCount;
            loadImglist(pagesCount);
        }
    }
}