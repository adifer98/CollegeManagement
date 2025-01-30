using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(
    string UserIdIrSlug
) : IRequest<ErrorOr<Deleted>>;