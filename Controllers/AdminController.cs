using Aptitude.Areas.Identity.Pages.Account;
using Aptitude.Data;
using Aptitude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Aptitude.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(ApplicationDbContext context, ILogger<RegisterModel> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            //The word c in viewbag mean Count
            //Total question including General Knowlegde + Math + Computer Technology
            var queslist = context.Question.Count();
            var genlist = context.Question.Where(q=>q.Category.Equals(1)).Count();
            var mathlist = context.Question.Where(q=>q.Category.Equals(2)).Count();
            var comlist = context.Question.Where(q=>q.Category.Equals(3)).Count();
            ViewBag.cQues = queslist;
            ViewBag.cGen = genlist;
            ViewBag.cMath = mathlist;
            ViewBag.cCom = comlist;
            //Total Department list
            var departlist = context.Department.Count();
            ViewBag.cDepart = departlist;
            //Total User(CV) list
            var userlist = context.CV.Count();
            ViewBag.cUser = userlist;
            //Total examination list 
            var examlist = context.Exam.Count();
            ViewBag.cExam = examlist;
            return View();
        }
        //List General question
        public IActionResult Question_General(int Search)
        {
            var model = context.Question.ToList();
            ViewBag.Depart = context.Department.ToList();
            ViewBag.Default = 0;
            ViewBag.Uri = HttpContext.Request.Path;
            //BEFORE SEARCH
            if (Search == 0)
            {
                return View(model);
            }
            //AFTER SEARCH
            else
            {
                var result = model.Where(q => q.DepartmentId.Equals(Search));
                return View(result);
            }

        }
        //List Math question
        public IActionResult Question_Math(int Search)
        {
            var model = context.Question.ToList();
            ViewBag.Depart = context.Department.ToList();
            ViewBag.Default = 0;
            //BEFORE SEARCH
            if (Search == 0)
            {
                return View(model);
            }
            //AFTER SEARCH
            else
            {
                var result = model.Where(q => q.DepartmentId.Equals(Search));
                return View(result);
            }
        }
        //List Computer Technology
        public IActionResult Question_Computer(int Search)
        {
            var model = context.Question.ToList();
            ViewBag.Depart = context.Department.ToList();
            ViewBag.Default = 0;
            //BEFORE SEARCH
            if (Search == 0)
            {
                return View(model);
            }
            //AFTER SEARCH
            else
            {
                var result = model.Where(q => q.DepartmentId.Equals(Search));
                return View(result);
            }
        }
        //Create Question
        [HttpGet]
        public IActionResult Question_Create()
        {
            TempData["depart"] = context.Department.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Question_Create(Question question, string Category, string Level, string Status)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int category = int.Parse(Category);
                    int level = int.Parse(Level);
                    int status = int.Parse(Status);
                    question.Category = category;
                    question.Level = level;
                    question.Status = status;

                    context.Question.Add(question);
                    context.SaveChanges();
                    return RedirectToAction("Question_General", "Admin");
                }
            }
            catch (Exception ex)
            {

                ViewBag.Msg = ex.Message;
            }
            return View();
        }
        //Update Question
        [HttpGet]
        public IActionResult Question_Update(int id)
        {
            var model = context.Question.SingleOrDefault(q => q.Id.Equals(id));
            ViewBag.depart = context.Department.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Question_Update(Question question, string Category, string Level, string Status, string DepartmentId)
        {
            try
            {
                var model = context.Question.SingleOrDefault(q => q.Id.Equals(question.Id));

                if (model != null)
                {
                    //Convert string to int for all the option 
                    int category = int.Parse(Category);
                    int level = int.Parse(Level);
                    int status = int.Parse(Status);
                    int depart = int.Parse(DepartmentId);
                    question.Category = category;
                    question.Level = level;
                    question.Status = status;
                    question.DepartmentId = depart;
                    //Start to update
                    model.QuestionName = question.QuestionName;
                    model.Answer1 = question.Answer1;
                    model.Answer2 = question.Answer2;
                    model.Answer3 = question.Answer3;
                    model.Answer4 = question.Answer4;
                    model.FinalAnswer = question.FinalAnswer;
                    model.Category = question.Category;
                    model.Level = question.Level;
                    model.Status = question.Status;
                    model.DepartmentId = question.DepartmentId;
                    context.SaveChanges();
                    return RedirectToAction("Question_General", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }
            return View();
        }
        //List Department
        public IActionResult Department_Index(int Search)
        {
            var model = context.Department.ToList();
            ViewBag.Depart = context.Department.ToList();
            ViewBag.Default = 0;
            //BEFORE SEARCH
            if (Search == 0)
            {
                return View(model);
            }
            //AFTER SEARCH
            else
            {
                var result = model.Where(d => d.Id.Equals(Search));
                return View(result);
            }
        }
        //Create Department
        [HttpGet]
        public IActionResult Department_Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Department_Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Department.Add(department);
                    context.SaveChanges();
                    return RedirectToAction("Department_Index", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }
            return View();
        }
        //Update Department
        [HttpGet]
        public IActionResult Department_Update(int id)
        {
            var model = context.Department.SingleOrDefault(d => d.Id.Equals(id));
            return View(model);
        }
        [HttpPost]
        public IActionResult Department_Update(Department department)
        {
            try
            {
                var model = context.Department.SingleOrDefault(d => d.Id.Equals(department.Id));
                if (model != null)
                {
                    model.Name = department.Name;
                    model.Requirement = department.Requirement;
                    model.Detail = department.Detail;
                    model.Location = department.Location;
                    model.Welfare = department.Welfare;
                    model.Salary = department.Salary;
                    context.SaveChanges();
                    return RedirectToAction("Department_Index", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }
            return View();
        }
        [HttpGet]
        // create user get
        public async Task<IActionResult> Create_User()
        {
            var model = context.CV.ToList();
            ViewBag.listuser = context.Users.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create_User(int ID)
        {
            try
            {
                var user = context.CV.Find(ID);// find CV user
                // add New user 
                if (user != null)
                {
                    bool x = await _roleManager.RoleExistsAsync("User");
                    if (!x)
                    {
                        var role = new IdentityRole();
                        role.Name = "User";
                        await _roleManager.CreateAsync(role);
                    }
                    string Password = Pass.Generate(20, 12);
                    var users = new IdentityUser { UserName = user.Email, Email = user.Email, EmailConfirmed = true };

                    var result = await _userManager.CreateAsync(users, Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(users, "User"); // add role
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(users);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = users.Id, code = code },
                            protocol: Request.Scheme);
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            // send mail
                            //SendEmail(user.Email, "Account activation was successful ", "Your account has been activated by admin <br />" + " Sign in name : " + user.Email + "<br /> Password: " + Password +
                            //  $"<br /> Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                            SendEmail(user.Email, "Account activation was successful ", $"<table style='-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%; width:650px !important;height: 100% !important;margin: 0 !important;padding: 0 !important;' margin: 0 !important;><div style='background - color: #f4f4f4; margin: 0 !important; padding: 0 !important;'><div style='display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'> We're thrilled to have you here! Get ready to dive into your new account. </div><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td bgcolor='#3CBEB2' align='center' style='-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;border-collapse: collapse !important;'><tr><td align='center' valign='top' style='padding: 40px 10px 40px 10px; -webkit-text-size-adjust: 100%;- ms - text - size - adjust: 100 %; '> </td></tr></table></td></tr><tr><td bgcolor='#3CBEB2' align='center' style='padding: 0px 10px 0px 10px;-webkit-text-size-adjust: 100%;- ms - text - size - adjust: 100 %; '><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;-webkit-text-size-adjust: 100%;- ms - text - size - adjust: 100 %; '><tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px; -webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><h1 style='font-size: 48px; font-weight: 400; margin: 2;'>Don't Leave!</h1> <img src=' https://img.icons8.com/clouds/100/000000/sad.png' width='125' height='120' style='display: block; border: 0px;' /></td></tr></table></td> </tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><tr><td bgcolor='#ffffff' align='left' style='padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;-webkit-text-size-adjust: 100%; - ms - text - size - adjust: 100 %; '><p style='margin: 0;'>We thank you for submitting your CV.<br /><br />We need to evaluate your qualifications in order to be able to review your application. A little test will be done directly to us to do just that.<br /> Click on the link below to go to your test.</p></td></tr><tr><td bgcolor='#ffffff' align='left' style='-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><tr><td bgcolor='#ffffff' align='center' style='padding: 20px 30px 60px 30px;-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;'><table border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;'><tr><td align='center' style='border-radius: 3px;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;' bgcolor='#3CBEB2'> Sign in name :{user.Email} <br/> Password: {Password} <br/><a href={HtmlEncoder.Default.Encode(callbackUrl)}>Take a Test</a></td></tr></table></td></tr></table></td></tr> <!-- COPY --><tr><td bgcolor='#ffffff' align='left' style='padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;'><p style='margin: 0;'>If you have any questions, just email - we're here to help.</p></td></tr><tr><td bgcolor='#ffffff' align='left' style='padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;-webkit-text-size-adjust: 100%;- ms - text - size - adjust: 100 %; '><p style='margin: 0;'>Thanks,<br>Aptitude Team</p></td></tr></table></td></tr</table></div> </table>");
                            //add  exam
                            Exam exam = new Exam();
                            exam.Mark = 0;
                            exam.ExamTime = null;
                            exam.Result = null;
                            context.Exam.Add(exam);
                            context.SaveChanges();
                            Exam exam1 = new Exam();
                            exam1.Mark = 0;
                            exam1.ExamTime = null;
                            exam1.Result = null;
                            context.Exam.Add(exam1);
                            context.SaveChanges();
                            Exam exam2 = new Exam();
                            exam2.Mark = 0;
                            exam2.ExamTime = null;
                            exam2.Result = null;
                            context.Exam.Add(exam2);
                            context.SaveChanges();
                            // update CV
                            //user.Pass = Password;
                            user.ExamId = exam.Id;
                            context.CV.Update(user);
                            context.SaveChanges();
                            return RedirectToAction("Create_User");
                        }
                    }
                    //}

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Create_User");
        }
        //function send mail
        public void SendEmail(string address, string subject, string message)
        {
            // Mail company and password
            string email = "webstercompany2020@gmail.com";
            string password = "swpfamdictygabub";
            // new NetworkCredential and Mail message
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            // using mail smtp
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            // manipulation of sending mail
            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
        //generate random passwords
        public static class Pass
        {

            //string that generates a password with special characters
            private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

            public static string Generate(int length, int numberOfNonAlphanumericCharacters)
            {
                // length of password generaters check
                if (length < 1 || length > 128)
                {
                    throw new ArgumentException(nameof(length));
                }
                // check    
                if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
                {
                    throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
                }

                //uses a random function that generates numeric characters
                using (var rng = RandomNumberGenerator.Create())
                {
                    var byteBuffer = new byte[length];

                    rng.GetBytes(byteBuffer);

                    var count = 0;
                    var characterBuffer = new char[length];

                    //randomly generated by characters
                    for (var iter = 0; iter < length; iter++)
                    {
                        var i = byteBuffer[iter] % 87;

                        if (i < 10)
                        {
                            characterBuffer[iter] = (char)('0' + i);
                        }
                        else if (i < 36)
                        {
                            characterBuffer[iter] = (char)('A' + i - 10);
                        }
                        else if (i < 62)
                        {
                            characterBuffer[iter] = (char)('a' + i - 36);
                        }
                        else
                        {
                            characterBuffer[iter] = Punctuations[i - 62];
                            count++;
                        }
                    }

                    if (count >= numberOfNonAlphanumericCharacters)
                    {
                        return new string(characterBuffer);
                    }

                    int j;
                    var rand = new Random();

                    for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                    {
                        int k;
                        do
                        {
                            k = rand.Next(0, length);
                        }
                        while (!char.IsLetterOrDigit(characterBuffer[k]));

                        characterBuffer[k] = Punctuations[rand.Next(0, Punctuations.Length)];
                    }

                    return new string(characterBuffer);
                }
            }
        }
        //Report - List Exam

        public IActionResult ReportExam(DateTime? start, DateTime? end)
        {
            var model = context.Exam.ToList();
            ViewBag.Depart = context.Department.ToList();
            if (start == null && end == null)
            {
                return View(model);
            }
            else
            {
                var result = model.Where(q => q.Date >= start && q.Date <= end);
                return View(result);
            }
        }

        //Report - List CV
        public IActionResult ReportCV(DateTime? search)
        {
            var model = context.CV.ToList();
            ViewBag.Depart = context.Department.ToList();
            //search - before
            if (search == null)
            {
                return View(model);
            }
            //search - after
            else
            {
                var result = model.Where(q => q.DateCV.Equals(search));
                return View(result);
            }
        }

        //Report - List Question
        public IActionResult ReportQuestion(int dep, Department department)
        {
            var model = context.Question.ToList();
            ViewBag.depart = context.Department.ToList();
            ViewBag.Default = 0;
            if (dep == 0)
            {
                return View(model);
            }
            else
            {
                var result = model.Where(d => d.Id.Equals(department.Id));
                return View(result);
            }


        }
    }
}
