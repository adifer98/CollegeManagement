using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid CourseId) : IRequest<ErrorOr<Deleted>>;