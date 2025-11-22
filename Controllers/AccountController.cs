using System.Web.Mvc;
using CrudApplication.Models.Entities;
using CrudApplication.Models.ViewModels;
using CrudApplication.Services;
using UserManagementSystem.Models.ViewModels;
using UserManagementSystem.Services;

namespace CrudApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController()
        {
            _authService = new AuthService();
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Gender = model.Gender,
                Skills = model.Skills,
                Education = model.Education,
                Address = model.Address
            };

            var result = _authService.RegisterUser(user, model.Password);

            if (result != null)
            {
                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Email already exists or registration failed.");
            return View(model);
        }

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _authService.ValidateUser(model.EmailOrPhone, model.Password);

            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.Name;
                Session["UserEmail"] = user.Email;

                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Invalid email/phone or password.");
            return View(model);
        }

        // GET: Account/Logout
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