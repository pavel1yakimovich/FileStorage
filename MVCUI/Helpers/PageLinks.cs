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
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var li = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее, добавляя класс
                tag.AddCssClass(i == pageInfo.PageNumber ? "w3-black" : "w3-hover-black");
                li.InnerHtml = tag.ToString();
                ul.InnerHtml += li.ToString();
            }
            result.Append(ul.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}