using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class CheckUserInfo : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            //string ShopID = Request.QueryString["ShopID"];
            //string Email = Request.QueryString["Email"];
            //string MD5ID = Request.QueryString["MD5ID"];

            if (type == "EmalCheck")
            {
                //QueryString.Add("ShopID", ShopClientID);
                //QueryString.Add("Email", strTo);
                //QueryString.Add("MD5ID", Eggsoft.Common.DESCrypt.Crypt(ShopClientID));


                string strShopID = "";
                string strEmail = "";
                string strMD5ID = "";

                QueryString_EggSoft QueryString = new QueryString_EggSoft(Request.QueryString["Data"]);
                foreach (String key in QueryString.Keys)
                {
                    strShopID = QueryString["ShopID"].ToString();
                    strEmail = QueryString["Email"].ToString();
                    strMD5ID = QueryString["MD5ID"].ToString();
                }



                if (strShopID == Eggsoft.Common.DESCrypt.DeCrypt(strMD5ID))
                {
                    try
                    {
                        EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
                        string strXML = tab_ShopClient_bll.GetModel(Int32.Parse(strShopID)).XML;


                        Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                        XML__Class_Shop_Client.CheckEmail = true;
                        XML__Class_Shop_Client.Email = strEmail;
                        string strmyXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.UnicodeEncoding.UTF8);
                        tab_ShopClient_bll.Update("XML='" + strmyXML__Class_Shop_Client + "'", "ID=" + strShopID);
                        Eggsoft.Common.JsUtil.TipAndRedirect("Email地址验证成功！", "https://" + HttpContext.Current.Request.Url.Host + "/ClientAdmin/default.aspx", "2");
                    }
                    catch { }
                    finally { }
                }
                else
                {
                    Eggsoft.Common.JsUtil.TipAndRedirect("Email地址验证失败！", "https://" + HttpContext.Current.Request.Url.Host, "2");
                }

            }

        }
    }
}