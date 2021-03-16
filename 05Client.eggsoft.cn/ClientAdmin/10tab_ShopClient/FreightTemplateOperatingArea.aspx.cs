using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class FreightTemplateOperatingArea1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];

                //Link0.Text = "./Default" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + ".aspx";

                if (type.ToLower() == "delete")
                {
                    string strFreightTemplateArea_ID = Request.QueryString["FreightTemplateArea_ID"];
                    if (!CommUtil.IsNumStr(strFreightTemplateArea_ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_FreightTemplate_Area bll = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                    bll.Delete("ID='" + strFreightTemplateArea_ID + "'");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", " <script lanuage=javascript>GetDataAndClose(); </script>");
                    //这一步很关键，就是传说中的后台调用前台脚本，实现了关闭模态对话框的功能，关闭后程序转到父窗口中的前台javascript继续执行代码。    

                    //JsUtil.ShowMsg("删除成功!", "FreightTemplate.aspx");
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
                string strFreightTemplateArea_ID = Request.QueryString["FreightTemplateArea_ID"];// 修改ID
                EggsoftWX.BLL.tab_FreightTemplate_Area bll = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                EggsoftWX.Model.tab_FreightTemplate_Area Model = bll.GetModel("ID=" + strFreightTemplateArea_ID + "");
                Literal_AddArea.Text = getPrArea(Model.AreaList);
                TextBox_BaoYouGeShu.Text = Model.HowmanysNoFreight.ToString();
                TextBox_BaoYouMoney.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.HowmuchNoFreight);

                txtFreight.Text = Model.Freight.ToString();
                txtFreightMore.Text = Model.FreightMore.ToString();
                TextBox_Allkg.Text = Model.HowkgNoFreight.ToString();


                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {
                Literal_AddArea.Text = getPrArea("");
            }
        }

        private string getPrArea(string strList)
        {
            #region 增加一个其他项目的统计  如果出现过 这个是 是灰色的 不能再选
            ArrayList myOthersArrayList = new ArrayList();
            string strFreightTemplate_ID = Request.QueryString["FreightTemplate_ID"];
            string strFreightTemplateArea_ID = Request.QueryString["FreightTemplateArea_ID"];

            EggsoftWX.BLL.tab_FreightTemplate_Area blltab_FreightTemplate_Area = new EggsoftWX.BLL.tab_FreightTemplate_Area();
            System.Data.DataTable Data_DataTable_Area = blltab_FreightTemplate_Area.SelectList("select AreaList from tab_FreightTemplate_Area where id<>" + strFreightTemplateArea_ID + " and FreightTemplate_ID=" + strFreightTemplate_ID).Tables[0];
            for (int i = 0; i < Data_DataTable_Area.Rows.Count; i++)
            {
                string strOLDAreaList = Data_DataTable_Area.Rows[i]["AreaList"].ToString();

                if (String.IsNullOrEmpty(strOLDAreaList) == false)
                {
                    string[] stAreaList = strOLDAreaList.Split(',');
                    int intOldLength = stAreaList.Length;

                    for (int j = 0; j < intOldLength; j++)
                    {
                        myOthersArrayList.Add(stAreaList[j]);
                    }
                }
            }
            #endregion

            ArrayList myArrayList = new ArrayList();
            if (String.IsNullOrEmpty(strList) == false)
            {
                string[] stAreaList = strList.Split(',');
                int intOldLength = stAreaList.Length;

                for (int i = 0; i < intOldLength; i++)
                {
                    myArrayList.Add(stAreaList[i]);
                }
            }


            ArrayList myAllSheng_ID_NameArrayList = new ArrayList();

            string strPreArea = "";
            EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
            System.Data.DataTable Data_DataTable = BLL_tab_PE_Region.SelectList("select CountryArea,min(id) AS idCountryArea from tab_PE_Region group by CountryArea ORDER BY idCountryArea ").Tables[0];
            strPreArea += "   <table style=\"width: 100%;\">\n";
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strCountryArea = Data_DataTable.Rows[i]["CountryArea"].ToString();
                string stridCountryArea = Data_DataTable.Rows[i]["idCountryArea"].ToString();

                strPreArea += "                   <tr>\n";
                strPreArea += "                       <td style=\"width:5%;\" class=\"auto-style2\">\n";
                strPreArea += "                           <label>" + strCountryArea + "</label></td>\n";
                strPreArea += "                       <td class=\"auto-style3\">\n";

                string strSQLEachSheng = "SELECT  Province,min(id) AS idProvince FROM [tab_PE_Region] where CountryArea='" + strCountryArea + "' group by Province   order by idProvince asc";
                System.Data.DataTable Data_DataTableProvince = BLL_tab_PE_Region.SelectList(strSQLEachSheng).Tables[0];
                for (int j = 0; j < Data_DataTableProvince.Rows.Count; j++)
                {
                    string strProvince = Data_DataTableProvince.Rows[j]["Province"].ToString();
                    string stridProvince = Data_DataTableProvince.Rows[j]["idProvince"].ToString();

                    //myAllArrayList.Add(stridProvince);
                    Eggsoft_Public_CL.XML_Sheng_ID_Name mmmXML_Sheng_ID_Name = new Eggsoft_Public_CL.XML_Sheng_ID_Name();
                    mmmXML_Sheng_ID_Name.ShengID = Int32.Parse(stridProvince);
                    mmmXML_Sheng_ID_Name.ShengName = strProvince;
                    myAllSheng_ID_NameArrayList.Add(mmmXML_Sheng_ID_Name);

                    string strChecked = "";
                    string strDisabled = "";
                    string strDelete = "";
                    if (myArrayList.Contains(stridProvince))
                    {
                        strChecked = "Checked";
                    }
                    else if (myOthersArrayList.Contains(stridProvince))
                    {
                        strDisabled = " Disabled=\"Disabled\"";
                        strDelete = " style=\"text-decoration:line-through;\"";

                    }
                    strPreArea += "                           <label" + strDelete + "><input  id=\"Province" + stridProvince + "\" type=\"checkbox\"   " + strChecked + " " + strDisabled + " name=\"Province" + stridProvince + "\"/>" + strProvince + "</label>&nbsp;&nbsp;\n";
                }
                strPreArea += "                           </td>\n";
                strPreArea += "                   </tr>\n";
            }
            strPreArea += "               </table>\n";


            #region 取静态化字符串 有利于前台调用 增强性能
            Eggsoft.Common.ArrayListHelper mmmArrayListHelper = new ArrayListHelper();
            string strArrayListHelper = mmmArrayListHelper.SerializeArrayList(myAllSheng_ID_NameArrayList, typeof(Eggsoft_Public_CL.XML_Sheng_ID_Name));

            string strXML_Sheng_ID_NamePubList = Eggsoft_Public_CL.XML_Sheng_ID_Name.strXML_Sheng_ID_NamePub;
            //反序列化...........
            Type[] extra2 = new Type[1];
            extra2[0] = typeof(Eggsoft_Public_CL.XML_Sheng_ID_Name);
            ArrayList laArrayListHelper = mmmArrayListHelper.DeserializeArrayList(strXML_Sheng_ID_NamePubList, typeof(ArrayList), extra2);
            #endregion  取静态化字符串 有利于前台调用 增强性能

            return strPreArea;
        }


        protected String getAreaList()
        {
            string strStringReturn = "";

            EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
            System.Data.DataTable Data_DataTable = BLL_tab_PE_Region.SelectList("select CountryArea,min(id) AS idCountryArea from tab_PE_Region group by CountryArea ORDER BY idCountryArea ").Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strCountryArea = Data_DataTable.Rows[i]["CountryArea"].ToString();
                string stridCountryArea = Data_DataTable.Rows[i]["idCountryArea"].ToString();

                string strSQLEachSheng = "SELECT  Province,min(id) AS idProvince FROM [tab_PE_Region] where CountryArea='" + strCountryArea + "' group by Province   order by idProvince asc";
                System.Data.DataTable Data_DataTableProvince = BLL_tab_PE_Region.SelectList(strSQLEachSheng).Tables[0];
                for (int j = 0; j < Data_DataTableProvince.Rows.Count; j++)
                {
                    string strProvince = Data_DataTableProvince.Rows[j]["Province"].ToString();
                    string stridProvince = Data_DataTableProvince.Rows[j]["idProvince"].ToString();
                    string strcheckbox_Empowered_ = Request.Form["Province" + stridProvince];

                    if (String.IsNullOrEmpty(strcheckbox_Empowered_) == false)
                    {
                        if (strcheckbox_Empowered_.IndexOf("on") > -1) strcheckbox_Empowered_ = "true";
                    }
                    bool bool_Empowered = false;
                    bool.TryParse(strcheckbox_Empowered_, out bool_Empowered);

                    if (bool_Empowered)
                    {
                        strStringReturn += stridProvince + ",";
                    }
                }
            }

            if (String.IsNullOrEmpty(strStringReturn) == false)
            {
                strStringReturn = strStringReturn.Substring(0, strStringReturn.Length - 1);//去掉逗号
            }
            return strStringReturn;
            //for (int i = 0; i < myDataTable.Rows.Count; i++)
            //{
            //    string ProductID = myDataTable.Rows[i]["ProductID"].ToString();

            //    string strcheckbox_Empowered_ = Request.Form["checkbox_Empowered_Name" + ProductID];
            //}
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                String strStringReturn = getAreaList();
                if (String.IsNullOrEmpty(strStringReturn))
                {
                    JsUtil.ShowMsg("没有做任何选择!", -1);
                }
                else
                {
                    string type = Request.QueryString["type"];
                    if (type.ToLower() == "modify")
                    {
                        string strID = Request.QueryString["FreightTemplateArea_ID"];// 修改ID
                        EggsoftWX.BLL.tab_FreightTemplate_Area bllArea = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                        EggsoftWX.Model.tab_FreightTemplate_Area ModelArea = bllArea.GetModel("ID=" + strID + "");
                        ModelArea.Freight = Convert.ToDecimal(txtFreight.Text);
                        ModelArea.FreightMore = Convert.ToDecimal(txtFreightMore.Text);
                        ModelArea.HowmanysNoFreight = Convert.ToInt32(TextBox_BaoYouGeShu.Text);
                        ModelArea.HowmuchNoFreight = Convert.ToDecimal(TextBox_BaoYouMoney.Text);
                        ModelArea.HowkgNoFreight = Convert.ToDecimal(TextBox_Allkg.Text);


                        ModelArea.UpdateTime = DateTime.Now;
                        ModelArea.AreaList = strStringReturn;
                        bllArea.Update(ModelArea);
                        //JsUtil.ShowMsg("修改成功!", -1);
                    }
                    else
                        if (type.ToLower() == "add")
                        {
                            EggsoftWX.BLL.tab_FreightTemplate_Area bllArea = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                            EggsoftWX.Model.tab_FreightTemplate_Area Model = new EggsoftWX.Model.tab_FreightTemplate_Area();
                            Model.ShopClient_ID = Convert.ToInt32(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users"));
                            Model.AreaList = strStringReturn;
                            Model.Freight = Convert.ToDecimal(txtFreight.Text);
                            Model.FreightMore = Convert.ToDecimal(txtFreightMore.Text);
                            Model.HowmanysNoFreight = Convert.ToInt32(TextBox_BaoYouGeShu.Text);
                            Model.HowmuchNoFreight = Convert.ToDecimal(TextBox_BaoYouMoney.Text);
                            string strFreightTemplateArea_ID = Request.QueryString["FreightTemplate_ID"];
                            Model.FreightTemplate_ID = Int32.Parse(strFreightTemplateArea_ID);
                            Model.HowkgNoFreight = Convert.ToDecimal(TextBox_Allkg.Text);

                            bllArea.Add(Model);
                            // JsUtil.ShowMsg("添加成功!", -1);
                        }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", " <script lanuage=javascript>GetDataAndClose(); </script>");

                    //这一步很关键，就是传说中的后台调用前台脚本，实现了关闭模态对话框的功能，关闭后程序转到父窗口中的前台javascript继续执行代码。    }
                }
            }
            catch
            {
                JsUtil.ShowMsg("出错了，请关闭重试!", -1);

            }
        }
    }
}