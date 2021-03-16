using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.MultiPhoto
{
    public partial class MultiPhotoSet_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        public String MenuLink = "";


        class ListItemnewText
        {
            private string _stringName;
            private string _stringFilename;


            /// <summary>
            /// 
            /// </summary>
            public string stringName
            {
                set { _stringName = value; }
                get { return _stringName; }
            }
            /// <summary>
            /// 
            public string stringFilename
            {
                set { _stringFilename = value; }
                get { return _stringFilename; }
            }
        }

        //private ListItem ListItemnew=new ListItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            ListBox_WaitChoice.Attributes.Add("ondblclick", "JsListChangeItem()");//为listBox1添加双击事件。

            if (!IsPostBack)
            {

                //RadioButtonList1.SelectedValue = "0";
                LinkShow.Visible = false;
                //TextShow.Visible = true;
                //RadioButton1.Checked = true;
                //RadioButton2.Checked = false;

                string type = Request.QueryString["Type"];


                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    ///EggsoftWX.BLL.Custom bll = new EggsoftWX.BLL.Custom();   ????

                    //EggsoftWX.Model.Custom Model = bll.GetModel(Int32.Parse(ID));//删除文件
                    ////Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.MenuIcon));//删除文件
                    //bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "MultiPhotoSet_Board.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    InitGoPage();
                    SetClass();
                }
            }
        }

        private void SetClass()
        {
            EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto bll = new EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto();


            string type = Request.QueryString["Type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model.tab_ShopClient_CustomMultiPhoto Model = bll.GetModel(Int32.Parse(ID));

                txtTitle.Text = Model.Title;
                txtWriter.Text = Model.Author;
                string strCustomMultiPhoto = Model.ContentMultiPhoto;


                string strArrayList_ListItemnew = ViewState["ddddd"] as String;
                DataTable myDataTable = Eggsoft.Common.Json2Table.JsonToDataTable(strArrayList_ListItemnew);

                DataTable mystrCustomMultiPhotoDataTable = Eggsoft.Common.Json2Table.JsonToDataTable(strCustomMultiPhoto);
                for (int i = 0; i < mystrCustomMultiPhotoDataTable.Rows.Count; i++)
                {
                    string _stringDatabaseTileName = mystrCustomMultiPhotoDataTable.Rows[i]["_stringTileName"].ToString();
                    string _stringDatabaseFileName = mystrCustomMultiPhotoDataTable.Rows[i]["_stringFilename"].ToString();
                    ListBox_OK.Items.Add(_stringDatabaseFileName);

                    for (int j = 0; j < myDataTable.Rows.Count; j++)
                    {
                        string _stringTileName = myDataTable.Rows[j]["_stringTileName"].ToString();
                        string _stringFilename = myDataTable.Rows[j]["_stringFilename"].ToString();



                        if (_stringDatabaseFileName == _stringFilename)
                        {
                            myDataTable.Rows[j]["_stringTileName"] = _stringDatabaseTileName;
                            break;
                        }
                    }
                }
                string strmyDataTable = Eggsoft.Common.Json2Table.ToJson(myDataTable);
                ViewState["ddddd"] = strmyDataTable;


                //CheckBox1.Checked=Model.IF3DPic;
                //txtContent.Text = Server.HtmlDecode(Model.Content);
                //txtContent3D.Text = Server.HtmlDecode(Model.IF3DPicContent);

                //Label1Link.Text = "./03CustomInfo-" + Model.INC_User_ID + ".aspx";
                MenuLink = "./03CustomInfo-" + Model.ShopClient_ID.ToString() + "-" + ID + ".aspx";
                btnAdd.Text = "保 存";
            }
            else if (type.ToLower() == "add")
            {

                //txtContent3D.Text = strExample;

                //Label1Link.Text = "./03CustomInfo-" + bll.GetMaxId() + ".aspx";
                //MenuLink = "./01Info-" + Model.INC_User_ID + "-" + ID + ".aspx";
            }
        }


        private void InitGoPage()
        {


            DataTable mtReadTable = Eggsoft_Public_CL.MultiPhotoSet_DataTable.getMultiTable();

            //strMange
            string strFilePath = "/Upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";
            string fpath = Server.MapPath(strFilePath);
            DirectoryInfo di = new DirectoryInfo(fpath);

            if (di.Exists == false) return;
            foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
            {

                if (fsi is FileInfo)//如果是文件
                {
                    FileInfo fi = (FileInfo)fsi;

                    string fname = fi.Name;
                    //int sourceWidth = 0;
                    //int sourceHeight = 0;
                    //string strIMG = "";
                    if ((fname.ToLower().IndexOf(".jpg") != -1) || (fname.ToLower().IndexOf(".jpeg") != -1) || (fname.ToLower().IndexOf(".bmp") != -1) || (fname.ToLower().IndexOf(".gif") != -1))
                    {
                        ListBox_WaitChoice.Items.Add(fname);
                        DataRow row;
                        row = mtReadTable.NewRow();
                        row["_stringTileName"] = "";
                        row["_stringFilename"] = fname;
                        mtReadTable.Rows.Add(row);

                    }
                    else
                    {
                        continue;
                    }
                }
                //BindFile(fpath);
            }

            //System.Text.StringBuilder st = new System.Text.StringBuilder();
            //st = st.Append(ArrayList_ListItemnew);
            //string res = st.ToString();

            ViewState["ddddd"] = Eggsoft.Common.Json2Table.ToJson(mtReadTable);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];

            DataTable mtReadTable = Eggsoft_Public_CL.MultiPhotoSet_DataTable.getMultiTable();

            for (int i = 0; i < ListBox_OK.Items.Count; i++)
            {
                DataRow row;
                row = mtReadTable.NewRow();
                row["_stringFilename"] = ListBox_OK.Items[i].ToString();
                row["_stringTileName"] = getTileName(ListBox_OK.Items[i].ToString());
                mtReadTable.Rows.Add(row);
            }

            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto bll = new EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto();
                EggsoftWX.Model.tab_ShopClient_CustomMultiPhoto Model = bll.GetModel(Int32.Parse(ID));
                Model.Author = txtWriter.Text;
                Model.Title = txtTitle.Text;
                Model.ContentMultiPhoto = Eggsoft.Common.Json2Table.ToJson(mtReadTable);
                Model.UpdateTime = DateTime.Now;
                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", "MultiPhotoSet_Board.aspx");
            }
            else
                if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto bll = new EggsoftWX.BLL.tab_ShopClient_CustomMultiPhoto();
                    EggsoftWX.Model.tab_ShopClient_CustomMultiPhoto Model = new EggsoftWX.Model.tab_ShopClient_CustomMultiPhoto();

                    Model.Title = txtTitle.Text;
                    //Model.Content = Server.HtmlEncode(txtContent.Text);
                    Model.Author = txtWriter.Text;

                    Model.ShopClient_ID = Int32.Parse(Eggsoft.Common.Session.Read("INCID").ToString());
                    Model.ContentMultiPhoto = Eggsoft.Common.Json2Table.ToJson(mtReadTable);
                    Model.UpdateTime = DateTime.Now;
                    //Model.IF3DPic = CheckBox1.Checked;
                    //Model.IF3DPicContent = Server.HtmlEncode(txtContent3D.Text);                

                    bll.Add(Model);
                    JsUtil.ShowMsg("添加成功!", "MultiPhotoSet_Board.aspx");
                }
        }


        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LinkShow.Visible = false;
            //TextShow.Visible = true;
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LinkShow.Visible = true;
            //TextShow.Visible = false;
        }

        protected void ListBox_WaitChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox_WaitChoice.SelectedIndex == -1) return;
            string strFilePath = "/Upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";

            string strFilename = ListBox_WaitChoice.SelectedValue;

            string strpath = System.Web.HttpContext.Current.Server.MapPath(strFilePath + strFilename);

            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(strpath);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            ImageShow.ImageUrl = strFilePath + strFilename;


        }
        protected void Button_Choice_Click(object sender, EventArgs e)
        {
            string strFilename = ListBox_WaitChoice.SelectedValue;
            ListBox_OK.Items.Add(strFilename);
            //ListBox_OK_Text.Items.Add("");//标题
        }
        protected void ListBox_OK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox_OK.SelectedIndex == -1) return;
            string strFilePath = "/Upload/" + Eggsoft.Common.Session.Read("INCUploadpath") + "/images/";

            string strFilename = ListBox_OK.SelectedValue;

            string strpath = System.Web.HttpContext.Current.Server.MapPath(strFilePath + strFilename);

            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(strpath);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            ImageShow.ImageUrl = strFilePath + strFilename;

            ShowText();
            //TextBoxPicName.Text = ListBox_OK_Text.Items[ListBox_OK.SelectedIndex].Text;//标题
        }
        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (ListBox_OK.SelectedIndex == -1) return;
            if (ListBox_OK.Items.Count <= 1) return;
            if (ListBox_OK.SelectedIndex == 0) return;

            ListItem lt = new ListItem(ListBox_OK.SelectedItem.Text, ListBox_OK.SelectedValue);

            ListBox_OK.Items[ListBox_OK.SelectedIndex].Value = ListBox_OK.Items[ListBox_OK.SelectedIndex - 1].Value;
            ListBox_OK.Items[ListBox_OK.SelectedIndex].Text = ListBox_OK.Items[ListBox_OK.SelectedIndex - 1].Text;

            ListBox_OK.Items[ListBox_OK.SelectedIndex - 1].Text = lt.Text;
            ListBox_OK.Items[ListBox_OK.SelectedIndex - 1].Value = lt.Value;
            ListBox_OK.SelectedIndex = ListBox_OK.SelectedIndex - 1;



        }
        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if (ListBox_OK.SelectedIndex == -1) return;
            if (ListBox_OK.Items.Count <= 1) return;
            if (ListBox_OK.SelectedIndex == ListBox_OK.Items.Count - 1) return;

            ListItem lt = new ListItem(ListBox_OK.SelectedItem.Text, ListBox_OK.SelectedValue);

            ListBox_OK.Items[ListBox_OK.SelectedIndex].Value = ListBox_OK.Items[ListBox_OK.SelectedIndex + 1].Value;
            ListBox_OK.Items[ListBox_OK.SelectedIndex].Text = ListBox_OK.Items[ListBox_OK.SelectedIndex + 1].Text;

            ListBox_OK.Items[ListBox_OK.SelectedIndex + 1].Text = lt.Text;
            ListBox_OK.Items[ListBox_OK.SelectedIndex + 1].Value = lt.Value;
            ListBox_OK.SelectedIndex = ListBox_OK.SelectedIndex + 1;
        }
        protected void MoveOut_Click(object sender, EventArgs e)
        {
            if (ListBox_OK.SelectedIndex == -1) return;
            ListBox_OK.Items.RemoveAt(ListBox_OK.SelectedIndex);
        }

        private void ShowText()
        {

            if (ListBox_OK.SelectedIndex == -1) return;
            string strFilename = ListBox_OK.Items[ListBox_OK.SelectedIndex].Text;

            TextBoxPicName.Text = getTileName(strFilename);


        }

        private string getTileName(string strFilename)
        {
            string strArrayList_ListItemnew = ViewState["ddddd"] as String;
            DataTable myDataTable = Eggsoft.Common.Json2Table.JsonToDataTable(strArrayList_ListItemnew);
            //利用foreach循环   

            String strReturn = "";
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string _stringTileName = myDataTable.Rows[i]["_stringTileName"].ToString();
                string _stringFilename = myDataTable.Rows[i]["_stringFilename"].ToString();

                if (_stringFilename == strFilename)
                {
                    strReturn = _stringTileName;
                    break;
                }

            }
            return strReturn;
        }



        protected void Button_SaveTitle_Click(object sender, EventArgs e)
        {
            if (ListBox_OK.SelectedIndex == -1) return;

            string strArrayList_ListItemnew = ViewState["ddddd"] as String;
            DataTable myDataTable = Eggsoft.Common.Json2Table.JsonToDataTable(strArrayList_ListItemnew);

            //遍历元素 -- 通过索引   
            //利用foreach循环   

            string strFilename = ListBox_OK.Items[ListBox_OK.SelectedIndex].Text;

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string _stringTileName = myDataTable.Rows[i]["_stringTileName"].ToString();
                string _stringFilename = myDataTable.Rows[i]["_stringFilename"].ToString();

                if (_stringFilename == strFilename)
                {
                    _stringTileName = TextBoxPicName.Text;
                    myDataTable.Rows[i]["_stringTileName"] = _stringTileName;
                    ViewState["ddddd"] = Eggsoft.Common.Json2Table.ToJson(myDataTable);

                    return;
                }
            }



        }
    }
}