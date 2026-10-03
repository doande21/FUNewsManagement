using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Services;

namespace DoanDEMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISystemAccountService _accountService;

        // TIÊM (INJECT) SERVICE VÀO
        public AccountController(IConfiguration config, ISystemAccountService accountService)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "NewsArticle");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // 1. Kiểm tra Admin (đọc từ appsettings.json)
            string adminEmail = _config["AdminAccount:Email"];
            string adminPwd = _config["AdminAccount:Password"];

            if (email == adminEmail && password == adminPwd)
            {
                await CreateAuthCookie("Admin", "Admin", "0"); // Admin không có ID trong DB, truyền 0
                return RedirectToAction("Index", "SystemAccount");
            }

            // 2. Kiểm tra DB (Staff hoặc Lecturer)
            var account = _accountService.GetAccountByEmail(email);
            if (account != null && account.AccountPassword == password)
            {
                string roleName = account.AccountRole == 1 ? "Staff" : "Lecturer";
                // Truyền AccountID vào Cookie để dùng ở các controller khác
                await CreateAuthCookie(account.AccountName, roleName, account.AccountID.ToString());

                return RedirectToAction("Index", "NewsArticle");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        private async Task CreateAuthCookie(string name, string role, string accountId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name ?? "Unknown"),
                new Claim(ClaimTypes.Role, role),
                new Claim("AccountId", accountId) // Lưu ID thật vào claims
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}