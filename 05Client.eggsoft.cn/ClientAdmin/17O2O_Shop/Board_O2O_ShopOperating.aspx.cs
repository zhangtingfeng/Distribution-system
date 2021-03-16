using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._17O2O_Shop
{
    public partial class Board_O2O_ShopOperating : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String DisPlayStatus_New_None = "";
        public String strText_Shopping_Vouchers_Start = "";
        public String strText_Shopping_Vouchers_End = "";

        EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
        private EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string strVouchersNum = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strVouchersNum))
                        MyError.ThrowException("传递参数错误!");
                    BLL_O2O_ShopInfo.Delete("ID='" + strVouchersNum + "'");

                    JsUtil.ShowMsg("删除成功!", "Board_O2O_Shop.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    bingProvinceCity(sender, e);
                    SetClass(sender, e);
                }
            }
        }
        protected void bingProvinceCity(object sender, EventArgs e)
        {

            string strSQL = "";
            strSQL = "select * from tab_PE_Region where id in(Select min(id) FROM tab_PE_Region group by Province) ";
            DropDownList_Class1.DataSource = BLL_tab_PE_Region.SelectList(strSQL);
            DropDownList_Class1.DataTextField = "Province";
            DropDownList_Class1.DataValueField = "ID";
            DropDownList_Class1.DataBind();
            DropDownList_Class1_SelectedIndexChanged(sender, e);

        }

        protected void DropDownList_Class1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //return;
            string strSQL = "";
            strSQL = "select * from tab_PE_Region where id in(Select min(id) FROM tab_PE_Region where Province='" + DropDownList_Class1.SelectedItem + "' group by City )";

            DropDownList_Class2.DataSource = BLL_tab_PE_Region.SelectList(strSQL);
            DropDownList_Class2.DataTextField = "City";
            DropDownList_Class2.DataValueField = "ID";
            DropDownList_Class2.DataBind();
            DropDownList_Class2_SelectedIndexChanged(sender, e);
        }
        protected void DropDownList_Class2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //return;
            string strSQL = "";
            strSQL = "select * from tab_PE_Region where id in(Select min(id) FROM tab_PE_Region where City='" + DropDownList_Class2.SelectedItem + "' group by Area )";

            DropDownList_Class3.DataSource = BLL_tab_PE_Region.SelectList(strSQL); //new EggsoftWX.BLL.tab_Class3().GetDataTable("100", "ID,Area", "City=" + DropDownList_Class2.SelectedItem + " order by id asc");
            DropDownList_Class3.DataTextField = "Area";
            DropDownList_Class3.DataValueField = "ID";
            DropDownList_Class3.DataBind();

        }


        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_O2O_ShopInfo = BLL_O2O_ShopInfo.GetModel("ID=" + strID + "");

                Shop_Name.Text = Model_O2O_ShopInfo.ShopName;
                TextBox_Contactman.Text = Model_O2O_ShopInfo.ContactMan;
                TextBox_Tel.Text = Model_O2O_ShopInfo.Tel;
                TextBox_ShopAdMsg.Text = Model_O2O_ShopInfo.ShopAdMsg;
                TextBox_ShopDayTime.Text = Model_O2O_ShopInfo.ShopDayTime;

                DropDownList_Class1.SelectedValue = Model_O2O_ShopInfo.AdddressProvince.ToString();
                DropDownList_Class1_SelectedIndexChanged(sender, e);
                DropDownList_Class2.SelectedValue = Model_O2O_ShopInfo.AdddressCity.ToString();
                DropDownList_Class2_SelectedIndexChanged(sender, e);
                DropDownList_Class3.SelectedValue = Model_O2O_ShopInfo.AdddressCountry.ToString();

                TextBox_Address.Text = Model_O2O_ShopInfo.ShopAdress;
                TextBox_Lnt_Lat.Text = Model_O2O_ShopInfo.BaiDulng + "," + Model_O2O_ShopInfo.BaiDulat;



                #region XML Email 验证 font color 尚未关联微信号
                string strXML = Model_O2O_ShopInfo.XML;

                try
                {
                    if (string.IsNullOrEmpty(strXML))
                    {
                        Literal_CheckEmail.Text = "该Email尚未验证！";
                        Label_LinkWeiXin.Text = "尚未关联微信号，不能及时和用户沟通交流";
                    }
                    else
                    {
                        Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);
                        if (XML__Class_Shop_O2o.CheckEmail == false)
                        {
                            Literal_CheckEmail.Text = "该Email尚未验证！";
                        }
                        else
                        {
                            Literal_CheckEmail.Text = "该Email已验证！";
                        }

                        Image_ContactO2oLogo.ImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_O2o.ShopLogoo2oImage;
                        Textbox_Email.Text = XML__Class_Shop_O2o.Email;

                        if (String.IsNullOrEmpty(XML__Class_Shop_O2o.WeiXinRalationUserIDList))
                        {
                            Label_LinkWeiXin.Text = "尚未关联微信号，不能及时和用户沟通交流";
                        }
                        else
                        {


                            string[] strlist = XML__Class_Shop_O2o.WeiXinRalationUserIDList.Split(',');
                            int k = -1;
                            for (int i = 0; i < strlist.Length; i++)
                            {
                                if (String.IsNullOrEmpty(strlist[i]) == false)
                                {
                                    k++;
                                    String strUserid = strlist[i];
                                    String strUserid_nickname = Eggsoft_Public_CL.Pub.GetNickName(strUserid);
                                    Color yourColor = Color.FromName("#003300");
                                    Label_LinkWeiXin.ForeColor = yourColor;

                                    if (k == 0)
                                    {
                                        Label_LinkWeiXin.Text = "已关联的 账户:" + strUserid_nickname + " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserid);
                                    }
                                    else
                                    {
                                        Label_LinkWeiXin.Text += ",账户:" + strUserid_nickname + " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserid);
                                    }
                                }
                            }

                            if (strlist.Length >= 3)//只能绑定三个微信号
                            {

                                LinkButton_SaoYiSao.Enabled = false;
                                LinkButton_SaoYiSao.Text = LinkButton_SaoYiSao.Text + "已关联3的微信号（最多3个）！可以清除后重新绑定！";
                            }
                        }
                    }
                }
                catch
                {//...

                }
                finally
                {
                    //...
                }
                #endregion Email 验证




                btnAdd.Text = "保 存";
                DisPlayStatus_New_None = "";
            }
            else
            {
                DisPlayStatus_New_None = "display:none;";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            saveModelAction();
            if (type.ToLower() == "modify")
            {
                JsUtil.ShowMsg("修改成功!", "Board_O2O_Shop.aspx");
            }
            else if (type.ToLower() == "add")
            {
                JsUtil.ShowMsg("添加成功!", "Board_O2O_Shop.aspx");
            }
        }

        private int saveModelAction()
        {


            int into2oID = 0;

            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string type = Request.QueryString["type"];



                string strO2oLogo = "";

                string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

                if (FileUpload_ContactO2oLogo.HasFile == true)
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                    strO2oLogo = upLoadpath + saveName;
                    SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                    string eMsg = "";
                    string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                    upl.UploadFile(FileUpload_ContactO2oLogo.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                    System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
                }

                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_O2O_ShopInfo = new EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo();
                if (type.ToLower() == "modify")
                {
                    string strID = Request.QueryString["ID"];// 修改ID
                    Model_O2O_ShopInfo = BLL_O2O_ShopInfo.GetModel("ID=" + strID + "");
                }
                else if (type.ToLower() == "add")
                {

                }

                Model_O2O_ShopInfo.ShopDayTime = TextBox_ShopDayTime.Text;
                Model_O2O_ShopInfo.ShopAdMsg = TextBox_ShopAdMsg.Text.Trim();
                Model_O2O_ShopInfo.ShopClientID = Int32.Parse(strShopClientID);
                Model_O2O_ShopInfo.ShopName = Shop_Name.Text.Trim();
                Model_O2O_ShopInfo.ContactMan = TextBox_Contactman.Text.Trim();
                Model_O2O_ShopInfo.Tel = TextBox_Tel.Text.Trim();
                Model_O2O_ShopInfo.AdddressProvince = Int32.Parse(DropDownList_Class1.SelectedValue);
                Model_O2O_ShopInfo.AdddressCity = Int32.Parse(DropDownList_Class2.SelectedValue);
                Model_O2O_ShopInfo.AdddressCountry = Int32.Parse(DropDownList_Class3.SelectedValue);
                Model_O2O_ShopInfo.ShopAdress = TextBox_Address.Text.Trim();

                string strTextBox_Lnt_Lat = TextBox_Lnt_Lat.Text;
                string[] strTextBox_Lnt_LatList = strTextBox_Lnt_Lat.Split(',');
                if (strTextBox_Lnt_LatList.Length == 2)
                {
                    Model_O2O_ShopInfo.BaiDulng = strTextBox_Lnt_LatList[0];
                    Model_O2O_ShopInfo.BaiDulat = strTextBox_Lnt_LatList[1];
                }

                Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_o2o = new Eggsoft_Public_CL.XML__Class_Shop_O2o();
                if (String.IsNullOrEmpty(Model_O2O_ShopInfo.XML))
                {
                }
                else
                {
                    XML__Class_Shop_o2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(Model_O2O_ShopInfo.XML, System.Text.Encoding.UTF8);
                }
                if (String.IsNullOrEmpty(strO2oLogo) == false)
                {
                    XML__Class_Shop_o2o.ShopLogoo2oImage = strO2oLogo;
                }
                if (String.IsNullOrEmpty(Textbox_Email.Text.Trim()) == false)
                {
                    if (XML__Class_Shop_o2o.Email != Textbox_Email.Text.Trim())///不一样 肯定是假的
                    {
                        XML__Class_Shop_o2o.Email = Textbox_Email.Text.Trim();
                        XML__Class_Shop_o2o.CheckEmail = false;
                    }
                }
                string strmyXML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_o2o, System.Text.UnicodeEncoding.UTF8);
                Model_O2O_ShopInfo.XML = strmyXML__Class_Shop_O2o;

                if (type.ToLower() == "modify")
                {
                    into2oID = Int32.Parse(Request.QueryString["ID"]);
                    BLL_O2O_ShopInfo.Update(Model_O2O_ShopInfo);
                }
                else if (type.ToLower() == "add")
                {
                    into2oID = BLL_O2O_ShopInfo.Add(Model_O2O_ShopInfo);
                }






            }
            catch
            {

            }

            finally
            {

            }

            return into2oID;
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    // Eggsoft.Common.JsUtil.OpenWindow("Board_O2O_ShopNav.html", 0, 0, 0, 0);


        //    Response.Write("<script>window.open('Board_O2O_ShopNav.html','_blank')</script>");//——原窗口保留，另外新增一个新页面;
        //}
        protected void Button_CheckEmail_Click_Click(object sender, EventArgs e)
        {
            int into2oID = saveModelAction();





            //string strFrom=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")+"微店":
            //string strFrom = "China@tom.com";
            string strTo = Textbox_Email.Text.Trim();
            string strSubject = Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店" + " o2o电子邮件地址验证！";
            string strBody = "你好，我们给你发信，是因为您在" + Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店" + "进行o2o邮件地址验证引起！" + "\n";
            strBody += "如果这不是你的操作引起的，请忽略掉即可！" + "\n";
            strBody += "请点击如下的连接进行地址验证。如果不能点击，请复制如下连接到浏览器地址栏！" + "\n";

            QueryString_EggSoft QueryString = new QueryString_EggSoft();
            QueryString.Add("into2oID", into2oID.ToString());
            QueryString.Add("Email", strTo);
            QueryString.Add("MD5ID", Eggsoft.Common.DESCrypt.Crypt(into2oID.ToString()));
            //Response.Redirect("Receive.aspx?data=" + QueryString.ToString());
            strBody += "https://" + HttpContext.Current.Request.Url.Host + "/ClientAdmin/17O2O_Shop/CheckUserEmailInfo.aspx?type=EmalCheck&data=" + QueryString.ToString();

            string strCityName = Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店";
            if (Eggsoft_Public_CL.Pub.SendEmail_AddTask(strCityName, strTo, strSubject, strBody))
            {
                int intPos = strTo.IndexOf('@');
                string strWeb = "http://mail." + strTo.Substring(intPos + 1);


                Eggsoft.Common.JsUtil.ShowMsgNew("发信成功,将访问:" + strWeb, strWeb);
            }
        }

        protected void Textbox_Email_TextChanged(object sender, EventArgs e)
        {
            Literal_CheckEmail.Text = "该Email尚未验证！";
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        protected void LinkButton_SaoYiSao_Click(object sender, EventArgs e)
        {
            int into2oID = saveModelAction();
            string strURL = "/ClientAdmin/17O2O_Shop/RegisterOpenID.aspx?type=AskRalation&into2oID=" + into2oID;
            Eggsoft.Common.JsUtil.LocationNewHref(strURL);
        }
        protected void LinkButton_Clear_Click(object sender, EventArgs e)
        {
            int into2oID = saveModelAction();
            string strURL = "/ClientAdmin/17O2O_Shop/RegisterOpenID.aspx?type=ClearRalation&into2oID=" + into2oID;
            Eggsoft.Common.JsUtil.LocationNewHref(strURL);

        }
    }
}