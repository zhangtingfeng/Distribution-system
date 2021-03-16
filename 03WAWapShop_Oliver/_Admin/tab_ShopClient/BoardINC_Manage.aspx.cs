using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.Admin.tab_ShopClient
{
    public partial class BoardINC_Manage : Eggsoft.Common.DotAdminPage__Admin//System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_Admin_ShopClientPower tab_Admin_ShopClientPower_bll = new EggsoftWX.BLL.tab_Admin_ShopClientPower();


        private EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();

        private EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();

        public string strClientID = "";
        public string strDisPlay = "style=\"display:none;\"";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];

                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(ID));//删除文件

                    tab_ShopClient_bll.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", "UserManage.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {

                    SetClass(sender, e);
                }
            }
        }




        private void inttab_Admin_ShopClientPower(object sender, EventArgs e)
        {

            string strShopClientID = Request.QueryString["ID"];// 修改ID         


            CheckBox_o2o.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "o2oPower");

            CheckBox_WeiXinPayRedHongBao.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "WeiXinPayRedHongBao");
            CheckBox_GoodChildClass.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "GoodChildClass");
            CheckBox_AgentStatusPower.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "AgentStatusPower");


        }
        private void Save_ShopClientPower(object sender, EventArgs e)
        {

            string strShopClientID = Request.QueryString["ID"];// 修改ID         
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "o2oPower", CheckBox_o2o.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "WeiXinPayRedHongBao", CheckBox_WeiXinPayRedHongBao.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "GoodChildClass", CheckBox_GoodChildClass.Checked);
            Eggsoft_Public_CL.Pub.boolSaveShowPower(strShopClientID, "AgentStatusPower", CheckBox_AgentStatusPower.Checked);

        }

        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                strDisPlay = "";//这种情况是show的  关于是否合伙人关联的  关联才能有收入  才能收到消息 才能 找到收入
                strClientID = Request.QueryString["ID"];// 修改ID
                tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strClientID));//删除文件

                #region 验证 font color 尚未合伙人关联微信号
                int intPartnerWeiXinID = Convert.ToInt32(tab_ShopClient_Model.PartnerWeiXinID);
                inttab_Admin_ShopClientPower(sender, e);
                try
                {
                    HyperLink_LinkWeiXin.NavigateUrl = "/_Admin/System_WeiXin/RegisterOpenID_Partner.aspx?type=AskRalation_Partner&ClientID=" + strClientID;
                    HyperLink_Clear.NavigateUrl = "/_Admin/System_WeiXin/RegisterOpenID_Partner.aspx?type=ClearRalation&ClientID=" + strClientID;


                    if (intPartnerWeiXinID == 0)
                    {
                        Label_LinkWeiXin.Text = "尚未关联微信号，不能拥有合伙人收益";
                    }
                    else
                    {
                        String strUserid_nickname = Eggsoft_Public_CL.Pub.GetNickName(intPartnerWeiXinID.ToString());
                        Color yourColor = Color.FromName("#003300");
                        Label_LinkWeiXin.ForeColor = yourColor;

                        Label_LinkWeiXin.Text = "已关联的 账户:" + strUserid_nickname + " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intPartnerWeiXinID.ToString());


                    }

                }
                catch
                {//...

                }
                finally
                {
                    //...
                }
                #endregion 是否合伙人关联的 验证


                #region 验证 font color 尚未介绍人关联微信号
                int intRecommendWeiXinID = Convert.ToInt32(tab_ShopClient_Model.RecommendWeiXinID);

                try
                {
                    HyperLink_Recommand.NavigateUrl = "/_Admin/System_WeiXin/RegisterOpenID_Partner_Recommand.aspx?type=Ask_Recommand_Partner&ClientID=" + strClientID;
                    HyperLink_Clear_Recommand.NavigateUrl = "/_Admin/System_WeiXin/RegisterOpenID_Partner_Recommand.aspx?type=ClearRecommand&ClientID=" + strClientID;


                    if (intRecommendWeiXinID == 0)
                    {
                        Label_LinkWeiXin_Recommond.Text = "尚未关联微信号，不能拥有介绍人收益";
                    }
                    else
                    {
                        String strUserid_nickname = Eggsoft_Public_CL.Pub.GetNickName(intRecommendWeiXinID.ToString());
                        Color yourColor = Color.FromName("#003300");
                        Label_LinkWeiXin_Recommond.ForeColor = yourColor;

                        Label_LinkWeiXin_Recommond.Text = "已关联的 账户:" + strUserid_nickname + " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intRecommendWeiXinID.ToString());


                    }

                }
                catch
                {//...

                }
                finally
                {
                    //...
                }
                #endregion 是否合伙人关联的 验证

                tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strClientID));

                CheckBox_ShenMaShopping.Checked = Convert.ToBoolean(tab_ShopClient_Model.ShenMaShopping);

                TextboxUserName.Enabled = false;
                TextboxUserName.Text = tab_ShopClient_Model.Username;

                Label_ModifyTip.Visible = true;
                Label_ModifyTip0.Visible = true;
                TextboxUser_Authortime.Text = Convert.ToDateTime(tab_ShopClient_Model.AuthorTime).ToShortDateString();

                Textbox_Email.Text = tab_ShopClient_Model.Email;
                Textbox_RealName.Text = tab_ShopClient_Model.ContactMan;

                if (tab_ShopClient_Model.Sex == false)
                { RadioButtonList_Sex.SelectedValue = "0"; }
                else
                { RadioButtonList_Sex.SelectedValue = "1"; }
                Textbox_BeiZhu.Text = tab_ShopClient_Model.ShopMemo;



                TextboxINCPhone.Text = tab_ShopClient_Model.ContactPhone;
                TextboxAddress.Text = tab_ShopClient_Model.Address;
                RequiredFieldValidatorTextboxUserPassword.Enabled = false;//密码可为空
                btnAdd.Text = "保 存";

                //处理商家购物红包
                Textbox_Shopping_Vouchers_Money.Text = Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(tab_ShopClient_Model.Shopping_Vouchers_Money));
                //end 处理商家购物红包

                set_stringPowerList(tab_ShopClient_Model);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"];
            string strShopClientID = "0";
            string strShopClientUsername = "";
            string strErJiYuMing = "";
            if (type.ToLower() == "modify")
            {
                Save_ShopClientPower(sender, e);
                strShopClientID = Request.QueryString["ID"];// 修改ID
                tab_ShopClient_Model = tab_ShopClient_bll.GetModel(Int32.Parse(strShopClientID));
                tab_ShopClient_Model = saveModel(tab_ShopClient_Model);



                tab_ShopClient_Model.ShenMaShopping = CheckBox_ShenMaShopping.Checked;
                tab_ShopClient_Model.XML = get_stringPowerList(tab_ShopClient_Model.XML);

                strShopClientUsername = tab_ShopClient_Model.Username.Trim().ToLower();
                strErJiYuMing = Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strShopClientID), 6) + strShopClientUsername.Replace(" ", "") + ".eggsoft.cn";
                tab_ShopClient_Model.ErJiYuMing = strErJiYuMing;

                tab_ShopClient_bll.Update(tab_ShopClient_Model);
                //JsUtil.ShowMsg("修改成功!", "UserManage.aspx");
            }
            else
                if (type.ToLower() == "add")
            {
                EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();

                tab_ShopClient_Model.ID = tab_ShopClient_bll.GetMaxId();
                strShopClientID = tab_ShopClient_Model.ID.ToString();
                tab_ShopClient_Model = saveModel(tab_ShopClient_Model);
                tab_ShopClient_Model.XML = get_stringPowerList(tab_ShopClient_Model.XML);

                string UserName = tab_ShopClient_Model.Username;
                if (UserName.Length > 3) UserName = UserName.Substring(0, 2);
                tab_ShopClient_Model.UpLoadPath = "/upload/" + Eggsoft.Common.StringNum.Add000000Num(tab_ShopClient_Model.ID, 6) + "_" + UserName + "";


                tab_ShopClient_Model.ErJiYuMing = Eggsoft.Common.StringNum.Add000000Num(tab_ShopClient_Model.ID, 6) + UserName;



                tab_ShopClient_Model.ShenMaShopping = CheckBox_ShenMaShopping.Checked;
                if (Convert.ToBoolean(tab_ShopClient_Model.ShenMaShopping))
                {
                    tab_ShopClient_Model.Shopping_Vouchers = true;//直接启用它的购物券功能
                }
                strShopClientUsername = tab_ShopClient_Model.Username;
                strErJiYuMing = Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strShopClientID), 6) + strShopClientUsername.Replace(" ", "") + ".eggsoft.cn";
                tab_ShopClient_Model.ErJiYuMing = strErJiYuMing;



                tab_ShopClient_Model.Shopping_Vouchers_Goods_Percent = 10;///给予默认值  购物券的比例
                tab_ShopClient_bll.Add(tab_ShopClient_Model);

                #region 二级域名
                try
                {
                    //Eggsoft.Common.debug_Log.Call_WriteLog("strErJiYuMing=:" + strErJiYuMing);
                    Eggsoft.Common.Marker_ErJiYuMing_tab_DoTask_Services.insertATask(strErJiYuMing);
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("Marker_ErJiYuMing_tab_DoTask_Services:" + Exceptione.ToString());
                }
                finally
                {

                }
                #endregion
            }



            JsUtil.ShowMsg("保存成功!", "UserManage.aspx");
        }


        private EggsoftWX.Model.tab_ShopClient saveModel(EggsoftWX.Model.tab_ShopClient myModel)
        {

            //if (Radio2MemberType.SelectedValue == "0")
            //{ myModel.IFCompany = false; }
            //else
            //{ 
            myModel.IFCompany = true;
            //}

            myModel.Username = TextboxUserName.Text.Trim();
            myModel.Username = myModel.Username.Replace(" ", "");//不能有空格
            myModel.ShopClientName = myModel.Username;///人性化设置 起一个默认名字
            if (TextboxUserPassword.Text.Trim() != "")
            {
                myModel.PassWord = Eggsoft.Common.DESCrypt.GetMd5Str32(TextboxUserPassword.Text.Trim());
            }

            myModel.Email = Textbox_Email.Text;



            myModel.ContactMan = Textbox_RealName.Text;
            if (RadioButtonList_Sex.SelectedValue == "0")
            { myModel.Sex = false; }
            else
            { myModel.Sex = true; }
            myModel.ShopMemo = Textbox_BeiZhu.Text;

            myModel.ShopClientName = txtINCName.Text;


            string strUserClientClass = "1";

            strUserClientClass = Eggsoft_Public_CL.ClassP.RemoveLastDouHao(strUserClientClass);

            myModel.ShopClientClass = strUserClientClass;
            myModel.ContactPhone = TextboxINCPhone.Text;
            myModel.Address = TextboxAddress.Text;
            myModel.Updatetime = DateTime.Now;

            if (TextboxUser_Authortime.Text == "")
            {
                DateTime Authortime = DateTime.Now.AddYears(3);
                //Strin .ToString("yyyy-MM-dd") 
                TextboxUser_Authortime.Text = Authortime.ToString();
            }
            myModel.AuthorTime = Convert.ToDateTime(TextboxUser_Authortime.Text);

            //处理商家购物红包
            Decimal myShopping_Vouchers_Money = 0;
            Decimal.TryParse(Textbox_Shopping_Vouchers_Money.Text, out myShopping_Vouchers_Money);
            myModel.Shopping_Vouchers_Money = myShopping_Vouchers_Money;
            //end 处理商家购物红包



            return myModel;
        }




        protected void set_stringPowerList(EggsoftWX.Model.tab_ShopClient Model)
        {
            if (string.IsNullOrEmpty(Model.XML) == false)
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(Model.XML, System.Text.Encoding.UTF8);

                if (string.IsNullOrEmpty(myFahuoDan.stringPowerList) == false)
                {
                    String[] stringPowerList = myFahuoDan.stringPowerList.Split(',');

                    for (int i = 0; i < CheckBoxList_PowerList.Items.Count; i++)
                    {
                        if (stringPowerList.Length > i) CheckBoxList_PowerList.Items[i].Selected = stringPowerList[i] == "1";
                    }
                }
            }
        }

        protected string get_stringPowerList(string strModel_XML)
        {
            string stringPowerList = "";
            for (int i = 0; i < CheckBoxList_PowerList.Items.Count; i++)
            {
                string str0or1 = CheckBoxList_PowerList.Items[i].Selected ? "1" : "0";
                if (i == 0)
                {
                    stringPowerList = str0or1;
                }
                else
                {
                    stringPowerList += "," + str0or1;
                }
            }



            if (string.IsNullOrEmpty(strModel_XML) == false)
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strModel_XML, System.Text.Encoding.UTF8);
                myFahuoDan.stringPowerList = stringPowerList;
                strModel_XML = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);
            }
            else
            {
                Eggsoft_Public_CL.XML__Class_Shop_Client myFahuoDan = new Eggsoft_Public_CL.XML__Class_Shop_Client();
                myFahuoDan.stringPowerList = stringPowerList;
                strModel_XML = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);
            }
            return strModel_XML;
        }



        protected void CheckBox_o2o_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void CheckBox_WeiXinPayRedHongBao_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}