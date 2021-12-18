using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aptitude.Data;
using Aptitude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aptitude.Controllers
{
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> ListExam()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProFile()
        {
            var model = _context.CV.Where(a => a.Email.ToUpper().Equals(User.Identity.Name.ToUpper())).FirstOrDefault();
            return View(model);
        }
    }
}
