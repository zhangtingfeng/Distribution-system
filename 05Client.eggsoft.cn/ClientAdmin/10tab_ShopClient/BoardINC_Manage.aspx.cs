using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class BoardINC_Manage1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
        private EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("BasicSetting_BoardINC_Manage")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限


            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];


                DropDownList_Class1.DataSource = new EggsoftWX.BLL.tab_Class1().GetDataTable("100", "ID,ClassName", "1=1 order by Sort asc,ID asc");
                DropDownList_Class1.DataTextField = "ClassName";
                DropDownList_Class1.DataValueField = "ID";
                DropDownList_Class1.DataBind();
                SetClass(sender, e);
            }
        }




        private void SetClass(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strID));




                //TextboxUserName.Enabled = false;
                Label_UserName.Text = tab_ShopClient_Model.Username;

                Label_ModifyTip.Visible = true;

                Textbox_Email.Text = tab_ShopClient_Model.Email.Trim();
                Textbox_RealName.Text = tab_ShopClient_Model.ContactMan.Trim();
                Textbox_ContactManPostion.Text = tab_ShopClient_Model.ContactManPostion;

                if (tab_ShopClient_Model.Sex == false)
                { RadioButtonList_Sex.SelectedValue = "0"; }
                else
                { RadioButtonList_Sex.SelectedValue = "1"; }
                Textbox_BeiZhu.Text = tab_ShopClient_Model.ShopMemo;

                txtINCName.Text = tab_ShopClient_Model.ShopClientName;
                DropDownList_INC.SelectedValue = tab_ShopClient_Model.ShopClientType;
                ImageButton.ImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + tab_ShopClient_Model.ShopButton;


                //setImage_Logo("Show");




                #region XML Email 验证 font color 尚未关联微信号
                string strXML = tab_ShopClient_Model.XML;

                try
                {
                    if (string.IsNullOrEmpty(strXML))
                    {
                        Literal_CheckEmail.Text = "该Email尚未验证！";
                        Label_LinkWeiXin.Text = "尚未关联微信号，不能及时和用户沟通交流";
                    }
                    else
                    {
                        Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);

                        Image_GongZhongPingTaiErWeiMa.ImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_Client.WeiXinGongZhongPingTaiErWeiMaIMG;


                        if ((XML__Class_Shop_Client.CheckEmail == false) || (XML__Class_Shop_Client.Email != tab_ShopClient_Model.Email))
                        {
                            Literal_CheckEmail.Text = "该Email尚未验证！";
                        }
                        else
                        {
                            Literal_CheckEmail.Text = "该Email已验证！";
                        }
                        RadioButtonList_GoodListShowType.SelectedValue = XML__Class_Shop_Client.IntGoodClassShowType.ToString();

                        ColorPicker_BackColor.Color = XML__Class_Shop_Client.Back_Color;
                        ColorPicker_Font.Color = XML__Class_Shop_Client.FontColor;
                        ColorPicker_MenuBar_Color.Color = XML__Class_Shop_Client.MenuBarColor;
                        Image_Logo.ImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_Client.ShopLogoImage;
                        Image_ContactManErWeiMa.ImageUrl = ConfigurationManager.AppSettings["UpLoadURL"] + XML__Class_Shop_Client.WeiXinErWeiMaIMG;
                        CheckBox_AddPic_Auto.Checked = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;


                        if (String.IsNullOrEmpty(XML__Class_Shop_Client.WeiXinRalationUserIDList))
                        {
                            Label_LinkWeiXin.Text = "尚未关联微信号，不能及时和用户沟通交流";
                        }
                        else
                        {


                            string[] strlist = XML__Class_Shop_Client.WeiXinRalationUserIDList.Split(',');
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
                            String[] stringPowerList = Eggsoft_Public_CL.Pub.GetstringPowerList(strXML);
                            if (stringPowerList.Length == 0)//还没设置过
                            {
                                if (stringPowerList[3] == "1")//只能3个
                                {
                                    HyperLink_LinkWeiXin.Enabled = false;
                                    HyperLink_LinkWeiXin.Text = HyperLink_LinkWeiXin.Text + "已关联3的微信号！可以清除后重新绑定！";
                                }
                            }
                            else
                            {
                                if (strlist.Length == 1)//只能绑定三个微信号
                                {
                                    if (stringPowerList[3] == "0")//只能一个
                                    {
                                        HyperLink_LinkWeiXin.Enabled = false;
                                        HyperLink_LinkWeiXin.Text = HyperLink_LinkWeiXin.Text + "已关联1的微信号（最多1个）！可以清除后重新绑定！";
                                    }
                                }


                                if (strlist.Length >= 3)//只能绑定三个微信号
                                {
                                    if (stringPowerList[3] == "1")//只能3个
                                    {
                                        HyperLink_LinkWeiXin.Enabled = false;
                                        HyperLink_LinkWeiXin.Text = HyperLink_LinkWeiXin.Text + "已关联3的微信号（最多3个）！可以清除后重新绑定！";
                                    }
                                }
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




                string strUserClientClass = tab_ShopClient_Model.ShopClientClass;
                string[] strList = strUserClientClass.Split(',');
                DropDownList_Class1.SelectedValue = strList[0];
                if (strList.Length > 1)
                {
                    DropDownList_Class1_SelectedIndexChanged(sender, e);
                    DropDownList_Class2.SelectedValue = strList[1];
                }
                else
                {
                    DropDownList_Class2.Visible = false;
                }
                if (strList.Length > 2)
                {
                    DropDownList_Class2_SelectedIndexChanged(sender, e);
                    DropDownList_Class3.SelectedValue = strList[2];
                }
                else
                {
                    DropDownList_Class3.Visible = false;
                }

                TextboxINCPhone.Text = tab_ShopClient_Model.ContactPhone;
                TextboxAddress.Text = tab_ShopClient_Model.Address;
                Literal_Authortime.Text = Convert.ToDateTime(tab_ShopClient_Model.AuthorTime).ToShortDateString();

                //tab_ShopClient_Model.Updatetime = DateTime.Now;       
                btnAdd.Text = "保 存";

                RequiredFieldValidatorTextboxUserPassword.Enabled = false;



            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SaveMode();

            JsUtil.ShowMsg("修改成功!", "BoardINC_Manage.aspx?type=Modify");
        }
        private void SaveMode()
        {
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            string type = Request.QueryString["type"];

            tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strINCID));
            tab_ShopClient_Model = saveModel(tab_ShopClient_Model);

            System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
            string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_WeiXinErWeiMa.asmx";
            string[] args = new string[1];
            args[0] = strINCID;// 
            object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WebMethod_APPCODE_getImage_WeiXinErWeiMa", args);
            string strresult = result.ToString();

            tab_ShopClient_bll.Update(tab_ShopClient_Model);
        }

        private EggsoftWX.Model.tab_ShopClient saveModel(EggsoftWX.Model.tab_ShopClient myModel)
        {
            if (TextboxRePassword.Text.Trim() != "")
            {
                myModel.PassWord = Eggsoft.Common.DESCrypt.GetMd5Str32(TextboxRePassword.Text.Trim());
            }
            myModel.Email = Textbox_Email.Text.Trim();
            myModel.ContactMan = Textbox_RealName.Text.Trim();
            myModel.ContactManPostion = Textbox_ContactManPostion.Text.Trim();
            if (RadioButtonList_Sex.SelectedValue == "0")
            { myModel.Sex = false; }
            else
            { myModel.Sex = true; }
            myModel.ShopMemo = Textbox_BeiZhu.Text;

            myModel.ShopClientName = txtINCName.Text.Trim();
            myModel.ShopClientType = DropDownList_INC.SelectedValue;


            String strFileUpload_Logo = "";
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strINCID)) + "/images/";

            if (FileUpload_Logo.HasFile == true)
            {
                //strFileUpload_Logo = Eggsoft.Common.FileFolder.btnFileUpload(FileUpload_Logo, upLoadpath);
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileUpload_Logo = upLoadpath + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                upl.UploadFile(FileUpload_Logo.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
            }
            String strFileUpload_Logo_WeiXinErWeiMa = "";
            if (FileUpload_ContactManErWeiMa.HasFile == true)
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileUpload_Logo_WeiXinErWeiMa = upLoadpath + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                upl.UploadFile(FileUpload_ContactManErWeiMa.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
            }

            String strFileUpload_Logo_WeiXinErWeiMaWeiXinGongZhongPingTai = "";
            if (FileUpload_GongZhongPingTaiErWeiMa.HasFile == true)
            {
                //strFileUpload_Logo = Eggsoft.Common.FileFolder.btnFileUpload(FileUpload_Logo, upLoadpath);
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                strFileUpload_Logo_WeiXinErWeiMaWeiXinGongZhongPingTai = upLoadpath + saveName;
                SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                string eMsg = "";
                string strRemoveUpload = upLoadpath.Substring("/upload/".Length, upLoadpath.Length - 1 - "/upload/".Length);
                upl.UploadFile(FileUpload_GongZhongPingTaiErWeiMa.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
            }


            try
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(myModel.XML, System.Text.Encoding.UTF8);
                XML__Class_Shop_Client.Back_Color = ColorPicker_BackColor.Color;
                XML__Class_Shop_Client.FontColor = ColorPicker_Font.Color;
                XML__Class_Shop_Client.Bool_AddWatermater_Logo_ = CheckBox_AddPic_Auto.Checked;
                XML__Class_Shop_Client.IntGoodClassShowType = Int32.Parse(RadioButtonList_GoodListShowType.SelectedValue);
                if (String.IsNullOrEmpty(strFileUpload_Logo) == false) XML__Class_Shop_Client.ShopLogoImage = strFileUpload_Logo;
                if (String.IsNullOrEmpty(strFileUpload_Logo_WeiXinErWeiMa) == false) XML__Class_Shop_Client.WeiXinErWeiMaIMG = strFileUpload_Logo_WeiXinErWeiMa;
                XML__Class_Shop_Client.MenuBarColor = ColorPicker_MenuBar_Color.Color;
                //myModel.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.Encoding.UTF8);
                //System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式

                if (String.IsNullOrEmpty(strFileUpload_Logo_WeiXinErWeiMaWeiXinGongZhongPingTai) == false) XML__Class_Shop_Client.WeiXinGongZhongPingTaiErWeiMaIMG = strFileUpload_Logo_WeiXinErWeiMaWeiXinGongZhongPingTai;
                myModel.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.Encoding.UTF8);
                System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式

            }
            catch
            {

                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = new Eggsoft_Public_CL.XML__Class_Shop_Client();
                XML__Class_Shop_Client.Email = Textbox_Email.Text;
                XML__Class_Shop_Client.CheckEmail = false;
                if (String.IsNullOrEmpty(strFileUpload_Logo) == false) XML__Class_Shop_Client.ShopLogoImage = strFileUpload_Logo;
                if (String.IsNullOrEmpty(strFileUpload_Logo_WeiXinErWeiMa) == false) XML__Class_Shop_Client.WeiXinErWeiMaIMG = strFileUpload_Logo_WeiXinErWeiMa;
                XML__Class_Shop_Client.Back_Color = ColorPicker_BackColor.Color;
                XML__Class_Shop_Client.FontColor = ColorPicker_Font.Color;
                XML__Class_Shop_Client.Bool_AddWatermater_Logo_ = CheckBox_AddPic_Auto.Checked;

                XML__Class_Shop_Client.MenuBarColor = ColorPicker_MenuBar_Color.Color;
                XML__Class_Shop_Client.IntGoodClassShowType = Int32.Parse(RadioButtonList_GoodListShowType.SelectedValue);

                myModel.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.Encoding.UTF8);
            }
            finally
            {


            }

            string strUserClientClass = "";
            strUserClientClass += DropDownList_Class1.SelectedValue + ",";
            if (DropDownList_Class2.Visible == true) strUserClientClass += DropDownList_Class2.SelectedValue + ",";
            if (DropDownList_Class3.Visible == true) strUserClientClass += DropDownList_Class3.SelectedValue + ",";
            strUserClientClass = Eggsoft_Public_CL.ClassP.RemoveLastDouHao(strUserClientClass);
            myModel.ShopClientClass = strUserClientClass;
            myModel.ContactPhone = TextboxINCPhone.Text.Trim();
            myModel.Address = TextboxAddress.Text.Trim();
            myModel.Updatetime = DateTime.Now;

            return myModel;

        }

        protected void DropDownList_Class1_SelectedIndexChanged(object sender, EventArgs e)
        {


            DropDownList_Class2.DataSource = new EggsoftWX.BLL.tab_Class2().GetDataTable("100", "ID,ClassName", "Class1_ID=" + DropDownList_Class1.SelectedValue);
            DropDownList_Class2.DataTextField = "ClassName";
            DropDownList_Class2.DataValueField = "ID";
            DropDownList_Class2.DataBind();
        }
        protected void DropDownList_Class2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList_Class3.DataSource = new EggsoftWX.BLL.tab_Class3().GetDataTable("100", "ID,ClassName", "Class2_ID=" + DropDownList_Class2.SelectedValue);
            DropDownList_Class3.DataTextField = "ClassName";
            DropDownList_Class3.DataValueField = "ID";
            DropDownList_Class3.DataBind();

        }



        protected void Textbox_Email_TextChanged(object sender, EventArgs e)
        {
            Literal_CheckEmail.Text = "该Email尚未验证！";
        }
        protected void Button_CheckEmail_Click_Click(object sender, EventArgs e)
        {

            SaveMode();

            string ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();// 修改ID


            //string strFrom=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")+"微店";
            //string strFrom = "China@tom.com";
            string strTo = Textbox_Email.Text.Trim();
            string strSubject = Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店" + " 电子邮件地址验证！";
            string strBody = "你好，我们给你发信，是因为您在" + Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店" + "进行邮件地址验证引起！" + "\n";
            strBody += "如果这不是你的操作引起的，请忽略掉即可！" + "\n";
            strBody += "请点击如下的连接进行地址验证。如果不能点击，请复制如下连接到浏览器地址栏！" + "\n";

            QueryString_EggSoft QueryString = new QueryString_EggSoft();
            QueryString.Add("ShopID", ShopClientID);
            QueryString.Add("Email", strTo);
            QueryString.Add("MD5ID", Eggsoft.Common.DESCrypt.Crypt(ShopClientID));
            //Response.Redirect("Receive.aspx?data=" + QueryString.ToString());
            strBody += "https://" + HttpContext.Current.Request.Url.Host + "/ClientAdmin/10tab_ShopClient/CheckUserInfo.aspx?type=EmalCheck&data=" + QueryString.ToString();

            string strCityName = Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName") + "微店";
            if (Eggsoft_Public_CL.Pub.SendEmail_AddTask(strCityName, strTo, strSubject, strBody))
            {
                int intPos = strTo.IndexOf('@');
                string strWeb = "http://mail." + strTo.Substring(intPos + 1);


                Eggsoft.Common.JsUtil.ShowMsgNew("发信成功,将访问:" + strWeb, strWeb);
            }
        }
    }
}