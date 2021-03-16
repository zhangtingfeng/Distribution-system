using Eggsoft.Common;
using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class Suggestion : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_Suggestion_By_Qiu Suggestion_bll = new EggsoftWX.BLL.tab_Suggestion_By_Qiu();
        private EggsoftWX.Model.tab_Suggestion_By_Qiu Suggestion_Model = new EggsoftWX.Model.tab_Suggestion_By_Qiu();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];

                if (type != null && type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    Suggestion_bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "Suggestion_List.aspx");
                }
                else if ((type != null && type.ToLower() == "modify"))
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.Model.tab_Suggestion_By_Qiu Model = Suggestion_bll.GetModel(Int32.Parse(ID));
                    Id.Text = ID;
                    Title.Text = Model.Title;
                    Content.Text = Model.Content;

                }
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strAdd = "";
            int saveMode = SaveSuggestion();
            //        SaveMode();
            if (saveMode == 0)
            {
                strAdd = "添加";
            }
            else
            {
                strAdd = "修改";
            }
            string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Int32.Parse(str_Pub_ShopClientID));
            if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
            {
                XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                if (myXML__Class_Shop_Client.CheckEmail == true)
                {
                    string strTo = myXML__Class_Shop_Client.Email;
                    string strSubject = "尊敬的商户：" + my_Model_tab_ShopClient.ShopClientName + "," + tab_System_And_.getTab_System("CityName") + "已收到你的" + strAdd + "意见反馈！";
                    string strBody = "你好，我们给你发信，是因为意见反馈通知引起！" + "\n";
                    strBody += "微店意见反馈标题：" + Title.Text.Trim() + "\n";
                    strBody += "意见反馈内容：" + Content.Text.Trim() + "\n";
                    strBody += "对于优秀的反馈意见，我们将给予使用期延长等回馈" + "\n";

                    string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + tab_System_And_.getTab_System("CityName") + "官方技术小组", strTo, strSubject, strBody);

                }
            }
            if (saveMode == 0)
            {
                JsUtil.ShowMsg("添加成功!", "Suggestion_List.aspx");
            }
            else
            {
                JsUtil.ShowMsg("修改成功!", "Suggestion_List.aspx");
            }

        }
        private int SaveSuggestion()
        {
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string title = Title.Text.Trim();
            string content = Content.Text.Trim();

            Suggestion_Model.ShopClientID = Int32.Parse(strINCID);
            Suggestion_Model.Title = title;
            Suggestion_Model.Content = content;
            string id = Id.Text.Trim();
            if (id == null || id.Length == 0)
            {
                Suggestion_bll.Add(Suggestion_Model);
                return 0;
            }
            else
            {
                Suggestion_Model.ID = Int32.Parse(id);
                Suggestion_bll.Update(Suggestion_Model);
                return 1;
            }
        }

    }
}