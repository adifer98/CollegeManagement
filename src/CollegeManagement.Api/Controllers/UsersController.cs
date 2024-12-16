using System.Reflection.Metadata.Ecma335;
using CollegeManagement.Application.Users.Commands.CreateUser;
using CollegeManagement.Application.Users.Queries.GetUser;
using CollegeManagement.Contracts.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainUserRole = CollegeManagement.Domain.Users.UserRole;

namespace CollegeManagement.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
       if (!DomainUserRole.TryFromName(
            request.role.ToString(),
            out var role))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid user role");
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
            Problem();
        }
        
        var createUserResponse = new UserResponse(
            id: createUserResult.Value.Id,
            name: createUserResult.Value.Name,
            role: ToDto(createUserResult.Value.Role)
        );

        return Ok(createUserResponse);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var getUserQuery = new GetUserQuery(userId);

        var getUserResult = await _mediator.Send(getUserQuery);

        return getUserResult.MatchFirst(
            user => Ok(new UserResponse(
                user.Id,
                user.Name,
                ToDto(user.Role))),
            error => Problem());
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        return Ok();
    }

    private static UserRole ToDto(DomainUserRole subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainUserRole.Student) => UserRole.Student,
            nameof(DomainUserRole.Admin) => UserRole.Admin,
            _ => throw new InvalidOperationException(),
        };
    }
}