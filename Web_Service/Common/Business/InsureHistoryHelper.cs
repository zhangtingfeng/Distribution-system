using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InsureApi.Entity;
using InsureApi.WebApi.Model.Insurance;
using InsureApi.Common.Common;
using InsureApi.BLL;
using Insure.Common;

namespace Web_Service.Common
{
    public class InsureHistoryHelper
    {
        private static InsureHistoryHelper _instance = new InsureHistoryHelper();
        public static InsureHistoryHelper getIns() { return _instance; }

        /// <summary>
        /// 检索数据库获取报价数据
        /// </summary>
        public CarInsuranceResultModel queryCarInsureQueryInfo(CarInsureQueryHistoryInfo carInsureQueryHistoryInfo, bool isQuote = true)
        {
            var result = new CarInsuranceResultModel();
            result.CarInsuranceDataList = new List<CarInsuranceAPIModel>();
            #region 检索数据库,获取报价数据
            var companys = new CarInsureQueryCompanyHistoryBC().getInfoByCarInsureQueryHistoryID(carInsureQueryHistoryInfo.CarInsureQueryHistoryID);
            var id = Check.getIns().isEmpty(carInsureQueryHistoryInfo.OldCarInsureQueryHistoryID) ? carInsureQueryHistoryInfo.CarInsureQueryHistoryID : carInsureQueryHistoryInfo.OldCarInsureQueryHistoryID;
            var carInsuranceQueryTaskDatas = new CarInsuranceQueryTaskBC().GetListByTaskTypeID(id);
            var carInsuranceQueryTaskDetailDatas = new CarInsuranceQueryTaskDetailBC().GetListByTaskTypeID(id);
            //var formulaDatas = (new TFormulaBC()).getFormulas(carInsureQueryHistoryInfo.RunCity, TFormulaBC.FormulaCodes.ReturnPoint);
            //var userFormulaDatas = (new TFormulaBC()).getFormulas(TFormulaBC.FormulaCodes.CommissionPointCustomer);

            Parallel.ForEach(carInsuranceQueryTaskDatas, (data) =>
            {
                try
                {
                    if (data.IsFinished != 1) return;

                    //线上报价出错
                    //如果一小时内重试次数未足5次的，重新尝试报价
                    if (isQuote)
                    {
                        if (data.IsFinished == 1
                        && !Check.getIns().isEmpty(data.ReturnErrorCode)
                        && data.BusinessType != 1)
                        {
                            ////已重试次数
                            //var retryCount = new CarInsuranceQueryTaskBC().GetTaskReturnErrorCount(data.CarInsuranceQueryTaskID, 1);
                            //if (retryCount < 5) return;
                        }
                    }

                    var model = new CarInsuranceAPIModel();

                    model.IsQuoteCompleted = true;//该公司报价完成

                    model.QueryTaskID = data.CarInsuranceQueryTaskID;
                    model.CompanyName = data.CompanyName;
                    model.CompanyCode = data.CompanyCode;
                    model.CompanyUrl = data.CompanyUrl;
                    model.Tel = data.CompanyTel;

                    //判断是否有选择投保该公司
                    if (companys.Count((e) => { return e.CompanyCode.ToUpper().Equals(model.CompanyCode.ToUpper()); }) <= 0) return;

                    model.ReturnErrorCode = data.ReturnErrorCode == "0" ? "" : data.ReturnErrorCode.toString();
                    //model.ReturnErrorCodeJQX = data.ReturnErrorCodeJQX.toString() == "0" ? "" : data.ReturnErrorCodeJQX.toString();
                    model.InsureQueryID = data.ReturnSignID;
                    model.BrandOrderNum = data.Priority;
                    //是的话 要出电话 联系官网 不是的话 直接投保
                    model.VisitType = "1";
                    if (!Check.getIns().isEmpty(data.VisitType))
                    {
                        model.VisitType = data.VisitType.ToLower().IndexOf("qinyibao") > -1 ? "0" : "1";
                    }

                    var carInsureQueryTaskBc = new CarInsuranceQueryTaskBC();
                    string strErrorCode = model.ReturnErrorCode.toString();
                    if (strErrorCode.Equals("0") || strErrorCode.Equals(""))
                    {
                        foreach (var detailData in carInsuranceQueryTaskDetailDatas)
                        {
                            if (!data.CompanyID.Equals(detailData.CompanyID)) continue;
                            model.QuoteDateTime = detailData.CreateTime.Value;
                            #region 报价
                            switch (detailData.ProductID.ToUpper())
                            {
                                case "63":
                                    //机动车损失保险
                                    model.CarDamageInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "A63":
                                    //机动车损失保险 不计免赔
                                    model.CarDamageInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "68":
                                    //第三者责任保险
                                    model.ThirdPartyInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "A68":
                                    //第三者责任保险 不计免赔
                                    model.ThirdPartyInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "74":
                                    //全国盗抢险
                                    model.CarRobberyInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "A74":
                                    //全国盗抢险 不计免赔
                                    model.CarRobberyInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "73":
                                    //车上人员责任险（司机）
                                    model.CarDriverInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "A73":
                                    //车上人员责任险（司机） 不计免赔
                                    model.CarDriverInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "89":
                                    //车上人员责任险（乘客）
                                    model.CarPassengerInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "A89":
                                    //车上人员责任险（乘客） 不计免赔
                                    model.CarPassengerInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "A7389":
                                    //车上人员责任险（司机+乘客） 不计免赔
                                    model.CarPersonnelInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.MainDeductable = true;
                                    break;
                                case "34":
                                    //玻璃破碎险
                                    model.CarGlassInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "75":
                                    //车身划痕险
                                    model.CarScratchInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "77":
                                    //发动机涉水险
                                    model.CarEngineInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "82":
                                    //自燃
                                    model.CarFireInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "200":
                                    //交强
                                    model.StrongInsurance = detailData.EndProductPrice.ToString();
                                    break;
                                case "PCCARSHIPTAXINFODTO":
                                    //车船税
                                    model.CarShipTax = detailData.EndProductPrice.ToString();
                                    model.Description = detailData.Description;
                                    break;
                                //denglei 获取附加险不计免赔保费 20151012  begin
                                case "A75":
                                    model.CarScratchInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.SubDeductable = true;
                                    break;
                                case "A82":
                                    model.CarFireInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.SubDeductable = true;
                                    break;
                                case "A77":
                                    model.CarEngineInsuranceDeductable = detailData.EndProductPrice.ToString();
                                    model.SubDeductable = true;
                                    break;
                                //denglei end
                                case "85":
                                    if (model.CompanyCode != "Insure_05TaiPingYang")
                                    {
                                        model.IsSupportFactory = detailData.EndProductPrice.toInt32() > 0;
                                        model.ServiceFactoryInsurance = detailData.EndProductPrice.ToString();
                                    }
                                    break;
                            }
                            #endregion
                        }

                        //判断报价是否全部有效/包含
                        model = IsErrorQuote(data, carInsureQueryHistoryInfo, model);

                        //计算返利
                        string errorMsg = "";
                        //model.Point = new TuserAmountBC().SetTuserAmountByBaoJia(carInsureQueryHistoryInfo.RunCity, data.CompanyID,
                        //    model.BusinessTotal,
                        //    model.StrongInsurance.toDecimal(),
                        //    model.CarShipTax.toDecimal(),
                        //    carInsureQueryHistoryInfo.UserID,
                        //    out errorMsg);

                        if (!Check.getIns().isEmpty(errorMsg))
                        {
                            throw new Exception(errorMsg);
                        }

                        //报价有效时间为48小时
                        model.IsEnable = false;
                        if (!Check.getIns().isEmpty(model.QuoteDateTime))
                        {
                            model.IsEnable = Function.getIns().dateDiff("hh", model.QuoteDateTime, DateTime.Now) <= 48;
                        }
                        model.Total = model.BusinessTotal + model.StrongInsurance.toDecimal() + model.CarShipTax.toDecimal();
                        model.SubDeductableTotal = model.CarScratchInsuranceDeductable.toDecimal() +
                                                   +model.CarEngineInsuranceDeductable.toDecimal() +
                                                   model.CarFireInsuranceDeductable.toDecimal();
                        model.MainDeductableTotal = model.CarDamageInsuranceDeductable.toDecimal()
                                                    + model.CarDriverInsuranceDeductable.toDecimal()
                                                    + model.CarRobberyInsuranceDeductable.toDecimal()
                                                    + model.CarPassengerInsuranceDeductable.toDecimal()
                                                    + model.CarPersonnelInsuranceDeductable.toDecimal()
                                                    + model.ThirdPartyInsuranceDeductable.toDecimal();





                        //报价成功后修改Task表的报价字段 denglei 2015-11-03 begin
                        var taskInfo = carInsureQueryTaskBc.SelectByPrimaryKey(data.CarInsuranceQueryTaskID);
                        if (!Check.getIns().isEmpty(taskInfo))
                        {
                            //taskInfo.BackAmount = model.Point;
                            taskInfo.BusinessAmount = model.BusinessTotal;
                            //taskInfo.StrongCarShipTaxAmount = model.StrongInsurance.toDecimal() + model.CarShipTax.toDecimal();
                            taskInfo.OrderTotalAmount = model.Total;
                            carInsureQueryTaskBc.Update(taskInfo);
                        }
                        //报价成功后修改Task表的报价字段 denglei 2015-11-03 end

                        //关于指定驾驶员，指定专修，指定行驶城市的情况
                        /**
                         *                  天安 	中银  	阳光	    安盛	    太平
                         * 指定行驶城市	    √	     X	    √	    X	    X
                         * 是否指定驾驶员	√	     √	    √	    X	    X
                         */
                        model.IsSupportDriveArea = true;
                        if (model.CompanyCode == "Insure_51ZhongYin" || model.CompanyCode == "Insure_07AnShen" || model.CompanyCode == "Insure_06TaiPing")
                        {
                            model.IsSupportDriveArea = false;
                        }
                        model.IsSupportCarDriver = true;
                        if (model.CompanyCode == "Insure_07AnShen" || model.CompanyCode == "Insure_06TaiPing")
                        {
                            model.IsSupportCarDriver = false;
                        }

                        if (model.CompanyCode != "Insure_03YangGuang" && model.CompanyCode != "Insure_05TaiPingYang")
                        {
                            if (result.StrongEndPrice <= 0)
                                result.StrongEndPrice = model.StrongInsurance.toDecimal();
                            if (result.CarShipTaxEndPrice <= 0)
                                result.CarShipTaxEndPrice = model.CarShipTax.toDecimal();
                        }
                        //model.IsServiceFactory = carInsureQueryHistoryInfo.IsServiceFactory.GetValueOrDefault() == 1;
                        //model.IsCarDriver = carInsureQueryHistoryInfo.IsCarDriver.GetValueOrDefault() == 1;
                        //model.IsDriveArea = carInsureQueryHistoryInfo.IsDriveArea.GetValueOrDefault() == 1;
                    }

                    result.CarInsuranceDataList.Add(model);
                }
                catch (Exception ex)
                {
                    //LogController.writeErrorLog(ex, "InsureHistoryHelper");
                    debug_Log.Call_WriteLog(ex.Message, "InsureHistoryHelper", "queryCarInsureQueryInfo");
                }
            });
            #endregion

            foreach (var temp in
                    result.CarInsuranceDataList.Where(
                        x => x.CompanyCode == "Insure_02RenBao" || x.CompanyCode == "Insure_101RenBao" || x.CompanyCode == "Insure_05TaiPingYang"))
            {
                if (temp.StrongInsurance.toDecimal() <= 0)
                {
                    temp.StrongInsurance = result.StrongEndPrice > 0 ? result.StrongEndPrice.toString() : "";
                }
                if (temp.CarShipTax.toDecimal() <= 0)
                {
                    temp.CarShipTax = result.CarShipTaxEndPrice > 0 ? result.CarShipTaxEndPrice.toString() : "";
                }
                temp.Total = temp.BusinessTotal + temp.StrongInsurance.toDecimal() + temp.CarShipTax.toDecimal();
                temp.IsErrorStrongInsurance = false;
                temp.IsErrorCarShipTax = false;
            }
            if (companys.Count(x => x.CompanyCode != "Insure_02RenBao" && x.CompanyCode != "Insure_101RenBao" && x.CompanyCode != "Insure_05TaiPingYang")
                == result.CarInsuranceDataList.Count(x => x.CompanyCode != "Insure_02RenBao" && x.CompanyCode != "Insure_101RenBao" && x.CompanyCode != "Insure_05TaiPingYang"))
            {
                result.HasFinished = true;
                if (result.StrongEndPrice <= 0)
                    result.HasErrorForStrongInsure = true;
                if (result.CarShipTaxEndPrice <= 0)
                    result.HasErrorForCarShipTax = true;
            }
            return result;
        }


