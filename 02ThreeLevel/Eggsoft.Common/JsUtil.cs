using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class JsUtil
    {
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMsg(string msg)
        {
            string js = @"<Script language='JavaScript'>
                    alert('" + msg + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsg(string msg, int intURL)
        {
            String strURL = "";


            if (intURL == -1)
            {
                strURL = "javascript:history.go(-1);";
            }
            else if (intURL == -2)
            {
                strURL = "javascript:history.go(-2);";
            }
            else if (intURL == -3)
            {
                strURL = "javascript:history.go(-3);";
            }
            else if (intURL == 0)
            {
                strURL = "javascript:window.location.reload();";
            }
            else if (intURL == -99)
            {
                strURL = "javascript:self.location = document.referrer;";
            }
            else if (intURL == -100)
            {
                strURL = "javascript:window.close();";
            }
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, strURL));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 点击页面上的按钮，弹出一个对话框提示是“确定”还是“取消”操作，选择“确定”或“取消”后跳转到相应的页面
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void Confirm(string msg, string strUrl_Yes, string strUrl_No)
        {


            HttpContext.Current.Response.Write("<Script Language='JavaScript'>if ( window.confirm('" + msg + "')) {  window.location.href='" + strUrl_Yes +
                                    "' } else {window.location.href='" + strUrl_No + "' };</script>");
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            //HttpContext.Current.Response.Write(string.Format(js, msg, toURL));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsg(string msg, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');self.location.href='{1}';</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, toURL));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 弹出消息框并且新开到新的URL
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsgNew(string msg, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, toURL));
            HttpContext.Current.Response.End();
        }

        ///// <summary>
        ///// 停留指定时间后，跳转到OpenWindow指定页
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="goUrl"></param>
        ///// <param name="second"></param>
        //public static void TipAndRedirect_OpenWindow(string msg, string goUrl, string second)
        //{
        //    HttpContext.Current.Response.Write("<script language=\"JavaScript\">window.open('" + goUrl + "'</script>");
        //    HttpContext.Current.Response.End();

        //}

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url"></param>
        public static void RefreshParent(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void OpenWindow(string url, int width = 0, int heigth = 0, int top = 0, int left = 0)
        {
            string stringjs = "";
            if (width == 0)
            {
                stringjs = "Response.Write(\"<script>window.showModalDialog('Board_O2O_ShopNav.html','_blank')</script>\")";//——原窗口保留，另外新增一个新页面;

            }
            else
            {
                stringjs = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            }
            HttpContext.Current.Response.Write(stringjs);
        }

        /// <summary>
        /// 转向指定的Url
        /// </summary>
        /// <param name="url"></param>
        public static void LocationNewHref(int int0)
        {
            string strurl = "";
            if (int0 == 0)
            {
                strurl = HttpContext.Current.Request.RawUrl;//重新载入本页
            }
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, strurl);
            HttpContext.Current.Response.Write(js);
        }
        /// <summary>
        /// 转向指定的Url
        /// </summary>
        /// <param name="url"></param>
        public static void LocationNewHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void ShowModalDialog(string url, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            string js = @"<script language=javascript>							
							showModalDialog('" + url + "','','" + features + "');</script>";
            HttpContext.Current.Response.Write(js);
        }



        /// <summary>
        /// 停留指定时间后，跳转到指定页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="goUrl"></param>
        /// <param name="second"></param>
        public static void TipAndRedirect(string msg, string goUrl, string second)
        {
            //HttpContext.Current.Response.Write("<meta http-equiv='refresh' content='"+second+";url=" + goUrl + "'>");
            //HttpContext.Current.Response.Write("<br/><br/><p align=center><div style=\"size:12px\">&nbsp;&nbsp;&nbsp;&nbsp;" + msg.Replace("!", "") + ",页面" + second + "秒内跳转!<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + goUrl + "\">如果没有跳转，请点击!</a></div></p></div>");
            //HttpContext.Current.Response.End();
            string stWapAppURL = ConfigurationManager.AppSettings["WapApp"];

            string strTemplet = Eggsoft.Common.FileFolder.Read_Remote_File(stWapAppURL + "/Templet/02ShiYi/Jump_Page_Templet.html");
            //strTemplet = setToday(strTemplet);//今日上线
            //strTemplet = setCheap(strTemplet);//今日优惠


            strTemplet = strTemplet.Replace("###JumpURL###", goUrl);
            strTemplet = strTemplet.Replace("###JumpURL_Text###", msg);
            strTemplet = strTemplet.Replace("###JumpURL_Second###", second);

            HttpContext.Current.Response.Write(strTemplet);

        }

        public static void TipAndRedirect(string msg, string goUrl, int intsecond)
        {
            string strTemplet = Eggsoft.Common.FileFolder.ReadTemple("/Templet/02ShiYi/Jump_Page_Templet.html");
            //strTemplet = setToday(strTemplet);//今日上线
            //strTemplet = setCheap(strTemplet);//今日优惠


            strTemplet = strTemplet.Replace("###JumpURL###", goUrl);
            strTemplet = strTemplet.Replace("###JumpURL_Text###", msg);
            strTemplet = strTemplet.Replace("###JumpURL_Second###", intsecond.ToString());

            HttpContext.Current.Response.Write(strTemplet);

            //  HttpContext.Current.Response.Write("<meta http-equiv='refresh' content='" + intsecond + ";url=" + goUrl + "'>");
            //   HttpContext.Current.Response.Write("<br/><br/><p align=center><div style=\"size:12px\">&nbsp;&nbsp;&nbsp;&nbsp;" + msg.Replace("!", "") + ",页面" + intsecond.ToString() + "秒内跳转!<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + goUrl + "\">如果没有跳转，请点击!</a></div></p></div>");
            HttpContext.Current.Response.End();
        }


        public static void RefreshCurWindow()
        {
            HttpContext.Current.Response.Write("<script>window.location.href=window.location.href;</script>");
        }

    }
}
