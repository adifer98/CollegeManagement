namespace CollegeManagement.Contracts.Users;

public record UserResponse(Guid id, string name, UserRole role, string slug);