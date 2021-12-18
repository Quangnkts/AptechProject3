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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Identity;

namespace Aptitude.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ExamController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("general", "1");
            //HttpContext.Session.SetString("math", "1");
            //HttpContext.Session.SetString("computer", "1");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("exam")))
                HttpContext.Session.SetString("exam", "1");
            return View();
        }
        [HttpGet]
        public IActionResult GeneralExam()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("exam")))
            {
                return View("Homepage", "Home");
            }
            if (!HttpContext.Session.GetString("exam").Equals("1"))
            {
                return View("Index");
            }
            var listquestion = _context.Question.Where(q => q.Category == 1).Take(5);
            ViewBag.Result = "open";
            ViewBag.checking = 1;

            //HttpContext.Session.SetString("general","2");
            //HttpContext.Session.Remove("exam");
            HttpContext.Session.SetString("exam", "2");
            return View(listquestion);
        }
        [HttpPost]
        public IActionResult GeneralExam(IFormCollection form, string submit)
        {
            string aswn1 = form["answer1"];
            string aswn2 = form["answer2"];
            string aswn3 = form["answer3"];
            string aswn4 = form["answer4"];
            string aswn5 = form["answer5"];
            string FinalAnswer = aswn1 + "//" + aswn2 + "//" + aswn3 + "//" + aswn4 + "//" + aswn5;
            string[] array1 = aswn1.Split('+');
            string[] array2 = aswn2.Split('+');
            string[] array3 = aswn3.Split('+');
            string[] array4 = aswn4.Split('+');
            string[] array5 = aswn5.Split('+');

            var listquestion = _context.Question.ToList();

            //check answer
            int Point = 0;
            if (!String.IsNullOrEmpty(aswn1))
            {
                var question1 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array1[1])));
                if (question1.FinalAnswer.Equals(array1[0]))
                {
                    Point += 10;
                }
            }

            if (!String.IsNullOrEmpty(aswn2))
            {
                var question2 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array2[1])));
                if (question2.FinalAnswer.Equals(array2[0]))
                {
                    Point += 10;
                }
            }


            if (!String.IsNullOrEmpty(aswn3))
            {
                var question3 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array3[1])));
                if (question3.FinalAnswer.Equals(array3[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn4))
            {
                var question4 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array4[1])));
                if (question4.FinalAnswer.Equals(array4[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn5))
            {
                var question5 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array5[1])));
                if (question5.FinalAnswer.Equals(array5[0]))
                {
                    Point += 40;
                }
            }

            //save record
            var general = new Exam();
            general.Mark = Point;
            if (Point <= 50)
                general.Result = "Failed";
            else
                general.Result = "Passed";
            general.Date = DateTime.Now;
            general.Answer = FinalAnswer;

            _context.Exam.Add(general);
            _context.SaveChanges();
            //end save record
            ViewBag.Point = Point;
            ViewBag.Result = "done";
            ViewBag.checking = 2;
            HttpContext.Session.SetString("GeneralPoints", Point.ToString());
            return View(listquestion);
        }

        [HttpGet]
        public IActionResult MathExam()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("exam")))
            {
                return View("Homepage", "Home");
            }
            if (!HttpContext.Session.GetString("exam").Equals("2"))
            {
                return View("Index");
            }

            var listquestion = _context.Question.Where(q => q.Category == 2).Take(5);
            ViewBag.Result = "open";
            ViewBag.checking = 1;

            HttpContext.Session.SetString("exam", "3");

            return View(listquestion);
        }
        [HttpPost]
        public IActionResult MathExam(IFormCollection form, string submit)
        {
            string aswn1 = form["answer1"];
            string aswn2 = form["answer2"];
            string aswn3 = form["answer3"];
            string aswn4 = form["answer4"];
            string aswn5 = form["answer5"];
            string FinalAnswer = aswn1 + "//" + aswn2 + "//" + aswn3 + "//" + aswn4 + "//" + aswn5;
            string[] array1 = aswn1.Split('+');
            string[] array2 = aswn2.Split('+');
            string[] array3 = aswn3.Split('+');
            string[] array4 = aswn4.Split('+');
            string[] array5 = aswn5.Split('+');

            var listquestion = _context.Question.ToList();

            //check answer
            int Point = 0;
            if (!String.IsNullOrEmpty(aswn1))
            {
                var question1 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array1[1])));
                if (question1.FinalAnswer.Equals(array1[0]))
                {
                    Point += 10;
                }
            }

            if (!String.IsNullOrEmpty(aswn2))
            {
                var question2 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array2[1])));
                if (question2.FinalAnswer.Equals(array2[0]))
                {
                    Point += 10;
                }
            }


            if (!String.IsNullOrEmpty(aswn3))
            {
                var question3 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array3[1])));
                if (question3.FinalAnswer.Equals(array3[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn4))
            {
                var question4 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array4[1])));
                if (question4.FinalAnswer.Equals(array4[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn5))
            {
                var question5 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array5[1])));
                if (question5.FinalAnswer.Equals(array5[0]))
                {
                    Point += 40;
                }
            }

            //save record
            var general = new Exam();
            general.Mark = Point;
            if (Point <= 50)
                general.Result = "Failed";
            else
                general.Result = "Passed";
            general.Date = DateTime.Now;
            general.Answer = FinalAnswer;

            _context.Exam.Add(general);
            _context.SaveChanges();
            //end save record

            ViewBag.Point = Point;
            ViewBag.Result = "done";
            ViewBag.checking = 2;
            HttpContext.Session.SetString("MathPoints", Point.ToString());
            return View(listquestion);
        }
        [HttpGet]
        public IActionResult ComputerExam()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("exam")))
            {
                return View("Homepage", "Home");
            }
            if (!HttpContext.Session.GetString("exam").Equals("3"))
            {
                return View("Index");
            }

            var listquestion = _context.Question.Where(q => q.Category == 3).Take(5);
            ViewBag.Result = "open";
            ViewBag.checking = 1;

            HttpContext.Session.SetString("exam", "4");
            return View(listquestion);
        }
        [HttpPost]
        public IActionResult ComputerExam(IFormCollection form, string submit)
        {
            string aswn1 = "";
            if (string.IsNullOrEmpty(form["answer1"]))
                aswn1 = "t+1";
            else
                aswn1 = form["answer1"];
            string aswn2 = "";
            if (string.IsNullOrEmpty(form["answer2"]))
                aswn2 = "t+1";
            else
                aswn2 = form["answer2"];
            string aswn3 = "";
            if (string.IsNullOrEmpty(form["answer3"]))
                aswn3 = "t+1";
            else
                aswn3 = form["answer3"];
            string aswn4 = "";
            if (string.IsNullOrEmpty(form["answer4"]))
                aswn4 = "t+1";
            else
                aswn4 = form["answer4"];
            string aswn5 = "";
            if (string.IsNullOrEmpty(form["answer5"]))
                aswn5 = "t+1";
            else
                aswn5 = form["answer5"];
            //string aswn2 = form["answer2"];
            //string aswn3 = form["answer3"];
            //string aswn4 = form["answer4"];
            //string aswn5 = form["answer5"];
            string FinalAnswer = aswn1 + "//" + aswn2 + "//" + aswn3 + "//" + aswn4 + "//" + aswn5;
            string[] array1 = aswn1.Split('+');
            string[] array2 = aswn2.Split('+');
            string[] array3 = aswn3.Split('+');
            string[] array4 = aswn4.Split('+');
            string[] array5 = aswn5.Split('+');

            var listquestion = _context.Question.ToList();

            //check answer
            int Point = 0;
            if (!string.IsNullOrEmpty(aswn1))
            {
                var question1 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array1[1])));
                if (question1.FinalAnswer.Equals(array1[0]))
                {
                    Point += 10;
                }
            }

            if (!string.IsNullOrEmpty(aswn2))
            {
                var question2 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array2[1])));
                if (question2.FinalAnswer.Equals(array2[0]))
                {
                    Point += 10;
                }
            }


            if (!String.IsNullOrEmpty(aswn3))
            {
                var question3 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array3[1])));
                if (question3.FinalAnswer.Equals(array3[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn4))
            {
                var question4 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array4[1])));
                if (question4.FinalAnswer.Equals(array4[0]))
                {
                    Point += 20;
                }
            }


            if (!String.IsNullOrEmpty(aswn5))
            {
                var question5 = listquestion.SingleOrDefault(l => l.Id.Equals(int.Parse(array5[1])));
                if (question5.FinalAnswer.Equals(array5[0]))
                {
                    Point += 40;
                }
            }

            //save record
            var general = new Exam();
            general.Mark = Point;
            if (Point <= 50)
                general.Result = "Failed";
            else
                general.Result = "Passed";
            general.Date = DateTime.Now;
            general.Answer = FinalAnswer;

            _context.Exam.Add(general);
            _context.SaveChanges();
            //end save record


            ViewBag.Point = Point;
            ViewBag.Result = "done";
            ViewBag.checking = 2;

            HttpContext.Session.SetString("ComputerPoints", Point.ToString());
            return View(listquestion);
        }
        [HttpGet]
        public IActionResult ResultExam()
        {
            int generalPoint = int.Parse(HttpContext.Session.GetString("GeneralPoints"));
            int mathPoint = int.Parse(HttpContext.Session.GetString("MathPoints"));
            int computerPoint = int.Parse(HttpContext.Session.GetString("ComputerPoints"));
            ViewBag.general = generalPoint;
            ViewBag.math = mathPoint;
            ViewBag.computer = computerPoint;

            if (generalPoint <= 50 || mathPoint <= 50 || computerPoint <= 50)
            {
                ViewBag.resultPoint = "Failed";
            }
            else
            {
                ViewBag.resultPoint = "Passed";
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            _signInManager.SignOutAsync();
            return View("Homepage");
        }
    }
}
