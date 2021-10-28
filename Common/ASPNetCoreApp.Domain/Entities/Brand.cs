using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using ASPNetCoreApp.Domain.Entities.Base;

namespace ASPNetCoreApp.Domain.Entities
{
    /// <summary>
    /// Бренд
    /// </summary>
    public class Brand : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Порядок
        /// </summary>
        public int Order { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
