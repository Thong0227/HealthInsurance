using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
namespace HealthInsurance.Controllers
{
    public class Registpolicy : Controller
    {
        private readonly HealthInsuranceContext _context;

        public Registpolicy(HealthInsuranceContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(Employee id)
        {
            //ViewData["EmployeeId"] = _context.Employees.FindAsync(id);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name");
            return View();
        }
    }
}
