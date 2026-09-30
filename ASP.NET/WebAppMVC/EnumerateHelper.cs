using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models;

namespace WebAppMVC
{
    public static class EnumerateHelper
    {
        public static HtmlString EnumHelp(this IHtmlHelper html, string[] items)
        {
            string resultHtml = "<ul>";
            foreach (var item in items)
            {
                resultHtml += $"<li>{item}</li>";
            }
            resultHtml += "</ul>";
            return new HtmlString(resultHtml);
        }
    }
}
