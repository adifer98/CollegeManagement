using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetEnrollmentsByStudent;

public record GetEnrollmentsByStudentQuery(Guid studentId) : IRequest<ErrorOr<List<Enrollment>>>;