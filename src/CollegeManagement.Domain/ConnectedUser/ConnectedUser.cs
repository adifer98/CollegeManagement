using CollegeManagement.Domain.Common;
using CollegeManagement.Domain.ConnectedUser.Events;
using CollegeManagement.Domain.Users;
using Throw;

namespace CollegeManagement.Domain.ConnectedUser;

public class ConnectedUser : Entity
{
    public Guid? userId { get; private set; } = null;
    public UserRole? userRole { get; private set; } = null;

    public void LogOutUser()
    {
        userId = null;
        userRole = null;
    }
    
    public void SetConnectedUser(User user)
    {
        userId = user.Id;
        userRole = user.Role;
    }

    public void DeleteUser(Guid deletedUserId)
    {
        deletedUserId.ThrowIfNull();
        
        if (userId == deletedUserId)
        {
            LogOutUser();
            AddDomainEvent(new DeleteUserDomainEvent(deletedUserId));
            return;
        }

        userRole.ThrowIfNull().IfNotEquals(UserRole.Admin);
        
        AddDomainEvent(new DeleteUserDomainEvent(deletedUserId));
    }
    
    public void DeleteCourse(Guid deletedCourseId)
    {
        deletedCourseId.ThrowIfNull();
        

        userRole.ThrowIfNull().IfNotEquals(UserRole.Admin);
        
        AddDomainEvent(new DeleteCourseDomainEvent(deletedCourseId));
    }

    private ConnectedUser()
    {
    }

}