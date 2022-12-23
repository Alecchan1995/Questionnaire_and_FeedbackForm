using Microsoft.AspNetCore.Mvc;
//using AD.Models;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
        private readonly IUserService _UserSerive;
        public UserApiController(IUserService UserSerive)
        {
            _UserSerive = UserSerive;
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            _UserSerive.GetUsers();
            return Ok();
        }

        [HttpGet]
        [Route("TPEadName/{adName}")]
        public ActionResult GetTPEADUserInfoByADName(string adName)
        {
            var info = _UserSerive.GetTPEADUserInfoByADName(adName, propertyList);
            return Ok(info);
        }
        [HttpGet]
        [Route("KSADadName/{adName}")]
        public ActionResult GetKSADADUserInfoByADName(string adName)
        {
            var info = _UserSerive.GetKSADADUserInfoByADName(adName, propertyList);
            return Ok(info);
        }
        [HttpGet]
        [Route("empId/{id}")]
        public ActionResult GetADUserInfoById(string id)
        {
            var info = _UserSerive.GetADUserInfoById(id, propertyList);
            return Ok(info);
        }

        [HttpGet]
        [Route("department/{department}")]
        public ActionResult GetADUserInfoByDepartment(string department, string division)
        {
            var info = _UserSerive.GetADUserInfoByDepartment(department, division);
            return Ok(info);
        }

        [HttpGet]
        [Route("outlook/groupName/{groupName}")]
        public ActionResult GetOutlookGroupUser(string groupName)
        {
            var info = _UserSerive.GetOutlookGroupUser(groupName, new string[] {"member"});
            return Ok(info);
        }
    
        [HttpGet]
        [Route("adName/{adName}/outlookGroup")]
        public ActionResult GetADUserOutlookGroupByADName(string adName)
        {
            var info = _UserSerive.GetTPEADUserInfoByADName(adName, new string[] {"memberOf"});
            return Ok(info);
        }
    }
}