        /// <summary>
        /// 判断报价是否全部有效/包含
        /// </summary>
        private CarInsuranceAPIModel IsErrorQuote(CarInsuranceQueryTaskInfoExt taskInfo, CarInsureQueryHistoryInfo historyInfo, CarInsuranceAPIModel model)
        {
            if (!IsCheckCompany(taskInfo))
            {
                return model;
            }
            #region 主险
            //投保检查
            //主险
            model.IsErrorCarDamageInsurance = IsInsureItem("CarDamageInsurance", historyInfo);//机动车损失保险
            model.IsErrorThirdPartyInsurance = IsInsureItem("ThirdPartyInsurance", historyInfo);//第三者责任保险
            model.IsErrorCarRobberyInsurance = IsInsureItem("CarRobberyInsurance", historyInfo);//全国盗抢险
            model.IsErrorCarDriverInsurance = IsInsureItem("CarDriverInsurance", historyInfo);//车上人员责任险（司机）
            model.IsErrorCarPassengerInsurance = IsInsureItem("CarPassengerInsurance", historyInfo);//车上人员责任险（乘客）

            //不记免赔
            model.IsErrorCarDamageInsuranceDeductable = IsInsureItem("CarDamageInsuranceDeductable", historyInfo);//机动车损失保险
            model.IsErrorThirdPartyInsuranceDeductable = IsInsureItem("ThirdPartyInsuranceDeductable", historyInfo);//第三者责任保险
            model.IsErrorCarRobberyInsuranceDeductable = IsInsureItem("CarRobberyInsuranceDeductable", historyInfo);//全国盗抢险
            model.IsErrorCarDriverInsuranceDeductable = IsInsureItem("CarDriverInsuranceDeductable", historyInfo);//车上人员责任险（司机）
            model.IsErrorCarPassengerInsuranceDeductable = IsInsureItem("CarPassengerInsuranceDeductable", historyInfo);//车上人员责任险（乘客）
            #endregion 主险

            #region 附加险

            //附加险
            model.IsErrorCarGlassInsurance = IsInsureItem("CarGlassInsurance", historyInfo);//玻璃破碎险
            model.IsErrorCarScratchInsurance = IsInsureItem("CarScratchInsurance", historyInfo);//车身划痕险
            model.IsErrorCarEngineInsurance = IsInsureItem("CarEngineInsurance", historyInfo);//发动机涉水险
            model.IsErrorCarFireInsurance = IsInsureItem("CarFireInsurance", historyInfo);//自燃损失险
            model.IsErrorcannotFindThird = IsInsureItem("cannotFindThird", historyInfo);
            model.IsErrorSheBeiSunShiXian = IsInsureItem("SheBeiSunShiXian", historyInfo);
            model.IsErrorXiuLiQiJianFeiYongBuChangXian = IsInsureItem("XiuLiQiJianFeiYongBuChangXian", historyInfo);
            model.IsErrorCheShangHuoWuZeRenXian = IsInsureItem("CheShangHuoWuZeRenXian", historyInfo);
            model.IsErrorJinShenSunHaiFuWeiJinZeRenXian = IsInsureItem("JinShenSunHaiFuWeiJinZeRenXian", historyInfo);
            model.IsErrorBuJiMianPeiLvXian = IsInsureItem("BuJiMianPeiLvXian", historyInfo);
        
            
                      

            model.IsErrorCarGlassInsuranceDeductable = IsInsureItem("CarGlassInsuranceDeductable", historyInfo);//玻璃破碎险
            model.IsErrorCarScratchInsuranceDeductable = IsInsureItem("CarScratchInsuranceDeductable", historyInfo);//车身划痕险
            model.IsErrorCarEngineInsuranceDeductable = IsInsureItem("CarEngineInsuranceDeductable", historyInfo);//发动机涉水险
            model.IsErrorCarFireInsuranceDeductable = IsInsureItem("CarFireInsuranceDeductable", historyInfo);//自燃损失险
            model.IsErrorcannotFindThirdDeductable = IsInsureItem("cannotFindThirdDeductable", historyInfo);
            model.IsErrorSheBeiSunShiXianDeductable = IsInsureItem("SheBeiSunShiXianDeductable", historyInfo);
            model.IsErrorXiuLiQiJianFeiYongBuChangXianDeductable = IsInsureItem("XiuLiQiJianFeiYongBuChangXianDeductable", historyInfo);
            model.IsErrorCheShangHuoWuZeRenXianDeductable = IsInsureItem("CheShangHuoWuZeRenXianDeductable", historyInfo);
            model.IsErrorJinShenSunHaiFuWeiJinZeRenXianDeductable = IsInsureItem("JinShenSunHaiFuWeiJinZeRenXianDeductable", historyInfo);
            model.IsErrorBuJiMianPeiLvXianDeductable = IsInsureItem("BuJiMianPeiLvXianDeductable", historyInfo);


            #endregion 附加险
            /// 交强险
            model.IsErrorStrongInsurance = IsInsureItem("StrongInsurance", historyInfo);
            /// 车船税
            model.IsErrorCarShipTax = IsInsureItem("StrongInsurance", historyInfo);


            #region 主险
            //保费检查
            //主险
            model.IsErrorCarDamageInsurance &= model.CarDamageInsurance.toDecimal() <= 0;//机动车损失保险
            model.IsErrorThirdPartyInsurance &= model.ThirdPartyInsurance.toDecimal() <= 0;//第三者责任保险
            model.IsErrorCarRobberyInsurance &= model.CarRobberyInsurance.toDecimal() <= 0;//全国盗抢险
            model.IsErrorCarDriverInsurance &= model.CarDriverInsurance.toDecimal() <= 0;//车上人员责任险（司机）
            model.IsErrorCarPassengerInsurance &= model.CarPassengerInsurance.toDecimal() <= 0;//车上人员责任险（乘客）

            //不记免赔
            model.IsErrorCarDamageInsuranceDeductable &= model.CarDamageInsuranceDeductable.toDecimal() <= 0;//机动车损失保险
            model.IsErrorThirdPartyInsuranceDeductable &= model.ThirdPartyInsuranceDeductable.toDecimal() <= 0;//第三者责任保险
            model.IsErrorCarRobberyInsuranceDeductable &= model.CarRobberyInsuranceDeductable.toDecimal() <= 0;//全国盗抢险
            model.IsErrorCarDriverInsuranceDeductable &= model.CarDriverInsuranceDeductable.toDecimal() <= 0 && model.CarPersonnelInsuranceDeductable.toDecimal() <= 0;//车上人员责任险（司机）
            model.IsErrorCarPassengerInsuranceDeductable &= model.CarPassengerInsuranceDeductable.toDecimal() <= 0 && model.CarPersonnelInsuranceDeductable.toDecimal() <= 0;//车上人员责任险（乘客）
            #endregion 主险

            #region 附加险
            //附加险
            model.IsErrorCarGlassInsurance &= model.CarGlassInsurance.toDecimal() <= 0;//玻璃破碎险
            model.IsErrorCarScratchInsurance &= model.CarScratchInsurance.toDecimal() <= 0;//车身划痕险
            model.IsErrorCarEngineInsurance &= model.CarEngineInsurance.toDecimal() <= 0;//发动机涉水险
            model.IsErrorCarFireInsurance &= model.CarFireInsurance.toDecimal() <= 0;//自燃损失险
            model.IsErrorcannotFindThird &= model.cannotFindThird.toDecimal() <= 0;
            model.IsErrorSheBeiSunShiXian &= model.SheBeiSunShiXian.toDecimal() <= 0;
            model.IsErrorXiuLiQiJianFeiYongBuChangXian &= model.XiuLiQiJianFeiYongBuChangXian.toDecimal() <= 0;
            model.IsErrorCheShangHuoWuZeRenXian &= model.CheShangHuoWuZeRenXian.toDecimal() <= 0;
            model.IsErrorJinShenSunHaiFuWeiJinZeRenXian &= model.JinShenSunHaiFuWeiJinZeRenXian.toDecimal() <= 0;
            model.IsErrorBuJiMianPeiLvXian &= model.BuJiMianPeiLvXian.toDecimal() <= 0;
          
            model.IsErrorCarGlassInsuranceDeductable &= model.CarGlassInsuranceDeductable.toDecimal() <= 0;//玻璃破碎险
            model.IsErrorCarScratchInsuranceDeductable &= model.CarScratchInsuranceDeductable.toDecimal() <= 0;//车身划痕险
            model.IsErrorCarEngineInsuranceDeductable &= model.CarEngineInsuranceDeductable.toDecimal() <= 0;//发动机涉水险
            model.IsErrorCarFireInsuranceDeductable &= model.CarFireInsuranceDeductable.toDecimal() <= 0;//自燃损失险

            model.IsErrorcannotFindThirdDeductable &= model.cannotFindThirdDeductable.toDecimal() <= 0;
            model.IsErrorSheBeiSunShiXianDeductable &= model.SheBeiSunShiXianDeductable.toDecimal() <= 0;
            model.IsErrorXiuLiQiJianFeiYongBuChangXianDeductable &= model.XiuLiQiJianFeiYongBuChangXianDeductable.toDecimal() <= 0;
            model.IsErrorCheShangHuoWuZeRenXianDeductable &= model.CheShangHuoWuZeRenXianDeductable.toDecimal() <= 0;
            model.IsErrorJinShenSunHaiFuWeiJinZeRenXianDeductable &= model.JinShenSunHaiFuWeiJinZeRenXianDeductable.toDecimal() <= 0;
            model.IsErrorBuJiMianPeiLvXianDeductable &= model.BuJiMianPeiLvXianDeductable.toDecimal() <= 0;
            #endregion

            /// 交强险
            model.IsErrorStrongInsurance &= model.StrongInsurance.toDecimal() <= 0;
            /// 车船税
            model.IsErrorCarShipTax &= model.CarShipTax.toDecimal() <= 0;

            return model;
        }

