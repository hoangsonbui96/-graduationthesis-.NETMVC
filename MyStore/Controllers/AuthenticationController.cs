using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly MyStoreContext _context;

        public AuthenticationController(MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            var validUser = await _context.Account.FirstOrDefaultAsync(i => i.Username == model.Username 
            && i.Password == model.Password);

            if (validUser == null)
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.Username));
                identity.AddClaim(new Claim(CustomClaim.Fullname, validUser.Fullname));
                identity.AddClaim(new Claim(ClaimTypes.MobilePhone, validUser.PhoneNumber));
                identity.AddClaim(new Claim(ClaimTypes.Email, validUser.EmailAdress));
                identity.AddClaim(new Claim("CustomerPhone", validUser.PhoneNumber));

                var functions = await _context.RolePermision.Where(i => i.RoleCode == validUser.RoleCode).Select(i => i.FunctionRole).ToListAsync();
                foreach (var function in functions)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, function));
                }

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberLogin,
                    ExpiresUtc = DateTime.UtcNow.AddYears(20)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost, ActionName("Logout")]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
