
using CollegeManagement.Application.Courses.Commands.CreateCourse;
using CollegeManagement.Application.Courses.Commands.DeleteCourse;
using CollegeManagement.Application.Courses.Commands.UpdateCourse;
using CollegeManagement.Application.Courses.Queries.GetAllCoursesQuery;
using CollegeManagement.Application.Courses.Queries.GetCourse;
using CollegeManagement.Application.Courses.Queries.GetCourseRatingQuery;
using CollegeManagement.Contracts.Courses;
using CollegeManagement.Domain.Courses;
using ErrorOr;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace CollegeManagement.Api.Controllers;


[Route("api/[controller]")]
public class CoursesController : ApiController
{
    private readonly ISender _mediator;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ISender mediator, ILogger<CoursesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [Authorize("Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(CourseResponse),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorType),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
        _logger.LogInformation("Creating new Course");
        
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
        _logger.LogInformation("Getting Course");
        
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

    [Authorize("Admin")]
    [HttpDelete("{courseId:Guid}")]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        _logger.LogInformation("Deleting Course");
        
        var deleteCourseCommand = new DeleteCourseCommand(courseId);

        var deleteCourseResult = await _mediator.Send(deleteCourseCommand);

        return deleteCourseResult.Match<IActionResult>(
            _ => NoContent(),
            Problem
        );
    }

    [Authorize("Admin")]
    [HttpPut("{courseId:Guid}")]
    public async Task<IActionResult> UpdateCourse(Guid courseId, UpdateCourseRequest request)
    {
        _logger.LogInformation("Updating Course");
        
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
        _logger.LogInformation("Getting all Courses");
        
        var getAllCoursesQuery = new GetAllCoursesQuery();
        
        var getAllCoursesResult = await _mediator.Send(getAllCoursesQuery);

        return getAllCoursesResult.Match(
            courses => Ok(MapToCoursesResponse(courses)),
            Problem
        );
    }

    [HttpGet("rating/{courseId:Guid}")]
    public async Task<IActionResult> GetCourseRating(Guid courseId)
    {
        _logger.LogInformation("Getting Course rating");

        var getCourseRatingQuery = new GetCourseRatingQuery(courseId);
        
        var getCourseRatingResult = await _mediator.Send(getCourseRatingQuery);

        return getCourseRatingResult.Match(
            rating => Ok(rating),
            Problem);
    }

    private static CoursesResponse MapToCoursesResponse(List<Course> courses)
    {
        return new CoursesResponse(
            items: courses.Select(course => new CourseResponse(
                Id: course.Id,
                Title: course.Title
            ))
        );
    }
}