using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Commands.CreateEnrollment;

public record CreateEnrollmentCommand(Guid UserId, Guid CourseId) : IRequest<ErrorOr<Enrollment>>;