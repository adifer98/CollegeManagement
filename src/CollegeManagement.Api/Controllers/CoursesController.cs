using CollegeManagement.Application.Courses.Commands.CreateCourse;
using CollegeManagement.Application.Courses.Queries.GetCourse;
using CollegeManagement.Contracts.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ISender _mediator;

    public CoursesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
        var createCourseCommand = new CreateCourseCommand(
            Title: request.Title,
            Description: request.Description,
            Hours: request.Hours,
            Price: request.Price
        );

        var createCourseResult = await _mediator.Send(createCourseCommand);

        if (createCourseResult.IsError)
        {
            return Problem();
        }

        var createCourseResponse = new CourseResponse(
            Id: createCourseResult.Value.Id,
            Title: createCourseResult.Value.Title
        );

        return Ok(createCourseResponse);
    }

    [HttpGet("{userId:Guid}")]
    public async Task<IActionResult> GetCourse(Guid userId)
    {
        var getCourseQuery = new GetCourseQuery(userId);

        var getCourseResult = await _mediator.Send(getCourseQuery);

        return getCourseResult.Match(
            course => Ok(new CourseResponse(
                Id: course.Id,
                Title: course.Title
            )),
            errors => Problem()
        );
    }
}