using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreApp.Domain.Entities
{
    [Index(nameof(LastName),IsUnique = true)]
    public class Employee : NamedEntity
    { 

        public string LastName { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
    }
}
