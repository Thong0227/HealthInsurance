using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using HealthInsurance.Data;

namespace HealthInsurance.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public AdminController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var totalEmployee = _context.PolicyOnEmp.Count();
            ViewBag.totalEmployee = totalEmployee;
            return View();
        }
    }
}
