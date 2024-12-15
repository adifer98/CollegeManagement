using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetCourse;

public class GetCourseQueryHandler
: IRequestHandler<GetCourseQuery, ErrorOr<Course>>
{
    private readonly ICoursesRepository _coursesRepository;
    
    public GetCourseQueryHandler(
        ICoursesRepository coursesRepository
    )
    {
        _coursesRepository = coursesRepository;
    }
    public async Task<ErrorOr<Course>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesRepository.GetByIdAsync(request.CourseId);

        return course is null
            ? Error.NotFound(description: "Course not found")
            : course;
    }
        
}
