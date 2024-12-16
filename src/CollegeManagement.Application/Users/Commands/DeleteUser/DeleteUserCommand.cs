using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(
    Guid UserId
) : IRequest<ErrorOr<Deleted>>;