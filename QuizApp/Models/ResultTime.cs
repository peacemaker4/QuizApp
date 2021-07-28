using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class ResultTime
    {
        [DisplayName("Начало теста")]
        [Required(ErrorMessage = "Заполните дату начало")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }
        [DisplayName("Конец теста")]
        [Required(ErrorMessage = "Заполните дату конца")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
    }
}