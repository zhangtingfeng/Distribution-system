using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class ResourceSavePost : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                String strReturnURL = Request.Form["ReturnURL"];
                String strType = Request.Form["type"];
                String strResourceID = Request.Form["ResourceID"];
                String strTextContent = Server.HtmlEncode(Request.Form["TextContent"]);

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();



                EggsoftWX.BLL.tab_ShopClient_System_XML_Resource myBLLnew = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();

                if (strType == "Modify")
                {
                    //Eggsoft.Common.JsUtil.TipAndRedirect("Text='" + strTextContent + "'", "ID=" + strResourceID, "ResourceSavePost.aspx");
                    //Eggsoft.Common.JsUtil.ShowMsg("Text='" + strTextContent + "'", "ID=" + strResourceID);

                    myBLLnew.Update("Text='" + strTextContent + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);

                    //Response.Write("1");


                }
                else if (strType == "Modify2")
                {
                    String strTextURL = Server.HtmlEncode(Request.Form["TextURL"]);
                    if ((strTextURL != null) && (strTextURL != "")) strTextURL = strTextURL.Trim();
                    String strPICURL = Server.HtmlEncode(Request.Form["PICURL"]);


                    myBLLnew.Update(" Text='" + strTextContent + "', Pic='" + strPICURL + "' , LinkURL='" + strTextURL + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);

                    //Response.Write("1");


                }
                else if (strType == "Modify5")
                {
                    String strPICURL = Server.HtmlEncode(Request.Form["PICURL"]);


                    myBLLnew.Update(" Text='" + Server.HtmlDecode(strTextContent) + "', Pic='" + strPICURL + "' , LinkURL='" + "" + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);

                    //Response.Write("1");



                }


                else if (strType == "Modify3")
                {
                    String strTextURL = Server.HtmlEncode(Request.Form["TextURL"]);
                    if ((strTextURL != null) && (strTextURL != "")) strTextURL = strTextURL.Trim();
                    String strPICURL = Server.HtmlEncode(Request.Form["PICURL"]);


                    myBLLnew.Update(" Text='" + strTextContent + "', Pic='" + strPICURL + "' , LinkURL='" + strTextURL + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);

                }
                else if (strType == "Modify6")
                {
                    //String strText = Server.HtmlEncode(Request.Form["Text"]);
                    //if ((strText != null) && (strText != "")) strText = strText.Trim();
                    String VoiceURL = Server.HtmlDecode(Request.Form["TextContent"]);


                    myBLLnew.Update(" Text='" + VoiceURL + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }
                else if (strType == "Modify7")
                {
                    String strVideo = Server.HtmlDecode(Request.Form["Text"]);
                    myBLLnew.Update(" Text='" + strVideo + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }
                else if (strType == "Modify8")
                {
                    String strText = Server.HtmlEncode(Request.Form["Text"]);
                    if ((strText != null) && (strText != "")) strText = strText.Trim();
                    String strPICURL = Server.HtmlEncode(Request.Form["PICURL"]);


                    myBLLnew.Update(" Text='" + strText + "', Pic='" + strPICURL + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }


                else if (strType == "New3_AddTail")
                {
                    String strTextURL = Server.HtmlEncode(Request.Form["TextURL"]);
                    if ((strTextURL != null) && (strTextURL != "")) strTextURL = strTextURL.Trim();
                    String strPICURL = Server.HtmlEncode(Request.Form["PICURL"]);

                    int intMax = myBLLnew.GetMaxId();

                    EggsoftWX.Model.tab_ShopClient_System_XML_Resource myModelnew = new EggsoftWX.Model.tab_ShopClient_System_XML_Resource();
                    myModelnew.ShopClientID = Int32.Parse(strShopClientID);
                    myModelnew.ParentID = Convert.ToInt32(strResourceID);
                    myModelnew.Text = strTextContent;
                    myModelnew.Pic = strPICURL;
                    myModelnew.LinkURL = strTextURL;
                    myModelnew.type = 3;

                    myBLLnew.Add(myModelnew);

                    //myBLLnew.Add((" ParentID=" + strResourceID + ", Text='" + strTextContent + "', Pic='" + strPICURL + "' , LinkURL='" + strTextURL + "'", "ID=" + strResourceID);

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);

                }
                else if (strType == "Delete")
                {
                    myBLLnew.Delete(Convert.ToInt32(strResourceID));
                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }
                else if (strType == "Delete3")///多图文
                {
                    if (myBLLnew.Exists("ParentID=" + strResourceID))
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("含有下一个图文消息，不能删除！");
                    }
                    else
                    {
                        myBLLnew.Delete(Convert.ToInt32(strResourceID));
                    }
                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }
                else if (strType == "ModifySelectThisJPG")
                {
                    if (strReturnURL.IndexOf("?") == -1)
                    {
                        strReturnURL = strReturnURL + "?ID=" + strResourceID + "&type=ModifySelectThisJPG&PicURL=" + Server.HtmlDecode(strTextContent);
                    }
                    else
                    {
                        strReturnURL = strReturnURL + "&ID=" + strResourceID + "&type=ModifySelectThisJPG&PicURL=" + Server.HtmlDecode(strTextContent);
                    }
                    //myBLLnew.Update("Pic=" + Server.HtmlDecode(strTextContent) + "", "ID=" + strResourceID);
                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }
                else if (strType == "AddNew")///新增图片
                {
                    //if (strReturnURL.IndexOf('?') != -1)
                    //{
                    //    strReturnURL = strReturnURL + "&AddNew=" + strTextContent;
                    //}
                    //else
                    //{
                    //    strReturnURL = strReturnURL + "?AddNew=" + Server.HtmlDecode(strTextContent);
                    //}



                    Eggsoft.Common.Session.Add("AddNewURL", Server.HtmlDecode(strTextContent));

                    //strReturnURL = strReturnURL.Replace("'", "");
                    //strReturnURL = strReturnURL.Replace("\"", "");

                    Eggsoft.Common.JsUtil.LocationNewHref(strReturnURL);
                }


            }
        }
    }
}