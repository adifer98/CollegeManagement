using CollegeManagement.Application.Enrollments.Commands.DeleteEnrollment;
using CollegeManagement.Application.Enrollments.Queries.GetAllEnrollments;
using CollegeManagement.Domain.Enrollments;
using CollegeManagement.Application.Enrollments.Commands.CreateEnrollment;
using CollegeManagement.Application.Enrollments.Queries.GetEnrollment;
using CollegeManagement.Application.Enrollments.Queries.GetEnrollmentsByStudent;
using CollegeManagement.Contracts.Enrollments;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CollegeManagement.Api.Controllers;

[Route("api/[controller]")]
public class EnrollmentsController : ApiController
{
    private readonly ISender _mediator;
    private readonly ILogger<EnrollmentsController> _logger;
    public EnrollmentsController(ISender mediator, ILogger<EnrollmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequest request)
    {
        _logger.LogInformation("Received request to create enrollment");
        
        var createEnrollmentCommand = new CreateEnrollmentCommand(
            UserId: request.UserId,
            CourseId: request.CourseId
        );

        var createEnrollmentResult = await _mediator.Send(createEnrollmentCommand);

        return createEnrollmentResult.Match(
            enrollment => Ok(new EnrollmentResponse(
                enrollment.Id,
                enrollment.UserId,
                enrollment.CourseId
            )),
            Problem
        );
    }

    // [HttpGet("{enrollmentId:guid}")]
    // public async Task<IActionResult> GetEnrollment(Guid enrollmentId)
    // {
    //     var getEnrollmentQuery = new GetEnrollmentQuery(enrollmentId);
    //
    //     var getEnrollmentResult = await _mediator.Send(getEnrollmentQuery);
    //
    //     return getEnrollmentResult.Match(
    //         enrollment => Ok(new EnrollmentResponse(
    //             enrollment.Id,
    //             enrollment.UserId,
    //             enrollment.CourseId
    //         )),
    //         Problem
    //     );
    // }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEnrollments()
    {
        _logger.LogInformation("Received request to get all enrollments");
        
        var getAllEnrollmentsQuery = new GetAllEnrollmentsQuery();
        
        var getAllEnrollmentsResult = await _mediator.Send(getAllEnrollmentsQuery);

        return getAllEnrollmentsResult.Match(
            enrollments => Ok(MapToEnrollmentsResponse(enrollments)),
            Problem
        );
    }

    [HttpDelete("{enrollmentId:guid}")]
    public async Task<IActionResult> DeleteEnrollment(Guid enrollmentId)
    {
        _logger.LogInformation("Received request to delete enrollment with ID: {ID}", enrollmentId);
        
        var deleteEnrollmentCommand = new DeleteEnrollmentCommand(enrollmentId);
        
        var deleteEnrollmentResult = await _mediator.Send(deleteEnrollmentCommand);

        return deleteEnrollmentResult.Match(
            _ => NoContent(),
            Problem
        );
    }
    
    [HttpGet("{studentId:guid}")]
    public async Task<IActionResult> GetEnrollmentsByStudent(Guid studentId)
    {
        var getEnrollmentByStudentQuery = new GetEnrollmentsByStudentQuery(studentId);

        var getEnrollmentsByStudentResult = await _mediator.Send(getEnrollmentByStudentQuery);

        return getEnrollmentsByStudentResult.Match(
            enrollments => Ok(MapToEnrollmentsResponse(enrollments)),
            Problem
        );
    }

    private static EnrollmentsResponse MapToEnrollmentsResponse(List<Enrollment> enrollments)
    {
        return new EnrollmentsResponse(
            items: enrollments.Select(enrollment => new EnrollmentResponse(
                Id: enrollment.Id,
                UserId: enrollment.UserId,
                CourseId: enrollment.CourseId
            ))
        );
    }
}