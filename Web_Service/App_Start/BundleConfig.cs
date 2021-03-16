using System.Web;
using System.Web.Optimization;

namespace Web_Service
{
    //test
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Source/Scripts/jquery-1.10.2.js",
                        "~/Source/Scripts/cookies.js",
                        "~/Source/Scripts/bootstrap.js",
                        "~/Source/Scripts/respond.js",
                        "~/Source/js/model/*.js",///?deng
                        "~/Source/Scripts/angular.js",
                        "~/Source/Scripts/restangular.js",
                        "~/Source/Scripts/ng-file-upload-shim.js",
                        "~/Source/Scripts/ng-file-upload.js",
                        "~/Source/Scripts/jquery.signalR-2.2.0.js",
                        "~/Source/Scripts/lodash.js",
                        "~/Source/Scripts/bootstrap-datetimepicker.js",
                        "~/Source/Scripts/locales/bootstrap-datetimepicker.zh-CN.js",
                        "~/Source/Scripts/pnotify.custom.min.js",
                        "~/Source/Scripts/angular-sanitize.js",
                        "~/Source/Scripts/app.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Source/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Source/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Source/Content/bootstrap.css",
                      "~/Source/Content/bootstrap-theme.css",
                      "~/Source/Content/bootstrap-datetimepicker.css",
                      "~/Source/Content/site.css",
                      "~/Source/Content/pnotify.custom.min.css",
                       "~/Source/Content/style.css"
                      ).Include("~/Source/Content/font-awesome.css", new CssRewriteUrlTransform())
             );

            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
