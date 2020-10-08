using Academic.Domain.CourseAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Academic.Infrastructure
{
    public class AcademicDbSeed
    {
        public async Task SeedAsync(AcademicDbContext context, IServiceProvider services)
        {
            // Get a logger
            var logger = services.GetRequiredService<ILogger<AcademicDbSeed>>();

            context.Database.EnsureCreated();

            if (await context.CourseGroups.AnyAsync())
            {
                return;
            }

            context.CourseGroups.Add(CourseGroup.G1);
            context.CourseGroups.Add(CourseGroup.G2);
            context.CourseGroups.Add(CourseGroup.G3);
            context.CourseGroups.Add(CourseGroup.G4);

            context.Courses.Add(new Course { 
                Code = "C5303004", 
                Name = "Programación 2", 
                CourseGroup = CourseGroup.G2 });

            context.Courses.Add(new Course
            {
                Code = "C5404003",
                Name = "Programación 3",
                CourseGroup = CourseGroup.G3
            });

            await context.SaveChangesAsync();
        }
    }
}
