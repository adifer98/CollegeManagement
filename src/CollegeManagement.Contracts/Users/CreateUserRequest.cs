namespace CollegeManagement.Contracts.Users;

public record CreateUserRequest(string name, string email, string city, UserRole role);