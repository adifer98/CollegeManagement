using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler 
 : IRequestHandler<DeleteCourseCommand, ErrorOr<Deleted>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IEnrollmentsRepository _enrollmentsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(
        ICoursesRepository coursesRepository,
        IEnrollmentsRepository enrollmentsRepository,
        IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _enrollmentsRepository = enrollmentsRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {

        var course = await _coursesRepository.GetByIdAsync(request.CourseId);

        if (course is null)
        {
            return Error.NotFound(description: "Course not found");
        }

        await _coursesRepository.RemoveCourseAsync(course);
        await _enrollmentsRepository.RemoveEnrollmentsAsyncByCourseId(course.Id);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}
