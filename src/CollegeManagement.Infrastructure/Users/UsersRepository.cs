using CollegeManagement.Application.Commono.Interfaces;
using CollegeManagement.Domain.Users;
using GymManagement.Infrastructure.Common.Persistence;

namespace CollegeManagement.Infrastructure.Users;

public class UsersRepository : IUsersRepository
{

    private readonly CollegeManagementDbContext _dbContext;

    public UsersRepository(CollegeManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserAsync(User subscription)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}