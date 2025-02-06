using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Ratings.Commands.CreateRating;

public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, ErrorOr<Rating>>
{
    private readonly IRatingsRepository _ratingsRepository;
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRatingCommandHandler(ICoursesRepository coursesRepository, IUnitOfWork unitOfWork, IRatingsRepository ratingsRepository)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
        _ratingsRepository = ratingsRepository;
    }

    public async Task<ErrorOr<Rating>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        //Check if course exists
        var isCourseExists = await _coursesRepository.ExistsAsync(request.CourseId);

        if (!isCourseExists)
        {
            return Error.NotFound(description: "Course not exists"); 
        }
        
        var rating = new Rating(request.CourseId, request.Rate);

        //Add rating to database
        await _ratingsRepository.AddRatingAsync(rating);
        await _unitOfWork.CommitChangesAsync();
        
        return rating;
    }
}