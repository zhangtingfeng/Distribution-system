using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _01WA_WebDestop._05XianChangHuoDong
{
    public partial class result_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strSceenXianChangHuoDongNumber = HttpContext.Current.Request["SceenXianChangHuoDongNumber"];//是不是访问代理的网页；
            String strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            strSceenXianChangHuoDongNumber = Eggsoft.Common.CommUtil.SafeFilter(strSceenXianChangHuoDongNumber);
            strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(strShopClientID);


            string strWhere = "SELECT   tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.ID as ID,tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserID, ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.XianChangHuoDongNumberbyShopClientID,  ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserShopClientID,  ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserNickName,  ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserShakeNumber, tab_User.ShopUserID,  ";
            strWhere += "   tab_User.ContactPhone, tab_User.ContactMan, tab_User.UserRealName,  ";
            strWhere += "   tab_User.HeadImageUrl ";
            strWhere += "  FROM      tab_ShopClient_XianChangHuoDong_Number_UserShakeNum LEFT OUTER JOIN ";
            strWhere += "   tab_User ON tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserID = tab_User.ID ";
            strWhere += "  where  tab_User.ShopClientID={0} and tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserShopClientID={0} and tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.XianChangHuoDongNumberbyShopClientID={1} order by tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.UserShakeNumber desc";
            strWhere = String.Format(strWhere, strShopClientID, strSceenXianChangHuoDongNumber);

            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
            System.Data.DataSet DataSetNumber_UserShakeNum = BLL_tab_ShopClient_XianChangHuoDong_Number_UserShakeNum.SelectList(strWhere);

            GridView1.DataSource = DataSetNumber_UserShakeNum.Tables[0];   //设置绑定数据源
            GridView1.DataKeyNames = new string[] { "ID" };//设置主键
            GridView1.DataBind();      //绑定控件
        }
    }
}