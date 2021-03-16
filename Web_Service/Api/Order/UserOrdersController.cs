using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.Insurance;
using InsureApi.WebApi.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Service.Common;

namespace Web_Service.Api.Order
{
    public class UserOrdersController : BasicController
    {
        /// <summary>
        /// Get 获取订单详情
        /// </summary>
        /// <param name="InsuranceOrderID">订单号</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Order/UserOrders/{InsuranceOrderID}/{UserID}")]
        public ResultAPIModel<UserOrdersAPIModel.UserOrderDetailReturn> GetUserOrderDetail(string InsuranceOrderID, string UserID)
        {
            const string apiUrl = "get api/order/UserOrders/{InsuranceOrderID}/{UserID}";
            var result = new ResultAPIModel<UserOrdersAPIModel.UserOrderDetailReturn>();

            try
            {
                var msg = "";
                result.data = new UserOrdersAPIModel.UserOrderDetailReturn();

                if (Check.getIns().isEmpty(InsuranceOrderID, UserID))
                {
                    result.resultMessage.errorMessage = "参数错误";
                    return result;
                }

                var userOrderDetail = new UserOrderDetailAPIModel();

                //查询订单信息
                var orderDetail = new InsuranceOrderBC().GetInsuranceOrderDetail(InsuranceOrderID);
                if (Check.getIns().isEmpty(orderDetail))
                {
                    result.resultMessage.errorMessage = "无此订单";
                    return result;
                }

                result = GetOrderDetail(InsuranceOrderID, orderDetail);
            }
            catch (Exception ex)
            {

                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    errorMessage = ex.Message
                };
            }
            return result;
        }