        /// <summary>
        /// 是否是要检查的公司
        /// </summary>
        private bool IsCheckCompany(CarInsuranceQueryTaskInfoExt taskInfo)
        {
            return "Insure_02RenBao".ToUpper().Equals(taskInfo.CompanyCode.Trim().ToUpper());
        }

        /// <summary>
        /// 项目是否投保
        /// </summary>
        private bool IsInsureItem(string key, CarInsureQueryHistoryInfo historyInfo)
        {
            switch (key)
            {
                #region 主险
                case "CarDamageInsurance": return !historyInfo.CarDamageInsurance.Equals("0"); //机动车损失保险
                case "ThirdPartyInsurance": return !historyInfo.ThirdPartyInsurance.Equals("0");//第三者责任保险
                case "CarRobberyInsurance": return !historyInfo.CarRobberyInsurance.Equals("0");//全国盗抢险
                case "CarDriverInsurance": return !historyInfo.CarDriverInsurance.Equals("0");//车上人员责任险（司机）
                case "CarPassengerInsurance": return !historyInfo.CarPassengerInsurance.Equals("0");//车上人员责任险（乘客）

                //不记免赔
                case "CarDamageInsuranceDeductable": return !historyInfo.CarDamageInsuranceDeductable.toString().Equals("0");//机动车损失保险
                case "ThirdPartyInsuranceDeductable": return !historyInfo.ThirdPartyInsuranceDeductable.toString().Equals("0");//第三者责任保险
                case "CarRobberyInsuranceDeductable": return !historyInfo.CarRobberyInsuranceDeductable.toString().Equals("0");//全国盗抢险
                case "CarDriverInsuranceDeductable": return !historyInfo.CarDriverInsuranceDeductable.toString().Equals("0");//车上人员责任险（司机）
                case "CarPassengerInsuranceDeductable": return !historyInfo.CarPassengerInsuranceDeductable.toString().Equals("0");//车上人员责任险（乘客））
                #endregion 主险

                #region 附加险
                //附加险
                case "CarGlassInsurance": return !historyInfo.CarGlassInsurance.Equals("0");//玻璃破碎险
                case "CarScratchInsurance": return !historyInfo.CarScratchInsurance.Equals("0");//车身划痕险
                case "CarEngineInsurance": return !historyInfo.CarEngineInsurance.Equals("0");//发动机涉水险
                case "CarFireInsurance": return !historyInfo.CarFireInsurance.Equals("0");//自燃损失险
                case "cannotFindThird": return !historyInfo.CannotFindThird.Equals("0");
                case "SheBeiSunShiXian": return !historyInfo.SheBeiSunShiXian.Equals("0");
                case "XiuLiQiJianFeiYongBuChangXian": return !historyInfo.XiuLiQiJianFeiYongBuChangXian.Equals("0");
                case "CheShangHuoWuZeRenXian": return !historyInfo.CheShangHuoWuZeRenXian.Equals("0");
                case "JinShenSunHaiFuWeiJinZeRenXian": return !historyInfo.JinShenSunHaiFuWeiJinZeRenXian.Equals("0");
                case "BuJiMianPeiLvXian": return !historyInfo.BuJiMianPeiLvXian.Equals("0");

                case "CarGlassInsuranceDeductable": return !historyInfo.CarGlassInsuranceDeductable.Equals("0");
                case "CarScratchInsuranceDeductable": return !historyInfo.CarScratchInsuranceDeductable.Equals("0");
                case "CarEngineInsuranceDeductable": return !historyInfo.CarEngineInsuranceDeductable.Equals("0");
                case "CarFireInsuranceDeductable": return !historyInfo.CarFireInsuranceDeductable.Equals("0");
                case "cannotFindThirdDeductable": return !historyInfo.CannotFindThirdDeductable.Equals("0");
                case "SheBeiSunShiXianDeductable": return !historyInfo.SheBeiSunShiXianDeductable.Equals("0");
                case "XiuLiQiJianFeiYongBuChangXianDeductable": return !historyInfo.XiuLiQiJianFeiYongBuChangXianDeductable.Equals("0");
                case "CheShangHuoWuZeRenXianDeductable": return !historyInfo.CheShangHuoWuZeRenXianDeductable.Equals("0");
                case "JinShenSunHaiFuWeiJinZeRenXianDeductable": return !historyInfo.JinShenSunHaiFuWeiJinZeRenXianDeductable.Equals("0");
                case "BuJiMianPeiLvXianDeductable": return !historyInfo.BuJiMianPeiLvXianDeductable.Equals("0");
                #endregion 主险
                /// 交强险
                case "StrongInsurance": return !historyInfo.StrongInsurance.toString().Equals("0");
            }

            return false;
        }
    }
}