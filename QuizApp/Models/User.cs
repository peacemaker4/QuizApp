using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Заполните имя")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Заполните фамилию")]
        public string LastName { get; set; }
    }
}