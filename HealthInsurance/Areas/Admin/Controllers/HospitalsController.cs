using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
namespace HealthInsurance.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HospitalsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public HospitalsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Hospitals
        public async Task<IActionResult> Index()
        {
              return _context.Hospitals != null ? 
                          View(await _context.Hospitals.ToListAsync()) :
                          Problem("Entity set 'HealthInsuranceContext.Hospitals'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string keyword,string location,string phone)
        {
            var hospital = _context.Hospitals.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                hospital = hospital.Where(x => x.Name.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(location))
            {
                hospital = hospital.Where(x => x.Location.Contains(location));
            }
            int phoneNumber;
            if (!string.IsNullOrEmpty(phone))
            {
                hospital = hospital.Where(x => x.Phone.Contains(phone));
            }
            ViewBag.keyword = keyword;
            ViewBag.location = location;
            ViewBag.phone = phone;
            return View(await hospital.ToListAsync());
        }
        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospitals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Location")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Location")] Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.Id))
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
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hospitals == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hospitals == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Hospitals'  is null.");
            }
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
          return (_context.Hospitals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
