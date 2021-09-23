using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ASPNetCoreApp.Data;

namespace ASPNetCoreApp.Services
{
    public class EmployeesManagementService : IEmployeeService
    {

        private readonly ILogger<EmployeesManagementService> _Logger;

        private int _maxCurrentId;

        public EmployeesManagementService(ILogger<EmployeesManagementService> logger)
        {
            _Logger = logger;

            _maxCurrentId = TestData.EmployeesList.Max(x => x.Id);
        }


        public int Add(Employee emp)
        {
            if (emp is null) throw new ArgumentNullException(nameof(emp));

            if (TestData.EmployeesList.Contains(emp)) return emp.Id;

            emp.Id = ++_maxCurrentId;
            TestData.EmployeesList.Add(emp);

            return emp.Id;
        }

        public bool Delete(int id)
        {
            var emp = GetById(id);

            if (emp is null) return false;

            TestData.EmployeesList.Remove(emp);

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return TestData.EmployeesList;
        }

        public Employee GetById(int id)
        {
            return TestData.EmployeesList.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Employee emp)
        {
            if (emp is null) throw new ArgumentNullException(nameof(emp));

            if (TestData.EmployeesList.Contains(emp)) return;

            var db_employee = GetById(emp.Id);

            if (db_employee is null) return;

            db_employee.FirstName = emp.FirstName;
            db_employee.LastName = emp.LastName;
            db_employee.BirthdayDate = emp.BirthdayDate;
            db_employee.Age = emp.Age;
        }
    }
}
