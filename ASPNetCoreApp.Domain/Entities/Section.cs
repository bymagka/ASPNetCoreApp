using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using ASPNetCoreApp.Domain.Entities.Base;
using System.Collections.Generic;

namespace ASPNetCoreApp.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
