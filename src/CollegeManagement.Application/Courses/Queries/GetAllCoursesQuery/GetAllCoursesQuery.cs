using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetAllCoursesQuery;

public record GetAllCoursesQuery : IRequest<ErrorOr<List<Course>>>;