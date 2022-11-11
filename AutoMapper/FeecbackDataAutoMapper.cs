using AutoMapper;
using Questionnaire_and_FeedbackForm.Models;


namespace Questionnaire_and_FeedbackForm.AutoMapper
{
    public class FeecbackDataAutoMapper : Profile
    {
      public FeecbackDataAutoMapper(){
        CreateMap<SystemFeedbackFormDataDTO,SystemFeedbackForm>();
          // .ForMember(s => s.Questionnaire , x=>x.MapFrom(f=>new Questionnaire{Service_Fraction="",Process_Fraction="",System_Name=f.System_Name,Fill_In_Person=f.Fill_In_Person,Other_Idea=""}));
          //.ForMember(s => s.QuestionnaireData , x=>x.MapFrom<QuestionnaireDataDTO>(f=> f.QuestionnaireData));

        //  CreateMap<SystemFeedbackFormDataDTO.QuestionnaireDataDTO,QuestionnaireData>()
        //   .ForMember(s => s.ID , x=>x.Ignore())
        //   .ForMember(s => s.SystemFeedbackFormDataID , x=>x.Ignore());
         // .ForMember(s => s.QuestionnaireData , x=>x.MapFrom(f=>new QuestionnaireData{Service_Fraction="",Process_Fraction="",System_Name="",Fill_In_Person="",Other_Idea=""}));
        CreateMap<Questionnaires,Questionnaire>()
        .ForMember(s => s.SystemFeedbackForm , x=>x.MapFrom<SystemFeedbackForms>(t=>t.SystemFeedbackForm));
      }
    }
}
