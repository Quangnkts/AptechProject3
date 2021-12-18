using Aptitude.Data;
using Aptitude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Aptitude.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //View HomePage Page
        public IActionResult Homepage()
        {
            return View();
        }

        //View About Us
        public IActionResult AboutUs()
        {
            return View();
        }

        //View Contact Us Page
        public IActionResult ContactUs()
        {
            return View();
        }

        //Display all Jobs
        public async Task<IActionResult> ListJobs()
        {
            return View(await _context.Department.ToListAsync());
        }

        //Display jobs details
        public async Task<IActionResult> JobsDetails(int id)
        {
            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        //View Form CV
        [HttpGet]
        public IActionResult FormCV(int id)
        {
            var department = _context.Department.Find(id);
            ViewBag.dep = department.Id;
            return View();
        }

        //Submit CV registration form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormCV
            ([Bind("FirstName,LastName,Email,University,Mark,PersonalDetail,Advantage,Disadvantage,Hobby,Language,Experience,Project,LastPostion,DateCV,DepartmentId")]
        CV cv, string depid, string cvdate, string mark, int id)
        {
            ViewBag.dep = depid;
            if (ModelState.IsValid)
            {
                cv.DateCV = DateTime.Parse(cvdate);
                cv.DepartmentId = int.Parse(depid);
                cv.Mark = int.Parse(mark);
                _context.Add(cv);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThanksFor");
            }
            return View(cv);
        }

        //View page ThanksFor
        public IActionResult ThanksFor()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
