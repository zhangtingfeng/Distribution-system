using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._17O2O_Shop
{
    public partial class CheckUserEmailInfo : Eggsoft.Common.DotAdminPage_ClientAdmin
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


                string strinto2oID = "";
                string strEmail = "";
                string strMD5ID = "";

                QueryString_EggSoft QueryString = new QueryString_EggSoft(Request.QueryString["Data"]);
                foreach (String key in QueryString.Keys)
                {
                    strinto2oID = QueryString["into2oID"].ToString();
                    strEmail = QueryString["Email"].ToString();
                    strMD5ID = QueryString["MD5ID"].ToString();
                }



                if (strinto2oID == Eggsoft.Common.DESCrypt.DeCrypt(strMD5ID))
                {
                    try
                    {

                        EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                        EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_O2O_ShopInfo = BLL_O2O_ShopInfo.GetModel("ID=" + strinto2oID + "");
                        string strXML = BLL_O2O_ShopInfo.GetModel(Int32.Parse(strinto2oID)).XML;
                        Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_O2o = new Eggsoft_Public_CL.XML__Class_Shop_O2o();
                        if (string.IsNullOrEmpty(strXML))
                        {
                            XML__Class_Shop_O2o = new Eggsoft_Public_CL.XML__Class_Shop_O2o();
                        }
                        else
                        {
                            XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);
                        }
                        XML__Class_Shop_O2o.CheckEmail = true;
                        XML__Class_Shop_O2o.Email = strEmail;
                        string strmyXML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_O2o, System.Text.UnicodeEncoding.UTF8);
                        BLL_O2O_ShopInfo.Update("XML='" + strmyXML__Class_Shop_O2o + "'", "ID=" + strinto2oID);
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