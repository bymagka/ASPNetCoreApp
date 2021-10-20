using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
    }
}
