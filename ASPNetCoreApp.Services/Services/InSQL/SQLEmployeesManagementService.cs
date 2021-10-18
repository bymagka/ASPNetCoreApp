using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.DAL.Context;
using ASPNetCoreApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ASPNetCoreApp.Services.InSQL
{
    public class SQLEmployyesManagementService : IEmployeeService
    {
        private readonly ILogger<SQLEmployyesManagementService> _Logger;

      

        private readonly ASPNetCoreAPPDb db;

        public SQLEmployyesManagementService(ASPNetCoreAPPDb db,ILogger<SQLEmployyesManagementService> logger)
        {

            _Logger = logger;
            this.db = db;        
        }


        public int Add(Employee emp)
        {
            if (emp is null) throw new ArgumentNullException(nameof(emp));

            if (db.Employees.Contains(emp)) return emp.Id;

            

            using (db.Database.BeginTransaction())
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                db.Database.CommitTransaction();
            }

            return emp.Id;
        }

        public bool Delete(int id)
        {
            var emp = GetById(id);

            if (emp is null) return false;

            

            using (db.Database.BeginTransaction())
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                
                db.Database.CommitTransaction();
                _Logger.LogInformation("Сотрудник удален с БД");
            }

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public Employee GetById(int id)
        {
            return db.Employees.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Employee emp)
        {
            if (emp is null) throw new ArgumentNullException(nameof(emp));

            if (db.Employees.SingleOrDefault(x=>x.Id == emp.Id) is null) return;

            var db_employee = GetById(emp.Id);
              
            if (db_employee is null) return;
           
            using (db.Database.BeginTransaction())
            {
                db_employee.Name = emp.Name;
                db_employee.LastName = emp.LastName;
                db_employee.BirthdayDate = emp.BirthdayDate;
                db_employee.Age = emp.Age;

                db.SaveChanges();
                db.Database.CommitTransaction();
            }
        }
    }
}