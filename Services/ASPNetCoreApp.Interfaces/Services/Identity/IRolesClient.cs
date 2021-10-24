using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ASPNetCoreApp.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<Role>
    {
        
    }
}
