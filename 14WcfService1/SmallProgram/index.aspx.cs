using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _14WcfService1.SmallProgram
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;

            string strReturnJSON = "";

            try
            {
                string strOopt = context.QueryString["opt"];
                switch (strOopt)
                {
                    case "getListUserIntegralMoney": strReturnJSON = getListUserIntegralMoney(context); break;//会员积分排序
                    case "getQR": strReturnJSON = getQR(context); break;//获取支付条形码  二维码
                    case "getBanner": strReturnJSON = getBanner(context); break;//获取banner 轮播图  http://localhost:8014/SmallProgram/WebSP.asmx/doXWXAction?opt=getBanner&ShopClientID=1&page=1&size=46
                    //case "onLogin": strReturnJSON = onLogin(context); break;//登录  http://localhost:8014/SmallProgram/WebSP.asmx/doXWXAction?opt=onLogin
                    case "getClassSayIcon": strReturnJSON = getClassSayIcon(context); break;//获取分类列表 http://localhost:8014/SmallProgram/index.aspx?opt=getClassSayIcon&ShopClientID=1&page=1&size=46
                    case "getHotProductList": strReturnJSON = getHotProductList(context); break;//获取最热推荐分类列表 http://localhost:8014/SmallProgram/index.aspx?opt=getClassSayIcon&ShopClientID=1&page=1&size=46
                    case "getProductPageByClass1List": strReturnJSON = getProductPageByClass1List(context); break;//获取产品列表  根据分类排列
                    case "getClassID1SmallPic": strReturnJSON = getClassID1SmallPic(context); break;//读取分类
                    //case "addUser": strReturnJSON = addUser(context); break;//创建会员
                    case "updateUser": strReturnJSON = updateUser(context); break;//修改会员信息
                    case "getListUser": strReturnJSON = getListUser(context); break;//分页查询会员
                    case "getListUserCount": strReturnJSON = getListUserCount(context); break;//统计会员
                    case "getUser": strReturnJSON = getUser(context); break;//读取会员信息
                    case "getListUserIntegral": strReturnJSON = getListUserIntegral(context); break;//会员积分排序
                    case "getWebSite": strReturnJSON = getWebSite(context); break;//站点信息
                    case "getColumnList": strReturnJSON = getColumnList(context); break;//栏目列表
                    case "getArticlePage": strReturnJSON = getArticlePage(context); break;//公告 分页查询内容 http://localhost:8014/SmallProgram/WebSP.asmx/doXWXAction?opt=getArticlePage&ShopClientID=1&page=1&size=46
                    case "getArticleInfo": strReturnJSON = getArticleInfo(context); break;//内容详情
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
                    case "getUserRechargeList": strReturnJSON = getUserRechargeList(context); break; //获取充值记录
                    case "getUserIntegralList": strReturnJSON = getUserIntegralList(context); break; //获取积分记录
                    case "addUserRecharge": strReturnJSON = addUserRecharge(context); break;//充值
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
            string ip = "用户IP:" + Request.UserHostAddress;
            Eggsoft.Common.debug_Log.Call_WriteLog(strReturnJSON, "WXSP返回数据" + ip);
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/json";
            //response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            response.Charset = Encoding.UTF8.ToString();///：ContentEncoding 是管字节流到文本的，而 Charset 是管在浏览器中显示的。
            response.Write(strReturnJSON);
            response.End();
        }
        public String getListUserIntegralMoney(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            string UserID = context.QueryString["UserID"].toString();
            string type = context.QueryString["type"].toString();
            //string where = String.IsNullOrEmpty(context.QueryString["where"]) ? (" and ShopClient_ID=" + ShopClientID) : (" and " +context.QueryString["where"] + " and ShopClient_ID = " + ShopClientID);
            //string orderBy = context.QueryString["orderBy"] ?? "";
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;

            string strTableWhere = "";
            if (type == "1")
            {
                // ")
                //{

                strTableWhere = @"(SELECT [ID]
      ,[UserID]
      ,[ShopClient_ID]
      ,CONVERT(varchar(100), [UpdateTime], 111) as UpdateTime
      ,[ConsumeOrRechargeType]
      ,[Bool_ConsumeOrRecharge] 
      ,[ConsumeOrRechargeMoney] as Money
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum]  as RemainingSum
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]
      ,[Creatby],
 CASE Bool_ConsumeOrRecharge
            WHEN 1 THEN '增加'
            WHEN 0 THEN '减少' 
        END AS 'ConsumeOrRecharge'
  FROM [tab_TotalCredits_Consume_Or_Recharge] WHERE [UserID]=@UserID and ShopClient_ID=@ShopClientID) TableWhere";
                }
            else if (type == "2")
            {
                strTableWhere = @"(SELECT [ID]
      ,[UserID]
      ,[ShopClient_ID]
       ,CONVERT(varchar(100), [UpdateTime], 111) as UpdateTime
      ,[ConsumeOrRechargeType]
      ,[ConsumeOrRecharge_Vouchers] as Money
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum_Vouchers] as RemainingSum
      ,[Bool_ConsumeOrRecharge]
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]
      ,[Creatby],
        CASE Bool_ConsumeOrRecharge
            WHEN 1 THEN '增加'
            WHEN 0 THEN '减少' 
        END AS 'ConsumeOrRecharge'
  FROM [tab_Total_Vouchers_Consume_Or_Recharge] WHERE [UserID]=@UserID and ShopClient_ID=@ShopClientID) TableWhere";
            }





            DataTable dsTables = EggsoftWX.SQLServerDAL.DbHelperSQL.GetPageDataTable(page, size, "*", strTableWhere, "", "ID", true, UserID, ShopClientID);



            //DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetPageDataTable(page, size, "*", where, "Sort asc,id ", false);

            //DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetDataTable("600", "*", where + " " + orderBy + "Sort asc,id desc");

            //DataSet ds = new Cms.BLL.C_product().GetListByPage(where, orderBy + "sortId asc,id desc", start, end);
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsTables);
                //IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dsTables);

                ////List<Cms.Model.C_product> list = new Cms.BLL.C_product().DataTableToList(ds.Tables[0]);
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        #region 通过code获取用户的openid=======================
        //public String onLogin(HttpRequest context)
        //{


        //    string code = context.QueryString["code"];
        //    int ShopClientID = context.QueryString["ShopClientID"].toInt32();

        //      if (Str != null)
        //    {
        //        return (Str);
        //        ////Response.End();
        //    }
        //    else
        //    {
        //        return "";
        //        //Response.End();
        //    }
        //}
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

            string strWhere = @"select tab_User.*,maxtab_TotalCredits_Consume_Or_Recharge.RemainingSum,maxtab_Total_Vouchers_Consume_Or_Recharge.RemainingSum_Vouchers from tab_User 
LEFT OUTER JOIN (  select [RemainingSum],[UserID] from [tab_TotalCredits_Consume_Or_Recharge] where ID=(select max(ID) from [tab_TotalCredits_Consume_Or_Recharge] where [UserID]=@ID)) maxtab_TotalCredits_Consume_Or_Recharge
  on maxtab_TotalCredits_Consume_Or_Recharge.UserID=tab_User.ID 
LEFT OUTER JOIN (  select [RemainingSum_Vouchers],[UserID] from [tab_Total_Vouchers_Consume_Or_Recharge] where ID=(select max(ID) from [tab_Total_Vouchers_Consume_Or_Recharge] where [UserID]=@ID)) maxtab_Total_Vouchers_Consume_Or_Recharge
  on maxtab_Total_Vouchers_Consume_Or_Recharge.UserID=tab_User.ID 
