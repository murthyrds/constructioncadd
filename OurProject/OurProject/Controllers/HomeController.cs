using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurProject.Entities;
using OurProject.Models;
using OurProject.Services;
using PagedList;
using PagedList.Core.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurProject.Controllers
{
    public class HomeController : Controller
    {
        private IUserData _userData;
        private UserManager<User> _userManager;
        private OurProjectDbContext _context;

        public HomeController(IUserData userData, UserManager<User> userManager, OurProjectDbContext context)
        {
            _userData = userData;
            _userManager = userManager;
            _context = context;
        }

        //Get the admin detail's.
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var model = new DetailsModel();
            model.users = _userData.GetAll();           
            return View(model);
        }

        //Get the detailer's only.
        public IActionResult Detailer()
        {
            var model = new DetailsModel();
            model.users = _userData.GetDetailer();
            return View(model);
        }

        //Get the Vendor's only.
        public IActionResult Vendors()
        {
            var model = new DetailsModel();
            model.users = _userData.GetVendor();
            return View(model);
        }

        //Get the Vendor's only.
        public IActionResult Client()
        {
            var model = new DetailsModel();
            model.users = _userData.GetClient();
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

       
        public async Task<IActionResult> Delete(string Id)
        {

            //get User Data from Userid
            var user = await _userManager.FindByIdAsync(Id);

            //List Logins associated with user
            var logins = user;

            //Gets list of Roles associated with current user
            var rolesForUser = await _userManager.GetRolesAsync(user);

            using (var transaction = _context.Database.BeginTransaction())
            {

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                //Delete User
                await _userManager.DeleteAsync(user);

                TempData["Message"] = "User Deleted Successfully. ";
                TempData["MessageValue"] = "1";
                //transaction.commit();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}