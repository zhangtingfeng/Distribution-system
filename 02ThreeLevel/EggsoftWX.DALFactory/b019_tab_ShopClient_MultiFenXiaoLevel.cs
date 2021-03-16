using System;
using System.Reflection;
using System.Configuration;
using EggsoftWX.IDAL;
namespace EggsoftWX.DALFactory
{
	/// <summary>
	/// 工厂类b019_tab_ShopClient_MultiFenXiaoLevel 的摘要说明。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  
	/// DataCache类在导出代码的文件夹里
	/// <appSettings>  
	/// <add key="DAL" value="EggsoftWX.SQLServerDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
	public class b019_tab_ShopClient_MultiFenXiaoLevel
	{
		public static EggsoftWX.IDAL.Ib019_tab_ShopClient_MultiFenXiaoLevel Create()
		{
			string path = System.Configuration.ConfigurationManager.AppSettings["EggsoftWX.DALFactory"];
			string CacheKey = path+".b019_tab_ShopClient_MultiFenXiaoLevel";
			object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(path).CreateInstance(CacheKey);
					Eggsoft.Common.DataCache.SetCache(CacheKey, objType);// 写入缓存
				}
				catch{}
			}
			return (Ib019_tab_ShopClient_MultiFenXiaoLevel)objType;
		}
	}
}
