using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace System_Questionnaire.WebControllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class User_NameController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var name = User.Identity?.Name;
           // Console.WriteLine(name);
            if (name == null)
            {
                name = "None";
            }
            return name;
        }
        
    }
}