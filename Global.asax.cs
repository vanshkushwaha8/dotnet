using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserManagementSystem
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set database initializer to null to prevent automatic migrations
            System.Data.Entity.Database.SetInitializer<Models.ApplicationDbContext>(null);
        }

        protected void Session_Start()
        {
            // Initialize session variables
            Session["UserId"] = null;
            Session["AdminId"] = null;
        }
    }
}