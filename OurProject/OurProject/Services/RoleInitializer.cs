using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OurProject.Services
{
    public static class RoleInitializer
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Client"))
            {
                var role = new IdentityRole("Client");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Detailer"))
            {
                var role = new IdentityRole("Detailer");
                await roleManager.CreateAsync(role);
            }
        }
    }
}
