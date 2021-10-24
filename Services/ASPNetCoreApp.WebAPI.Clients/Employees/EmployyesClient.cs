using System.Collections.Generic;
using ASPNetCoreApp.Interfaces.Services;
using ASPNetCoreApp.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;
using ASPNetCoreApp.Services.Infostructure;

namespace ASPNetCoreApp.WebAPI.Clients
{
    public class EmployyesClient : BaseClient, IEmployeeService
    {

        public EmployyesClient(HttpClient Client) : base(Client, WebApiAdresses.Employees)
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
