using Microsoft.AspNetCore.Mvc;
using Questionnaire_and_FeedbackForm.Models;
namespace Questionnaire_and_FeedbackForm.WebApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancelOrderAPIController : Controller
    {
        private readonly QuestionnaireDataDBContext _db;
        public CancelOrderAPIController(QuestionnaireDataDBContext db)
        {
            _db = db;
        }
        [Route("GetCancelOrderOption")]
        [HttpGet]
        public List<ModelOption> GetCancelOption()
        {
            return _db.ModelOptions.Where(x => x.OptionColumn == "SystemFeedbackForms.Cancel_Oder_Option").ToList();
        }
    }
}