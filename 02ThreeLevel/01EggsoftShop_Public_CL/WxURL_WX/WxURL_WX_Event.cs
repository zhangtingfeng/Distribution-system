using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

/// <summary>
///Subscribe 的摘要说明
/// </summary>
public class WX_Event
{

    public static WX_Model.WX_Model_Event Call_Event(string strXML)
    {
        WX_Model.WX_Model_Event myWX_Model = new WX_Model.WX_Model_Event();
        myWX_Model = myWX_Model.GetWX_Model_Event(strXML);





        return myWX_Model;
    }

}