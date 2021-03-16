using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class_BoardSet : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strUploadUrl = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ApplicationManage_Class_BoardSet")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            BindBigClass();
        }



        //    <%# Eval("ID") %>
        public static string getColor(string strID)
        {
            String strColor = "";

            int conToInt16 = Convert.ToInt32(strID);
            bool mybool = Convert.ToBoolean(conToInt16 % 2);
            if (mybool)
            {
                strColor = "#ECF5FF";
            }
            else
            {
                strColor = "#E3E3E3";
            }

            return strColor;
        }



        public void BindBigClass()
        {
            EggsoftWX.BLL.tab_Class1 bll = new EggsoftWX.BLL.tab_Class1();

            string strSQL = "";
            strSQL += "SELECT     tab_Class1.ID, tab_Class1.ClassName, tab_Class1.ClassIcon, tab_Class1.Updatetime, tab_Class1.Sort, tab_Class1.IsShow, tab_Class1.IsLock, ";
            strSQL += "              tab_Class1.BigPicpath, tab_Class1.ShopClientID, ISNULL(View_1.Class1_IDCount, 0) AS Class1_IDCount";
            strSQL += " FROM         tab_Class1 LEFT OUTER JOIN";
            strSQL += "                 (SELECT     COUNT(Class1_ID) AS Class1_IDCount, Class1_ID";
            strSQL += "                   FROM          tab_Goods";
            strSQL += "                   WHERE      (IsDeleted = 0 and ShopClient_ID=" + str_Pub_ShopClientID + ")";
            strSQL += "                  GROUP BY Class1_ID) AS View_1 ON tab_Class1.ID = View_1.Class1_ID ";
            strSQL += " WHERE     (tab_Class1.ShopClientID = " + str_Pub_ShopClientID + ") order by tab_Class1.sort asc,tab_Class1.id asc";
            this.DataList1.DataSource = bll.SelectList(strSQL);

            DataList1.DataKeyField = "ID";
            this.DataList1.DataBind();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            DataList myDataList = (DataList)e.Item.FindControl("dlst2Class");
            if (myDataList != null)
            {
                EggsoftWX.BLL.tab_Class2 bll = new EggsoftWX.BLL.tab_Class2();
                int BigClassID = Int32.Parse(DataList1.DataKeys[e.Item.ItemIndex].ToString());

                string strSQL = "";
                strSQL += "SELECT     tab_Class2.ID, tab_Class2.ClassName, tab_Class2.ClassIcon, tab_Class2.Updatetime, tab_Class2.Sort, tab_Class2.IsShow, tab_Class2.IsLock, ";
                strSQL += "              tab_Class2.ShopClientID, ISNULL(View_2.Class2_IDCount, 0) AS Class2_IDCount";
                strSQL += " FROM         tab_Class2 LEFT OUTER JOIN";
                strSQL += "                 (SELECT     COUNT(Class1_ID) AS Class2_IDCount, Class2_ID";
                strSQL += "                   FROM          tab_Goods";
                strSQL += "                   WHERE      (IsDeleted = 0 and ShopClient_ID=" + str_Pub_ShopClientID + ")";
                strSQL += "                  GROUP BY Class2_ID) AS View_2 ON tab_Class2.ID = View_2.Class2_ID ";
                strSQL += " WHERE     (Class1_ID=" + BigClassID + ") order by tab_Class2.sort asc,tab_Class2.id asc";
                //this.DataList1.DataSource = bll.SelectList(strSQL);
                myDataList.DataSource = bll.SelectList(strSQL);

                //myDataList.DataSource = bll.GetList("*", "Class1_ID=" + BigClassID + " order by Sort asc,id asc");
                myDataList.DataBind();
            }
        }

        protected void DataList2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
           

            String strBig2ClassID = "0";

            HiddenField Field_Big2ClassID = (HiddenField)e.Item.FindControl("Class2_ID");
            if (Field_Big2ClassID != null)
            {
                strBig2ClassID = Field_Big2ClassID.Value.ToString().Trim();
            }

            DataList myDataList = (DataList)e.Item.FindControl("dlst3Class");
            if (myDataList != null)
            {
                EggsoftWX.BLL.tab_Class3 bll = new EggsoftWX.BLL.tab_Class3();
                string strSQL = "";
                strSQL += "SELECT     tab_Class3.ID, tab_Class3.ClassName, tab_Class3.ClassIcon, tab_Class3.Updatetime, tab_Class3.Sort, tab_Class3.IsShow, tab_Class3.IsLock, ";
                strSQL += "              tab_Class3.ShopClientID, ISNULL(View_3.Class3_IDCount, 0) AS Class3_IDCount";
                strSQL += " FROM         tab_Class3 LEFT OUTER JOIN";
                strSQL += "                 (SELECT     COUNT(Class3_ID) AS Class3_IDCount, Class3_ID";
                strSQL += "                   FROM          tab_Goods";
                strSQL += "                   WHERE      (IsDeleted = 0 and ShopClient_ID=" + str_Pub_ShopClientID + ")";
                strSQL += "                  GROUP BY Class3_ID) AS View_3 ON tab_Class3.ID = View_3.Class3_ID ";
                strSQL += " WHERE     (Class2_ID=" + strBig2ClassID + ") order by tab_Class3.sort asc,tab_Class3.id asc";
                myDataList.DataSource = bll.SelectList(strSQL);
               myDataList.DataBind();            
            }

        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("tab_Class1_Add.aspx");
        }
    }
}