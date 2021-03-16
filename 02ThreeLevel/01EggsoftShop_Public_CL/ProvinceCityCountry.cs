using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft_Public_CL
{
    public class ProvinceCityCountry
    {
        public static string getProvinceCityCountry(int intID, string strReturnType)
        {
            EggsoftWX.BLL.tab_PE_Region BLL_tab_PE_Region = new EggsoftWX.BLL.tab_PE_Region();
            EggsoftWX.Model.tab_PE_Region Model_tab_PE_Region = new EggsoftWX.Model.tab_PE_Region();
            Model_tab_PE_Region = BLL_tab_PE_Region.GetModel(intID);

            string strReturnName = "";
            if (Model_tab_PE_Region != null)
            {
                switch (strReturnType)
                {
                    case "Province": strReturnName = Model_tab_PE_Region.Province; break;
                    case "City": strReturnName = Model_tab_PE_Region.City; break;
                    case "Area": strReturnName = Model_tab_PE_Region.Area; break;
                }
            }
            return strReturnName;
        }

    }
}
