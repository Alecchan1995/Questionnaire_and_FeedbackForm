using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackformDBfunctionController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
        private readonly IUserService _userSerive;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _Environment;
        private readonly IPModel _ip;
        public FeedbackformDBfunctionController(QuestionnaireDataDBContext db,IMapper mapper,IWebHostEnvironment Environment,IUserService userSerive,IOptions<IPModel> options)
        {
            _mapper = mapper;
            _db = db;
            _Environment = Environment;
            _userSerive=userSerive;
            _ip = options.Value;
        }

        [Route("Save")]
        [HttpPost]
        //回饋完成發出Mail通知
        public string SaveData(SystemFeedbackFormDataDTO SystemFeedbackFormData)
        {
            var Feedbackdata = new SystemFeedbackForm();
            Feedbackdata = _mapper.Map<SystemFeedbackForm>(SystemFeedbackFormData);
            Feedbackdata.Questionnaire=new Questionnaire();
            Feedbackdata.Fill_In_Person = Feedbackdata.Fill_In_Person.Replace("COMPAL\\","");
            //取分機
            try{
               Feedbackdata.FillInPersontelephoneNumber = _userSerive!.GetTPEADUserInfoByADName(Feedbackdata.Fill_In_Person, propertyList)!.Where(x => x.Property == "telephoneNumber")!.FirstOrDefault()!.Value.First();
            } 
            catch{  Feedbackdata.FillInPersontelephoneNumber = "沒有分機號碼"; }
            //取RD和Lender，給lender選處理人
            List<string> System_Maintainer = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms."+SystemFeedbackFormData.System_Name+"_Maintainer" && x.GroupKey == "RD").Select(x => x.OptionItem).ToList()!;
            //取lender
            var lender = _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms."+SystemFeedbackFormData.System_Name+"_Maintainer" && x.GroupKey == "Lender").Select(x => x.OptionItem).FirstOrDefault()!;
            Feedbackdata.Questionnaire.principal = lender;
            _db.SystemFeedbackForms.Add(Feedbackdata);
            _db.SaveChanges();
            System_Maintainer.Add(lender);
            //給lender
            var mail = new User_TO_Administrate(Feedbackdata,System_Maintainer,"新的需求，請分配!",_Environment,_ip);
            //先發給lender
            mail.RecipientAddress = new List<string> {lender};
            MailHelper<User_TO_Administrate>.SendMail(mail);
            return "OK";
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