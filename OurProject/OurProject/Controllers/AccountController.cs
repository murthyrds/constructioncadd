using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OurProject.Entities;
using OurProject.Models;

namespace OurProject.Controllers
{
    public class AccountController : Controller
    {        
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;           

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;           
        }

        //call the Register page.
        [HttpGet]
        public IActionResult Register()
        {          
            return View();
        }

        //Post the user entered data to db.
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber };
                //var phoneNo = new User { PhoneNumber = model.PhoneNumber };
                var createResult = await _userManager.CreateAsync(user, model.Password);
                
                if(createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");                    
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        //Logout the user.
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //call the Register page.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnurl = null )
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