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
                var tag = new TagBuilder("li");//find out why button is not black

                tag.InnerHtml = pageUrl(i);
                tag.AddCssClass("page");
                // если текущая страница, то выделяем ее, добавляя класс
                tag.AddCssClass(i == pageInfo.PageNumber ? "active" : "not-active");
                tag.MergeAttribute("value", i.ToString());
                ul.InnerHtml += tag.ToString();
            }
            result.Append(ul.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}