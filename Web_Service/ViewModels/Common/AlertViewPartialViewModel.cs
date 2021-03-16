using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.Common
{
    public class AlertViewPartialViewModel
    {
        public AlertViewPartialViewModel(string AlertWindowID, string PageID)
        {
            this.PageID = PageID;
            this.AlertWindowID = AlertWindowID;
        }

        /// <summary>
        /// 分页ID，用于多页面引用区分
        /// </summary>
        public string PageID { get; set; }

        /// <summary>
        /// 提示框ID
        /// </summary>
        public string AlertWindowID { get; set; }
    }
}