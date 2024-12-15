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

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Courses.FirstOrDefaultAsync(course => course.Id == id);
    }

    public Task<List<Course>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveCourseAsync(Course course)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Course course)
    {
        throw new NotImplementedException();
    }
}