
using CollegeManagement.Application.Users.Commands.CreateUser;
using CollegeManagement.Application.Users.Commands.DeleteUser;
using CollegeManagement.Application.Users.Commands.UpdateUser;
using CollegeManagement.Application.Users.Queries.GetAllUsers;
using CollegeManagement.Application.Users.Queries.GetUser;
using CollegeManagement.Contracts.Users;
using CollegeManagement.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainUserRole = CollegeManagement.Domain.Users.UserRole;
using UserRole = CollegeManagement.Contracts.Users.UserRole;

namespace CollegeManagement.Api.Controllers;

[Route("api/[controller]")]
public class UsersController : ApiController
{
    private readonly ISender _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ISender mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [Authorize("Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        _logger.LogInformation("Received a request to create a  user");
        if (!DomainUserRole.TryFromName(
            request.role.ToString(),
            out var role))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid user role"
            );
        }

        var createUserCommand = new CreateUserCommand(
            Name: request.name,
            Email: request.email,
            City: request.city,
            Role: role
        );

        var createUserResult = await _mediator.Send(createUserCommand);

        if (createUserResult.IsError)
        {
            return Problem(createUserResult.Errors);
        }
        
        var createUserResponse = new UserResponse(
            id: createUserResult.Value.Id,
            name: createUserResult.Value.Name,
            role: ToDto(createUserResult.Value.Role),
            slug: createUserResult.Value.Slug
        );

        return Ok(createUserResponse);
    }
    
    
    [HttpGet("{userIdOrSlug}")]
    public async Task<IActionResult> GetUser(string userIdOrSlug)
    {
        _logger.LogInformation("Received a request to get user");
        
        var getUserQuery = new GetUserQuery(userIdOrSlug);

        var getUserResult = await _mediator.Send(getUserQuery);

        return getUserResult.Match(
            user => Ok(new UserResponse(
                user.Id,
                user.Name,
                ToDto(user.Role),
                user.Slug)),
            Problem);
    }
    
    [HttpDelete("{userIdOrSlug}")]
    public async Task<IActionResult> DeleteUser(string userIdOrSlug)
    {
        var deleteUserCommand = new DeleteUserCommand(userIdOrSlug);

        var deleteUserResult = await _mediator.Send(deleteUserCommand);

        return deleteUserResult.Match<IActionResult>(
            _ => NoContent(),
            Problem
        );
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserRequest request)
    {
        _logger.LogInformation("Received a request to update user");
        
        if (!DomainUserRole.TryFromName(
                request.Role.ToString(),
                out var role))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid user role"
            );
        }
        
        var updateUserCommand = new UpdateUserCommand(
            id: userId,
            name: request.Name,
            email: request.Email,
            city: request.City,
            role: role
        );

        var updateUserResult = await _mediator.Send(updateUserCommand);

        return updateUserResult.Match<IActionResult>(
            _ => NoContent(),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        _logger.LogInformation("Received a request to get all users");
        
        var getAllUsersQuery = new GetAllUsersQuery();
        
        var getAllUsersResult = await _mediator.Send(getAllUsersQuery);

        return getAllUsersResult.Match(
            users => Ok(MapToUsersResponse(users)),
            Problem
        );

    }

    private static UserRole ToDto(DomainUserRole subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainUserRole.Student) => UserRole.Student,
            nameof(DomainUserRole.Admin) => UserRole.Admin,
            _ => throw new InvalidOperationException()
        };
    }

    private static UsersResponse MapToUsersResponse(List<User> users)
    {
        return new UsersResponse(
            items: users.Select(user => new UserResponse(
                id: user.Id,
                name: user.Name,
                role: ToDto(user.Role),
                slug: user.Slug
            ))
        );
    }
}