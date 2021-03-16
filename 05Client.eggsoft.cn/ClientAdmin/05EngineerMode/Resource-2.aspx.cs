using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class Resource_2 : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected string strLinkGoodlist = "";
        EggsoftWX.BLL.tab_ShopClient_System_XML_Resource myBLLnew = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();



        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["pageIndex"] = Request.QueryString["pageIndex"];
            int intpageIndex = 0;
            if (ViewState["pageIndex"] != null)
            {
                string strpageIndex = ViewState["pageIndex"].ToString();
                intpageIndex = Convert.ToInt32(strpageIndex);
            }
            if (intpageIndex > 1)
            {//第二页不要这个
                TableCell1_New.Visible = false;
                TableCell2_New.Visible = false;
            }

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            AspNetPager1.RecordCount = myBLLnew.ExistsCount("type=2 and ShopClientID=" + strINCID);

            AspNetPager1.UrlRewritePattern = "Resource-2.aspx?strType=" + "2" + "&pageIndex={0}";

            string strAddNew = Eggsoft.Common.Session.Read("AddNewURL");

            if ((strAddNew != "") && (strAddNew != null))//传来了新增的图
            {
                strAddNew = strAddNew.Replace("'", "");
                strAddNew = strAddNew.Replace("\"", "");

                Eggsoft.Common.Session.Delete("AddNewURL");
                Image_Add_New.ImageUrl = strAddNew;
            }
            Eggsoft.Common.Session.Delete("UploadMarker");//上传 文件 来回跳转的标志



            # region 检测是否新建那个图形更新了
            String strType = Request.QueryString["type"];
            if (strType != null)
            {
                if (strType == "ModifySelectThisJPG")
                {
                    String strResourceID = Request.QueryString["ResourceID"];
                    String strFormIMAGEURL = Request.Form["TextContent"];
                    if (strResourceID != null)//可能是单击按钮时的回调

                        if (strResourceID == "-1")//可能是单击按钮时的回调
                        {

                            if (strFormIMAGEURL != null)//可能是单击按钮时的回调
                            {
                                string strNewPic = strFormIMAGEURL.Replace("'", "");
                                Image_Add_New.ImageUrl = strNewPic;
                            }
                        }
                }

            }
            #endregion
        }


        //protected void btnIntroduction_Click1(object sender, EventArgs e)
        //{
        //    Eggsoft.Common.JsUtil.LocationNewHref("Resource-1.aspx");

        //}
        //protected void btnIntroduction_Click3(object sender, EventArgs e)
        //{
        //    Eggsoft.Common.JsUtil.LocationNewHref("Resource-3.aspx");

        //}


        protected void ImageButton_ADD_Click(object sender, EventArgs e)
        {
            EggsoftWX.Model.tab_ShopClient_System_XML_Resource myModelnew = new EggsoftWX.Model.tab_ShopClient_System_XML_Resource();
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            myModelnew.ShopClientID = Int32.Parse(strShopClientID);
            myModelnew.type = 2;
            if (TextBox_ADD_New.Text.Length == 0)
            {
                Eggsoft.Common.JsUtil.ShowMsg("保存对象不能为空！");
                TextBox_ADD_New.Focus();
                Eggsoft.Common.JsUtil.LocationNewHref(Request.RawUrl);
                return;
            }
            if ((Image_Add_New.ImageUrl == "") || (Image_Add_New.ImageUrl.IndexOf("nothing.png") > -1))
            {
                Eggsoft.Common.JsUtil.ShowMsg("图像必须选择！");
                Eggsoft.Common.JsUtil.LocationNewHref("Resource-2.aspx");//新建 肯定是对首
                return;
            }

            myModelnew.Pic = Image_Add_New.ImageUrl;
            myModelnew.Text = Server.HtmlEncode(TextBox_Title.Text + "#@#$#" + TextBox_ADD_New.Text);
            //string INC_User_ID = Eggsoft.Common.Session.Read("INCID");
            myModelnew.LinkURL = Server.HtmlEncode(TextBox_Url_New.Text);
            myBLLnew.Add(myModelnew);

            Eggsoft.Common.JsUtil.LocationNewHref("Resource-2.aspx");//新建 肯定是对首
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            int intpageIndex = 0;
            if (ViewState["pageIndex"] != null)
            {
                string strpageIndex = ViewState["pageIndex"].ToString();
                intpageIndex = Convert.ToInt32(strpageIndex);
            }
            //string INC_User_ID = Eggsoft.Common.Session.Read("INCID");

            //strLinkGoodlist = "ShopClient-{0}.aspx";
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strWhere = " and type=2 and ShopClientID=" + strShopClientID;

            AspNetPager1.CurrentPageIndex = intpageIndex;

            System.Data.DataTable myDataTable = myBLLnew.GetPageDataTable(intpageIndex, AspNetPager1.PageSize, "ID,Text,Pic,LinkURL,UpdateTime", strWhere, "ID", true);


            //string strNav = "";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                TableRow myTableRow = new TableRow();

                if (i % 2 == 0)
                {
                    myTableRow.CssClass = "oddrowcolor CSSTableRow1";
                }
                else
                {
                    myTableRow.CssClass = "evenrowcolor CSSTableRow1";
                }

                string strID = myDataTable.Rows[i]["ID"].ToString();

                string strTextAll = myDataTable.Rows[i]["Text"].ToString();
                strTextAll = Server.HtmlDecode(strTextAll);


                //strTextAll = strTextAll.Replace("#@#$#", "@");//是用先用 "@"代替\r\n",
                //string[] strTitleAndContentList = strTextAll.Split('@');
                string[] strTitleAndContentList = strTextAll.Split(new char[5] { '#', '@', '#', '$', '#' }, StringSplitOptions.RemoveEmptyEntries);
                if (strTitleAndContentList.Length < 2) continue;

                string strPic = myDataTable.Rows[i]["Pic"].ToString();
                string strLinkURL = Server.HtmlDecode(myDataTable.Rows[i]["LinkURL"].ToString());
                strLinkURL = Server.HtmlDecode(strLinkURL);

                string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();


                TextBox myTextBox = new TextBox();


                TableCell myTableIMGTEXTURLCell = new TableCell();
                myTableIMGTEXTURLCell.HorizontalAlign = HorizontalAlign.Center;
                myTableIMGTEXTURLCell.VerticalAlign = VerticalAlign.Middle;


                # region 检测那个图形更新了
                String strType = Request.QueryString["type"];
                if (strType != null)
                {
                    if (strType == "ModifySelectThisJPG")
                    {
                        String strResourceID = Request.QueryString["ResourceID"];
                        String strFormIMAGEURL = Request.Form["TextContent"];
                        if (strResourceID == strID)
                        {
                            strPic = strFormIMAGEURL.Replace("'", "");
                        }
                    }
                }
                #endregion
                string strLeft = "";
                strLeft += " <table style=\"width: 100%;\">\n";
                strLeft += "                  <tbody><tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                          <img class=\"picCss\" title=\"单击右边的上传按钮选择相应的封面图片。\" class=\"picCss\" src=\"" + strPic + "\" id=\"SingalImage" + strID + "\">\n";
                strLeft += "                      </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "                  <tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                           <textarea class=\"picCss\" style=\"height:50px;\" title=\"请在此处输入标题内容。\" id=\"NewmyTextBoxTitle" + strID + "\" cols=\"20\" rows=\"2\" name=\"TextBox_ADD_Title\">" + strTitleAndContentList[0] + "</textarea>\n";
                strLeft += "                      </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "                  <tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                           <textarea class=\"picCss\" style=\"height:100px;\" title=\"请在此处输入正文内容。\" id=\"NewmyTextBox" + strID + "\" cols=\"20\" rows=\"2\" name=\"TextBox_ADD\">" + strTitleAndContentList[1] + "</textarea>\n";
                strLeft += "                      </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "                   <tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                           <input type=\"text\" class=\"picCss\" style=\"height:40px;\" title=\"编辑正文后，请复制连接地址到当前编辑框。请在此处输入原文链接。没有留空。\" id=\"TextBox_Url_" + strID + "\" value=\"" + strLinkURL + "\" name=\"TextBox_Url\">\n";
                strLeft += "                        </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "     </tbody></table>\n";
                myTableIMGTEXTURLCell.Text = strLeft;


                myTableRow.Cells.Add(myTableIMGTEXTURLCell);





                string strSelectIMG = "<IMG title=\"选择并保存" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='ModifySelectThisJPG(" + strID + ")' src=\"../skin/Images/SelectJPG.png\">";
                string strIMG = "<IMG title=\"保存文本信息" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='saveThis(" + strID + ")' src=\"../skin/Images/save.png\">";
                string strDeleteIMG = "<IMG title=\"删除" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='deleteThis(" + strID + ")' src=\"../skin/Images/Delete.png\">";


                //System.Web.UI.WebControls.Image myImage=new System.Web.UI.WebControls.Image();
                //myImage.ImageUrl = "Images/save.jpg";
                //myImage. = "saveThis(" + strID + ")";
                TableCell myButtonTableCell = new TableCell();
                myButtonTableCell.HorizontalAlign = HorizontalAlign.Center;
                myButtonTableCell.VerticalAlign = VerticalAlign.Middle;
                myButtonTableCell.CssClass = "mymiddle";
                myButtonTableCell.Text = "ID:" + strID + "<br /><br />" + strSelectIMG + "<br /><br />" + strIMG + "<br /><br />" + strDeleteIMG;
                //myButtonTableCell.Controls.Add(myButton);
                myTableRow.Cells.Add(myButtonTableCell);

                Table_Show.Rows.Add(myTableRow);
            }

        }
    }
}