using System;
using System.ComponentModel.DataAnnotations;


namespace ASPNetCoreApp.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно заполните Имя сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя сотрудника не может быть таким коротким")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Обязательно заполните Фамилию сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Фамилия сотрудника не может быть такой короткой")]
        [RegularExpression("[a-zA-Za-яА-Я]{3,20}",ErrorMessage ="Фамилия не подходит ограничениям")]
        public string LastName { get; set; }


        [Range(minimum:10,maximum:200, ErrorMessage = "На этом сайте нечего делать детям")]
        public int Age { get; set; }

        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
    }
}
