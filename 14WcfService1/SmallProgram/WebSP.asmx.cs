using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.SmallProgram
{
    /// <summary>
    /// WebSP 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebSP : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public String doXWXAction()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string strReturnJSON = "";

            try
            {
                string strOopt = context.QueryString["opt"];
                switch (strOopt)
                {
                    case "onLogin": strReturnJSON = onLogin(context); break;//登录  http://localhost:8014/SmallProgram/WebSP.asmx/doXWXAction?opt=onLogin
                    case "addUser": strReturnJSON = addUser(context); break;//创建会员
                    case "updateUser": strReturnJSON = updateUser(context); break;//修改会员信息
                    case "getListUser": strReturnJSON = getListUser(context); break;//分页查询会员
                    case "getListUserCount": strReturnJSON = getListUserCount(context); break;//统计会员
                    case "getUser": strReturnJSON = getUser(context); break;//读取会员信息
                    case "getListUserIntegral": strReturnJSON = getListUserIntegral(context); break;//会员积分排序
                    case "getWebSite": strReturnJSON = getWebSite(context); break;//站点信息
                    case "getColumnList": strReturnJSON = getColumnList(context); break;//栏目列表
                    case "getArticlePage": strReturnJSON = getArticlePage(context); break;//分页查询内容 http://localhost:8014/SmallProgram/WebSP.asmx/doXWXAction?opt=getArticlePage&classId=93&page=1&size=46
                    case "getArticleInfo": strReturnJSON = getArticleInfo(context); break;//内容详情
                    case "getBanner": strReturnJSON = getBanner(context); break;//获取banner
                    case "getProductPageList": strReturnJSON = getProductPageList(context); break;//获取产品列表
                    case "getProductInfo": strReturnJSON = getProductInfo(context); break;//获取产品列表
                    case "getType": strReturnJSON = getType(context); break;//读取分类
                    case "getHotType": strReturnJSON = getHotType(context); break;//读取热门分类
                    case "getTypeModel": strReturnJSON = getTypeModel(context); break;//读取分类详细信息
                    case "setCartIsChecked": strReturnJSON = setCartIsChecked(context); break;//设置购物车默认不选择
                    case "addCart": strReturnJSON = addCart(context); break;//插入购物车
                    case "getCartList": strReturnJSON = getCartList(context); break;//读取购物车
                    case "dTotal": strReturnJSON = dTotal(context); break;//购物车计算金额
                    case "updateCart": strReturnJSON = updateCart(context); break;//购物车加减
                    case "deleteCart": strReturnJSON = deleteCart(context); break;//购物车删除
                    case "choice": strReturnJSON = choice(context); break;//购物车选择
                    case "selection": strReturnJSON = selection(context); break;//购物车全选
                    case "getCountChecked": strReturnJSON = getCountChecked(context); break;//购物车是否选中获取全选
                    case "addOrder": strReturnJSON = addOrder(context); break;//提交订单
                    case "canalOrder": strReturnJSON = canalOrder(context); break;//取消订单
                    case "getAddressList": strReturnJSON = getAddressList(context); break;//获取地址
                    case "addAddress": strReturnJSON = addAddress(context); break;//更新地址
                    case "getAddress": strReturnJSON = getAddress(context); break;//查询地址详细
                    case "deleteAddress": strReturnJSON = deleteAddress(context); break;//删除地址
                    case "defaultAddress": strReturnJSON = defaultAddress(context); break;//设置默认地址
                    case "getOrderList": strReturnJSON = getOrderList(context); break;//查询订单列表
                    case "getOrder": strReturnJSON = getOrder(context); break;//查看订单详细
                    case "getOrderSubList": strReturnJSON = getOrderSubList(context); break;//查看订单购物明细
                    case "getOrderSate": strReturnJSON = getOrderSate(context); break;//查看订单状态
                    case "getOrderRed": strReturnJSON = getOrderRed(context); break;//计算订单红点状态
                    case "GetUnifiedOrderResult": strReturnJSON = GetUnifiedOrderResult(context); break;//微信支付统一下单
                                                                                                        //case "getUserRechargeList": strReturnJSON = getUserRechargeList(context); break; //获取充值记录
                                                                                                        //case "getUserIntegralList": strReturnJSON = getUserIntegralList(context); break; //获取积分记录
                                                                                                        //case "addUserRecharge": strReturnJSON = addUserRecharge(context); break;//充值
                                                                                                        //case "getIntegralProductPageList": strReturnJSON = getIntegralProductPageList(context); break;//获取积分产品列表
                                                                                                        //case "getIntegralProductInfo": strReturnJSON = getIntegralProductInfo(context); break;//获取积分产品
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                strReturnJSON = Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                strReturnJSON = Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

            //response.Charset = "gb2312"; //设置字符集类型  
            //response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            //response.Write(strReturnJSON);
            //response.End();
            return strReturnJSON;
        }




        #region 通过code获取用户的openid=======================
        public String onLogin(HttpRequest context)
        {


            string code = context.QueryString["code"];
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string Str = GetJson("https://api.weixin.qq.com/sns/jscode2session?appid=" + Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "SmallProgram_APPID") + "&secret=" + Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "mallProgram_Secret") + "&js_code=" + code + "&grant_type=authorization_code");
            if (Str != null)
            {
                return (Str);
                ////Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        /// <summary>
        /// 接受信息
        /// </summary>
        /// <param name="url">目标连接地址</param>
        /// <returns></returns>
        public static string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);
            if (returnText.Contains("errcode"))
            {
                //可能发生错误  
            }
            return returnText;
        }
        #endregion

        #region getUser会员=======================
        public String getUser(HttpRequest context)
        {
            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();


            string struserId = context.QueryString["userId"];
            int userId = struserId.toInt32();
            EggsoftWX.Model.tab_User Model_tab_User = BLLtab_User.GetModel(struserId.toInt32());
            //Cms.Model.C_user modelUpdate = new Cms.BLL.C_user().GetModel(userId);
            string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(Model_tab_User);
            return strJson;
        }
        public String getListUser(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;

            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            System.Data.DataTable dsDataTable = BLLtab_User.GetPageDataTable(page, size, "*", "shopclientID=" + ShopClientID, "ID", false);

            //DataSet ds = new Cms.BLL.C_user().GetListByPage("", "id desc", start, end);
            if (dsDataTable != null && dsDataTable.Rows.Count > 0)
            {
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsDataTable);
                //ToJson
                //IList<EggsoftWX.Model.tab_User> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_User>(dsDataTable);
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        public String getListUserCount(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int result = new EggsoftWX.BLL.tab_User().ExistsCount("ShopClientID=@ShopClientID", ShopClientID);
            string strJson = "{\"userCount\":" + result + "}";
            return strJson;

        }
        public String getListUserIntegral(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;

            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            System.Data.DataTable dsDataTable = BLLtab_User.GetPageDataTable(page, size, "*", "shopclientID=" + ShopClientID, "ID", false);

            //DataSet ds = new Cms.BLL.C_user().GetListByPage("", "userscore desc", start, end);
            if (dsDataTable != null && dsDataTable.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_User> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_User>(dsDataTable);

                //List<Cms.Model.C_user> list = new Cms.BLL.C_user().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
            }
            else
            {
                return "";
            }


        }
        public String updateUser(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string mobile = context.QueryString["mobile"] ?? "";
            string address = context.QueryString["address"] ?? "";
            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");

            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User modelUpdate = BLLtab_User.GetModel(userId.toInt32());


            modelUpdate.ContactPhone = mobile;
            int intuserUser_AddressID = 0;
            EggsoftWX.BLL.tab_User_Address BLLtab_User_Address = new EggsoftWX.BLL.tab_User_Address();
            var intExistsModel = BLLtab_User_Address.GetModel("UserID=@UserID", userId);
            if (intExistsModel != null)
            {
                EggsoftWX.Model.tab_User_Address Model_tab_User_Address = new EggsoftWX.Model.tab_User_Address();
                Model_tab_User_Address.MobilePhone = mobile;
                Model_tab_User_Address.XiangXiDiZHi = address;
                Model_tab_User_Address.UserID = userId;
                Model_tab_User_Address.CreatTime = DateTime.Now;
                intuserUser_AddressID = BLLtab_User_Address.Add(Model_tab_User_Address);
            }
            else
            {
                intuserUser_AddressID = intExistsModel.ID;
            }
            modelUpdate.Default_Address = intuserUser_AddressID;
            modelUpdate.Updatetime = DateTime.Now;
            modelUpdate.UpdateBy = "小程序更新地址";
            BLLtab_User.Update(modelUpdate);
            string strJson = "{\"state\":1}";
            return strJson;
        }
        public String addUser(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();


            string openid = context.QueryString["openid"];
            string nickName = context.QueryString["nickName"];
            string avatarUrl = context.QueryString["avatarUrl"];
            int gender = Convert.ToInt32(context.QueryString["gender"] ?? "0");
            EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
            Model_tab_User.NickName = nickName;
            Model_tab_User.SmallProgramOpenID = openid;
            Model_tab_User.HeadImageUrl = avatarUrl;
            Model_tab_User.Sex = gender == 1 ? true : false;
            Model_tab_User.ShopClientID = ShopClientID;
            //Model_tab_User.userscore = 0;
            //Model_tab_User.userMoney = 0;
            Model_tab_User.Updatetime = DateTime.Now;
            Model_tab_User.Password = "123456";

            EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
            var varGetModel = BLLtab_User.GetModel("SmallProgramOpenID=@SmallProgramOpenID and ShopClientID=@ShopClientID", openid, ShopClientID);

            if (varGetModel != null)
            {
                // Cms.Model.C_user modelUpdate = new Cms.BLL.C_user().GetModel(Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()));
                varGetModel.NickName = nickName;
                varGetModel.HeadImageUrl = avatarUrl;
                BLLtab_User.Update(varGetModel);
                string strJson = "{\"userId\":" + varGetModel.ID.ToString() + "}";
                return strJson;
                //Response.End();
            }
            else
            {
                int result = BLLtab_User.Add(Model_tab_User);
                if (result > 0)
                {
                    string strJson = "{\"userId\":" + result + "}";
                    return strJson;
                }
                else { return ""; }
            }

        }
        #endregion

        #region 站点信息=======================
        public String getWebSite(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

            Model_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient().GetModel(ShopClientID);
            if (Model_tab_ShopClient != null)
            {
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(Model_tab_ShopClient);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region 分页查询内容列表=======================
        public String getColumnList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int classId = Convert.ToInt32(context.QueryString["classId"] ?? "0");
            int top = Convert.ToInt32(context.QueryString["top"] ?? "0");
            EggsoftWX.BLL.tab_Class1 BLLtab_Class1 = new EggsoftWX.BLL.tab_Class1();

            System.Data.DataTable dsDataTable = BLLtab_Class1.GetDataTable("1000", "*", "shopclientID=" + ShopClientID);

            //DataSet ds = new Cms.BLL.C_Column().GetList(top, "parentId=" + classId + " and isShowChannel=1 ", "orderNumber desc");
            if (dsDataTable != null && dsDataTable.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_Class1> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Class1>(dsDataTable);

                // List<Cms.Model.C_Column> list = new Cms.BLL.C_Column().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        public String getArticlePage(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int classId = Convert.ToInt32(context.QueryString["classId"] ?? "0");
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;
            System.Data.DataTable dsDataTable = new EggsoftWX.BLL.tab_ShopClient_GuidePages().GetPageDataTable(page, size, "*", " and shopclientID=@ShopClientID and ParentID=0 and isnull(IsDeleted,0)=0", "ID", false, ShopClientID);

            // DataSet ds = new Cms.BLL.C_article().GetListByPage("parentId=" + classId + "", "ordernumber asc,articleId desc", start, end);
            if (dsDataTable != null && dsDataTable.Rows.Count > 0)
            {
                //IList<EggsoftWX.Model.tab_ShopClient_GuidePages> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_ShopClient_GuidePages>(dsDataTable);

                //List<Cms.Model.C_article> list = new Cms.BLL.C_article().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsDataTable);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        public String getArticleInfo(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            EggsoftWX.Model.tab_ShopClient_GuidePages model = new EggsoftWX.BLL.tab_ShopClient_GuidePages().GetModel(id);
            if (model != null)
            {
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region  获取banner===========================
        public String getBanner(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //int adtype = Convert.ToInt32(context.QueryString["typeId"] ?? "0");
            DataTable dsTables = new EggsoftWX.BLL.tab_AnnouncePic().GetDataTable("60", "*", "UserID=" + ShopClientID + " order by Pos asc,id desc");
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_AnnouncePic> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_AnnouncePic>(dsTables);

                //List<Cms.Model.C_ad> list = new Cms.BLL.C_ad().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region 获取产品列表=======================
        public String getProductPageList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string where = context.QueryString["where"] ?? "";
            string orderBy = context.QueryString["orderBy"] ?? "";
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;
            DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetDataTable("600", "*", where + " " + orderBy + "Sort asc,id desc");

            //DataSet ds = new Cms.BLL.C_product().GetListByPage(where, orderBy + "sortId asc,id desc", start, end);
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dsTables);

                //List<Cms.Model.C_product> list = new Cms.BLL.C_product().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        public String getProductInfo(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            EggsoftWX.Model.tab_Goods model = new EggsoftWX.BLL.tab_Goods().GetModel(id);
            if (model != null)
            {

                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region 读取分类================
        public String getType(HttpRequest context)
        {
            string parentId = context.QueryString["parentId"];

            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            DataTable dsTables = new EggsoftWX.BLL.tab_Class2().GetDataTable("600", "*", " Class1_ID=@Class1_ID order by Pos desc,id desc", parentId);

            //EggsoftWX.Model.tab_Class1 modeltab_Class1 = new EggsoftWX.BLL.tab_Class1()。.(id);
            //List<Cms.Model.C_type> list = new Cms.BLL.C_type().GetModelList("parent_id=" + parentId + " order by sort_id asc");
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dsTables);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region 读取热门分类================
        public String getHotType(HttpRequest context)
        {
            return getType(context);
            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //string parentId = context.QueryString["parentId"];
            //DataTable dsTables = new EggsoftWX.BLL.tab_Class2().GetDataTable("600", "*", " Class1_ID=@Class1_ID order by Pos desc,id desc", parentId);

            ////List<Cms.Model.C_type> list = new Cms.BLL.C_type().GetModelList("parent_id=" + parentId + " and isHot=1 order by id desc,sort_id asc");
            //if (list != null && list.Count > 0)
            //{
            //    string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            //    return "";
            //    //Response.End();
            //}
        }
        #endregion

        #region 读取分类详细信息=======================
        public String getTypeModel(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            EggsoftWX.Model.tab_Class1 model = new EggsoftWX.BLL.tab_Class1().GetModel(id);
            //Cms.Model.C_type model = new Cms.BLL.C_type().GetModel(id);
            if (model != null)
            {
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion

        #region 购物车=======================
        public String setCartIsChecked(HttpRequest context)
        {

            Eggsoft.Common.debug_Log.Call_WriteLog("设置购物车的默认选中状态1", "购物车 不知道如何操作");

            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            //int result = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=1  where user_id=" + userId);//设置购物车的默认选中状态1
            //if (result > 0)
            //{
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
        }
        public String addCart(HttpRequest context)
        {
            Eggsoft.Common.debug_Log.Call_WriteLog("addCart(HttpRequest context)", "addCart购物车 不知道如何操作");

            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            //int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            //int quantity = Convert.ToInt32(context.QueryString["quantity"] ?? "0");
            //string unit = context.QueryString["unit"] ?? "";
            //int isChecked = Convert.ToInt32(context.QueryString["isChecked"] ?? "1");
            //int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            //Cms.Model.C_user_cart model = new Cms.Model.C_user_cart();
            //model.article_id = id;
            //if (isChecked == 2)
            //{
            //    Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=1  where user_id=" + userId + " and typeId=" + typeId);//设置购物车的默认选中状态1
            //}
            //if (typeId == 1)
            //{
            //    Cms.Model.C_product product = new Cms.BLL.C_product().GetModel(id);
            //    model.title = product.name.ToString();
            //    model.price = product.price;
            //    model.integral = product.integral;
            //}
            //else
            //{
            //    Cms.Model.C_integral_product product = new Cms.BLL.C_integral_product().GetModel(id);
            //    model.title = product.name.ToString();
            //    model.price = product.price;
            //    model.integral = product.integral;
            //}

            //model.quantity = quantity;
            //model.user_id = userId;
            //model.is_checked = isChecked;
            //model.property_value = unit;
            //model.note = "";
            //model.typeId = typeId;
            //model.updateTime = DateTime.Now;
            //int result = 0;
            //if (new Cms.BLL.C_user_cart().Exists(id, userId, typeId))
            //{
            //    result = Cms.DBUtility.DbHelperSQL.ExecuteSql("update C_user_cart set is_checked=" + isChecked + ",quantity=quantity+" + quantity + ",updateTime='" + DateTime.Now + "' where article_id=" + id + " and user_id=" + userId + " and typeId=" + typeId);

            //}
            //else
            //{
            //    result = new Cms.BLL.C_user_cart().Add(model);
            //}
            //if (result > 0)
            //{
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
        }

        public String getCartList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            string where = context.QueryString["where"] ?? "";

            System.Data.DataTable dt_DataTable_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart().GetList("UserID=" + userId + " and IsDeleted<>1").Tables[0];

            //List<Cms.Model.C_user_cart> list = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and typeId=" + typeId + where);
            if (dt_DataTable_ShopingCart != null && dt_DataTable_ShopingCart.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dt_DataTable_ShopingCart);
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                //List<Dictionary<string, object>> listNew = new List<Dictionary<string, object>>();
                //foreach (Cms.Model.C_user_cart record in list)
                //{
                //    Dictionary<string, object> map = new Dictionary<string, object>();
                //    map.Add("id", record.id);
                //    map.Add("user_id", record.user_id);
                //    if (typeId == 1)
                //    {
                //        if (new Cms.BLL.C_product().Exists(Convert.ToInt32(record.article_id)))
                //        {
                //            map.Add("litpic", new Cms.BLL.C_product().GetModel(Convert.ToInt32(record.article_id)).litpic.ToString());
                //            map.Add("marketPrice", new Cms.BLL.C_product().GetModel(Convert.ToInt32(record.article_id)).marketPrice);
                //        }
                //    }
                //    else
                //    {
                //        if (new Cms.BLL.C_integral_product().Exists(Convert.ToInt32(record.article_id)))
                //        {
                //            map.Add("litpic", new Cms.BLL.C_integral_product().GetModel(Convert.ToInt32(record.article_id)).litpic.ToString());
                //            map.Add("marketPrice", new Cms.BLL.C_integral_product().GetModel(Convert.ToInt32(record.article_id)).marketPrice);
                //        }
                //    }
                //    map.Add("title", record.title);
                //    map.Add("price", record.price);
                //    map.Add("quantity", record.quantity);
                //    map.Add("integral", record.integral);
                //    map.Add("property_value", record.property_value);
                //    map.Add("note", record.note);
                //    map.Add("is_checked", record.is_checked);
                //    map.Add("checked", record.is_checked == 2 ? "true" : "false");
                //    listNew.Add(map);
                //}
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(listNew);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        #region 选择============================
        public String choice(HttpRequest context)
        {
            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //string id = context.QueryString.QueryString["id"];
            //string checkId = context.QueryString.QueryString["checkId"];
            //if (checkId == "1")
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=2  where id=" + id);
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=1  where id=" + id);
            string strJson = "{\"status\":0}";
            return strJson;
            //    //Response.End();
            //}

        }
        #endregion

        #region 全选============================
        public String selection(HttpRequest context)
        {
            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //string checkId = context.QueryString["checkId"];
            //string userId = context.QueryString["userId"];
            //int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            //if (checkId == "1")
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=2  where user_id=" + Convert.ToInt32(userId) + " and typeId=" + typeId);
            string strJson = "{\"status\":0}";
            return strJson;
            //    //Response.End();
            //}
            //else
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set is_checked=1  where user_id=" + Convert.ToInt32(userId) + " and typeId=" + typeId);//设置购物车的默认选中状态1
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}

        }
        public String getCountChecked(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            //List<Cms.Model.C_user_cart> listCount = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and typeId=" + typeId);
            //List<Cms.Model.C_user_cart> list = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and is_checked=2" + " and typeId=" + typeId);
            //if (list != null && list.Count > 0)
            //{
            //    if (listCount.Count == list.Count)
            //    {
            //        string strJson = "{\"status\":2}";
            //        return strJson;
            //        //Response.End();
            //    }
            //    else
            //    {
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
            //}
            //    else
            //    {
            //        string strJson = "{\"status\":0}";
            //        return strJson;
            //        //Response.End();
            //    }
        }
        #endregion

        #region 计算选中的金额============================
        public String dTotal(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            Double dTotal = 0.00;//总计款额
            Double marketPriceTotal = 0.00;//总计款额
            int integralTotal = 0;
            int quantity = 0;
            string userId = context.QueryString["userId"];
            int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            //System.Data.DataSet ds = new Cms.BLL.C_user_cart().GetList("user_id=" + userId + " and typeId=" + typeId);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (ds.Tables[0].Rows[i]["is_checked"].ToString() == "2")
            //        {
            //            integralTotal += Convert.ToInt32(ds.Tables[0].Rows[i]["integral"]) * Convert.ToInt32(ds.Tables[0].Rows[i]["quantity"]);
            //            dTotal += Convert.ToDouble(ds.Tables[0].Rows[i]["price"]) * Convert.ToInt32(ds.Tables[0].Rows[i]["quantity"]);
            //            marketPriceTotal += Convert.ToDouble(new Cms.BLL.C_product().GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["article_id"])).marketPrice * Convert.ToInt32(ds.Tables[0].Rows[i]["quantity"]));
            //            quantity += Convert.ToInt32(ds.Tables[0].Rows[i]["quantity"]);
            //        }
            //    }
            //    marketPriceTotal = marketPriceTotal - dTotal;
            //    Response.Write("{\"dTotal\":" + dTotal + ",\"marketPriceTotal\":" + marketPriceTotal + ",\"quantity\":" + quantity + ",\"integralTotal\":" + integralTotal + "}");
            //    //Response.End();
            //}
            //else
            //{
            return ("{\"dTotal\":" + dTotal + ",\"marketPriceTotal\":" + marketPriceTotal + ",\"quantity\":" + quantity + ",\"integralTotal\":" + integralTotal + "}");
            //Response.End();
            //}

        }
        #endregion

        #region 加减============================
        public String updateCart(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string id = context.QueryString["id"];
            string type = context.QueryString["type"];
            //if (type == "1")
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set quantity=quantity+1 where id=" + id);
            string strJson = "{\"status\":0}";
            return strJson;
            //Response.End();
            //}
            //else
            //{
            //    int count = Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_cart set quantity=quantity-1 where id=" + id);
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}

        }
        #endregion

        #region 删除============================
        public String deleteCart(HttpRequest context)
        {
            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //string id = context.QueryString["id"];
            //new Cms.BLL.C_user_cart().Delete(Convert.ToInt32(id));
            string strJson = "{\"status\":0}";
            return strJson;
            //Response.End();

        }
        #endregion
        #endregion

        #region 提交订单=======================
        public String addOrder(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //Cms.Model.C_order modelOrder = new Cms.Model.C_order();
            //Cms.Model.C_ordersub modelOrderSub = new Cms.Model.C_ordersub();
            //int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            //int addressId = Convert.ToInt32(context.QueryString["addressId"] == "" ? "0" : context.QueryString["addressId"]);
            //int quantity_sum = Convert.ToInt32(context.QueryString["quantity_sum"] ?? "0");
            //string price_sum = context.QueryString["price_sum"] ?? "0";
            //string shipping_method = context.QueryString["shipping_method"] ?? "0";
            //string recommended_code = context.QueryString["recommended_code"] ?? "0";
            //string note = context.QueryString["note"] ?? "0";
            //modelOrder.order_num = Cms.Common.Utils.GetOrderNumber();//生成订单号
            //modelOrder.user_id = userId;
            //modelOrder.adress_id = addressId;//收货地址id
            //modelOrder.quantity_sum = quantity_sum;
            //modelOrder.price_sum = Convert.ToDecimal(price_sum);
            //modelOrder.integral_sum = 0;
            //modelOrder.is_payment = 0;//0表示未支付
            //modelOrder.order_status = 0;//订单状态
            //modelOrder.is_delivery = 0;//订单是否发货
            //modelOrder.is_receiving = 0;//是否收货
            //modelOrder.is_transaction = 0;//订单是否交易完成
            //modelOrder.is_sms = 0;//是否发送短信
            //modelOrder.shipping_method = shipping_method;//配送方式
            //modelOrder.pay_method = "微信支付";
            //modelOrder.note = note;//留言
            //modelOrder.recommended_code = recommended_code;//推荐码
            //modelOrder.updateTime = DateTime.Now;
            //int result = new Cms.BLL.C_order().Add(modelOrder);
            //if (result > 0)
            //{
            //    DataTable dt = new Cms.BLL.C_user_cart().GetList("is_checked=2 and user_id=" + userId).Tables[0];
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            modelOrderSub.order_id = result;
            //            modelOrderSub.order_num = modelOrder.order_num;
            //            modelOrderSub.user_id = userId;
            //            modelOrderSub.article_id = Convert.ToInt32(dt.Rows[i]["article_id"]);
            //            modelOrderSub.title = dt.Rows[i]["title"].ToString();
            //            modelOrderSub.price = Convert.ToDecimal(dt.Rows[i]["price"].ToString());
            //            modelOrderSub.quantity = Convert.ToInt32(dt.Rows[i]["quantity"]);
            //            modelOrderSub.integral = Convert.ToInt32(dt.Rows[i]["integral"]);
            //            modelOrderSub.property_value = dt.Rows[i]["property_value"].ToString();
            //            modelOrderSub.note = dt.Rows[i]["note"].ToString();
            //            modelOrderSub.updateTime = modelOrder.updateTime;
            //            new Cms.BLL.C_ordersub().Add(modelOrderSub);
            //            new Cms.BLL.C_user_cart().Delete(Convert.ToInt32(dt.Rows[i]["id"]));
            //        }
            //    }
            //    string strJson = "{\"status\":" + result + "}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = "{\"status\":0}";
            return strJson;
            //Response.End();
            //}
        }

        public String canalOrder(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            //Cms.Model.C_order orderEntity = new Cms.BLL.C_order().GetModel(id);
            //orderEntity.order_status = 1;
            //if (new Cms.BLL.C_order().Update(orderEntity))
            //{
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
        }
        //查询地址
        public String getAddressList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            string where = context.QueryString["where"] ?? "0";

            System.Data.DataTable dt_DataTable_tab_User_Address = new EggsoftWX.BLL.tab_User_Address().GetList("UserID=" + userId + " and IsDeleted<>1").Tables[0];

            //List<Cms.Model.C_user_cart> list = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and typeId=" + typeId + where);
            if (dt_DataTable_tab_User_Address != null && dt_DataTable_tab_User_Address.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_User_Address> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_User_Address>(dt_DataTable_tab_User_Address);


                //List<EggsoftWX.Model.tab_User_Address> list = new Cms.BLL.c_user_address().GetModelList("user_id=" + userId + where);
                //if (list != null && list.Count > 0)
                //{
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                string strJson = "";
                return strJson;
                //Response.End();
            }
        }
        public String getAddress(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            EggsoftWX.Model.tab_User_Address model = new EggsoftWX.BLL.tab_User_Address().GetModel(id);
            if (model != null)
            {
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        public String deleteAddress(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");

            if (new EggsoftWX.BLL.tab_User_Address().Delete(id) > 0)
            {
                string strJson = "{\"status\":1}";
                return strJson;
                //Response.End();
            }
            else
            {
                string strJson = "{\"status\":0}";
                return strJson;
                //Response.End();
            }
        }
        public String defaultAddress(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");

            EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(userId);
            Modeltab_User.Default_Address = id;
            new EggsoftWX.BLL.tab_User().Update(Modeltab_User);
            //Cms.DBUtility.DbHelperSQL.ExecuteSql("update c_user_address set is_default=0 where user_id=" + userId);
            //Cms.Model.c_user_address model = new Cms.BLL.c_user_address().GetModel(id);
            //model.is_default = 1;
            //if (new Cms.BLL.c_user_address().Update(model))
            //{
            //    string strJson = "{\"status\":0}";
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
        }
        public String addAddress(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            string consignee = context.QueryString["consignee"] ?? "0";
            string cellphone = context.QueryString["cellphone"] ?? "0";
            string code = context.QueryString["code"] ?? "0";
            string Province = context.QueryString["Province"] ?? "0";
            string City = context.QueryString["City"] ?? "0";
            string District = context.QueryString["District"] ?? "0";
            string address = context.QueryString["address"] ?? "0";
            string is_default = context.QueryString["is_default"] ?? "0";
            EggsoftWX.Model.tab_User_Address model = new EggsoftWX.Model.tab_User_Address();
            EggsoftWX.BLL.tab_User_Address BLLmodel = new EggsoftWX.BLL.tab_User_Address();

            //Cms.Model.c_user_address model = new Cms.Model.c_user_address();
            model.UserID = userId;
            //model.consignee = consignee;
            model.MobilePhone = cellphone;
            model.PostCode = code;
            model.pc_province = Province;
            model.pc_city = City;
            model.pc_district = District;
            //model.street = this.street.Value;
            model.XiangXiDiZHi = address;
            BLLmodel.Add(model);
            //model.is_default = Convert.ToInt32(is_default);
            //if (id > 0)
            //{
            //    model.id = id;
            //    if (new Cms.BLL.c_user_address().Update(model))
            //    {
            //        string strJson = "{\"status\":0}";
            //        return strJson;
            //        //Response.End();
            //    }
            //    else
            //    {
            string strJson = "{\"status\":1}";
            return strJson;
            //Response.End();
            //}
            //}
            //    else
            //    {

            //        int result = new Cms.BLL.c_user_address().Add(model);
            //        if (result > 0)
            //        {
            //            string strJson = "{\"status\":0}";
            //            return strJson;
            //            //Response.End();
            //        }
            //        else
            //        {
            //            string strJson = "{\"status\":1}";
            //            return strJson;
            //            //Response.End();
            //        }
            //    }

        }
        #endregion

        #region 读取订单信息=======================
        public String getOrderList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            string where = context.QueryString["where"] ?? "0";
            //List<Cms.Model.C_order> list = new Cms.BLL.C_order().GetModelList(where + " user_id=" + userId + "and order_status=0 order by id desc");
            //if (list != null && list.Count > 0)
            //{
            //    List<Dictionary<string, object>> listNew = new List<Dictionary<string, object>>();
            //    foreach (Cms.Model.C_order record in list)
            //    {
            //        Dictionary<string, object> map = new Dictionary<string, object>();
            //        map.Add("id", record.id);
            //        map.Add("user_id", record.user_id);
            //        List<Cms.Model.C_ordersub> listSub = new Cms.BLL.C_ordersub().GetModelList("order_id=" + record.id);
            //        if (listSub != null && listSub.Count > 0)
            //        {
            //            List<Dictionary<string, object>> listSubNew = new List<Dictionary<string, object>>();
            //            foreach (Cms.Model.C_ordersub recordSub in listSub)
            //            {
            //                Dictionary<string, object> mapSub = new Dictionary<string, object>();
            //                mapSub.Add("id", recordSub.id);
            //                mapSub.Add("title", recordSub.title);
            //                mapSub.Add("quantity", recordSub.quantity);
            //                mapSub.Add("price", recordSub.price);
            //                mapSub.Add("property_value", recordSub.property_value);
            //                if (new Cms.BLL.C_product().Exists(Convert.ToInt32(recordSub.article_id)))
            //                {
            //                    mapSub.Add("litpic", new Cms.BLL.C_product().GetModel(Convert.ToInt32(recordSub.article_id)).litpic.ToString());
            //                    mapSub.Add("marketPrice", new Cms.BLL.C_product().GetModel(Convert.ToInt32(recordSub.article_id)).marketPrice);
            //                }
            //                listSubNew.Add(mapSub);
            //            }
            //            map.Add("orderSub", listSubNew);
            //        }
            //        map.Add("order_num", record.order_num);
            //        map.Add("price_sum", record.price_sum);
            //        map.Add("quantity_sum", record.quantity_sum);
            //        map.Add("is_payment", record.is_payment);
            //        map.Add("is_transaction", record.is_transaction);
            //        map.Add("order_status", getOrderStatus(record.id));
            //        map.Add("updateTime", record.updateTime);
            //        listNew.Add(map);
            //    }
            //    string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(listNew);
            //    return strJson;
            //    //Response.End();
            //}
            //else
            //{
            string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(null);
            return (strJson);
            //Response.End();
            //}
        }

        public String getOrder(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            EggsoftWX.Model.tab_Order model = new EggsoftWX.BLL.tab_Order().GetModel(id);
            if (model != null)
            {
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
                return strJson;
                ////Response.End();
            }
            else
            {
                return "";
                ////Response.End();
            }
        }

        public String getOrderSate(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            string result = getOrderStatus(id);
            string strJson = "{\"status\":\"" + result + "\"}";
            return strJson;
            //Response.End();
        }

        public String getOrderSubList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            string where = context.QueryString["where"] ?? "0";
            //List<Cms.Model.C_ordersub> list = new Cms.BLL.C_ordersub().GetModelList("order_id=" + id);
            //if (list != null && list.Count > 0)
            //{
            //    List<Dictionary<string, object>> listSubNew = new List<Dictionary<string, object>>();
            //    foreach (Cms.Model.C_ordersub recordSub in list)
            //    {
            //        Dictionary<string, object> mapSub = new Dictionary<string, object>();
            //        mapSub.Add("id", recordSub.id);
            //        mapSub.Add("title", recordSub.title);
            //        mapSub.Add("quantity", recordSub.quantity);
            //        mapSub.Add("price", recordSub.price);
            //        mapSub.Add("property_value", recordSub.property_value);
            //        if (new Cms.BLL.C_product().Exists(Convert.ToInt32(recordSub.article_id)))
            //        {
            //            mapSub.Add("litpic", new Cms.BLL.C_product().GetModel(Convert.ToInt32(recordSub.article_id)).litpic.ToString());
            //            mapSub.Add("marketPrice", new Cms.BLL.C_product().GetModel(Convert.ToInt32(recordSub.article_id)).marketPrice);
            //        }
            //        listSubNew.Add(mapSub);
            //    }
            string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(null);
            return strJson;
            //Response.End();
            //}
            //    else
            //    {
            //        string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
            //        return strJson;
            //        //Response.End();
            //    }
        }

        public string getOrderStatus(int orderId)
        {
            // int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string result = "";
            EggsoftWX.Model.tab_Order model = new EggsoftWX.BLL.tab_Order().GetModel(orderId);

            //Cms.Model.C_order model = new Cms.BLL.C_order().GetModel(orderId);
            if (model.IsDeleted == true)
            {
                result = "订单取消";
            }
            else
            {
                if (model.PayStatus == 0)
                {
                    result = "待付款";
                }
                else if (model.PayStatus == 1 && string.IsNullOrEmpty(model.DeliveryText))
                {
                    result = "待发货";
                }
                else if (model.PayStatus == 1 && !string.IsNullOrEmpty(model.DeliveryText) && model.isReceipt == false)
                {
                    result = "待收货";
                }
                else if (model.PayStatus == 1 && !string.IsNullOrEmpty(model.DeliveryText) && model.isReceipt == true)
                {
                    result = "待评价";
                }
                else if (model.PayStatus == 1 && !string.IsNullOrEmpty(model.DeliveryText) && model.isReceipt == true)
                {
                    result = "交易完成";
                }
            }
            return result;
        }
        #endregion

        #region 获取订单红点==============================
        public String getOrderRed(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int type = Convert.ToInt32(context.QueryString["type"] ?? "0");
            int count = 0;
            EggsoftWX.BLL.tab_Order my_tab_Order = new EggsoftWX.BLL.tab_Order();

            switch (type)
            {
                case 1:
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=0  and isdeleted<>1");
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 2:
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=1 and isReceipt=0 and isnull(DeliveryText,'')=''  and isdeleted<>1");

                    //count = new Cms.BLL.C_order().GetRecordCount("is_payment=1 and order_status=0 and is_delivery=0 and user_id=" + userId);
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 3:
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=1 and isReceipt=0 and isnull(DeliveryText,'')<>''  and isdeleted<>1");

                    // count = new Cms.BLL.C_order().GetRecordCount("is_payment=1 and order_status=0 and is_delivery=1 and is_receiving=0 and user_id=" + userId);
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 4:
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=1 and isReceipt=1  and isdeleted<>1");
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
            }
            return count.toString();
        }
        #endregion

        #region 微信支付统一下单==============================
        public String GetUnifiedOrderResult(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            //Cms.Model.C_user userModel = new Cms.BLL.C_user().GetModel(userId);
            //JsApiPay jsApiPay = new JsApiPay(this);
            //string openid = userModel.openid.ToString();
            //string total_fee = "0";
            //if (typeId == 1)//购物
            //{
            //    Cms.Model.C_order orderEntity = new Cms.BLL.C_order().GetModel(id);
            //    total_fee = ((int)(Convert.ToDecimal(orderEntity.price_sum) * 100)).ToString();
            //    jsApiPay.openid = openid;
            //    jsApiPay.orderid = orderEntity.order_num;
            //    jsApiPay.productName = "链鲜社区生活馆";
            //    jsApiPay.total_fee = int.Parse(total_fee);
            //}
            //else
            //{//充值
            //    Cms.Model.C_user_recharge orderEntity = new Cms.BLL.C_user_recharge().GetModel(id);
            //    total_fee = ((int)(Convert.ToDecimal(orderEntity.price) * 100)).ToString();
            //    jsApiPay.openid = openid;
            //    jsApiPay.orderid = orderEntity.orderNumber;
            //    jsApiPay.productName = "链鲜社区生活馆";
            //    jsApiPay.total_fee = int.Parse(total_fee);
            //}
            ////JSAPI支付预处理
            //WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
            //string wxJsApiParam = jsApiPay.GetJsApiParameters();
            //Response.Write(wxJsApiParam);
            //Response.End();
            return "";
        }
        #endregion

        #region 获取充值记录=======================
        //public String getUserRechargeList(HttpRequest context)
        //{
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //    int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
        //    int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
        //    int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
        //    int start = (page - 1) * size + 1;
        //    int end = page * size;
        //    DataSet ds = new Cms.BLL.C_user_recharge().GetListByPage("userId=" + userId + " and isPay=1", "id desc", start, end);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        List<Cms.Model.C_user_recharge> list = new Cms.BLL.C_user_recharge().DataTableToList(ds.Tables[0]);
        //        string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
        //        return strJson;
        //        //Response.End();
        //    }
        //    else
        //    {
        //        Response.Write("");
        //        //Response.End();
        //    }
        //}

        //public String addUserRecharge(HttpRequest context)
        //{
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //    int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
        //    Decimal price = Convert.ToDecimal(context.QueryString["price"] ?? "0.1");
        //    Cms.Model.C_user_recharge model = new Cms.Model.C_user_recharge();
        //    model.isPay = 0;
        //    model.userId = userId;
        //    model.price = price;
        //    model.typeId = 1;
        //    model.createdTime = DateTime.Now;
        //    model.note = "微信充值";
        //    model.orderNumber = "95" + Cms.Common.Utils.GetOrderNumber();
        //    int result = new Cms.BLL.C_user_recharge().Add(model);
        //    if (result > 0)
        //    {
        //        string strJson = "{\"status\":" + result + "}";
        //        return strJson;
        //        //Response.End();
        //    }
        //    else
        //    {
        //        string strJson = "{\"status\":0}";
        //        return strJson;
        //        //Response.End();
        //    }
        //}
        #endregion

        #region 获取积分记录=======================
        //public String getUserIntegralList(HttpRequest context)
        //{
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //    int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
        //    int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
        //    int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
        //    int start = (page - 1) * size + 1;
        //    int end = page * size;
        //    DataSet ds = new Cms.BLL.C_user_integral().GetListByPage("userId=" + userId + " ", "id desc", start, end);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        List<Cms.Model.C_user_integral> list = new Cms.BLL.C_user_integral().DataTableToList(ds.Tables[0]);
        //        string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
        //        return strJson;
        //        //Response.End();
        //    }
        //    else
        //    {
        //        return "";
        //        //Response.End();
        //    }
        //}
        #endregion

        #region 获取积分产品列表=======================
        //public String getIntegralProductPageList(HttpRequest context)
        //{
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //    string where = context.QueryString["where"] ?? "";
        //    string orderBy = context.QueryString["orderBy"] ?? "";
        //    int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
        //    int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
        //    int start = (page - 1) * size + 1;
        //    int end = page * size;
        //    DataTable dsRows = new EggsoftWX.BLL.tab_Goods().GetPageDataTable(page, size, "*", where, "sortId ,id desc", false);
        //    if (dsRows != null && dsRows.Rows.Count > 0)
        //    {
        //        IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dsRows);

        //       // List<Cms.Model.C_integral_product> list = new Cms.BLL.C_integral_product().DataTableToList(ds.Tables[0]);
        //        string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
        //        return strJson;
        //        //Response.End();
        //    }
        //    else
        //    {
        //        return "";
        //        //Response.End();
        //    }
        //}
        //public String getIntegralProductInfo(HttpRequest context)
        //{
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //    int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
        //    Cms.Model.C_integral_product model = new Cms.BLL.C_integral_product().GetModel(id);
        //    if (model != null)
        //    {
        //        string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(model);
        //        return strJson;
        //        //Response.End();
        //    }
        //    else
        //    {
        //        return "";
        //        //Response.End();
        //    }
        //}
        #endregion

        #region  DataSet Datatable转换为Json======================


        public static class ConvertJson
        {
            //#region  DataSet转换为Json

            ///// <summary>           
            ///// DataSet转换为Json     
            ///// </summary>       
            ///// <param name="dataSet">DataSet对象</param>
            ///// <returns>Json字符串</returns>  
            //public static string ToJson(DataSet dataSet)
            //{
            //    string jsonString = "{\"status\":0,";
            //    foreach (DataTable table in dataSet.Tables)
            //    {
            //        jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            //    }
            //    jsonString = jsonString.TrimEnd(',');
            //    return jsonString + "}";
            //}
            //#endregion

            #region Datatable转换为Json

            /// <summary>   
            /// Datatable转换为Json 
            /// </summary>      
            /// <param name="table">Datatable对象</param>
            /// <returns>Json字符串</returns>    
            public static string ToJson(DataTable dt)
            {
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("[");
                DataRowCollection drc = dt.Rows;
                for (int i = 0; i < drc.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string strKey = dt.Columns[j].ColumnName;
                        string strValue = drc[i][j].ToString();
                        Type type = dt.Columns[j].DataType;
                        jsonString.Append("\"" + strKey + "\":");
                        strValue = StringFormat(strValue, type);
                        if (j < dt.Columns.Count - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }
                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("]");
                return jsonString.ToString();
            }

            /// <summary>  
            /// DataTable转换为Json 
            /// </summary>    
            public static string ToJson(DataTable dt, string jsonName)
            {
                StringBuilder Json = new StringBuilder();
                if (string.IsNullOrEmpty(jsonName))
                    jsonName = dt.TableName;
                Json.Append("{\"" + jsonName + "\":[");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Type type = dt.Rows[i][j].GetType();
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
                Json.Append("]}");
                return Json.ToString();
            }

            #endregion

            /// <summary>     
            /// 格式化字符型、日期型、布尔型 
            /// </summary>     
            /// <param name="str"></param>   
            /// <param name="type"></param> 
            /// <returns></returns>     
            private static string StringFormat(string str, Type type)
            {
                if (type == typeof(string))
                {
                    str = String2Json(str);
                    str = "\"" + str + "\"";
                }
                else if (type == typeof(DateTime))
                {
                    str = "\"" + str + "\"";
                }
                else if (type == typeof(bool))
                {
                    str = str.ToLower();
                }
                else if (type != typeof(string) && string.IsNullOrEmpty(str))
                {
                    str = "\"" + str + "\"";
                }
                return str;
            }

            #region 私有方法
            /// <summary>     
            /// 过滤特殊字符    
            /// </summary>    
            /// <param name="s">字符串</param> 
            /// <returns>json字符串</returns> 
            private static string String2Json(String s)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < s.Length; i++)
                {
                    char c = s.ToCharArray()[i];
                    switch (c)
                    {
                        case '\"':
                            sb.Append("\\\""); break;
                        case '\\':
                            sb.Append("\\\\"); break;
                        case '/':
                            sb.Append("\\/"); break;
                        case '\b':
                            sb.Append("\\b"); break;
                        case '\f':
                            sb.Append("\\f"); break;
                        case '\n':
                            sb.Append("\\n"); break;
                        case '\r':
                            sb.Append("\\r"); break;
                        case '\t':
                            sb.Append("\\t"); break;
                        default:
                            sb.Append(c); break;
                    }
                }
                return sb.ToString();
            }
            #endregion
        }
        #endregion
    }
}