namespace Questionnaire_and_FeedbackForm.Models
{
    public class SystemFeedbackForms
    {

        public int ID { get; set; }
        public string description { get; set; } = "";
        public string filename { get; set; } = "";
        public string Fill_In_Person { get; set; } = "";
        public string System_Name { get; set; } = "";
        public string deal_with_state {get; set;} = "";
        public string Problem_Type {get;set;} = "";
      
    }
}