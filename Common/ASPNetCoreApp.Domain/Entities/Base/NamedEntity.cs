using ASPNetCoreApp.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreApp.Domain.Entities.Base
{
    [Index(nameof(Name),IsUnique = true)]
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
