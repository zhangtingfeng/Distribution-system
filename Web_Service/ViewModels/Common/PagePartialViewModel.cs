using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.Common
{
    public class PagePartialViewModel
    {
        /// <summary>
        /// 页面布局
        /// </summary>
        /// <param name="HtmlType"></param>
        public PagePartialViewModel(string PageLayoutID)
        {
            this.PageLayoutID = PageLayoutID;
        }

        /// <summary>
        /// 页面布局
        /// </summary>
        public string PageLayoutID { get; set; }
    }
}