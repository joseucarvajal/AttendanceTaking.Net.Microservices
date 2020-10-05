using Academic.Domain.CourseAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
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

            await context.SaveChangesAsync();
        }
    }
}
