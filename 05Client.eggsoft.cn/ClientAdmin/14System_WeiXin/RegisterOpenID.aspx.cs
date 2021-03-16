using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._14System_WeiXin
{
    public partial class RegisterOpenID : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private const String strOpenIDSessionName = "OpenIDSession";

        private EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
        private EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();

        public String pubStringUserListID_ForCheck_ = "0";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(IsPostBack))
            {
                try
                {
                    string type = Request.QueryString["type"];
                    string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strShopClientID));
                    if (type.ToLower() == "askralation")
                    {
                        Eggsoft.Common.Session.Delete(strOpenIDSessionName);
                        Eggsoft.Common.Session.Add(strOpenIDSessionName, strShopClientID);

                        pubStringUserListID_ForCheck_ = Eggsoft_Public_CL.Pub.Get_WeiXinRalationUserIDList_ID_FromDateBase(strShopClientID);
                        if (String.IsNullOrEmpty(pubStringUserListID_ForCheck_)) pubStringUserListID_ForCheck_ = "0";


                        Image_RegisterOpenID.ImageUrl = Eggsoft_Public_CL.Pub_GetOpenID_And_.MakeOpenIDBitmap(Int32.Parse(strShopClientID), "RelationShopClient_" + strShopClientID, true);

                        /*                    string stringOpenID = GetOpenID_FromDateBase();
                                            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();

                                            if (BLLtab_User.Exists("OpenID='" + stringOpenID + "'"))
                                            {
                                                string strID = new EggsoftWX.BLL.tab_User().GetList("ID", "OpenID='" + stringOpenID + "'").Tables[0].Rows[0][0].ToString();
                                                Literal_ID.Text = strID;
                                            }
                                            else
                                            {
                                                Literal_ID.Text = "w";
                                            }
                         */
                    }
                    else if (type.ToLower() == "clearralation")
                    {
                        string strXML = tab_ShopClient_Model.XML;

                        try
                        {
                            Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                            XML__Class_Shop_Client.WeiXinRalationUserIDList = "";
                            tab_ShopClient_Model.XML = Eggsoft.Common.XmlHelper.XmlSerialize(XML__Class_Shop_Client, System.Text.Encoding.UTF8);
                            tab_ShopClient_bll.Update(tab_ShopClient_Model);

                        }
                        catch (Exception Exceptione)
                        {
                            debug_Log.Call_WriteLog("ClearRalation:" + Exceptione.ToString());
                        }
                        finally
                        {
                            //...
                        }
                        Eggsoft.Common.JsUtil.TipAndRedirect("清除成功", "/ClientAdmin/10tab_ShopClient/BoardINC_Manage.aspx?type=Modify", "3");
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

       
        /*
        public String ChengIFSaoMiao()
        {
            String String_OpenIDSession = Eggsoft.Common.Session.Read(strOpenIDSessionName);
            String String_OpenID = GetOpenID_FromDateBase();
            if (String_OpenIDSession != String_OpenID)
            {
                string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                return String_OpenID;
            }
            else
            {
                return "0";
            }
        }*/


        //public void SaySomeThing()
        //{
        //    string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
        //    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        //    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
        //    Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + strID + "");
        //    string strOpenID = Model_tab_ShopClient.OpenID;



        //}



        //private static String GetOpenID_FromDateBase()
        //{
        //    string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
        //    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
        //    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
        //    Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("ID=" + strID + "");

        //    string strOpenID = "0";
        //    if (Model_tab_ShopClient != null)
        //    {
        //        strOpenID = Model_tab_ShopClient.OpenID;
        //    }
        //    else
        //    {

        //    }
        //    return strOpenID;
        //}

    }
}