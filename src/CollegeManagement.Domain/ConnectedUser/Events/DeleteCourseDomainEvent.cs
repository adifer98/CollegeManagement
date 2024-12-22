using CollegeManagement.Domain.Common;

namespace CollegeManagement.Domain.ConnectedUser.Events;

public record DeleteCourseDomainEvent(Guid deletedCourseId) : IDomainEvent;