using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class FreightTemplateOperating1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public String strText_Shopping_Vouchers_Start = "";
        public String strText_Shopping_Vouchers_End = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnRefresh_Click(sender, e);

            }
        }

        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_FreightTemplate bll = new EggsoftWX.BLL.tab_FreightTemplate();
                EggsoftWX.Model.tab_FreightTemplate Model = bll.GetModel("ID=" + strID + "");
                lblID.Text = Model.ID.ToString();
                txtFreight.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.Freight);
                txtFreightMore.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.FreightMore);
                txtRemarks.Text = Model.Remarks;
                TextBox_Name.Text = Model.Name;
                TextBox_BaoYouGeShu.Text = Model.HowmanysNoFreight.ToString();
                TextBox_BaoYouMoney.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.HowmuchNoFreight);
                TextBox_Allkg.Text = Model.HowkgNoFreight.ToString();

                btnAdd.Text = "保 存";
                //DisPlayStatus_New_None = "";

                LiteraldisplayOLDClass.Text = displayOLDClass();

                if (string.IsNullOrEmpty(Model.Province) == false)
                {
                    DropDownList_Area1.SelectedValue = Model.Province;
                    DropDownList_Area1_SelectedIndexChanged(sender, e);
                }
                if (string.IsNullOrEmpty(Model.City) == false)
                {
                    DropDownList_Area2.SelectedValue = Model.City;
                    DropDownList_Area2_SelectedIndexChanged(sender, e);
                    DropDownList_Area2.Visible = true;
                }
                else
                {
                    DropDownList_Area2.Visible = false;
                }

                if (string.IsNullOrEmpty(Model.Area) == false)
                {
                    DropDownList_Area3.SelectedValue = Model.Area;
                    DropDownList_Area3.Visible = true;
                }
                else
                {
                    DropDownList_Area3.Visible = false;
                }
            }
            else
            {
                DropDownList_Area1_SelectedIndexChanged(sender, e);
                //DisPlayStatus_New_None = "display:none;";
            }
        }




        protected void DropDownList_Area1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSQLEachCity = "SELECT  City,min(id) AS ID FROM [tab_PE_Region] where Province='" + DropDownList_Area1.SelectedItem.Text + "' group by City   order by id asc";
            DropDownList_Area2.DataSource = new EggsoftWX.BLL.tab_PE_Region().SelectList(strSQLEachCity);
            DropDownList_Area2.DataTextField = "City";
            DropDownList_Area2.DataValueField = "ID";
            DropDownList_Area2.DataBind();
            if (DropDownList_Area2.Items.Count > 1)
            {
                DropDownList_Area2.Visible = true;
            }
            else
            {
                DropDownList_Area2.Visible = false;
            }
            DropDownList_Area2_SelectedIndexChanged(sender, e);
        }
        protected void DropDownList_Area2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSQLEachArea = "SELECT  Area,min(id) AS ID FROM [tab_PE_Region] where City='" + DropDownList_Area2.SelectedItem.Text + "' group by Area order by id asc";
            DropDownList_Area3.DataSource = new EggsoftWX.BLL.tab_PE_Region().SelectList(strSQLEachArea);
            DropDownList_Area3.DataTextField = "Area";
            DropDownList_Area3.DataValueField = "ID";
            DropDownList_Area3.DataBind();

            if (DropDownList_Area3.Items.Count > 1)
            {
                DropDownList_Area3.Visible = true;
            }
            else
            {
                DropDownList_Area3.Visible = false;
            }
        }


        private String displayOLDClass()
        {
            string strdisplayOLDClass = "";
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_FreightTemplate_Area blltab_FreightTemplate_Area = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                System.Data.DataTable myDataTable_Area = blltab_FreightTemplate_Area.GetList("FreightTemplate_ID=" + strID + " order by id asc").Tables[0];

                EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
                EggsoftWX.Model.tab_PE_Region Model_tab_PE_Region = new EggsoftWX.Model.tab_PE_Region();


                for (int i = 0; i < myDataTable_Area.Rows.Count; i++)
                {
                    string strAreaID = myDataTable_Area.Rows[i]["ID"].ToString();
                    string strFreight = myDataTable_Area.Rows[i]["Freight"].ToString();
                    string strFreightMore = myDataTable_Area.Rows[i]["FreightMore"].ToString();
                    string strHowmanysNoFreight = myDataTable_Area.Rows[i]["HowmanysNoFreight"].ToString();
                    string strHowmuchNoFreight = myDataTable_Area.Rows[i]["HowmuchNoFreight"].ToString();
                    string strAreaList = myDataTable_Area.Rows[i]["AreaList"].ToString();
                    string strHowkgNoFreight = myDataTable_Area.Rows[i]["HowkgNoFreight"].ToString();

                    string strThisSheng = "";
                    if (String.IsNullOrEmpty(strAreaList) == false)
                    {
                        string[] strLengthList = strAreaList.Split(',');
                        for (int j = 0; j < strLengthList.Length; j++)
                        {
                            string strSheng = strLengthList[j];
                            int intSheng = Int32.Parse(strSheng);
                            if (intSheng > 0)
                            {
                                Model_tab_PE_Region = BLL_tab_PE_Region.GetModel(Int32.Parse(strSheng));
                                if (Model_tab_PE_Region != null)
                                {
                                    if (String.IsNullOrEmpty(strThisSheng) == false) strThisSheng += ",";
                                    strThisSheng += Model_tab_PE_Region.Province;
                                }
                            }
                        }
                    }

                    //           ,[Name]
                    //,[ShopClient_ID]
                    //,[Freight]
                    //,[FreightMore]
                    //,[UpdateTime]
                    //,[CreateTime]
                    //,[Remarks]
                    //,[AreaList]
                    //,[FreightTemplate_ID]

                    strdisplayOLDClass += "  <tr class=\"tdbg\" style=\"\" bgcolor=\"#e3e3e3\">\n";
                    strdisplayOLDClass += " <td class=\"style2\" align=\"right\" width=\"150\" height=\"35\">\n";
                    strdisplayOLDClass += "     <strong>" + strThisSheng + "</strong>\n";
                    strdisplayOLDClass += " </td>\n";
                    strdisplayOLDClass += " <td bgcolor=\"#ecf5ff\">\n";
                    strdisplayOLDClass += "     <table style=\"width: 100%;\">\n";
                    strdisplayOLDClass += "        <tr>\n";
                    strdisplayOLDClass += "            <td style=\"width: 15%;\">首件运费</td>\n";
                    strdisplayOLDClass += "            <td>\n";
                    strdisplayOLDClass += "                <span>" + strFreight + "元</span>\n";
                    strdisplayOLDClass += "            </td>\n";
                    strdisplayOLDClass += "        </tr>\n";
                    strdisplayOLDClass += "       <tr>\n";
                    strdisplayOLDClass += "           <td style=\"width: 15%;\">每添加一件商品：</td>\n";
                    strdisplayOLDClass += "           <td>\n";
                    strdisplayOLDClass += "               <span>" + strFreightMore + "元</span>\n";
                    strdisplayOLDClass += "           </td>\n";
                    strdisplayOLDClass += "       </tr>\n";
                    strdisplayOLDClass += "       <tr>\n";
                    strdisplayOLDClass += "           <td style=\"width: 15%;\">满多少重量包邮：</td>\n";
                    strdisplayOLDClass += "           <td>\n";
                    strdisplayOLDClass += "               <span>" + strHowkgNoFreight + "公斤</span>\n";
                    strdisplayOLDClass += "           </td>\n";
                    strdisplayOLDClass += "       </tr>\n";
                    strdisplayOLDClass += "       <tr>\n";
                    strdisplayOLDClass += "           <td style=\"width: 15%;\">满多少钱包邮：</td>\n";
                    strdisplayOLDClass += "           <td>\n";
                    strdisplayOLDClass += "               <span>" + strHowmuchNoFreight + "元</span>\n";
                    strdisplayOLDClass += "           </td>\n";
                    strdisplayOLDClass += "       </tr>\n";
                    strdisplayOLDClass += "       <tr>\n";
                    strdisplayOLDClass += "           <td style=\"width: 15%;\">满多少件包邮：</td>\n";
                    strdisplayOLDClass += "           <td>\n";
                    strdisplayOLDClass += "               <span>" + strHowmanysNoFreight + "件</span>\n";
                    strdisplayOLDClass += "           </td>\n";
                    strdisplayOLDClass += "       </tr>\n";
                    strdisplayOLDClass += "       <tr>\n";
                    strdisplayOLDClass += "           <td colspan=2>\n";
                    strdisplayOLDClass += "        <input id=\"ButtonAdd" + strAreaID + "\" type=\"button\" onclick='showdialogAddShengArea(\"" + strAreaID + "\");' value=\"编辑\" />\n";
                    strdisplayOLDClass += "        &nbsp;&nbsp;&nbsp;&nbsp;<input id=\"ButtonDelete" + strAreaID + "\" type=\"button\" onclick='DeleteAddShengArea(\"" + strAreaID + "\");' value=\"删除\" />\n";
                    strdisplayOLDClass += "           </td>\n";
                    strdisplayOLDClass += "       </tr>\n";
                    strdisplayOLDClass += "   </table>\n";
                    strdisplayOLDClass += "</td>\n";
                    strdisplayOLDClass += " </tr>\n";
                }




            }
            return strdisplayOLDClass;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string strID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_FreightTemplate bll = new EggsoftWX.BLL.tab_FreightTemplate();
                EggsoftWX.Model.tab_FreightTemplate Model = bll.GetModel("ID=" + strID + "");

                Model.ID = Convert.ToInt32(lblID.Text);
                Model.ShopClient_ID = Convert.ToInt32(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users"));
                Model.Freight = Convert.ToDecimal(txtFreight.Text);
                Model.FreightMore = Convert.ToDecimal(txtFreightMore.Text);
                Model.UpdateTime = DateTime.Now;
                Model.Remarks = txtRemarks.Text;
                Model.Name = TextBox_Name.Text;
                Model.HowmanysNoFreight = Convert.ToInt32(TextBox_BaoYouGeShu.Text);
                Model.HowmuchNoFreight = Convert.ToDecimal(TextBox_BaoYouMoney.Text);
                Model.Province = DropDownList_Area1.SelectedItem.Value;
                Model.HowkgNoFreight = Convert.ToDecimal(TextBox_Allkg.Text);

                if (DropDownList_Area2.SelectedItem != null) Model.City = DropDownList_Area2.SelectedItem.Value;
                Model.Area = DropDownList_Area3.Visible ? DropDownList_Area3.SelectedItem.Value : "";


                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "FreightTemplate.aspx");
            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_FreightTemplate bll = new EggsoftWX.BLL.tab_FreightTemplate();
                    EggsoftWX.Model.tab_FreightTemplate Model = new EggsoftWX.Model.tab_FreightTemplate();
                    Model.ShopClient_ID = Convert.ToInt32(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users"));
                    Model.Freight = Convert.ToDecimal(txtFreight.Text);
                    Model.FreightMore = Convert.ToDecimal(txtFreightMore.Text);
                    Model.Remarks = txtRemarks.Text;
                    Model.Name = TextBox_Name.Text;
                    Model.HowmanysNoFreight = Convert.ToInt32(TextBox_BaoYouGeShu.Text);
                    Model.HowmuchNoFreight = Convert.ToDecimal(TextBox_BaoYouMoney.Text);
                    Model.Province = DropDownList_Area1.SelectedItem.Value;
                    Model.City = DropDownList_Area2.SelectedItem.Value;
                    Model.Area = DropDownList_Area3.Visible ? DropDownList_Area3.SelectedItem.Value : "";
                    Model.HowkgNoFreight = Convert.ToDecimal(TextBox_Allkg.Text);

                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "FreightTemplate.aspx");
                }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];

            //Link0.Text = "./Default" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + ".aspx";

            if (type.ToLower() == "delete")
            {
                string strVouchersNum = Request.QueryString["ID"];
                if (!CommUtil.IsNumStr(strVouchersNum))
                    MyError.ThrowException("传递参数错误!");
                EggsoftWX.BLL.tab_FreightTemplate bll = new EggsoftWX.BLL.tab_FreightTemplate();
                bll.Delete("ID='" + strVouchersNum + "'");


                EggsoftWX.BLL.tab_Goods bll_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                bll_tab_Goods.Update("FreightTemplate_ID=0", "FreightTemplate_ID=" + strVouchersNum);

                EggsoftWX.BLL.tab_FreightTemplate_Area blltab_FreightTemplate_Area = new EggsoftWX.BLL.tab_FreightTemplate_Area();
                blltab_FreightTemplate_Area.Delete("FreightTemplate_ID=" + strVouchersNum);

                JsUtil.ShowMsg("删除成功!", "FreightTemplate.aspx");
            }
            else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
            {
                string strSQLEachSheng = "SELECT  Province,min(id) AS ID FROM [tab_PE_Region] where Country='中华人民共和国' group by Province   order by id asc";
                DropDownList_Area1.DataSource = new EggsoftWX.BLL.tab_PE_Region().SelectList(strSQLEachSheng);
                DropDownList_Area1.DataTextField = "Province";
                DropDownList_Area1.DataValueField = "ID";
                DropDownList_Area1.DataBind();

                SetClass(sender, e);
            }
        }
    }
}