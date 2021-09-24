using ASPNetCoreApp.Domain.Entities.Base.Interfaces;

namespace ASPNetCoreApp.Domain.Entities.Base
{
    public abstract class OrderedEntity : Entity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
