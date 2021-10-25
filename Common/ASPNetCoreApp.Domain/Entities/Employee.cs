using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApp.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreApp.Domain.Entities
{

    /// <summary>
    /// Сотрудник
    /// </summary>
    [Index(nameof(LastName),IsUnique = true)]
    public class Employee : NamedEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }


        /// <summary>
        /// День рождени
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }
    }
}
