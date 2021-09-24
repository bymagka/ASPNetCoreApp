using ASPNetCoreApp.Domain.Entities.Base.Interfaces;

namespace ASPNetCoreApp.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
