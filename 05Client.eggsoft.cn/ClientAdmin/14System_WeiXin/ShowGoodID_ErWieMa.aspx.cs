using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._14System_WeiXin
{
    public partial class ShowGoodID_ErWieMa : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                try
                {
                    string strGoodID = Request.QueryString["GoodID"];
                    if (String.IsNullOrEmpty(strGoodID) == false)
                    {
                        Image_GoodID.ImageUrl = Eggsoft_Public_CL.Pub_GetOpenID_And_.MakeOpenIDBitmap(Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(Int32.Parse(strGoodID)), "Good_" + strGoodID, false);
                    }
                }
                catch (Exception Exceptione)
                {
                    debug_Log.Call_WriteLog("Image_GoodID.ImageUrl:" + Exceptione.ToString());
                }

                finally
                { }
            }
        }
    }
}