#nullable disable warnings
namespace Questionnaire_and_FeedbackForm.Models
{
    public class AutoHelpModelDTO
    {
        public int ID { get; set; }
        public string Service_Fraction { get; set; } 
        public string Process_Fraction { get; set; } 
        public string Other_Idea { get; set; } 
        public string deal_with_idea {get; set;} 
        public DateTime deal_with_time {get;set;} 
        public string description { get; set; } 
        public string filename { get; set; } 
        public string Fill_In_Person { get; set; } 
        public string System_Name { get; set; } 
        public DateTime Send_Time {get;set;} 
        public string deal_with_state {get; set;} 
        public string Problem_Type {get;set;} 
    }
}