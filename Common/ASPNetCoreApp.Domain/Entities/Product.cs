using ASPNetCoreApp.Domain.Entities.Base;
using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreApp.Domain.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Заказ
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public Section Section { get; set; }

        /// <summary>
        /// Идентификатор бренда
        /// </summary>
        public int BrandId { get; set; }


        /// <summary>
        /// Бренд
        /// </summary>
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        /// <summary>
        /// Ссылка на изображение товара
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
