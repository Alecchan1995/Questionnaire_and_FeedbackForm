namespace Questionnaire_and_FeedbackForm.Models
{
    public class SystemFeedbackFormDataDTO
    {

        public string description { get; set; } = "";
        public string filename { get; set; } = "";
        public string Fill_In_Person { get; set; } = "";
        public string System_Name { get; set; } = "";
        
       public DateTime Send_Time {get;set;} = DateTime.Now;
       public string Problem_Type {get;set;} = "";
    }


}