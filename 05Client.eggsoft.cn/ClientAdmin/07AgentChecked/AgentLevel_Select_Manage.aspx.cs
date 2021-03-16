using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class AgentLevel_Select_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient_Agent_Level bll_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                int intExistsCount = bll_tab_ShopClient_Agent_Level.ExistsCount("ShopClientID=" + strShopClientID);

                if (intExistsCount > 1)////系统默认的有一个
                {
                    DataSet myds = null;
                    myds = bll_tab_ShopClient_Agent_Level.GetList("ID,AgentLevelName,AgentlevelMemo", " ShopClientID=" + strShopClientID + " order by sort asc,id asc");

                    string strUserID = Request.QueryString["UserID"];// 修改ID
                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent = bll_tab_ShopClient_Agent.GetModel("UserID=" + strUserID + " and ShopClientID=" + strShopClientID);

                    int intParentID = Model_tab_ShopClient_Agent.ParentID.toInt32();
                    if (intParentID > 0)
                    {
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_PID = bll_tab_ShopClient_Agent.GetModel("UserID=" + intParentID);
                        if (Model_tab_ShopClient_Agent_PID != null)
                        {
                            if (Model_tab_ShopClient_Agent_PID.AgentLevelSelect > 0)
                            {
                                EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = bll_tab_ShopClient_Agent_Level.GetModel("ID=" + Model_tab_ShopClient_Agent_PID.AgentLevelSelect);
                                if (Model_tab_ShopClient_Agent_PID != null)
                                {
                                    //Literal_ParentID_Show.Text = "上级代理" + Eggsoft_Public_CL.Pub.GetNickName(intParentID.ToString()) + " <span style=\"color:red;\">" + Model_tab_ShopClient_Agent_Level.AgentLevelName + "</span>";
                                    //Literal_ParentID_Show.Text += "<br />如批准下级代理的权限，其购物券将按照上级代理商的价格<span style=\"color:red;\">" + Eggsoft_Public_CL.Pub.getPubMoney(Model_tab_ShopClient_Agent_Level.GouWuQuanGoodPrice) + "元</span>等值进行折算,从上级代理那里取走购物券，等值折算给下级代理商";
                                    
                                }

                            }
                        }
                    }
                 
                    bool boolHaveSelected = false;


                    for (int i = 0; i < intExistsCount; i++)
                    {
                        string strD = myds.Tables[0].Rows[i]["ID"].ToString();
                        string strAgentLevelName = myds.Tables[0].Rows[i]["AgentLevelName"].ToString();
                        string strAgentlevelMemo = myds.Tables[0].Rows[i]["AgentlevelMemo"].ToString();

                        ListItem myListItem = new ListItem();
                        myListItem.Text = strAgentLevelName + " " + strAgentlevelMemo;
                        myListItem.Value = strD;

                        if (Model_tab_ShopClient_Agent != null)
                        {
                            if (Model_tab_ShopClient_Agent.AgentLevelSelect.ToString() == strD)
                            {
                                boolHaveSelected = true;
                                myListItem.Selected = true;
                            }
                        }


                        LevelRadioButtonList.Items.Add(myListItem);
                    }

                    ListItem myListItem1 = new ListItem();
                    myListItem1.Text = "分销代理";
                    myListItem1.Value = "9999999";
                    if (boolHaveSelected == false)
                    {
                        boolHaveSelected = true;
                        myListItem1.Selected = true;
                    }
                    LevelRadioButtonList.Items.Add(myListItem1);
                }
                else
                {
                    string strNav = "Agent_Manage.aspx" + Eggsoft.Common.Application.RequestUrlQuery();
                    Eggsoft.Common.JsUtil.LocationNewHref(strNav);
                }
                //string ID = Request.QueryString["ID"];// 修改ID
                //EggsoftWX.BLL.tab_ShopClient_Agent_Level bll = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                //EggsoftWX.Model.tab_ShopClient_Agent_Level Model = bll.GetModel(Int32.Parse(ID));

                //txtTitle.Text = Model.AgentLevelName;
                //txtMenuPos.Text = Model.Sort.ToString();
                //TextBox_AgentlevelMemo.Text = Model.AgentlevelMemo;
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (LevelRadioButtonList.SelectedValue == "9999999")//"普通分销代理";
                {
                    string strNav = "Agent_Manage.aspx" + Eggsoft.Common.Application.RequestUrlQuery();
                    Eggsoft.Common.JsUtil.LocationNewHref(strNav);
                }
                else
                {
                    string strNav = "Agent_Product_Manage.aspx" + Eggsoft.Common.Application.RequestUrlQuery() + "&AgentLevelSelect=" + LevelRadioButtonList.SelectedValue;
                    Eggsoft.Common.JsUtil.LocationNewHref(strNav);
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "07AgentChecked", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "07AgentChecked");
            }
        }
    }
}