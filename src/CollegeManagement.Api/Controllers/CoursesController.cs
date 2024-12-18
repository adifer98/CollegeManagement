using CollegeManagement.Application.Courses.Commands.CreateCourse;
using CollegeManagement.Application.Courses.Commands.DeleteCourse;
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

    [HttpGet("{courseId:Guid}")]
    public async Task<IActionResult> GetCourse(Guid courseId)
    {
        var getCourseQuery = new GetCourseQuery(courseId);

        var getCourseResult = await _mediator.Send(getCourseQuery);

        return getCourseResult.Match(
            course => Ok(new CourseResponse(
                Id: course.Id,
                Title: course.Title
            )),
            errors => Problem()
        );
    }

    [HttpDelete("{courseId:Guid}")]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        var deleteCourseCommand = new DeleteCourseCommand(courseId);

        var deleteCourseResult = await _mediator.Send(deleteCourseCommand);

        return deleteCourseResult.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem()
        );
    }
}