using CollegeManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeManagement.Infrastructure.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
        .ValueGeneratedNever();

        builder.Property(user => user.Role)
        .HasConversion(
            role => role.Name,
            value => UserRole.FromName(value, false)
        );
    }
}