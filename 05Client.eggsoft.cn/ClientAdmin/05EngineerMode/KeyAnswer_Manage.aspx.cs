using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class KeyAnswer_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer BLL_tab_ShopClient_EngineerMode_KeyAnswer = new EggsoftWX.BLL.tab_ShopClient_EngineerMode_KeyAnswer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    BLL_tab_ShopClient_EngineerMode_KeyAnswer.Delete(Convert.ToInt32(ID));
                    JsUtil.ShowMsg("删除成功!", "KeyAnswer_Board.aspx");
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
                EggsoftWX.Model.tab_ShopClient_EngineerMode_KeyAnswer Model = BLL_tab_ShopClient_EngineerMode_KeyAnswer.GetModel(Int32.Parse(ID));


                String strMarkerContent = Model.MarkerContent;

                //strMarkerContent = strMarkerContent.Replace("#$#$#", "@");
                //String[] strList = strMarkerContent.Split('@');
                string[] strList = strMarkerContent.Split(new char[5] { '#', '$', '#', '$', '#' }, StringSplitOptions.RemoveEmptyEntries);
                TextBox_KeyName.Text = Model.Marker;
                RadioButtonList_View_Click.SelectedValue = strList[0];
                TextBox_MenuContent.Text = strList[1];


                //RequiredFieldValidator3.Enabled = false;
            }
        }


        protected void Button_Save_Click(object sender, EventArgs e)
        {
            string str_SelectedValue_Type = RadioButtonList_View_Click.SelectedValue;
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string ResourceID = TextBox_MenuContent.Text.Trim();
            EggsoftWX.BLL.tab_ShopClient_System_XML_Resource bll_tab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();
            if (bll_tab_ShopClient_System_XML_Resource.Exists("ID=" + ResourceID + " and ShopClientID=" + strShopClientID))
            {
                string strType = bll_tab_ShopClient_System_XML_Resource.GetList("type", "ID=" + ResourceID).Tables[0].Rows[0][0].ToString();
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

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_EngineerMode_KeyAnswer Model = BLL_tab_ShopClient_EngineerMode_KeyAnswer.GetModel(Int32.Parse(ID));
                Model.Marker = TextBox_KeyName.Text.Trim();
                Model.MarkerContent = RadioButtonList_View_Click.SelectedValue + "#$#$#" + TextBox_MenuContent.Text;
                Model.UpdateTime = DateTime.Now;

                BLL_tab_ShopClient_EngineerMode_KeyAnswer.Update(Model);
                JsUtil.ShowMsg("修改成功!", "KeyAnswer_Board.aspx");

            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.Model.tab_ShopClient_EngineerMode_KeyAnswer Model = new EggsoftWX.Model.tab_ShopClient_EngineerMode_KeyAnswer();
                    Model.Marker = TextBox_KeyName.Text.Trim();
                    Model.MarkerContent = RadioButtonList_View_Click.SelectedValue + "#$#$#" + TextBox_MenuContent.Text;
                    Model.UpdateTime = DateTime.Now;
                    Model.ShopClientID = Int32.Parse(strShopClientID);
                    BLL_tab_ShopClient_EngineerMode_KeyAnswer.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "KeyAnswer_Board.aspx");

                }
        }
    }
}