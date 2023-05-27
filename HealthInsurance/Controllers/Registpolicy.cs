using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using System.Diagnostics;

namespace HealthInsurance.Controllers
{
    public class Registpolicy : Controller
    {
        private readonly HealthInsuranceContext _context;

        public Registpolicy(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                if (_emp != null)
                {
                    ViewData["EmployeeId"] = _emp.Id;
                }

                ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name");
                return View();
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                if (_emp != null)
                {
                    ViewData["EmployeeId"] = _emp.Id;
                }
            }
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name");
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyId,EmployeeId,EmiSubmitted,StartDate,EndDate,PolicyStatus")] PolicyOnEmp policyOnEmp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyOnEmp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Registpolicy");
        }
    }
}
