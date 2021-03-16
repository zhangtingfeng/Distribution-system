using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Eggsoft.Common
{
    public class doDateTime
    {

         #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >=1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion


        public static DateTime formatTime(String createTime)
        {
            // 将微信传入的CreateTime转换成long类型，再乘以1000  


            long msgCreateTime = long.Parse(createTime);
            if (msgCreateTime <= 635135781484664151)//设一个时间 有利于调试
            {
                msgCreateTime = 635135781484664151;
            }
            DateTime theDate = new DateTime(msgCreateTime);

            //DateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            return theDate;
        }

        public static string formatTime(DateTime createTime)
        {
            // 将微信传入的CreateTime转换成long类型，再乘以1000  

            return createTime.Ticks.ToString();
            //long msgCreateTime = long.Parse(createTime);
            //if (msgCreateTime <= 635135781484664151)//设一个时间 有利于调试
            //{
            //    msgCreateTime = 635135781484664151;
            //}
            //DateTime theDate = new DateTime(msgCreateTime);

            ////DateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            //return theDate;
        }
    }
}
