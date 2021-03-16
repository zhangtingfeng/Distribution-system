using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class Good_XML_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        EggsoftWX.BLL.tab_Goods_XML bll_tab_Goods_XML = new EggsoftWX.BLL.tab_Goods_XML();
        EggsoftWX.BLL.tab_Goods_XML_Goods_ID bll_tab_Goods_XML_Goods_ID = new EggsoftWX.BLL.tab_Goods_XML_Goods_ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ini_CheckBoxList_GoodList();

                string type = Request.QueryString["type"];


                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    bll_tab_Goods_XML.Delete(Int32.Parse(ID));
                    bll_tab_Goods_XML_Goods_ID.Delete("XMLName_ID=" + ID);
                    JsUtil.ShowMsg("删除成功!", "BoardGood_XML.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void ini_CheckBoxList_GoodList()
        {


            string strWhere = " ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + "  and isdeleted=0 order by ID asc";

            System.Data.DataTable DataTable1 = bll_tab_Goods.GetList(strWhere).Tables[0];

            for (int i = 0; i < DataTable1.Rows.Count; i++)
            {
                string strID = DataTable1.Rows[i]["ID"].ToString();
                string strGoodName = DataTable1.Rows[i]["Name"].ToString();

                ListItem ListItemNew = new ListItem(strID + " " + strGoodName, strID);
                CheckBoxList_ini_GoodList.Items.Add(ListItemNew);

            }

        }

        private void SetClass()
        {



            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_Goods_XML Model = bll_tab_Goods_XML.GetModel(Int32.Parse(ID));

                txtName.Text = Model.XMLName;
                txtXML.Text = Model.XMLContent;


                string strWhere = " XMLName_ID=" + ID + "  order by ID asc";

                System.Data.DataTable DataTable1 = bll_tab_Goods_XML_Goods_ID.GetList(strWhere).Tables[0];

                for (int i = 0; i < CheckBoxList_ini_GoodList.Items.Count; i++)
                {

                    string strValue = CheckBoxList_ini_GoodList.Items[i].Value;
                    for (int j = 0; j < DataTable1.Rows.Count; j++)
                    {
                        string strGoodID = DataTable1.Rows[j]["GoodID"].ToString();
                        if (strGoodID == strValue)
                        {
                            CheckBoxList_ini_GoodList.Items[i].Selected = true;
                            break;
                        }
                    }

                }
            }
            else if (type.ToLower() == "add")
            {
                #region   read xml
                Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries ShopClient_Dictionaries = new Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries();
                string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(ShopClient_Dictionaries, System.Text.Encoding.UTF8);
                txtXML.Text = strXML;
                #endregion
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string ID = Request.QueryString["ID"];// 修改ID

                String strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                string strXML = txtXML.Text.Trim();
                string type = Request.QueryString["type"];

                int int_bll_tab_Goods_XML_ID = 0;
                if (type.ToLower() == "modify")
                {
                    int_bll_tab_Goods_XML_ID = Int32.Parse(Request.QueryString["ID"]);// 修改ID
                    EggsoftWX.Model.tab_Goods_XML Model = bll_tab_Goods_XML.GetModel(int_bll_tab_Goods_XML_ID);
                    Model.XMLName = txtName.Text;
                    Model.XMLContent = strXML;
                    Model.UpdateTime = DateTime.Now;
                    bll_tab_Goods_XML.Update(Model);

                }
                else if (type.ToLower() == "add")
                {
                    //EggsoftWX.BLL.tab_Goods_Class bll = new EggsoftWX.BLL.tab_Goods_Class();
                    EggsoftWX.Model.tab_Goods_XML Model_tab_Goods_XML = new EggsoftWX.Model.tab_Goods_XML();

                    Model_tab_Goods_XML.ShopClient_ID = Int32.Parse(strINCID);
                    Model_tab_Goods_XML.XMLName = txtName.Text;
                    Model_tab_Goods_XML.XMLContent = strXML;
                    Model_tab_Goods_XML.UpdateTime = DateTime.Now;
                    int_bll_tab_Goods_XML_ID = bll_tab_Goods_XML.Add(Model_tab_Goods_XML);
                }

                #region goodid
                bll_tab_Goods_XML_Goods_ID.Delete("XMLName_ID=" + int_bll_tab_Goods_XML_ID);
                EggsoftWX.Model.tab_Goods_XML_Goods_ID Model_tab_Goods_XML_Goods_ID = new EggsoftWX.Model.tab_Goods_XML_Goods_ID();


                for (int i = 0; i < CheckBoxList_ini_GoodList.Items.Count; i++)
                {
                    bool boolID = CheckBoxList_ini_GoodList.Items[i].Selected;
                    string strGoodID = CheckBoxList_ini_GoodList.Items[i].Value;

                    if (boolID)
                    {
                        Model_tab_Goods_XML_Goods_ID.GoodID = Int32.Parse(strGoodID);
                        Model_tab_Goods_XML_Goods_ID.XMLName_ID = int_bll_tab_Goods_XML_ID;
                        Model_tab_Goods_XML_Goods_ID.UpdateTime = DateTime.Now;
                        bll_tab_Goods_XML_Goods_ID.Add(Model_tab_Goods_XML_Goods_ID);
                    }
                }

                JsUtil.ShowMsg("修改成功!", "BoardGood_XML.aspx");

                #endregion

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