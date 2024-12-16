namespace CollegeManagement.Contracts.Enrollments;
public record CreateEnrollmentRequest(Guid UserId, Guid CourseId);