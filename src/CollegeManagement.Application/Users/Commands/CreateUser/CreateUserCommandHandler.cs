using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            name: request.Name,
            email: request.Email,
            city: request.City,
            role: request.Role
        );

        await _usersRepository.AddUserAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return user;
    }
}
