using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using Microsoft.EntityFrameworkCore;
namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class Send_Mail_Controller : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly IWebHostEnvironment _Environment;
         public Send_Mail_Controller(QuestionnaireDataDBContext db,IMapper mapper,IWebHostEnvironment Environment)
        {
            _db = db;
            _Environment = Environment;
        }
        [Route("Send_TO_Member/{name}/{NO}")]
        [HttpGet]
        public string Send_TO_Member(string name,int NO)
        {
            var Feedbackdata = _db.SystemFeedbackForms.Include(f => f.Questionnaire).Where(x => x.ID == NO).FirstOrDefault();
            Feedbackdata!.Questionnaire.deal_with_person = name;
            _db.OrderPrincipalDataModels.Add(new OrderPrincipalDataModel{order_id = Feedbackdata.ID, deal_with_persons = name});
            _db.SaveChanges();
            var message = @$"被分配以下工作。完成後，請用以下連結，編寫結果。<br><a href=""https://localhost:44439/Administrator?id={NO}"">https://localhost:44439/Administrator?id={NO}</a>";
            var mail = new User_TO_Administrate(Feedbackdata!,new List<string>(),message,_Environment,true);
            mail.RecipientAddress = new List<string> {name};
            MailHelper<User_TO_Administrate>.SendMail(mail);
            Send_mail_to_User(Feedbackdata);
            return "OK";
        }
        [HttpGet]
        public void Send_mail_to_User(SystemFeedbackForm Feedbackdata)
        {
            var mail = new User_TO_Administrate(Feedbackdata!,new List<string>(),"已在處理你的需求!",_Environment);
            mail.RecipientAddress = new List<string> {Feedbackdata.Fill_In_Person};
            MailHelper<User_TO_Administrate>.SendMail(mail);
        }
        [Route("Downfile/{filename}")]
        [HttpGet]
        public ActionResult Downfile(string Filename)
        {
            try
            {
                FileStream fileStream = new FileStream(_Environment.WebRootPath + "/file/" + Filename, FileMode.Open);
                return File(fileStream, "application/octet-stream");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
   
}