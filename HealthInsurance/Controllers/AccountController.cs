using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthInsurance.Controllers
{
    public class AccountController : Controller
    {
        private readonly HealthInsuranceContext _context;
        public AccountController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Lấy thông tin người dùng đã đăng nhập từ cookie
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                if(_emp != null)
                {
                    return View(_emp);
                }
                // Trả về view để hiển thị thông tin người dùng
            }
            return RedirectToAction("Login", "Account");

        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            } else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(Employee emp)
        {
            var _emp = _context.Employees.Where(m => m.UserName == emp.UserName && m.Password == emp.Password).FirstOrDefault();
            if (_emp == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _emp.Email),
                    new Claim("FullName", _emp.FullName),
                    new Claim(ClaimTypes.Role, _emp.UserRole),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                var isAdmin = _emp.UserRole == "Administrator";

                if (isAdmin)
                {
                    return RedirectToAction("Employees", "Admin");
                } else
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
