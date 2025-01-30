using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetAllEnrollments;

public record GetAllEnrollmentsQuery : IRequest<ErrorOr<List<Enrollment>>>;