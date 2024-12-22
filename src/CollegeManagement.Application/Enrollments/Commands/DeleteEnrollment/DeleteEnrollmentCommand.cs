using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Commands.DeleteEnrollment;

public record DeleteEnrollmentCommand(Guid enrollmentId) : IRequest<ErrorOr<Deleted>>;