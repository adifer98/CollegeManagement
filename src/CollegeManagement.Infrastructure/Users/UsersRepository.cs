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
    
    public async Task<User?> GetBySlugAsync(string slug)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Slug == slug);
    }

    public async Task<List<User>> ListAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public Task RemoveUserAsync(User user)
    {
        _dbContext.Remove(user);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        _dbContext.Update(user);
        
        return Task.CompletedTask;
    }
}