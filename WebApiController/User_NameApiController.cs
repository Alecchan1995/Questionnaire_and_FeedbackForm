using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Questionnaire_and_FeedbackForm.Helpers;
using Questionnaire_and_FeedbackForm;
namespace System_Questionnaire.WebControllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class User_NameController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {   
            var name = User.Identity!.Name!.Replace("COMPAL\\","");
            if (name == null)
            {
                name = "None";
            }
            return name;
        }
        
    }
}