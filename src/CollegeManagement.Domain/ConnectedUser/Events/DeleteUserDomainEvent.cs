using CollegeManagement.Domain.Common;

namespace CollegeManagement.Domain.ConnectedUser.Events;

public record DeleteUserDomainEvent(Guid deletedUserId) : IDomainEvent;