using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetCourse;

public record GetCourseQuery(Guid CourseId) : IRequest<ErrorOr<Course>>;