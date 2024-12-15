using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand(
    string Title,
    string Description,
    int Hours,
    int Price
) : IRequest<ErrorOr<Course>>;