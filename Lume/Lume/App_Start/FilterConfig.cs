using System.Web;
using System.Web.Mvc;
using MySql.Data;

namespace Lume
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}