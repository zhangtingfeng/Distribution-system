using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._25WeiXianChang
{
    public partial class _25WeiXianChang_BoardSet : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        public string strUploadUrl = ConfigurationManager.AppSettings["UpLoadURL"];
        string str_Pub_ShopClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        { 
            #region 没有打开的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_25WeiXianChang_BoardSet")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开的权限
            str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            BindBigClassXianChangHuoDong();
        }


        //    <%# Eval("ID") %>
        public static string GetColor(string strID)
        {
            String strColor = "";

            int conToInt16 = Convert.ToInt32(strID);
            bool mybool = Convert.ToBoolean(conToInt16 % 2);
            if (mybool)
            {
                strColor = "#ECF5FF";
            }
            else
            {
                strColor = "#E3E3E3";
            }

            return strColor;
        }





        public void BindBigClassXianChangHuoDong()
        {
            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong bll_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();

            string strSubscribe_Must = "";
            strSubscribe_Must += " case when Subscribe_Must=1";
            strSubscribe_Must += "      then '是'";
            strSubscribe_Must += "       when Subscribe_Must=0";
            strSubscribe_Must += "       then '否'";
            strSubscribe_Must += " end  as Subscribe_Must";

            string strActivityState = "";
            strActivityState += " case when ActivityState=1";
            strActivityState += "      then '是'";
            strActivityState += "       when ActivityState=0";
            strActivityState += "       then '否'";
            strActivityState += " end  as ActivityState";

            string strGetBonusRepeat = "";
            strGetBonusRepeat += " case when GetBonusRepeat=1";
            strGetBonusRepeat += "      then '是'";
            strGetBonusRepeat += "       when GetBonusRepeat=0";
            strGetBonusRepeat += "       then '否'";
            strGetBonusRepeat += " end  as GetBonusRepeat";

            string strAddress_Must = "";
            strAddress_Must += " case when Address_Must=1";
            strAddress_Must += "      then '是'";
            strAddress_Must += "       when Address_Must=0";
            strAddress_Must += "       then '否'";
            strAddress_Must += " end  as Address_Must";

            string strSQL = "";
            strSQL += " SELECT id,[ActivityName],[ShopClientID],[ShowAgentErWeiMa_UserID_ByAgent]," + strSubscribe_Must;
            strSQL += "," + strActivityState + "," + strGetBonusRepeat + ",[GetBonusRepeat_OneDrawBonus]," + strAddress_Must + " from tab_ShopClient_XianChangHuoDong where IsDeleted = 0 and ShopClientID=" + str_Pub_ShopClientID;
            strSQL += "  order by Sort asc,ID asc";
            this.DataList1tab_ShopClient_XianChangHuoDong.DataSource = bll_tab_ShopClient_XianChangHuoDong.SelectList(strSQL);
            DataList1tab_ShopClient_XianChangHuoDong.DataKeyField = "ID";
            this.DataList1tab_ShopClient_XianChangHuoDong.DataBind();
        }

        //protected String getBigScreentLink(String strARGShowAgentErWeiMa_UserID_ByAgent)
        //{ 

        //}

        /// <summary>
        /// 展示推广的二维码
        /// </summary>
        /// <param name="strARGShowAgentErWeiMa_UserID_ByAgent"></param>
        /// <returns></returns>
        protected String getShowAgentErWeiMa_UserID_ByAgent(String strARGShowAgentErWeiMa_UserID_ByAgent)
        {
            string strReturn = "";

            #region  代理的二维码
            int intShowAgentErWeiMa_UserID_ByAgent = 0;
            int.TryParse(strARGShowAgentErWeiMa_UserID_ByAgent, out intShowAgentErWeiMa_UserID_ByAgent);
            if (intShowAgentErWeiMa_UserID_ByAgent == 0)
            {
                strReturn = "微信服务号二维码";
            }
            else
            {
                EggsoftWX.BLL.View_ShopClient_Agent bll_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
                EggsoftWX.Model.View_ShopClient_Agent Model_View_ShopClient_Agent = bll_View_ShopClient_Agent.GetModel("ShopClientID=" + str_Pub_ShopClientID + " and userID=" + intShowAgentErWeiMa_UserID_ByAgent);
                if (Model_View_ShopClient_Agent != null)
                {
                    strReturn = "代理ID：" + Model_View_ShopClient_Agent.ShopUserID + "<br />昵称：" + Model_View_ShopClient_Agent.NickName + "<br />昵称：" + Model_View_ShopClient_Agent.UserRealName + "<br />店铺名称：" + Model_View_ShopClient_Agent.ShopName + "<br />联系方式：" + Model_View_ShopClient_Agent.ContactPhone;
                }
            }
            #endregion

            return strReturn;
        }

        protected void DataList1_ItemDataBoundtab_ShopClient_XianChangHuoDong(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string strActivityState = drv["ActivityState"].ToString();
                HyperLink HyperLinkBigScreenLink = (HyperLink)e.Item.FindControl("HyperLinkBigScreenLink");
                if (strActivityState == "是")
                {
                    HyperLinkBigScreenLink.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["ClientWebDestopURL"] + "/05XianChangHuoDong/WF_YaoYiYao-" + str_Pub_ShopClientID + "-" + drv.Row.ItemArray[0].ToString() + ".aspx";
                    HyperLinkBigScreenLink.Text = "启动大屏幕";
                    HyperLinkBigScreenLink.Target = "_blank";
                    HyperLinkBigScreenLink.Attributes.Add("onclick", "return confirm('大屏幕只能启动一个，启动多次会出现异常。确认启动吗？');");
                }

                #region HyperLink_Numbers
                string strID = drv["ID"].ToString();
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                int intExsitCount = BLL_tab_ShopClient_XianChangHuoDong_Number.ExistsCount("XianChangHuoDongID=" + strID + " and IsDoing=0 and ShopClientID=" + str_Pub_ShopClientID);
                HyperLink HyperLink_NumbersLink = (HyperLink)e.Item.FindControl("HyperLink_Numbers");
                HyperLink_NumbersLink.Text = intExsitCount.ToString();
                HyperLink_NumbersLink.NavigateUrl = "25tab_ShopClient_XianChangHuoDong_Number.aspx?XianChangHuoDongID=" + strID;
                #endregion


                #region HyperLink_Bonus
                //string strID = drv["ID"].ToString();
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus BLL_tab_ShopClient_XianChangHuoDong_Bonus = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus();
                intExsitCount = BLL_tab_ShopClient_XianChangHuoDong_Bonus.ExistsCount("XianChangHuoDongID=" + strID + " and ShopClientID=" + str_Pub_ShopClientID);
                HyperLink HyperLink_Bonus = (HyperLink)e.Item.FindControl("HyperLink_Bonus");
                HyperLink_Bonus.Text = intExsitCount.ToString();
                HyperLink_Bonus.NavigateUrl = "25tab_ShopClient_XianChangHuoDong_Bonus.aspx?XianChangHuoDongID=" + strID;
                #endregion



                //drv["ID"] = "";
                ///Response.Write(drv.Row.ItemArray[0].ToString() + "<br />");//drv.Row.ItemArray[0]就是你要取的数据源中的第0列了，你的Uname在第几列就自己写了。。。
            }

        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("25WeiXianChang_Manage.aspx?type=add");
        }
    }
}