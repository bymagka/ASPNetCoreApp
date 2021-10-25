using ASPNetCoreApp.Domain.Entities.Base;
using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreApp.Domain.Entities
{
    /// <summary>
    /// Позиция заказа
    /// </summary>
    public class OrderItem : Entity 
    {
        /// <summary>
        /// Заказ
        /// </summary>
        public Order Order { get ; set; }


        /// <summary>
        /// Товар
        /// </summary>
        [Required]
        public Product Product { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Общая стоимость
        /// </summary>
        [NotMapped]
        public decimal TotalPrice => Price * Quantity;
    }
}
