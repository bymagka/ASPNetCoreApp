using ASPNetCoreApp.Interfaces.TestApi;
using ASPNetCoreApp.WebAPI.Clients.Base;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace ASPNetCoreApp.WebAPI.Clients.Values
{
    public class ValuesClient : BaseClient,IValuesService
    {
        public ValuesClient(HttpClient Client) : base(Client,"api/values")
        {
            
        }
        public void Add(string Value)
        {
            var response = Client.PostAsJsonAsync(Adress, Value).Result;

            response.EnsureSuccessStatusCode();
        }

        public int Count()
        {
            var response = Client.GetAsync($"{Adress}/count").Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<int>().Result;

            return -1;
        }

        public bool Delete(int id)
        {
            var response = Client.DeleteAsync($"{Adress}/{id}").Result;

            return response.IsSuccessStatusCode;
        }

        public void Edit(int id, string Value)
        {
            var response = Client.PutAsJsonAsync($"{Adress}/{id}", Value).Result;

            response.EnsureSuccessStatusCode();
        }

        public IEnumerable<string> GetAll()
        {
            var response = Client.GetAsync(Adress).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string GetById(int id)
        {
            var response = Client.GetAsync($"{Adress}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<string>().Result;

            return null;
        }
    }
}
