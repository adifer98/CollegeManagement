using CollegeManagement.Domain.Enrollments;

namespace CollegeManagement.Application.Common.Interfaces;

public interface IEnrollmentsRepository
{
    public Task AddEnrollmentAsync(Enrollment enrollment);
    Task<bool> ExistsAsync(Guid id);
    Task<Enrollment?> GetByIdAsync(Guid id);
    Task<List<Enrollment>> ListAsync();
    Task<List<Enrollment>> ListByUserIdAsync(Guid userId);
    Task<List<Enrollment>> ListByCourseIdAsync(Guid courseId);
    Task RemoveEnrollmentAsync(Enrollment enrollment);
    Task RemoveEnrollmentsAsyncByUserId(Guid userId);
    Task RemoveEnrollmentsAsyncByCourseId(Guid courseId);
    Task UpdateAsync(Enrollment enrollment);
}