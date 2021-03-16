using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class Subscribe : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin BLL_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin();

        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_Subscribe")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                IniReadContent();
            }

        }

        protected void IniReadContent()
        {
            System.Web.UI.WebControls.TextBox myTextBox = new System.Web.UI.WebControls.TextBox();

            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);
            myTextBox.Text = Model_tab_ShopClient_EngineerMode.Subscribe;

            String strSplistText = myTextBox.Text;

            if (strSplistText.IndexOf(",") != -1)
            {
                String[] strList = strSplistText.Split(',');
                String strText = strList[0];
                RadioButtonList_View_Click.SelectedValue = strList[0];
                TextBox_MenuContent.Text = strList[1];
            }
        }



        protected void Button_Save_Click(object sender, EventArgs e)
        {

            string str_SelectedValue_Type = RadioButtonList_View_Click.SelectedValue;

            string ResourceID = TextBox_MenuContent.Text.Trim();
            EggsoftWX.BLL.tab_ShopClient_System_XML_Resource bll_tab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            if (bll_tab_ShopClient_System_XML_Resource.Exists("parentID=0 and ID=" + ResourceID + " and ShopClientID=" + strShopClientID))
            {
                string strType = bll_tab_ShopClient_System_XML_Resource.GetList("type", "parentID=0 and ID=" + ResourceID + " and ShopClientID=" + strShopClientID).Tables[0].Rows[0][0].ToString();
                if (str_SelectedValue_Type != strType)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("目前的素材类型选择错误，系统已自动为你纠正。");
                    RadioButtonList_View_Click.SelectedValue = strType;
                    str_SelectedValue_Type = strType;
                }
            }
            else
            {
                Eggsoft.Common.JsUtil.ShowMsg("素材的ID号不存在，请查询。");
                return;
            }

            System.Web.UI.WebControls.TextBox myTextBox = new System.Web.UI.WebControls.TextBox();
            myTextBox.Text = str_SelectedValue_Type + "," + TextBox_MenuContent.Text.Trim();

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);
            Model_tab_ShopClient_EngineerMode.Subscribe = myTextBox.Text;///关注时回复
            BLL_tab_ShopClient_EngineerMode.Update(Model_tab_ShopClient_EngineerMode);

            Eggsoft.Common.JsUtil.ShowMsg("保存成功！");
        }
    }
}