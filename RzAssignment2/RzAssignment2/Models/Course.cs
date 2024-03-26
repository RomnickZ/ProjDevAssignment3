using System.ComponentModel.DataAnnotations;

namespace RzAssignment2.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Instructor name is required.")]
        [MaxLength(100)]
        public string Instructor { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Room Number is required.")]
        public string RoomNumber { get; set; }

        public ICollection<Student> Students { get; set; }
        public int StudentCount => Students?.Count ?? 0;
    }
}
