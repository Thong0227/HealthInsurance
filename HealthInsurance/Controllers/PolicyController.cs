using HealthInsurance.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    public class PolicyController : Controller
    {
        private readonly HealthInsuranceContext _context;
        public PolicyController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var policies = _context.Policies.Include(p => p.Hospital);
            return View(await policies.ToListAsync());
        }
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }
    }
}
