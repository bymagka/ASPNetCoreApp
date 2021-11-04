using ASPNetCoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Section Section { get; set; }

        public Brand Brand { get; set; }

        public string Product { get; set; }
    }
}
