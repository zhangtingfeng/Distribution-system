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
    public partial class _25tab_ShopClient_XianChangHuoDong_Number_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strHuoDong_NumberID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strHuoDong_NumberID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number bll_HuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_HuoDong_Number = bll_HuoDong_Number.GetModel(Int32.Parse(strHuoDong_NumberID));

                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum bll_UserShakeNum_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
                    bll_UserShakeNum_User.Delete("XianChangHuoDongNumberbyShopClientID=" + Model_HuoDong_Number.XianChangHuoDongNumberbyShopClientID + " and UserShopClientID=" + Model_HuoDong_Number.ShopClientID);

                    bll_HuoDong_Number.Delete(Int32.Parse(strHuoDong_NumberID));
                    string strCallBackUrl = Request.QueryString["CallBackUrl"];
                    strCallBackUrl = strCallBackUrl.Replace("*", "?").Replace("@", "&");
                    JsUtil.ShowMsg("删除成功!", strCallBackUrl);
                }
            }
        }
    }
}