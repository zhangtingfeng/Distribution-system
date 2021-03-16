using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

/// <summary>
///Subscribe 的摘要说明
/// </summary>
public class WX_Parent
{

    public static WX_Model.WX_Model_Parent Call_Parent(string strXML)
    {
        WX_Model.WX_Model_Parent myWX_Model = new WX_Model.WX_Model_Parent();
        myWX_Model = myWX_Model.GetWX_Model_Parent(strXML);


        return myWX_Model;
    }

    

}