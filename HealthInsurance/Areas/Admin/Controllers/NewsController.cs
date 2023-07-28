using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Areas.Admin.Models;
using HealthInsurance.Data;
using HealthInsurance.Models;

namespace HealthInsurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public NewsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string keyword)
        {
            var news = _context.News.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                news = news.Where(n => n.Title.Contains(keyword));
            }
            ViewBag.keyword = keyword;
            return View(await news.ToListAsync());
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, IFormFile imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.Length > 0)
                {
                    // Lưu ảnh vào thư mục của ứng dụng
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/news", imageUpload.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageUpload.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn ảnh vào đối tượng Policy
                    news.Image = imageUpload.FileName; // Thêm dấu gạch chéo (/) sau "policy"
                }
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Content,Image")] News news, IFormFile imageUpload)
        {
            if (id != news.Id)
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
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/news", imageUpload.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await imageUpload.CopyToAsync(stream);
                        }

                        // Lưu đường dẫn ảnh vào đối tượng Policy
                        news.Image = imageUpload.FileName; // Thêm dấu gạch chéo (/) sau "policy"
                    }
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
