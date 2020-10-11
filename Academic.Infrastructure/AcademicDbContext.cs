using Academic.Domain.CourseAllocationAggregate;
using Academic.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academic.Infrastructure
{
    public class AcademicDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "";

        public AcademicDbContext(DbContextOptions<AcademicDbContext> options): base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CourseGroupEntityConfiguration());
        }
    }
}
