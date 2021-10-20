using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ASPNetCoreApp.WebAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; set; }

        public string Adress { get; set; }

        public BaseClient(HttpClient Client,string Adress)
        {
            this.Client = Client;
            this.Adress = Adress;
        }

        protected T Get<T>(string url)
        {
            return GetAsync<T>(url).Result; 

        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var response = await Client.GetAsync(url).ConfigureAwait(false);
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);

        }

        protected HttpResponseMessage Delete<T>(string url)
        {
            return DeleteAsync<HttpResponseMessage>(url).Result;

        }

        protected async Task<HttpResponseMessage> DeleteAsync<T>(string url)
        {
            var response = await Client.DeleteAsync(url).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }
        protected HttpResponseMessage Post<T>(string url,T item)
        {
            return PostAsync(url,item).Result;

        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string url,T item)
        {
            var response = await Client.PostAsJsonAsync(url,item).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }

        protected HttpResponseMessage Put<T>(string url, T item)
        {
            return PutAsync(url, item).Result;

        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await Client.PutAsJsonAsync(url, item).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }
    }
}
