using ASPNetCoreApp.Domain.Entities.Base;
using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreApp.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? SectionId { get; set; }

        public Section Section { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
    }
}
