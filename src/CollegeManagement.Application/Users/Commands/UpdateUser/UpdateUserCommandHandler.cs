using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.id);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        user.Update(
            name: request.name,
            email: request.email,
            city: request.city,
            role: request.role
        );
        
        await _usersRepository.UpdateAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}