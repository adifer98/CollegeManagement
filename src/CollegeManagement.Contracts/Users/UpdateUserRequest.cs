namespace CollegeManagement.Contracts.Users;

public record UpdateUserRequest(string Name, string Email, string City, UserRole Role);