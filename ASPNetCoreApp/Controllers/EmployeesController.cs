using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Models;
using ASPNetCoreApp.Data;

namespace ASPNetCoreApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<Employee> EmployeesList;

        public EmployeesController()
        {
            EmployeesList = TestData.EmployeesList;
        }

        public IActionResult Index()
        {
            return View(EmployeesList);
        }

        public IActionResult Details(int? id)
        {
            Employee Employee = EmployeesList.Single(x => x.Id == id);

            if (Employee is null) return NotFound();
            

            return View(Employee);
        }
    }
}
