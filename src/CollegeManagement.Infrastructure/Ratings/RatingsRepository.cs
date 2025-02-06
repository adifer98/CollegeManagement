using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Ratings;
using CollegeManagement.Infrastructure.Common.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Infrastructure.Ratings;

public class RatingsRepository : IRatingsRepository
{
    private readonly CollegeManagementDbContext _context;

    public RatingsRepository(CollegeManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddRatingAsync(Rating rating)
    {
        await _context.Ratings.AddAsync(rating);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        
        return result != null;
    }

    public async Task<Rating?> GetByIdAsync(Guid id)
    {
        return await _context.Ratings.FirstOrDefaultAsync(rating => rating.Id == id);
    }

    public async Task<List<Rating>> ListAsync()
    {
        return await _context.Ratings.ToListAsync();
    }

    public Task RemoveRatingAsync(Rating rating)
    {
        _context.Remove(rating);

        return Task.CompletedTask;
    }

    public async Task<ErrorOr<double>> GetCourseAverageRatingAsync(Guid courseId)
    {
        var courseRatings = await _context.Ratings
            .Where(rating => rating.CourseId == courseId)
            .ToListAsync();

        if (courseRatings.Count == 0)
        {
            return Error.NotFound(description: "No ratings for this course.");
        }
        
        var averageRating = courseRatings.Average(r => r.Rate);
        
        return averageRating;
    }
}