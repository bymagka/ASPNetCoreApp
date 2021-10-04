using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.Identity
{
    public class Role : IdentityRole      
    {
        public const string  Administrators = "Administrator";
        public const string Users = "Users";
    }
}
