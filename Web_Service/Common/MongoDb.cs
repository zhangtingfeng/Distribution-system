using MongoDB.Driver;
using System;

namespace DALMongoDbHelper
{
    public class MongoDb
    {
        public MongoDb(string host, string timeOut)
        {
            this.CONNECT_TIME_OUT = timeOut;
            this.MONGO_CONN_HOST = host;
        }

        /// <summary>  
        /// 数据库所在主机  
        /// </summary>  
        private readonly string MONGO_CONN_HOST;

        /// <summary>  
        /// 数据库所在主机的端口  
        /// </summary>  
        //private readonly int MONGO_CONN_PORT = 27017;

        /// <summary>  
        /// 连接超时设置 秒  
        /// </summary>  
        private readonly string CONNECT_TIME_OUT;

        /// <summary>  
        /// 数据库的名称  
        /// </summary>  
        //private readonly string DB_NAME = "001QinyibaoDB";

        /// <summary>  
        /// 得到数据库实例  
        /// </summary>  
        /// <returns></returns>  
        public MongoDB.Driver.MongoDatabase GetDataBase()
        {
            //MongoClientSettings mongoSetting = new MongoClientSettings();
            ////设置连接超时时间  
            //mongoSetting.ConnectTimeout = new TimeSpan(int.Parse(CONNECT_TIME_OUT) * TimeSpan.TicksPerSecond);
            ////设置数据库服务器  
            //mongoSetting.Server = new MongoServerAddress(MONGO_CONN_HOST, MONGO_CONN_PORT);
            ////创建Mongo的客户端  
            //MongoClient client = new MongoClient(mongoSetting);
            string strQyb_MongoDB = System.Configuration.ConfigurationManager.AppSettings["Qyb_MongoDB"];
            string strQyb_MongoDBDatabase = System.Configuration.ConfigurationManager.AppSettings["Qyb_MongoDBDatabase"];


            MongoClient client = new MongoClient(strQyb_MongoDB + strQyb_MongoDBDatabase);

            //得到服务器端并且生成数据库实例  
            return client.GetServer().GetDatabase(strQyb_MongoDBDatabase);
        }
    }
}