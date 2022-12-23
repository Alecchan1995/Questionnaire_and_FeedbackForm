#nullable disable warnings
using System.Text.Json.Serialization;
namespace Questionnaire_and_FeedbackForm.Models
{
    public class SystemFeedbackForm
    {

        public int ID { get; set; }
        public string description { get; set; } = "";
        public string filename { get; set; } = "";
        public string Fill_In_Person { get; set; } = "";
        public string FillInPersontelephoneNumber {get;set;} = "";
        public string System_Name { get; set; } = "";
        public DateTime Send_Time {get;set;} = DateTime.Now;
        public string deal_with_state {get; set;} = "New";
        public string Problem_Type {get;set;} = "";
        [JsonIgnore]
        public virtual Questionnaire Questionnaire { get; set; } 
    }
}