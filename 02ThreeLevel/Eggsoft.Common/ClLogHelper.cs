using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft.Common
{
    public class ClLogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)

        public static void WriteLog(Type t, string msg)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(t);
            //log.Error(msg);


            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string txtLogPath = string.Empty;
            string iisBinPath = AppDomain.CurrentDomain.RelativeSearchPath;
            if (!string.IsNullOrEmpty(iisBinPath))

                txtLogPath = Path.Combine(iisBinPath.Remove(iisBinPath.Length - 3, 3), @"Log\ErrorLog" + DateTime.Now.ToString("yyyyMMddHH") + ".txt");
            else
                txtLogPath = Path.Combine(currentPath, @"Log\ErrorLog" + DateTime.Now.ToString("yyyyMMddHH") + ".txt");
            FileAppender fileAppender = new FileAppender();
            fileAppender.Name = "LogFileAppender";
            fileAppender.File = txtLogPath;
            fileAppender.AppendToFile = true;
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "记录时间：%date \r\n线程ID:[%thread] \r\n日志级别：%-5level \r\n描述：%message%\r\n";
            patternLayout.Footer = "------------------------add by end--------------------";
            patternLayout.ActivateOptions();
            fileAppender.Layout = patternLayout;
            //UTF8编码，确保中文不乱码。
            fileAppender.Encoding = Encoding.UTF8;
            fileAppender.ActivateOptions();
            BasicConfigurator.Configure(fileAppender);
            InvokeErrorLog(MethodBase.GetCurrentMethod().DeclaringType, msg, null);
        }

        public static void InvokeErrorLog(Type methedType, string errorMsg, Exception ex)
        {
            ILog log = log4net.LogManager.GetLogger(methedType);
            log.Info(errorMsg, ex);
        }

        #endregion

    }
}
