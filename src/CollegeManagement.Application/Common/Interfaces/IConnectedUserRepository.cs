using CollegeManagement.Domain.ConnectedUser;

namespace CollegeManagement.Application.Common.Interfaces;

public interface IConnectedUserRepository
{
    Task<ConnectedUser?> GetConnectedUserAsync(string userId);
    
    Task UpdateAsync(ConnectedUser connectedUser);
}