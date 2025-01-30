using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, ErrorOr<List<Course>>>
{
    private readonly ICoursesRepository _coursesRepository;

    public GetAllCoursesQueryHandler(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }
    
    public async Task<ErrorOr<List<Course>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _coursesRepository.ListAsync();

        if (courses is null || courses.Count == 0)
        {
            return Error.NotFound("There are no courses available");
        }
        
        return courses;
    }
}