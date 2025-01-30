using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid id,
    string name,
    string email,
    string city,
    UserRole role
    ) : IRequest<ErrorOr<Success>>;