using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11WA_ClientShop
{
    public partial class Cart_Self : System.Web.UI.Page
    {
        private EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
        private EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
        protected string ShengCityCountry30PercentOr50 = "48";

        protected string pub_GetAgentShopName_From_Visit__ = "";
        protected int pub_Int_ShopClientID = 0;
        protected int pub_Int_Session_CurUserID = 0;
        protected string Pub_Agent_Path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setAllNeedID();

                try
                {


                    string type = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(type) == false)
                    {
                        if (type == "deleteaddress")
                        {

                            string RadioButtonList_Address = Request.Form["RadioButtonList_Address"];

                            if (String.IsNullOrEmpty(RadioButtonList_Address) == false)
                            {
                                EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
                                my_BLL_tab_User_Address.Delete("ID=" + RadioButtonList_Address);

                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();

                                if (my_BLL_tab_User_Address.ExistsCount("userID=" + pub_Int_Session_CurUserID + " and IsDeleted=0") > 0)
                                {
                                    string strID = my_BLL_tab_User_Address.GetList("userID=" + pub_Int_Session_CurUserID + " and IsDeleted=0").Tables[0].Rows[0]["ID"].ToString();
                                    my_BLL_tab_User.Update("Default_Address=" + strID, "ID=" + pub_Int_Session_CurUserID);
                                }
                                else
                                {
                                    my_BLL_tab_User.Update("Default_Address=" + "0", "ID=" + pub_Int_Session_CurUserID);
                                }
                                Eggsoft.Common.JsUtil.ShowMsg("删除成功", Pub_Agent_Path + "/cart_self.aspx");
                            }
                            Eggsoft.Common.JsUtil.ShowMsg("什么也没删除", Pub_Agent_Path + "/cart_self.aspx");
                            //System.Data.DataTable myDataTable = my_BLL_tab_User_Address.GetList("id,XiangXiDiZhi", "UserID=" + userID).Tables[0];


                        }
                        else if (type == "saveaddress")
                        {
                            try
                            {
                                string province = Request.Form["province"];
                                string city = Request.Form["city"];
                                string Area = Request.Form["Area"];
                                string XiangxiDizhi = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["XiangxiDizhi"]);
                                string LianXiren = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["LianXiren"]);
                                string YoouBian = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["YoouBian"]);
                                string phone = Eggsoft.Common.CommUtil.SafeFilter(Request.Form["phone"]);


                                if (XiangxiDizhi.Trim().Length == 0)
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("详细地址不能为空", -1);
                                    return;
                                }

                                #region 详细地址不能包含 省市可选择的地址。必须要选择。这是为了全场满运费 要使用省市地址来定位的原因
                                EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
                                string strtab_PE_RegionSQLShengShi = " SELECT CHARINDEX ( Province ,'" + XiangxiDizhi + "' ,  0 ) AS pos,CHARINDEX ( City ,'" + XiangxiDizhi + "' ,  0 ) AS pos1  FROM [tab_PE_Region] WHERE (CHARINDEX ( Province ,'" + XiangxiDizhi + "' ,  0 )>0 OR CHARINDEX ( City ,'" + XiangxiDizhi + "' ,  0 )>0)";
                                int intRowsCountShengShi = BLL_tab_PE_Region.SelectList(strtab_PE_RegionSQLShengShi).Tables[0].Rows.Count;
                                if (intRowsCountShengShi > 0)
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog(strtab_PE_RegionSQLShengShi, "省市地址请直接选择");
                                }
                                string strtab_PE_RegionSQL = " SELECT CHARINDEX ( Province ,'" + XiangxiDizhi + "' ,  0 ) AS pos  FROM [tab_PE_Region] WHERE (CHARINDEX ( Province ,'" + XiangxiDizhi + "' ,  0 )>0)";
                                int intRowsCount = BLL_tab_PE_Region.SelectList(strtab_PE_RegionSQL).Tables[0].Rows.Count;
                                if (intRowsCount > 0)
                                {
                                    Eggsoft.Common.debug_Log.Call_WriteLog(strtab_PE_RegionSQL, "省地址请直接选择");

                                    Eggsoft.Common.JsUtil.ShowMsg("友情提醒.详细地址不需包含省市地址,省市地址请直接选择", -1);
                                    return;
                                }
                                #endregion

                                # region saveContactInfo();
                                //int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();
                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User myModel = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                                myModel.Sheng = province;
                                myModel.Country = "中国";
                                myModel.City = city;
                                if (Area != "")
                                {
                                    myModel.Area = Area;
                                }
                                else
                                {
                                    myModel.Area = "";
                                }
                                myModel.PostCode = YoouBian;
                                myModel.ContactMan = LianXiren;
                                myModel.ContactPhone = phone;
                                myModel.Address = XiangxiDizhi;
                                my_BLL_tab_User.Update(myModel);
                                #endregion




                                # region     saveContactInfoAddress(true);///jump

                                Int32 saveContactInfoAddressINT = 0;

                                EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();

                                String strAddress = "";

                                strAddress += province;
                                strAddress += city;
                                if (Area != "")
                                {
                                    strAddress += Area;
                                }
                                strAddress += XiangxiDizhi;
                                strAddress += " " + LianXiren;
                                strAddress += " " + YoouBian;
                                strAddress += " " + phone;
                                int intUserAddressID = 0;
                                if (my_BLL_tab_User_Address.Exists("UserID=" + pub_Int_Session_CurUserID + " and XiangXiDiZhi='" + strAddress + "' and IsDeleted=0") == false)
                                {
                                    EggsoftWX.Model.tab_User_Address my_Model_tab_User_Address = new EggsoftWX.Model.tab_User_Address();
                                    my_Model_tab_User_Address.UserID = pub_Int_Session_CurUserID;
                                    my_Model_tab_User_Address.XiangXiDiZHi = strAddress;

                                    if (String.IsNullOrEmpty(Area))///处理直辖市情况
                                    {
                                        my_Model_tab_User_Address.pc_province = province;
                                        my_Model_tab_User_Address.pc_city = province;
                                        my_Model_tab_User_Address.pc_district = city;
                                    }
                                    else
                                    {
                                        my_Model_tab_User_Address.pc_province = province;
                                        my_Model_tab_User_Address.pc_city = city;
                                        my_Model_tab_User_Address.pc_district = Area;
                                    }


                                    //my_Model_tab_User_Address.pc_province = province;
                                    //my_Model_tab_User_Address.pc_city = city;
                                    //my_Model_tab_User_Address.pc_district = Area;
                                    my_Model_tab_User_Address.pc_street = XiangxiDizhi;

                                    my_Model_tab_User_Address.TelPhone = phone;
                                    my_Model_tab_User_Address.PostCode = YoouBian;
                                    my_Model_tab_User_Address.RealName = LianXiren;
                                    intUserAddressID = my_BLL_tab_User_Address.Add(my_Model_tab_User_Address);

                                    //EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                    my_BLL_tab_User.Update("Default_Address=" + intUserAddressID, "ID=" + pub_Int_Session_CurUserID);
                                    saveContactInfoAddressINT = Convert.ToInt32(intUserAddressID.ToString());


                                    #region  检查一些订单 是不是没有 地址 或者 地址已被删除
                                    EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                                    bool mybool = BLL_tab_Order.Exists("UserID=" + pub_Int_Session_CurUserID);
                                    if (mybool)
                                    {
                                        System.Data.DataTable myDataTable = BLL_tab_Order.GetList("id,User_Address", "UserID=" + pub_Int_Session_CurUserID).Tables[0];
                                        for (int k = 0; k < myDataTable.Rows.Count; k++)
                                        {
                                            string strID = myDataTable.Rows[k]["id"].ToString();
                                            string strUser_Address = myDataTable.Rows[k]["User_Address"].ToString();

                                            if (my_BLL_tab_User_Address.Exists("id=" + strUser_Address) == false)//not exsit .say  u buy goods in first
                                            {
                                                BLL_tab_Order.Update("User_Address=" + intUserAddressID, "id=" + strID);
                                            }
                                            else//check if exsit ,but delete
                                            {
                                                string strIsDeleted = my_BLL_tab_User_Address.GetList("IsDeleted", "id=" + strUser_Address).Tables[0].Rows[0]["IsDeleted"].ToString();
                                                if (strIsDeleted == "1")
                                                {
                                                    BLL_tab_Order.Update("User_Address=" + intUserAddressID, "id=" + strID);//也更新
                                                }
                                            }
                                        }
                                    }

                                    #endregion


                                }
                                else
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("亲,收货地址已存在", "javascript:history.back();");
                                    Response.End();
                                }
                                #endregion


                                string strModifyOrderID = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["ModifyOrderID"]);
                                string strpaymoneymusthaveaddress = Eggsoft.Common.CommUtil.SafeFilter(Request.QueryString["paymoney"]);

                                if (String.IsNullOrEmpty(strModifyOrderID) == false)
                                {
                                    if (Int32.Parse(strModifyOrderID) > 0)
                                    {
                                        EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                        mytab_Order.Update("User_Address=" + intUserAddressID, "id=" + strModifyOrderID);

                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", "/cart_good.aspx");
                                    }
                                    else
                                    {
                                        strModifyOrderID = (0 - Int32.Parse(strModifyOrderID)).ToString();//已发货的
                                        EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                        mytab_Order.Update("User_Address=" + intUserAddressID, "id=" + strModifyOrderID);

                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", Pub_Agent_Path + "/cart_good2.aspx");
                                    }
                                    Response.End();
                                }
                                else if (String.IsNullOrEmpty(strpaymoneymusthaveaddress) == false)
                                {
                                    if (strpaymoneymusthaveaddress == "paymoneymusthaveaddress")
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", "/cart.aspx");
                                    }

                                    else if (strpaymoneymusthaveaddress == "addaddressfromcart")
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("添加成功", "/cart.aspx");
                                    }
                                    Response.End();
                                }
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

                            Eggsoft.Common.JsUtil.ShowMsg("提交成功！", Pub_Agent_Path + "/cart_self.aspx");
                        }
                        else if (type == "choiceaddress")
                        {
                            string RadioButtonList_Address = Request.Form["RadioButtonList_Address"];
                            if (String.IsNullOrEmpty(RadioButtonList_Address) == false)
                            {
                                #region 充值 user 的省  为了 免运费的方案  等等
                                EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
                                EggsoftWX.Model.tab_User_Address my_Model_tab_User_Address = my_BLL_tab_User_Address.GetModel(Int32.Parse(RadioButtonList_Address));
                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();

                                if ((string.IsNullOrEmpty(my_Model_tab_User_Address.pc_province) == false))
                                {
                                    #region //后期更改 往表里插入了pc_province]      ,[pc_city]      ,[pc_district]  如果存在有效数据可以用这里的
                                    EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                                    if (string.IsNullOrEmpty(my_Model_tab_User_Address.pc_district) == true)////处理直辖市情况  程序的不足引起的 如上海市上海市闵行区
                                    {
                                        if (my_Model_tab_User.Sheng != my_Model_tab_User_Address.pc_province)
                                        {
                                            my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                                            my_Model_tab_User.City = my_Model_tab_User_Address.pc_province;
                                            my_Model_tab_User.Area = my_Model_tab_User_Address.pc_city;
                                            my_BLL_tab_User.Update(my_Model_tab_User);
                                        }
                                    }
                                    else
                                    {
                                        if (my_Model_tab_User.Sheng != my_Model_tab_User_Address.pc_province)
                                        {
                                            my_Model_tab_User.Sheng = my_Model_tab_User_Address.pc_province;
                                            my_Model_tab_User.City = my_Model_tab_User_Address.pc_city;
                                            my_Model_tab_User.Area = my_Model_tab_User_Address.pc_district;
                                            my_BLL_tab_User.Update(my_Model_tab_User);
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    string strXiangXiDiZHi = my_Model_tab_User_Address.XiangXiDiZHi;
                                    string strReturnProvince = "";

                                    int intZiZhiQu = strXiangXiDiZHi.IndexOf("自治区");
                                    int intProvince = strXiangXiDiZHi.IndexOf("省");
                                    int intCity = strXiangXiDiZHi.IndexOf("市");

                                    if (intZiZhiQu > -1)
                                    {
                                        strReturnProvince = strXiangXiDiZHi.Substring(0, intZiZhiQu + 3);
                                    }
                                    else if (intProvince > -1)
                                    {
                                        strReturnProvince = strXiangXiDiZHi.Substring(0, intProvince + 1);
                                    }
                                    else if (intCity > -1)  ///上海等直辖市
                                    {
                                        strReturnProvince = strXiangXiDiZHi.Substring(0, intCity + 1);
                                    }


                                    string strUpdate = "";
                                    //string strUpdate = "Default_Address=" + strXiangXiDiZhiID;
                                    if (String.IsNullOrEmpty(strReturnProvince) == false)
                                    {
                                        strUpdate += "Sheng='" + strReturnProvince + "'";
                                        my_BLL_tab_User.Update(strUpdate, "ID=" + pub_Int_Session_CurUserID);

                                        #region 优化算法  以后就不会进入这里  。因为 省份的优化已写出
                                        ///优化算法  以后就不会进入这里  。因为 省份的优化已写出
                                        ///
                                        my_Model_tab_User_Address.pc_province = strReturnProvince;
                                        my_BLL_tab_User_Address.Update(my_Model_tab_User_Address);
                                        #endregion
                                    }
                                }
                                #endregion

                            }

                            string strModifyOrderID = Request.QueryString["ModifyOrderID"];
                            if (String.IsNullOrEmpty(strModifyOrderID) == false)
                            {
                                if (Int32.Parse(strModifyOrderID) > 0)
                                {
                                    EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                    mytab_Order.Update("User_Address=" + RadioButtonList_Address, "id=" + strModifyOrderID);
                                    Eggsoft.Common.JsUtil.ShowMsg("设置成功", "/cart_good.aspx");
                                }
                                else
                                {
                                    strModifyOrderID = (0 - Int32.Parse(strModifyOrderID)).ToString();//已发货的
                                    EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                    mytab_Order.Update("User_Address=" + RadioButtonList_Address, "id=" + strModifyOrderID);
                                    Eggsoft.Common.JsUtil.ShowMsg("设置成功", Pub_Agent_Path + "/cart_good2.aspx");
                                }
                                Response.End();
                            }

                            else//当前页面更改了默认的收货地址
                            {
                                Eggsoft.Common.JsUtil.ShowMsg("选择成功", Pub_Agent_Path + "/cart_self.aspx");
                            }
                        }
                        else if (type.ToLower() == "isreadweixinaddress")//修改微信地址回来了
                        {
                            try
                            {
                                string province = HttpUtility.UrlDecode(Request.QueryString["provicefirststagename"]);
                                string city = HttpUtility.UrlDecode(Request.QueryString["addresscitysecondstagename"]);
                                string Area = HttpUtility.UrlDecode(Request.QueryString["addresscountiesthirdstagename"]);
                                string XiangxiDizhi = HttpUtility.UrlDecode(Request.QueryString["addressdetailinfo"]);
                                string LianXiren = HttpUtility.UrlDecode(Request.QueryString["username"]);
                                string YoouBian = HttpUtility.UrlDecode(Request.QueryString["addresspostalcode"]);
                                string phone = HttpUtility.UrlDecode(Request.QueryString["telnumber"]);


                                if (XiangxiDizhi.Trim().Length == 0)
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("详细地址不能为空", -1);

                                    return;
                                }

                                # region saveContactInfo();
                                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User myModel = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                                myModel.Sheng = province;
                                myModel.City = city;
                                if (Area != "")
                                {
                                    myModel.Area = Area;
                                }
                                else
                                {
                                    myModel.Area = "";
                                }
                                myModel.PostCode = YoouBian;
                                myModel.ContactMan = LianXiren;
                                myModel.ContactPhone = phone;
                                myModel.Address = XiangxiDizhi;
                                my_BLL_tab_User.Update(myModel);
                                #endregion




                                # region     saveContactInfoAddress(true);///jump

                                Int32 saveContactInfoAddressINT = 0;

                                EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();

                                String strAddress = "";

                                strAddress += province;
                                strAddress += city;
                                if (Area != "")
                                {
                                    strAddress += Area;
                                }
                                strAddress += XiangxiDizhi;
                                strAddress += " " + LianXiren;
                                strAddress += " " + YoouBian;
                                strAddress += " " + phone;
                                int intUserAddressID = 0;
                                if ((strAddress.Contains("undefined") == true))
                                {
                                    Eggsoft.Common.JsUtil.ShowMsg("操作失败.可能是权限不足");
                                }
                                else if ((strAddress.Contains("undefined") == false) && (my_BLL_tab_User_Address.Exists("UserID=" + pub_Int_Session_CurUserID + " and XiangXiDiZhi='" + strAddress + "' and IsDeleted=0") == false))
                                {
                                    EggsoftWX.Model.tab_User_Address my_Model_tab_User_Address = new EggsoftWX.Model.tab_User_Address();
                                    my_Model_tab_User_Address.UserID = pub_Int_Session_CurUserID;
                                    my_Model_tab_User_Address.XiangXiDiZHi = strAddress;
                                    if (String.IsNullOrEmpty(Area))///处理直辖市情况
                                    {
                                        my_Model_tab_User_Address.pc_province = province;
                                        my_Model_tab_User_Address.pc_city = province;
                                        my_Model_tab_User_Address.pc_district = city;
                                    }
                                    else
                                    {
                                        my_Model_tab_User_Address.pc_province = province;
                                        my_Model_tab_User_Address.pc_city = city;
                                        my_Model_tab_User_Address.pc_district = Area;
                                    }

                                    my_Model_tab_User_Address.pc_street = XiangxiDizhi;
                                    my_Model_tab_User_Address.TelPhone = phone;
                                    my_Model_tab_User_Address.PostCode = YoouBian;
                                    my_Model_tab_User_Address.RealName = LianXiren;
                                    intUserAddressID = my_BLL_tab_User_Address.Add(my_Model_tab_User_Address);

                                    //EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                    my_BLL_tab_User.Update("Default_Address=" + intUserAddressID, "ID=" + pub_Int_Session_CurUserID);
                                    saveContactInfoAddressINT = Convert.ToInt32(intUserAddressID.ToString());


                                    #region  检查一些订单 是不是没有 地址 或者 地址已被删除
                                    EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                                    bool mybool = BLL_tab_Order.Exists("UserID=" + pub_Int_Session_CurUserID);
                                    if (mybool)
                                    {
                                        System.Data.DataTable myDataTable = BLL_tab_Order.GetList("id,User_Address", "UserID=" + pub_Int_Session_CurUserID).Tables[0];
                                        for (int k = 0; k < myDataTable.Rows.Count; k++)
                                        {
                                            string strID = myDataTable.Rows[k]["id"].ToString();
                                            string strUser_Address = myDataTable.Rows[k]["User_Address"].ToString();

                                            if (my_BLL_tab_User_Address.Exists("id=" + strUser_Address) == false)//not exsit .say  u buy goods in first
                                            {
                                                BLL_tab_Order.Update("User_Address=" + intUserAddressID, "id=" + strID);
                                            }
                                            else//check if exsit ,but delete
                                            {
                                                string strIsDeleted = my_BLL_tab_User_Address.GetList("IsDeleted", "id=" + strUser_Address).Tables[0].Rows[0]["IsDeleted"].ToString();
                                                if (strIsDeleted == "1")
                                                {
                                                    BLL_tab_Order.Update("User_Address=" + intUserAddressID, "id=" + strID);//也更新
                                                }
                                            }
                                        }
                                    }

                                    #endregion


                                }
                                else
                                {
                                    ///v3pay_weixin/DefaultAdress.aspx

                                    string strv3pay_weixin = Request.UrlReferrer.AbsolutePath;
                                    if (strv3pay_weixin.ToLower().Contains("v3pay_weixin/defaultadress.aspx"))
                                    {

                                        Eggsoft.Common.JsUtil.ShowMsg("收货地址已存在了");
                                    }
                                    else
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("收货地址已存在", "javascript:history.back();");
                                        Response.End();
                                    }
                                }
                                #endregion


                                string strModifyOrderID = Request.QueryString["ModifyOrderID"];
                                string strpaymoneymusthaveaddress = Request.QueryString["paymoney"];

                                if (String.IsNullOrEmpty(strModifyOrderID) == false)
                                {
                                    if (Int32.Parse(strModifyOrderID) > 0)
                                    {
                                        EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                        mytab_Order.Update("User_Address=" + intUserAddressID, "id=" + strModifyOrderID);

                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", "/cart_good.aspx");
                                    }
                                    else
                                    {
                                        strModifyOrderID = (0 - Int32.Parse(strModifyOrderID)).ToString();//已发货的
                                        EggsoftWX.BLL.tab_Order mytab_Order = new EggsoftWX.BLL.tab_Order();
                                        mytab_Order.Update("User_Address=" + intUserAddressID, "id=" + strModifyOrderID);

                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", Pub_Agent_Path + "/cart_good2.aspx");
                                    }
                                    Response.End();
                                }
                                else if (String.IsNullOrEmpty(strpaymoneymusthaveaddress) == false)
                                {
                                    if (strpaymoneymusthaveaddress.Contains("paymoneymusthaveaddress"))
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("设置成功", "/cart.aspx");
                                    }

                                    else if (strpaymoneymusthaveaddress == "addaddressfromcart")
                                    {
                                        Eggsoft.Common.JsUtil.ShowMsg("添加成功", "/cart.aspx");
                                    }
                                    Response.End();
                                }

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

                            Eggsoft.Common.JsUtil.ShowMsg("提交成功！", Pub_Agent_Path + "/cart_self.aspx");
                        }
                        else if (type == "modifyorder")//外部传来修改的指令 要掉重复来
                        {
                            setLoadBody();
                        }
                    }
                    else
                    {
                        setLoadBody();
                    }
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
        private void setLoadBody()
        {
            try
            {

                string strLoadTodayGood = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/address_Templet.html");
                strLoadTodayGood = strLoadTodayGood.Replace("###SAgentPath###", Pub_Agent_Path);
                strLoadTodayGood = Eggsoft_Public_CL.WxConfig.WxConfig_Change_PulicChageWeiXin(strLoadTodayGood, "ShareShopFunction");//微信分享代码

                strLoadTodayGood = strLoadTodayGood.Replace("###header###", "");

                string strSubscribe = Eggsoft_Public_CL.Pub_GetOpenID_And_.CheckSubscribe(pub_Int_Session_CurUserID);
                strLoadTodayGood = strLoadTodayGood.Replace("###IFSubscribeHeader###", strSubscribe);
                //strLoadTodayGood = strLoadTodayGood.Replace("###WeiXin__o2o_FootMarker_Location_###", Eggsoft_Public_CL.Pub.Get_o2o_script_From_ShopClientID_(pub_Int_Session_CurUserID, "个人中心"));



                strLoadTodayGood = strLoadTodayGood.Replace("###Webuy8Footer###", Eggsoft_Public_CL.Pub_Agent.strGetMyAgentFooter(pub_Int_Session_CurUserID, pub_Int_ShopClientID, Pub_Agent_Path));
                strLoadTodayGood = strLoadTodayGood.Replace("###UserID###", pub_Int_Session_CurUserID.ToString()).Replace("###ServiceURL###", Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL()).Replace("###ShopClientID###", pub_Int_ShopClientID.ToString());


                #region if show ModifyOrder
                String strModifyOrder_Address = "";
                string type = Request.QueryString["type"];
                if (string.IsNullOrEmpty(type) == false)
                {
                    if (type == "modifyorder")
                    {
                        strModifyOrder_Address = "\n<button onclick=\"return sub_Choice_form();\" type=\"button\" class=\"Clickbutton\"  value=\"删除收货地址\" style=\"background-color: #f05200\">选择收货地址</button>\n";

                    }
                }
                strLoadTodayGood = strLoadTodayGood.Replace("###strModifyOrder_Address###", strModifyOrder_Address);
                #endregion


                strLoadTodayGood = InitContact_Address(strLoadTodayGood);

                Response.Write(strLoadTodayGood);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }

            finally
            {

            }
        }


        //private string setProvinceArrayList(string strLoadTodayGood)
        //{


        //    string strprovinceArrayListcityArrayListAreaArrayList = "";

        //    string setCanchekey = "provinceArrayListcityArrayListAreaArrayList";

        //    string strContent = (string)Eggsoft.Common.DataCache.GetCache(setCanchekey);
        //    if (string.IsNullOrEmpty(strContent))
        //    {

        //        string strProvinceContent = "provinceArray = new Array(";
        //        string strCityContent = "cityArray = new Array();	";
        //        string strAreaContent = "AreaArray = new Array();";
        //        //"北京市", "上海市", "天津市", "重庆市", "河北省", "山西省",
        //        //        "内蒙古自治区", "辽宁省", "吉林省", "黑龙江省", "江苏省", "浙江省", "安徽省", "福建省", "江西省",
        //        //        "山东省", "河南省", "湖北省", "湖南省", "广东省", "广西壮族自治区", "海南省", "四川省", "贵州省",
        //        //        "云南省", "西藏自治区", "陕西省", "甘肃省", "宁夏回族自治区", "青海省", "新疆维吾尔族自治区",
        //        //        "香港特别行政区", "澳门特别行政区", "台湾省", "其它"
        //        string strSql = "select * from (select max(id) as indexfield,Province from [tab_PE_Region] group by Province) as aaa order by aaa.indexfield  ";

        //        EggsoftWX.BLL.tab_PE_Region bll_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
        //        DataSet mydsProvince = bll_tab_PE_Region.SelectList(strSql);


        //        //string strMenuContent = "";
        //        int intAreaCount = 0;

        //        for (int i = 0; i < mydsProvince.Tables[0].Rows.Count; i++)
        //        {
        //            string Province = mydsProvince.Tables[0].Rows[i]["Province"].ToString();
        //            strProvinceContent = strProvinceContent + "\"" + Province + "\"" + ",";

        //            strSql = "select * from (select max(id) as indexfield,Province,City from [tab_PE_Region] where Province='" + Province + "' group by Province,City ) as aaa  order by aaa.indexfield  ";

        //            DataSet mydscityArrayList = bll_tab_PE_Region.SelectList(strSql);

        //            strCityContent += "cityArray[" + i.ToString() + "] = new Array(" + "\"" + Province + "\"" + ",\"";
        //            for (int j = 0; j < mydscityArrayList.Tables[0].Rows.Count; j++)
        //            {
        //                string City = mydscityArrayList.Tables[0].Rows[j]["City"].ToString();
        //                strCityContent = strCityContent + City + "|";

        //                strSql = "select * from (select max(id) as indexfield,City,Area from [tab_PE_Region] where City='" + City + "' group by City,Area ) as aaa  order by aaa.indexfield  ";
        //                DataSet mydsAreaArrayList = bll_tab_PE_Region.SelectList(strSql);

        //                strAreaContent += "AreaArray[" + (intAreaCount++).ToString() + "] = new Array(" + "\"" + City + "\"" + ",\"";
        //                for (int k = 0; k < mydsAreaArrayList.Tables[0].Rows.Count; k++)
        //                {
        //                    string Area = mydsAreaArrayList.Tables[0].Rows[k]["Area"].ToString();
        //                    strAreaContent = strAreaContent + Area + "|";
        //                }
        //                strAreaContent = strAreaContent.Remove(strAreaContent.Length - 1);//去最后字符@
        //                strAreaContent += "\");\n";
        //            }
        //            strCityContent = strCityContent.Remove(strCityContent.Length - 1);//去最后字符@
        //            strCityContent += "\");\n";
        //        }
        //        strProvinceContent = strProvinceContent.Remove(strProvinceContent.Length - 1);//去最后字符，

        //        strProvinceContent += ");\n";
        //        strProvinceContent += "//定义 城市 数据数组;\n";

        //        strprovinceArrayListcityArrayListAreaArrayList = strProvinceContent + "\n" + strCityContent + "\n" + strAreaContent;

        //        Eggsoft.Common.DataCache.SetCache(setCanchekey, strprovinceArrayListcityArrayListAreaArrayList);

        //    }
        //    else
        //    { strprovinceArrayListcityArrayListAreaArrayList = strContent; }

        //    strLoadTodayGood = strLoadTodayGood.Replace("###provinceArrayListcityArrayListAreaArrayList###", strprovinceArrayListcityArrayListAreaArrayList);


        //    return strLoadTodayGood;
        //}


        protected string InitContact_Address(string strLoadTodayGood)
        {
            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
            EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
            Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + pub_Int_ShopClientID);
            string str_appid = Model_tab_ShopClient_EngineerMode.WeiXinAppId;

            string localhosturl = Eggsoft.Common.Application.httpFullUrl_BeforeUrlRewriting();// HttpContext.Current.Request.Url.Host;
            string strAddressBackURL = localhosturl;
            if (strAddressBackURL.IndexOf("?") != -1)
            {
                strAddressBackURL = strAddressBackURL + "&appid=" + str_appid;
            }
            else
            {
                strAddressBackURL = strAddressBackURL + "?appid=" + str_appid;
            }

            strAddressBackURL += "&type=isreadweixinaddress";

            var ModifyOrderID = Request.QueryString["ModifyOrderID"];
            if (ModifyOrderID != null)
            {
                strAddressBackURL += "&modifyorderud=" + ModifyOrderID;
            }

            var paymoneymusthaveaddress = Request.QueryString["paymoney"];
            if (paymoneymusthaveaddress != null)
            {
                strAddressBackURL += "&paymoney=" + paymoneymusthaveaddress;
            }
            //varNeedAdd = encodeURIComponent(varNeedAdd);
            //var varisReadWeiXinAddressCur = varisReadWeiXinAddress.replace("{0}", varNeedAdd);
            //debugger;

            #region tab_ShopClient_WeiXin_stateurl   转换到一个整数
            EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl BLL_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.BLL.tab_ShopClient_WeiXin_Stateurl();
            bool boolstrstate = BLL_tab_ShopClient_WeiXin_Stateurl.Exists("UrlFrom='" + strAddressBackURL + "' and ShopClientID=" + pub_Int_ShopClientID);
            Int32 int32AspxCallBackURLState = 0;
            if (boolstrstate)
            {
                EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = BLL_tab_ShopClient_WeiXin_Stateurl.GetModel("UrlFrom='" + strAddressBackURL + "' and ShopClientID=" + pub_Int_ShopClientID);
                Model_tab_ShopClient_WeiXin_Stateurl.updateTime = DateTime.Now;
                Model_tab_ShopClient_WeiXin_Stateurl.intFromCount += 1;
                BLL_tab_ShopClient_WeiXin_Stateurl.Update(Model_tab_ShopClient_WeiXin_Stateurl);
                int32AspxCallBackURLState = Model_tab_ShopClient_WeiXin_Stateurl.ID;
            }
            else
            {
                EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl Model_tab_ShopClient_WeiXin_Stateurl = new EggsoftWX.Model.tab_ShopClient_WeiXin_Stateurl();
                Model_tab_ShopClient_WeiXin_Stateurl.intFromCount = 1;
                Model_tab_ShopClient_WeiXin_Stateurl.UrlFrom = Eggsoft.Common.StringNum.MaxLengthString(strAddressBackURL,400);
                Model_tab_ShopClient_WeiXin_Stateurl.ShopClientID = pub_Int_ShopClientID;
                int32AspxCallBackURLState = BLL_tab_ShopClient_WeiXin_Stateurl.Add(Model_tab_ShopClient_WeiXin_Stateurl);
            }
            #endregion

            //strstate = HttpUtility.UrlEncode(strstate);


            string strWXRedirect_uri = (Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/wxurl/myoauth1-" + pub_Int_ShopClientID + ".aspx");

            string strInitContact_Address = "";
            strInitContact_Address += "<script type=\"text/javascript\">\n";
            strInitContact_Address += "varisReadWeiXinAddress = \"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + str_appid + "&redirect_uri=" + strWXRedirect_uri + "&response_type=code&scope=snsapi_base&state=" + int32AspxCallBackURLState + "#wechat_redirect\";\n";
            strInitContact_Address += "</script>\n";


            EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
            System.Data.DataTable my_Default_Address_DataTable = my_BLL_tab_User.GetList("Default_Address", "ID=" + pub_Int_Session_CurUserID).Tables[0];
            string strDefault_AddressID = my_Default_Address_DataTable.Rows[0]["Default_Address"].ToString();

            EggsoftWX.BLL.tab_User_Address my_BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
            System.Data.DataTable myDataTable = my_BLL_tab_User_Address.GetList("id,XiangXiDiZhi", "UserID=" + pub_Int_Session_CurUserID + " and IsDeleted=0").Tables[0];

            strInitContact_Address += "<table id=\"RadioButtonList_Address\" class=\"RadioButtonList_Address_SmallFont\">";

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string strXiangXiDiZhi = myDataTable.Rows[i]["XiangXiDiZhi"].ToString();
                string strID = myDataTable.Rows[i]["id"].ToString();

                string StrCheck = "";
                if (strDefault_AddressID == strID)
                {
                    StrCheck = "checked";
                }


                strInitContact_Address += "<tr><td><label>\n";

                strInitContact_Address += "<input onclick=\"sub_Choice_Default();\" id=\"RadioButtonList_Address_" + i + "\" type=\"radio\" " + StrCheck + " value=\"" + strID + "\" name=\"RadioButtonList_Address\">\n";
                strInitContact_Address += strXiangXiDiZhi + "</label>\n";


                strInitContact_Address += "<td><tr>\n";
            }





            strInitContact_Address += "</table>\n";

            strLoadTodayGood = strLoadTodayGood.Replace("###strInitContact_Address###", strInitContact_Address);





            EggsoftWX.Model.tab_User myModel = my_BLL_tab_User.GetModel(pub_Int_Session_CurUserID);

            strLoadTodayGood = strLoadTodayGood.Replace("###myModel.Sheng###", myModel.Sheng != "undefined" ? myModel.Sheng : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###myModel.City###", myModel.City != "undefined" ? myModel.City : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###myModel.Area###", myModel.Area != "undefined" ? myModel.Area : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###XiangxiDizhi###", myModel.Address != "undefined" ? myModel.Address : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###LianXiren###", myModel.ContactMan != "undefined" ? myModel.ContactMan : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###YoouBian###", myModel.PostCode != "undefined" ? myModel.PostCode : "");
            strLoadTodayGood = strLoadTodayGood.Replace("###phone###", myModel.ContactPhone != "undefined" ? myModel.ContactPhone : "");



            return strLoadTodayGood;

        }
    }
}