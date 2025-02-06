using CollegeManagement.Domain.Ratings;
using ErrorOr;

namespace CollegeManagement.Application.Common.Interfaces;

public interface IRatingsRepository
{
    public Task AddRatingAsync(Rating rating);
    Task<bool> ExistsAsync(Guid id);
    Task<Rating?> GetByIdAsync(Guid id);
    Task<List<Rating>> ListAsync();
    Task RemoveRatingAsync(Rating rating);
    Task<ErrorOr<double>> GetCourseAverageRatingAsync(Guid courseId);
}