using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models;

namespace WebAppMVC
{
    public static class EnumerateHelper
    {
        public static HtmlString EnumHelp(this IHtmlHelper html, Human[] items)
        {
            string resultHtml = "<ul>";
            foreach (var item in items)
            {
                resultHtml += $"<li>{item.ToString()}</li>";
            }
            resultHtml += "</ul>";
            return new HtmlString(resultHtml);
        }
    }
}
