using ASPNetCoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        bool Delete(int id);

        int Add(Employee emp);

        void Update(Employee emp);



    }
}
