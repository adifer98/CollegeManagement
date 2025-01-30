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
    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        
        var user = new User(
            name: command.Name,
            email: command.Email,
            city: command.City,
            role: command.Role
        );

        await _usersRepository.AddUserAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return user;
    }
}
