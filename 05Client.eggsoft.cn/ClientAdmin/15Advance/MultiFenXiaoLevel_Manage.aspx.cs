using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class MultiFenXiaoLevel_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                    //EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = bll.GetModel(Int32.Parse(ID)+ " and ShopClient_ID="+ strINCID);


                    bll.Update("IsDeleted=1,UpdateTime=getdate(),UpdateBy=@UpdateBy", "ID=" + ID+ " and ShopClient_ID=@ShopClient_ID", strwebuy8_ClientAdmin_Users_ClientUserAccount, strINCID);
                    //bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "MultiFenXiaoLevel_Board.aspx");
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

                txtTitle.Text = Model.Name;
                txtMenuPos.Text = Model.Sort.ToString();
                TextBox_OperationGet.Text = Model.OperationGet.ToString();
                TextBox_OperationGetParent.Text = Model.OperationParentGet.ToString();
                TextBox_OperationGetGrandParent.Text = Model.OperationGrandParentGet.ToString();
                TextBox_FenxiaoParentGet.Text = Model.FenxiaoParentGet.ToString();
                TextBox_FenxiaoGrandParentGet.Text = Model.FenxiaoGrandParentGet.ToString();
                TextBox_FenxiaoGreatParentGet.Text = Model.FenxiaoGreatParentGet.ToString();
                TextBox_ChildGet.Text = Model.ChildGet.ToString();
                TextBox_GrandsonGet.Text = Model.GrandsonGet.ToString();
                TextBox_GreatsonGet.Text = Model.GreatsonGet.ToString();
                CheckBox_ChildGet.Checked = Model.ChildGet_Money.toBoolean();
                CheckBox_GrandsonGet.Checked = Model.Grandson_Money.toBoolean();
                CheckBox_GreatsonGet.Checked = Model.GreatsonGet_Money.toBoolean();
                btnAdd.Text = "保 存";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                decimal decimalMy =0+
                    
                    //TextBox_OperationGet.Text.toDecimal() +
                    //  TextBox_OperationGetParent.Text.toDecimal() +
                    //  TextBox_OperationGetGrandParent.Text.toDecimal() +

       TextBox_FenxiaoParentGet.Text.toDecimal() +
     TextBox_FenxiaoGrandParentGet.Text.toDecimal() +
     TextBox_FenxiaoGreatParentGet.Text.toDecimal() +

     TextBox_ChildGet.Text.toDecimal() +
     TextBox_GrandsonGet.Text.toDecimal() +
       TextBox_GreatsonGet.Text.toDecimal();


                 if ((decimalMy) != 100)
                {
                    JsUtil.ShowMsg("保存失败，总数是"+ decimalMy + ",应该等于100%，", -1);
                    return;
                }



                string ID = Request.QueryString["ID"];// 修改ID

                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = bll.GetModel(Int32.Parse(ID));

                    Model.Sort = Convert.ToInt32(txtMenuPos.Text);
                    Model.Name = txtTitle.Text.Trim();

                  
                    Model.FenxiaoParentGet = TextBox_FenxiaoParentGet.Text.toDecimal();
                    Model.FenxiaoGrandParentGet = TextBox_FenxiaoGrandParentGet.Text.toDecimal();
                    Model.FenxiaoGreatParentGet = TextBox_FenxiaoGreatParentGet.Text.toDecimal();

                    Model.ChildGet = TextBox_ChildGet.Text.toDecimal();
                    Model.GrandsonGet = TextBox_GrandsonGet.Text.toDecimal();
                    Model.GreatsonGet = TextBox_GreatsonGet.Text.toDecimal();

                    Model.ChildGet_Money = CheckBox_ChildGet.Checked;
                    Model.Grandson_Money = CheckBox_GrandsonGet.Checked;
                    Model.GreatsonGet_Money = CheckBox_GreatsonGet.Checked;

                    //CheckBox_ChildGet.Checked = Model.ChildGet_Money.toBoolean();
                    //CheckBox_GrandsonGet.Checked = Model.Grandson_Money.toBoolean();
                    //CheckBox_GreatsonGet.Checked = Model.GreatsonGet_Money.toBoolean();
                    Model.OperationGet = TextBox_OperationGet.Text.toDecimal();
                    Model.OperationParentGet = TextBox_OperationGetParent.Text.toDecimal();
                    Model.OperationGrandParentGet = TextBox_OperationGetGrandParent.Text.toDecimal();


                    Model.UpdateTime = DateTime.Now;
                    Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    bll.Update(Model);
                    JsUtil.ShowMsg("修改成功!", "MultiFenXiaoLevel_Board.aspx");

                }
                else
                    if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel bll = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model = new EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel();

                    Model.Sort = Convert.ToInt32(txtMenuPos.Text);
                    Model.Name = txtTitle.Text.Trim();
                    Model.ShopClient_ID = strINCID.toInt32();
                 

                    Model.FenxiaoParentGet = TextBox_FenxiaoParentGet.Text.toDecimal();
                    Model.FenxiaoGrandParentGet = TextBox_FenxiaoGrandParentGet.Text.toDecimal();
                    Model.FenxiaoGreatParentGet = TextBox_FenxiaoGreatParentGet.Text.toDecimal();

                    Model.ChildGet = TextBox_ChildGet.Text.toDecimal();
                    Model.GrandsonGet = TextBox_GrandsonGet.Text.toDecimal();
                    Model.GreatsonGet = TextBox_GreatsonGet.Text.toDecimal();

                    Model.ChildGet_Money = CheckBox_ChildGet.Checked;
                    Model.Grandson_Money = CheckBox_GrandsonGet.Checked;
                    Model.GreatsonGet_Money = CheckBox_GreatsonGet.Checked;

                    Model.OperationGet = TextBox_OperationGet.Text.toDecimal();
                    Model.OperationParentGet = TextBox_OperationGetParent.Text.toDecimal();
                    Model.OperationGrandParentGet = TextBox_OperationGetGrandParent.Text.toDecimal();


                    Model.CreatTime = DateTime.Now;
                    Model.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "MultiFenXiaoLevel_Board.aspx");

                }
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione.Message,"后台保存新型分销方案");
            }

            finally
            {

            }
        }
    }
}