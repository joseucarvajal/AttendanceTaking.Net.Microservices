using App.Common.Repository;

namespace Academic.Domain.CourseAllocationAggregate
{
    public class Course : BaseEntity
    {        
        public string Code { get; set; }
        public string Name { get; set; }

        public int? CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }

        public Course() { }

        public Course(string code, string name, int? courseGroupId)
        {
            Code = code;
            Name = name;
            CourseGroupId = courseGroupId;
        }
    }
}
