using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Threading;
using System.Net;

namespace ASPNetCoreApp.WebAPI.Clients
{
    public abstract class BaseClient : IDisposable
    {
        protected HttpClient Client { get; set; }

        public string Adress { get; set; }

        public BaseClient(HttpClient Client,string Adress)
        {
            this.Client = Client;
            this.Adress = Adress;
        }

        protected T Get<T>(string url, CancellationToken token = default)
        {
            return GetAsync<T>(url,token).Result; 

        }

        protected async Task<T> GetAsync<T>(string url, CancellationToken token = default)
        {
            var response = await Client.GetAsync(url,token).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NoContent) return default;

            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);

        }

        protected HttpResponseMessage Delete<T>(string url, CancellationToken token = default)
        {
            return DeleteAsync<HttpResponseMessage>(url,token).Result;

        }

        protected async Task<HttpResponseMessage> DeleteAsync<T>(string url, CancellationToken token = default)
        {
            var response = await Client.DeleteAsync(url,token).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }
        protected HttpResponseMessage Post<T>(string url,T item, CancellationToken token = default)
        {
            return PostAsync(url,item,token).Result;

        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string url,T item,CancellationToken token = default)
        {
            var response = await Client.PostAsJsonAsync(url,item,token).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }

        protected HttpResponseMessage Put<T>(string url, T item, CancellationToken token = default)
        {
            return PutAsync(url, item,token).Result;

        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken token = default)
        {
            var response = await Client.PutAsJsonAsync(url, item,token).ConfigureAwait(false);
            return response
                .EnsureSuccessStatusCode();

        }

        public void Dispose()
        {
            
        }
    }
}
