using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class Resource_1 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected string strLinkGoodlist = "";
        EggsoftWX.BLL.tab_ShopClient_System_XML_Resource myBLLnew = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();



        protected void Page_Load(object sender, EventArgs e)
        {  
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("Devolopment_Resource")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            ViewState["pageIndex"] = Request.QueryString["pageIndex"];
            string strType = Request.QueryString["strType"];
            if ((strType == null) || (strType == "")) strType = "1";
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            AspNetPager1.RecordCount = myBLLnew.ExistsCount("type=1 and ShopClientID=" + strINCID);

            AspNetPager1.UrlRewritePattern = "Resource-1.aspx?strType=" + strType + "&pageIndex={0}";

            if (strType == "1")
            {
                //btnIntroduction_Click(btnIntroduction1, e);

            }

        }

        protected void ImageButton_ADD_Click(object sender, EventArgs e)
        {
            EggsoftWX.Model.tab_ShopClient_System_XML_Resource myModelnew = new EggsoftWX.Model.tab_ShopClient_System_XML_Resource();
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            myModelnew.ShopClientID = Int32.Parse(strINCID);
            myModelnew.type = 1;
            if (TextBox_ADD.Text.Length == 0)
            {
                Eggsoft.Common.JsUtil.ShowMsg("保存对象不能为空！");
                TextBox_ADD.Focus();
                Eggsoft.Common.JsUtil.LocationNewHref(Request.RawUrl);
                return;
            }
            myModelnew.Text = Server.HtmlEncode(TextBox_ADD.Text);

            myBLLnew.Add(myModelnew);
            string strType = Request.QueryString["strType"];
            if ((strType == null) || (strType == "")) strType = "1";

            Eggsoft.Common.JsUtil.LocationNewHref("Resource-1.aspx?strType=" + strType);
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            int intpageIndex = 0;
            if (ViewState["pageIndex"] != null)
            {
                string strpageIndex = ViewState["pageIndex"].ToString();
                intpageIndex = Convert.ToInt32(strpageIndex);
            }
            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strWhere = " and type=1 and ShopClientID=" + strINCID;

            AspNetPager1.CurrentPageIndex = intpageIndex;
            //gvUser.DataSource = 

            System.Data.DataTable myDataTable = myBLLnew.GetPageDataTable(intpageIndex, AspNetPager1.PageSize, "ID,Text,UpdateTime", strWhere, "ID", true);


            //string strNav = "";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                TableRow myTableRow = new TableRow();

                string strID = myDataTable.Rows[i]["ID"].ToString();
                string strText = Server.HtmlDecode(myDataTable.Rows[i]["Text"].ToString());
                string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();


                TextBox myTextBox = new TextBox();
                myTextBox.Text = strText;
                myTextBox.ID = "NewmyTextBox" + strID;
                myTextBox.CssClass = "picCss100Percent";
                myTextBox.Height = 200;
                myTextBox.ToolTip = "这里修改文本消息！";

                myTextBox.TextMode = TextBoxMode.MultiLine;

                TableCell myTableCell = new TableCell();
                myTableCell.HorizontalAlign = HorizontalAlign.Center;
                myTableCell.VerticalAlign = VerticalAlign.Middle;
                myTableCell.Controls.Add(myTextBox);
                myTableRow.Cells.Add(myTableCell);

                string strIMG = "<IMG title=\"" + "这里保存响应的文本修改！" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='saveThis(" + strID + ")' src=\"../skin/Images/save.png\">";
                string strDeleteIMG = "<IMG title=\"" + "这里删除响应的文本消息！" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='deleteThis(" + strID + ")' src=\"../skin/Images/Delete.png\">";

                TableCell myButtonTableCell = new TableCell();

                myButtonTableCell.HorizontalAlign = HorizontalAlign.Center;
                myButtonTableCell.VerticalAlign = VerticalAlign.Middle;
                myButtonTableCell.CssClass = "mymiddle";
                myButtonTableCell.Text = "ID:" + strID + "<br /><br />" + strIMG + "<br /><br />" + strDeleteIMG;

                myTableRow.Cells.Add(myButtonTableCell);

                Table_Show.Rows.Add(myTableRow);
            }
            intpageIndex++;
        }
    }
}