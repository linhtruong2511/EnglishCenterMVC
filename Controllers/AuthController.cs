using System.Threading.Tasks;
using EnglishCenterMVC.Authorization;
using EnglishCenterMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace EnglishCenterMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await authService.Login(email, password);
                if (result.Roles.Contains(Roles.ADMIN.ToString()))
                {
                    return Redirect("/Admin");
                }
                else if (result.Roles.Contains(Roles.STUDENT.ToString()))
                {
                    return Redirect("/Student");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Thông tin ??ng nh?p không chính xác";
            }
            return Redirect("/");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await authService.Logout(User);
                return Redirect("/");
            }
            catch (Exception ex)
            {
            }
            return View("/");
        }
    }
}
