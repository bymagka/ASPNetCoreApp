using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Models;

namespace ASPNetCoreApp.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> EmployeesList = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Age = 27, BirthdayDate = DateTime.Now.AddYears(-27) },
            new Employee { Id = 2, LastName = "Кучаев", FirstName = "Константин", Age = 31, BirthdayDate = DateTime.Now.AddYears(-31) },
            new Employee { Id = 3, LastName = "Семёнов", FirstName = "Семён", Age = 18, BirthdayDate = DateTime.Now.AddYears(-18) },
        };

        public IActionResult Index()
        {
            return View(EmployeesList);
        }

        public IActionResult Details(int? id)
        {
            Employee Employee = EmployeesList.Single(x => x.Id == id);

            return View(Employee);
        }
    }
}
