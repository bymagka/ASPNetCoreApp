using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNetCoreApp.WebAPI.Clients.Base;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;

namespace ASPNetCoreApp.WebAPI.Clients.Employees
{
    public class EmployyesClient : BaseClient, IEmployeeService
    {

        public EmployyesClient(HttpClient Client) : base(Client, "api/employyes")
        {
            
        }

        public int Add(Employee emp)
        {
            var response = Post(Adress, emp);
            var newEmp = response.Content.ReadFromJsonAsync<Employee>().Result;
            var newId = newEmp.Id;

            return newId;
        }

        public bool Delete(int id)
        {
            var result = Delete<Employee>($"{Adress}/{id}");
            return result.IsSuccessStatusCode;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = Get<IEnumerable<Employee>>(Adress);
            return employees;
        }

        public Employee GetById(int id)
        {
            var emp = Get<Employee>($"{Adress}/{id}");
            return emp;
        }

        public void Update(Employee emp)
        {
            Put(Adress, emp);
        }
    }
}
