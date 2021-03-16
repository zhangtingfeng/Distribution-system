using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _06ThirdplatForm.eggsoft.cn.v3pay_weixin
{
    public partial class CheckIfGetWinXinMoney : System.Web.UI.Page
    {
        public string strPubOrderNum = "";
        private static Object thislock = new Object();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lock (thislock)
                {
                    try
                    {
                        strPubOrderNum = Request.QueryString["OrderNum"].ToString();
                        //Eggsoft.Common.debug_Log.Call_WriteLog("strPubOrderNum=" + strPubOrderNum);
                        if (string.IsNullOrEmpty(strPubOrderNum) == false)
                        {

                            EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();
                            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                            my_Model_tab_Order = my_tab_Order.GetModel("OrderNum='" + strPubOrderNum + "'");
                            string strParent = "";
                            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                            //Eggsoft.Common.debug_Log.Call_WriteLog("2");
                            if (BLL_tab_Orderdetails.Exists("OrderID=" + my_Model_tab_Order.ID))
                            {
                                EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("OrderID=" + my_Model_tab_Order.ID);
                                int intParentID = Convert.ToInt32(Model_tab_Orderdetails.ParentID);
                                if (intParentID > 0)
                                {
                                    strParent = intParentID.ToString();
                                }

                                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + my_Model_tab_Order.UserID + " and (isnull(Empowered, 0) = 1  or OnlyIsAngel=1)" + " and ShopClientID=" + my_Model_tab_Order.ShopClient_ID+ "  and IsDeleted=0 ");///有代理啊
                                string strAgent = "";
                                if (boolAgent)
                                {
                                    strAgent = "/sagent-" + my_Model_tab_Order.UserID;
                                }
                                else if (string.IsNullOrEmpty(strParent) == false)
                                {
                                    strAgent = "/sagent-" + strParent;
                                }
                                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Convert.ToInt32(my_Model_tab_Order.UserID));

                                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));


                                if (Convert.ToBoolean(my_Model_tab_Order.PayStatus))
                                {
                                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                                    strErJiYuMing = "https://" + strErJiYuMing + strAgent + "/cart_good2.aspx";
                                    Eggsoft.Common.JsUtil.LocationNewHref(strErJiYuMing);

                                }
                            }
                        }

                    }
                    catch (System.Threading.ThreadAbortException ettt)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                    }
                    catch (Exception ee)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(ee);
                    }
                    finally { }
                }
            }
        }
    }
}