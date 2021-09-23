using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Models;
using ASPNetCoreApp.Data;
using ASPNetCoreApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

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

            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return RedirectToAction("PageNotFound", "Home");

            return RedirectToAction("Index");
        }


        //GET
        public IActionResult Edit(int? id)
        {
            return RedirectToAction("Index");
        }


        //POST
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {


            return RedirectToAction("Index");
        }
    }
}
