using Academic.Domain.CourseAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academic.Infrastructure.EntityConfigurations
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> courseConfiguration)
        {
            //courseConfiguration.ToTable("courses", AcademicDbContext.DEFAULT_SCHEMA);
            courseConfiguration.ToTable("courses");

            courseConfiguration.HasKey(c => c.Id);

            courseConfiguration.Property(c => c.Code)
                .HasMaxLength(15)
                .IsRequired();

            courseConfiguration.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            courseConfiguration.Property<int?>("CourseGroupId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CourseGroupId")
                .IsRequired(false);
            courseConfiguration.HasOne(c => c.CourseGroup)
                .WithMany()
                .HasForeignKey("CourseGroupId");
        }
    }
}
