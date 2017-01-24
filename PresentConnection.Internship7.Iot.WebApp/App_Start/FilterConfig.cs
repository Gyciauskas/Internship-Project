using System.Web;
using System.Web.Mvc;

namespace PresentConnection.Internship7.Iot.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
