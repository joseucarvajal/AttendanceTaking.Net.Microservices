using App.Common.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academic.Domain.CourseAllocationAggregate
{
    public class CourseGroup : Enumeration
    {
        public static CourseGroup G1 = new CourseGroup(1, "Grupo 1");
        public static CourseGroup G2 = new CourseGroup(2, "Grupo 2");
        public static CourseGroup G3 = new CourseGroup(3, "Grupo 3");
        public static CourseGroup G4 = new CourseGroup(4, "Grupo 4");

        public CourseGroup(int id, string name)
                   : base(id, name)
        {
        }
    }
}
