using HealthInsurance.Areas.Admin.Models;
using HealthInsurance.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    public class ContactController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ContactController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Phone,Subject,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return Json(new { success = true});
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
    }
}
