using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurProject.Entities;
using OurProject.Models;
using OurProject.Services;
using PagedList;
using PagedList.Core.Mvc;

namespace OurProject.Controllers
{
    public class HomeController : Controller
    {
        private IUserData _userData;
        private UserManager<User> _userManager;

        public HomeController(IUserData userData, UserManager<User> userManager)
        {
            _userData = userData;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = new DetailsModel();
            model.users = _userData.GetAll();

            return View(model);
        }
        public IActionResult Details(string id)
        {
            var model = _userData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = _userData.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, RegisterUserModel register)
        {
            var model = _userData.Get(id);
           // var usrid = model;
            if (ModelState.IsValid)
            {
                //var newPassword = register.Password;
                model.UserName = register.Username;
                //model.PasswordHash = userEditModel.Password;
                model.Email = register.Email;
                model.PhoneNumber = register.PhoneNumber;

                //await _userManager.RemovePasswordAsync(usrid);
                //await _userManager.AddPasswordAsync(usrid, newPassword);

                _userData.Commit();


                return RedirectToAction("Details");
            }
            return View(model);
        }
    }
}