using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassInsureBLL.Model_Public;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.LogsEveryDay;
using InsureApi.Entity;
using InsureApi.BLL;
using InsureApi.Common.Common;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Text;
using DALMongoDbHelper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Web_Service.Api.LogsEveryDay
{
    public class Logs_EveryDay : Insure.Common.Logs_EveryDay
    {
        public ObjectId _id { get; set; }

    }
    public class LogsEveryDayController : BasicController
    {


        static Dictionary<String, Int32> pRecordCountList = new Dictionary<String, Int32>();
        [HttpPost]
        public ResultAPIModel<List<LogsEveryDayApiModel.LogsEveryDayInfo>> Post(InsureApi.WebApi.Model.Common.ResultAPIModel<LogsEveryDayApiModel.LogsEveryDaySearchInfo> m)
        {
            var result = new ResultAPIModel<List<LogsEveryDayApiModel.LogsEveryDayInfo>>();
            try
            {
                result.data = new List<LogsEveryDayApiModel.LogsEveryDayInfo>();

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

                //LogsEveryDayInfoExt searchModel = new LogsEveryDayInfoExt
                //{
                //    BeginTime = m.data.BeginTime,
                //    EndTime = m.data.EndTime,
                //    LogsSubject = m.data.LogsSubject,
                //    LogsSubSubject = m.data.LogsSubSubject,
                //    LogsContent = m.data.LogsContent,
                //    PageNumber = m.paging.pageNumber,
                //    PageSize = m.paging.pageSize
                //};


                MongoDbHelper mh = new MongoDbHelper("219.235.15.144", "100");

                DateTime DateTimeInput;
                if (m.data.EndTime.Value.Hour == 0)
                {
                    DateTimeInput = m.data.EndTime.Value.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    DateTimeInput = Convert.ToDateTime(m.data.EndTime);
                }

                IMongoQuery query = null;

                //query = Query.And(
                //    Query.GTE("CreateTime", m.data.BeginTime),
                //    Query.LTE("CreateTime", DateTimeInput)
                //    );
                //if (String.IsNullOrEmpty(m.data.LogsSubject.toString()) == false) {
                query = Query.And(
              Query.GTE("CreateTime", m.data.BeginTime),
              Query.LTE("CreateTime", DateTimeInput),
              Query.Matches("Logs_Subject", m.data.LogsSubject.toString()),
              Query.Matches("Logs_SubSubject", m.data.LogsSubSubject.toString()),
              Query.Matches("Logs_Content", m.data.LogsContent.toString())
              );
                //}
                //String.IsNullOrEmpty(m.data.LogsSubject.toString()) ? null : Query.EQ("Logs_Subject", m.data.LogsSubject.toString()),
                //    String.IsNullOrEmpty(m.data.LogsSubSubject.toString()) ? null : Query.EQ("Logs_SubSubject", m.data.LogsSubSubject.toString())


                #region
                //            MongoDB中返回指定的字段的查询方法如下：
                //            在MongoDB中，我们同样可以创建复合索引，如：
                //--数字1表示username键的索引按升序存储，-1表示age键的索引按照降序方式存储。
                //> db.getCollection('Logs_EveryDay').ensureIndex({ "CreateTime":-1})
                //db.person.find({ Name: "小丑"},{ Age: 1,Sex: 1})   db.getCollection('Logs_EveryDay').count()  索引的创建db.getCollection('Logs_EveryDay').ensureIndex({"Logs_Subject":1}).ensureIndex({"Logs_Content":1}).ensureIndex({"Logs_SubSubject":1})
                ///db.getCollection('Logs_EveryDay').getIndexes() 

                //该语句表示：查询person表中name为小丑的所有数据，但是只返回age列和sex列。（_id列是默认返回的，设为0表示不返回）
                FieldsDocument fd = new FieldsDocument();
                fd.Add("CreateTime", 1);
                fd.Add("Logs_Subject", 1);
                fd.Add("Logs_SubSubject", 1); //只返回Name和Sex列
                #endregion

                // SortByDocument sortCreateTime = new SortByDocument { { "CreateTime", -1 } };
                //fd = null;
                //IMongoQuery my = Query.Matches("LogsSubject", "Oliver");
                //IMongoQuery my = Query.Matches("LogsContent", "{$regex:/徐.*/i}");
                //IMongoQuery my = Query.EQ("listRecord", "2");
                List<Logs_EveryDay> listRecord = mh.Find<Logs_EveryDay>(query, m.paging.pageNumber, m.paging.pageSize, null, fd);

                //List<LogsEveryDayInfoExt> listRecord = (new LogsEveryDayBC()).SelectLogsEveryDayList(searchModel);

                //组织数据
                var dataCount = 0;
                if (listRecord != null && listRecord.Count > 0)
                {
                    StringBuilder myStringBuilder = new StringBuilder();
                    myStringBuilder.Append(m.data.BeginTime.toString());
                    myStringBuilder.Append(DateTimeInput.toString());
                    myStringBuilder.Append(m.data.LogsSubject.toString());
                    myStringBuilder.Append(m.data.LogsSubSubject.toString());
                    myStringBuilder.Append(m.data.LogsContent.toString());
                    string strMaker = Insure.Common.DESCrypt.GetMd5Str32(myStringBuilder.ToString());
                    if (pRecordCountList.ContainsKey(strMaker))
                    {
                        dataCount = pRecordCountList[strMaker];
                    }
                    else {
                        Int32 Int32RecordCount = mh.GetCount<Logs_EveryDay>(query, "Logs_EveryDay").toInt32();
                        pRecordCountList.Add(strMaker, Int32RecordCount);
                        dataCount = Int32RecordCount;
                    }
                    // dataCount = 


                    //dataCount = 1111111111;// mh.GetCount<Logs_EveryDay>(query, "Logs_EveryDay").toInt32();
                    foreach (var data in listRecord)
                    {
                        var model = new LogsEveryDayApiModel.LogsEveryDayInfo();
                        model.CreateTime = data.CreateTime;
                        model.LogsSubject = data.Logs_Subject;
                        model.LogsSubSubject = data.Logs_SubSubject;
                        model.LogsEveryDayID = data._id.toString();

                        // Function.getIns().copyValue(data, model);
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
        public ResultAPIModel<LogsEveryDayApiModel.LogsEveryDayInfo> GetLogsEveryDayByID(string id)
        {
            string apiUrl = "get api/LogsEveryDay/LogsEveryDay";
            var result = new ResultAPIModel<LogsEveryDayApiModel.LogsEveryDayInfo>();
            try
            {
                result.data = new LogsEveryDayApiModel.LogsEveryDayInfo();

                if (string.IsNullOrEmpty(id))
                {
                    result.resultMessage = new ResultMessageAPIModel()
                    {
                        code = (int)ResultMessageAPIModel.Codes.fail,
                        errorMessage = "无效的查询条件"
                    };
                }

                //var item = new LogsEveryDayBC().SelectByPrimaryKey(id);
                IMongoQuery query = null;
                query = Query.And(
                    Query.EQ("_id", new ObjectId(id))
                    );
                MongoDbHelper mh = new MongoDbHelper("219.235.15.144", "100");
                Logs_EveryDay myLogs_EveryDay = mh.FindOne<Logs_EveryDay>(query, "Logs_EveryDay");

                result.data.CreateTime = myLogs_EveryDay.CreateTime;
                result.data.LogsSubject = myLogs_EveryDay.Logs_Subject;
                result.data.LogsSubSubject = myLogs_EveryDay.Logs_SubSubject;
                result.data.LogsContent = myLogs_EveryDay.Logs_Content;
                //Function.getIns().copyValue(item, result.data);
                if (result.data.LogsContent != null)
                {
                    if (result.data.LogsContent.StartsWith("{"))
                    {
                        result.data.LogsContent = ConvertJsonString(result.data.LogsContent);
                    }
                    else if (result.data.LogsContent.StartsWith("<?xml"))
                    {
                        result.data.LogsContent = FormatXml(myLogs_EveryDay.Logs_Content);
                    }

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
                        Formatting = Newtonsoft.Json.Formatting.Indented,
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



        private string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = System.Xml.Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }
    }
}