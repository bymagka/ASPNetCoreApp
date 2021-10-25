using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using ASPNetCoreApp.Domain.Entities.Base;
using System.Collections.Generic;

namespace ASPNetCoreApp.Domain.Entities
{

    /// <summary>
    /// Категория
    /// </summary>
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительская категория
        /// </summary>
        public Section Parent { get; set; }


        /// <summary>
        /// Список товаров категории
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
