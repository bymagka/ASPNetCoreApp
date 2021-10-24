using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ASPNetCoreApp.Services.Infostructure;

namespace ASPNetCoreApp.WebAPI.Clients.Identity
{
    public class RolesClient : BaseClient
    {
        public RolesClient(HttpClient client) : base(client,WebApiAdresses.Identity.Roles)
        {

        }
    }
}