        public ResultAPIModel<UserOrdersAPIModel.UserOrderDetailReturn> GetOrderDetail(string InsuranceOrderID, InsuranceOrderInfoExt orderInfo = null)
        {
            var result = new ResultAPIModel<UserOrdersAPIModel.UserOrderDetailReturn>();
            result.data = new UserOrdersAPIModel.UserOrderDetailReturn();

            var userOrderDetail = new UserOrderDetailAPIModel();

            //查询订单信息
            var orderDetail = orderInfo;
            if (Check.getIns().isEmpty(orderDetail))
            {
                orderDetail = new InsuranceOrderBC().GetInsuranceOrderDetail(InsuranceOrderID);
                if (Check.getIns().isEmpty(orderDetail))
                {
                    result.resultMessage.errorMessage = "无此订单";
                    return result;
                }
            }

            #region 城市名称
            var CityInfo = new InsuranceCityBC().SelectByPrimaryKey(orderDetail.RunCity);
            if (!Check.getIns().isEmpty(CityInfo))
            {
                result.data.CityName = CityInfo.CityNameCN;
            }
            #endregion

            #region  保单详情
            var queryTask = new CarInsuranceQueryTaskBC().SelectByPrimaryKey(orderDetail.CarInsuranceQueryTaskID);
            if (Check.getIns().isEmpty(queryTask)
                || queryTask.IsDelete != 0)
            {
                result.resultMessage.errorMessage = "无保单详情";
                return result;
            }

            var queryHistory = new CarInsureQueryHistoryBC().SelectByPrimaryKey(queryTask.CarInsureQueryHistoryID);
            //投保方案
            if (Check.getIns().isEmpty(queryHistory)
                || queryHistory.IsDelete != 0)
            {
                result.resultMessage.errorMessage = "无投保方案";
                return result;
            }

            var queryTaskDetail = new CarInsuranceQueryTaskDetailBC().GetInfoByCarInsureQueryTaskID(orderDetail.CarInsuranceQueryTaskID);
            //保单报价
            if (Check.getIns().isEmpty(queryTaskDetail)
                || queryTaskDetail.Count <= 0)
            {
                result.resultMessage.errorMessage = "无保单明细";
                return result;
            }

            //组织数据
            Function.getIns().copyValue(orderDetail, result.data, false);

            #region 活动信息
            //result.data.ActivityID = orderDetail.ActivityID;
            //if (!Check.getIns().isEmpty(orderDetail.ActivityID))
            //{
            //    var ActivityOrder = new CooperationActivityBC().SelectByPrimaryKey(orderDetail.ActivityID);
            //    if (!Check.getIns().isEmpty(ActivityOrder))
            //    {
            //        result.data.ActivityTitle = ActivityOrder.ActivityTitle;
            //        result.data.ActivityCode = ActivityOrder.ActivityCode;
            //        result.data.StartTime = ActivityOrder.StartTime.Value;
            //        result.data.EndTime = ActivityOrder.EndTime.Value;
            //        result.data.ActivityContent = ActivityOrder.ActivityContent;
            //        result.data.State = ActivityOrder.State.Value;
            //        result.data.ActivityLevel = ActivityOrder.ActivityLevel.Value;
            //        result.data.ActivityType = ActivityOrder.ActivityType.Value;

            //        if (!Check.getIns().isEmpty(ActivityOrder.JiGouID))
            //        {
            //            var AactivityCompanyInfo = new CooperationActivityCompanyBC().SelectByPrimaryKey(ActivityOrder.JiGouID);
            //            if (!Check.getIns().isEmpty(AactivityCompanyInfo))
            //            {
            //                result.data.CooperationCompanyName = AactivityCompanyInfo.CooperationCompanyName;
            //            }
            //        }
            //    }
            //}

            #endregion

            //result.data.InsuranceCompanyBusinessTypeText = EnumHelper.GetDescription(orderDetail.Payment,
            //    typeof(AppEnum.PaymentType), "线上支付");
            ////中银特殊处理
            //if (!Check.getIns().isEmpty(orderDetail.CompanyCode))
            //{
            //    if (orderDetail.CompanyCode.ToUpper().Equals("INSURE_51ZHONGYIN"))
            //    {
            //        result.data.InsuranceCompanyBusinessTypeText = "支付请联系客服";
            //    }
            //}
            //代理人信息
            //var userinfo = new TUserBC().SelectByPrimaryKey(orderDetail.TUserID) ?? new TUserInfo();
            //result.data.UserName = userinfo.UserName;
            //result.data.UserPhone = userinfo.UserPhone;
            result.data.Payment = orderDetail.Payment;
            result.data.InsuranceOrderStatuMessage = orderDetail.DealMessage;
            result.data.OrderAmount = 0;
            result.data.OrderAmountBusinessTotal = 0;
            result.data.OrderAmountJQXTotal = 0;
            result.data.RunProvinceName = orderDetail.ProvinceName;
            result.data.RunCityName = orderDetail.CityName;
            result.data.CarInsureQueryHistoryID = queryHistory.CarInsureQueryHistoryID;
            //result.data.InsureEndDate = queryHistory.InsureEndDate.GetValueOrDefault();
            //result.data.StartDate = queryHistory.StartDate.Value;
            //result.data.SuccessChuDanTime = orderDetail.SuccessChuDanTime; //成功出单时间
            //result.data.IsCarDriver = queryHistory.IsCarDriver.GetValueOrDefault() == 1;
            //result.data.IsDriveArea = queryHistory.IsDriveArea.GetValueOrDefault() == 1;
            //result.data.IsServiceFactory = queryHistory.IsServiceFactory.GetValueOrDefault() == 1;
            result.data.Detail = new List<UserOrdersAPIModel.UserOrderInsuranceDetailReturn>();
            decimal ExcludingDeductible = 0;
            foreach (var data in queryTaskDetail)
            {
                if (GetProductName(data.ProductID) == "附加险不计免赔")
                {
                    ExcludingDeductible += data.EndProductPrice.Value;
                    continue;
                }
                var model = new UserOrdersAPIModel.UserOrderInsuranceDetailReturn()
                {
                    ProductID = data.ProductID.ToUpper(),
                    ProductName = GetProductName(data.ProductID),
                    ProductType = GetProductType(data.ProductID),
                    ProductPlan = GetProductPlan(data.ProductID, queryHistory),
                    ProductQty = data.ProductQty,
                    ProductAmount = data.ProductAmount,
                    EndProductPrice = data.EndProductPrice,
                    ProductSort = GetProductSort(data.ProductID).toInt32()
                };

                result.data.OrderAmount += model.EndProductPrice.HasValue ? model.EndProductPrice.Value : 0;
                if ("商业".Equals(model.ProductType))
                {
                    result.data.OrderAmountBusinessTotal += model.EndProductPrice.HasValue ? model.EndProductPrice.Value : 0;
                }

                if ("交强险和车船税".Equals(model.ProductType))
                {
                    result.data.OrderAmountJQXTotal += model.EndProductPrice.HasValue ? model.EndProductPrice.Value : 0;
                }
                if ("专修厂维修特约险".Equals(model.ProductType))
                {
                    result.data.IsSupportFactory = model.EndProductPrice.GetValueOrDefault() > 0;
                    result.data.OrderAmountBusinessTotal += model.EndProductPrice.HasValue ? model.EndProductPrice.Value : 0;
                }
                result.data.Detail.Add(model);
            }

            //附加险不计免赔
            var model2 = new UserOrdersAPIModel.UserOrderInsuranceDetailReturn()
            {
                ProductID = "",
                ProductName = "附加险不计免赔",
                ProductType = "商业",
                ProductPlan = "",
                ProductQty = 1,
                ProductAmount = 0,
                EndProductPrice = ExcludingDeductible,
                ProductSort = GetProductSort("附加险不计免赔").toInt32()
            };

            result.data.OrderAmount += ExcludingDeductible;
            if ("商业".Equals(model2.ProductType))
            {
                result.data.OrderAmountBusinessTotal += ExcludingDeductible;
            }
            result.data.Detail.Add(model2);


            //排序
            result.data.Detail.Sort((x, y) => StringComparer.CurrentCultureIgnoreCase.Compare(x.ProductSort, y.ProductSort));
            #endregion

            #region 传统保险公司
            ////传统公司
            //if (orderDetail.InsuranceCompanyBusinessType == (int)AppEnum.InsuranceCompanyBusinessTypeEnum.Offline)
            //{
            //    var companyDetailInfo = new InsuranceCompanyDetailBC().getInsuranceCompanyDetailInfo(orderDetail.RunCity, orderDetail.CompanyCode);
            //    if (!Check.getIns().isEmpty(companyDetailInfo))
            //    {
            //        result.data.InsuranceCompanyBankName = companyDetailInfo.BankName;
            //        result.data.InsuranceCompanyBankAccountName = companyDetailInfo.BankAccountName;
            //        result.data.InsuranceCompanyBankAccountID = companyDetailInfo.BankAccountID;
            //    }
            //}

            #endregion

            #region 支付有效时间
            result.data.IsPayEnable = false;
            //if (orderDetail.InsuranceOrderStatu == StateCode.InsuranceOrderStatus.DaiFuKuang.Value)
            //{
            //    var hebaoTime = new InsuranceOrderBC().getInsuranceOrderStatusTime(InsuranceOrderID, StateCode.InsuranceOrderStatus.DaiFuKuang.Value.ToString());
            //    //如果没有核保时间，则使用订单时间（兼容旧数据）
            //    if (!hebaoTime.HasValue)
            //    {
            //        hebaoTime = orderDetail.InsuranceOrderTime;
            //    }

            //    //支付有效时间为48小时
            //    result.data.IsPayEnable = Function.getIns().dateDiff("hh", hebaoTime.Value, DateTime.Now) <= 48;
            //}
            #endregion

            //检索数据库获取报价数据
            result.data.InsureList = InsureHistoryHelper.getIns().queryCarInsureQueryInfo(queryHistory, false).CarInsuranceDataList;
            if (result.data.InsureList.Count > 0) result.data.InsureList.OrderBy(O => O.CompanyName);
            result.data.InsureQueryHistoryInfo = new CarInsureQueryHistoryAPIModel();
            Function.getIns().copyValue(queryHistory, result.data.InsureQueryHistoryInfo);
            return result;
        }

