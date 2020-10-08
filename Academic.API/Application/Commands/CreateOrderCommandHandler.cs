using Academic.Domain.CourseAllocationAggregate;
using Academic.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Academic.API.Application.Commands
{
    public class CreateOrderCommandHandler
    {
        public class Command : IRequest<bool>
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public int? CourseGroupId { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private AcademicDbContext _academicDbContext;

            public Handler(AcademicDbContext academicDbContext)
            {
                _academicDbContext = academicDbContext;
            }

            public async Task<bool> Handle(
                Command request, 
                CancellationToken cancellationToken
                )
            {                
                Course course = new Course(request.Code, request.Name, request.CourseGroupId);
                _academicDbContext.Courses.Add(course);
                int count = await _academicDbContext.SaveChangesAsync();

                return count > 0;
            }
        }
    }
}
