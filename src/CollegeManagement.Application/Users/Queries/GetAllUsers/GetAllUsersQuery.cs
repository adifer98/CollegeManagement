using CollegeManagement.Domain.Users;
using MediatR;
using ErrorOr;

namespace CollegeManagement.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<ErrorOr<List<User>>>;