using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class FenXiaoLevel_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
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
                    EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "FenXiaoLevel_Board.aspx");
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
                EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = bll.GetModel(Int32.Parse(ID));

                //txtTitle.Text = Model.Name;
                //txtMenuPos.Text = Model.Sort.ToString();
                //TextBox_LevelPercent.Text = Model.LevelPercent.ToString();
                btnAdd.Text = "保 存";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string ID = Request.QueryString["ID"];// 修改ID

                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = bll.GetModel(Int32.Parse(ID));

                    Model.Sort = Convert.ToInt32(txtMenuPos.Text);
                    Model.Name = txtTitle.Text.Trim();
                    //Model.LevelPercent = Int32.Parse(TextBox_LevelPercent.Text);

                    Model.UpdateTime = DateTime.Now;

                    bll.Update(Model);
                    JsUtil.ShowMsg("修改成功!", "FenXiaoLevel_Board.aspx");

                }
                else
                    if (type.ToLower() == "add")
                    {
                        EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                        EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = new EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel();

                        Model.Sort = Convert.ToInt32(txtMenuPos.Text);
                        Model.Name = txtTitle.Text.Trim();
                        //Model.LevelPercent = Int32.Parse(TextBox_LevelPercent.Text);
                        Model.ShopClient_ID = Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                        Model.UpdateTime = DateTime.Now;

                        bll.Add(Model);
                        JsUtil.ShowMsg("添加成功!", "FenXiaoLevel_Board.aspx");

                    }
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione.Message);
            }

            finally
            {

            }
        }
    }
}