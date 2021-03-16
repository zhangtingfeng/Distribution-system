using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassInsureBLL.Model_Public;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.ActionRecord;
using InsureApi.Entity;
using InsureApi.BLL;
using InsureApi.Common.Common;
using Newtonsoft.Json;
using System.IO;

namespace Web_Service.Api.ActionRecord
{
    public class ActionRecordController : BasicController
    {
        [HttpPost]
        public ResultAPIModel<List<ActionRecordApiModel.ActionRecordInfo>> Post(ResultAPIModel<ActionRecordApiModel.ActionRecordSearchInfo> m)
        {
            var result = new ResultAPIModel<List<ActionRecordApiModel.ActionRecordInfo>>();
            try
            {
                result.data = new List<ActionRecordApiModel.ActionRecordInfo>();

                string msg = "";
                if (!m.IsValidUserInfo(out msg)
                    || !m.IsValidPagingInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                //页数从1开始
                if (m.paging.pageNumber <= 0)
                {
                    m.paging.pageNumber = 1;
                }

                #region 业务数据查询

                APIActionRecordExt searchModel = new APIActionRecordExt
                {
                     LicenseNo=m.data.LicenseNo,
                    BeginTime = m.data.BeginTime,
                    EndTime = m.data.EndTime,
                    ChannelID = m.data.ChannelID,
                    ActionType = m.data.ActionType,
                    TransactionNumber = m.data.TransactionNumber,
                    SerialDecimal = m.data.SerialDecimal,
                    PageNumber = m.paging.pageNumber,
                    PageSize = m.paging.pageSize
                };

                List<APIActionRecordExt> listRecord = (new APIActionRecordBC()).SelectActionRecordList(searchModel);

                //组织数据
                var dataCount = 0;
                if (listRecord != null && listRecord.Count > 0)
                {
                    dataCount = listRecord.First().DataCount;
                    foreach (var data in listRecord)
                    {
                        var model = new ActionRecordApiModel.ActionRecordInfo();
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
                result.resultMessage.code = (int)ResultMessageAPIModel.Codes.fail;
                result.resultMessage.errorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据ID获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultAPIModel<ActionRecordApiModel.ActionRecordInfo> GetActionRecordByID(string id)
        {
            string apiUrl = "get api/ActionRecord/ActionRecord";
            var result = new ResultAPIModel<ActionRecordApiModel.ActionRecordInfo>();
            try
            {
                result.data = new ActionRecordApiModel.ActionRecordInfo();

                if (string.IsNullOrEmpty(id))
                {
                    result.resultMessage = new ResultMessageAPIModel()
                    {
                        code = (int)ResultMessageAPIModel.Codes.fail,
                        errorMessage = "无效的查询条件"
                    };
                }

                var item = new APIActionRecordBC().SelectByPrimaryKey(id);

                Function.getIns().copyValue(item, result.data);
                if (result.data.JsonString.StartsWith("{"))
                {
                    result.data.JsonString = ConvertJsonString(result.data.JsonString);
                }
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

        private string ConvertJsonString(string str)
        {
            try
            {
                //格式化json字符串
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
                else
                {
                    return str;
                }
            }
            catch (Exception ex)
            {
                return str;
            }
        }
    }
}