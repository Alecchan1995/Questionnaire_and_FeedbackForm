using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire_and_FeedbackForm.Models;
using Questionnaire_and_FeedbackForm.Models.MailTemplate;
using Questionnaire_and_FeedbackForm.Helpers;
namespace Questionnaire_and_FeedbackForm.WebApiController
{
     
    [ApiController]
    [Route("api/[controller]")]
    public class EditSystemDataApiController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        public  EditSystemDataApiController(QuestionnaireDataDBContext db){
            _db=db;
        }
        [Route("GETSystemName")]
        [HttpGet]
        public List<string> getsystemname(){
            var data = _db.ModelOptions.Where( x=>x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();
            return data;
        }
        [Route("AddSystemName/{newsystemname}")]
        [HttpGet]
        public List<string> addsystemname(string newsystemname){
            _db.ModelOptions.Add(new  ModelOption{GroupKey="",OptionColumn="SystemFeedbackForms.System_Name",OptionItem=newsystemname});
            _db.ModelOptions.Add(new  ModelOption{GroupKey="",OptionColumn=$@"SystemFeedbackForms.{newsystemname}_Maintainer",OptionItem=newsystemname});
            _db.SaveChanges();
            return _db.ModelOptions.Where( x=>x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();
        }
        [Route("DeleteSystemName/{systemname}")]
        [HttpGet]
        public List<string> Deletesystemname(string systemname)
        {
            //名字表單delete
            var deletedata = _db.ModelOptions.Where( x=>x.OptionColumn == "SystemFeedbackForms.System_Name" && x.OptionItem == systemname).FirstOrDefault();
            _db.ModelOptions.Remove(deletedata!);
            //有權限delete SystemFeedbackForms.CMS_Maintainer
            var deletememberList = _db.ModelOptions.Where( x=>x.OptionColumn == $@"SystemFeedbackForms.{systemname}_Maintainer" && x.OptionItem == systemname).ToList();
            foreach (var datas in deletememberList){
                 _db.ModelOptions.Remove(datas);
            }
            _db.SaveChanges();
            return  _db.ModelOptions.Where( x=>x.OptionColumn == "SystemFeedbackForms.System_Name").Select(x=>x.OptionItem).ToList();
        }

        [Route("GETSystemmember/{systemname}")]
        [HttpGet]
        public List<string> getsystemmember(string systemname){
            List<string> member = new List<string>(); 
            member = _db.ModelOptions.Where( x=>x.OptionColumn == $@"SystemFeedbackForms.{systemname}_Maintainer" && x.GroupKey != "Lender").Select(x=>x.OptionItem).ToList();
            string lender = _db.ModelOptions.Where( x=>x.OptionColumn == $@"SystemFeedbackForms.{systemname}_Maintainer" && x.GroupKey == "Lender").Select(x=>x.OptionItem).FirstOrDefault()!;
            if(lender != null){
                member.Add(lender+$@"-> Lender");
            }
            return member ;
        }

        [Route("GETMemberrole")]
        [HttpGet]
        public List<string> getmemberrole(){
            return  _db.ModelOptions.Where( x=>x.OptionColumn == $@"SystemFeedbackForms.Member_Role").Select(x=>x.OptionItem).ToList();
        }
        [Route("AddMember/{systemname}/{role}/{member}")]
        [HttpGet]
        public List<string> addmember(string systemname,string member,string role){
            _db.ModelOptions.Add(new  ModelOption{GroupKey=role,OptionColumn=$@"SystemFeedbackForms.{systemname}_Maintainer",OptionItem=member});
            _db.SaveChanges();
            return  getsystemmember(systemname);
        }
        [Route("DeleteMember/{systemname}/{role}/{member}")]
        [HttpGet]
        public List<string> deletemember(string systemname,string member,string role){
            var data =  _db.ModelOptions.Where( x=>x.OptionColumn == $@"SystemFeedbackForms.{systemname}_Maintainer" && x.OptionItem == member).FirstOrDefault();
            _db.ModelOptions.Remove(data!);
            _db.SaveChanges();
            return  getsystemmember(systemname);
        }
    }
}