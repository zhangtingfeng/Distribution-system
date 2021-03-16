using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class Resource_3 : Eggsoft.Common.DotAdminPage_ClientAdmin
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
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            AspNetPager1.RecordCount = myBLLnew.ExistsCount("type=3 and ShopClientID=" + strShopClientID);
            AspNetPager1.UrlRewritePattern = "Resource-3.aspx?strType=" + "3" + "&pageIndex={0}";

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


        protected void btnIntroduction_Click1(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-1.aspx");

        }
        protected void btnIntroduction_Click2(object sender, EventArgs e)
        {
            Eggsoft.Common.JsUtil.LocationNewHref("Resource-2.aspx");

        }


        protected void ImageButton_ADD_Click(object sender, EventArgs e)
        {
            EggsoftWX.Model.tab_ShopClient_System_XML_Resource myModelnew = new EggsoftWX.Model.tab_ShopClient_System_XML_Resource();
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            myModelnew.ShopClientID = Int32.Parse(strShopClientID);
            myModelnew.type = 3;
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
                Eggsoft.Common.JsUtil.LocationNewHref("Resource-3.aspx");//新建 肯定是队首
                return;
            }

            myModelnew.Pic = Image_Add_New.ImageUrl;
            myModelnew.Text = Server.HtmlEncode(TextBox_ADD_New.Text);
            //string INC_User_ID = Eggsoft.Common.Session.Read("INCID");
            myModelnew.LinkURL = Server.HtmlEncode(TextBox_Url_New.Text);
            myBLLnew.Add(myModelnew);
            string strType = Request.QueryString["strType"];
            if ((strType == null) || (strType == "")) strType = "3";

            Eggsoft.Common.JsUtil.LocationNewHref("Resource-3.aspx");
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
            string strWhere = " and parentID=0 and type=3 and ShopClientID=" + strShopClientID;

            AspNetPager1.RecordCount = myBLLnew.ExistsCount(strWhere);
            AspNetPager1.CurrentPageIndex = intpageIndex;
            //gvUser.DataSource = 
            System.Data.DataTable myDataTable = myBLLnew.GetPageDataTable(intpageIndex, AspNetPager1.PageSize, "ID,Text,Pic,LinkURL,UpdateTime", strWhere, "ID", true);


            //string strNav = "";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                string strID = myDataTable.Rows[i]["ID"].ToString();

                string strText = myDataTable.Rows[i]["Text"].ToString();
                strText = Server.HtmlDecode(strText);


                string strPic = myDataTable.Rows[i]["Pic"].ToString();
                string strLinkURL = Server.HtmlDecode(myDataTable.Rows[i]["LinkURL"].ToString());
                strLinkURL = Server.HtmlDecode(strLinkURL);

                string strUpdateTime = myDataTable.Rows[i]["UpdateTime"].ToString();

                TableCell myTableIMGTEXTURLCell = new TableCell();
                myTableIMGTEXTURLCell.HorizontalAlign = HorizontalAlign.Center;
                myTableIMGTEXTURLCell.VerticalAlign = VerticalAlign.Middle;


                TableRow myTableRow = new TableRow();
                myTableRow.ID = "myHeadTableRow" + strID;
                if (i % 2 == 0)
                {
                    myTableRow.CssClass = "CSSTableRow_Multi1 oddrowcolor";
                }
                else
                {
                    myTableRow.CssClass = "CSSTableRow_Multi1 evenrowcolor";
                }
                TableCell myButtonTableCell = new TableCell();
                myButtonTableCell.HorizontalAlign = HorizontalAlign.Center;
                myButtonTableCell.VerticalAlign = VerticalAlign.Middle;
                myButtonTableCell.CssClass = "mymiddle";
                myTableRow.Cells.Add(myButtonTableCell);


                #region add td_only One td

                //add  TableCell


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

                //add  table
                Table myOnlyTable = new Table();
                myOnlyTable.ID = "EachTableID" + strID;
                TableRow myOnlyTableRow = new TableRow();
                myOnlyTableRow.ID = "HeadInfo" + strID;
                #region add td1
                string strLeft = "";
                strLeft += " <table style=\"width: 100%;\">\n";
                strLeft += "                  <tbody><tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                          <img class=\"picCss\" title=\"单击右边的上传按钮选择相应的封面图片。\" style=\"width:100%;\" src=\"" + strPic + "\" id=\"SingalImage_Head_" + strID + "\">\n";
                strLeft += "                      </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "                  <tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                           <textarea style=\"height:100px;width:100%;\" title=\"请在此处输入文本内容。\" id=\"SingalImage_Head_TextBox" + strID + "\" cols=\"20\" rows=\"2\" name=\"TextBox_ADD\">" + strText + "</textarea>\n";
                strLeft += "                      </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "                   <tr>\n";
                strLeft += "                      <td>\n";
                strLeft += "                           <input type=\"text\" style=\"height:20px;width:100%;\" title=\"编辑正文后，请复制连接地址到当前编辑框。请在此处输入跳转地址。没有留空。\" id=\"TextBox_Url_Head_" + strID + "\" value=\"" + strLinkURL + "\" name=\"TextBox_Url\">\n";
                strLeft += "                        </td>\n";
                strLeft += "                  </tr>\n";
                strLeft += "     </tbody></table>\n";
                myTableIMGTEXTURLCell.Text = strLeft;
                myOnlyTableRow.Cells.Add(myTableIMGTEXTURLCell);
                #endregion

                #region add td2
                string strSelectIMG = "<IMG title=\"选择并保存" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='ModifySelectThisJPG(" + strID + ")' src=\"../skin/Images/SelectJPG.png\">";
                string strIMG = "<IMG title=\"保存文本信息" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='saveThis(" + strID + ")' src=\"../skin/Images/save.png\">";
                string strDeleteIMG = "<IMG title=\"删除" + strID + "\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\"   onclick='deleteThis(" + strID + ")' src=\"../skin/Images/Delete.png\">";

                TableCell myImageButtonTableCell = new TableCell();
                myImageButtonTableCell.HorizontalAlign = HorizontalAlign.Center;
                myImageButtonTableCell.VerticalAlign = VerticalAlign.Middle;
                myImageButtonTableCell.CssClass = "mymiddle";
                myImageButtonTableCell.Text = "ID:" + strID + "<br /><br />" + strSelectIMG + "<br /><br />" + strIMG + "<br /><br />" + strDeleteIMG;

                myOnlyTableRow.Cells.Add(myImageButtonTableCell);

                #endregion

                myOnlyTable.Rows.Add(myOnlyTableRow);


                #endregion

                string strLastTailParentID = strID;
                #region 循环 逐行加入tail
                bool boolWhileParentid = true;


                while (boolWhileParentid)
                {

                    bool boolTail = myBLLnew.Exists("ParentID=" + strLastTailParentID);
                    if (boolTail)///有尾巴
                    {




                        EggsoftWX.Model.tab_ShopClient_System_XML_Resource tailModel = myBLLnew.GetModel("ParentID=" + strLastTailParentID);


                        # region 检测有尾巴图形更新了
                        string strNewTailPic = tailModel.Pic;
                        if (strType != null)
                        {
                            if (strType == "ModifySelectThisJPG")
                            {
                                String strResourceID = Request.QueryString["ResourceID"];
                                String strFormIMAGEURL = Request.Form["TextContent"];
                                if (strResourceID == tailModel.ID.ToString())
                                {
                                    strNewTailPic = strFormIMAGEURL.Replace("'", "");
                                }
                            }
                        }
                        #endregion


                        String strWhileAdd = "";
                        strWhileAdd += "<tr>\n";
                        strWhileAdd += "<td id=\"multiTuWen_Add_Left-Tail-0" + tailModel.ID + "\">\n";
                        strWhileAdd += "<table border=\"0\" class=\"mymultiTuWen_Add_LeftTable\">\n";
                        strWhileAdd += "<tbody>\n";
                        strWhileAdd += "<tr>\n";
                        strWhileAdd += "<td id=\"mymultiTuWen_Add_LeftTableRow_TableCell_Tail_0" + tailModel.ID + "\">\n";
                        strWhileAdd += " <table style=\"width: 100%;\">\n";
                        strWhileAdd += "   <tbody>\n";
                        strWhileAdd += "     <tr>\n";
                        strWhileAdd += "       <td>\n";
                        strWhileAdd += "        <textarea name=\"TextBox_ADD\" rows=\"2\" cols=\"20\" id=\"strmymultiTuWen_Add_LeftTableRow_TableCellText_Tail" + tailModel.ID + "\" title=\"请在此处输入文本内容。\" style=\"height:60px;width:300px;\">" + Server.HtmlDecode(tailModel.Text) + "</textarea>\n";
                        strWhileAdd += "        </td>\n";
                        strWhileAdd += "         <td>\n";
                        strWhileAdd += "             <img title=\"单击右边的上传按钮选择相应的封面图片。\" style=\"height:40px;width:40px;\" src=\"" + strNewTailPic + "\" id=\"strmymultiTuWen_Add_LeftTableRow_TableCellIMG_Tail" + tailModel.ID + "\">\n";
                        strWhileAdd += "          </td>\n";
                        strWhileAdd += "       </tr>\n";
                        strWhileAdd += " </tbody>\n";
                        strWhileAdd += "</table>\n";
                        strWhileAdd += "</td>\n";
                        strWhileAdd += "</tr>\n";
                        strWhileAdd += "<tr id=\"mymultiTuWen_Add_LeftTableRow_LinkURL_Tail" + tailModel.ID + "\">\n";
                        strWhileAdd += "\n";
                        strWhileAdd += "<td>\n";
                        strWhileAdd += "<textarea name=\"TextBox_ADD\" rows=\"2\" cols=\"20\" id=\"mymultiTuWen_Add_LeftTableRow_TableCell_LinkURL_TextArea_Tail" + tailModel.ID + "\" title=\"编辑正文后，请复制连接地址到当前编辑框。请在此处输入链接内容，没有留空。\" style=\"height:30px;width:100%;\">" + Server.HtmlDecode(tailModel.LinkURL) + "</textarea>\n";
                        strWhileAdd += "</td>\n";
                        strWhileAdd += "</tr>\n";
                        strWhileAdd += "</tbody>\n";
                        strWhileAdd += "</table>\n";
                        strWhileAdd += "</td>\n";
                        strWhileAdd += "<td id=\"multiTuWen_Add_Right-New-Button_Tail" + tailModel.ID + "\" class=\"mymiddle\" valign=\"middle\" align=\"center\">\n";
                        strWhileAdd += "<img title=\"选择并保存" + tailModel.ID + " newMulti\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\" onclick=\"ModifySelectThisJPG(" + tailModel.ID + ")\" src=\"../skin/Images/SelectJPG.png\">\n";
                        strWhileAdd += "<br>\n";
                        strWhileAdd += "<img title=\"保存文本信息 " + tailModel.ID + "newMulti\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\" onclick=\"saveTailThis(" + tailModel.ID + ")\" src=\"../skin/Images/save.png\">\n";
                        strWhileAdd += "<br>\n";
                        strWhileAdd += "<img src=\"../skin/Images/Delete.png\" onclick=\"deleteThis(" + tailModel.ID + ")\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\" title=\"删除26\">\n";
                        strWhileAdd += "</td>\n";

                        strWhileAdd += "</tr>\n";


                        TableRow tailTableRow = new TableRow();
                        tailTableRow.ID = "tailTableRow" + tailModel.ID;
                        TableCell tailTableCell = new TableCell();
                        tailTableCell.ID = "tailTableCell" + tailModel.ID;
                        tailTableCell.Text = strWhileAdd;
                        tailTableRow.Cells.Add(tailTableCell);
                        myOnlyTable.Rows.Add(tailTableRow);

                        strLastTailParentID = tailModel.ID.ToString();//为下一个while 准备
                    }
                    else
                    {
                        boolWhileParentid = false;//结束循环
                    }
                }

                #endregion




                #region multiTuWen Add
                TableRow mymultiTuWenTableRow = new TableRow();
                mymultiTuWenTableRow.ID = "NewAddNewTail" + strLastTailParentID;
                TableCell multiTuWen_Add_Left = new TableCell();
                multiTuWen_Add_Left.ID = "multiTuWen_Add_Left-New-0" + strLastTailParentID;
                multiTuWen_Add_Left.CssClass = "mymiddle";


                TableCell multiTuWen_Add_Right = new TableCell();
                multiTuWen_Add_Right.ID = "multiTuWen_Add_Right-New-Button" + strLastTailParentID;
                multiTuWen_Add_Right.CssClass = "mymiddle";
                multiTuWen_Add_Right.VerticalAlign = VerticalAlign.Middle;
                multiTuWen_Add_Right.HorizontalAlign = HorizontalAlign.Center;

                #region multiTuWen Add multiTuWen_Add_Left multiTuWen_Add_Right-New-Button Text
                String strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text = "";

                strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text += "<img src=\"../skin/Images/SelectJPG.png\" onclick=\"SelectNewThis(" + strLastTailParentID + ")\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\" title=\"选择并保存" + strLastTailParentID + " newMulti\">\n";
                strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text += "<br>\n";
                strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text += "<img src=\"../skin/Images/save.png\" onclick=\"saveNewThis(" + strLastTailParentID + ")\" style=\"  ADDING-LEFT: 0px; CURSOR: pointer\" title=\"保存文本信息 " + strLastTailParentID + "newMulti\">\n";
                strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text += "<br>\n";
                multiTuWen_Add_Right.Text = strmultiTuWen_Add_multiTuWen_Add_Left_multiTuWen_Add_Right_New_Button_Text;
                #endregion multiTuWen Add multiTuWen_Add_Left multiTuWen_Add_Right-New-Button Text




                mymultiTuWenTableRow.Controls.Add(multiTuWen_Add_Left);

                mymultiTuWenTableRow.Controls.Add(multiTuWen_Add_Right);
                myOnlyTable.Rows.Add(mymultiTuWenTableRow);


                #region multiTuWen Add multiTuWen_Add_Left
                Table mymultiTuWen_Add_LeftTable = new Table();
                mymultiTuWen_Add_LeftTable.CssClass = "mymultiTuWen_Add_LeftTable";
                TableRow mymultiTuWen_Add_LeftTableRow = new TableRow();
                TableCell mymultiTuWen_Add_LeftTableRow_TableCell = new TableCell();
                mymultiTuWen_Add_LeftTableRow_TableCell.ID = "mymultiTuWen_Add_LeftTableRow_TableCell_New_0" + strLastTailParentID;

                #region new TextBox


                # region 检测那个图形更新了  ///不是-1  表示为负值新增的
                string strNewPic = "Images/nothing.png";
                if (strType != null)
                {
                    if (strType == "ModifySelectThisJPG")
                    {
                        String strResourceID = Request.QueryString["ResourceID"];
                        String strFormIMAGEURL = Request.Form["TextContent"];
                        if (strResourceID == "-" + strLastTailParentID)
                        {
                            strNewPic = strFormIMAGEURL.Replace("'", "");
                        }
                    }
                }
                #endregion

                string strmymultiTuWen_Add_LeftTableRow_TableCell = "";
                strmymultiTuWen_Add_LeftTableRow_TableCell += " <table style=\"width: 100%;\">\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                  <tbody><tr>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                      <td>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                           <textarea style=\"height:60px;width:300px;\" title=\"请在此处输入文本内容。\" id=\"strmymultiTuWen_Add_LeftTableRow_TableCellText" + strLastTailParentID + "\" cols=\"20\" rows=\"1\" name=\"TextBox_ADD\">" + "" + "</textarea>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                      </td>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                      <td>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                           <img title=\"单击右边的上传按钮选择相应的封面图片。\" id=\"strmymultiTuWen_Add_LeftTableRow_TableCellIMG" + strLastTailParentID + "\" src=\"" + strNewPic + "\" style=\"height:40px;width:40px;\">\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                        </td>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "                  </tr>\n";
                strmymultiTuWen_Add_LeftTableRow_TableCell += "     </tbody></table>\n";
                mymultiTuWen_Add_LeftTableRow_TableCell.Text = strmymultiTuWen_Add_LeftTableRow_TableCell;



                #endregion

                mymultiTuWen_Add_LeftTable.Rows.Add(mymultiTuWen_Add_LeftTableRow);
                mymultiTuWen_Add_LeftTableRow.Controls.Add(mymultiTuWen_Add_LeftTableRow_TableCell);
                multiTuWen_Add_Left.Controls.Add(mymultiTuWen_Add_LeftTable);

                TableRow mymultiTuWen_Add_LeftTableRow_LinkURL = new TableRow();
                mymultiTuWen_Add_LeftTableRow_LinkURL.ID = "mymultiTuWen_Add_LeftTableRow_LinkURL" + strLastTailParentID;
                TableCell mymultiTuWen_Add_LeftTableRow_TableCell_LinkURL = new TableCell();
                mymultiTuWen_Add_LeftTableRow_LinkURL.Controls.Add(mymultiTuWen_Add_LeftTableRow_TableCell_LinkURL);
                mymultiTuWen_Add_LeftTableRow_TableCell_LinkURL.Text = "<textarea style=\"height:30px;width:100%;\" title=\"编辑正文后，请复制连接地址到当前编辑框。请在此处输入链接内容，没有留空。\" id=\"mymultiTuWen_Add_LeftTableRow_TableCell_LinkURL_TextArea" + strLastTailParentID + "\" cols=\"20\" rows=\"1\" name=\"TextBox_ADD\">" + "" + "</textarea>";
                mymultiTuWen_Add_LeftTable.Rows.Add(mymultiTuWen_Add_LeftTableRow_LinkURL);
                #endregion

                #endregion


                myButtonTableCell.Controls.Add(myOnlyTable);
                myButtonTableCell.ColumnSpan = 2;




                Table_Show.Rows.Add(myTableRow);
            }
        }
    }
}