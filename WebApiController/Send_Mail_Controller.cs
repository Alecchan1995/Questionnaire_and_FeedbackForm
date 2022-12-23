using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class Send_Mail_Controller : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly IWebHostEnvironment _Environment;
        private readonly IPModel _ip;
        private readonly IUserService _UserSerive;
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
         public Send_Mail_Controller(QuestionnaireDataDBContext db,IMapper mapper,IWebHostEnvironment Environment,IOptions<IPModel> options,IUserService UserSerive)
        {
            _db = db;
            _Environment = Environment;
            _ip = options.Value;
            _UserSerive = UserSerive;
        }
        [Route("Send_TO_Member/{name}/{NO}")]
        [HttpGet]
        public void Send_TO_Member(string name,int NO)
        {
            var Feedbackdata = _db.SystemFeedbackForms.Include(f => f.Questionnaire).Where(x => x.ID == NO).FirstOrDefault();
            Feedbackdata!.Questionnaire.deal_with_person = name;
            try{
                Feedbackdata!.Questionnaire.deal_with_person_telephoneNumber = _UserSerive!.GetTPEADUserInfoByADName(User.Identity!.Name!.Replace("COMPAL\\",""), propertyList).Where(x => x.Property == "telephoneNumber").FirstOrDefault()!.Value.First();
            }
            catch{ Feedbackdata!.Questionnaire.deal_with_person_telephoneNumber = "沒有分機號碼";}
            _db.OrderPrincipalDataModels.Add(new OrderPrincipalDataModel{order_id = Feedbackdata.ID, deal_with_persons = name});
            _db.SaveChanges();
            var message = @$"被分配以下工作。完成後，請用以下連結，編寫結果。<br><a href=""{_ip.Frontweb}/Administrator?id={NO}"">{_ip.Frontweb}/Administrator?id={NO}</a>";
            var mail = new User_TO_Administrate(Feedbackdata!,new List<string>(),message,_Environment,_ip,true);
            // 發給Member ， 副本是Lender
            mail.RecipientAddress = new List<string> {name};
            mail.CCAddress = new List<string> {Feedbackdata.Questionnaire.principal};
            MailHelper<User_TO_Administrate>.SendMail(mail);
            Send_mail_to_User(Feedbackdata);
        }

        [HttpGet]
        public void Send_mail_to_User(SystemFeedbackForm Feedbackdata)
        {
            //發信給User，沒有給副本。
            var been_has_send = _db.OrderPrincipalDataModels.Where(x=>x.order_id == Feedbackdata.ID).ToList();
            if(been_has_send.Count == 1){
                string message = Send_mail_to_User_message(Feedbackdata);
                var mail = new User_TO_Administrate(Feedbackdata!,new List<string>(),message,_Environment,_ip);
                mail.RecipientAddress = new List<string> {Feedbackdata.Fill_In_Person};
                MailHelper<User_TO_Administrate>.SendMail(mail);
            }
        }
        [Route("Send_function")]
        [HttpGet]
        public string Send_mail_to_User_message(SystemFeedbackForm Feedbackdata){
            //"已在處理你的需求!"
            string message =@$"【需求受理通知】<br>
                            {Feedbackdata.Fill_In_Person+Feedbackdata.FillInPersontelephoneNumber}您好：<br>
                            系統關於您所提交的需求，後續將由處理人員來為您服務。";
            return message;
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