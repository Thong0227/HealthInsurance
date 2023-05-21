using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsurance.Controllers
{
    public class LoginController : Controller
    {
        private readonly HealthInsuranceContext _context;
        public LoginController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Employee emp)
        {
            var _emp = _context.Employees.Where(m=>m.UserName== emp.UserName && m.Password== emp.Password).FirstOrDefault();
            if (_emp != null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
