using CollegeManagement.Domain.Users;

namespace CollegeManagement.Application.Common.Interfaces;

public interface IUsersRepository
{
    public Task AddUserAsync(User user);
    Task<bool> ExistsAsync(Guid id);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetBySlugAsync(string slug);
    Task<List<User>> ListAsync();
    Task RemoveUserAsync(User user);
    Task UpdateAsync(User user);
}