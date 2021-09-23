using System;
using System.ComponentModel.DataAnnotations;


namespace ASPNetCoreApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
    }
}
