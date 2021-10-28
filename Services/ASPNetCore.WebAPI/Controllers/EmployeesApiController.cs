using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.Services.Infostructure;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ASPNetCoreApp.Controllers
{

    /// <summary>
    /// Управление сотрудниками
    /// </summary>
    [ApiController]
    [Route(WebApiAdresses.Employees)]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeesApiController> logger;

        public EmployeesApiController(IEmployeeService employeeService,ILogger<EmployeesApiController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }


        /// <summary>
        /// Получение всех товаров
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var employyes = employeeService.GetAll();
            logger.LogInformation("Запрос на получение всех сотрудников");
            return Ok(employyes);
        }


        /// <summary>
        /// Сотрудник по идентификатору
        /// </summary>
        /// <param name="Id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int Id)
        {
            logger.LogInformation("Getting employee by id {0}",Id);
            var employye = employeeService.GetById(Id);

            if (employye == null)
                logger.LogError("Employee with {0} was not found",Id);
                return NotFound();

            logger.LogInformation("Employee with {0} was found. It is {0}", employye);

            return Ok(employye);
        }

        /// <summary>
        /// Удаление сотрудника по ид
        /// </summary>
        /// <param name="Id">Идентификатор сотрудника</param>
        /// <returns>Флаг осуществления операции</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = employeeService.Delete(Id);

            if (result)
                logger.LogInformation("Employee with id {0} was deleted.", Id);
            else
                logger.LogError("Employee with id {0} wasn't deleted");

            return result ? Ok(result) : NotFound(result);
        }
        
        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="emp">описание сотрудника</param>
        /// <returns>Идентификатор созданного сотрудника</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            int id = employeeService.Add(emp);

            logger.LogInformation("Employee {0} was added", emp);

            return CreatedAtAction(nameof(Add), new { id }, emp);
        }

        /// <summary>
        /// Обновление сотрудника
        /// </summary>
        /// <param name="emp">описание сотрудника</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            employeeService.Update(emp);

            logger.LogInformation("Employee {0} was updated");

            return Ok();
        }
    }
}
