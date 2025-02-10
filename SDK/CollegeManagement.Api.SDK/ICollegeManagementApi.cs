using CollegeManagement.Contracts.Users;
using Refit;

namespace CollegeManagement.Api.SDK;

[Headers("Authorization: Bearer")]
public interface ICollegeManagementApi
{
    [Get("/api/Users/{userIdOrSlug}")]
    Task<UserResponse> GetUserAsync(string userIdOrSlug);
    
    [Get("/api/Users")]
    Task<UsersResponse> GetAllUsersAsync();
}