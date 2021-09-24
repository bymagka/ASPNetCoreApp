using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using ASPNetCoreApp.Domain.Entities.Base;

namespace ASPNetCoreApp.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
