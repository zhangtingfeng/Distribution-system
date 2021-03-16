using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.GuidePages
{
    public partial class GuidePages__Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        public String MenuLink = "";
        string str_Pub_ClientApp ="";
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        //private bool boolOnlyDO = false;
        EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");
            EggsoftWX.BLL.tab_ShopClient blltab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Modeltab_ShopClient = blltab_ShopClient.GetModel(Int32.Parse(strShopClient_ID));
            string strErJiYuMing = Modeltab_ShopClient.ErJiYuMing;///默认一个数值
            str_Pub_ClientApp = "https://" + strErJiYuMing;

            if (!IsPostBack)
            {
                LinkShow.Visible = false;
                TextShow.Visible = true;
                RadioButton1.Checked = true;
                RadioButton2.Checked = false;
                BindAnnounce();
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    try
                    {
                        string ID = Request.QueryString["ID"];
                        if (!CommUtil.IsNumStr(ID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();

                        string strJumpURL = getParentIDRestring(ID);
                        bll.Delete(Int32.Parse(ID));
                        JsUtil.LocationNewHref(strJumpURL);
                        HttpContext.Current.Response.End();
                    }
                    catch (Exception Exceptione)
                    {
                        debug_Log.Call_WriteLog("saveModel:" + Exceptione.ToString(), "咨询管理");
                    }
                    finally
                    {

                    }
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
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages Model = bll.GetModel(Int32.Parse(ID));

                txtTitle.Text = Model.MenuName;
                txtLink.Text = Model.MenuLink;

                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                Upload_MultiSeclect2.OnInit(Model.MenuIcon, upLoadpath);

                txtMenuPos.Text = Model.MenuPos.ToString();
                //txtWriter.Text = Model.Author;

                txtContent.Text = Server.HtmlDecode(Model.MenuText);
                if (Model.LinkOrText.toBoolean())
                {
                    RadioButton1.Checked = false;
                    RadioButton2.Checked = true;
                    LinkShow.Visible = true;
                    TextShow.Visible = false;

                   // btnAddClass.Visible = false;
                    //gvMenu.Visible = false;
                }
                MenuLink = str_Pub_ClientApp + "/guidepages-" + ID + ".aspx";
                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {
                txtMenuPos.Text = "0";
                btnAddClass.Visible = false;
                gvMenu.Visible = false;


                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";
                string webFilePath = System.Web.HttpContext.Current.Server.MapPath(upLoadpath);
                Eggsoft.Common.FileFolder.makeFolder(webFilePath);
                Upload_MultiSeclect2.OnInit("", upLoadpath);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            string ParentID = Request.QueryString["ParentID"];

            TextBox dddTextBox = Upload_MultiSeclect2.FindControl("TextBox_txtReturnValue") as TextBox;
            if (string.IsNullOrEmpty(dddTextBox.Text))
            {
                JsUtil.ShowMsg("图片必须选择", "javascript:history.back();");
                return;
            }

            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages Model = bll.GetModel(Int32.Parse(ID));


                Model.MenuPos = Convert.ToInt32(txtMenuPos.Text);
                //Model. = txtWriter.Text; 


                Model.MenuName = txtTitle.Text;
                Model.MenuLink = txtLink.Text;
                Model.MenuText = Server.HtmlEncode(txtContent.Text);
                Model.UpdateTime = DateTime.Now;
                if (RadioButton1.Checked == true)
                {
                    Model.LinkOrText = false;
                }
                else
                {
                    Model.LinkOrText = true;
                }

                Model.MenuIcon = dddTextBox.Text;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", getParentIDRestring(ID));
            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.BLL.tab_ShopClient_GuidePages bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages Model = new EggsoftWX.Model.tab_ShopClient_GuidePages();

                if (RadioButton1.Checked == true)
                {
                    Model.LinkOrText = false;
                }
                else
                {
                    Model.LinkOrText = true;
                }
                Model.MenuPos = Convert.ToInt32(txtMenuPos.Text);

                Model.ID = bll.GetMaxId();
                Model.MenuName = txtTitle.Text;
                Model.MenuLink = txtLink.Text;
                Model.MenuIcon = dddTextBox.Text;

                if (String.IsNullOrEmpty(ParentID))
                {
                    ParentID = "0";
                }
                Model.MenuLevel = 0;
                Model.ParentID = Convert.ToInt32(ParentID);
                Model.ShopClientID = Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                Model.MenuText = Server.HtmlEncode(txtContent.Text);
                Model.UpdateTime = DateTime.Now;


                bll.Add(Model);
                JsUtil.ShowMsg("添加成功!", getCurIDRestring(ParentID));

            }



        }


        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton2.Checked = false;
            LinkShow.Visible = false;
            TextShow.Visible = true;

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                btnAddClass.Visible = true;
                gvMenu.Visible = true;
            }
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton1.Checked = false;
            LinkShow.Visible = true;
            TextShow.Visible = false;

            //btnAddClass.Visible = false;
            //gvMenu.Visible = false;
        }
        protected void btnAddClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("GuidePages__Manage.aspx?type=Add&ParentID=" + Request.QueryString["ID"]);
        }


        private void BindAnnounce()
        {
            string ParentID = Request.QueryString["ParentID"];
            if ((ParentID == "") || (ParentID == null))
            {
                ParentID = Request.QueryString["ID"];
            }

            string strCondition = " and ShopClientID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and parentID=" + ParentID + "  and isnull(IsDeleted,0)=0 order by MenuPos asc,id desc";///只有oliver 才能看到所有记录

            gvMenu.DataSource = bll.GetDataTable("1000", "ID,MenuName,LinkOrText,MenuIcon,MenuLink", strCondition);
            gvMenu.DataBind();
        }


        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strURL = str_Pub_ClientApp + "/guidepage-" + e.Row.Cells[0].Text + ".aspx";
                e.Row.Cells[1].Text = "<a target=\"_blank\" href=\"" + strURL + "\"><font color=blue>" + e.Row.Cells[1].Text + "</font></a>";

                if (e.Row.Cells[3].Text == "True")
                {
                    e.Row.Cells[3].Text = "链接";
                }
                else
                {
                    e.Row.Cells[3].Text = "内容";
                }
                e.Row.Cells[5].Text = "<a href=\"GuidePages__Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[6].Text = "<a href=\"GuidePages__Delete.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";
                string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString())) + "/QRCodeImage/";
                string strImageUrl = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strURL, str_Pub_upLoadpath, "");
                e.Row.Cells[7].Text = "<a href=\"" + strURL + "\"><img style=\"display:block;\" width=\"100px;\" height=\"100px;\" src=\"" + strImageUrl + "\"></a>";
            }
        }

        private string getCurIDRestring(string CurentID)
        {
            string Restring = "";
            if (CurentID == "0")
            {
                Restring = strClientAdminURL + "/ClientAdmin/04GuidePages/GuidePages_Board.aspx";
            }
            else
            {
                Restring = "GuidePages__Manage.aspx?type=Modify&ID=" + CurentID;
            }
            return Restring;
        }

        private string getParentIDRestring(string CurentID)
        {
            //string CurentID = Request.QueryString["ID"];
            string strParentID = bll.GetList("ParentID", "ID=" + CurentID).Tables[0].Rows[0][0].ToString();
            string Restring = "";
            if (strParentID == "0")
            {
                //JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/Board_Good.aspx");
                Restring = strClientAdminURL + "/ClientAdmin/04GuidePages/GuidePages_Board.aspx";
            }
            else
            {
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                Restring = Eggsoft.Common.Application.getwebHttp() + "/UpLoadFile/ClientAdmin/LoginClientAdmin.aspx?Act=gotouserFrom_ClientAdmin&ShoClientID=" + strShopClient_ID + "&GoToUrl=";

                Restring = Restring + Server.UrlEncode("GuidePages/GuidePages__Manage.aspx?type=Modify&ID=" + strParentID);

                //Restring = "GuidePages__Manage.aspx?type=Modify?type=Modify&ID=" + CurentID;

            }
            return Restring;
        }
    }
}