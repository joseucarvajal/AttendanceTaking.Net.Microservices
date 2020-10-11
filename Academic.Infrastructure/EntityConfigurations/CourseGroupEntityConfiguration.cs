using Academic.Domain.CourseAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academic.Infrastructure.EntityConfigurations
{
    public class CourseGroupEntityConfiguration : IEntityTypeConfiguration<CourseGroup>
    {
        public void Configure(EntityTypeBuilder<CourseGroup> courseGroupConfiguration)
        {
            //courseGroupConfiguration.ToTable("coursegroup", AcademicDbContext.DEFAULT_SCHEMA);
            courseGroupConfiguration.ToTable("coursegroup");            

            courseGroupConfiguration.HasKey(g => g.Id);

            courseGroupConfiguration.Property(g => g.Id)
                .ValueGeneratedNever()
                .IsRequired();

            courseGroupConfiguration.Property(g => g.Name)
                .HasMaxLength(11)
                .IsRequired();
        }
    }
}
