﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApp.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string  Description { get; set; }

        public decimal TotalPrice { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
