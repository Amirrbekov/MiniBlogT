using BL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceApp.Areas.Admin.Controllers;

[Area("Admin")]
public class LoginController : Controller
{
    UserManager userManager = new UserManager();
    public IActionResult Index()
    {
        TempData["ReturnUrl"] = HttpContext.Request.Query["ReturnUrl"];
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string email, string password, string ReturnUrl)
    {
        try
        {
            var account = userManager.Get(x => x.Email == email && x.Password == password && x.IsActive == true);

            if (account == null)
            {
                ModelState.AddModelError("", "User not found!");
                TempData["Message"] = "User Not Found!";
            }
            else
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Default");

                if (string.IsNullOrWhiteSpace(ReturnUrl)) return RedirectToAction("Index", "Default");
                return RedirectToAction(ReturnUrl);
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Something went wrong! User not found!");
            TempData["Message"] = "Somethin Went Wrong! User Not Found!";
            throw;
        }

        return View();
    }

    [Route("Admin/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Login");
    }
}
