namespace CollegeManagement.Api.Controllers;

using CollegeManagement.Application.Enrollments.Commands.CreateEnrollment;
using CollegeManagement.Application.Enrollments.Queries.GetEnrollment;
using CollegeManagement.Contracts.Enrollments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly ISender _mediator;

    public EnrollmentsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(CreateEnrollmentRequest request)
    {
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
            errors => Problem()
        );
    }

    [HttpGet("{enrollmentId:guid}")]
    public async Task<IActionResult> GetEnrollment(Guid enrollmentId)
    {
        var getEnrollmentQuery = new GetEnrollmentQuery(enrollmentId);

        var getEnrollmentResult = await _mediator.Send(getEnrollmentQuery);

        return getEnrollmentResult.Match(
            enrollment => Ok(new EnrollmentResponse(
                enrollment.Id,
                enrollment.UserId,
                enrollment.CourseId
            )),
            errors => Problem()
        );
    }
}