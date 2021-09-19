using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Models;

namespace ASPNetCoreApp.Data
{
    public static class TestData
    {
        public static  List<Employee> EmployeesList { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Age = 27, BirthdayDate = DateTime.Now.AddYears(-27) },
            new Employee { Id = 2, LastName = "Кучаев", FirstName = "Константин", Age = 31, BirthdayDate = DateTime.Now.AddYears(-31) },
            new Employee { Id = 3, LastName = "Семёнов", FirstName = "Семён", Age = 18, BirthdayDate = DateTime.Now.AddYears(-18) },
        };
    }
}
