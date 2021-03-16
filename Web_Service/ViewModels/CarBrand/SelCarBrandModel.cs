using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Service.ViewModels.CarBrand
{
    public class SelCarBrandModel
    {
        public SelCarBrandModel(string HtmlType, bool IsShowHeader = true)
        {
            this.HtmlType = HtmlType;
            this.IsShowHeader = IsShowHeader;
        }

        /// <summary>
        /// Html\Javascript
        /// </summary>
        public string HtmlType { get; set; }

        public bool IsShowHeader { get; set; }
    }
}