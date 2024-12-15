using System.Reflection;
using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Courses;
using CollegeManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Infrastructure.Common.Persistence;

public class CollegeManagementDbContext(DbContextOptions options) :
    DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}