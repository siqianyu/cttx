using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Net.Mail;


    public class sendMail
    {
        private const string hostSmtp = "smtp.163.com";                                                 //设置用于 SMTP 事务的主机的名称
        private const string hostEmailAddress = "a982846932@163.com";                                     //使用邮箱
        private const string hostEmailPassword = "liu3586768";                                         //使用邮箱密码
        private const string showEmailAddress = "zjjypxzx@163.com";                                    //假邮箱，收件人看到的邮箱
        private const string showEmailName = "support";                                                 //假邮箱，收件人看到的邮箱

        public string sendEmailAddress;                                                                 //送信邮箱
        public string sendSubject;                                                                      //标题
        public string sendBodyText;                                                                     //发送内容

        public bool sendMailFn()
        {
            try
            {
                System.Net.Mail.SmtpClient client = new SmtpClient(hostSmtp);

                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(hostEmailAddress, hostEmailPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailAddress addressFrom = new MailAddress(showEmailAddress, showEmailName);
                MailAddress addressTo = new MailAddress(sendEmailAddress, sendEmailAddress);

                System.Net.Mail.MailMessage message = new MailMessage(addressFrom,addressTo);
                message.Sender = new MailAddress(hostEmailAddress);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = sendSubject;
                message.Body = sendBodyText;
                message.IsBodyHtml = true;

                client.Send(message);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        
    }

