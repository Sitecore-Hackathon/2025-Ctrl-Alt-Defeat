
using System.Web.Mvc;
using System.Web.Routing;

namespace ReadLogsAndSolve
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "AnalyizLogs",
                 url: "api/{controller}/{action}",
                 defaults: new { controller = "{controller}", action = "{action}" }
             );
        }
    }
}