using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._05EngineerMode
{
    public partial class WeiXinMenu_Set : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin BLL_tab_ShopClient_System_Menu_WeiXin = new EggsoftWX.BLL.tab_ShopClient_System_Menu_WeiXin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                String str_Type_MakeThisMenuPost = Request.QueryString["type"];
                if (str_Type_MakeThisMenuPost != null)
                {
                    if (str_Type_MakeThisMenuPost == "MakeThisMenuPost")
                    {

                        string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                        String strGet_ACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(Int32.Parse(strShopClientID));
                        //String strGet_ACCESS_TOKEN = Eggsoft_Public_CL.Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(0,Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform());
                        string ErrInfo = "";
                        if (pSetMenu(strGet_ACCESS_TOKEN, out ErrInfo))
                        {
                            Eggsoft.Common.JsUtil.ShowMsgNew("菜单设置成功！ 详细描述是;" + ErrInfo, "WeiXinMenu.aspx");
                        }
                        else
                        {
                            Eggsoft.Common.JsUtil.ShowMsgNew("菜单设置失败！ 详细描述是;" + ErrInfo, "WeiXinMenu.aspx");
                        }

                    }
                }
            }
        }





        private void pdeleteMenu(String strGet_ACCESS_TOKEN)
        {
            string strDelUrl;

            if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
            {
                strDelUrl = "https://api.yixin.im/cgi-bin/menu/delete?access_token=" + strGet_ACCESS_TOKEN;
            }
            else
            {
                strDelUrl = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + strGet_ACCESS_TOKEN;
            }

            string strResult2 = PostMenu.PostXml(strDelUrl, "");
        }


        private string pGetMenu()
        {
            string strData;

            strData = "";

            //微导航	餐饮行业	
            //微官网	房产行业	微充值
            //微店	汽车4S店	微动力
            //会员卡	酒店	微培训
            //微群聊	微订票	微服务
            //业务模板	案例	关于我们

            /// http://localhost:13937/_Admin/EngineerMode/WeiXinMenu.aspx?type=MakeThisMenuPost 本机调试 手动调用
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            bool myBool = BLL_tab_ShopClient_System_Menu_WeiXin.Exists("ParentID=0 and ShopClientID=" + strShopClientID);
            if (myBool)
            {


                strData += "{";
                strData += "\"button\":";
                strData += "[";


                System.Data.DataTable myDataTable = BLL_tab_ShopClient_System_Menu_WeiXin.GetList("ParentID=0 and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strMenuName = myDataTable.Rows[i]["MenuName"].ToString();
                    string strMMenuType = myDataTable.Rows[i]["MenuType"].ToString();
                    string strMenuContent = myDataTable.Rows[i]["MenuContent"].ToString();
                    string strID = myDataTable.Rows[i]["ID"].ToString();


                    bool myChild = BLL_tab_ShopClient_System_Menu_WeiXin.Exists("ParentID=" + strID);

                    if (myChild == false)
                    {
                        if (Int32.Parse(strMMenuType) == 4)
                        {
                            strData += "{\"type\":\"view\",\"name\":\"" + strMenuName + "\",\"url\":\"" + strMenuContent + "\"}";
                        }
                        else
                        {
                            strData += "{\"type\":\"click\",\"name\":\"" + strMenuName + "\",\"key\":\"Call_EventKey#" + "" + strMMenuType + "#" + strMenuContent + "\"}";
                        }

                    }
                    else
                    {
                        strData += "{\"name\":\"" + strMenuName + "\",";
                        strData += "\"sub_button\":";
                        strData += "[";


                        System.Data.DataTable myChildDataTable = BLL_tab_ShopClient_System_Menu_WeiXin.GetList("ParentID=" + strID + " and ShopClientID=" + strShopClientID + " order by pos asc,id asc").Tables[0];
                        for (int j = 0; j < myChildDataTable.Rows.Count; j++)
                        {
                            string strChildMenuName = myChildDataTable.Rows[j]["MenuName"].ToString();
                            string strChildMMenuType = myChildDataTable.Rows[j]["MenuType"].ToString();
                            string strChildMenuContent = myChildDataTable.Rows[j]["MenuContent"].ToString();
                            string strChildID = myChildDataTable.Rows[j]["ID"].ToString();

                            if (Int32.Parse(strChildMMenuType) == 4)
                            {
                                strData += "{\"type\":\"view\",\"name\":\"" + strChildMenuName + "\",\"url\":\"" + strChildMenuContent + "\"}";
                            }
                            else
                            {
                                strData += "{\"type\":\"click\",\"name\":\"" + strChildMenuName + "\",\"key\":\"Call_EventKey#" + "" + strChildMMenuType + "#" + strChildMenuContent + "\"}";
                            }
                            if (j < myChildDataTable.Rows.Count - 1) strData += ",";
                        }
                        strData += "]}";
                    }
                    if (i < myDataTable.Rows.Count - 1) strData += ",";
                }
                strData += "]";
                strData += "}";
            }


            return strData;





        }


        private bool pSetMenu(String strGet_ACCESS_TOKEN, out string ErrInfo)
        {
            //string strResult3 = PostMenu.delete_PostMenu();

            string access_token = strGet_ACCESS_TOKEN;

            string posturl = "";
            if (Eggsoft_Public_CL.Pub_SocialPlatform.Check_SocialPlatform() == "YiXin")
            {
                posturl = "https://api.yixin.im/cgi-bin/menu/create?access_token=" + access_token;
            }
            else
            {
                posturl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            }



            string menuStr = pGetMenu();


            //menuStr = responeJsonStr;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] postdata = Encoding.GetEncoding("UTF-8").GetBytes(menuStr);
            request.ContentLength = postdata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(postdata, 0, postdata.Length);
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();//得到结果
            ErrInfo = content;
            bool myBool = false;
            if (content.IndexOf("ok") != -1)
            {
                myBool = true;
            }
            return myBool;
            //Response.Write(content);

        }

    }
}