using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthInsurance.Data;

namespace HealthInsurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeAdminController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public HomeAdminController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var totalEmployee = _context.PolicyOnEmp.Count();
            var request = _context.PolicyOnEmp.Where(x => x.PolicyStatus == false).Count();
            var earning = _context.PolicyOnEmp.Where(x => x.PolicyStatus == true).Sum(x => x.EmiSubmitted);
            var contact = _context.Contact.Count();
            ViewBag.contact = contact;
            ViewBag.earning = earning;
            ViewBag.totalEmployee = totalEmployee;
            ViewBag.request = request;
            return View();
        }
    }
}
