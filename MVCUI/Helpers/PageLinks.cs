using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCUI.ViewModels;

namespace MVCUI.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("w3-pagination");
            int i = 1;
            if (pageInfo.TotalPages > 3 && pageInfo.PageNumber > 2)
            {
                CreateTag(i, pageInfo, pageUrl, ul);
                i = pageInfo.PageNumber - 1;
            }
            int k = Math.Min(i+3, pageInfo.TotalPages);

            for (; i < k; i++)
            {
                CreateTag(i, pageInfo, pageUrl, ul);
            }

            if ( i <= pageInfo.TotalPages )
            {
                CreateTag(pageInfo.TotalPages, pageInfo, pageUrl, ul);
            }
            result.Append(ul.ToString());
            return MvcHtmlString.Create(result.ToString());
        }

        private static void CreateTag(int i, PageInfo pageInfo, Func<int, string> pageUrl, TagBuilder ul)
        {
            var tag = new TagBuilder("li");

            tag.InnerHtml = pageUrl(i);

            tag.AddCssClass("page");
            tag.AddCssClass(i == pageInfo.PageNumber ? "active" : "not-active");
            tag.MergeAttribute("value", i.ToString());

            ul.InnerHtml += tag.ToString();
        }
    }
}