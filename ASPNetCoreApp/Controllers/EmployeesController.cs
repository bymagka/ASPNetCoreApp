using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Models;
using ASPNetCoreApp.Data;
using ASPNetCoreApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ASPNetCoreApp.ViewModels;

namespace ASPNetCoreApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeesController> _Logger;

      
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            _Logger = logger;

        }

        public IActionResult Index()
        {
            return View(employeeService.GetAll());
        }

        public IActionResult Details(int? id)
        {

            if (id is null) return RedirectToAction("PageNotFound", "Home");

            var employee = employeeService.GetById((int)id);

            if (employee is null) return RedirectToAction("PageNotFound", "Home");

            return View(new EmployeeViewModel{BirthdayDate = employee.BirthdayDate});
        }

        public IActionResult Create()
        {
            return View("Edit",new EmployeeViewModel() { FirstName=null,LastName=null});
        }

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id is null) return RedirectToAction("PageNotFound", "Home");

            if ((int)id < 0) return RedirectToAction("PageNotFound", "Home");

            var emp = employeeService.GetById((int)id);

            if (emp is null) return RedirectToAction("PageNotFound", "Home");
            
            var model = new EmployeeViewModel
            {
                Id = emp.Id,
                Age = emp.Age,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                BirthdayDate = emp.BirthdayDate,
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            employeeService.Delete(id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        //GET
        public IActionResult Edit(int? id)
        {
            if (id is null) return RedirectToAction("PageNotFound", "Home");

            var emp = employeeService.GetById((int)id);

            if(emp is null) return RedirectToAction("PageNotFound", "Home");

            var model = new EmployeeViewModel
            {
                Age = emp.Age,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                BirthdayDate = emp.BirthdayDate,
            };

            return View(model);
        }


        //POST
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel empModel)
        {

            //if (!ModelState.IsValid) return View(empModel);

            var employee = new Employee
            {
                Id = empModel.Id,
                Age = empModel.Age,
                FirstName = empModel.FirstName,
                LastName = empModel.LastName,
                BirthdayDate = empModel.BirthdayDate,
            };

            if (employee.Id == 0) employeeService.Add(employee);
            else employeeService.Update(employee);

            
            return RedirectToAction("Index");
        }
        #endregion
    }
}
