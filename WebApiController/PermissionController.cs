using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : Controller
    { 
        private readonly QuestionnaireDataDBContext _db;
        public PermissionController(QuestionnaireDataDBContext db){
            _db=db;
        }
        [Route("GetRole")]
        [HttpGet]
        public RoleAndPermission RoleAndPermission(){
            var permission = _db.RoleAndPermissions.Where(x => x.User ==  User.Identity!.Name!).FirstOrDefault();
            return permission!;
        }

        [Route("GetPrincipal")]
        [HttpGet]
        public List<string> GetPrincipal(){
            return _db.RoleAndPermissions.Where(x => x.Role == "Administrator").Select(x => x.User).ToList();
        }
        [Route("AddPrincipal/{name}")]
        [HttpGet]
        public List<string> AddPrincipal(string name){
            _db.RoleAndPermissions.Add(new RoleAndPermission{User="COMPAL\\"+name,Role="Administrator"});
            _db.SaveChanges();
            return _db.RoleAndPermissions.Where(x => x.Role == "Administrator").Select(x => x.User).ToList();
        }
        [Route("DeletePrincipal/{name}")]
        [HttpGet]
        public List<string> DeletePrincipal(string name){
            var permission = _db.RoleAndPermissions.Where(x => x.User ==  "COMPAL\\"+name).FirstOrDefault();
            _db.RoleAndPermissions.Remove(permission!);
            _db.SaveChanges();
            return _db.RoleAndPermissions.Where(x => x.Role == "Administrator").Select(x => x.User).ToList();
        }
    }
}