namespace RzAssignment2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public StudentStatus Status { get; set; }

        // Foreign key for the 1-to-many relationship
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public enum StudentStatus
        {
            ConfirmationMessageNotSent,
            ConfirmationMessageSent,
            EnrollmentConfirmed,
            EnrollmentDeclined
        }
    }
}
