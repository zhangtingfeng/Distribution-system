using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.UpLoadFile.ClientAdmin.OnlineBaoMing
{
    public partial class OnlineBaoMing_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

        public String MenuLink = "";
        EggsoftWX.BLL.tab_ShopClient_OlineContent bll_tab_OlineContent = new EggsoftWX.BLL.tab_ShopClient_OlineContent();
        EggsoftWX.Model.tab_ShopClient_OlineContent Model_tab_OlineContent = new EggsoftWX.Model.tab_ShopClient_OlineContent();

        EggsoftWX.BLL.tab_ShopClient_OnlineRegistration bll_tab_OnlineRegistration = new EggsoftWX.BLL.tab_ShopClient_OnlineRegistration();
        EggsoftWX.Model.tab_ShopClient_OnlineRegistration Model_tab_OnlineRegistration = new EggsoftWX.Model.tab_ShopClient_OnlineRegistration();

        public string strTextbox_Timer0_Text = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];


            if (!IsPostBack)
            {

                //RadioButtonList1.SelectedValue = "0";
                // LinkShow.Visible = false;
                //TextShow.Visible = true;
                //RadioButton1.Checked = true;
                //RadioButton2.Checked = false;


                if (string.IsNullOrEmpty(type) == false)
                {
                    if (type.ToLower() == "delete")
                    {
                        string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                        string ID = Request.QueryString["ID"];
                        if (!CommUtil.IsNumStr(ID))
                            MyError.ThrowException("传递参数错误!");
                        bll_tab_OlineContent.Delete(Int32.Parse(ID));
                        bll_tab_OnlineRegistration.Update("IsDeleted=1", "OnlineID=" + ID + " and ShopClient_ID=" + strShopClient_ID);

                        string strCallBackUrl = Request.QueryString["CallBackUrl"];
                        strCallBackUrl = strCallBackUrl.Replace("*", "?");
                        JsUtil.ShowMsg("删除成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/OnlineBaoMing/" + strCallBackUrl);
                        //JsUtil.ShowMsg("删除成功!", "OnlineBaoMing.aspx");
                    }
                    else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass();
                    }
                }
            }
            else
            {
                if ((type.ToLower() == "modify"))
                {
                    Bind();
                }
            }
        }

        private void SetClass()
        {

            string type = Request.QueryString["Type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_OlineContent Model = bll_tab_OlineContent.GetModel(Int32.Parse(ID));

                txtTitle.Text = Model.Title;

                txtContent.Text = Server.HtmlDecode(Model.Oline_Content);

                string strXML = Model.XML;
                if (string.IsNullOrEmpty(strXML) == false)
                {
                    Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Online>(strXML, System.Text.Encoding.UTF8);
                    CheckBox_NeedCheck.Checked = myXML.BoolNeedCheck;
                    CheckBox_Need_WriteData.Checked = myXML.NeedWrite_NotChoiceShengFeng;
                    TextBox1My_DeadLine.Text = myXML.NeedWrite_DeadlineTime.ToString("yyyy-MM-dd");
                }

                #region  ExpListText  custorm your self Item  自定义字段
                CheckBox_IFShow_Cus_Item.Checked = Model.AddExpListTextShow.toBoolean();

                string strAddExpListText = Model.AddExpListText;
                if ((String.IsNullOrEmpty(strAddExpListText) == false))
                {
                    string[] strAddExpListTextList = strAddExpListText.Split('#');
                    for (int i = 0; i < strAddExpListTextList.Length; i++)
                    {
                        if (Model.AddExpListTextShow.toBoolean())
                        {
                            BoundField f_BoundField = new BoundField();
                            f_BoundField.DataField = "AddExp" + (i + 1).ToString();
                            f_BoundField.HeaderText = strAddExpListTextList[i];
                            GridView_ShowAll.Columns.Add(f_BoundField);
                        }
                        ListBox_Item.Items.Add(strAddExpListTextList[i]);
                    }
                }
                #endregion
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient blltab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Modeltab_ShopClient = blltab_ShopClient.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Modeltab_ShopClient.ErJiYuMing;///默认一个数值
                MenuLink = "https://" + strErJiYuMing + "/huodong/05OlineInfo-" + Model.ShopClient_ID.ToString() + "-" + ID + ".aspx";
                btnAdd.Text = "保 存";
                
                Bind();
            }
            else if (type.ToLower() == "add")
            {
                DateTime my1datetime = DateTime.Now.AddDays(7);
                TextBox1My_DeadLine.Text = my1datetime.ToString("yyyy-MM-dd");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];


            #region  ExpListText  custorm your self Item  自定义字段
            string strAddExpListText = "";



            for (int i = 0; i < ListBox_Item.Items.Count; i++)
            {
                if (i >= 20) break;
                strAddExpListText += "#" + ListBox_Item.Items[i].Text.Trim();
            }
            if (strAddExpListText.Length > 0)
            {
                strAddExpListText = strAddExpListText.Remove(0, 1);
            }
            #endregion

            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                Model_tab_OlineContent = bll_tab_OlineContent.GetModel(Int32.Parse(ID));


                Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = new Eggsoft_Public_CL.XmlHelper_Instance_Online();
                myXML.BoolNeedCheck = CheckBox_NeedCheck.Checked;
                myXML.NeedWrite_NotChoiceShengFeng = CheckBox_Need_WriteData.Checked;

                string strText_SecondBuyStart = TextBox1My_DeadLine.Text;
                DateTime my1datetime = DateTime.Now;
                DateTime.TryParseExact(strText_SecondBuyStart, "yyyy-MM-dd", null, DateTimeStyles.None, out my1datetime);
                myXML.NeedWrite_DeadlineTime = my1datetime;
                string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myXML, System.Text.Encoding.UTF8);
                Model_tab_OlineContent.XML = strXML;

                Model_tab_OlineContent.Title = txtTitle.Text;
                Model_tab_OlineContent.Oline_Content = Server.HtmlEncode(txtContent.Text);

                Model_tab_OlineContent.AddExpListText = strAddExpListText;
                Model_tab_OlineContent.AddExpListTextShow = CheckBox_IFShow_Cus_Item.Checked;
                Model_tab_OlineContent.UpdateTime = DateTime.Now;
                Model_tab_OlineContent.Updateby = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                bll_tab_OlineContent.Update(Model_tab_OlineContent);

                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                strCallBackUrl = strCallBackUrl.Replace("*", "?");
                JsUtil.ShowMsg("修改成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/OnlineBaoMing/" + strCallBackUrl);


                //JsUtil.ShowMsg("修改成功!", "OnlineBaoMing.aspx");
            }
            else
                if (type.ToLower() == "add")
            {

                Model_tab_OlineContent.Title = txtTitle.Text;
                Model_tab_OlineContent.Oline_Content = Server.HtmlEncode(txtContent.Text);
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                Model_tab_OlineContent.ShopClient_ID = Int32.Parse(str_Pub_ShopClientID);

                Eggsoft_Public_CL.XmlHelper_Instance_Online myXML = new Eggsoft_Public_CL.XmlHelper_Instance_Online();
                myXML.BoolNeedCheck = CheckBox_NeedCheck.Checked;
                myXML.NeedWrite_NotChoiceShengFeng = CheckBox_Need_WriteData.Checked;
                string strText_SecondBuyStart = TextBox1My_DeadLine.Text;
                DateTime my1datetime = DateTime.Now.AddDays(7);
                DateTime.TryParseExact(strText_SecondBuyStart, "yyyy-MM-dd", null, DateTimeStyles.None, out my1datetime);
                myXML.NeedWrite_DeadlineTime = my1datetime;

                string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myXML, System.Text.Encoding.UTF8);
                Model_tab_OlineContent.XML = strXML;

                Model_tab_OlineContent.AddExpListText = strAddExpListText;
                Model_tab_OlineContent.AddExpListTextShow = CheckBox_IFShow_Cus_Item.Checked;
                Model_tab_OlineContent.Createby = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                Model_tab_OlineContent.Updateby = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                bll_tab_OlineContent.Add(Model_tab_OlineContent);

                string strCallBackUrl = Request.QueryString["CallBackUrl"];
                if (String.IsNullOrEmpty(strCallBackUrl) == true)
                {
                    strCallBackUrl = "OnlineBaoMing.aspx";
                }
                else
                {
                    strCallBackUrl = strCallBackUrl.Replace("*", "?");
                }
                JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/23WeiHuoDong/OnlineBaoMing/" + strCallBackUrl);
            }



        }

        //为表格绑定数据源  
        private void Bind()
        {
            string strOnlineID = Request.QueryString["ID"];// 修改ID
            GridView_ShowAll.DataSource = bll_tab_OnlineRegistration.GetList("*", "OnlineID=" + strOnlineID + " and isnull(IsDeleted,0)=0 order by id asc");
            GridView_ShowAll.DataBind();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strNum = e.CommandArgument.ToString();  //教师编号
            int intID = 0;
            int.TryParse(strNum, out intID);
            switch (e.CommandName)
            {
                case "Del":
                    bll_tab_OnlineRegistration.Update("IsDeleted=1", "ID=" + intID);
                    Bind();
                    break;
                case "Pass":
                    Model_tab_OnlineRegistration = bll_tab_OnlineRegistration.GetModel(intID);
                    Model_tab_OnlineRegistration.Valid = !Model_tab_OnlineRegistration.Valid;

                    Model_tab_OnlineRegistration.Updateby = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");
                    Model_tab_OnlineRegistration.UpdateTime = DateTime.Now;
                    bll_tab_OnlineRegistration.Update(Model_tab_OnlineRegistration);
                    Bind();
                    break;
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_ShowAll.PageIndex = e.NewPageIndex;
            Bind();
        }
        protected void CheckBox_IFShow_Cus_Item_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePanel1.Visible = CheckBox_IFShow_Cus_Item.Checked;
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            ListBox_Item.Items.Add(TextBox_Item.Text.Trim());
        }
        protected void Button_Del_Click(object sender, EventArgs e)
        {
            ListBox_Item.Items.Remove(ListBox_Item.SelectedItem);
        }
    }
}