where tab_User.ID=@ID";

            System.Data.DataTable DataTableModel_tab_User = BLLtab_User.SelectList(strWhere, userId).Tables[0];

            //Cms.Model.C_user modelUpdate = new Cms.BLL.C_user().GetModel(userId);
            string strJson = Eggsoft.Common.Json2Table.ToJson(DataTableModel_tab_User);
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

        private static object myobjectCodeGetOpenid = new object();
        /// <summary>
        /// 用Code换取Openid
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        private string CodeGetOpenid(string Code, int ShopClientID)
        {
            string ReText = "";
            //向微信服务端 使用登录凭证 code 获取 session_key 和 openid   
            //lock (myobjectCodeGetOpenid)
            //{
            string Str = GetJson("https://api.weixin.qq.com/sns/jscode2session?appid=" + Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "SmallProgram_APPID") + "&secret=" + Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "mallProgram_Secret") + "&js_code=" + Code + "&grant_type=authorization_code");
            ///{"session_key":"A7kap0Bb2y6EqDVCqnX+pw==","openid":"oSTYi0Ttxd9I1N5yoNA3ABO3osGM"}

            var result555 = Str.toJsonDynamicObject();
            ReText = result555.openid;
            if (ReText == "" || ReText == null) ReText = Str;
            //string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "SmallProgram_APPID"), Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.ToString(), "mallProgram_Secret"), Code);

            // ReText = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(url);
            //}

            //string ReText = WebRequestHelper.WebRequestPostOrGet(url, "");//post/get方法获取信息 
            return ReText;
        }


        //public static string DecodeUserInfo(string raw, string signature, string encryptedData, string iv)
        //{

        //    byte[] iv2 = Convert.FromBase64String(iv);

        //    if (string.IsNullOrEmpty(encryptedData)) return "";
        //    Byte[] toEncryptArray = Convert.FromBase64String(encryptedData);

        //    System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
        //    {
        //        Key = Convert.FromBase64String(session_key),
        //        IV = iv2,
        //        Mode = System.Security.Cryptography.CipherMode.CBC,
        //        Padding = System.Security.Cryptography.PaddingMode.PKCS7
        //    };

        //    System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
        //    Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return Encoding.UTF8.GetString(resultArray);

        //}

        /// <summary>
        /// /http://localhost:8014/SmallProgram/index.aspx?opt=updateUser&ShopClientID=1&userInfo=%7B%22type%22%3A%22getuserinfo%22%2C%22timeStamp%22%3A5409%2C%22target%22%3A%7B%22id%22%3A%22%22%2C%22offsetLeft%22%3A0%2C%22offsetTop%22%3A216%2C%22dataset%22%3A%7B%22id%22%3A%221753%22%2C%22unit%22%3A%22%C3%A4%C2%B8%C2%AA%22%7D%7D%2C%22currentTarget%22%3A%7B%22id%22%3A%22%22%2C%22offsetLeft%22%3A0%2C%22offsetTop%22%3A216%2C%22dataset%22%3A%7B%22id%22%3A%221753%22%2C%22unit%22%3A%22%C3%A4%C2%B8%C2%AA%22%7D%7D%2C%22detail%22%3A%7B%22errMsg%22%3A%22getUserInfo%3Aok%22%2C%22rawData%22%3A%22%7B%5C%22nickName%5C%22%3A%5C%22%E5%BC%A0%E5%BB%B7%E9%94%8B%E8%87%AA%E5%8A%A9%5C%22%2C%5C%22gender%5C%22%3A1%2C%5C%22language%5C%22%3A%5C%22zh_CN%5C%22%2C%5C%22city%5C%22%3A%5C%22Minhang%5C%22%2C%5C%22province%5C%22%3A%5C%22Shanghai%5C%22%2C%5C%22country%5C%22%3A%5C%22China%5C%22%2C%5C%22avatarUrl%5C%22%3A%5C%22https%3A%2F%2Fwx.qlogo.cn%2Fmmopen%2Fvi_32%2FQ0j4TwGTfTJUn7Hbn2xgCKibmORHUAOqEBp8JLRIWibHnsfia4SXbcNpRqDxXZuqfqCeK2LWnF7ic2xoLvtIziaOnfA%2F132%5C%22%7D%22%2C%22signature%22%3A%22e5c8f89ec5ede4116d055030dbe3d574562fd895%22%2C%22encryptedData%22%3A%228ZFXpDAji0pi6%2BF%2BPfVHGBdKxJPENsgfdQJxJa0%2B%2FVeXGBbQiQ7WTqmm%2BB46N0%2FJkh6xlSCKzFU4fhl76ukr3OGNRvfigUfv%2FjmRiwDJOobhAP5KQrEg0k3%2Fv1Pve%2BUEQotVYelsfaCoxuZyiW3SCMPo3%2F36168KiQd3D9mSqeB7frw5QclSKBzUfelMG1cqJhD9aAYpMNbigP9Z7qrQH7FLLl6%2Bw3yXdonbT9dWo2rmr47JZTZ%2BryzXkiN35ROZuaKT2qQEr82rtifuPcWKRBnaioLjdp9nZ5N%2BrCB57ZJsT6xs4oESu3VhUliZdaVjXOcprrRoTV6%2B2OiGOOD%2B4CrnM%2BB6hZhLt4v4ZYAybuMWiO6XKmhrGAYhvhx%2B6UZzlPrpZnT7KqzuNaCalVZt0cum%2F9Vb4Kg9h%2Fkglsf5sxO8oWrcoiOL%2Bc6M8TiN5aGUPd3fcYPCnYv8PmRJB0xCOBHCutqYgxIUtQSThcuJ8qc%3D%22%2C%22iv%22%3A%22cw1tv6rd9%2By7KlWfP%2FGjhw%3D%3D%22%2C%22userInfo%22%3A%7B%22nickName%22%3A%22%E5%BC%A0%E5%BB%B7%E9%94%8B%E8%87%AA%E5%8A%A9%22%2C%22gender%22%3A1%2C%22language%22%3A%22zh_CN%22%2C%22city%22%3A%22Minhang%22%2C%22province%22%3A%22Shanghai%22%2C%22country%22%3A%22China%22%2C%22avatarUrl%22%3A%22https%3A%2F%2Fwx.qlogo.cn%2Fmmopen%2Fvi_32%2FQ0j4TwGTfTJUn7Hbn2xgCKibmORHUAOqEBp8JLRIWibHnsfia4SXbcNpRqDxXZuqfqCeK2LWnF7ic2xoLvtIziaOnfA%2F132%22%7D%7D%7D&code=081gaiqK078FA82Sl3qK0y74qK0gaiqT
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String updateUser(HttpRequest context)
        {
            string strJson = "{\"state\":1}";
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            string userInfo = context.QueryString["userInfo"] ?? "";
            //{"type":"getuserinfo","timeStamp":3494,"target":{"id":"","offsetLeft":0,"offsetTop":216,"dataset":{"id":"1533","unit":"å°"}},"currentTarget":{"id":"","offsetLeft":0,"offsetTop":216,"dataset":{"id":"1533","unit":"å°"}},"detail":{"errMsg":"getUserInfo:ok","rawData":"{\"nickName\":\"张廷锋自助\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"Minhang\",\"province\":\"Shanghai\",\"country\":\"China\",\"avatarUrl\":\"https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJUn7Hbn2xgCKibmORHUAOqEBp8JLRIWibHnsfia4SXbcNpRqDxXZuqfqCeK2LWnF7ic2xoLvtIziaOnfA/132\"}","signature":"79ce080756d181c4751464e55e3ea90b744d71cc","encryptedData":"s8fVX+MG/ISD1+4D3bTt7lAayFNFVE6ZYI287AegV01DvhTxZODLFf2Zq5VLqkZRNg5jJtOUJ4i0wuxOb8TSc/ayp1btjpYbTuTfpX2B8UT8bpZwtgGt20DcBZj5bRdrdBPQK41V5CaWga26+C+N+9Vp6YsFgp4LuWvWga9P4ykiDzqmRNGke1IQ/1KljeBZTSGSGNofFg0PyofJPs2yEY/jUTi21AILdqjyFYb6zAkL8ndgEEBCrTdDYKiUwPAGktxhSwnWu/LB2RfFmdnIhADyETie9iPsMSobplfxrJJlhewg47+D59MPbHxtm8IO0aqts6bMXOPaGfFJzHhXHKHQBHEZuXQSXdUWBhl3kqMmeuwaA3a+o1K1mNQhS1r9HyxNELGAzWMep6FdY7FADy6HFQF1AFr9QE3YRu6Ue9noMusZJQ8htAaRkSUO1fOpDbSQkaHQKrbd4XY0uMYushL/E+Pbo1RzeQ6zopbN2D8=","iv":"Qq0KTFLGtpr6JJF9G+UF2Q==","userInfo":{"nickName":"张廷锋自助","gender":1,"language":"zh_CN","city":"Minhang","province":"Shanghai","country":"China","avatarUrl":"https://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJUn7Hbn2xgCKibmORHUAOqEBp8JLRIWibHnsfia4SXbcNpRqDxXZuqfqCeK2LWnF7ic2xoLvtIziaOnfA/132"}}}
            var varuserInfo = userInfo.toJsonDynamicObject();

            string code = context.QueryString["code"] ?? "";
            if (String.IsNullOrEmpty(code)) return "{\"state\":-1}";
            var varnickName = varuserInfo.detail.userInfo.nickName;
            var varavatarUrl = varuserInfo.detail.userInfo.avatarUrl;

            string strOpenID = CodeGetOpenid(code, ShopClientID);

            if (String.IsNullOrEmpty(strOpenID) || strOpenID.Contains("errcode"))
            {
                strJson = "{\"state\":-1,\"Err\":\"读取openID失败\",\"ErrData\":\"" + strOpenID + "\"}";

            }
            else
            {

                EggsoftWX.BLL.tab_User BLLtab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User modelUpdate = BLLtab_User.GetModel("[SmallProgramOpenID]=@SmallProgramOpenID and [ShopClientID]=@ShopClientID", strOpenID, ShopClientID);
                if (modelUpdate == null)
                {
                    modelUpdate = new EggsoftWX.Model.tab_User();
                    modelUpdate.ShopClientID = ShopClientID;
                    modelUpdate.SmallProgramOpenID = strOpenID;
                    modelUpdate.SocialPlatform = "SmallProgram";
                    modelUpdate.HeadImageUrl = varavatarUrl;
                    modelUpdate.NickName = varnickName;
                    modelUpdate.Country = varuserInfo.detail.userInfo.country;
                    modelUpdate.City = varuserInfo.detail.userInfo.city;
                    modelUpdate.Sheng = varuserInfo.detail.userInfo.province;
                    modelUpdate.Sex = varuserInfo.detail.userInfo.gender;
                    BLLtab_User.Add(modelUpdate);
                    modelUpdate = BLLtab_User.GetModel("[SmallProgramOpenID]=@SmallProgramOpenID and [ShopClientID]=@ShopClientID", strOpenID, ShopClientID);
                }
                else
                {
                    modelUpdate.HeadImageUrl = varavatarUrl;
                    modelUpdate.NickName = varnickName;
                    modelUpdate.Country = varuserInfo.detail.userInfo.country;
                    modelUpdate.City = varuserInfo.detail.userInfo.city;
                    modelUpdate.Sheng = varuserInfo.detail.userInfo.province;
                    modelUpdate.Sex = varuserInfo.detail.userInfo.gender;
                    modelUpdate.Updatetime = DateTime.Now;
                    modelUpdate.UpdateBy = "更新";
                    BLLtab_User.Update(modelUpdate);
                }
                string strJsondddd = Eggsoft.Common.JsonHelper.LocalSerialize(modelUpdate);

                strJson = strJsondddd;
            }


            //modelUpdate.ContactPhone = mobile;
            //int intuserUser_AddressID = 0;
            //EggsoftWX.BLL.tab_User_Address BLLtab_User_Address = new EggsoftWX.BLL.tab_User_Address();
            //var intExistsModel = BLLtab_User_Address.GetModel("UserID=@UserID", userId);
            //if (intExistsModel != null)
            //{
            //    EggsoftWX.Model.tab_User_Address Model_tab_User_Address = new EggsoftWX.Model.tab_User_Address();
            //    Model_tab_User_Address.MobilePhone = mobile;
            //    Model_tab_User_Address.XiangXiDiZHi = address;
            //    Model_tab_User_Address.UserID = userId;
            //    Model_tab_User_Address.CreatTime = DateTime.Now;
            //    intuserUser_AddressID = BLLtab_User_Address.Add(Model_tab_User_Address);
            //}
            //else
            //{
            //    intuserUser_AddressID = intExistsModel.ID;
            //}
            //modelUpdate.Default_Address = intuserUser_AddressID;
            //modelUpdate.Updatetime = DateTime.Now;
            //modelUpdate.UpdateBy = "小程序更新地址";
            //BLLtab_User.Update(modelUpdate);

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


        #region  获取会员的二维码路径  条形码===========================
        public String getQR(HttpRequest context)
        {


            int intUserID = context.QueryString["UserID"].toInt32();
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            String StringType = context.QueryString["type"].toString();
            DataTable dsTables = null;
            //int adtype = Convert.ToInt32(context.QueryString["typeId"] ?? "0");
            if (StringType == "0")////2个  都要  条形码  二维码
            {
                //创建一个名为"Table_New"的空表
                dsTables = new DataTable("Table_New");
                dsTables.Columns.Add("BarCodeImages", typeof(String));
                dsTables.Columns.Add("QRCodeImages", typeof(String));
                dsTables.Columns.Add("MemberNo", typeof(String));

                DataRow dr = dsTables.NewRow();
                dsTables.Rows.Add(dr);
                dr["BarCodeImages"] = DateTime.Now.ToString(); //通过名称赋值
                dr["QRCodeImages"] = DateTime.Now.ToString(); //通过名称赋值
                                                              // public static String Pub_Take__Path(int UserID, int intShopClientID, out string stringHttp)

                #region 二维码
                int intGetMyShopUserID = Eggsoft_Public_CL.Pub.GetMyShopUserID(intUserID.toString());

                ///http://localhost:8014/SmallProgram/index.aspx?UserID=55182&opt=getQR&type=0&ShopClientID=26
                string strCardmemberNum = Eggsoft.Common.StringNum.Add000000Num(ShopClientID, 4) + Eggsoft.Common.StringNum.Add000000Num(intGetMyShopUserID, 8);
                strCardmemberNum = Eggsoft.Common.StringNum.getBareanCode(strCardmemberNum);

                string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/Pub_Take_Goods_Path.asmx";
                string[] args = new string[3];
                args[0] = intUserID.toString();// "/UpLoad/images/";
                args[1] = ShopClientID.toString();// "/UpLoad/images/";
                args[2] = strCardmemberNum;
                object resultQRCodeImages = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "Pub_Take__Path", args);
                string strQRCodeImagesresult = resultQRCodeImages.toString();
                dr["QRCodeImages"] = strQRCodeImagesresult; //通过名称赋值
                #endregion 二维码

                #region 二维码
                ///http://localhost:8014/SmallProgram/index.aspx?UserID=55182&opt=getQR&type=0&ShopClientID=26
                object resultBarCode = WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "Pub_Take__Path_BarCode", args);
                string strBarCodesresult = resultBarCode.toString();
                dr["BarCodeImages"] = strBarCodesresult; //通过名称赋值
                #endregion 二维码

                dr["MemberNo"] = strCardmemberNum; //通过名称赋值


                string strJson = Eggsoft.Common.Json2Table.ToJson(dsTables);
                //List<Cms.Model.C_ad> list = new Cms.BLL.C_ad().DataTableToList(ds.Tables[0]);
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
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
            String StringType = context.QueryString["type"].toString();
            DataTable dsTables = null;
            //int adtype = Convert.ToInt32(context.QueryString["typeId"] ?? "0");
            if (StringType == "cardMember")
            {
                dsTables = new EggsoftWX.BLL.b080_Shop_cardMemberPic().GetDataTable("60", "PicUrl,ShowText,Arg1 as CDescription,Arg2 as EDescription", " and [Type]=1 and UserID=" + ShopClientID + " order by Pos asc,id desc");
            }
            else
            {
                dsTables = new EggsoftWX.BLL.tab_AnnouncePic().GetDataTable("60", "*", "UserID=" + ShopClientID + " order by Pos asc,id desc");
            }
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                //IList<EggsoftWX.Model.tab_AnnouncePic> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_AnnouncePic>(dsTables);
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsTables);
                //List<Cms.Model.C_ad> list = new Cms.BLL.C_ad().DataTableToList(ds.Tables[0]);
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
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

        /// <summary>
        /// 读取分类图片 http://localhost:8014/SmallProgram/index.aspx?opt=getClassSayIcon&ShopClientID=1&page=1&size=46
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String getClassSayIcon(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            //string where = context.QueryString["where"] ?? "";
            //string orderBy = context.QueryString["orderBy"] ?? "";
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;
            DataTable dsTables = new EggsoftWX.BLL.tab_Class1().GetDataTable("600", "*", " ShopClientID=@ShopClientID order by Sort asc,id desc", ShopClientID);

            //DataSet ds = new Cms.BLL.C_product().GetListByPage(where, orderBy + "sortId asc,id desc", start, end);
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                IList<EggsoftWX.Model.tab_Class1> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Class1>(dsTables);

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
        /// <summary>
        /// http://localhost:8014/SmallProgram/index.aspx?opt=getHotProductList&ShopClientID=1   最热商品
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String getHotProductList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            int size = Convert.ToInt32(context.QueryString["size"] ?? "5");

            string strWhere = @"SELECT top (@size)  tab_Goods.ID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, tab_Goods.Class2_ID, 
                tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, tab_Goods.Icon, 
                 tab_Goods.KuCunCount, tab_Goods.Unit, tab_Goods.kg, 
                tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, tab_Goods.IsCommend, 
                tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, tab_Goods.UpdateTime, 
                tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, tab_Goods.Good_Class, 
                 tab_Goods.LimitTimerBuy_StartTime, tab_Goods.LimitTimerBuy_EndTime, 
                tab_Goods.LimitTimerBuy_TimePrice, tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, 
                tab_Goods.MaxOrderNum, tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, 
                tab_Goods.IS_Admin_check, tab_Goods.CheckBox_WeiBai_RedMoney, 
                tab_Goods.Webuy8_DistributionMoney_Value, tab_Goods.FreightTemplate_ID, tab_Goods.XML, 
                tab_Goods.AgentPercent, tab_Goods.Shopping_Vouchers_Percent, tab_Goods.WealthMoney, 
                tab_Goods.CreatTime, tab_Goods.Send_Vouchers_IfBuy, tab_Goods.Send_Money_IfBuy, 
                V6.SalesCount AS SalesCount, tab_Class1.ClassName, tab_Class1.Sort AS Class1Sort,tab_Goods_Unit.Unit as UnitName
FROM      tab_Goods LEFT OUTER JOIN tab_Class1 ON tab_Goods.Class1_ID = tab_Class1.ID 
                    LEFT OUTER JOIN tab_Goods_Unit ON tab_Goods.Unit = tab_Goods_Unit.ID 
                    LEFT OUTER JOIN
                    (SELECT   GoodID, COUNT(OrderCount) AS SalesCount
                     FROM      (SELECT   tab_Order.PayStatus, tab_Orderdetails.GoodID, tab_Orderdetails.OrderCount, 
                                                      tab_Orderdetails.ShopClient_ID
                                      FROM      tab_Order RIGHT OUTER JOIN
                                                      tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND
                                                       tab_Order.ID = tab_Orderdetails.OrderID
                                      WHERE   (tab_Order.PayStatus = 1) AND (tab_Orderdetails.ShopClient_ID = @ShopClient_ID)) AS V5
                     GROUP BY GoodID) AS V6 ON tab_Goods.ID = V6.GoodID where tab_Goods.IsDeleted=0 and tab_Goods.ShopClient_ID=@ShopClient_ID and isSaled=1 order by V6.SalesCount desc";

            DataSet myDataSet = new EggsoftWX.BLL.tab_Goods().SelectList(strWhere, size, ShopClientID);

            //string where = context.QueryString["where"] ?? "";
            //string orderBy = context.QueryString["orderBy"] ?? "";
            //int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            //int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            //int start = (page - 1) * size + 1;
            //int end = page * size;
            //DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetDataTable("600", "*", where + " " + orderBy + "Sort asc,id desc");

            //DataSet ds = new Cms.BLL.C_product().GetListByPage(where, orderBy + "sortId asc,id desc", start, end);
            if (myDataSet != null && myDataSet.Tables[0] != null && myDataSet.Tables[0].Rows.Count > 0)
            {
                //IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(myDataSet.Tables[0]);

                //List<Cms.Model.C_product> list = new Cms.BLL.C_product().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.Json2Table.ToJson(myDataSet.Tables[0]);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }


        public class Modeltab_Class1AddObject : EggsoftWX.Model.tab_Class1
        {
            public Object AddObject { get; set; }
        }

        //获取产品列表  根据分类排列
        /// <summary>
        /// 获取产品列表  根据分类排列   http://localhost:8014/SmallProgram/index.aspx?opt=getProductPageByClass1List&ShopClientID=1
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String getProductPageByClass1List(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            DataTable dsTables = new EggsoftWX.BLL.tab_Class1().GetDataTable("600", "*", " ShopClientID=@ShopClientID order by Sort asc,id desc", ShopClientID);

            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                IList<Modeltab_Class1AddObject> list = DotConvert.DataTableToList<Modeltab_Class1AddObject>(dsTables);


                for (int i = 0; i < list.Count; i++)
                {
                    string strWhere = @"SELECT top 10 tab_Goods.ID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, tab_Goods.Class2_ID, 
                tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, tab_Goods.Icon, 
                 tab_Goods.KuCunCount, tab_Goods.Unit, tab_Goods.kg, 
                tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, tab_Goods.IsCommend, 
                tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, tab_Goods.UpdateTime, 
                tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, tab_Goods.Good_Class, 
                 tab_Goods.LimitTimerBuy_StartTime, tab_Goods.LimitTimerBuy_EndTime, 
                tab_Goods.LimitTimerBuy_TimePrice, tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, 
                tab_Goods.MaxOrderNum, tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, 
                tab_Goods.IS_Admin_check, tab_Goods.CheckBox_WeiBai_RedMoney, 
                tab_Goods.Webuy8_DistributionMoney_Value, tab_Goods.FreightTemplate_ID, tab_Goods.XML, 
                tab_Goods.AgentPercent, tab_Goods.Shopping_Vouchers_Percent, tab_Goods.WealthMoney, 
                tab_Goods.CreatTime, tab_Goods.Send_Vouchers_IfBuy, tab_Goods.Send_Money_IfBuy, 
                V6.SalesCount AS SalesCount, tab_Class1.ClassName, tab_Class1.Sort AS Class1Sort,tab_Goods_Unit.Unit as UnitName
FROM      tab_Goods LEFT OUTER JOIN tab_Class1 ON tab_Goods.Class1_ID = tab_Class1.ID 
                    LEFT OUTER JOIN tab_Goods_Unit ON tab_Goods.Unit = tab_Goods_Unit.ID 
                    LEFT OUTER JOIN
                    (SELECT   GoodID, COUNT(OrderCount) AS SalesCount
                     FROM      (SELECT   tab_Order.PayStatus, tab_Orderdetails.GoodID, tab_Orderdetails.OrderCount, 
                                                      tab_Orderdetails.ShopClient_ID
                                      FROM      tab_Order RIGHT OUTER JOIN
                                                      tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND
                                                       tab_Order.ID = tab_Orderdetails.OrderID
                                      WHERE   (tab_Order.PayStatus = 1) AND (tab_Orderdetails.ShopClient_ID = @ShopClient_ID)) AS V5
                     GROUP BY GoodID) AS V6 ON tab_Goods.ID = V6.GoodID where tab_Goods.IsDeleted=0 and tab_Goods.Class1_ID=@Class1_ID and tab_Goods.ShopClient_ID=@ShopClient_ID and isSaled=1 order by tab_Goods.Sort desc,tab_Goods.updateTime desc";

                    DataSet myDataSet = new EggsoftWX.BLL.tab_Goods().SelectList(strWhere, ShopClientID, list[i].ID);

                    if (myDataSet != null && myDataSet.Tables[0] != null && myDataSet.Tables[0].Rows.Count > 0)
                    {
                        list[i].AddObject = Eggsoft.Common.Json2Table.ToJson(myDataSet.Tables[0]);
                    }
                }
                string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        public String getProductPageList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            string SearchName = context.QueryString["SearchName"].toString();
            //string where = String.IsNullOrEmpty(context.QueryString["where"]) ? (" and ShopClient_ID=" + ShopClientID) : (" and " +context.QueryString["where"] + " and ShopClient_ID = " + ShopClientID);
            string orderBy = context.QueryString["orderBy"] ?? "";
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;

            string strTableWhere = @"(SELECT top 200  tab_Goods.ID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, tab_Goods.Class2_ID, 
                tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, tab_Goods.Icon, 
                 tab_Goods.KuCunCount, tab_Goods.Unit, tab_Goods.kg, 
                tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, tab_Goods.IsCommend, 
                tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, tab_Goods.UpdateTime, 
                tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, tab_Goods.Good_Class, 
                 tab_Goods.LimitTimerBuy_StartTime, tab_Goods.LimitTimerBuy_EndTime, 
                tab_Goods.LimitTimerBuy_TimePrice, tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, 
                tab_Goods.MaxOrderNum, tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, 
                tab_Goods.IS_Admin_check, tab_Goods.CheckBox_WeiBai_RedMoney, 
                tab_Goods.Webuy8_DistributionMoney_Value, tab_Goods.FreightTemplate_ID, tab_Goods.XML, 
                tab_Goods.AgentPercent, tab_Goods.Shopping_Vouchers_Percent, tab_Goods.WealthMoney, 
                tab_Goods.CreatTime, tab_Goods.Send_Vouchers_IfBuy, tab_Goods.Send_Money_IfBuy, 
                V6.SalesCount AS SalesCount, tab_Class1.ClassName, tab_Class1.Sort AS Class1Sort,tab_Goods_Unit.Unit as UnitName
FROM      tab_Goods LEFT OUTER JOIN tab_Class1 ON tab_Goods.Class1_ID = tab_Class1.ID 
                    LEFT OUTER JOIN tab_Goods_Unit ON tab_Goods.Unit = tab_Goods_Unit.ID 
                    LEFT OUTER JOIN
                    (SELECT   GoodID, COUNT(OrderCount) AS SalesCount
                     FROM      (SELECT   tab_Order.PayStatus, tab_Orderdetails.GoodID, tab_Orderdetails.OrderCount, 
                                                      tab_Orderdetails.ShopClient_ID
                                      FROM      tab_Order RIGHT OUTER JOIN
                                                      tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND
                                                       tab_Order.ID = tab_Orderdetails.OrderID
                                      WHERE   (tab_Order.PayStatus = 1) AND (tab_Orderdetails.ShopClient_ID = @ShopClient_ID)) AS V5
                     GROUP BY GoodID) AS V6 ON tab_Goods.ID = V6.GoodID where tab_Goods.IsDeleted=0 and tab_Goods.ShopClient_ID=@ShopClient_ID and tab_Goods.Name like '%'+@Name+'%' and tab_Goods.isSaled=1 ###WhereClassID1### order by V6.SalesCount desc)UUUUUUUTable";

            #region 是否搜索一级分类
            string strwhereClassID1 = context.QueryString["whereClassID1"].toString();
            if (string.IsNullOrEmpty(strwhereClassID1))
            {
                strwhereClassID1 = "";
            }
            else
            {
                strwhereClassID1 = " and tab_Goods.Class1_ID=" + strwhereClassID1;
            }
            strTableWhere = strTableWhere.Replace("###WhereClassID1###", strwhereClassID1);
            #endregion 是否搜索一级分类


            DataTable dsTables = EggsoftWX.SQLServerDAL.DbHelperSQL.GetPageDataTable(page, size, "*", strTableWhere, "", orderBy, true, ShopClientID, SearchName, strwhereClassID1);



            //DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetPageDataTable(page, size, "*", where, "Sort asc,id ", false);

            //DataTable dsTables = new EggsoftWX.BLL.tab_Goods().GetDataTable("600", "*", where + " " + orderBy + "Sort asc,id desc");

            //DataSet ds = new Cms.BLL.C_product().GetListByPage(where, orderBy + "sortId asc,id desc", start, end);
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsTables);
                //IList<EggsoftWX.Model.tab_Goods> list = DotConvert.DataTableToList<EggsoftWX.Model.tab_Goods>(dsTables);

                ////List<Cms.Model.C_product> list = new Cms.BLL.C_product().DataTableToList(ds.Tables[0]);
                //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(list);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        #region 读取分类小图片================
        public String getClassID1SmallPic(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            DataTable dsTables = new EggsoftWX.BLL.tab_Class1().GetDataTable("600", "*", " ShopClientID=@ShopClientID  order by Sort desc,id desc", ShopClientID);
            if (dsTables != null && dsTables.Rows.Count > 0)
            {
                string strJson = Eggsoft.Common.Json2Table.ToJson(dsTables);
                return strJson;
            }
            else
            {
                return "";

            }

        }
        #endregion 读取分类小图片================

        public String getProductInfo(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");

            //int ShopClientID = context.QueryString["ShopClientID"].toInt32();
            int size = Convert.ToInt32(context.QueryString["size"] ?? "5");

            string strWhere = @"SELECT top 1  tab_Goods.ID, tab_Goods.ShopClient_ID, tab_Goods.Class1_ID, tab_Goods.Class2_ID, 
                tab_Goods.Class3_ID, tab_Goods.isSaled, tab_Goods.Name, tab_Goods.Icon, 
                 tab_Goods.KuCunCount, tab_Goods.Unit, tab_Goods.kg,tab_Goods.[ShortInfo]
      ,tab_Goods.[LongInfo], 
                tab_Goods.Price, tab_Goods.MemberPrice, tab_Goods.PromotePrice, tab_Goods.IsCommend, 
                tab_Goods.HitCount, tab_Goods.PromoteCount, tab_Goods.UpTime, tab_Goods.UpdateTime, 
                tab_Goods.ContactMan, tab_Goods.Sort, tab_Goods.IsDeleted, tab_Goods.Good_Class, 
                 tab_Goods.LimitTimerBuy_StartTime, tab_Goods.LimitTimerBuy_EndTime, 
                tab_Goods.LimitTimerBuy_TimePrice, tab_Goods.LimitTimerBuy_Bool, tab_Goods.MinOrderNum, 
                tab_Goods.MaxOrderNum, tab_Goods.LimitTimerBuy_MaxSalesCount, tab_Goods.Shopping_Vouchers, 
                tab_Goods.IS_Admin_check, tab_Goods.CheckBox_WeiBai_RedMoney, 
                tab_Goods.Webuy8_DistributionMoney_Value, tab_Goods.FreightTemplate_ID, tab_Goods.XML, 
                tab_Goods.AgentPercent, tab_Goods.Shopping_Vouchers_Percent, tab_Goods.WealthMoney, 
                tab_Goods.CreatTime, tab_Goods.Send_Vouchers_IfBuy, tab_Goods.Send_Money_IfBuy, 
                V6.SalesCount AS SalesCount, tab_Class1.ClassName, tab_Class1.Sort AS Class1Sort,tab_Goods_Unit.Unit as UnitName
FROM      tab_Goods LEFT OUTER JOIN tab_Class1 ON tab_Goods.Class1_ID = tab_Class1.ID 
                    LEFT OUTER JOIN tab_Goods_Unit ON tab_Goods.Unit = tab_Goods_Unit.ID 
                    LEFT OUTER JOIN
                    (SELECT   GoodID, COUNT(OrderCount) AS SalesCount
                     FROM      (SELECT   tab_Order.PayStatus, tab_Orderdetails.GoodID, tab_Orderdetails.OrderCount, 
                                                      tab_Orderdetails.ShopClient_ID
                                      FROM      tab_Order RIGHT OUTER JOIN
                                                      tab_Orderdetails ON tab_Order.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND
                                                       tab_Order.ID = tab_Orderdetails.OrderID
                                      WHERE   (tab_Order.PayStatus = 1) AND (tab_Orderdetails.ShopClient_ID = @ShopClient_ID)) AS V5
                     GROUP BY GoodID) AS V6 ON tab_Goods.ID = V6.GoodID where tab_Goods.ID=@tab_GoodsID and tab_Goods.ShopClient_ID=@ShopClient_ID and isSaled=1 order by V6.SalesCount desc";

            DataSet myDataSet = new EggsoftWX.BLL.tab_Goods().SelectList(strWhere, ShopClientID, id);

            if (myDataSet != null && myDataSet.Tables[0] != null && myDataSet.Tables[0].Rows.Count > 0)
            {
                string strJson = Eggsoft.Common.Json2Table.ToJson(myDataSet.Tables[0]);
                return strJson;
            }
            else
            {
                return "";
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

            int intErrorCode = 0;

            //string unit = context.QueryString["unit"] ?? "";
            //int isChecked = Convert.ToInt32(context.QueryString["isChecked"] ?? "1");
            //int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            string str = "";
            try
            {
                int ShopClientID = context.QueryString["ShopClientID"].toInt32();

                String strGoodID = Convert.ToInt32(context.QueryString["id"] ?? "0").toString();
                String strUserID = Convert.ToInt32(context.QueryString["userId"] ?? "0").toString();
                String strbuyCount = Convert.ToInt32(context.QueryString["quantity"] ?? "0").toString();
                //    String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["UserID"]);
                //    String strGoodID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["GoodID"]);
                //String strParentID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ParentID"]);
                //String strbuyCount = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["buyCount"]);
                String MultiBuyType = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["MultiBuyType"]);////商品种类
                                                                                                              //String strTSign = (context.QueryString["TSign"]);

                //#region 检查签名
                //string strSafeCode = "";
                //EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                //if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                //string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strGoodID + strbuyCount + MultiBuyType + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode));
                //if (strTSign != strNetSign)
                //{
                //    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID+ strGoodID+ strParentID+ strbuyCount MultiBuyType" + strUserID + " " + strGoodID + " " + strbuyCount + " " + MultiBuyType + " " + strSafeCode, "添加购物车签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                //    return "";
                //}
                //#endregion 检查签名






                if (strGoodID == null) { intErrorCode = 6; };
                int pIntUserID = Convert.ToInt32(strUserID);
                int pIntGoodID = Convert.ToInt32(strGoodID);



                string[] strMoney_List = new string[] { "0" };
                #region 处理财富积分
                string[] strWealth_List = new string[] { "0" };

                #endregion

                string[] strNumber_Vouchers_Bean_List = new string[] { "0" };
                intErrorCode = Eggsoft_Public_CL.ShoppingCart.AddToShoppingCart(pIntUserID, pIntGoodID, Int32.Parse(strbuyCount), MultiBuyType.toInt32(), strMoney_List, strWealth_List, strNumber_Vouchers_Bean_List, 0, 0, 0, "");



            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "服务添加购物车");
            }
            finally
            {

            }




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
            string strJson = "{\"status\":" + intErrorCode + "}";
            return strJson;
            //Response.End();
            //}
        }

        #region 购物车 是否选择


        public class ClassgetCartList
        {
            public bool is_checked { get; set; }
            public string Send_Vouchers_IfBuy { get; set; }

            public string ShortInfo { get; set; }
            public string Icon { get; set; }
            public string Name { get; set; }
            public string ID { get; set; }
            public string UserID { get; set; }
            public string GoodID { get; set; }
            public string GoodIDCount { get; set; }
            public string MultiBuyType { get; set; }
            public string VouchersNum_List { get; set; }
            public string Beans { get; set; }
            public string MoneyCredits { get; set; }
            public string MoneyWeBuy8Credits { get; set; }
            public string WealthMoney { get; set; }
            public string ShopClientID { get; set; }
            public string GoodType { get; set; }
            public string GoodTypeId { get; set; }
            public string GoodTypeIdBuyInfo { get; set; }
            public string UserSay { get; set; }
            public string CreatTime { get; set; }
            public string CreateBy { get; set; }
            public string UpdateTime { get; set; }
            public string UpdateBy { get; set; }
            public string IsDeleted { get; set; }
            public string IsDeletedTime { get; set; }
            public string MemberPrice { get; set; }
            public string PromotePrice { get; set; }
            public string Price { get; set; }
            public string Unit { get; set; }
        }


        public String getCartList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            string where = context.QueryString["where"] ?? "";


            string strWhere = @"SELECT   TOP (100) PERCENT tab_Goods.ShortInfo, tab_Goods.Icon, tab_Goods.Name, 
                tab_Order_ShopingCart.ID, tab_Order_ShopingCart.UserID,tab_Order_ShopingCart.checkChoice as is_checked, tab_Order_ShopingCart.GoodID, 
                tab_Order_ShopingCart.GoodIDCount, tab_Order_ShopingCart.MultiBuyType, 
                tab_Order_ShopingCart.VouchersNum_List, tab_Order_ShopingCart.Beans, 
                tab_Order_ShopingCart.MoneyCredits, tab_Order_ShopingCart.MoneyWeBuy8Credits, 
                tab_Order_ShopingCart.WealthMoney, tab_Order_ShopingCart.ShopClientID, 
                tab_Order_ShopingCart.GoodType, tab_Order_ShopingCart.GoodTypeId, 
                tab_Order_ShopingCart.GoodTypeIdBuyInfo, tab_Order_ShopingCart.UserSay, 
                tab_Order_ShopingCart.CreatTime, tab_Order_ShopingCart.CreateBy, tab_Order_ShopingCart.UpdateTime, 
                tab_Order_ShopingCart.UpdateBy, tab_Order_ShopingCart.IsDeleted, 
                tab_Order_ShopingCart.IsDeletedTime, tab_Goods.MemberPrice, tab_Goods.PromotePrice, tab_Goods.Send_Vouchers_IfBuy,
                tab_Goods.Price, tab_Goods_Unit.Unit AS Unit
FROM      tab_Goods_Unit LEFT OUTER JOIN
                tab_Goods ON tab_Goods_Unit.ID = tab_Goods.Unit RIGHT OUTER JOIN
                tab_Order_ShopingCart ON tab_Goods.ShopClient_ID = tab_Order_ShopingCart.ShopClientID AND 
                tab_Goods.ID = tab_Order_ShopingCart.GoodID where tab_Order_ShopingCart.UserID=@UserID and tab_Order_ShopingCart.ShopClientID=@ShopClientID and tab_Order_ShopingCart.IsDeleted<>1  
ORDER BY tab_Order_ShopingCart.ID DESC";
            EggsoftWX.BLL.tab_Order_ShopingCart EggsoftWXBLLtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            System.Data.DataTable dt_DataTable_ShopingCart = EggsoftWXBLLtab_Order_ShopingCart.SelectList(strWhere, userId, ShopClientID).Tables[0];
            //List<Cms.Model.C_user_cart> list = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and typeId=" + typeId + where);
            if (dt_DataTable_ShopingCart != null && dt_DataTable_ShopingCart.Rows.Count > 0)
            {

                string strJson = Eggsoft.Common.Json2Table.ToJson(dt_DataTable_ShopingCart);
                List<ClassgetCartList> listNew = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassgetCartList>>(strJson);

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
                return listNew.toJsonString();
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }
        #endregion 购物车 是否选择
        #region 选择============================
        public String choice(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");
            int checkId = context.QueryString["checkId"].toInt32();


            EggsoftWX.BLL.tab_Order_ShopingCart EggsoftWXBLLtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
            EggsoftWX.Model.tab_Order_ShopingCart Modeltab_Order_ShopingCart = EggsoftWXBLLtab_Order_ShopingCart.GetModel(id);
            Modeltab_Order_ShopingCart.checkChoice = !Modeltab_Order_ShopingCart.checkChoice;
            Modeltab_Order_ShopingCart.UpdateTime = DateTime.Now;
            Modeltab_Order_ShopingCart.UpdateBy = "更改选择状态";
            EggsoftWXBLLtab_Order_ShopingCart.Update(Modeltab_Order_ShopingCart);

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
            EggsoftWX.BLL.tab_Order_ShopingCart EggsoftWXBLLtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            string checkId = context.QueryString["checkId"];
            string userId = context.QueryString["userId"];
            //int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");
            if (checkId == "1")
            {
                EggsoftWXBLLtab_Order_ShopingCart.Update("checkChoice=1,updatetime=getdate(),updateby='全选'", "UserID=@UserID", userId);
            }
            else if (checkId == "2")
            {
                EggsoftWXBLLtab_Order_ShopingCart.Update("checkChoice=0,updatetime=getdate(),updateby='取消全选'", "UserID=@UserID", userId);
            }
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

            EggsoftWX.BLL.tab_Order_ShopingCart EggsoftWXBLLtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            int allExistsCount = EggsoftWXBLLtab_Order_ShopingCart.ExistsCount("UserID=@UserID and ShopClientID=@ShopClientID and IsDeleted<>1", userId, ShopClientID);
            int allExistsChoicCount = EggsoftWXBLLtab_Order_ShopingCart.ExistsCount("UserID=@UserID and ShopClientID=@ShopClientID and checkChoice=1 and IsDeleted<>1", userId, ShopClientID);
            string strJson = "";
            if (allExistsCount == allExistsChoicCount && allExistsChoicCount > 0)
            {
                strJson = "{\"status\":2}";
            }
            else if (allExistsCount > allExistsChoicCount && allExistsChoicCount > 0)
            {
                strJson = "{\"status\":1}";
            }
            else
            {
                strJson = "{\"status\":0}";
            }

            return strJson;

        }
        #endregion

        #region 计算选中的金额============================
        public String dTotal(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string strJSon = getCartList(context);
            List<ClassgetCartList> listNew = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassgetCartList>>(strJSon);
            Decimal dTotal = 0;//总计款额
            Decimal marketPriceTotal = 0;//总计款额
            Decimal integralTotal = 0;
            int quantity = 0;
            for (int i = 0; i < listNew.Count; i++)
            {
                if (listNew[i].is_checked)
                {
                    integralTotal += listNew[i].Send_Vouchers_IfBuy.toDecimal() * listNew[i].GoodIDCount.toDecimal();
                    dTotal += listNew[i].GoodIDCount.toDecimal() * listNew[i].PromotePrice.toDecimal();
                    marketPriceTotal += listNew[i].GoodIDCount.toDecimal() * listNew[i].Price.toDecimal(); ;
                    quantity += listNew[i].GoodIDCount.toInt32();
                    //    }
                    //}


                    //string userId = context.QueryString["userId"];
                    //int typeId = Convert.ToInt32(context.QueryString["typeId"] ?? "1");



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
                }
                //Response.End();
            }
            marketPriceTotal = marketPriceTotal - dTotal;
            //     Response.Write("{\"dTotal\":" + dTotal + ",\"marketPriceTotal\":" + marketPriceTotal + ",\"quantity\":" + quantity + ",\"integralTotal\":" + integralTotal + "}");
            //Response.End();
            //}
            //  else
            // {
            return ("{\"dTotal\":" + dTotal + ",\"marketPriceTotal\":" + marketPriceTotal + ",\"quantity\":" + quantity + ",\"integralTotal\":" + integralTotal + "}");

        }

        #endregion

        #region 加减============================
        public String updateCart(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            string id = context.QueryString["id"];
            string type = context.QueryString["type"];
            EggsoftWX.BLL.tab_Order_ShopingCart EggsoftWXBLLtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
            EggsoftWX.Model.tab_Order_ShopingCart Model_Order_ShopingCart = EggsoftWXBLLtab_Order_ShopingCart.GetModel(id.toInt32());
            if (type == "1")
            {
                Model_Order_ShopingCart.GoodIDCount = Model_Order_ShopingCart.GoodIDCount + 1;
                Model_Order_ShopingCart.UpdateBy = "购物车增加";
                Model_Order_ShopingCart.UpdateTime = DateTime.Now;
            }
            else if (type == "2")
            {
                if (Model_Order_ShopingCart.GoodIDCount > 0)
                {
                    Model_Order_ShopingCart.GoodIDCount = Model_Order_ShopingCart.GoodIDCount - 1;
                    Model_Order_ShopingCart.UpdateBy = "购物车减少";
                    Model_Order_ShopingCart.UpdateTime = DateTime.Now;
                }
            }
            EggsoftWXBLLtab_Order_ShopingCart.Update(Model_Order_ShopingCart);

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
            EggsoftWX.BLL.tab_Orderdetails my_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
            my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
            string strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
            my_Model_tab_Order.OrderNum = strOrderNum;
            my_Model_tab_Order.ShopClient_ID = ShopClientID;

            string userId = context.QueryString["userId"];


            string strJSon = getCartList(context);
            List<ClassgetCartList> listNew = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassgetCartList>>(strJSon);
            Decimal dTotal = 0;//总计款额
            Decimal marketPriceTotal = 0;//总计款额
            Decimal integralTotal = 0;
            int quantity = 0;
            for (int i = 0; i < listNew.Count; i++)
            {
                if (listNew[i].is_checked)
                {
                    integralTotal += listNew[i].Send_Vouchers_IfBuy.toDecimal() * listNew[i].GoodIDCount.toDecimal();
                    dTotal += listNew[i].GoodIDCount.toDecimal() * listNew[i].PromotePrice.toDecimal();
                    marketPriceTotal += listNew[i].GoodIDCount.toDecimal() * listNew[i].Price.toDecimal(); ;
                    quantity += listNew[i].GoodIDCount.toInt32();

                    EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

                    my_Model_tab_Orderdetails.GoodID = listNew[i].GoodID.toInt32();
                    my_Model_tab_Orderdetails.GoodName = listNew[i].Name;
                    my_Model_tab_Orderdetails.GoodPrice = listNew[i].PromotePrice.toDecimal();
                    my_Model_tab_Orderdetails.OrderCount = listNew[i].GoodIDCount.toInt32();
                    my_Model_tab_Orderdetails.ShopClient_ID = ShopClientID;
                    my_Model_tab_Orderdetails.OrderID = intID;
                    my_tab_Orderdetails.Add(my_Model_tab_Orderdetails);
                    // my_tab_Order_ShopingCart.Update("IsDeleted=1,IsDeletedTime=getdate(),UpdateTime=getdate(),UpdateBy='进行支付'", "UserID=" + userId + " and id=" + listNew[i].ID + " and IsDeleted=0");

                }
            }
            my_Model_tab_Order.OrderName = listNew[0].Name + "等";
            my_Model_tab_Order.ShopClient_ID = ShopClientID;
            my_Model_tab_Order.TotalMoney = dTotal;
            my_Model_tab_Order.UserID = userId.toInt32();
            my_Model_tab_Order.User_Address = context.QueryString["addressId"].toInt32();
            my_BLL_tab_Order.Update(my_Model_tab_Order);
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
            string strJson = "{\"status\":1,\"OrderID\":" + intID + "}";
            return strJson;
            //Response.End();
            //}
        }

        public String canalOrder(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int id = Convert.ToInt32(context.QueryString["id"] ?? "0");

            new EggsoftWX.BLL.tab_Order().Delete(id);
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
            string strJson = "{\"status\":0}";
            return strJson;
            //Response.End();
            //}
        }


        public class isDefaultClass : EggsoftWX.Model.tab_User_Address
        {
            public bool isDefault { get; set; }
        }
        //查询地址
        public String getAddressList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            string where = context.QueryString["where"] ?? "0";
            string orderID = context.QueryString["orderID"].toString();

            string strWhere = "1=1";
            if (string.IsNullOrEmpty(orderID) == false)
            {
                EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.BLL.tab_Order().GetModel(orderID.toInt32());
                strWhere = "id=" + Modeltab_Order.User_Address;
            }

            System.Data.DataTable dt_DataTable_tab_User_Address = new EggsoftWX.BLL.tab_User_Address().GetList("UserID=" + userId + " and " + strWhere + " and IsDeleted<>1").Tables[0];

            //List<Cms.Model.C_user_cart> list = new Cms.BLL.C_user_cart().GetModelList("user_id=" + userId + " and typeId=" + typeId + where);
            if (dt_DataTable_tab_User_Address != null && dt_DataTable_tab_User_Address.Rows.Count > 0)
            {


                //List<EggsoftWX.Model.tab_User_Address> list = new Cms.BLL.c_user_address().GetModelList("user_id=" + userId + where);
                //if (list != null && list.Count > 0)
                //{
                int intDefault_Address = new EggsoftWX.BLL.tab_User().GetModel(userId).Default_Address.toInt32();

                string strJson = Eggsoft.Common.Json2Table.ToJson(dt_DataTable_tab_User_Address);
                List<isDefaultClass> listNew = Newtonsoft.Json.JsonConvert.DeserializeObject<List<isDefaultClass>>(strJson);
                isDefaultClass dddd = listNew.Where(u => u.ID == intDefault_Address).FirstOrDefault();
                dddd.isDefault = true;

                return listNew.toJsonString();
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
                string strJson = "{\"status\":0}";
                return strJson;
                //Response.End();
            }
            else
            {
                string strJson = "{\"status\":1}";
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
            int intWhere = new EggsoftWX.BLL.tab_User().Update(Modeltab_User);
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
            string strJson = "{\"status\":" + (!(intWhere > 0)).toInt32() + "}";
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



            EggsoftWX.BLL.tab_User_Address BLLmodel = new EggsoftWX.BLL.tab_User_Address();
            EggsoftWX.Model.tab_User_Address model = BLLmodel.GetModel(id);
            int intID = 0;
            if (model == null)
            {
                model = new EggsoftWX.Model.tab_User_Address();
                model.UserID = userId;
                model.RealName = consignee;
                model.MobilePhone = cellphone;
                model.PostCode = code;
                model.pc_province = Province;
                model.pc_city = City;
                model.pc_district = District;
                //model.street = this.street.Value;
                model.XiangXiDiZHi = address;
                intID = BLLmodel.Add(model);
            }
            else
            {

                model.RealName = consignee;
                model.MobilePhone = cellphone;
                model.PostCode = code;
                model.pc_province = Province;
                model.pc_city = City;
                model.pc_district = District;
                //model.street = this.street.Value;
                model.XiangXiDiZHi = address;
                intID = BLLmodel.Update(model);
            }






            //Cms.Model.c_user_address model = new Cms.Model.c_user_address();

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
            string strJson = "{\"status\":" + (!(intID > 0)).toInt32() + "}";
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

        public class returnOrder : EggsoftWX.Model.tab_Order
        {
            public List<dynamic> DetailOrderList { get; set; }
        }


        public String getOrderList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            string where = context.QueryString["where"] ?? "0";
            string keyWord = context.QueryString["keyWord"] ?? "0";

            string strCondittion = "1=1";
            if (where == "all")
            {

            }
            else if (where == "waitpay")
            {
                strCondittion = "PayStatus=0 and IsDeleted=0";
            }
            else if (where == "waitGet")
            {
                strCondittion = "PayStatus=1 and isReceipt=0";
            }
            else if (where == "finishend")
            {
                strCondittion = "PayStatus=1 and isReceipt=1";
            }
            else if (where == "cancel")
            {
                strCondittion = "IsDeleted=1";
            }


            string strWhere = @"SELECT    dbo.tab_Order.*
FROM            tab_Order 
            where tab_Order.userID=@userID and tab_Order.ShopClient_ID=@ShopClient_ID and " + strCondittion + @" order by id desc
";
            System.Data.DataTable Data_DataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataSet(strWhere, userId, ShopClientID).Tables[0];

            string strJsonData_DataTable = Eggsoft.Common.Json2Table.ToJson(Data_DataTable);
            List<returnOrder> returnOrderList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<returnOrder>>(strJsonData_DataTable);
            if (returnOrderList != null && returnOrderList.Count > 0)
            {

                foreach (returnOrder record in returnOrderList)
                {

                    string strOrdertialWhere = @"SELECT    tab_Orderdetails.*,tab_GoodsLike.name,tab_GoodsLike.icon,tab_GoodsLike.unitName 
FROM            tab_Orderdetails  LEFT OUTER JOIN
            (select tab_Goods.*,tab_Goods_Unit.unit as unitName from tab_Goods LEFT OUTER JOIN tab_Goods_Unit on tab_Goods.Unit=tab_Goods_Unit.id) tab_GoodsLike ON tab_GoodsLike.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND 
                tab_GoodsLike.ID = tab_Orderdetails.GoodID
            where tab_Orderdetails.OrderID=@OrderID 
order by id desc
";

                    System.Data.DataTable tab_OrderdetailsData_DataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataSet(strOrdertialWhere, record.ID).Tables[0];

                    string strtab_OrderdetailsData_DataTable = Eggsoft.Common.Json2Table.ToJson(tab_OrderdetailsData_DataTable);
                    List<dynamic> returnOrderdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(strtab_OrderdetailsData_DataTable);
                    record.DetailOrderList = returnOrderdetails;
                }
            }

            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(returnOrderList); ;
            return (strJson);

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
            string strCondittion = "1=1";

            string strOrdertialWhere = @"SELECT    tab_Orderdetails.*,tab_GoodsLike.name,tab_GoodsLike.icon,tab_GoodsLike.unitName,tab_GoodsLike.Price,tab_GoodsLike.PromotePrice 
FROM            tab_Orderdetails  LEFT OUTER JOIN
            (select tab_Goods.*,tab_Goods_Unit.unit as unitName from tab_Goods LEFT OUTER JOIN tab_Goods_Unit on tab_Goods.Unit=tab_Goods_Unit.id) tab_GoodsLike ON tab_GoodsLike.ShopClient_ID = tab_Orderdetails.ShopClient_ID AND 
                tab_GoodsLike.ID = tab_Orderdetails.GoodID
            where tab_Orderdetails.OrderID=@OrderID 
order by id desc
";

            System.Data.DataTable tab_OrderdetailsData_DataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetDataSet(strOrdertialWhere, id).Tables[0];

            string strtab_OrderdetailsData_DataTable = Eggsoft.Common.Json2Table.ToJson(tab_OrderdetailsData_DataTable);



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
            //string strJson = Eggsoft.Common.JsonHelper.LocalSerialize(null);
            return strtab_OrderdetailsData_DataTable;
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
                case 1://待支付
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=0  and isdeleted<>1");
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 2://待发货
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=1 and isReceipt=0 and isnull(DeliveryText,'')=''  and isdeleted<>1");

                    //count = new Cms.BLL.C_order().GetRecordCount("is_payment=1 and order_status=0 and is_delivery=0 and user_id=" + userId);
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 3://待收货
                    count = my_tab_Order.ExistsCount("UserID=" + userId + " and PayStatus=1 and isReceipt=0 and isnull(DeliveryText,'')<>''  and isdeleted<>1");

                    // count = new Cms.BLL.C_order().GetRecordCount("is_payment=1 and order_status=0 and is_delivery=1 and is_receiving=0 and user_id=" + userId);
                    if (count > 0)
                    {
                        string strJson = "{\"status\":" + count + "}";
                        return strJson;
                        //Response.End();
                    }
                    break;
                case 4://已完成
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

            string strReturn = _14WcfService1.SmallProgram.ClassPay.spayModelGetPay(context);

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
            return strReturn;
        }
        #endregion

        #region 获取充值记录=======================
        public String getUserRechargeList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;
            System.Data.DataTable DataDataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetPageDataTable(page, size, "*",
                "tab_TotalCredits_Consume_Or_Recharge", " and UserID=@UserID", "ID", true, userId);

            //DataSet ds = new Cms.BLL.C_user_recharge().GetListByPage("userId=" + userId + " and isPay=1", "id desc", start, end);
            if (DataDataTable != null && DataDataTable.Rows.Count > 0)
            {
                //List<Cms.Model.C_user_recharge> list = new Cms.BLL.C_user_recharge().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.Json2Table.ToJson(DataDataTable);
                return strJson;
                //Response.End();
            }
            else
            {
                return "";
                //Response.End();
            }
        }

        public String addUserRecharge(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            Decimal price = Convert.ToDecimal(context.QueryString["price"] ?? "0.1");

            int int_InputMoney_GoodID = 0;
            string strInputMoney_GoodID = Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.toString(), "InputMoney_GoodID");
            int.TryParse(strInputMoney_GoodID, out int_InputMoney_GoodID);


            EggsoftWX.BLL.tab_Orderdetails my_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();

            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            int intID = my_BLL_tab_Order.Add(my_Model_tab_Order);///先用存储过程插入  然后在更新 防止重复插入相同的ID
            my_Model_tab_Order = my_BLL_tab_Order.GetModel(intID);
            string strOrderNum = DateTime.Now.ToString("yyyyMMddHHmmss") + Eggsoft.Common.StringNum.Add000000Num(intID, 2);
            my_Model_tab_Order.OrderNum = strOrderNum;
            my_Model_tab_Order.ShopClient_ID = ShopClientID;

            Decimal dTotal = 0;//总计款额
            Decimal marketPriceTotal = 0;//总计款额
            Decimal integralTotal = 0;
            int quantity = 0;
            integralTotal += 0;
            dTotal += price;
            marketPriceTotal += price; ;
            quantity += 1;

            EggsoftWX.Model.tab_Orderdetails my_Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

            my_Model_tab_Orderdetails.GoodID = int_InputMoney_GoodID;
            my_Model_tab_Orderdetails.GoodName = "会员充值";
            my_Model_tab_Orderdetails.GoodPrice = price;
            my_Model_tab_Orderdetails.OrderCount = 1;
            my_Model_tab_Orderdetails.ShopClient_ID = ShopClientID;
            my_Model_tab_Orderdetails.OrderID = intID;
            my_tab_Orderdetails.Add(my_Model_tab_Orderdetails);


            my_Model_tab_Order.OrderName = "会员充值";
            my_Model_tab_Order.ShopClient_ID = ShopClientID;
            my_Model_tab_Order.TotalMoney = dTotal;
            my_Model_tab_Order.UserID = userId.toInt32();
            my_Model_tab_Order.User_Address = 0;
            my_BLL_tab_Order.Update(my_Model_tab_Order);


            if (intID > 0)
            {
                string strJson = "{\"status\":" + intID + "}";
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
        #endregion

        #region 获取积分记录=======================
        public String getUserIntegralList(HttpRequest context)
        {
            int ShopClientID = context.QueryString["ShopClientID"].toInt32();

            int userId = Convert.ToInt32(context.QueryString["userId"] ?? "0");
            int page = Convert.ToInt32(context.QueryString["page"] ?? "0");
            int size = Convert.ToInt32(context.QueryString["size"] ?? "3");
            int start = (page - 1) * size + 1;
            int end = page * size;
            System.Data.DataTable DataDataTable = EggsoftWX.SQLServerDAL.DbHelperSQL.GetPageDataTable(page, size, "*",
                "tab_Total_Vouchers_Consume_Or_Recharge", " and UserID=@UserID", "ID", true, userId);

            //DataSet ds = new Cms.BLL.C_user_recharge().GetListByPage("userId=" + userId + " and isPay=1", "id desc", start, end);
            if (DataDataTable != null && DataDataTable.Rows.Count > 0)
            {
                //List<Cms.Model.C_user_recharge> list = new Cms.BLL.C_user_recharge().DataTableToList(ds.Tables[0]);
                string strJson = Eggsoft.Common.Json2Table.ToJson(DataDataTable);
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