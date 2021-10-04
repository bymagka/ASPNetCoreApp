using ASPNetCoreApp.Domain;
using ASPNetCoreApp.Domain.Entities;
using ASPNetCoreApp.DAL.Context;
using ASPNetCoreApp.Services.Interfaces;
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

            db.Employees.Add(emp);

            using (db.Database.BeginTransaction())
            {
                db.SaveChanges();
                db.Database.CommitTransaction();
            }

            return emp.Id;
        }

        public bool Delete(int id)
        {
            var emp = GetById(id);

            if (emp is null) return false;

            db.Employees.Remove(emp);

            using (db.Database.BeginTransaction())
            {
                db.SaveChanges();
                _Logger.LogInformation("Сотрудник удален с БД");
                db.Database.CommitTransaction();
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
            db.Employees.Update(db_employee);

            db_employee.Name = emp.Name;
            db_employee.LastName = emp.LastName;
            db_employee.BirthdayDate = emp.BirthdayDate;
            db_employee.Age = emp.Age;

            using (db.Database.BeginTransaction())
            {

                db.SaveChanges();
                db.Database.CommitTransaction();
            }
        }
    }
}