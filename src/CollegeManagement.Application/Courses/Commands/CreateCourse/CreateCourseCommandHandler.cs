using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Courses;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Course>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCourseCommandHandler(
        ICoursesRepository coursesRepository,
        IUnitOfWork unitOfWork
    )
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Course>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course(
            title: request.Title,
            description: request.Description,
            hours: request.Hours,
            price: request.Price
        );

        await _coursesRepository.AddCourseAsync(course);
        await _unitOfWork.CommitChangesAsync();

        return course;
    }
}