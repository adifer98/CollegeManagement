using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<List<User>>>
{
    private readonly IUsersRepository _usersRepository;

    public GetAllUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersList = await _usersRepository.ListAsync();

        if (usersList.Count == 0)
        {
            return Error.NotFound(description: "There are no users");
        }

        return usersList;
    }
}