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
        private static IEnumerable<Employee> EmployeesList;

        private static bool _wasChanged = false; 

        public EmployeesController()
        {
            if (!_wasChanged)
            {
                EmployeesList = TestData.EmployeesList;
            }

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

        public IActionResult Delete(int? id)
        {
            EmployeesList = EmployeesList.Select(x => x)
                            .Where(x => x.Id != id)
                            .ToList();

            //если этого не сделать, то при обновлении страницы контроллер создается заново и получает данные класса TestData в конструкторе
            _wasChanged = true;

            return View("Index",EmployeesList);
        }
    }
}
