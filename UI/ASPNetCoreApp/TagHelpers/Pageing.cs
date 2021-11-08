using ASPNetCoreApp.Domain.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreApp.TagHelpers
{
    public class Pageing : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public PageViewModel PageViewModel { get; set; }

        [ViewContext,HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public Pageing(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            var url_helper = urlHelperFactory.GetUrlHelper(ViewContext);

            for(int i = 1; i <= PageViewModel.TotalPages; i++)
            {
                ul.InnerHtml.AppendHtml(CreateElement(i, url_helper));
               
            }
            output.Content.AppendHtml(ul);
        }

        private IHtmlContent CreateElement(int PageNumber, IUrlHelper url_helper)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            a.InnerHtml.AppendHtml(PageNumber.ToString());

            if (PageNumber == PageViewModel.Page)
                li.AddCssClass("active");
            else
            {
                PageUrlValues["page"] = PageNumber;
                a.Attributes["href"] = url_helper.Action(PageAction, PageUrlValues);
            }

            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
