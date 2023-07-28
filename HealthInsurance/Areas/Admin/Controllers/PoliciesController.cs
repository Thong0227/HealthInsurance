using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthInsurance.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PoliciesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public PoliciesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Policies
        public async Task<IActionResult> Index()
        {
            var hospitalList = _context.Hospitals.ToList();
            hospitalList.Add(new Hospital() { Id = 0, Name = "Select Hospital" });
            ViewBag.HospitalId = new SelectList(hospitalList.OrderBy(x => x.Id), "Id", "Name");
            var healthInsuranceContext = _context.Policies.Include(p => p.Hospital);
            return View(await healthInsuranceContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string keyword, int HospitalId)
        {
            var hospitalList = _context.Hospitals.ToList();
            hospitalList.Add(new Hospital() { Id = 0, Name = "Select Hospital" });
            ViewBag.HospitalId = new SelectList(hospitalList.OrderBy(x => x.Id), "Id", "Name");

            var policies = _context.Policies.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                policies = policies.Where(x => x.Name.Contains(keyword));
            }
            if (HospitalId > 0)
            {
                policies = policies.Where(x => x.HospitalId == HospitalId);
            }
            policies = policies.Include(p => p.Hospital);
            ViewBag.keyword = keyword;
            return View(await policies.ToListAsync());
        }

        // GET: Policies/Details/5
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

        // GET: Policies/Create
        public IActionResult Create()
        {
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name");
            return View();
        }

        // POST: Policies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Policy policy, IFormFile imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.Length > 0)
                {
                    // Lưu ảnh vào thư mục của ứng dụng
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/policy", imageUpload.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageUpload.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn ảnh vào đối tượng Policy
                    policy.Image = imageUpload.FileName; // Thêm dấu gạch chéo (/) sau "policy"
                }

                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name", policy.HospitalId);
            return View(policy);
        }

        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name", policy.HospitalId);
            return View(policy);
        }

        // POST: Policies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Policy policy, IFormFile imageUpload)
        {
            if (id != policy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageUpload != null && imageUpload.Length > 0)
                    {
                        // Lưu ảnh vào thư mục của ứng dụng
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/policy", imageUpload.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await imageUpload.CopyToAsync(stream);
                        }

                        // Lưu đường dẫn ảnh vào đối tượng Policy
                        policy.Image = imageUpload.FileName; // Thêm dấu gạch chéo (/) sau "policy"
                    }
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.Id))
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
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Name", policy.HospitalId);
            return View(policy);
        }

        // GET: Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Policies == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Policies'  is null.");
            }
            var policy = await _context.Policies.FindAsync(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyExists(int id)
        {
          return (_context.Policies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
