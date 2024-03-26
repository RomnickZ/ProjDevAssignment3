using Microsoft.EntityFrameworkCore;
using static RzAssignment2.Models.Student;

namespace RzAssignment2.Models
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed the database with sample data if empty
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Webtech",
                    Instructor = "Juan Luna",
                    StartDate = DateTime.Now.AddDays(7),
                    RoomNumber = "24B1"
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Bob Marley",
                    Email = "BobM@gmail.com",
                    Status = StudentStatus.ConfirmationMessageNotSent,
                    CourseId = 1
                }
            );

        }
    }
}
