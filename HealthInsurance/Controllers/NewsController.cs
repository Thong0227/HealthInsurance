using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Areas.Admin.Models;
using HealthInsurance.Data;

namespace HealthInsurance.Controllers
{
    public class NewsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public NewsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            return _context.News != null ?
                        View(await _context.News.ToListAsync()) :
                        Problem("Entity set 'HealthInsuranceContext.News'  is null.");
        }

        // GET: News/Details/5
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
    }
}
