using System.Data;
using CollegeManagement.Application.Courses.Commands.CreateCourse;
using CollegeManagement.Application.Courses.Commands.DeleteCourse;
using CollegeManagement.Application.Courses.Commands.UpdateCourse;
using CollegeManagement.Application.Courses.Queries.GetAllCoursesQuery;
using CollegeManagement.Application.Courses.Queries.GetCourse;
using CollegeManagement.Contracts.Courses;
using CollegeManagement.Domain.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Api.Controllers;

[Route("[controller]")]
public class CoursesController : ApiController
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
            return Problem(createCourseResult.Errors);
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
            Problem
        );
    }

    [HttpDelete("{courseId:Guid}")]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        var deleteCourseCommand = new DeleteCourseCommand(courseId);

        var deleteCourseResult = await _mediator.Send(deleteCourseCommand);

        return deleteCourseResult.Match<IActionResult>(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPut("{courseId:Guid}")]
    public async Task<IActionResult> UpdateCourse(Guid courseId, UpdateCourseRequest request)
    {
        var updateCourseCommand = new UpdateCourseCommand(
            CourseId: courseId,
            Title: request.title,
            Description: request.description,
            Hours: request.hours,
            Price: request.price
        );
        
        var updateCourseResult = await _mediator.Send(updateCourseCommand);

        return updateCourseResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var getAllCoursesQuery = new GetAllCoursesQuery();
        
        var getAllCoursesResult = await _mediator.Send(getAllCoursesQuery);

        return getAllCoursesResult.Match(
            courses => Ok(MapToCoursesResponse(courses)),
            Problem
        );
    }

    private CoursesResponse MapToCoursesResponse(List<Course> courses)
    {
        return new CoursesResponse(
            items: courses.Select(course => new CourseResponse(
                Id: course.Id,
                Title: course.Title
            ))
        );
    }
}