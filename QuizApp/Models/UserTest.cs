using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    [Table("UserTests")]
    public class UserTest
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Количество вопросов")]
        public int Questions { get; set; }
        [DisplayName("Количество ответов")]
        public int Answers { get; set; }
        [DisplayName("Начало теста")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DisplayName("Конец теста")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }


    }
}