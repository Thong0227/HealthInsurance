﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
using X.PagedList;

namespace HealthInsurance.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class EmployeesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public EmployeesController(HealthInsuranceContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6; // Số lượng phần tử trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, nếu không có thì mặc định là trang 1

            var employees = await _context.Employees.ToListAsync();

            IPagedList<Employee> pagedEmployees = employees.ToPagedList(pageNumber, pageSize);

            return View(pagedEmployees);
        }
       
        [HttpPost]
        public async Task<IActionResult> Index(string keyword, string role, int? page)
        {
            int pageSize = 6; // Số lượng phần tử trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại, nếu không có thì mặc định là trang 1
            var employee = _context.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(keyword)){
                employee = employee.Where(x => x.UserName.Contains(keyword) || x.FullName.Contains(keyword) || x.Email.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(role))
            {
                employee = employee.Where(r => r.UserRole.Contains(role));
            }
            var employees = await employee.ToListAsync();

            IPagedList<Employee> pagedEmployees = employees.ToPagedList(pageNumber, pageSize);
            ViewBag.keyword = keyword;
            ViewBag.role = role;
            return View(pagedEmployees);
        }
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,FullName,DateOfBirth,Email,Address,JoinDate,PolicyStatus,UserRole")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,FullName,DateOfBirth,Email,Address,JoinDate,PolicyStatus,UserRole")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        

    }
}
