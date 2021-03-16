using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace _03WAWapShop_Oliver.Status
{
    public class Scheduler
    {
        private static IScheduler _instance;
        private Scheduler()
        {
        }
        /// <summary>
        /// 获得本类实例的唯一全局访问点。
        /// </summary>
        /// <returns></returns>
        [CLSCompliant(false)]
        public static IScheduler GetIntance()
        {
            if(_instance == null)
            {
                ISchedulerFactory schedFact = new StdSchedulerFactory();
                _instance = schedFact.GetScheduler();
            }
            return _instance;
        }
    }

    public class JobHelper
    {

        #region 命名前缀(可以自行设置)

        /// <summary>
        ///     作业前缀
        /// </summary>
        public static string JobPerfix = "Job_";

        /// <summary>
        ///     作业分组前缀
        /// </summary>
        public static string GroupPerfix = "Group_";

        /// <summary>
        ///     作业触发器前缀
        /// </summary>
        public static string TriggerPerfix = "Trigger_";

        #endregion

        #region 初始化作业实体

        /// <summary>
        ///     根据传入类型初始化作业实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static JobKey GetJobKey<T>()
        {
            string name = typeof(T).FullName;
            return new JobKey(JobPerfix + name, GroupPerfix + name);
        }
        #endregion 初始化作业实体


        #region 开启作业

        /// <summary>
        ///     开启作业
        /// </summary>
        /// <param name="expression">表达式
        /// <returns></returns>
        public static bool StartJob<T>(string expression) where T : IJob
        {
            string name = typeof(T).FullName;
            IJobDetail job = Scheduler.GetIntance().GetJobDetail(GetJobKey<T>());
            if(job != null && !Scheduler.GetIntance().IsJobGroupPaused(GroupPerfix + name))
            {
                return true;
            }
            if(!Scheduler.GetIntance().IsStarted)
            {
                Scheduler.GetIntance().Start();
            }
            if(job != null)
            {
                Scheduler.GetIntance().ResumeJob(GetJobKey<T>());
            }
            else
            {
                IJobDetail detail = JobBuilder.Create<T>().WithIdentity(JobPerfix + name, GroupPerfix + name).Build();
                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity(TriggerPerfix + name)
                                                 .StartNow()
                                                 .WithCronSchedule(expression).ForJob(detail)
                                                 .Build();
                Scheduler.GetIntance().ScheduleJob(detail, trigger);
                return true;
            }

            return false;
        }

        #endregion

        #region 停止作业

        /// <summary>
        ///     停止作业
        /// </summary>
        /// <returns></returns>
        public static bool StopJob<T>()
        {
            string name = typeof(T).FullName;
            IJobDetail detail = Scheduler.GetIntance().GetJobDetail(GetJobKey<T>());
            if(detail != null && !Scheduler.GetIntance().IsJobGroupPaused(GroupPerfix + name))
            {
                Scheduler.GetIntance().PauseJob(GetJobKey<T>());
                return true;
            }
            return false;
        }

        #endregion

        #region 删除作业

        /// <summary>
        ///     删除作业
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool DelJob<T>()
        {
            string name = typeof(T).FullName;
            IJobDetail detail = Scheduler.GetIntance().GetJobDetail(GetJobKey<T>());
            if(detail != null && !Scheduler.GetIntance().IsJobGroupPaused(GroupPerfix + name))
            {
                Scheduler.GetIntance().PauseJob(GetJobKey<T>());
                Scheduler.GetIntance().DeleteJob(GetJobKey<T>());
                return true;
            }
            return false;
        }

        #endregion

        #region 取得作业运行状态

        /// <summary>
        ///     取得作业运行状态 true 正在运行，false 未在运行
        /// </summary>
        /// <returns></returns>
        public static bool GetJobStatus<T>()
        {
            string name = typeof(T).FullName;
            IJobDetail detail = Scheduler.GetIntance().GetJobDetail(GetJobKey<T>());
            if(detail != null && !Scheduler.GetIntance().IsJobGroupPaused(GroupPerfix + name))
            {
                return true;
            }
            return false;
        }

        public static bool GetJobStatus(string jobName)
        {
            string className = jobName.Substring(JobPerfix.Length);
            IJobDetail detail = Scheduler.GetIntance().GetJobDetail(new JobKey(jobName, GroupPerfix + className));
            if(detail != null && !Scheduler.GetIntance().IsJobGroupPaused(GroupPerfix + className))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}