using System.Text.RegularExpressions;

namespace Qiniu.Http
{
    /// <summary>
    /// URL辅助工具(RegExp)
    /// </summary>
    public class UrlHelper
    {
        private static Regex regx = new Regex(@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

        private static Regex regu = new Regex(@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,/~\+#]*)?");

        private static Regex regd = new Regex(@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,/~\+#]*)?/");

        /// <summary>
        /// 是否合法URL
        /// </summary>
        /// <param name="_url">待判断的url</param>
        /// <returns></returns>
        public static bool isValidUrl(string _url)
        {
            return regx.IsMatch(_url);
        }

        /// <summary>
        /// 是否一般URL(不包含？等后缀参数)
        /// </summary>
        /// <param name="_url">待判断的url</param>
        /// <returns></returns>
        public static bool isNormalUrl(string _url)
        {
            return regu.IsMatch(_url);
        }

        /// <summary>
        /// 是否合法URL目录
        /// </summary>
        /// <param name="_dir">待判断的url目录</param>
        /// <returns></returns>
        public static bool isValidDir(string _dir)
        {
            return regd.IsMatch(_dir);
        }

        /// <summary>
        /// 从原始URL转换为一般URL(根据需要截断)
        /// </summary>
        /// <param name="_url">待转换的url</param>
        /// <returns></returns>
        public static string getNormalUrl(string _url)
        {
            var m = regu.Match(_url);
            return m.Value;
        }

        /// <summary>
        /// URL分析，拆分出Host,Path,File,Query各个部分
        /// </summary>
        /// <param name="url">原始URL</param>
        /// <param name="host">host部分</param>
        /// <param name="path">path部分</param>
        /// <param name="file">文件名</param>
        /// <param name="query">参数</param>
        public static void urlSplit(string url, out string host, out string path, out string file, out string query)
        {
            int start = 0;

            Regex regHost = new Regex(@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+");
            host = regHost.Match(url, start).Value;
            start += host.Length;

            Regex regPath = new Regex(@"(/(\w|\-)*)+/");
            path = regPath.Match(url, start).Value;
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }
            start += path.Length;

            int index = url.IndexOf('?', start);
            file = url.Substring(start, index - start);

            query = url.Substring(index);
        }
    }
}
