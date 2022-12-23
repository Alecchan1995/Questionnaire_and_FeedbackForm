using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire_and_FeedbackForm.Models;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using Microsoft.Extensions.Options;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancelOrderAPIController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly IPModel _ip;
        public CancelOrderAPIController(QuestionnaireDataDBContext db,IOptions<IPModel> options)
        {
            _db = db;
            _ip = options.Value;
        }
        public class canceldata{
            public string no{get;set;} ="";
            public string data{get;set;} ="";
        }
        [Route("GetCancelOrderOption")]
        [HttpGet]
        public List<ModelOption> GetCancelOption()
        {
            return _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.Cancel_Order_Option").ToList();
        }
        [Route("SaveData")]
        [HttpPost]
        public string SaveData(canceldata canceldata)
        {
            var data = _db.Questionnaires.Include(x => x.SystemFeedbackForm).Where(x => x.ID.ToString() == canceldata.no).FirstOrDefault();
            data!.deal_with_idea = "Cancel: "+canceldata.data;
            data!.SystemFeedbackForm.deal_with_state = "Pending";
            data!.deal_with_time = DateTime.Now;
            _db.SaveChanges();
            List<string> name_list = new List<string>();
            var mail = new Cancel_order_to_User_and_Administrate(data.SystemFeedbackForm,"問題要取消!",_ip);
            mail.RecipientAddress = new List<string>{data!.deal_with_person};
            mail.CCAddress = new List<string>{data!.principal};
            MailHelper<Cancel_order_to_User_and_Administrate>.SendMail(mail);
            return "OK";
        }
        [Route("GetPrivateData")]
        [HttpGet]
        public List<Questionnaire> Yourself_data()
        {
            //var AutoHelpdata = _db.Questionnaires.Include(f =>f.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person == User.Identity.Name).ProjectTo<AutoHelpModelDTO>(_mapper.ConfigurationProvider).ToList();
            //var AutoHelpdatas = new AutoHelpModelDTO();
            //AutoHelpdata = Mapper.<AutoHelpModelDTO>(AutoHelpdata);
            return _db.Questionnaires.Include(f => f.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person.Contains(User.Identity!.Name!.Replace("COMPAL\\","")) == true && x.SystemFeedbackForm.deal_with_state != "Resolved" && x.SystemFeedbackForm.deal_with_state != "Pending").OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
            //return new Questionnaire();

        }
    }
}