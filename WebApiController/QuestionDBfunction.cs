using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire_and_FeedbackForm.Models;
using Questionnaire_and_FeedbackForm.Helpers;
using Questionnaire_and_FeedbackForm.Models.Mail;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;

namespace Questionnaire_and_FeedbackForm.WebControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionDBfunctionController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
       
        public QuestionDBfunctionController(QuestionnaireDataDBContext db)
        {
            _db = db;
        }

        [Route("Save")]
        [HttpPost]
        public string SaveData(Questionnaires Questionnaires)
        {
            var data = _db.Questionnaires.Find(Questionnaires.SystemFeedbackForm.ID);
            data!.Process_Fraction = Questionnaires.Process_Fraction;
            data.Service_Fraction = Questionnaires.Service_Fraction;
            data.Other_Idea = Questionnaires.Other_Idea;
            _db.SaveChanges();
            //  var mail = new User_TO_Administrate(data);
            //     MailHelper<User_TO_Administrate>.SendMail(mail);
            return "OK";
        }
        [Route("GetQuestionnaireForm/{QuestionnaireID}")]
        [HttpGet]
        public Questionnaire GetQuestionnaireForm(int QuestionnaireID)
        {
            var QuestionnaireFormData = _db.Questionnaires.Include(f => f.SystemFeedbackForm).Where(x => x.ID == QuestionnaireID).FirstOrDefault();
            if (QuestionnaireFormData == null)
            {
                return new Questionnaire
                {
                    Service_Fraction = "",
                    Process_Fraction = "",
                    Other_Idea = "",
                    SystemFeedbackForm = new SystemFeedbackForm
                    {
                        description = "",
                        filename = "",
                        Fill_In_Person  = "",
                        System_Name  = ""
                    }
                };
            }
            return QuestionnaireFormData!;
        }
    }
}