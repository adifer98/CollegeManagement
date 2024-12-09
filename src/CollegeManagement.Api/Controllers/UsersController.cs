using CollegeManagement.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        var response = new CreateUserResponse(id: Guid.NewGuid(), role: request.role);

        return Ok(response);
    }
}