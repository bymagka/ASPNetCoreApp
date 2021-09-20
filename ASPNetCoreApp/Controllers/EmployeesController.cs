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
            Employee _Employee = EmployeesList.Single(x => x.Id == id);

            if (_Employee is null) return NotFound();
            

            return View(_Employee);
        }

        public IActionResult Delete(int? id)
        {
            EmployeesList = EmployeesList.Select(x => x)
                            .Where(x => x.Id != id)
                            .ToList();

            //если этого не сделать, то при обновлении страницы контроллер создается заново и получает данные класса TestData в конструкторе
            _wasChanged = true;

            return RedirectToAction("Index");
        }


        //GET
        public IActionResult Edit(int? id)
        {
            Employee _Employee = EmployeesList.Single(x => x.Id == id);

            if (_Employee is null) return NotFound();

            //если этого не сделать, то при обновлении страницы контроллер создается заново и получает данные класса TestData в конструкторе, то есть затирая потенциальные изменения экземпляров

            _wasChanged = true;

            return View(_Employee);
        }


        //POST
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            //Сохраним изменения. Почему с представления редактирования приходит другая ссылка на модель, нежели модель переданная по ссылке на представление редактирования?
            // или по методу post отправляется копия полученной модели c представления Edit?.

             //Поэтому пришлось опять искать объект в списке и переназначать ему поля.

             //как кажется, реализован костыль здесь у меня.


             _wasChanged = true;

            var oldEmployee = EmployeesList.Single(x => x.Id == emp.Id);

            if (oldEmployee is not null)
            {
                //oldEmployee = emp;
                oldEmployee.BirthdayDate = emp.BirthdayDate;
                oldEmployee.Age = emp.Age;
                oldEmployee.FirstName = emp.FirstName;
                oldEmployee.LastName = emp.LastName;

            }

            return RedirectToAction("Index");
        }
    }
}
