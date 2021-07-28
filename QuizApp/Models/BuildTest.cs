using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public enum TestTypes
    {
        Mix,
        Numbers,
        Strings
    }
    public class BuildTest
    {
        public int Id { get; set; }
        [DisplayName("Количество вопросов")]
        [Range(5, 20, ErrorMessage = "Заполните количество вопросов (5-20)")]
        public int Questions { get; set; }
        [DisplayName("Тип вопросов")]
        [Required(ErrorMessage = "Заполните тип вопросов")]
        public TestTypes TestType { get; set; }

    }
}