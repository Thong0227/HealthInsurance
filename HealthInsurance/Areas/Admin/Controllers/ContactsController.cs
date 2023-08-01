using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Areas.Admin.Models;
using HealthInsurance.Data;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using HealthInsurance.Models;
using X.PagedList;

namespace HealthInsurance.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ContactsController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public ContactsController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Admin/Contacts
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6; // Số lượng phần tử trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, nếu không có thì mặc định là trang 1
            var contacts = await _context.Contact.ToListAsync();
            IPagedList<Contact> pageContact = contacts.ToPagedList(pageNumber, pageSize);
            return View(pageContact);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string keyword,string phone, int? page)
        {
            int pageSize = 6; // Số lượng phần tử trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, nếu không có thì mặc định là trang 1
            var contact = _context.Contact.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                contact = contact.Where(x => x.FullName.Contains(keyword) || x.Email.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                contact = contact.Where(x => x.Phone.Contains(phone));
            }
            var contacts = await contact.ToListAsync();
            IPagedList<Contact> pageContact = contacts.ToPagedList(pageNumber, pageSize);
            ViewBag.keyword = keyword;
            ViewBag.phone = phone;
            return View(pageContact);
        }


        // GET: Admin/Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
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
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone,Subject,Message")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Admin/Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Admin/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Contact'  is null.");
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
