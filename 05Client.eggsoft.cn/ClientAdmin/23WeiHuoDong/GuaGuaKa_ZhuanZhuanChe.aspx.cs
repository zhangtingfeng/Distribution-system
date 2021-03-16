using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong
{
    public partial class GuaGuaKa_ZhuanZhuanChe : Eggsoft.Common.DotAdminPage_ClientAdmin
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_ZhuanZhuanChe")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限
            if (!IsPostBack)
            {
                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
                string strErJiYuMing = Model.ErJiYuMing;///默认一个数值


                string str_Pub_upLoadpath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(str_Pub_ShopClientID)) + "/QRCodeImage/";

                string strLabel_GuaGuaKa = "https://" + strErJiYuMing + "/huodong/lottery/scratchcard" + ".aspx";
                string strImageUrl_GuaGuaKa = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLabel_GuaGuaKa, str_Pub_upLoadpath, "");


                string strLabel_wheelURL = "https://" + strErJiYuMing + "/huodong/lottery/wheel" + ".aspx";
                string strImageUrl_ZhuanZhuanChe = Eggsoft_Public_CL.Pub.Get_Remote_creatQRCodeImage(strLabel_wheelURL, str_Pub_upLoadpath, "");

                Image1_GuaGuaKa.ImageUrl = strImageUrl_GuaGuaKa;
                HyperLink_GuaGuaKa.NavigateUrl = strLabel_GuaGuaKa;
                Image2_ZhuanZhuanChe.ImageUrl = strImageUrl_ZhuanZhuanChe;
                HyperLink_ZhuanZhuanChe.NavigateUrl = strLabel_wheelURL;
                SetClass();
            }
        }

        private void SetClass()
        {
            string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            EggsoftWX.BLL.tab_ShopClient bll = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model = bll.GetModel(Int32.Parse(str_Pub_ShopClientID));
            string strErJiYuMing = Model.ErJiYuMing;///默认一个数值

            EggsoftWX.BLL.tab_ShopClient_HuoDong blltab_ShopClient_HuoDong = new EggsoftWX.BLL.tab_ShopClient_HuoDong();
            EggsoftWX.Model.tab_ShopClient_HuoDong Modeltab_ShopClient_HuoDong = blltab_ShopClient_HuoDong.GetModel("ShopClientID=" + str_Pub_ShopClientID);
            if (Modeltab_ShopClient_HuoDong != null)
            {
                string strXML = Modeltab_ShopClient_HuoDong.XML;
                if (string.IsNullOrEmpty(strXML) == false)
                {
                    Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe>(strXML, System.Text.Encoding.UTF8);

                    txtTypeAllCount_0.Text = myFahuoDan.intValue0.ToString();
                    txtTypeAllCount_1.Text = myFahuoDan.intValue1.ToString();
                    txtTypeAllCount_2.Text = myFahuoDan.intValue2.ToString();
                    txtTypeAllCount_3.Text = myFahuoDan.intValue3.ToString();
                    txtTypeAllCount_4.Text = myFahuoDan.intValue4.ToString();
                    TextBox_CanSecondTime.Text = myFahuoDan.intSecondGetBonusTime.ToString();

                    txtTypeTitle_0.Text = myFahuoDan.stringValue0;
                    txtTypeTitle_1.Text = myFahuoDan.stringValue1;
                    txtTypeTitle_2.Text = myFahuoDan.stringValue2;
                    txtTypeTitle_3.Text = myFahuoDan.stringValue3;
                    txtTypeTitle_4.Text = myFahuoDan.stringValue4;

                    txtTypeAllCount.Text = myFahuoDan.intALLCountValue.ToString();

                    myFahuoDan.stringValue0 = txtTypeTitle_0.Text.Trim();
                    myFahuoDan.stringValue1 = txtTypeTitle_1.Text.Trim();
                    myFahuoDan.stringValue2 = txtTypeTitle_2.Text.Trim();
                    myFahuoDan.stringValue3 = txtTypeTitle_3.Text.Trim();
                    myFahuoDan.stringValue4 = txtTypeTitle_4.Text.Trim();

                    CheckBoxRun.Checked = myFahuoDan.boolEnable;
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {





            string strVale0 = txtTypeAllCount_0.Text;
            string strVale1 = txtTypeAllCount_1.Text;
            string strVale2 = txtTypeAllCount_2.Text;
            string strVale3 = txtTypeAllCount_3.Text;
            string strVale4 = txtTypeAllCount_4.Text;
            string strTypeAllCount = txtTypeAllCount.Text;
            string strTextBox_CanSecondTime = TextBox_CanSecondTime.Text;

            //  .Text = myFahuoDan.intSecondGetBonusTime.ToString();

            int intValue0 = Int32.Parse(strVale0);
            int intValue1 = Int32.Parse(strVale1);
            int intValue2 = Int32.Parse(strVale2);
            int intValue3 = Int32.Parse(strVale3);
            int intValue4 = Int32.Parse(strVale4);

            int intTypeAllCount = Int32.Parse(strTypeAllCount);
            int intCanSecondTime = Int32.Parse(strTextBox_CanSecondTime);

            int intCountAll = (intValue0 + intValue1 + intValue2 + intValue3 + intValue4);
            if (intCountAll > intTypeAllCount)
            {
                Eggsoft.Common.JsUtil.ShowMsg("奖品总数" + intCountAll.ToString() + "大于抽奖总数" + intTypeAllCount.ToString() + "？", "javascript:history.back()");
                return;
                //    JsUtil.ShowMsg("旧密码错误!", "javascript:history.back()");

            }
            else
            {
                Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe myFahuoDan = new Eggsoft_Public_CL.XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe();
                myFahuoDan.intValue0 = intValue0;
                myFahuoDan.intValue1 = intValue1;
                myFahuoDan.intValue2 = intValue2;
                myFahuoDan.intValue3 = intValue3;
                myFahuoDan.intValue4 = intValue4;
                myFahuoDan.intALLCountValue = intTypeAllCount;
                myFahuoDan.intSecondGetBonusTime = intCanSecondTime;

                myFahuoDan.stringValue0 = txtTypeTitle_0.Text.Trim();
                myFahuoDan.stringValue1 = txtTypeTitle_1.Text.Trim();
                myFahuoDan.stringValue2 = txtTypeTitle_2.Text.Trim();
                myFahuoDan.stringValue3 = txtTypeTitle_3.Text.Trim();
                myFahuoDan.stringValue4 = txtTypeTitle_4.Text.Trim();

                myFahuoDan.boolEnable = CheckBoxRun.Checked;
                string strXML = Eggsoft.Common.XmlHelper.XmlSerialize(myFahuoDan, System.Text.Encoding.UTF8);

                string str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                EggsoftWX.BLL.tab_ShopClient_HuoDong blltab_ShopClient_HuoDong = new EggsoftWX.BLL.tab_ShopClient_HuoDong();
                EggsoftWX.Model.tab_ShopClient_HuoDong Modeltab_ShopClient_HuoDong = blltab_ShopClient_HuoDong.GetModel("ShopClientID=" + str_Pub_ShopClientID);

                if (Modeltab_ShopClient_HuoDong != null)
                {
                    Modeltab_ShopClient_HuoDong.XML = strXML;
                    blltab_ShopClient_HuoDong.Update(Modeltab_ShopClient_HuoDong);
                }
                else
                {
                    Modeltab_ShopClient_HuoDong = new EggsoftWX.Model.tab_ShopClient_HuoDong();
                    Modeltab_ShopClient_HuoDong.ShopClientID = Int32.Parse(str_Pub_ShopClientID);
                    Modeltab_ShopClient_HuoDong.XML = strXML;
                    Modeltab_ShopClient_HuoDong.Type = "guaguaka";
                    blltab_ShopClient_HuoDong.Add(Modeltab_ShopClient_HuoDong);
                }


                JsUtil.ShowMsg("保存成功!", "javascript:history.back()");

            }






        }
    }
}