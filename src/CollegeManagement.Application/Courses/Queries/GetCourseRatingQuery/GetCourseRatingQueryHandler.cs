using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Queries.GetCourseRatingQuery;

public class GetCourseRatingQueryHandler : IRequestHandler<GetCourseRatingQuery, ErrorOr<double>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IRatingsRepository _ratingsRepository;

    public GetCourseRatingQueryHandler(ICoursesRepository coursesRepository, IRatingsRepository ratingsRepository, IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _ratingsRepository = ratingsRepository;
    }

    public async Task<ErrorOr<double>> Handle(GetCourseRatingQuery request, CancellationToken cancellationToken)
    {
        var isCourseExists = await _coursesRepository.ExistsAsync(request.CourseId);

        if (!isCourseExists)
        {
            return Error.NotFound(description: "Course does not exist");
        }
        
        return await _ratingsRepository.GetCourseAverageRatingAsync(request.CourseId);
    }
}