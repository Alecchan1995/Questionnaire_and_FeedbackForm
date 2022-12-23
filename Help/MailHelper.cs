using System;
using System.Collections.Generic;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using Questionnaire_and_FeedbackForm.Models.Mail;

namespace Questionnaire_and_FeedbackForm.Helpers
{
    public class MailHelper<TMail>
        where TMail : BaseMail
    {
        public static void SendMails(List<TMail> mails)
        {
            foreach (var mail in mails)
            {
                SendMail(mail);
            }
        }
        public static void SendMail(TMail mail)
        {
            //smtpClient.EnableSsl = true;
            MimeMessage send_mail = new MimeMessage();
           // MailMessage send_mail = new Mail
            //Setting From , To and CC           
            send_mail.From.Add(MailboxAddress.Parse(mail.FromAddress));
            send_mail.To.Add(MailboxAddress.Parse("alecchan1995@gmail.com"));

            // foreach (var recipient in mail.RecipientAddress)
            // {
            //     send_mail.To.Add(MailboxAddress.Parse(recipient));
            // }

            // foreach (var cc in mail.CCAddress)
            // {
            //     send_mail.Cc.Add(MailboxAddress.Parse(cc));
            // }

            send_mail.Subject = mail.Subject;
            //setting mail body
             var bodyBuilder = mail.Body;//var bodyBuilder = new BodyBuilder();
                
            foreach (var attachment in mail.Attachments)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    attachment.CopyTo(memoryStream);
                    bodyBuilder.Attachments.Add(attachment.FileName, memoryStream.ToArray());
                }
            }

            send_mail.Body = bodyBuilder.ToMessageBody();
            using (var smtpClient = new SmtpClient())
            {
                //smtpClient.Connect("10.110.15.79", 25);
                 smtpClient.Connect("smtp.gmail.com", 465);
                 smtpClient.Authenticate("alecchan1995@gmail.com", "rqkjijrhoxuciwjq");
                //認證
                try
                {
                    smtpClient.Send(send_mail);
                }
                catch (Exception e)
                {
                    Console.Write("-----"+e.Message);
                }
                smtpClient.Disconnect(true);
            }
        }  
    }
}