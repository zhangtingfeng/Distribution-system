using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /// <summary>
    /// 控件验证类
    /// </summary>
    public class ControlValidate
    {

        /// <summary>
        /// image验证
        /// </summary>
        /// <param name="txt">image框</param>
        /// <param name="txtName">image框名称</param>
        /// <param name="validateItems">验证项目，以'|'分隔，如'NullVal|LenVal|FormatVal';不验证的设为空，如'NullVal||'表示只验证是否为空,不验证长度及格式;</param>
        /// /// <param name="lengthRound">当验证长度时有销，如'0-12'表示长度不能超过12个字符;</param>
        /// <param name="regExStr">当验证格式时有效</param>
        public static void ImageValidate(System.Web.UI.WebControls.Image image, string txtName, string validateItems, string lengthRound, string regExStr)
        {
            if (validateItems.IndexOf("|") < 0)
                throw new Exception("验证项格式错误!");
            string[] arr = validateItems.Split('|');
            if (arr.Length < 3)
                throw new Exception("验证项格式错误!");
            if (arr[0] == "NullVal")
            {

                if (image.ImageUrl.Trim().Length == 0)
                {
                    //throw new Exception("必须填写!");
                    JsUtil.ShowMsg(txtName + "必须填写!", "?");
                    string url=HttpContext.Current.Request.Url.ToString(); 
//url= http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli
                    Eggsoft.Common.JsUtil.LocationNewHref(url);

                    //return true;
                }
            }
            //return false;
        }



        /// <summary>
        /// 文本框验证
        /// </summary>
        /// <param name="txt">文本框</param>
        /// <param name="txtName">文本框名称</param>
        /// <param name="validateItems">验证项目，以'|'分隔，如'NullVal|LenVal|FormatVal';不验证的设为空，如'NullVal||'表示只验证是否为空,不验证长度及格式;</param>
        /// /// <param name="lengthRound">当验证长度时有销，如'0-12'表示长度不能超过12个字符;</param>
        /// <param name="regExStr">当验证格式时有效</param>
        public static void TextBoxValidate(TextBox txt,string txtName,string validateItems,string lengthRound,string regExStr)
        {
            if (validateItems.IndexOf("|") < 0)
                throw new Exception("验证项格式错误!");
            string[] arr = validateItems.Split('|');
            if(arr.Length<3)
                throw new Exception("验证项格式错误!");
            if (arr[0] == "NullVal")
            {
                if (txt.Text.Trim().Length == 0)
                {
                    JsUtil.ShowMsg(txtName+"必须填写!","?");
                    return;
                }
            }
            if (arr[1] == "LenVal")
            {
                int min=Int32.Parse(lengthRound.Split('-')[0]);
                int max = Int32.Parse(lengthRound.Split('-')[1]);
                if (txt.Text.Trim().Length < min)
                {
                    JsUtil.ShowMsg(txtName + "长度不能小于"+min.ToString()+"个字符!", "?");
                    return;
                }
                if (txt.Text.Trim().Length > max)
                {
                    JsUtil.ShowMsg(txtName + "长度不能超过" + max.ToString() + "个字符!", "?");
                    return;
                }
            }
            if (arr[2] == "FormatVal")
            {
                Regex reg = new Regex(regExStr);
                if (!reg.IsMatch(txt.Text.Trim()))
                {
                    JsUtil.ShowMsg(txtName + "格式不正确!", "?");
                    return;
                }
            }
        }
    }
}
