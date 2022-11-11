using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
namespace Questionnaire_and_FeedbackForm.Models.Mail
{
    public class BaseMail
    {
        public string FromAddress { get; set; } ="Questionnaire_function@compal.com" ;
        public List<string> CCAddress { get; set; } = new List<string>{};
        public List<string> RecipientAddress { get; set; } = new List<string>{};
        public string Subject { get; set; } ="Feedbackform";
        public BodyBuilder Body { get; set; } = new BodyBuilder();
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>{};
    }
}