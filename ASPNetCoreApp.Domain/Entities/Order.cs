using ASPNetCoreApp.Domain.Entities.Base;
using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using ASPNetCoreApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.Domain.Entities
{
    public class Order : Entity,IDocument
    {
        [Required]
        public User User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(500)]
        public string Adress { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public string Description { get; set ; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
