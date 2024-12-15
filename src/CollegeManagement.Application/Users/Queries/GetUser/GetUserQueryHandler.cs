using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);

        return user is null
            ? Error.NotFound(description: "User not found")
            : user;
    }
}