        /// <summary>
        /// 险种类型
        /// </summary>
        private string GetProductType(string ProductID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("63", "商业");
            dic.Add("68", "商业");
            dic.Add("74", "商业");
            dic.Add("73", "商业");
            dic.Add("89", "商业");

            dic.Add("A63", "商业");
            dic.Add("A68", "商业");
            dic.Add("A74", "商业");
            dic.Add("A73", "商业");
            dic.Add("A89", "商业");
            dic.Add("A7389", "商业");

            dic.Add("34", "商业");
            dic.Add("75", "商业");
            dic.Add("77", "商业");
            dic.Add("82", "商业");

            dic.Add("200", "交强险和车船税");
            dic.Add("pcCarShipTaxInfoDto".ToUpper(), "交强险和车船税");
            dic.Add("85", "专修厂维修特约险");

            return dic.ContainsKey(ProductID.ToUpper()) ? dic[ProductID.ToUpper()] : "";
        }

        /// <summary>
        /// 险种名称
        /// </summary>
        private string GetProductName(string ProductID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("63", "车辆损失险");
            dic.Add("68", "第三者责任险");
            dic.Add("74", "全车盗抢险");
            dic.Add("73", "司机座位险");
            dic.Add("89", "乘客座位险");

            dic.Add("A63", "车辆损失险不计免赔");
            dic.Add("A68", "第三者责任险不计免赔");
            dic.Add("A74", "全车盗抢险不计免赔");
            dic.Add("A73", "司机座位险不计免赔");
            dic.Add("A89", "乘客座位险不计免赔");
            dic.Add("A7389", "车上人员险不计免赔");

            dic.Add("34", "玻璃破碎险");
            dic.Add("75", "车身划痕险");
            dic.Add("77", "发动机涉水险");
            dic.Add("82", "自燃险");

            dic.Add("A34", "附加险不计免赔");
            dic.Add("A75", "附加险不计免赔");
            dic.Add("A77", "附加险不计免赔");
            dic.Add("A82", "附加险不计免赔");

            dic.Add("200", "交强险");
            dic.Add("pcCarShipTaxInfoDto".ToUpper(), "车船税");
            dic.Add("85", "专修厂维修特约险");
            #region 参考代码
            //<li id="L63"><span id="KindCode63"></span>车辆损失险</li>
            //<li id="L68"><span id="KindCode68"></span>第三者责任险</li>
            //<li id="L73"><span id="KindCode73"></span>司机座位险</li>
            //<li id="L74"><span id="KindCode74"></span>全车被盗险</li>
            //<li id="L34"><span id="KindCode34"></span>玻璃破碎险</li>
            //<li id="L75"><span id="KindCode75"></span>车身划痕险</li>
            //<li id="L77"><span id="KindCode77"></span>发动机涉水险</li>
            //<li id="L82"><span id="KindCode82"></span>自燃损失险</li>
            //<li id="L89"><span id="KindCode89"></span>乘客座位险</li>
            #endregion

            return dic.ContainsKey(ProductID.ToUpper()) ? dic[ProductID.ToUpper()] : "";
        }

