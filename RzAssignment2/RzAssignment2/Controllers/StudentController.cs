using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using RzAssignment2.Models;

namespace RzAssignment2.Controllers
{
    public class StudentController : Controller
    {
        private readonly CourseDbContext _context;

        public StudentController(CourseDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CreateStudent")]
        public async Task<IActionResult> Create(int courseId, [Bind("Name,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.CourseId = courseId;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Course", new { id = courseId });
            }

            ViewBag.CourseId = courseId;
            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            // Add logic to send confirmation email (using your existing email sending logic)

            return RedirectToAction("Manage", "Course", new { id = student.CourseId });
        }

        public IActionResult ConfirmEnrollment()
        {
            return View();
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
