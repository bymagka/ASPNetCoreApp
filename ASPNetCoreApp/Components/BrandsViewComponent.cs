﻿using ASPNetCoreApp.Services.Interfaces;
using ASPNetCoreApp.ViewModels;
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

        public IViewComponentResult Invoke()
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
