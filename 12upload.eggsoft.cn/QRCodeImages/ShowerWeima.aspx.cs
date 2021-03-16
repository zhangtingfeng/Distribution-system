using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.QRCodeImages
{
    public partial class ShowerWeima : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strType = Request.QueryString["type"];
            string strShopID = Request.QueryString["ShopID"];
            string strhttpUrl = Request.QueryString["httpUrl"];
            string strParentID = Request.QueryString["ParentID"];
            string strGoodsIDOrb004_OperationGoods = Request.QueryString["GoodsID"];
            string strGetGoodErWeiMaImage = Class_Pub.APPCODE_GetGoodErWeiMaImage(strType, strShopID, strhttpUrl, strParentID, strGoodsIDOrb004_OperationGoods);
            Image_ErWei_ShopClient.ImageUrl = strGetGoodErWeiMaImage;

        }
    }
}