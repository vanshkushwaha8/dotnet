using System.Web;
using System.Web.Mvc;

namespace CrudApplication.Filters
{
    public class UserAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = HttpContext.Current.Session["UserId"];

            if (userId == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}