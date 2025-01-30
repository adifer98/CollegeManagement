using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ErrorOr<Success>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseCommandHandler(
        ICoursesRepository coursesRepository,
        IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _coursesRepository.GetByIdAsync(request.CourseId);

        if (course is null)
        {
            return Error.NotFound(description: $"Course with id {request.CourseId} does not exist");
        }
        
        course.Update(
            title: request.Title,
            description: request.Description,
            hours: request.Hours,
            price : request.Price
        );
        
        await _coursesRepository.UpdateAsync(course);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Success;
    }

}