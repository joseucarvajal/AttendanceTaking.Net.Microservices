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
    public class DeleteCourseCommandHandler
    {
        public class Command : IRequest<bool>
        {
            public Command(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id)
                    .NotNull().NotEmpty().WithMessage("El id es requerido");
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
                var course = _academicDbContext.Courses.Find(request.Id);
                if(course == null)
                {
                    throw new AppException($"The course {request.Id} does not exists");
                }
                _academicDbContext.Courses.Remove(course);
                return await _academicDbContext.SaveChangesAsync() > 0; 
            }
        }
    }
}
