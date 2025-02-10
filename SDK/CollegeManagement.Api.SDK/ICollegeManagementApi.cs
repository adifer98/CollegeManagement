using CollegeManagement.Contracts.Users;
using Refit;

namespace CollegeManagement.Api.SDK;

public interface ICollegeManagementApi
{
    [Get("/api/Users/{userIdOrSlug}")]
    Task<UserResponse> GetUserAsync(string userIdOrSlug);
}