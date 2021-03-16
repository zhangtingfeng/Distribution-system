using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Agent__AddExpListTextShow_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        public String MenuLink = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];

            try
            {
                if (!IsPostBack)
                {

                    //RadioButtonList1.SelectedValue = "0";
                    // LinkShow.Visible = false;
                    //TextShow.Visible = true;
                    //RadioButton1.Checked = true;
                    //RadioButton2.Checked = false;

                    SetClass();

                }
                else
                {

                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }

        private void SetClass()
        {
            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + strShopClientID);


                #region  ExpListText  custorm your self Item  自定义字段
                if (Model_tab_ShopClient_ShopPar != null)
                {
                    string strAddExpListText = Model_tab_ShopClient_ShopPar.AddExpListTextShow;
                    if ((String.IsNullOrEmpty(strAddExpListText) == false))
                    {
                        string[] strAddExpListTextList = strAddExpListText.Split('#');
                        for (int i = 0; i < strAddExpListTextList.Length; i++)
                        {
                            //if (Model.AddExpListTextShow)
                            //{
                            //    BoundField f_BoundField = new BoundField();
                            //    f_BoundField.DataField = "AddExp" + (i + 1).ToString();
                            //    f_BoundField.HeaderText = strAddExpListTextList[i];
                            //    GridView_ShowAll.Columns.Add(f_BoundField);
                            //}
                            ListBox_Item.Items.Add(strAddExpListTextList[i]);
                        }
                    }
                }

                #endregion


            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string type = Request.QueryString["Type"];


                #region  ExpListText  custorm your self Item  自定义字段
                string strAddExpListText = "";



                for (int i = 0; i < ListBox_Item.Items.Count; i++)
                {
                    if (i > 7) break;
                    strAddExpListText += "#" + ListBox_Item.Items[i].Text.Trim().ToLower();
                }
                if (strAddExpListText.Length > 0)
                {
                    strAddExpListText = strAddExpListText.Remove(0, 1);
                }
                #endregion

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + strShopClientID);
                if (Model_tab_ShopClient_ShopPar != null)
                {
                    Model_tab_ShopClient_ShopPar.AddExpListTextShow = strAddExpListText;
                    BLL_tab_ShopClient_ShopPar.Update(Model_tab_ShopClient_ShopPar);
                }
                else
                {
                    Model_tab_ShopClient_ShopPar = new EggsoftWX.Model.tab_ShopClient_ShopPar();
                    Model_tab_ShopClient_ShopPar.AddExpListTextShow = strAddExpListText;
                    Model_tab_ShopClient_ShopPar.ShopClientID = Int32.Parse(strShopClientID);
                    BLL_tab_ShopClient_ShopPar.Add(Model_tab_ShopClient_ShopPar);
                }
                Eggsoft.Common.JsUtil.ShowMsg("保存成功");

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
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