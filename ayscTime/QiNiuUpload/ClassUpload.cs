using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eggsoft.Common;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace QiNiuUpload
{
    public class ClassUploadAction
    {

        public Boolean UploadList(List<FileInformation> ListFileInformation)
        {//获取Configuration对象
         //Configuration myconfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         ////根据Key读取元素的Value
         //string name = myconfig.AppSettings.Settings["AccessKey"].Value;
         // 上传文件到七牛云储存   string strPostURL = System.Configuration.ConfigurationManager.AppSettings["WriteLogUrl"];

            string AccessKey = System.Configuration.ConfigurationManager.AppSettings["AccessKey"];
            string SecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
            string Bucket = System.Configuration.ConfigurationManager.AppSettings["Bucket"];

            Mac mac = new Mac(AccessKey, SecretKey);

            // 存储空间名
            
            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.InsertOnly = 0;

            for (int i = 0; i < ListFileInformation.Count; i++)
            {

                // 上传文件名
                string key = ListFileInformation[i].QiNiuUplodFullName;
                // 本地文件路径
                string filePath = ListFileInformation[i].FullName;
                // 设置要上传的目标空间
                putPolicy.Scope = Bucket + ":" + key;//覆盖
                                                     // 上传策略的过期时间(单位:秒)
                putPolicy.SetExpires(3600);
                // 文件上传完毕后，在多少天后自动被删除
                putPolicy.DeleteAfterDays = 6000;
                // 生成上传token
                string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                Config config = new Config();
                // 设置上传区域
                config.Zone = Zone.ZONE_CN_East;
                // 设置 http 或者 https 上传
                config.UseHttps = true;
                config.UseCdnDomains = true;
                config.ChunkSize = ChunkUnit.U512K;
                // 表单上传
                FormUploader target = new FormUploader(config);

                HttpResult result = target.UploadFile(filePath, key, token, null);
                Eggsoft.Common.debug_Log.Call_WriteLog(result.toJsonString(), "七牛", filePath);
            }
            
            //Console.WriteLine("form upload result: " + result.ToString());

            return false;
        }

    }


}