        /// <summary>
        /// 投保内容
        /// </summary>
        private string GetProductPlan(string ProductID, CarInsureQueryHistoryInfo queryHistory)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1000000", "100万");
            dic.Add("500000", "50万");
            dic.Add("300000", "30万");
            dic.Add("200000", "20万");
            dic.Add("150000", "15万");
            dic.Add("50000", "5万");
            dic.Add("30000", "3万");
            dic.Add("20000", "2万");
            dic.Add("10000", "1万");
            dic.Add("5000", "5000");
            dic.Add("2000", "2000");

            Dictionary<string, string> glass = new Dictionary<string, string>();
            glass.Add("1", "国产玻璃");
            glass.Add("2", "进口玻璃");

            #region 参考代码
            //<li id="L63"><span id="KindCode63"></span>车辆损失险</li>
            //<li id="L68"><span id="KindCode68"></span>第三者责任险</li>
            //<li id="L73"><span id="KindCode73"></span>司机座位险</li>
            //<li id="L74"><span id="KindCode74"></span>全车被盗险</li>
            //<li id="L34"><span id="KindCode34"></span>玻璃破碎险</li>
            //<li id="L75"><span id="KindCode75"></span>车身划痕险</li>
            //<li id="L77"><span id="KindCode77"></span>发动机涉水险</li>
            //<li id="L82"><span id="KindCode82"></span>自燃损失险</li>
            //<li id="L89"><span id="KindCode89"></span>乘客座位险</li>
            #endregion

