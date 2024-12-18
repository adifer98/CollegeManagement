using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Courses;
using CollegeManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Infrastructure.Courses;

public class CoursesRepository : ICoursesRepository
{
    private readonly CollegeManagementDbContext _dbContext;

    public CoursesRepository(CollegeManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCourseAsync(Course course)
    {
        await _dbContext.Courses.AddAsync(course);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var result = await GetByIdAsync(id);

        return result is not null;
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Courses.FirstOrDefaultAsync(course => course.Id == id);
    }

    public async Task<List<Course>> ListAsync()
    {
       return await _dbContext.Courses.ToListAsync();
    }

    public Task RemoveCourseAsync(Course course)
    {
        _dbContext.Remove(course);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Course course)
    {
        _dbContext.Update(course);
        
        return Task.CompletedTask;
    }
}