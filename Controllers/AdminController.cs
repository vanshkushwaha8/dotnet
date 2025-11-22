using System.Web.Mvc;
using CrudApplication.Models.ViewModels;
using CrudApplication.Services;
using UserManagementSystem.Models.ViewModels;
using UserManagementSystem.Services;

namespace CrudApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthService _authService;

        public AdminController()
        {
            _authService = new AuthService();
        }

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = _authService.ValidateAdmin(model.EmailOrPhone, model.Password);

            if (admin != null)
            {
                Session["AdminId"] = admin.AdminId;
                Session["AdminEmail"] = admin.Email;

                return RedirectToAction("Index", "UserManagement");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // GET: Admin/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var service = _authService as AuthService;
                if (service != null)
                {
                    service.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}