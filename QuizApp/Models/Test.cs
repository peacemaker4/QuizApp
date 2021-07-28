using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    [Table("Tests")]
    public class Test
    {
        public int Id { get; set; }
        [DisplayName("Вопрос")]
        public string Question { get; set; }
        [DisplayName("Подсказка")]
        public string Prompt { get; set; }
        [DisplayName("Ответ")]
        public string Answer { get; set; }

    }
}