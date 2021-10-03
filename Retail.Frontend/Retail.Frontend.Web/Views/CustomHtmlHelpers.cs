using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace Retail.Frontend.Web.Views
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent RenderFrom(this IHtmlHelper htmlHelper, string url)
        {
            var response = new HttpClient().GetAsync(url).Result;
            return new HtmlString(response.Content.ReadAsStringAsync().Result);
        }
    }
}
