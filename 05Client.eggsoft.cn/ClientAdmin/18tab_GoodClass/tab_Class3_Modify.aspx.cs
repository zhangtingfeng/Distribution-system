﻿using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class3_Modify : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        string str_Pub_ShopClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (!IsPostBack)
            {
                int Class3_ID = Int32.Parse(Request.QueryString["Class3_ID"].ToString());
                EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();
                EggsoftWX.Model.tab_Class3 sc = bll.GetModel(Class3_ID);

                lblID.Text = Class3_ID.ToString();
                txtSmallClassName.Text = sc.ClassName;
                txtSmallClassPos.Text = sc.Sort.ToString();

                string strUpLoadURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadURL"];
                Image_Nav_PIC_Small.ImageUrl = strUpLoadURL + sc.ClassIcon;
                Image_Nav_PIC_Big.ImageUrl = strUpLoadURL + sc.BigPicpath;
                InitBigClass();
            }
        }

        private void InitBigClass()
        {
            EggsoftWX.BLL.tab_Class1 bcBll = new EggsoftWX.BLL.tab_Class1();
            ddlBigClass1.DataTextField = "ClassName";
            ddlBigClass1.DataValueField = "ID";
            ddlBigClass1.DataSource = bcBll.GetList("*", "ShopClientID=" + str_Pub_ShopClientID + " order by sort asc");
            ddlBigClass1.DataBind();
            ddlBigClass1.SelectedValue = Eggsoft_Public_CL.ClassP.GetClass1_ID_From_Class3_ID(Int32.Parse(Request.QueryString["Class3_ID"].ToString())).ToString();
            ddlBigClass1_SelectedIndexChanged(null, null);
        }

        protected void ddlBigClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EggsoftWX.BLL.tab_Class2 bcBll = new EggsoftWX.BLL.tab_Class2();
            ddlBigClass2.DataTextField = "ClassName";
            ddlBigClass2.DataValueField = "ID";
            ddlBigClass2.DataSource = bcBll.GetList("*", "Class1_ID=" + ddlBigClass1.SelectedValue + " and ShopClientID=" + str_Pub_ShopClientID + " order by sort asc");
            ddlBigClass2.DataBind();


            string strWhere = "Class1_ID=" + ddlBigClass1.SelectedValue + " and ID=" + Eggsoft_Public_CL.ClassP.GetClass2_ID_From_Class3_ID(Int32.Parse(Request.QueryString["Class3_ID"].ToString())).ToString().ToString();
            if (bcBll.Exists(strWhere))
            {
                ddlBigClass2.SelectedValue = Eggsoft_Public_CL.ClassP.GetClass2_ID_From_Class3_ID(Int32.Parse(Request.QueryString["Class3_ID"].ToString())).ToString();
            }
            else
            {
                // 另外还可以通过在绑定之后使用下面的方式添加
                this.ddlBigClass2.Items.Insert(0, new ListItem("请选择...", "-1"));
                this.ddlBigClass2.SelectedIndex = 0;
            }
        }


        protected void btnModify_Click(object sender, EventArgs e)
        {
            int Class3_ID = Int32.Parse(Request.QueryString["Class3_ID"].ToString());
            EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();
            EggsoftWX.Model.tab_Class3 sc = bll.GetModel(Class3_ID);

            sc.Class1_ID = Int32.Parse(ddlBigClass1.SelectedValue);
            sc.Class2_ID = Int32.Parse(ddlBigClass2.SelectedValue);
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