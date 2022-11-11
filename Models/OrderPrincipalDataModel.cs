using System.ComponentModel.DataAnnotations.Schema;
namespace Questionnaire_and_FeedbackForm.Models
{
    public class OrderPrincipalDataModel {
        public int ID {get;set;}
        public int order_id {get;set;} 
        public DateTime access_time {get;set;} =  DateTime.Now;
        public string deal_with_persons {get;set;} = "";
        
    }
}