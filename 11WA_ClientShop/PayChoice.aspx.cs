using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class PayChoice : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setAllNeedID();
                pBeforePayCheckAllParentIDAndRechargeIt();

                string strContent = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/PayChoice_Templet.html");
                strContent = strContent.Replace("###SAgentPath###", Pub_Agent_Path);
                strContent = LoadTodayGood(strContent);

                HttpContext.Current.Response.Write(strContent);
                HttpContext.Current.Response.End();
            }
        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }

        private string LoadTodayGood(string strContent)
        {
            //###WeiXinPay###

            //###Aplipay###
            string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID


            string strAplipay = "";
            string strWeiXinPay = "";
            string strKuaiQianpay = "";

            if (strOrderINT != null)//订单记录的ID
            {
                strAplipay = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/Alipay/default.aspx?OrderINT=" + strOrderINT;
                strWeiXinPay = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/v3pay_weixin/default.aspx?OrderINT=" + strOrderINT;
                strKuaiQianpay = "./KuaiQianPay/send.aspx?OrderINT=" + strOrderINT;
            }
            else
            {
                String strOrderNumAll = Request.QueryString["OrderNum"].ToString();///可能是订单连号
                Decimal myAllDecimal = Decimal.Parse(Request.QueryString["myAllMoney"].ToString());
                String strOrderNameAll = Request.QueryString["OrderName"].ToString();

                string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();


                strAplipay = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/Alipay/default.aspx?OrderNum=" + strOrderNumAll + "&myAllMoney=" + Eggsoft_Public_CL.Pub.getPubMoney(myAllDecimal) + "&OrderName=" + strOrderNameAll;
                strWeiXinPay = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/v3pay_weixin/default.aspx?ShopClientID=" + strShopClientID + "&OrderNum=" + strOrderNumAll + "&myAllMoney=" + Eggsoft_Public_CL.Pub.getPubMoney(myAllDecimal) + "&OrderName=" + strOrderNameAll;
                strKuaiQianpay = "./KuaiQianPay/send.aspx?OrderNum=" + strOrderNumAll + "&myAllMoney=" + Eggsoft_Public_CL.Pub.getPubMoney(myAllDecimal) + "&OrderName=" + strOrderNameAll;

            }
            strContent = strContent.Replace("###Aplipay###", "<a href=\"" + strAplipay + "\" ><div id=\"buybtn\"  class=\"spro_mybtn\">支付宝支付</div></a>");
            strContent = strContent.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());

            //int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            //EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            //EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(userID);

            //if ((Modeltab_User.Subscribe) && (Modeltab_User.SocialPlatform == "WeiXin"))
            //{
            strContent = strContent.Replace("###WeiXinPay###", "<a href=\"" + strWeiXinPay + "\" ><div id=\"cartbtn\"  class=\"spro_mybtn\">微信支付</div></a>");
            //}
            //else
            //{
            //    strContent = strContent.Replace("###WeiXinPay###", "");
            //}


            //strContent = strContent.Replace("###KuaiQianpay###", "<a href=\"" + strKuaiQianpay + "\" ><div id=\"buyKuaibtn\"  class=\"spro_mybtn\">快钱支付</div></a>");



            return strContent;




        }
        //
        /// <summary>
        /// 在付钱之前 修改订单的parentid
        /// </summary>
        private void pBeforePayCheckAllParentIDAndRechargeIt()
        {

            try
            {
                #region 得到订单号
                string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID
                string strOrdernum = Request.QueryString["OrderNum"];///可能是订单连号  
                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order Model_tab_Order = null;
                if (string.IsNullOrEmpty(strOrderINT))//订单记录的ID
                {
                    Model_tab_Order = BLL_tab_Order.GetModel("OrderNum='" + strOrdernum + "'");
                }
                else
                {
                    Model_tab_Order = BLL_tab_Order.GetModel(Int32.Parse(strOrderINT));
                }

                #endregion

                #region 得到当前用户的 parentID   等等
                if (Model_tab_Order == null)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("找不到strOrderINT=" + strOrderINT + "  strOrdernum=" + strOrdernum, "在付钱之前 修改订单的parentid");
                }
                else
                {
                    int intUserID = Convert.ToInt32(Model_tab_Order.UserID);
                    int intParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(intUserID);
                    int intGrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intParentID);
                    int intGreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intGrandParentID);

                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                    EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    System.Data.DataTable DataTableParent = BLL_tab_Orderdetails.GetList("ID", "OrderID=" + Model_tab_Order.ID).Tables[0];
                    ArrayList ArrayListSQL = new ArrayList();

                    for (int i = 0; i < DataTableParent.Rows.Count; i++)
                    {
                        string strtab_OrderdetailsID = DataTableParent.Rows[i]["ID"].ToString();
                        Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(Int32.Parse(strtab_OrderdetailsID));
                        if (Model_tab_Orderdetails.ParentID != intParentID)
                        {
                            string strUpdate = "update tab_Orderdetails set ParentID=" + intParentID + ",GrandParentID=" + intGrandParentID + ",GreatParentID=" + intGreatParentID + ",UpdateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where id=" + strtab_OrderdetailsID;
                            Eggsoft.Common.debug_Log.Call_WriteLog("oldparentID=" + Model_tab_Orderdetails.ParentID + " " + strUpdate, "在付钱之前 修改订单的parentid");
                            ArrayListSQL.Add(strUpdate);
                        }
                    }
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);

                }

                #endregion

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