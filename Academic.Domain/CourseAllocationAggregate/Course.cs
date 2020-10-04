using App.Common.Repository;

namespace Academic.Domain.CourseAllocationAggregate
{
    public class Course : BaseEntity
    {        
        public string Code { get; set; }
        public string Name { get; set; }

        private int? _courseGroupId;
        public CourseGroup CourseGroup { get; private set; }
    }
}
