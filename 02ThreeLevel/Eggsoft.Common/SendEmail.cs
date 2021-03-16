using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Eggsoft.Common
{


    public class ClassEmail_Task
    {
        public ClassEmail_Task()
        { }
        #region Model
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private string _Email_FromCityName;
        private string _Email_To;
        private string _Email_Subject;
        private string _Email_Body;


        /// <summary>
        /// 
        /// </summary>
        public string Email_FromCityName
        {
            set { _Email_FromCityName = value; }
            get { return _Email_FromCityName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email_To
        {
            set { _Email_To = value; }
            get { return _Email_To; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email_Subject
        {
            set { _Email_Subject = value; }
            get { return _Email_Subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email_Body
        {
            set { _Email_Body = value; }
            get { return _Email_Body; }
        }


        #endregion Model
    }

    public class ClassWeiXin_Task
    {
        public ClassWeiXin_Task()
        { }
        #region Model
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private string _SendURL;
        private string _JSON;


        /// <summary>
        /// 
        /// </summary>
        public string SendURL
        {
            set { _SendURL = value; }
            get { return _SendURL; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JSON
        {
            set { _JSON = value; }
            get { return _JSON; }
        }
        #endregion Model
    }


    public class OurSendEmail
    {
        //在virusScan控制台中，选择访问保护，点击属性，把第一项“禁止发送大量邮件”前的勾去掉
        public static bool SendEmail(string strFromCityName, string strTo, string strSubject, string strBody)
        {
            try
            {
                strBody += "\n\n本Email地址仅发信使用，不收取任何回信！";

                String SystemEmailForSend = System.Configuration.ConfigurationManager.AppSettings["SystemEmailForSend"];
                string[] SystemEmailForSendList = SystemEmailForSend.Split('#');

                //MailAddress mailfrom = new MailAddress("foodztf@tom.com", strFromCityName, System.Text.Encoding.UTF8);//发件人邮箱地址，名称，编码UTF8   
                MailAddress mailfrom = new MailAddress(SystemEmailForSendList[0], strFromCityName, System.Text.Encoding.UTF8);//发件人邮箱地址，名称，编码UTF8   
                MailAddress mailto = new MailAddress(strTo, strFromCityName, System.Text.Encoding.UTF8);//收件人邮箱地址，名称，编码UTF8   //创建mailMessage对象   System.Net.Mail.MailMessage message = new MailMessage(mailfrom, mailto);   
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(mailfrom, mailto);
                mail.Subject = strSubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = strBody;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                #region 暗送
                MailAddress bcc1 = new MailAddress("2831777322@qq.com");
                //MailAddress bcc2 = new MailAddress("dhs@eggsoft.cn");
                //MailAddress bcc3 = new MailAddress("ztf@eggsoft.cn");
                mail.Bcc.Add(bcc1);
                //mail.Bcc.Add(bcc2);
                //mail.Bcc.Add(bcc3);
                #endregion
                // System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(("foodztf@tom.com", strTo, strSubject, strBody);
                //mail.CC.Add(new MailAddress(cc));
                //mail.Attachments.Add(new Attachment(attachment));
                mail.IsBodyHtml = false;
                //mail.
                // System.Net.Mail.SmtpClient send = new System.Net.Mail.SmtpClient("Smtp.163.com");
                //System.Net.Mail.SmtpClient send = new System.Net.Mail.SmtpClient("smtp.tom.com");
                System.Net.Mail.SmtpClient send = new System.Net.Mail.SmtpClient(SystemEmailForSendList[1]);


                send.Port = Int32.Parse(SystemEmailForSendList[2]);// gmail 587;
                // send.EnableSsl = true;
                send.UseDefaultCredentials = false;
                //send.Credentials = new NetworkCredential("foodztf@tom.com", "oliver000");
                send.Credentials = new NetworkCredential(SystemEmailForSendList[3], SystemEmailForSendList[4]);

                // debug_Log.Call_WriteLog(mail.ToString());
                //debug_Log.Call_WriteLog(Eggsoft.Common.XmlHelper.XmlSerialize(send,Encoding.UTF8), "邮件发送","发送信息XML");
                send.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                debug_Log.Call_WriteLog("SendEmail" + strFromCityName + strTo + strSubject + strBody, "邮件发送");
                debug_Log.Call_WriteLog(e, "邮件发送");
                return false;
            }
            finally
            {
            }
        }


        public static bool SendEmail_old(string strEmailfrom, string strEmailTo, string strEmailSubject, string strEmailBody)
        {
            try
            {

                MailMessage m_message = new MailMessage();
                m_message.From = new MailAddress(strEmailfrom);
                m_message.To.Add(new MailAddress(strEmailTo));
                m_message.Subject = strEmailSubject;
                m_message.Body = strEmailBody;
                SmtpClient m_smtpClient = new SmtpClient();
                m_smtpClient.Send(m_message);
                return true;
            }
            catch (Exception e)
            {
                MyError.ThrowException(e.Message);
                //throw new Exception(msg + "<br/>" + HttpContext.Current.Request.ServerVariables["path_translated"]);
                return false;
                //Console.WriteLine(e.Message);
            }
            //Console.ReadLine();
        }
    }
}