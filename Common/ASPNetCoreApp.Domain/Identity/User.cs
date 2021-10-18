using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "admin";
        public const string DefaultAdminPass = "admin";
    }
}
