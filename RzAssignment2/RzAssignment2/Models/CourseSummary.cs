using System;
using System.Collections.Generic;
using RzAssignment2.Models;

namespace RzAssignment2.Models
{
    public class CourseSummary
    {
        public Course Course { get; set; }
        public int InvitesSentCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public Student Student { get; set; }
    }
}
