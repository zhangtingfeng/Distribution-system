#region Usings
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
#endregion

//============================================================================
// 新软交易论坛 官方支持：WWW.eggsoft.cn 
//
// 新软小组 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /// <summary>
    /// Url转发处理
    /// </summary>
    public class UrlTrans
    {
        static public string DotTrans(string type,string p1,string p2,string p3)
        {
            string ret = "",transParm="",origParm="";

            if (type == "topic")
            {
                transParm = "Topic-"+p1;
                origParm = "ShowTopic.aspx?ID="+p1;
            }
            if (type == "newTopic")
            {
                transParm = "NewTopic-" + p1+"-"+p2;
                origParm = "NewTopic.aspx?Page=" + p1+"&BigPage="+p2;
            }
            if (type == "newReply")
            {
                transParm = "NewReply-" + p1 + "-" + p2;
                origParm = "NewReply.aspx?Page=" + p1 + "&BigPage=" + p2;
            }
            if (type == "Category")
            {
                transParm = "Category-" + p1 + "-" + p2;
                origParm = "Category.aspx?Page=" + p1 + "&BigPage=" + p2;
            }
            if (type == "NewWeixin")
            {
                transParm = "NewWeixin-" + p1 + "-" + p2;
                origParm = "NewWeixin.aspx?Page=" + p1 + "&BigPage=" + p2;
            }
            if (type == "recommend")
            {
                transParm = "Recommend-" + p1 + "-" + p2;
                origParm = "Recommend.aspx?Page=" + p1 + "&BigPage=" + p2;
            }
            if (type == "topic3")
            {
                transParm = "Topic-" + p1+"-"+p2+"-"+p3;
                origParm = "ShowTopic.aspx?ID=" + p1+"&Page="+p2+"&BigPage="+p3;
            }
            if (type == "boardList")
            {
                transParm = "BoardList-" + p1;
                origParm = "BoardList.aspx?BigClassID=" + p1;
            }
            if (type == "board")
            {
                transParm = "Board-" + p1;
                origParm = "Board.aspx?SmallClassID=" + p1;
            }
            if (type == "board3")
            {
                transParm = "Board-" + p1+"-"+p2+"-"+p3;
                origParm = "Board.aspx?SmallClassID=" + p1+"&Page="+p2+"&BigPage="+p3;
            }
           
            //Config config = new ConfigBll().GetModel();
            if (false)
            {
                //ret = transParm + config.UrlTransExt;
            }
            else
            {
                ret = origParm;
            }

            return ret;
        }
    }
}
