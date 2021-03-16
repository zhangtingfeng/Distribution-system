using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._07AgentChecked
{
    public partial class Agent_Level_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //ini_DropDownList_FenXiaoMoney();

                    string type = Request.QueryString["type"];
                    string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


                    if (type.ToLower() == "delete")
                    {
                        string strID = Request.QueryString["ID"];
                        if (!CommUtil.IsNumStr(strID))
                            MyError.ThrowException("传递参数错误!");
                        EggsoftWX.BLL.tab_ShopClient_Agent_Level blltab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                        EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();

                        blltab_ShopClient_Agent_Level.Delete(Int32.Parse(strID));
                        BLL_tab_ShopClient_Agent_Level_ProductInfo.Delete("ShopClient_Agent_Level_ID=" + strID + " and ShopClientID=" + strShopClient_ID);
                        JsUtil.ShowMsg("删除成功!", "Board_Agent_Level.aspx");
                    }
                    else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                    {
                        SetClass();
                    }
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

        private void read_ShopClient_Agent__ProductClassID_ForPrice()
        {
            try
            {
                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo = null;

                string type = Request.QueryString["type"];
                string strShopClient_Agent_Level_ID = Request.QueryString["ID"];// 修改ID

                #region 分销方案选择
                EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel BLL_b019_tab_ShopClient_MultiFenXiaoLevel = new EggsoftWX.BLL.b019_tab_ShopClient_MultiFenXiaoLevel();

                web.MultiOrderPagerSQL sql = new web.MultiOrderPagerSQL();
                sql.addOrderField("b019_tab_ShopClient_MultiFenXiaoLevel.sort", "asc");//第一排序字段  
                sql.addOrderField("b019_tab_ShopClient_MultiFenXiaoLevel.id", "asc");//第二排序字段  
                sql.table = "b019_tab_ShopClient_MultiFenXiaoLevel";
                sql.outfields = "[ID]      ,[ShopClient_ID]      ,[Name]      ,[Sort]      ,[UpdateTime] ";
                sql.nowPageIndex = 1;
                sql.pagesize = 10000;
                string strwhere = "ShopClient_ID=" + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString() + " and IsDeleted=0";
                sql.where = strwhere;
                string strSql = sql.getSQL(BLL_b019_tab_ShopClient_MultiFenXiaoLevel.ExistsCount(strwhere));
                System.Data.DataTable Data_DataTableMultiOrderPagerSQL = BLL_b019_tab_ShopClient_MultiFenXiaoLevel.SelectList(strSql).Tables[0];
                #endregion 分销方案选择


                System.Data.DataTable myDataTable2 = BLL_tab_Goods.GetList("ShopClient_ID=" + strShopClient_ID + " and IsDeleted=0 and issaled=1 order by sort asc,id asc").Tables[0];
                string multi_Price_Line = "";

                for (int i = 0; i < myDataTable2.Rows.Count; i++)
                {
                    string strProductID = myDataTable2.Rows[i]["ID"].ToString();
                    string strName = myDataTable2.Rows[i]["Name"].ToString();
                    string strPromotePrice = myDataTable2.Rows[i]["PromotePrice"].ToString();
                    string strGoodAgentPercent = myDataTable2.Rows[i]["AgentPercent"].ToString();

                    string strProductPrice = strPromotePrice;////给默认最大值
                    string strAgentPercent = strGoodAgentPercent;///代理商默认不给钱 叫他们自己去设置吧
                    string strMaxGouWuquanPrice = "0.00";
                    string strMaxMaxWealthPrice = "0.00";
                    Int32 Int32b019_tab_ShopClient_MultiFenXiaoLevelID = 0;

                    if (type.ToLower() == "modify")
                    {
                        Model_tab_ShopClient_Agent_Level_ProductInfo = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("ShopClient_Agent_Level_ID=" + strShopClient_Agent_Level_ID + " and ProductID=" + strProductID + " and ShopClientID=" + strShopClient_ID);
                        if (Model_tab_ShopClient_Agent_Level_ProductInfo != null)
                        {
                            strProductPrice = Model_tab_ShopClient_Agent_Level_ProductInfo.ProductPrice.ToString();
                            strAgentPercent = Model_tab_ShopClient_Agent_Level_ProductInfo.AgentPercent.ToString();
                            strMaxGouWuquanPrice = Model_tab_ShopClient_Agent_Level_ProductInfo.MaxGouWuQuan.ToString();
                            strMaxMaxWealthPrice = Model_tab_ShopClient_Agent_Level_ProductInfo.MaxWealth.ToString();
                            Int32b019_tab_ShopClient_MultiFenXiaoLevelID = Model_tab_ShopClient_Agent_Level_ProductInfo.b019_tab_ShopClient_MultiFenXiaoLevelID.toInt32();
                        }
                    }

                    multi_Price_Line += "<tr><td>商品名称：" + Eggsoft.Common.StringNum.MaxLengthString(strName, 8) + "</td>";
                    multi_Price_Line += "<td>打折价格：" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strPromotePrice)) + "元</td>";
                    multi_Price_Line += "<td>代理价格<input style=\"width:50px\" name=\"Text_Price_Agent" + strProductID + "\" value=\"" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strProductPrice)) + "\" id=\"Text_Price_Agent" + strProductID + "\" type=\"text\">￥</td>";
                    multi_Price_Line += "<td>分销费用<input style=\"width:50px\" name=\"Text_AgentPercent" + strProductID + "\" value=\"" + Eggsoft_Public_CL.Pub.getPubMoney((strAgentPercent.toDecimal())) + "\" id=\"Text_Price_Agent" + strProductID + "\" type=\"text\">￥</td>";
                    multi_Price_Line += "<td>最大购物券金额<input style=\"width:50px\" name=\"Text_Price_Agent_MaxGouWuQuan" + strProductID + "\" value=\"" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strMaxGouWuquanPrice)) + "\" id=\"Text_Price_Agent_MaxGouWuQuan" + strProductID + "\" type=\"text\">￥</td>";
                    multi_Price_Line += "<td>最大财富积分金额<input style=\"width:50px\" name=\"Text_Price_Agent_MaxWealth" + strProductID + "\" value=\"" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal.Parse(strMaxMaxWealthPrice)) + "\" id=\"Text_Price_Agent_MaxWealth" + strProductID + "\" type=\"text\">￥</td>";
                    multi_Price_Line += @"<td>分销方案选择 
                    <select NAME='Text_MultiFenXiaoLevel_Agent" + strProductID + "'>";

                    for (int k = 0; k < Data_DataTableMultiOrderPagerSQL.Rows.Count; k++)
                    {
                        string strCurName = Data_DataTableMultiOrderPagerSQL.Rows[k]["Name"].toString();
                        string strb019_tab_ShopClient_MultiFenXiaoLevelID = Data_DataTableMultiOrderPagerSQL.Rows[k]["ID"].toString();
                        multi_Price_Line += "<option value='" + strb019_tab_ShopClient_MultiFenXiaoLevelID + "'";
                        if (Int32b019_tab_ShopClient_MultiFenXiaoLevelID == strb019_tab_ShopClient_MultiFenXiaoLevelID.toInt32())
                        {
                            multi_Price_Line += " selected='selected'";
                        }
                        multi_Price_Line += ">" + strCurName + "</option>";
                    }
                    multi_Price_Line += "</select></td>";
                    multi_Price_Line += "</tr>\n";
                }


                Literal_Agent_Percent_Line.Text = multi_Price_Line;

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "高级代理设置", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "高级代理设置");
            }

        }


        private void SetClass()
        {
            try
            {
                read_ShopClient_Agent__ProductClassID_ForPrice();
                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    string ID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level bll = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level Model = bll.GetModel(Int32.Parse(ID));

                    CheckBox_AgentOperationGetChild.Checked = Model.OperationGetChild.toBoolean();
                    CheckBox_AgentOperationGetGrandChild.Checked = Model.OperationGetGrandChild.toBoolean();

                    txtTitle.Text = Model.AgentLevelName;
                    txtMenuPos.Text = Model.Sort.ToString();
                    TextBox_AgentlevelMemo.Text = Model.AgentlevelMemo;
                    TextBox_Vouchers_Consume_Or_Recharge.Text = Eggsoft_Public_CL.Pub.getPubMoney(Model.GouWuQuanGoodPrice.toDecimal());
                    btnAdd.Text = "保 存";
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "高级代理设置", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "高级代理设置");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //ControlValidate.ImageValidate(Image_Cover, "封面图片", "NullVal||", "", "");
            //string strAddinInfoList = CheckBox_Announce.Checked.ToString() + "," + CheckBox_ContactUs.Checked.ToString();
            try
            {

                string ID = Request.QueryString["ID"];// 修改ID
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

                string type = Request.QueryString["type"];
                if (type.ToLower() == "modify")
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level bll = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level Model = bll.GetModel(Int32.Parse(ID));

                    Model.OperationGetChild = CheckBox_AgentOperationGetChild.Checked;
                    Model.OperationGetGrandChild = CheckBox_AgentOperationGetGrandChild.Checked;
                    //Model.OperationGetGreatChild = CheckBox_AgentOperationGetGreatChild.Checked;
                    Model.Sort = Convert.ToInt32(txtMenuPos.Text);
                    Model.AgentLevelName = txtTitle.Text.Trim();
                    Model.UpdateTime = DateTime.Now;
                    Model.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model.AgentlevelMemo = TextBox_AgentlevelMemo.Text.Trim();
                    Model.GouWuQuanGoodPrice = Decimal.Parse(TextBox_Vouchers_Consume_Or_Recharge.Text);

                    saveMultiProductAgentPrice(Int32.Parse(ID));///级别代理的产品价格

                    bll.Update(Model);
                    JsUtil.ShowMsg("修改成功!", "Board_Agent_Level.aspx");

                }
                else
                    if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level bll = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level Announce = new EggsoftWX.Model.tab_ShopClient_Agent_Level();
                    Announce.ShopClientID = Int32.Parse(Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
                    Announce.AgentLevelName = txtTitle.Text.Trim();
                    Announce.Sort = Convert.ToInt32(txtMenuPos.Text);
                    Announce.AgentlevelMemo = TextBox_AgentlevelMemo.Text.Trim();
                    Announce.GouWuQuanGoodPrice = Decimal.Parse(TextBox_Vouchers_Consume_Or_Recharge.Text);
                    Announce.OperationGetChild = CheckBox_AgentOperationGetChild.Checked;
                    Announce.OperationGetGrandChild = CheckBox_AgentOperationGetGrandChild.Checked;
                    //Announce.OperationGetGreatChild = CheckBox_AgentOperationGetGreatChild.Checked;
                    Announce.UpdateTime = DateTime.Now;
                    Announce.CreatBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;

                    int intAdd = bll.Add(Announce);
                    saveMultiProductAgentPrice(intAdd);///级别代理的产品价格

                    JsUtil.ShowMsg("添加成功!", "Board_Agent_Level.aspx");

                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "高级代理设置", "线程异常");
            }
            catch (Exception Exceptione)
            {
                debug_Log.Call_WriteLog(Exceptione, "高级代理设置");
            }

            finally
            {

            }


        }


        private void saveMultiProductAgentPrice(int intShopClient_Agent_Level_ID)///级别代理的产品价格
        {
            try
            {
                string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                string strShopClient_ID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                System.Data.DataTable myDataTable = BLL_tab_Goods.GetList("ShopClient_ID=" + strShopClient_ID + " and isSaled=1 and IsDeleted=0 order by sort asc,id asc").Tables[0];
                EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo = null;

                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strProductID = myDataTable.Rows[i]["ID"].ToString();
                    string strProductPrice = Request.Form["Text_Price_Agent" + strProductID];//代理商价格
                    string str1Text_Price_Agent = Request.Form["Text_AgentPercent" + strProductID];//分销费用
                    string strText_Price_Agent_MaxGouWuQuan = Request.Form["Text_Price_Agent_MaxGouWuQuan" + strProductID];
                    string strText_Price_Agent_MaxWealth = Request.Form["Text_Price_Agent_MaxWealth" + strProductID];
                    Int32 int32Text_MultiFenXiaoLevel_Agent = Request.Form["Text_MultiFenXiaoLevel_Agent" + strProductID].toInt32();
                    Decimal Decimal_Price_ = 0;
                    Decimal.TryParse(strProductPrice, out Decimal_Price_);


                    if (Decimal_Price_ <= 0 || strText_Price_Agent_MaxGouWuQuan.toDecimal() < 0)
                    {
                        JsUtil.ShowMsg("第" + (i + 1).toString() + "行保存失败，本系统暂时不支持代理价格为0的产品,也不支持负值!", -1);
                        return;
                    }
                    if (Decimal_Price_ <= 0 || strText_Price_Agent_MaxWealth.toDecimal() < 0)
                    {
                        JsUtil.ShowMsg("第" + (i + 1).toString() + "行保存失败，本系统暂时不支持代理价格为0的产品,也不支持负值!", -1);
                        return;
                    }
                    if (Decimal_Price_ < strText_Price_Agent_MaxGouWuQuan.toDecimal())
                    {
                        JsUtil.ShowMsg("第" + (i + 1).toString() + "行保存失败，最大购物券不能超过代理价格!", -1);
                        return;
                    }
                    if (Decimal_Price_ < strText_Price_Agent_MaxWealth.toDecimal())
                    {
                        JsUtil.ShowMsg("第" + (i + 1).toString() + "行保存失败，最大财富积分不能超过代理价格!", -1);
                        return;
                    }
                    if (Decimal_Price_ < (str1Text_Price_Agent.toDecimal() + strText_Price_Agent_MaxWealth.toDecimal() + strText_Price_Agent_MaxGouWuQuan.toDecimal()))
                    {
                        JsUtil.ShowMsg("第" + (i + 1).toString() + "行保存失败，分销费用与最大财富积分与最大购物券之和不能超过代理价格!", -1);
                        return;
                    }


                    bool boolE = BLL_tab_ShopClient_Agent_Level_ProductInfo.Exists("ShopClient_Agent_Level_ID=" + intShopClient_Agent_Level_ID + " and ProductID=" + strProductID);
                    if (boolE)
                    {
                        Model_tab_ShopClient_Agent_Level_ProductInfo = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("ShopClient_Agent_Level_ID=" + intShopClient_Agent_Level_ID + " and ProductID=" + strProductID);
                        Model_tab_ShopClient_Agent_Level_ProductInfo.ProductPrice = Decimal_Price_;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.AgentPercent = str1Text_Price_Agent.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.UpdateTime = DateTime.Now;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.Updateby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.MaxGouWuQuan = strText_Price_Agent_MaxGouWuQuan.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.MaxWealth = strText_Price_Agent_MaxWealth.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.b019_tab_ShopClient_MultiFenXiaoLevelID = int32Text_MultiFenXiaoLevel_Agent;
                        BLL_tab_ShopClient_Agent_Level_ProductInfo.Update(Model_tab_ShopClient_Agent_Level_ProductInfo);
                    }
                    else
                    {
                        Model_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.ShopClientID = strShopClient_ID.toInt32();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.ProductPrice = Decimal_Price_;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.AgentPercent = str1Text_Price_Agent.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.MaxGouWuQuan = strText_Price_Agent_MaxGouWuQuan.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.MaxWealth = strText_Price_Agent_MaxWealth.toDecimal();
                        Model_tab_ShopClient_Agent_Level_ProductInfo.ShopClient_Agent_Level_ID = intShopClient_Agent_Level_ID;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.ProductID = Int32.Parse(strProductID);
                        Model_tab_ShopClient_Agent_Level_ProductInfo.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.Updateby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_tab_ShopClient_Agent_Level_ProductInfo.b019_tab_ShopClient_MultiFenXiaoLevelID = int32Text_MultiFenXiaoLevel_Agent;
                        BLL_tab_ShopClient_Agent_Level_ProductInfo.Add(Model_tab_ShopClient_Agent_Level_ProductInfo);
                    }
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "高级代理设置", "线程异常");
            }
            catch (Exception Exceptiondddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptiondddd, "高级代理设置");
            }
        }
    }
}