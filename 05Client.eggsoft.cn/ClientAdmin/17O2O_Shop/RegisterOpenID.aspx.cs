using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._17O2O_Shop
{
    public partial class RegisterOpenID : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private const String strOpenIDSessionName = "OpenO2OIDSession";

        private EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();


        public String pubStringUserListID_ForCheck_ = "0";
        public String pubstrinto2oIDID_ = "0";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(IsPostBack))
            {
                try
                {
                    string type = Request.QueryString["type"];
                    string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    //tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strShopClientID));

                    pubstrinto2oIDID_ = Request.QueryString["into2oID"];// 修改ID
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_O2O_ShopInfo = BLL_O2O_ShopInfo.GetModel("ID=" + pubstrinto2oIDID_ + "");


                    if (type.ToLower() == "askralation")
                    {
                        Eggsoft.Common.Session.Delete(strOpenIDSessionName);
                        Eggsoft.Common.Session.Add(strOpenIDSessionName, pubstrinto2oIDID_);
                        pubStringUserListID_ForCheck_ = Eggsoft_Public_CL.Pub.Get_WeiXinRalationUserID_o2o_List_ID_FromDateBase(pubstrinto2oIDID_);
                        if (String.IsNullOrEmpty(pubStringUserListID_ForCheck_)) pubStringUserListID_ForCheck_ = "0";
                        Image_RegisterOpenID.ImageUrl = Eggsoft_Public_CL.Pub_GetOpenID_And_.MakeOpenIDBitmap(Int32.Parse(strShopClientID), "relationshop_o2o_client" + pubstrinto2oIDID_, true);
                    }
                    else if (type.ToLower() == "clearralation")
                    {
                        string strXML = Model_O2O_ShopInfo.XML;

                        try
                        {
                            Eggsoft_Public_CL.XML__Class_Shop_O2o XML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_O2o>(strXML, System.Text.Encoding.UTF8);
                            XML__Class_Shop_O2o.WeiXinRalationUserIDList = "";
                            Model_O2O_ShopInfo.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_O2o, System.Text.Encoding.UTF8);
                            BLL_O2O_ShopInfo.Update(Model_O2O_ShopInfo);

                        }
                        catch (Exception Exceptione)
                        {
                            debug_Log.Call_WriteLog("ClearRalation:" + Exceptione.ToString());
                        }
                        finally
                        {
                            //...
                        }
                        Eggsoft.Common.JsUtil.TipAndRedirect("清除成功", "/ClientAdmin/17O2O_Shop/Board_O2O_ShopOperating.aspx?type=Modify&ID="+ pubstrinto2oIDID_, "3");
                    }
                }
                catch (Exception Exceptione)
                {
                    debug_Log.Call_WriteLog("tellShopClientID_UserPayMone111y_ByWeiXin:" + Exceptione.ToString());
                }

                finally
                { }
            }
        }

       

    }
}