using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class CollegeManagementDbContext(
    DbContextOptions options) :
    DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;

    public Task CommitChangesAsync()
    {
        throw new NotImplementedException();
    }
}