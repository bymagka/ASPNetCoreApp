using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Services.Data;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.Extensions.Logging;
using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Domain.Entities;

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
            return View(employeeService.GetAll().Select(x=> new EmployeeViewModel 
                                                        { 
                                                           Age = x.Age,
                                                           BirthdayDate = x.BirthdayDate,
                                                           FirstName = x.Name,
                                                           LastName = x.LastName,
                                                           Id = x.Id,
                                                        }));
        }

        public IActionResult Details(int? id)
        {

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
           

            var emp = employeeService.GetById((int)id);

            if (emp is null) return RedirectToAction("PageNotFound", "Home");

            var model = new EmployeeViewModel
            {
                Id = emp.Id,
                Age = emp.Age,
                FirstName = emp.Name,
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
            

            var emp = employeeService.GetById((int)id);

            if (emp is null) return RedirectToAction("PageNotFound", "Home");

            var model = new EmployeeViewModel
            {
                Age = emp.Age,
                FirstName = emp.Name,
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
                Name = empModel.FirstName,
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
