using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;


namespace Eggsoft_Public_CL
{
    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
    public class JsonHelper
    {
        public class typeMedia
        {//{"type":"image","media_id":"fUswgG1Wl48Fam3nKVuEVgWlM2Y2UTfEtvcaSozvGd8kasF7kp0fOHtdFGIF1f8q","created_at":1388304068}

            public string type { get; set; }
            public string media_id { get; set; }
            public string thumb_media_id { get; set; }
            public string created_at { get; set; }
        }

        //



        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }



    /*  使用帮助
    public class Person
       {
           public string Name { get; set; }
           public int Age { get; set; }
       }

       protected void Page_Load(object sender, EventArgs e)
       {
          Person p = new Person();
          p.Name = "张三";
          p.Age = 28;
    
          string jsonString = JsonHelper.JsonSerializer<Person>(p);
          Response.Write(jsonString);
 

           string jpppsonString = "{\"Age\":28,\"Name\":\"张三\"}";
           Person ppp = JsonHelper.JsonDeserialize<Person>(jsonString);
       }*/

}