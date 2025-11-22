using System.Web.Mvc;
using UserManagementSystem.Filters;

namespace UserManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Dashboard (User Dashboard)
        [UserAuthFilter]
        public ActionResult Dashboard()
        {
            ViewBag.UserName = Session["UserName"];
            return View();
        }
    }
}