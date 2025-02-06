using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetCourseRatingQuery;

public record GetCourseRatingQuery(Guid CourseId) : IRequest<ErrorOr<double>>;