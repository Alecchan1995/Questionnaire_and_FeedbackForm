using Microsoft.AspNetCore.Mvc;
//using AD.Models;

namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdApiController : ControllerBase
    {
        private readonly string[] propertyList = new string[] { "employeeID", "department", "SAMAccountName", "mailNickname", "telephoneNumber", "mail" };
        private readonly IADService _adSerive;
        public AdApiController(IADService adService)
        {
            _adSerive = adService;
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            _adSerive.GetUsers();
            return Ok();
        }

        [HttpGet]
        [Route("TPEadName/{adName}")]
        public ActionResult GetTPEADUserInfoByADName(string adName)
        {
            var info = _adSerive.GetTPEADUserInfoByADName(adName, propertyList);
            return Ok(info);
        }
        [HttpGet]
        [Route("KSADadName/{adName}")]
        public ActionResult GetKSADADUserInfoByADName(string adName)
        {
            var info = _adSerive.GetKSADADUserInfoByADName(adName, propertyList);
            return Ok(info);
        }
        [HttpGet]
        [Route("empId/{id}")]
        public ActionResult GetADUserInfoById(string id)
        {
            var info = _adSerive.GetADUserInfoById(id, propertyList);
            return Ok(info);
        }

        [HttpGet]
        [Route("department/{department}")]
        public ActionResult GetADUserInfoByDepartment(string department, string division)
        {
            var info = _adSerive.GetADUserInfoByDepartment(department, division);
            return Ok(info);
        }

        [HttpGet]
        [Route("outlook/groupName/{groupName}")]
        public ActionResult GetOutlookGroupUser(string groupName)
        {
            var info = _adSerive.GetOutlookGroupUser(groupName, new string[] {"member"});
            return Ok(info);
        }
    
        [HttpGet]
        [Route("adName/{adName}/outlookGroup")]
        public ActionResult GetADUserOutlookGroupByADName(string adName)
        {
            var info = _adSerive.GetTPEADUserInfoByADName(adName, new string[] {"memberOf"});
            return Ok(info);
        }
    }
}