using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OurProject.Entities;
using OurProject.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace OurProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly OurProjectDbContext _Context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, OurProjectDbContext Context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _Context = Context;
        }

        //call the Register page.
        [HttpGet]
        public IActionResult Register()
        {
            //List<SelectListItem> list = new List<SelectListItem>();
            //foreach (var role in _roleManager.Roles)
            //    list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            //ViewBag.Roles = list;
            ViewBag.Name = new SelectList(_Context.Roles.Where(u => !u.Name.Contains("Admin"))
                                            .ToList(), "Name", "Name");
            return View();
        }

        //Post the user entered data to db.
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var dip = new User { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber };
                //var phoneNo = new User { PhoneNumber = model.PhoneNumber };
                var createResult = await _userManager.CreateAsync(dip, model.Password);

                if (createResult.Succeeded)
                {
                    //createResult = await _userManager.AddToRoleAsync(user.Id, model.UserRoles);

                    //if(!await _roleManager.RoleExistsAsync("Admin"))
                    //{
                    //    var users = new IdentityRole("Admin");
                    //    var res = await _roleManager.CreateAsync(users);
                    //    if (res.Succeeded)
                    //    {
                    //        await _userManager.AddToRoleAsync(user, "Admin");
                    //        await _signInManager.SignInAsync(user, false);
                    //        return RedirectToAction("Index", "Home");
                    //    }                        
                    //}
                    await _signInManager.SignInAsync(dip, false);
                    await this._userManager.AddToRoleAsync(dip.Id, model.UserRoles);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Name = new SelectList(_Context.Roles.Where(u => !u.Name.Contains("Admin"))
                                          .ToList(), "Name", "Name");
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        //Logout the user.
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //call the Register page.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnurl = null)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Could not login!");
            return View(model);
        }
    }    
}