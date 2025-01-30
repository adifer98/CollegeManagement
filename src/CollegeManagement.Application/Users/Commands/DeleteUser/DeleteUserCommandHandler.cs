using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommandHandler
 : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEnrollmentsRepository _enrollmentsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(
        IUsersRepository usersRepository,
        IEnrollmentsRepository enrollmentsRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _enrollmentsRepository = enrollmentsRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = Guid.TryParse(request.UserIdIrSlug, out var id)
            ? await _usersRepository.GetByIdAsync(id)
            : await _usersRepository.GetBySlugAsync(request.UserIdIrSlug);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        await _usersRepository.RemoveUserAsync(user);
        await _enrollmentsRepository.RemoveEnrollmentsAsyncByUserId(user.Id);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
