using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommandHandler
 : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteUserCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        

        return Result.Deleted;
    }
}
