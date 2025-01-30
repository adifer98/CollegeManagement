using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Queries.GetUser;

public record GetUserQuery(string UserIdOrSlug) : IRequest<ErrorOr<User>>;