using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using Microsoft.Extensions.Options;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoHelpSystemDBfunctionController : Controller

    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _UserSerive;
        private readonly IWebHostEnvironment _Environment;
        private readonly IPModel _ip;
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
        public AutoHelpSystemDBfunctionController(QuestionnaireDataDBContext db, IMapper mapper, IWebHostEnvironment Environment,IOptions<IPModel> options,IUserService UserService)
        {
            _mapper = mapper;
            _db = db;
            _Environment = Environment;
            _ip = options.Value;
            _UserSerive = UserService;
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
            return _db.Questionnaires.Include(f => f.SystemFeedbackForm).Where(x => x.SystemFeedbackForm.Fill_In_Person.Contains(User.Identity!.Name!.Replace("COMPAL\\",""))).OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
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
        [Route("Check_User_Name/{name}")]
        [HttpGet]
        public List<string> check_name(string name)
        {
            var data = _UserSerive!.GetTPEADUserInfoByADName(name, propertyList).FirstOrDefault();
            if (data == null){return new List<string>{"Error"};}
            return new List<string>{"Ok"};
        }

        [Route("Senddata")]
        [HttpPost]
        public Questionnaire Senddata(Questionnaires datas)
        {
            var data = _db.Questionnaires.Include(x => x.SystemFeedbackForm).Where(x => x.ID == datas.SystemFeedbackForm.ID).FirstOrDefault();
            data!.deal_with_idea = datas.deal_with_idea;
            data.SystemFeedbackForm.deal_with_state = datas.SystemFeedbackForm.deal_with_state;
            //如果有改變處理人，再發信給新member和副件是lender
            if(data.deal_with_person != datas.deal_with_person){
                data.deal_with_person = datas.deal_with_person;
                try {
                    data.deal_with_person_telephoneNumber = _UserSerive!.GetTPEADUserInfoByADName(data.deal_with_person, propertyList).Where(x => x.Property == "telephoneNumber").FirstOrDefault()!.Value.First();
                }
                catch{
                    data.deal_with_person_telephoneNumber ="沒有分機號碼";
                }
                AddPrincipal(datas.deal_with_person,datas.SystemFeedbackForm.ID);
                var message = @$"被分配以下工作。完成後，請用以下連結，編寫結果。<br><a href=""{_ip.Frontweb}/Administrator?id={data.ID}"">{_ip.Frontweb}/Administrator?id={data.ID}</a>";
                var mail = new User_TO_Administrate(data.SystemFeedbackForm,new List<string>(),message,_Environment,_ip,true);
                mail.RecipientAddress = new List<string> {data.deal_with_person};
                mail.CCAddress = new List<string> {data.principal};
                MailHelper<User_TO_Administrate>.SendMail(mail);
            }
            data.deal_with_time = DateTime.Now;
            _db.SaveChanges();
            //完成再發信給User ，副本是Lender和Member
            if (data.SystemFeedbackForm.deal_with_state == "Resolved"){
                var mail = new Administrate_TO_User(data,_ip);
                mail.RecipientAddress = new List<string> {data.SystemFeedbackForm.Fill_In_Person};
                mail.CCAddress = new List<string>{data!.principal,data!.deal_with_person};
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
            //var user = User!.Identity!.Name!.Replace("COMPAL\\","")!;
            //var maintainer_list = new List<string>();
            //List<Questionnaire> your_data = new List<Questionnaire>();
            //var SystemName = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();    
            // foreach(var systemname in SystemName){
            //     if(_db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms."+systemname+"_Maintainer").Select(x=>x.OptionItem).First().IndexOf(user!) != -1){
            //         maintainer_list.Add(systemname);
            //     }
            // }
            // foreach(var data in maintainer_list){
            //     var datas = _db.Questionnaires.Include(f => f.SystemFeedbackForm ).Where(x => x.SystemFeedbackForm.Codata).ToList();
            //     foreach(var datadata in datas){
            //         your_data.Add(datadata);
            //     }
            // }
            //var datas = _db.Questionnaires.Include(f => f.SystemFeedbackForm ).Where(x => x.deal_with_person.Contains(User.Identity!.Name!.Replace("COMPAL\\","")) && x.SystemFeedbackForm.deal_with_state!="Resolved" && x.SystemFeedbackForm.deal_with_state!="Pending").OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
            //return datas;
            return _db.Questionnaires.Include(f => f.SystemFeedbackForm ).Where(x => x.deal_with_person.Contains(User.Identity!.Name!.Replace("COMPAL\\","")) && x.SystemFeedbackForm.deal_with_state!="Resolved" && x.SystemFeedbackForm.deal_with_state!="Pending").OrderBy(x => x.SystemFeedbackForm.Send_Time).ToList();
        }
        //AutoHelpSystemDBfunction/GetDeal_With_Person/{Id}
        [Route("GetDeal_With_Person/{id}")]
        [HttpGet]
        public List<OrderPrincipalDataModel> Get_Record(int id){
            return _db.OrderPrincipalDataModels.Where( x => x.order_id == id).ToList();
        }
        [Route("AddPrintciptal")]
        [HttpGet]
        public string AddPrincipal(string person,int ID){
            var data = _db.Questionnaires.Include(x => x.SystemFeedbackForm).Where(x => x.ID == ID).FirstOrDefault();
            data!.deal_with_person = person;
            _db.OrderPrincipalDataModels.Add(new OrderPrincipalDataModel{order_id = ID, deal_with_persons = person});
            _db.SaveChanges();
            return "Successful";
        }

    }
}