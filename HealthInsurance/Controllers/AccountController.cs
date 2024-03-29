﻿using HealthInsurance.Data;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Controllers
{
    public class AccountController : Controller
    {
        private readonly HealthInsuranceContext _context;
        public AccountController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Lấy thông tin người dùng đã đăng nhập từ cookie
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                if(_emp != null)
                {
                    return View(_emp);
                }
                // Trả về view để hiển thị thông tin người dùng
            }
            return RedirectToAction("Login", "Account");

        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            } else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(Employee emp)
        {
            Employee storedUser = _context.Employees.Where(m => m.UserName == emp.UserName).FirstOrDefault();
            if(storedUser != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(emp.Password, storedUser.Password);
                if (isPasswordValid)
                {
                    var _emp = _context.Employees.Where(m => m.UserName == storedUser.UserName && m.Password == storedUser.Password).FirstOrDefault();
                    if (_emp == null)
                    {
                        ViewBag.LoginStatus = 0;
                    }
                    else
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _emp.Email),
                    new Claim("FullName", _emp.FullName),
                    new Claim(ClaimTypes.Role, _emp.UserRole),
                };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {

                        };

                        HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        var isAdmin = _emp.UserRole == "Administrator";

                        if (isAdmin)
                        {
                            return RedirectToAction("HomeAdmin", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Account");
                        }
                    }
                }
                else
                {
                    ViewBag.LoginStatus = 0;
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
            [HttpPost]
        public ActionResult ChangePassword(string password, string newPassword)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, _emp.Password);
                if (isPasswordValid)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    _emp.Password = hashedPassword;
                    _context.Update(_emp);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(new { error = "Invalid password." });
                }
            }
            return View();
        }

        public ActionResult UpdateInfor([Bind("FullName,DateOfBirth,Email,Address")] Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;

                var _emp = _context.Employees.Where(m => m.Email == userName).FirstOrDefault();
                _emp.Address = employee.Address;
                _emp.Email = employee.Email;
                _emp.FullName = employee.FullName;
                _emp.DateOfBirth = employee.DateOfBirth;
                _context.Update(_emp);
                _context.SaveChanges();

            }
            return RedirectToAction("Index", "Account");
        }
    }
}
