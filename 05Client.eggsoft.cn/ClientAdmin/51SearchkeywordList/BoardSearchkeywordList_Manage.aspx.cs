using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._51SearchkeywordList
{
    public partial class BoardSearchkeywordList_Manage : System.Web.UI.Page
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
                    EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList bll = new EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList();

                    EggsoftWX.Model.tab_ShopClient_SearchKeyWordList Model = bll.GetModel(Int32.Parse(ID));//删除文件

                    bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "BoardSearchkeywordList.aspx");
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
                EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList bll = new EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList();
                EggsoftWX.Model.tab_ShopClient_SearchKeyWordList Model = bll.GetModel(Int32.Parse(ID));

                txtKeyword.Text = Model.Keyword;
                TextBoxKeywordCount.Text = Model.KeywordCount.ToString();
                TextBoxSearchArea.Text = Model.SearchArea.ToString();
                TextBoxSearchUserNickName.Text = Model.SearchUserNickName.ToString();

                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                //string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                //EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                //EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(strID));
                //txtTitle.Text = Model.ShopClientName;///默认一个数值
                //Link0.Text = "http://" + Model.ErJiYuMing;
            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList bll = new EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList();
                EggsoftWX.Model.tab_ShopClient_SearchKeyWordList Model = bll.GetModel(Int32.Parse(ID));



                Model.Keyword = txtKeyword.Text.Trim();
                Model.KeywordCount = Convert.ToInt32(TextBoxKeywordCount.Text.Trim());
                Model.UpdateTime = DateTime.Now;

                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "BoardSearchkeywordList_Manage.aspx");

            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList bll = new EggsoftWX.BLL.tab_ShopClient_SearchKeyWordList();
                    EggsoftWX.Model.tab_ShopClient_SearchKeyWordList Announce = new EggsoftWX.Model.tab_ShopClient_SearchKeyWordList();

                    Announce.Keyword = txtKeyword.Text.Trim();
                    Announce.KeywordCount = Convert.ToInt32(TextBoxKeywordCount.Text.Trim());
                    Announce.UpdateTime = DateTime.Now;
                    Announce.InsertTime = DateTime.Now;
                    bll.Add(Announce);
                    JsUtil.ShowMsg("添加成功!", "BoardSearchkeywordList.aspx");
                }
        }
    }
}