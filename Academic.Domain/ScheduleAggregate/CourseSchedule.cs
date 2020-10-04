using Academic.Domain.CourseAllocationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academic.Domain.ScheduleAggregate
{
    public class CourseSchedule
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int DayOfWeek { get; set; }
    }
}
