using ASPNetCoreApp.Domain.ViewModels;
using ASPNetCoreApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASPNetCoreApp.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }

        public IViewComponentResult Invoke(string BrandId)
        {

            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands()
                        .OrderBy(x => x.Order)
                        .Select(x => new BrandViewModel
                            {
                                Id = x.Id,
                                Name = x.Name
                            });

    }
}
