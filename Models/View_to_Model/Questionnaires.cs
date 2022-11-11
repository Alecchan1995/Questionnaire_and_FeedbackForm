#nullable disable warnings
namespace Questionnaire_and_FeedbackForm.Models
{
    public class Questionnaires
    {
        public string Service_Fraction { get; set; } = "";
        public string Process_Fraction { get; set; } = "";
        public string Other_Idea { get; set; } = "";
        public string deal_with_idea {get; set;} = "";
        public string principal {get;set;} = "";
        public string deal_with_person {get; set;} = "";
        public SystemFeedbackForms SystemFeedbackForm { get; set; }
    }
}