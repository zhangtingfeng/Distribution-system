using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Web;


namespace Eggsoft_Public_CL
{
    public class WXdbMsghelp
    {

        public WXdbMsghelp()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        ///// <summary>
        ///// 公开方法DBConn，返回数据库连接
        ///// </summary>
        ///// <returns></returns>
        //private OleDbConnection DBconn()
        //{
        //    string strConn = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        //    try
        //    {
        //        return new OleDbConnection(strConn);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 公开方法DBConn，返回数据库连接
        ///// </summary>
        ///// <returns></returns>
        //private void DBconn_Close(OleDbConnection conn)
        //{
        //    if (conn != null)
        //    {
        //        conn.Close();
        //        conn = null;
        //    }
        //}


        //public string  addOrderInfo(string user)
        //{
        //    int i = 0;
        //    OleDbConnection conn = DBconn();
        //    string strError = "";
        //    try
        //    {
        //        conn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        cmd.Connection = conn;
        //        Public_UserName = user;
        //        cmd.CommandText = "select * from [order] where username='" + user + "'";
        //        OleDbDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            Public_First = dr["first"].ToString();
        //            Public_Second = dr["second"].ToString();
        //            Public_Third = dr["third"].ToString();
        //            conn.Close();
        //        }
        //        else
        //        {
        //            dr.Close();
        //            cmd.CommandText = "insert into [order](username) values ('" + user + "')";
        //            i = cmd.ExecuteNonQuery();
        //            Public_First = "0";
        //            Public_Second = "0";
        //            Public_Third = "0";
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        strError = ee.Message;
        //        throw new Exception(ee.Message);
        //    }
        //    finally
        //    { DBconn_Close(conn); }
        //    return strError;
        //}
        /** 
     * 将微信消息中的CreateTime转换成标准格式的时间（yyyy-MM-dd HH:mm:ss） 
     *  
     * @param createTime 消息创建时间 
     * @return 
     */






        public static void addUser_Chat_InfoToMDF(Int32 intToUserID, Int32 intFromUserID, string strInOUTMsgType, string strTextImageType, string weixinXML, String strFromUserName, String INCUserToUserName)
        {
            try
            {
                EggsoftWX.BLL.tab_System_XML_Message my_BLL_tab_WeiXin_XML_Message = new EggsoftWX.BLL.tab_System_XML_Message();
                EggsoftWX.Model.tab_System_XML_Message my_Model_tab_WeiXin_XML_Message = new EggsoftWX.Model.tab_System_XML_Message();



                my_Model_tab_WeiXin_XML_Message.FromUserID = intFromUserID;
                my_Model_tab_WeiXin_XML_Message.ToUserID = intToUserID;



                my_Model_tab_WeiXin_XML_Message.FromUserName = strFromUserName;
                my_Model_tab_WeiXin_XML_Message.INCUserToUserName = INCUserToUserName;
                my_Model_tab_WeiXin_XML_Message.MessageContent = weixinXML;
                my_Model_tab_WeiXin_XML_Message.type = strInOUTMsgType;
                my_Model_tab_WeiXin_XML_Message.MessageType = strTextImageType;
                my_BLL_tab_WeiXin_XML_Message.Add(my_Model_tab_WeiXin_XML_Message);

            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally
            { }

        }


    }
}