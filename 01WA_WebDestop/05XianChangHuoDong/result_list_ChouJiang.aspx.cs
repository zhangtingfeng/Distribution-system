using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _01WA_WebDestop._05XianChangHuoDong
{
    public partial class result_list_ChouJiang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strBonusNumberByShopClientID = HttpContext.Current.Request["BonusNumberByShopClientID"];//是不是访问代理的网页；
            String strShopClientID = HttpContext.Current.Request["ShopClientID"];//是不是访问代理的网页；
            strBonusNumberByShopClientID = Eggsoft.Common.CommUtil.SafeFilter(strBonusNumberByShopClientID);
            strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(strShopClientID);


            string strWhere = "SELECT   tab_ShopClient_XianChangHuoDong_Bonus_User.ID as ID,tab_ShopClient_XianChangHuoDong_Bonus_User.UserID, ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Bonus_User.XianChangHuoDongBonusNumberbyShopClientID,  ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID,  ";
            strWhere += "   tab_User.NickName,  ";
            strWhere += "   tab_ShopClient_XianChangHuoDong_Bonus_User.GetBonusName, tab_User.ShopUserID,  ";
            strWhere += "   tab_User.ContactPhone, tab_User.ContactMan, tab_User.UserRealName,  ";
            strWhere += "   tab_User.HeadImageUrl ";
            strWhere += "  FROM      tab_ShopClient_XianChangHuoDong_Bonus_User LEFT OUTER JOIN ";
            strWhere += "   tab_User ON tab_ShopClient_XianChangHuoDong_Bonus_User.UserID = tab_User.ID ";
            strWhere += "  where  tab_User.ShopClientID={0} and tab_ShopClient_XianChangHuoDong_Bonus_User.ShopClientID={0} ";
            strWhere += "  and tab_ShopClient_XianChangHuoDong_Bonus_User.XianChangHuoDongBonusNumberbyShopClientID={1} ";
            strWhere += "  and tab_ShopClient_XianChangHuoDong_Bonus_User.GetBonusName is not null ";
            strWhere += "  order by tab_ShopClient_XianChangHuoDong_Bonus_User.UpdateTime desc";

            strWhere = String.Format(strWhere, strShopClientID, strBonusNumberByShopClientID);

            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User BLL_tab_ShopClient_XianChangHuoDong_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();
            System.Data.DataSet DataSetNumber_XianChangHuoDong_Bonus_User = BLL_tab_ShopClient_XianChangHuoDong_Bonus_User.SelectList(strWhere);

            GridView1.DataSource = DataSetNumber_XianChangHuoDong_Bonus_User.Tables[0];   //设置绑定数据源
            GridView1.DataKeyNames = new string[] { "ID" };//设置主键
            GridView1.DataBind();      //绑定控件
        }
    }
}