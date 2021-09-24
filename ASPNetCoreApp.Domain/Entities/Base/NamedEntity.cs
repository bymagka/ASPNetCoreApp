using ASPNetCoreApp.Domain.Entities.Base.Interfaces;

namespace ASPNetCoreApp.Domain.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
