using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using RzAssignment2.Models;
using static RzAssignment2.Models.Student;

namespace RzAssignment2.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseDbContext _context;

        public CoursesController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: /Course
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        //GET: /Course/Manage/1
        public async Task<IActionResult> Manage(int id)
        {
            var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var invitesSentCount = course.Students.Count();
            var confirmedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentConfirmed);
            var declinedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentDeclined);

            var viewModel = new CourseSummary
            {
                Course = course,
                InvitesSentCount = invitesSentCount,
                ConfirmedCount = confirmedCount,
                DeclinedCount = declinedCount
            };

            return View(viewModel);
        }



        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Instructor,StartDate,RoomNumber")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Instructor,StartDate,RoomNumber")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            // Add logic to send confirmation email
            var smtpClient = new SmtpClient("Rzinampan4187@conestogac.on.ca")
            {
                Port = 587,
                Credentials = new NetworkCredential("Rzinampan4187@conestogac.on.ca", "2268995507Tangan_"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-gmail-account@gmail.com"),
                Subject = "Enrollment Confirmation",
                Body = $"Dear student, please confirm your enrollment.",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(student.Email);

            // Send the email asynchronously
            await smtpClient.SendMailAsync(mailMessage);

            return RedirectToAction("ConfirmEnrollment");
        }
    }
}