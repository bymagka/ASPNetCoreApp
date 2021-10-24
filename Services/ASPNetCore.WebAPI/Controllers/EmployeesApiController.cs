using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Services.Infostructure;

namespace ASPNetCoreApp.Controllers
{
    [ApiController]
    [Route(WebApiAdresses.Employees)]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeesApiController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var employyes = employeeService.GetAll();
            return Ok(employyes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var employye = employeeService.GetById(Id);

            if (employye == null)
                return NotFound();

            return Ok(employye);
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = employeeService.Delete(Id);

            return result ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            int id = employeeService.Add(emp);

            return CreatedAtAction(nameof(Add), new { id }, emp);
        }


        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            employeeService.Update(emp);

            return Ok();
        }
    }
}
