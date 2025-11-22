using System.Web;
using System.Web.Mvc;

namespace CrudApplication.Filters
{
    public class AdminAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var adminId = HttpContext.Current.Session["AdminId"];

            if (adminId == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}