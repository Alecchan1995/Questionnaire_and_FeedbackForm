#nullable disable warnings
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Questionnaire_and_FeedbackForm.Models
{
    public class Questionnaire
    {
        [ForeignKey("SystemFeedbackForm")]
        public int ID { get; set; }
        public string Service_Fraction { get; set; } = "";
        public string Process_Fraction { get; set; } = "";
        public string Other_Idea { get; set; } = "";
        public string deal_with_idea {get; set;} = "";
        public string deal_with_person {get; set;} = "";
        public string deal_with_person_telephoneNumber {get;set;} = "";
        public string principal {get;set;} = "";
        public DateTime deal_with_time {get;set;} =  DateTime.Now;
        
        public virtual SystemFeedbackForm SystemFeedbackForm { get; set; } 

    }
}