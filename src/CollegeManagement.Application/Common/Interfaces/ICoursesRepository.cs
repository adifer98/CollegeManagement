using CollegeManagement.Domain.Courses;

namespace CollegeManagement.Application.Common.Interfaces;

public interface ICoursesRepository
{
    public Task AddCourseAsync(Course course);
    Task<bool> ExistsAsync(Guid id);
    Task<Course?> GetByIdAsync(Guid id);
    Task<List<Course>> ListAsync();
    Task RemoveCourseAsync(Course course);
    Task UpdateAsync(Course course);
}