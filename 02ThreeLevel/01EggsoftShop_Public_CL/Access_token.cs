using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
/// <summary>
///Access_token 的摘要说明
/// </summary>
public class Access_token
{
    public Access_token()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    string _access_token;
    string _expires_in;

    /// <summary>
    /// 获取到的凭证 
    /// </summary>
    public string access_token
    {
        get { return _access_token; }
        set { _access_token = value; }
    }

    /// <summary>
    /// 凭证有效时间，单位：秒
    /// </summary>
    public string expires_in
    {
        get { return _expires_in; }
        set { _expires_in = value; }
    }
}





/*
 
 
*/
public class GetTicket
{
    public GetTicket()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    string _ticket;
    string _expires_in;
    string _errcode;
    string _errmsg;

    /// <summary>
    /// 获取到的凭证 
    /// </summary>
    public string ticket
    {
        get { return _ticket; }
        set { _ticket = value; }
    }

    /// <summary>
    /// 凭证有效时间，单位：秒
    /// </summary>
    public string expires_in
    {
        get { return _expires_in; }
        set { _expires_in = value; }
    }

    /// <summary>
    /// 凭证有效时间，单位：秒
    /// </summary>
    public string errcode
    {
        get { return _errcode; }
        set { _errcode = value; }
    }

    /// <summary>
    /// 凭证有效时间，单位：秒
    /// </summary>
    public string errmsg
    {
        get { return _errmsg; }
        set { _errmsg = value; }
    }
}