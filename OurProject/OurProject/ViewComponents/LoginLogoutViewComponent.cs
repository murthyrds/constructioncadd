using Microsoft.AspNetCore.Mvc;

namespace OurProject.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
