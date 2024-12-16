using CollegeManagement.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeManagement.Infrastructure.Enrollments;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
        .ValueGeneratedNever();

        builder.Property(e => e.UserId)
        .ValueGeneratedNever();

        builder.Property(e => e.CourseId)
        .ValueGeneratedNever();
    }
}
