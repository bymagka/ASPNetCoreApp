using ASPNetCoreApp.Domain.Entities;
using System.Collections.Generic;

namespace ASPNetCoreApp.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Brand> GetBrands();

        IEnumerable<Section> GetSections();
    }
}
