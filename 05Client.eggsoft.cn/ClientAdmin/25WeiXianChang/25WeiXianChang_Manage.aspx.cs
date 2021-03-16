using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._25WeiXianChang
{
    public partial class _25WeiXianChang_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string strXianChangHuoDongID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(strXianChangHuoDongID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong blltab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();

                    #region 检查删除条件
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus blltab_ShopClient_XianChangHuoDong_Bonus = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus();
                    bool booExsit = blltab_ShopClient_XianChangHuoDong_Bonus.Exists("ISDoing=0 and XianChangHuoDongID=" + strXianChangHuoDongID);
                    if (booExsit)
                    {
                        JsUtil.ShowMsg("删除失败，存在抽奖有效数据!", "25WeiXianChang_BoardSet.aspx");
                        return;
                    }



                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number blltab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                    booExsit = blltab_ShopClient_XianChangHuoDong_Number.Exists("ISDoing=0 and XianChangHuoDongID=" + strXianChangHuoDongID);
                    if (booExsit)
                    {
                        JsUtil.ShowMsg("删除失败，存在摇一摇有效数据!", "25WeiXianChang_BoardSet.aspx");
                        return;
                    }

                    #endregion


                    #region 删除 子数据

                    ///1
                    System.Data.DataTable DataTableBonus = blltab_ShopClient_XianChangHuoDong_Bonus.GetList("XianChangHuoDongID=" + strXianChangHuoDongID).Tables[0];
                    for (int i = 0; i < DataTableBonus.Rows.Count; i++)
                    {
                        string strBonusID = DataTableBonus.Rows[i]["ID"].ToString();

                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus bll_HuoDong_Bonus = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus();
                        EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Bonus Model_HuoDong_Bonus = bll_HuoDong_Bonus.GetModel(Int32.Parse(strBonusID));

                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User bll_Bonus_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Bonus_User();
                        bll_Bonus_User.Delete("XianChangHuoDongBonusNumberbyShopClientID=" + Model_HuoDong_Bonus.XianChangHuoDongBonusNumberbyShopClientID + " and ShopClientID=" + Model_HuoDong_Bonus.ShopClientID);

                        bll_HuoDong_Bonus.Delete(Int32.Parse(strBonusID));

                    }


                    ///2
                    System.Data.DataTable DataTableNumber = blltab_ShopClient_XianChangHuoDong_Number.GetList("XianChangHuoDongID=" + strXianChangHuoDongID).Tables[0];
                    for (int i = 0; i < DataTableNumber.Rows.Count; i++)
                    {
                        string strNumberID = DataTableNumber.Rows[i]["ID"].ToString();

                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number bll_HuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
                        EggsoftWX.Model.tab_ShopClient_XianChangHuoDong_Number Model_HuoDong_Number = bll_HuoDong_Number.GetModel(Int32.Parse(strNumberID));

                        EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum bll_UserShakeNum_User = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number_UserShakeNum();
                        bll_UserShakeNum_User.Delete("XianChangHuoDongNumberbyShopClientID=" + Model_HuoDong_Number.XianChangHuoDongNumberbyShopClientID + " and UserShopClientID=" + Model_HuoDong_Number.ShopClientID);

                        bll_HuoDong_Number.Delete(Int32.Parse(strNumberID));

                    }

                    #endregion

                    blltab_ShopClient_XianChangHuoDong.Delete(Int32.Parse(strXianChangHuoDongID));
                    JsUtil.ShowMsg("删除成功!", "25WeiXianChang_BoardSet.aspx");
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass();
                }
            }
        }

        private void SetClass()
        {
            #region 显示所有代理的二维码
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.View_ShopClient_Agent bll_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
            System.Data.DataTable myDataTable2 = bll_View_ShopClient_Agent.GetList("ShopClientID=" + strShopClientID + " order by ShopUserID asc").Tables[0];

            ListItem myThisListItem = new ListItem("微信公众号二维码", "0");
            DropDownList_ShowAgentErWeiMa_UserID_ByAgent.Items.Add(myThisListItem);

            for (int i = 0; i < myDataTable2.Rows.Count; i++)
            {
                string strAgentID = myDataTable2.Rows[i]["userID"].ToString();
                string strShopUserID = myDataTable2.Rows[i]["ShopUserID"].ToString();
                string strShopName = myDataTable2.Rows[i]["ShopName"].ToString();
                string strUserRealName = myDataTable2.Rows[i]["UserRealName"].ToString();
                string strContactPhone = myDataTable2.Rows[i]["ContactPhone"].ToString();
                string strNickName = myDataTable2.Rows[i]["NickName"].ToString();


                myThisListItem = new ListItem("代理ID：" + strShopUserID + " " + strNickName + " " + strUserRealName + " " + strShopName + " " + strContactPhone, strAgentID);
                DropDownList_ShowAgentErWeiMa_UserID_ByAgent.Items.Add(myThisListItem);
            }
            DropDownList_ShowAgentErWeiMa_UserID_ByAgent.SelectedValue = "0";
            #endregion

            //string strUpLoadURL = ConfigurationManager.AppSettings["UpLoadURL"];

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong bll = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Modeltab_ShopClient_XianChangHuoDong = bll.GetModel(Int32.Parse(ID));

                DropDownList_ShowAgentErWeiMa_UserID_ByAgent.SelectedValue = Modeltab_ShopClient_XianChangHuoDong.ShowAgentErWeiMa_UserID_ByAgent.ToString();

                txtActivityName.Text = Modeltab_ShopClient_XianChangHuoDong.ActivityName;
                TextboxPassword.Text = Modeltab_ShopClient_XianChangHuoDong.PassWord;
                CheckBox_ActivityState.Checked = Convert.ToBoolean(Modeltab_ShopClient_XianChangHuoDong.ActivityState);
                CheckBox_Subscribe_Must.Checked = Convert.ToBoolean(Modeltab_ShopClient_XianChangHuoDong.Subscribe_Must);
                CheckBox_GetBonusRepeat.Checked = Convert.ToBoolean(Modeltab_ShopClient_XianChangHuoDong.GetBonusRepeat);
                CheckBox_Address_Must.Checked = Convert.ToBoolean(Modeltab_ShopClient_XianChangHuoDong.Address_Must);
                txtClassPos.Text = Modeltab_ShopClient_XianChangHuoDong.Sort.ToString();

                Image_Background_PIC_BigScreen.ImageUrl = ConfigurationManager.AppSettings["UpLoadResourceURL"] + Modeltab_ShopClient_XianChangHuoDong.Background_PIC_BigScreen;
                if (String.IsNullOrEmpty(Modeltab_ShopClient_XianChangHuoDong.Background_SoundPath) == false)
                {
                    HyperLinkBackground_SoundPath.Text = ConfigurationManager.AppSettings["UpLoadResourceURL"] + Modeltab_ShopClient_XianChangHuoDong.Background_SoundPath;
                    HyperLinkBackground_SoundPath.NavigateUrl = HyperLinkBackground_SoundPath.Text;
                }
                TextBox_MAXLongShakeTime.Text = Modeltab_ShopClient_XianChangHuoDong.LongShakeTime.ToString();
                TextBox_CountHowMany.Text = Modeltab_ShopClient_XianChangHuoDong.CountHowMany.ToString();
                TextBox_MaxTracks.Text = Modeltab_ShopClient_XianChangHuoDong.MaxTracks.ToString();
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                //string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region 存盘

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

                int intpos = 0;
                int.TryParse(txtClassPos.Text, out intpos);

                string type = Request.QueryString["type"];

                if (!(Int32.Parse(TextBox_MaxTracks.Text) > 2 && Int32.Parse(TextBox_MaxTracks.Text) < 21))
                {
                    Eggsoft.Common.JsUtil.ShowMsg("大屏幕显示轨道数应该不大于20，不小于3", -1);
                }

                string upLoadpathimages = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/images/";
                String strFileUpload_PIC_BigScreen = "";
                if (FileUpload_Background_PIC_BigScreen.HasFile == true)
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".jpg";
                    strFileUpload_PIC_BigScreen = upLoadpathimages + saveName;
                    SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                    string eMsg = "";
                    string strRemoveUpload = upLoadpathimages.Substring("/upload/".Length, upLoadpathimages.Length - 1 - "/upload/".Length);
                    upl.UploadFile(FileUpload_Background_PIC_BigScreen.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                    System.Threading.Thread.Sleep(1000);//asp.net怎么可以让等待1秒再执行下面的程式
                }

                String strFileUpload_Logo_Mp3 = "";
                string upLoadpathSound = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(Convert.ToInt32(strShopClientID)) + "/MP3/";
                if (FileUpload_Background_SoundPath.HasFile == true)
                {
                    //其中File和Img是控件的ID号
                    string fullFileName = this.FileUpload_Background_SoundPath.FileName;//获取文件名，其中包含了盘符
                    string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);//取出盘符，获取文件名
                    string SoundPathtype = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1);//获取文件类型
                    if (SoundPathtype == "mp3")
                    {
                        string saveName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ((int)((new Random()).NextDouble() * 1000)).ToString() + ".mp3";
                        strFileUpload_Logo_Mp3 = upLoadpathSound + saveName;
                        SR_Upload.WebService_UploadSoapClient upl = new SR_Upload.WebService_UploadSoapClient("WebService_UploadSoap");
                        string eMsg = "";
                        string strRemoveUpload = upLoadpathSound.Substring("/upload/".Length, upLoadpathSound.Length - 1 - "/upload/".Length);
                        upl.UploadFile(FileUpload_Background_SoundPath.FileBytes, saveName, strRemoveUpload, ref eMsg, "");
                        System.Threading.Thread.Sleep(10000);//asp.net怎么可以让等待1秒再执行下面的程式
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("你的音乐格式不正确，必须是Mp3格", -1);
                    }
                }
                ///同步 音乐和 大屏幕的 图像
                Eggsoft_Public_CL.Upload.doUploadToQiNiu_Task(Convert.ToInt32(strShopClientID));

                if (type.ToLower() == "modify")
                {
                    string strID = Request.QueryString["ID"];// 修改ID
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong blltab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Modeltab_ShopClient_XianChangHuoDong = blltab_ShopClient_XianChangHuoDong.GetModel(Int32.Parse(strID));

                    Modeltab_ShopClient_XianChangHuoDong.ShowAgentErWeiMa_UserID_ByAgent = Int32.Parse(DropDownList_ShowAgentErWeiMa_UserID_ByAgent.SelectedValue);

                    Modeltab_ShopClient_XianChangHuoDong.ActivityName = Eggsoft.Common.CommUtil.SafeFilter(txtActivityName.Text);
                    Modeltab_ShopClient_XianChangHuoDong.PassWord = Eggsoft.Common.CommUtil.SafeFilter(TextboxPassword.Text);
                    #region 只有一个现场活动可以是开启状态
                    if (CheckBox_ActivityState.Checked)
                    {
                        bool boolOtherTrue = blltab_ShopClient_XianChangHuoDong.Exists("ShopClientID=" + strShopClientID + " and ActivityState=1 and id<>" + strID);
                        if (boolOtherTrue)
                        {
                            CheckBox_ActivityState.Checked = false;
                            Eggsoft.Common.JsUtil.ShowMsg("只有一个现场活动可以是开启状态", -1);
                        }
                    }
                    #endregion 只有一个现场活动可以是开启状态

                    Modeltab_ShopClient_XianChangHuoDong.ActivityState = CheckBox_ActivityState.Checked;
                    Modeltab_ShopClient_XianChangHuoDong.Subscribe_Must = CheckBox_Subscribe_Must.Checked;
                    Modeltab_ShopClient_XianChangHuoDong.GetBonusRepeat = CheckBox_GetBonusRepeat.Checked;
                    Modeltab_ShopClient_XianChangHuoDong.Address_Must = CheckBox_Address_Must.Checked;
                    Modeltab_ShopClient_XianChangHuoDong.Sort = intpos;
                    if (String.IsNullOrEmpty(strFileUpload_PIC_BigScreen) == false) Modeltab_ShopClient_XianChangHuoDong.Background_PIC_BigScreen = strFileUpload_PIC_BigScreen;
                    if (String.IsNullOrEmpty(strFileUpload_Logo_Mp3) == false) Modeltab_ShopClient_XianChangHuoDong.Background_SoundPath = strFileUpload_Logo_Mp3;

                    Modeltab_ShopClient_XianChangHuoDong.LongShakeTime = Int32.Parse(TextBox_MAXLongShakeTime.Text);
                    Modeltab_ShopClient_XianChangHuoDong.CountHowMany = Int32.Parse(TextBox_CountHowMany.Text);
                    Modeltab_ShopClient_XianChangHuoDong.MaxTracks = Int32.Parse(TextBox_MaxTracks.Text);
                    Modeltab_ShopClient_XianChangHuoDong.UpdateTime = DateTime.Now;

                    blltab_ShopClient_XianChangHuoDong.Update(Modeltab_ShopClient_XianChangHuoDong);

                    DeleteBlank();
                    JsUtil.ShowMsg("修改成功!", "25WeiXianChang_BoardSet.aspx");

                }
                else if (type.ToLower() == "add")
                {
                    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong blltab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();
                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Announcetab_ShopClient_XianChangHuoDong = new EggsoftWX.Model.tab_ShopClient_XianChangHuoDong();

                    Announcetab_ShopClient_XianChangHuoDong.ShowAgentErWeiMa_UserID_ByAgent = Int32.Parse(DropDownList_ShowAgentErWeiMa_UserID_ByAgent.SelectedValue);

                    Announcetab_ShopClient_XianChangHuoDong.ActivityName = Eggsoft.Common.CommUtil.SafeFilter(txtActivityName.Text);
                    Announcetab_ShopClient_XianChangHuoDong.PassWord = Eggsoft.Common.CommUtil.SafeFilter(TextboxPassword.Text);

                    #region 只有一个现场活动可以是开启状态
                    if (CheckBox_ActivityState.Checked)
                    {
                        bool boolOtherTrue = blltab_ShopClient_XianChangHuoDong.Exists("ShopClientID=" + strShopClientID + " and ActivityState=1");
                        if (boolOtherTrue)
                        {
                            CheckBox_ActivityState.Checked = false;
                            Eggsoft.Common.JsUtil.ShowMsg("只有一个现场活动可以是开启状态", -1);
                        }
                    }
                    #endregion 只有一个现场活动可以是开启状态

                    Announcetab_ShopClient_XianChangHuoDong.ActivityState = CheckBox_ActivityState.Checked;
                    Announcetab_ShopClient_XianChangHuoDong.Subscribe_Must = CheckBox_Subscribe_Must.Checked;
                    Announcetab_ShopClient_XianChangHuoDong.GetBonusRepeat = CheckBox_GetBonusRepeat.Checked;
                    Announcetab_ShopClient_XianChangHuoDong.Address_Must = CheckBox_Address_Must.Checked;
                    Announcetab_ShopClient_XianChangHuoDong.ShopClientID = Int32.Parse(strShopClientID);
                    Announcetab_ShopClient_XianChangHuoDong.Sort = intpos;
                    if (String.IsNullOrEmpty(strFileUpload_PIC_BigScreen) == false) Announcetab_ShopClient_XianChangHuoDong.Background_PIC_BigScreen = strFileUpload_PIC_BigScreen;
                    if (String.IsNullOrEmpty(strFileUpload_Logo_Mp3) == false) Announcetab_ShopClient_XianChangHuoDong.Background_SoundPath = strFileUpload_Logo_Mp3;

                    Announcetab_ShopClient_XianChangHuoDong.LongShakeTime = Int32.Parse(TextBox_MAXLongShakeTime.Text);
                    Announcetab_ShopClient_XianChangHuoDong.CountHowMany = Int32.Parse(TextBox_CountHowMany.Text);
                    Announcetab_ShopClient_XianChangHuoDong.MaxTracks = Int32.Parse(TextBox_MaxTracks.Text);


                    blltab_ShopClient_XianChangHuoDong.Add(Announcetab_ShopClient_XianChangHuoDong);


                    DeleteBlank();
                    JsUtil.ShowMsg("添加成功!", "25WeiXianChang_BoardSet.aspx");

                }
                #endregion 存盘



            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

        }

        /// <summary>
        /// 删除空白的 还没有开始 摇动的编号
        /// </summary>
        protected void DeleteBlank()
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            ///服务器端修改了  大屏幕信息 这里 删除空白的编号 调用 信息
            ///
            EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake BLL_Help_01XianChangHuoDong_UserShake = new EggsoftWX.BLL.b018Help_01XianChangHuoDong_UserShake();


            EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main BLL_Help_01XianChangHuoDong_Main = new EggsoftWX.BLL.b017Help_01XianChangHuoDong_Main();
            System.Data.DataTable DataTable_01XianChangHuoDong_Main = BLL_Help_01XianChangHuoDong_Main.GetList("ShopClientID=" + strShopClientID + " and (XianChangHuoDongNum_BeginTime is null or XianChangHuoDongNum_EndTime is null or XianChangHuoDongStatus=1)").Tables[0];
            for (int i = 0; i < DataTable_01XianChangHuoDong_Main.Rows.Count; i++)
            {
                string strID = DataTable_01XianChangHuoDong_Main.Rows[i]["ID"].ToString();
                string strXianChangHuoDongNum = DataTable_01XianChangHuoDong_Main.Rows[i]["XianChangHuoDongNum"].ToString();


                BLL_Help_01XianChangHuoDong_Main.Delete(int.Parse(strID));
                BLL_Help_01XianChangHuoDong_UserShake.Delete("ShopClientID=" + strShopClientID + " and XianChangHuoDongNum=" + strXianChangHuoDongNum);
            }

            //string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            //try
            //{
            //    #region 删除空白的 还没有开始 摇动的编号
            //    EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number BLL_tab_ShopClient_XianChangHuoDong_Number = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong_Number();
            //    BLL_tab_ShopClient_XianChangHuoDong_Number.Delete("ShopClientID=" + strShopClientID + " and IsDoing=1");

            //    string urlasmxServicesURL_HelpMachine = System.Configuration.ConfigurationManager.AppSettings["ServicesURL_HelpMachine"] + "/01XianChangHuoDong/doWS_01XianChangHuoDong.asmx";
            //    string[] args = new string[1];
            //    args[0] = strShopClientID;// "/UpLoad/images/";
            //    object result = WebServiceHelper.WsCaller.InvokeWebService(urlasmxServicesURL_HelpMachine, "doDelete_Help_01XianChangHuoDong_Main", args);
            //    string strReturn = result.ToString();
            //    #endregion
            //}
            //catch (Exception eeee)
            //{
            //    Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "后台保存微现场");
            //}

        }
    }
}