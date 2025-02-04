using CollegeManagement.Application.Users.Commands.CreateUser;
using CollegeManagement.Domain.Users;
using CollegeManagement.TestsCommon.TestsConstants;

namespace CollegeManagement.TestsCommon.Users;

public class UserFactory
{
    public static User CreateUser(
        Guid? id = null,
        string? name = null, 
        string? email = null, 
        string? city= null, 
        UserRole? role = null)
    {
        return new User(
            id: id ?? UserConstants.ID,
            name: name ?? UserConstants.NAME,
            email: email ?? UserConstants.EMAIL,
            city: city ?? UserConstants.CITY,
            role: role ?? UserConstants.ROLE);
    }

    public static CreateUserCommand CreateCreateUserCommand(
        string? name = null,
        string? email = null,
        string? city = null,
        UserRole? role = null)
    {
        return new CreateUserCommand(
            Name: name ?? UserConstants.NAME,
            Email: email ?? UserConstants.EMAIL,
            City: city ?? UserConstants.CITY,
            Role: role ?? UserConstants.ROLE);
    }
    
}