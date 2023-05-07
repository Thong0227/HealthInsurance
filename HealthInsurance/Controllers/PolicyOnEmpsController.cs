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
    public class PolicyOnEmpsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public PolicyOnEmpsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: PolicyOnEmps
        public async Task<IActionResult> Index()
        {
            var healthInsuranceContext = _context.PolicyOnEmp.Include(p => p.Employee).Include(p => p.Policy);
            return View(await healthInsuranceContext.ToListAsync());
        }

        // GET: PolicyOnEmps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PolicyOnEmp == null)
            {
                return NotFound();
            }

            var policyOnEmp = await _context.PolicyOnEmp
                .Include(p => p.Employee)
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyOnEmp == null)
            {
                return NotFound();
            }

            return View(policyOnEmp);
        }

        // GET: PolicyOnEmps/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Address");
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name");
            return View();
        }

        // POST: PolicyOnEmps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyId,EmployeeId,EmiSubmitted,StartDate,EndDate")] PolicyOnEmp policyOnEmp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyOnEmp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Address", policyOnEmp.EmployeeId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name", policyOnEmp.PolicyId);
            return View(policyOnEmp);
        }

        // GET: PolicyOnEmps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PolicyOnEmp == null)
            {
                return NotFound();
            }

            var policyOnEmp = await _context.PolicyOnEmp.FindAsync(id);
            if (policyOnEmp == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Address", policyOnEmp.EmployeeId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name", policyOnEmp.PolicyId);
            return View(policyOnEmp);
        }

        // POST: PolicyOnEmps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PolicyId,EmployeeId,EmiSubmitted,StartDate,EndDate")] PolicyOnEmp policyOnEmp)
        {
            if (id != policyOnEmp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyOnEmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyOnEmpExists(policyOnEmp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Address", policyOnEmp.EmployeeId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "Id", "Name", policyOnEmp.PolicyId);
            return View(policyOnEmp);
        }

        // GET: PolicyOnEmps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PolicyOnEmp == null)
            {
                return NotFound();
            }

            var policyOnEmp = await _context.PolicyOnEmp
                .Include(p => p.Employee)
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyOnEmp == null)
            {
                return NotFound();
            }

            return View(policyOnEmp);
        }

        // POST: PolicyOnEmps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PolicyOnEmp == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.PolicyOnEmp'  is null.");
            }
            var policyOnEmp = await _context.PolicyOnEmp.FindAsync(id);
            if (policyOnEmp != null)
            {
                _context.PolicyOnEmp.Remove(policyOnEmp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyOnEmpExists(int id)
        {
          return (_context.PolicyOnEmp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
