using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetEnrollment;

public record GetEnrollmentQuery(Guid EnrollmentId) : IRequest<ErrorOr<Enrollment>>;