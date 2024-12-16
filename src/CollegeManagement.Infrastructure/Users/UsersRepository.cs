using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using CollegeManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ExistsAsync(Guid id)
    {
        var result = await GetByIdAsync(id);

        return result is not null;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
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