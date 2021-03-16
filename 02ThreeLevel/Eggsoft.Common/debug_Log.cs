using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.IO;
using System.Text;


namespace Eggsoft.Common
{

    public class Debug_Write
    {
        public static void Write(string msg)
        {
            HttpContext.Current.Response.Write(msg);
        }

        public static void WriteEnd(string msg)
        {
            HttpContext.Current.Response.Write(msg);
            HttpContext.Current.Response.End();
        }
    }






    /// <summary>
    ///Subscribe 的摘要说明
    /// </summary>
    public class debug_Log
    {
        /// <summary>
        /// 写日志(用于跟踪)
        /// </summary>
        public static string Call_WriteLog(Exception error, string strTypename = "", string strLogs_SubSubject = "")
        {
            string strReturn = "";

            try
            {
                DateTime dt = DateTime.Now;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼");
                sb.AppendLine("Error Msg：" + error.GetBaseException().Message);
                sb.AppendLine("Stack：" + error.GetBaseException().StackTrace);
                sb.AppendLine("Ect：");
                sb.AppendLine(error.Message.ToString());
                sb.AppendLine(error.Source);
                sb.AppendLine(error.StackTrace);
                sb.AppendLine("▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲");
                string strXML = sb.ToString();
                string strMemo = dt.ToString("yyyy-MM-dd HH:mm:ss fff") + ":" + strXML;
                strReturn = strMemo;

                Call_WriteLog(strMemo, strLogs_SubSubject, strLogs_SubSubject);


            }
            catch
            {
            }
            finally
            {
                //if (sr != null)
                //    sr.Close();
            }
            return strReturn;
        }


        /// <summary>
        /// 写日志(用于跟踪)
        /// </summary>
        public static void Call_WriteLog(string strXML, string strLogs_SubSubject = "", string strTypename = "WX")
        {
            // ClLogHelper.WriteLog(strXML.GetType(), strLogs_SubSubject + strXML);

            try
            {


                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    string strPostURL = System.Configuration.ConfigurationManager.AppSettings["WriteLogUrl"];
                    if (!string.IsNullOrEmpty(strPostURL))
                    {
                        if (string.IsNullOrEmpty(strTypename)) strTypename = "微系统";
                        if (string.IsNullOrEmpty(strLogs_SubSubject)) strLogs_SubSubject = "默认";

                        Eggsoft.Common.Logs_EveryDay thisLogs_EveryDay = new Eggsoft.Common.Logs_EveryDay();
                        thisLogs_EveryDay.Logs_Subject = strTypename;
                        thisLogs_EveryDay.Logs_SubSubject = strLogs_SubSubject;
                        thisLogs_EveryDay.Logs_Content = strXML;

                        try
                        {
                            string strJsonSerializer = Eggsoft.Common.JsonHelper.JsonSerializer<Eggsoft.Common.Logs_EveryDay>(thisLogs_EveryDay);
                            String ster = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_Post_JSON(strPostURL, strJsonSerializer);

                        }
                        catch (Exception eee)
                        {
                            ///
                        }
                        finally
                        {
                            //if (sr != null)
                            //    sr.Close();
                        }
                    }
                });
            }
            catch
            {
            }
            finally
            {
                //if (sr != null)
                //    sr.Close();
            }
        }
    }

    public class Logs_EveryDay
    {
        /// <summary>
        /// 响应结果信息
        /// </summary>
        //[JsonProperty("Logs_Subject")]
        public string Logs_Subject { get; set; }

        /// <summary>
        /// 响应结果信息 标题次级标题
        /// </summary>
        //[JsonProperty("Logs_SubSubject")]
        public string Logs_SubSubject { get; set; }
        /// <summary>
        /// 生成的签名串
        /// </summary>
        //[JsonProperty("Logs_Content")]
        public string Logs_Content { get; set; }
    }
}