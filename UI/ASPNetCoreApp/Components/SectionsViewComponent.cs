using Microsoft.AspNetCore.Mvc;
using ASPNetCoreApp.Interfaces.Services;
using System.Collections.Generic;
using ASPNetCoreApp.Domain.ViewModels;
using System.Linq;
using ASPNetCoreApp.ViewModels;

namespace ASPNetCoreApp.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }


        public IViewComponentResult Invoke(string SectionId)
        {
            var section_id = int.TryParse(SectionId, out int id) ? id : (int?)null;

            var sections = GetSections(section_id, out var parent_section_id);


            
           return  View(new SelectableSectionsViewModel { 
                Sections = sections,
                SectionId = section_id,
                ParentSectionId = parent_section_id
           });
        }

        public IEnumerable<SectionViewModel> GetSections(int? SectionId,out int? ParentSectionId)
        {
            ParentSectionId = null;

            var SectionsCollection = _ProductData.GetSections();

            var parent_sections = SectionsCollection.Where(sect => sect.ParentId is null)
                            .Select(x => new SectionViewModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Order = x.Order
                            })
                            .OrderBy(x => x.Order)
                            .ToList();

            foreach(var parent_section in parent_sections)
            {
                var child_sections = SectionsCollection.Where(x => x.ParentId.Equals(parent_section.Id))
                                                       .OrderBy(x=>x.Order);

                foreach(var child_section in child_sections)
                {
                    parent_section.ChildSections.Add(new SectionViewModel
                    { 
                        ParentSection = parent_section,
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order
                    });
                }
            }

            return parent_sections;
        }
    }
}
