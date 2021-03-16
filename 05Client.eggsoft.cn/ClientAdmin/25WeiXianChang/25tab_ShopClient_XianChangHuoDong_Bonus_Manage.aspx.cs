using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._25WeiXianChang
{
    public partial class _25tab_ShopClient_XianChangHuoDong_Bonus_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strHuoDong_BonusID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strHuoDong_BonusID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus bll_HuoDong_Bonus = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Bonus Model_HuoDong_Bonus = bll_HuoDong_Bonus.GetModel(Int32.Parse(strHuoDong_BonusID));

                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User bll_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();
                    bll_Bonus_User.Delete("XianChangHuoDongBonusNumberbyShopClientID=" + Model_HuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID + " and ShopClientID=" + Model_HuoDong_Bonus.ShopClientID);

                    bll_HuoDong_Bonus.Delete(Int32.Parse(strHuoDong_BonusID));

                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?").Replace("@", "&");
                    JsUtil.ShowMsg("删除成功!", strCallBackUrl);
                }
            }
        }
    }
}