using ASPNetCoreApp.Domain.Entities.Base;
using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreApp.Domain.Entities
{
    public class OrderItem : Entity 
    {
        public Order Order { get ; set; }

        [Required]
        public Product Product { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalPrice => Price * Quantity;
    }
}
