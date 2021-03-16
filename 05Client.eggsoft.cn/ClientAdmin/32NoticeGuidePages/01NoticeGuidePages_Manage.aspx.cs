using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._32NoticeGuidePages
{
    public partial class _01NoticeGuidePages_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String DisPlayStatus_New_None = "";
        public String strText_Shopping_Vouchers_Start = "";
        public String strText_Shopping_Vouchers_End = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 没有的权限
                if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_32NoticeGuidePages")))
                {
                    Response.Write("<script>window.close()</script>");
                    return;
                }
                #endregion 没有的权限

                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string strb016_NoticeGuidePagesID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strb016_NoticeGuidePagesID))
                        MyError.ThrowException("传递参数错误!");

                    string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                    EggsoftWX.BLL.b016_NoticeGuidePages bllb016_NoticeGuidePages = new EggsoftWX.BLL.b016_NoticeGuidePages();
                    bllb016_NoticeGuidePages.Update("IsDeleted=1,UpdateTime=getdate(),UpdateBy=@UpdateBy", "ID=@ID", strwebuy8_ClientAdmin_Users_ClientUserAccount, strb016_NoticeGuidePagesID);

                    JsUtil.ShowMsg("删除成功!", "01NoticeGuidePagesBoard.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }




        private void SetClass()
        {
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strb016_NoticeGuidePagesID = Request.QueryString["ID"];// 修改ID

                EggsoftWX.BLL.b016_NoticeGuidePages bllb016_NoticeGuidePages = new EggsoftWX.BLL.b016_NoticeGuidePages();
                EggsoftWX.Model.b016_NoticeGuidePages Model = bllb016_NoticeGuidePages.GetModel("ID=" + strb016_NoticeGuidePagesID + "");

                CheckBox_Active.Checked = Model.Active.toBoolean();
                b016_NoticeGuidePages_Title.Text = Model.Title;
                TextBox1_Linkurl.Text = Model.Linkurl;
                Textbox_Pos.Text = Model.Pos.toString();
                btnAdd.Text = "保 存";
            }
            else
            {
                

            }





        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"];


            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            if (type.ToLower() == "modify")
            {
                string strb016_NoticeGuidePagesID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.b016_NoticeGuidePages bllb016_NoticeGuidePages = new EggsoftWX.BLL.b016_NoticeGuidePages();
                EggsoftWX.Model.b016_NoticeGuidePages Model = bllb016_NoticeGuidePages.GetModel("ID=" + strb016_NoticeGuidePagesID + "");

                Model.Active = CheckBox_Active.Checked ;
                Model.Title = b016_NoticeGuidePages_Title.Text;
                Model.Linkurl = TextBox1_Linkurl.Text ;
                Model.Pos = Textbox_Pos.Text.toInt32();
                Model.UpdateTime = DateTime.Now;
                Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                bllb016_NoticeGuidePages.Update(Model);

             

                JsUtil.ShowMsg("修改成功!", "01NoticeGuidePagesBoard.aspx");
            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.BLL.b016_NoticeGuidePages bllb016_NoticeGuidePages = new EggsoftWX.BLL.b016_NoticeGuidePages();
                EggsoftWX.Model.b016_NoticeGuidePages Model =new EggsoftWX.Model.b016_NoticeGuidePages();

                Model.ShopClientID = strShopClientID.toInt32();
                Model.Active = CheckBox_Active.Checked;
                Model.Title = b016_NoticeGuidePages_Title.Text;
                Model.Linkurl = TextBox1_Linkurl.Text;
                Model.Pos = Textbox_Pos.Text.toInt32();
                Model.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                bllb016_NoticeGuidePages.Add(Model);

                JsUtil.ShowMsg("添加成功!", "01NoticeGuidePagesBoard.aspx");
            }
        }
    }
}