using Academic.Domain.CourseAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academic.Infrastructure.EntityConfigurations
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> courseConfiguration)
        {
            courseConfiguration.ToTable("courses", AcademicDbContext.DEFAULT_SCHEMA);
            courseConfiguration.HasKey(c => c.Id);

            courseConfiguration.Property(c => c.Code)
                .HasMaxLength(15)
                .IsRequired();

            courseConfiguration.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            courseConfiguration.Property<int>("_courseGroupId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CourseGroupId");                
        }
    }
}
