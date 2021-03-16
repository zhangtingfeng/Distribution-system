using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._19tab_Order
{
    public partial class KuaiDiQuery : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strOrderID = Request.QueryString["OrderID"];
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users");

                Eggsoft_Public_CL.GoodP.ReadHTTPjuheWuLiu(Int32.Parse(strOrderID), Int32.Parse(strShopClientID));////执行查询


                EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery blltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery();
                EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery Modeltab_ShopClient_Order_KuaiDiQuery = blltab_ShopClient_Order_KuaiDiQuery.GetModel("OrderID=" + strOrderID + " and ShopClient_ID=" + strShopClientID);
                if (Modeltab_ShopClient_Order_KuaiDiQuery != null)
                {
                    if (Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount > 0)
                    {
                        string strJuHeResultList = Modeltab_ShopClient_Order_KuaiDiQuery.JuHeResultList;
                        try
                        {
                            Eggsoft_Public_CL.FahuoDanJsonExp.resultList[] mmmFahuoDanJsonExp = Eggsoft_Public_CL.JsonHelper.JsonDeserialize<Eggsoft_Public_CL.FahuoDanJsonExp.resultList[]>(strJuHeResultList);
                            Table_QueryKuaidi.Visible = true;
                            for (int intii = 0; intii < mmmFahuoDanJsonExp.Length; intii++)
                            {
                                TableRow ppppTableRow = new TableRow();

                                TableCell pppTableCell1 = new TableCell();
                                pppTableCell1.Text = (intii + 1).ToString();
                                ppppTableRow.Cells.Add(pppTableCell1);

                                TableCell pppTableCell2 = new TableCell();
                                pppTableCell2.Text = mmmFahuoDanJsonExp[intii].datetime;
                                ppppTableRow.Cells.Add(pppTableCell2);

                                TableCell pppTableCell3 = new TableCell();
                                pppTableCell3.Text = mmmFahuoDanJsonExp[intii].remark;
                                ppppTableRow.Cells.Add(pppTableCell3);

                                Table_QueryKuaidi.Rows.Add(ppppTableRow);
                            }
                        }
                        catch (System.Threading.ThreadAbortException ettt)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "KuaiDiQuery", "线程异常");
                        }
                        catch (Exception eeee)
                        {
                            Table_QueryKuaidi.Visible = false;///属于查询失败的
                            Eggsoft.Common.debug_Log.Call_WriteLog(strJuHeResultList, "KuaiDiQuery");
                            Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "KuaiDiQuery");
                        }

                    }
                    else
                    {///===-99999   属于查询失败的
                        Table_QueryKuaidi.Visible = false;
                    }
                }
                else
                {
                    Table_QueryKuaidi.Visible = false;
                }

                if (Table_QueryKuaidi.Visible == false)
                {
                    Localize_ShowInfo.Visible = true;
                    Localize_ShowInfo.Text = "暂不支持查询或查询失败";
                }
                //Table_QueryKuaidi

            }
        }

    }
}