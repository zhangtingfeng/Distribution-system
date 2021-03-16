using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.UserOrder
{
    public class OrderDetailPartialViewModel
    {
        public OrderDetailPartialViewModel(string OrderID, string HtmlType, bool IsShowHeader = true)
        {
            this.OrderID = OrderID;
            this.HtmlType = HtmlType;
            this.IsShowHeader = IsShowHeader;
        }

        /// <summary>
        /// OrderID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// Html\Javascript
        /// </summary>
        public string HtmlType { get; set; }

        public bool IsShowHeader { get; set; }
    }
}