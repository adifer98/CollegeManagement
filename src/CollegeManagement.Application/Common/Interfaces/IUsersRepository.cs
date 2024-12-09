using CollegeManagement.Domain.Users;

namespace CollegeManagement.Application.Commono.Interfaces;

public interface IUsersRepository
{
    public Task AddUserAsync(User user);
    Task<bool> ExistsAsync(Guid id);
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> ListAsync();
    Task RemoveUserAsync(User subscription);
    Task UpdateAsync(User user);
}