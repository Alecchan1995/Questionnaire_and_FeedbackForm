using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using System.Text.RegularExpressions;
namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackformDBfunctionController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
        private readonly IADService _adSerive;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _Environment;
        public FeedbackformDBfunctionController(QuestionnaireDataDBContext db,IMapper mapper,IWebHostEnvironment Environment,IADService adSerive)
        {
            _mapper = mapper;
            _db = db;
            _Environment = Environment;
            _adSerive=adSerive;
        }

        [Route("Save")]
        [HttpPost]
        public string SaveData(SystemFeedbackFormDataDTO SystemFeedbackFormData)
        {
            var Feedbackdata = new SystemFeedbackForm();
            Feedbackdata = _mapper.Map<SystemFeedbackForm>(SystemFeedbackFormData);
            Feedbackdata.Questionnaire=new Questionnaire();
            try{ Feedbackdata.Fill_In_Person = Feedbackdata.Fill_In_Person.Replace("COMPAL\\","")+_adSerive.GetTPEADUserInfoByADName(Feedbackdata.Fill_In_Person.Replace("COMPAL\\",""), propertyList).Where(x => x.Property == "telephoneNumber").FirstOrDefault()?.Value.First(); }
            catch(Exception e){Feedbackdata.Fill_In_Person = Feedbackdata.Fill_In_Person.Replace("COMPAL\\","");}
            List<string> System_Maintainer = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms."+SystemFeedbackFormData.System_Name+"_Maintainer").Select(x => x.OptionItem).FirstOrDefault()!.Split(",").ToList()!;
            
            foreach(var i in System_Maintainer){
                if(i.IndexOf(":PM") != -1){
                    string PM_Name =  i.Replace(":PM","");
                    Feedbackdata.Questionnaire.deal_with_person = PM_Name;
                    Feedbackdata.Questionnaire.principal = PM_Name;
                    _db.SystemFeedbackForms.Add(Feedbackdata);
                    _db.SaveChanges();
                    var mail = new User_TO_Administrate(Feedbackdata,System_Maintainer,"新的需求，請分配!",_Environment);
                    mail.RecipientAddress = new List<string> {PM_Name};
                    MailHelper<User_TO_Administrate>.SendMail(mail);
                    _db.OrderPrincipalDataModels.Add(new OrderPrincipalDataModel{order_id = Feedbackdata.ID, deal_with_persons = Feedbackdata.Questionnaire.deal_with_person});
                    _db.SaveChanges();
                    return "OK";
                }
            };
            return "Error";
        }
        [Route("GetSystemName")]
        [HttpGet]
        public List<string> GetSystemName()
        {
            var SystemName = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();
            return SystemName;
        }
        [Route("GetProblemType")]
        [HttpGet]
        public List<string> GetProblemType()
        {
            var SystemName = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.Problem_Type").Select(x=>x.OptionItem).ToList();
            return SystemName;
        }
        [Route("UploadFile")]
        [HttpPost]
        public async Task<string> UploadFile([FromForm] List<IFormFile> files)
        {
            string wwwPath = _Environment.WebRootPath;  // service path to wwwroot
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(wwwPath+"\\file\\"+formFile.FileName))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return "OK";
        }
    }
}