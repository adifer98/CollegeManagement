namespace CollegeManagement.Contracts.Enrollments;

public record EnrollentsResponse(IEnumerable<EnrollmentResponse> items);