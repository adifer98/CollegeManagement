using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Infrastructure.Common.Persistence;

namespace CollegeManagement.Infrastructure.ConnectedUser;

public class ConnectedUserRepository : IConnectedUserRepository
{
    private readonly CollegeManagementDbContext _dbContext;

    public ConnectedUserRepository(CollegeManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.ConnectedUser.ConnectedUser?> GetConnectedUserAsync(string userId)
    {
        // return await _dbContext.ConnectedUser.First();
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.ConnectedUser.ConnectedUser connectedUser)
    {
        throw new NotImplementedException();
    }
}