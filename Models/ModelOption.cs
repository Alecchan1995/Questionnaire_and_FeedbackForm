using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire_and_FeedbackForm.Models
{
    public class ModelOption
    {
        [Key]
        public int ID { get; set; }

        public string GroupKey { get; set; } = "";

        [Required]
        public string OptionColumn { get; set; } = "";

        [Required]
        public string OptionItem { get; set; } = "";
    }
}