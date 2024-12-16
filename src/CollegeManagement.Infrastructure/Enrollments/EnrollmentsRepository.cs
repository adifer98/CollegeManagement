using System.Runtime.CompilerServices;
using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Enrollments;
using CollegeManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Infrastructure.Enrollments;

public class EnrollmentsRepository : IEnrollmentsRepository
{
    private readonly CollegeManagementDbContext _dbContext;

    public EnrollmentsRepository(CollegeManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddEnrollmentAsync(Enrollment enrollment)
    {
        await _dbContext.Enrollments.AddAsync(enrollment);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var result = await GetByIdAsync(id);

        return result is not null;
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Enrollments.FirstOrDefaultAsync(enrollment => enrollment.Id == id);
    }

    public Task<List<Enrollment>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveEnrollmentAsync(Enrollment enrollment)
    {
        _dbContext.Remove(enrollment);

        return Task.CompletedTask;
    }

    public async Task RemoveEnrollmentsAsyncByCourseId(Guid courseId)
    {
        var enrollmentsToDelete = await _dbContext.Enrollments
            .Where(e => e.CourseId == courseId)
            .ToListAsync();

        _dbContext.RemoveRange(enrollmentsToDelete);
    }

    public async Task RemoveEnrollmentsAsyncByUserId(Guid userId)
    {
        var enrollmentsToDelete = await _dbContext.Enrollments
            .Where(e => e.UserId == userId)
            .ToListAsync();

        _dbContext.RemoveRange(enrollmentsToDelete);
    }

    public Task UpdateAsync(Enrollment enrollment)
    {
        throw new NotImplementedException();
    }
}