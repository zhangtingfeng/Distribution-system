using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class MultiButton_AllMyMember : System.Web.UI.Page
    {
        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    setAllNeedID();
                

                    string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/MultiButton_AllMyMember.html");
                    strTemplet = strTemplet.Replace("###SAgentPath###", Pub_Agent_Path);
                    strTemplet = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strTemplet, "ShareShopFunction");//微信分享代码
                    strTemplet = InitOpenShopAsk(strTemplet);


                    strTemplet = strTemplet.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "微店首页"));

                    strTemplet = strTemplet.Replace("###Header###", "");

                    strTemplet = strTemplet.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                    string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                    strTemplet = strTemplet.Replace("###IFSubscribeHeader###", strSubscribe);
                    strTemplet = strTemplet.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                    #region 注销直推未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_MySonmember' and Readed=0", pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    #endregion 注销直推未读消息


                    Response.Write(strTemplet);
                }
                catch (System.Threading.ThreadAbortException ettt)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "前端所有会员");
                }
                finally
                {

                }
            }
        }

        private void setAllNeedID()
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            pub_Int_ShopClientID = Int32.Parse(strShopClientID);
            pub_Int_Session_CurUserID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
            int pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetParentID_Agent_From_Request_(pub_Int_Session_CurUserID);
            pub_GetAgentShopName_From_Visit__ = Eggsoft_Public_CL.Pub_Agent.GetAgentShopName_From_Visit__(pub_Int_Session_CurUserID, pub_Int_ShopClientID);
            Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
        }
        private String InitOpenShopAsk(string strTemplet)
        {



            int intOutAllMemShowStatic = 0;
            //userID = 38;
            string strBody = GetAllMember_listSList(pub_Int_Session_CurUserID,out intOutAllMemShowStatic);

            strTemplet = strTemplet.Replace("###AllMemShowStatic###", string.Format("总数统计：{0}", intOutAllMemShowStatic));

            //Decimal myCountWealth = 0;
            //Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(pub_Int_Session_CurUserID, out myCountWealth);
            //strTemplet = strTemplet.Replace("###TotalOrderCredits_OperationCenter###", "" + Eggsoft_Public_CL.Pub.getPubMoney(myCountWealth));
            strTemplet = strTemplet.Replace("###MyAllMember_list###", strBody);
            return strTemplet;
        }



        public static String GetAllMember_listSList(int intArgUserID, out int outIntShow)
        {
            string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();
            String strBody = "";

            strBody += "<li ms-repeat=\"items\">\n";
            strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>会员ID</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>会员昵称</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>上级ID</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>上级昵称</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>消费金额</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>角色</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "			</div>\n";
            strBody += "</li>\n";

            string strSQL = "";



            strSQL = @"DECLARE @tb1LevelOne TABLE
 (
 Id BIGINT,
 ShopUserID BIGINT,
 ParentID BIGINT,
 NickName NVARCHAR(50)
 )

INSERT INTO @tb1LevelOne
 (Id,
 ShopUserID,
 ParentID,
 NickName)
SELECT Id,
 ShopUserID,
 ParentID,
 NickName
FROM tab_User
WHERE ( ParentID = " + intArgUserID + @" )
 AND ( ID <> " + intArgUserID + @" )
AND ( ShopClientID =" + strShopClientID + @" )
SELECT top 1000 v55.Id,
       v55.NickName,
       v55.ParentID,
       v55.ShopUserID,
       Parentab_User.NickName   AS ParentNickName,
       Parentab_User.ShopUserID AS ParentShopUserID,
       Order_User.UsertotalMoney,
       tab_ShopClient_Agent_.Empowered,
       tab_ShopClient_Agent_.AgentLevelSelect
FROM  (SELECT ID,
              ShopUserID,
              NickName,
              ParentID
       FROM   @tb1LevelOne
       UNION
       SELECT ID,
              ShopUserID,
              NickName,
              ParentID
       FROM   (SELECT ID,
                      ShopUserID,
                      NickName,
                      ParentID
               FROM   dbo.tab_User
               WHERE  ( ParentID IN (SELECT ID
                                     FROM   @tb1LevelOne) )
                      AND ( ID NOT IN(SELECT ID
                                      FROM   @tb1LevelOne) ))v5) v55
      LEFT OUTER JOIN tab_User AS Parentab_User
                   ON Parentab_User.ID = v55.ParentID
      LEFT OUTER JOIN (SELECT UserID,
                              Sum(totalMoney) AS UsertotalMoney
                       FROM   tab_Order
                       WHERE  ShopClient_ID = " + strShopClientID + @"
                              AND IsDeleted = 0
                              AND PayStatus = 1
                       GROUP  BY UserID)Order_User
                   ON Order_User.UserID = v55.ID
      LEFT OUTER JOIN tab_ShopClient_Agent_
                   ON tab_ShopClient_Agent_.UserID = v55.ID
ORDER  BY id DESC

";

            EggsoftWX.BLL.b002_OperationCenter BLLb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
           

            System.Data.DataTable myDataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.Query(strSQL).Tables[0];

            outIntShow = myDataTable.Rows.Count;

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                string strShopUserID = myDataTable.Rows[i]["ShopUserID"].ToString();
                string strNickname = myDataTable.Rows[i]["NickName"].ToString();
                string strParentShopUserID = myDataTable.Rows[i]["ParentShopUserID"].ToString();
                string strParentNickName = myDataTable.Rows[i]["ParentNickName"].ToString();

                string strUserTotalMoney = myDataTable.Rows[i]["UserTotalMoney"].ToString();
                Boolean Empowered = myDataTable.Rows[i]["Empowered"].toBoolean();
                int AgentLevelSelect = myDataTable.Rows[i]["AgentLevelSelect"].toInt32();
                string strRole = "";
                if (Empowered == false)
                {
                    strRole = "天使";
                }
                else if (AgentLevelSelect > 0) {
                    strRole = "代理";
                }
                else 
                {
                    strRole = "分销";
                }
                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\" style=\"clear:both\">\n";

                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strShopUserID + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strNickname + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strParentShopUserID + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strParentNickName + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strUserTotalMoney + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Beans_16_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strRole + "</div>\n";
                strBody += "				</div>\n";


                strBody += "			</div>\n";
                strBody += "		</li>\n";



            }



            //myArgMoney = CountmyArgMoney;
            return strBody;

        }

    }
}