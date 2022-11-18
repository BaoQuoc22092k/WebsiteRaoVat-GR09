using System.Web;
using System.Web.Mvc;

namespace TTN_WebsiteRaoVat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
