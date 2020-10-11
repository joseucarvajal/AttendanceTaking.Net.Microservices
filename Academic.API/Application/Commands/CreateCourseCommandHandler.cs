using Academic.Domain.CourseAllocationAggregate;
using Academic.Infrastructure;
using App.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Academic.API.Application.Commands
{
    public class CreateCourseCommandHandler
    {
        public class Command : IRequest<bool>
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public int? CourseGroupId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Code)
                    .NotNull().NotEmpty().WithMessage("El código es requerido")
                    .MaximumLength(15).WithMessage("El código no puede tener más de 15 caracteres");

                RuleFor(x => x.Name)
                    .NotNull().NotEmpty().WithMessage("El nombre es requerido")
                    .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");
            }
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
                string code = request.Code;
                Course existingCourse = await _academicDbContext
                                                .Courses
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync(c => c.Code == code);
                                                
                if (existingCourse != null)
                {
                    throw new AppException($"El curso con código {code} ya se encuentra registrado");
                }

                Course course = new Course(request.Code, request.Name, request.CourseGroupId);
                _academicDbContext.Courses.Add(course);
                int count = await _academicDbContext.SaveChangesAsync();

                return count > 0;
            }
        }
    }
}
