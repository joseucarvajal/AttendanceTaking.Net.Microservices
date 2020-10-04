using System;
using System.Collections.Generic;
using System.Text;

namespace Academic.Domain.CourseAllocationAggregate
{
    public class CourseAllocation
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid AcademicPeriodId { get; set; }
        public AcademicPeriod AcademicPeriod { get; set; }
    }
}
