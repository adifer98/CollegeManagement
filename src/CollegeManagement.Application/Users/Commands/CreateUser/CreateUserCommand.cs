using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email, string City, UserRole Role) : IRequest<ErrorOr<User>>;