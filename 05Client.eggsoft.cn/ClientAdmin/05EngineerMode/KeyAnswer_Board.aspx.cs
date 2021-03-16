﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class KeyAnswer_Board : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer BLL_tab_ShopClient_EngineerMode_KeyAnswer = new EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_KeyAnswer")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {
                ViewState["PageIndex"] = 1;
                ViewState["PageSize"] = 20;

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                ViewState["RecordCount"] = BLL_tab_ShopClient_EngineerMode_KeyAnswer.ExistsCount("ShopClientID=" + strShopClientID);
                BindAnnounce();
                ShowState();
                InitGoPage();

                EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

                bool bool_Like_Or_Same = Model_tab_ShopClient_EngineerMode.RadioButtonList_Like_Or_Same;

                if (bool_Like_Or_Same)
                {
                    RadioButtonList_Like_Or_Same.SelectedValue = "1";
                    RadioButtonList_Like_Or_Same_SelectedIndexChanged(sender, e);//主动save 
                }
                else
                {
                    RadioButtonList_Like_Or_Same.SelectedValue = "0";
                    RadioButtonList_Like_Or_Same_SelectedIndexChanged(sender, e);//主动save 

                    //RadioButtonList_Like_Or_Same.SelectedValue = strLike_Or_Same;
                }
            }
        }


        private void InitGoPage()
        {
            ddlGoPage.Items.Clear();
            for (int i = 1; i <= GetPageCount(); i++)
            {
                ddlGoPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlGoPage.SelectedValue = ViewState["PageIndex"].ToString();
        }

        private void BindAnnounce()
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strCondition = "ShopClientID=" + strShopClientID;//
            //if (Eggsoft.Common.Session.Read("Eggsoft_Admin__Users").ToString().ToLower() != "oliver")
            //{
            //    strCondition = "ID<18917905147";
            //}

            gvAnnounce.DataSource = BLL_tab_ShopClient_EngineerMode_KeyAnswer.GetPageDataTable(Int32.Parse(ViewState["PageIndex"].ToString()), Int32.Parse(ViewState["PageSize"].ToString()), "ID,Marker,MarkerContent,UpdateTime", strCondition, "ID", false);
            gvAnnounce.DataBind();
        }

        protected void gvAnnounce_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String strMarkerContent = e.Row.Cells[2].Text;

                //strMarkerContent=strMarkerContent.Replace("#$#$#", "@");
                //String[] strList = strMarkerContent.Split('@');
                string[] strList = strMarkerContent.Split(new char[5] { '#', '$', '#', '$', '#' }, StringSplitOptions.RemoveEmptyEntries);
                //  1         2              3         4       5          6          7           8
                String[] strMsgTypeList = { "文本消息", "单图文消息", "多图文消息", "" };
                e.Row.Cells[2].Text = strMsgTypeList[Convert.ToInt32(strList[0]) - 1];
                e.Row.Cells[3].Text = strList[1];

                e.Row.Cells[4].Text = "<a href=\"KeyAnswer_Manage.aspx?type=Modify&ID=" + e.Row.Cells[0].Text + "\">修改</a>";
                e.Row.Cells[5].Text = "<a href=\"KeyAnswer_Manage.aspx?type=Delete&ID=" + e.Row.Cells[0].Text + "\" onclick=\"return confirm('确定删除吗?')\">删除</a>";

            }
        }


        ////为动态创建的按钮事件写一个方法 
        //protected void bt_Click(object sender, EventArgs e)
        //{
        //    ((Button)sender).BackColor = System.Drawing.Color.Red;
        //    Eggsoft.Common.JsUtil.ShowMsg("dd");
        //}

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = 1;
            BindAnnounce();
            ShowState();
            InitGoPage();
        }

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) > 1)
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) - 1;
            }
            else
            {
                ViewState["PageIndex"] = GetPageCount();
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(ViewState["PageIndex"].ToString()) < GetPageCount())
            {
                ViewState["PageIndex"] = Int32.Parse(ViewState["PageIndex"].ToString()) + 1;
            }
            else
            {
                ViewState["PageIndex"] = 1;
            }
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = GetPageCount();
            BindAnnounce();
            ShowState();
            InitGoPage();
        }
        protected void ddlGoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = Int32.Parse(ddlGoPage.SelectedValue);
            BindAnnounce();
            ShowState();
        }

        private void ShowState()
        {
            lblMsg.Text = "当前页:" + ViewState["PageIndex"].ToString() + "/" + GetPageCount().ToString() + " 每页:" + ViewState["PageSize"].ToString() + "条 共:" + ViewState["RecordCount"].ToString() + "条";
            if (GetPageCount() <= 1)
            {
                lbtnFirst.Enabled = false;
                lbtnPrev.Enabled = false;
                lbtnNext.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else
            {
                if (Int32.Parse(ViewState["PageIndex"].ToString()) <= 1)
                {
                    lbtnFirst.Enabled = false;
                    lbtnPrev.Enabled = false;
                    lbtnNext.Enabled = true;
                    lbtnLast.Enabled = true;
                }
                else
                {
                    if (Int32.Parse(ViewState["PageIndex"].ToString()) >= GetPageCount())
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = false;
                        lbtnLast.Enabled = false;
                    }
                    else
                    {
                        lbtnFirst.Enabled = true;
                        lbtnPrev.Enabled = true;
                        lbtnNext.Enabled = true;
                        lbtnLast.Enabled = true;
                    }
                }
            }
        }

        private int GetPageCount()
        {
            int pageCount = Int32.Parse(ViewState["RecordCount"].ToString()) % Int32.Parse(ViewState["PageSize"].ToString()) == 0 ? (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString())) : (Int32.Parse(ViewState["RecordCount"].ToString()) / Int32.Parse(ViewState["PageSize"].ToString()) + 1);
            return pageCount;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("KeyAnswer_Manage.aspx?type=Add");
        }
        protected void RadioButtonList_Like_Or_Same_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

            string strRadioButtonList_Like_Or_Same = RadioButtonList_Like_Or_Same.SelectedValue;

            if (strRadioButtonList_Like_Or_Same == "0")
            {
                strRadioButtonList_Like_Or_Same = "false";
            }
            else if (strRadioButtonList_Like_Or_Same == "1")
            {
                strRadioButtonList_Like_Or_Same = "true";
            }
            bool bool_Like_Or_Same = false;
            bool.TryParse(strRadioButtonList_Like_Or_Same, out bool_Like_Or_Same);


            Model_tab_ShopClient_EngineerMode.RadioButtonList_Like_Or_Same = bool_Like_Or_Same;
            BLL_tab_ShopClient_EngineerMode.Update(Model_tab_ShopClient_EngineerMode);


        }
    }
}