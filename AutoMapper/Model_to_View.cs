using AutoMapper;
using Questionnaire_and_FeedbackForm.Models;
namespace Questionnaire_and_FeedbackForm.AutoMapper
{
    public class Model_to_View : Profile
    {
      public Model_to_View(){
        CreateMap<Questionnaire,AutoHelpModelDTO>();
          
        CreateMap<SystemFeedbackForm,AutoHelpModelDTO>();
        
      }
    }
}