            switch (ProductID.ToUpper())
            {
                case "68"://第三者责任险
                    return dic.ContainsKey(queryHistory.ThirdPartyInsurance) ? dic[queryHistory.ThirdPartyInsurance] : "";
                case "73"://司机座位险
                    return dic.ContainsKey(queryHistory.CarDriverInsurance) ? dic[queryHistory.CarDriverInsurance] : "";
                case "89"://乘客座位险
                    return dic.ContainsKey(queryHistory.CarPassengerInsurance) ? dic[queryHistory.CarPassengerInsurance] : "";
                case "75"://车身划痕险
                    return dic.ContainsKey(queryHistory.CarScratchInsurance) ? dic[queryHistory.CarScratchInsurance] : "";
                case "34":
                    return glass.ContainsKey(queryHistory.CarGlassInsurance) ? glass[queryHistory.CarGlassInsurance] : "";
            }
            return "";
        }

        /// <summary>
        /// 险种排序
        /// </summary>
        private string GetProductSort(string ProductID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("63", "1");//车辆损失险
            dic.Add("68", "2");//第三者责任险
            dic.Add("74", "3");//全车盗抢险
            dic.Add("73", "4");//司机座位险
            dic.Add("89", "5");//乘客座位险

            dic.Add("A63", "6");//车辆损失险不计免赔
            dic.Add("A68", "7");//第三者责任险不计免赔
            dic.Add("A74", "8");//全车盗抢险不计免赔
            dic.Add("A73", "9");//司机座位险不计免赔
            dic.Add("A89", "10");//乘客座位险不计免赔
            dic.Add("A7389", "11");//车上人员险不计免赔

            dic.Add("34", "12");//玻璃破碎险
            dic.Add("75", "13");//车身划痕险
            dic.Add("77", "14");//发动机涉水险
            dic.Add("82", "15");//自燃险

            dic.Add("附加险不计免赔", "16");//自燃险

            dic.Add("200", "17");//交强险
            dic.Add("pcCarShipTaxInfoDto".ToUpper(), "18");//车船税

            return dic.ContainsKey(ProductID.ToUpper()) ? dic[ProductID.ToUpper()] : "";
        }

        [HttpPost]
        /// <summary>
        /// Post 获取订单列表
        /// </summary>
        public ResultAPIModel<List<UserOrdersAPIModel.UserOrderListReturn>> Post(ResultAPIModel<UserOrdersAPIModel.UserOrderListSearch> m)
        {
            string apiUrl = "post api/Order/UserOrders";
            var result = new ResultAPIModel<List<UserOrdersAPIModel.UserOrderListReturn>>();

            try
            {

                result.data = new List<UserOrdersAPIModel.UserOrderListReturn>();

                string msg = "";
                if (!m.IsValidUserInfo(out msg)
                    || !m.IsValidPagingInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                #region 业务数据查询
                var searchModel = new InsuranceOrderInfoExt
                {
                    InsuranceOrderTimeBegin = m.data.InsuranceOrderTimeBegin,
                    InsuranceOrderTimeEnd = m.data.InsuranceOrderTimeEnd,
                    VipName = m.data.VipName,
                    CarMasterName = m.data.CarMasterName,
                    CarNumber = m.data.CarNumber,
                    InsuranceCompany = m.data.InsuranceCompany,
                    InsuranceOrderStatus = m.data.InsuranceOrderStatus,
                    VipAmountStatuText = m.data.VipAmountStatuText,
                    FullTextSearch = m.data.FullTextSearch,
                    PageNumber = m.paging.pageNumber,
                    PageSize = m.paging.pageSize,
                    UserID = m.userCookie.userID,
                    UserName = m.data.UserName,
                    UserPhone = m.data.UserPhone,
                    IsIncludeTest = m.data.IsIncludeTest,
                    InsuranceCity = m.data.InsuranceCity
                };

                #region 验证用户
                var userInfo = new TUserBC().SelectByPrimaryKey(m.userCookie.userID);
                if (Check.getIns().isEmpty(userInfo)
                    || userInfo.IsDelete != 0)
                {
                    result.resultMessage.errorMessage = "用户信息错误";
                    return result;
                }

                if (!IsValidUserID(userInfo, out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                bool isRoleUser = true; //new QYBRoleBC().IsRoleUserByUserAccount(userInfo.UserAccount);

                //如果是管理员UserId不作处理
                //if ("ADMIN".Equals(userInfo.UserAccount.ToUpper()))
                //if (isRoleUser)
                //{
                //    searchModel.UserID = null;
                //}
                #endregion

                var orderInfos = (new InsuranceOrderBC()).GetInsuranceOrderInfos(searchModel);

                //组织数据
                var dataCount = 0;
                if (orderInfos != null && orderInfos.Count > 0)
                {
                    dataCount = orderInfos.First().DataCount;
                    foreach (var data in orderInfos)
                    {
                        var model = new UserOrdersAPIModel.UserOrderListReturn();
                        Function.getIns().copyValue(data, model);
                        result.data.Add(model);
                    }
                }

                result.paging = new PagingAPIModel()
                {
                    dataCount = dataCount,
                    pageSize = m.paging.pageSize,
                    pageNumber = m.paging.pageNumber
                };

                #endregion
            }
            catch (Exception ex)
            {

                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    errorMessage = ex.Message.ToString()
                };
            }

            return result;
        }

        /// <summary>
        /// 查询本月本周当天订单记录与金额
        /// </summary>
        [HttpPut]
        public ResultAPIModel<UserOrdersAPIModel.GetOrderRecord> put(ResultAPIModel<UserOrdersAPIModel.UserOrderListSearch> m)
        {
            string apiUrl = "put api/Order/UserOrders";
            var result = new ResultAPIModel<UserOrdersAPIModel.GetOrderRecord>();
            try
            {
                result.data = new UserOrdersAPIModel.GetOrderRecord();

                var searchModel = new InsuranceOrderInfoExt
                {
                    InsuranceOrderTimeBegin = m.data.InsuranceOrderTimeBegin,
                    InsuranceOrderTimeEnd = m.data.InsuranceOrderTimeEnd,
                    CarMasterName = m.data.CarMasterName,
                    CarNumber = m.data.CarNumber,
                    InsuranceProvince = m.data.InsuranceProvince,
                    InsuranceCity = m.data.InsuranceCity,
                    InsuranceCompany = m.data.InsuranceCompany,
                    InsuranceOrderStatus = m.data.InsuranceOrderStatus,
                    FullTextSearch = m.data.FullTextSearch,
                    UserID = m.userCookie.userID,
                    VipCarID = m.data.VipCarID,
                    ChannelName = m.data.ChannelName
                };

                var GetOrderRecord = new InsuranceOrderBC().GetOrderRecord(searchModel);
                if (!Check.getIns().isEmpty(GetOrderRecord))
                {
                    foreach (var item in GetOrderRecord)
                    {
                        result.data = new UserOrdersAPIModel.GetOrderRecord();
                        {
                            result.data.TotalAmount = item.TotalAmount;
                            result.data.MonthOrder = item.MonthOrder;
                            result.data.MonthOrderAmount = item.MonthOrderAmount;
                            result.data.WeekOrder = item.WeekOrder;
                            result.data.WeekOrderAmount = item.WeekOrderAmount;
                            result.data.DayOrder = item.DayOrder;
                            result.data.DayOrderAmount = item.DayOrderAmount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    message = ex.Message.ToString()
                };
            }
            return result;
        }
    }
}
