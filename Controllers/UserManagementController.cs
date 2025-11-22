using System.Web.Mvc;
using CrudApplication.Filters;
using CrudApplication.Models.Entities;
using CrudApplication.Models.ViewModels;
using CrudApplication.Services;
using UserManagementSystem.Filters;
using UserManagementSystem.Models.ViewModels;
using UserManagementSystem.Services;

namespace CrudApplication.Controllers
{
    [AdminAuthFilter]
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;

        public UserManagementController()
        {
            _userService = new UserService();
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        // GET: UserManagement/Details/5
        public ActionResult Details(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: UserManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateViewModel model)
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

            var result = _userService.CreateUser(user, model.Password);

            if (result != null)
            {
                TempData["Success"] = "User created successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Email already exists or creation failed.");
            return View(model);
        }

        // GET: UserManagement/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UserEditViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Gender = user.Gender,
                Skills = user.Skills,
                Education = user.Education,
                Address = user.Address
            };

            return View(model);
        }

        // POST: UserManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserId = model.UserId,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Gender = model.Gender,
                Skills = model.Skills,
                Education = model.Education,
                Address = model.Address
            };

            var result = _userService.UpdateUser(user, model.NewPassword);

            if (result)
            {
                TempData["Success"] = "User updated successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Email already exists or update failed.");
            return View(model);
        }

        // GET: UserManagement/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _userService.DeleteUser(id);

            if (result)
            {
                TempData["Success"] = "User deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Delete failed.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var service = _userService as UserService;
                if (service != null)
                {
                    service.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}