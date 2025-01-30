using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(Guid CourseId, string Title, string Description, int Hours, int Price)
    : IRequest<ErrorOr<Success>>;