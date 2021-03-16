using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.Common
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PagingViewPartialViewModel
    {
        public PagingViewPartialViewModel(string PageID)
        {
            this.PageID = PageID;
        }

        /// <summary>
        /// 分页ID，用于多页面引用区分
        /// </summary>
        public string PageID { get; set; }
    }
}