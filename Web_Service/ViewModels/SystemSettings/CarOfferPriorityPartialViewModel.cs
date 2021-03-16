using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.SystemSettings
{
    public class CarOfferPriorityPartialViewModel
    {
        public CarOfferPriorityPartialViewModel(string HtmlType)
        {
            this.HtmlType = HtmlType;
        }

        /// <summary>
        /// HtmlType
        /// </summary>
        public string HtmlType { get; set; }
    }
}