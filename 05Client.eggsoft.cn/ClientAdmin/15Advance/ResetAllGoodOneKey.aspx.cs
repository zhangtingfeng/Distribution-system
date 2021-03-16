using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._15Advance
{
    public partial class ResetAllGoodOneKey : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Eggsoft.Common.debug_Log.Call_WriteLog("Index888");

            //Eggsoft.Common.Session.Add("WriteHtml_write", "0");
            if (!IsPostBack)
            {
                Label_Memory.Text = "0";
            }
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string strpShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                int intShopClient = Int32.Parse(strpShopClientID);
                //int intShopClient = 5;

                int intStartInt = Int32.Parse(Label_Memory.Text);

                intStartInt++;

                lResult0_Show.Text = "<br />" + (intStartInt * 10).ToString() + "%";
                //lResult.Text += "<br />" + (intStartInt*10).ToString()+"%";

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                System.Data.DataTable myDataTable = BLL_tab_Goods.GetList("IsDeleted<>1  and ShopClient_ID=" + intShopClient + " and isSaled<>0 order by id asc").Tables[0];

                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    //if (i < myDataTable.Rows.Count)
                    //{
                    string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                    Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                    Eggsoft.Common.debug_Log.Call_WriteLog(DateTime.Now.ToLongTimeString(), "更新全局商品ShopClientID=" + strpShopClientID, i.ToString() + "   " + strGoodID);
                    //                    lResult0_Show.Text += i.ToString() + "   " + strGoodID + "<br />";
                    //}
                }

                //if (intStartInt == 1)
                //{
                //    //if (myDataTable.Rows.Count < 10)
                //    //{ ////数值太小 直接完成
                //    for (int i = 0; i < myDataTable.Rows.Count; i++)
                //    {
                //        if (i < myDataTable.Rows.Count)
                //        {
                //            string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //            Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //        }
                //    }
                //    intStartInt = 9;////直接跳到完成
                //    //}
                //}
                //else if (intStartInt == 2)
                //{
                //    if (myDataTable.Rows.Count < 60)
                //    { ////数值太小 直接完成
                //        for (int i = 0; i < 30; i++)
                //        {
                //            if (i < myDataTable.Rows.Count)
                //            {
                //                string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //                Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //            }
                //        }
                //    }
                //}
                //else if (intStartInt == 3)
                //{
                //    if (myDataTable.Rows.Count < 60)///数值中等 分步完成
                //    {
                //        for (int i = 30; i < myDataTable.Rows.Count; i++)
                //        {
                //            if (i < myDataTable.Rows.Count)
                //            {
                //                string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //                Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //            }
                //        }
                //        intStartInt = 9;////直接跳到完成
                //    }

                //}
                //else if (intStartInt == 4)
                //{
                //    for (int i = 0; i < 50; i++)////超大的值  50  100  .......
                //    {
                //        if (i < myDataTable.Rows.Count)
                //        {
                //            string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //            Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //        }
                //    }
                //}
                //else if (intStartInt == 5)
                //{
                //    for (int i = 50; i < 100; i++)////超大的值  50  100  .......
                //    {
                //        if (i < myDataTable.Rows.Count)
                //        {
                //            string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //            Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //        }
                //    }
                //}
                //else if (intStartInt == 6)////超大的值  50  100  .......
                //{
                //    for (int i = 100; i < myDataTable.Rows.Count; i++)
                //    {
                //        if (i < myDataTable.Rows.Count)
                //        {
                //            string strGoodID = myDataTable.Rows[i]["ID"].ToString();
                //            Eggsoft_Public_CL.GoodP.updateAllAgentPercent(Int32.Parse(strGoodID));
                //        }
                //    }
                //}
                //else if (intStartInt == 8)
                //{

                //    //JsUtil.ShowMsg("添加成功!", strClientAdminURL + "/ClientAdmin/18tab_GoodClass/Board_Good.aspx");

                //    //Eggsoft.Common.JsUtil.TipAndRedirect("生成完成", strClientAdminURL + "/ClientAdmin/right.aspx","1");

                //}
                //else if (intStartInt >= 10)
                //{
                //    lResult0_Show.Text += "<br />更新完成";
                //    Timer1.Enabled = false;
                //}
                Timer1.Enabled = false;
                Label_Memory.Text = intStartInt.ToString();
            }

            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex);
            }
            finally
            {

            }
        }
    }
}