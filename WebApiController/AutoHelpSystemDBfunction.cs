using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoHelpSystemDBfunctionController : Controller

    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _Environment;
    
        public AutoHelpSystemDBfunctionController(QuestionnaireDataDBContext db, IMapper mapper, IWebHostEnvironment Environment)
        {
            _mapper = mapper;
            _db = db;
            _Environment = Environment;
        }
        public class Filename{
            public string filename{get;set;}="";
        }
        [Route("GetPrivateData")]
        [HttpGet]
        public List<Questionnaire> Yourself_data()
        {
            //var AutoHelpdata = _db.Questionnaires.Include(f =>f.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person == User.Identity.Name).ProjectTo<AutoHelpModelDTO>(_mapper.ConfigurationProvider).ToList();
            //var AutoHelpdatas = new AutoHelpModelDTO();
            //AutoHelpdata = Mapper.<AutoHelpModelDTO>(AutoHelpdata);
            return _db.Questionnaires.Include(f => f.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person.Contains(User.Identity!.Name!.Replace("COMPAL\\","")) == true).OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
            //return new Questionnaire();

        }
        [Route("GetAllData")]
        [HttpGet]
        public List<Questionnaire> All_data()
        {
            return _db.Questionnaires.Include(f => f.SystemFeedbackForm).OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
        }
        [Route("GetStateType")]
        [HttpGet]
        public List<string> GetStateData()
        {
            return _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.deal_with_state").Select(x => x.OptionItem).ToList();
        }
        [Route("Senddata")]
        [HttpPost]
        public Questionnaire Senddata(Questionnaires datas)
        {
            var data = _db.Questionnaires.Include(x => x.SystemFeedbackForm).Where(x => x.ID == datas.SystemFeedbackForm.ID).FirstOrDefault();
            data!.deal_with_idea = datas.deal_with_idea;
            data.SystemFeedbackForm.deal_with_state = datas.SystemFeedbackForm.deal_with_state;
            data.deal_with_person = datas.deal_with_person;
            data.deal_with_time = DateTime.Now;
            _db.SaveChanges();
            if (data.SystemFeedbackForm.deal_with_state == "Resolved"){
                var mail = new Administrate_TO_User(data);
                MailHelper<Administrate_TO_User>.SendMail(mail);
            }
            return data;
        }
        [Route("GetSearchPeople")]
        [HttpPost]
        public List<Questionnaire> GetSearchPeople(string user)
        {
            var data = _db.Questionnaires.Include(x => x.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person == user).ToList();
            return data;
            //return new List<Questionnaire>();
        }

        [Route("Downfile")]
        [HttpPost]
        public ActionResult Downfile(Filename Filename)
        {
            try
            {
                FileStream fileStream = new FileStream(_Environment.WebRootPath + "/file/" + Filename.filename, FileMode.Open);
                return File(fileStream, "application/octet-stream");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        //只可以跟系統名帛
        [Route("GetYourmaintainer")]
        [HttpGet]
        public List<Questionnaire> Yourmaintainer_data()
        {
            var user = User!.Identity!.Name!.Replace("COMPAL\\","")!;
            var maintainer_list = new List<string>();
            List<Questionnaire> your_data = new List<Questionnaire>();
            var SystemName = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();    
            foreach(var systemname in SystemName){
                if(_db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms."+systemname+"_Maintainer").Select(x=>x.OptionItem).First().IndexOf(user!) != -1){
                    maintainer_list.Add(systemname);
                }
            }
            // foreach(var data in maintainer_list){
            //     var datas = _db.Questionnaires.Include(f => f.SystemFeedbackForm ).Where(x => x.SystemFeedbackForm.Codata).ToList();
            //     foreach(var datadata in datas){
            //         your_data.Add(datadata);
            //     }
            // }
            var datas = _db.Questionnaires.Include(f => f.SystemFeedbackForm ).Where(x => maintainer_list.Contains(x.SystemFeedbackForm.System_Name)).OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
            return datas;
        }
        //AutoHelpSystemDBfunction/GetDeal_With_Person/{Id}
        [Route("GetDeal_With_Person/{id}")]
        [HttpGet]
        public List<OrderPrincipalDataModel> Get_Record(int id){
            return _db.OrderPrincipalDataModels.Where( x => x.order_id == id).ToList();
        }
        public class Principal{
            public string person{get;set;}="";
            public int ID{get;set;}=0;
        }
        [Route("AddOrderPrincipal")]
        [HttpPost]
        public string AddPrincipal(Principal Person){
            _db.OrderPrincipalDataModels.Add(new OrderPrincipalDataModel{order_id = Person.ID, deal_with_persons = Person.person});
            _db.SaveChanges();
            return "Successful";
        }

    }
}