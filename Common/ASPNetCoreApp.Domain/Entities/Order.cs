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
{    /// <summary>
     /// Заказ
     /// </summary>
    public class Order : Entity,IDocument
    {

        /// <summary>
        /// Пользователь
        /// </summary>
        [Required]
        public User User { get; set; }


        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Phone { get; set; }


        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Adress { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set ; }


        /// <summary>
        /// Товары заказа
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public override string ToString()
        {
            return $"{Date} {Description} made by {User.UserName}. Phone is {Phone}";
        }
    }
}
