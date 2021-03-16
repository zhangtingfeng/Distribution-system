using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft.Common
{
    /*
    一、开发背景：

　　最近在公司开发的系统中，需要计算工作日，就是给出一个采购周期（n天），我需要计算出在n个工作日之后的日期。开始准备去调接口（ps:找了半天发现没有太合适的，还有吐槽下国家政府单位都没有官方接口的），但是负责这个项目的大佬说，万一别个的接口崩了，会影响我们自己的系统的正常运行，自己开发还是稳点，我就写了这个功能，特此记录下实现这个功能的思路。

二、定义：

　　工作日想必大家都知道，就是除去周末和每年国务院颁布的节假日放假安排(例如：2017年部分节假日安排)，其他就都是工作日(对了，差点忘记补班，这也算是工作日哦)。

三、实践：

　　“废话”说的够多了，下面撸起袖子开干吧，代码都写了注释。

　　提供了两个公共方法，先给大家看下简单测试的运行结果：

　　　　(1).根据传入的工作日天数，获得计算后的日期
    */

    public class HolidayHelper
    {
        #region 字段属性
        private static object _syncObj = new object();
        private static HolidayHelper _instance { get; set; }
        private static List<DateModel> cacheDateList { get; set; }
        private HolidayHelper() { }
        /// <summary>
        /// 获得单例对象,使用懒汉式（双重锁定）
        /// </summary>
        /// <returns></returns>
        public static HolidayHelper GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    {
                        _instance = new HolidayHelper();
                    }
                }
            }
            return _instance;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFileContent(string filePath)
        {
            string result = "";
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath);
            }
            return result;
        }
        /// <summary>
        /// 获取配置的Json文件
        /// </summary>
        /// <returns>经过反序列化之后的对象集合</returns>
        private List<DateModel> GetConfigList()
        {
            string fileContent = Eggsoft.Common.JsonHelper.GetFileJson("~/MultiButton_income_drawmoney_holidayConfig.json");

            //string path = string.Format("{0}/../../Config/holidayConfig.json", System.AppDomain.CurrentDomain.BaseDirectory);
            //string fileContent = GetFileContent(path);
            if (!string.IsNullOrWhiteSpace(fileContent))
            {
                cacheDateList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DateModel>>(fileContent);
            }
            return cacheDateList;
        }
        /// <summary>
        /// 获取指定年份的数据
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DateModel GetConfigDataByYear(int year)
        {
            if (cacheDateList == null)//取配置数据
                GetConfigList();
            DateModel result = cacheDateList.FirstOrDefault(m => m.Year == year);
            return result;
        }
        /// <summary>
        /// 判断是否为工作日
        /// </summary>
        /// <param name="currDate">要判断的时间</param>
        /// <param name="thisYearData">当前的数据</param>
        /// <returns></returns>
        private bool IsWorkDay(DateTime currDate, DateModel thisYearData)
        {
            if (currDate.Year != thisYearData.Year)//跨年重新读取数据
            {
                thisYearData = GetConfigDataByYear(currDate.Year);
            }
            if (thisYearData.Year > 0)
            {
                string date = currDate.ToString("MMdd");
                int week = (int)currDate.DayOfWeek;

                if (thisYearData.Work.IndexOf(date) >= 0)
                {
                    return true;
                }

                if (thisYearData.Holiday.IndexOf(date) >= 0)
                {
                    return false;
                }

                if (week != 0 && week != 6)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 公共方法
        public void CleraCacheData()
        {
            if (cacheDateList != null)
            {
                cacheDateList.Clear();
            }
        }
        /// <summary>
        /// 根据传入的工作日天数，获得计算后的日期,可传负数
        /// </summary>
        /// <param name="day">天数</param>
        /// <param name="isContainToday">当天是否算工作日（默认：true）</param>
        /// <returns></returns>
        public DateTime GetReckonDate(int day, bool isContainToday = true)
        {
            DateTime currDate = DateTime.Now;
            int addDay = day >= 0 ? 1 : -1;

            if (isContainToday)
                currDate = currDate.AddDays(-addDay);

            DateModel thisYearData = GetConfigDataByYear(currDate.Year);
            if (thisYearData.Year > 0)
            {
                int sumDay = Math.Abs(day);
                int workDayNum = 0;
                while (workDayNum < sumDay)
                {
                    currDate = currDate.AddDays(addDay);
                    if (IsWorkDay(currDate, thisYearData))
                        workDayNum++;
                }
            }
            return currDate;
        }

        public bool isWorkDay(DateTime date) {
            DateModel thisYearData = GetConfigDataByYear(date.Year);
            return IsWorkDay(date, thisYearData);
        }
        /// <summary>
        /// 根据传入的时间，计算工作日天数
        /// </summary>
        /// <param name="date">带计算的时间</param>
        /// <param name="isContainToday">当天是否算工作日（默认：true）</param>
        /// <returns></returns>
        public int GetWorkDayNum(DateTime date, bool isContainToday = true)
        {
            var currDate = DateTime.Now;

            int workDayNum = 0;
            int addDay = date.Date > currDate.Date ? 1 : -1;

            if (isContainToday)
            {
                currDate = currDate.AddDays(-addDay);
            }

            DateModel thisYearData = GetConfigDataByYear(currDate.Year);
            if (thisYearData.Year > 0)
            {
                bool isEnd = false;
                do
                {
                    currDate = currDate.AddDays(addDay);
                    if (IsWorkDay(currDate, thisYearData))
                        workDayNum += addDay;
                    isEnd = addDay > 0 ? (date.Date > currDate.Date) : (date.Date < currDate.Date);
                } while (isEnd);
            }
            return workDayNum;
        }
        #endregion

    }

    public struct DateModel
    {
        public int Year { get; set; }

        public List<string> Work { get; set; }

        public List<string> Holiday { get; set; }
    }
}
