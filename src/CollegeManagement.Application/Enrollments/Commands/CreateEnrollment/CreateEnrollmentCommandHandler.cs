using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, ErrorOr<Enrollment>>
{
    private readonly IEnrollmentsRepository _enrollmentsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEnrollmentCommandHandler(
        IEnrollmentsRepository enrollmentsRepository, 
        IUsersRepository usersRepository,
        ICoursesRepository coursesRepository,
        IUnitOfWork unitOfWork
    )
    {
        _enrollmentsRepository = enrollmentsRepository;
        _usersRepository = usersRepository;
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Enrollment>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        // id check
        var isUserExists = await _usersRepository.ExistsAsync(request.UserId);
        if (!isUserExists)
        {
            return Error.NotFound(description: "User not found");
        }

        var isCourseExists = await _coursesRepository.ExistsAsync(request.CourseId);

        if (!isCourseExists)
        {
            return Error.NotFound(description: "Course not found");
        }

        var enrollment = new Enrollment(userId: request.UserId, courseId: request.CourseId);

        //add enrollment
        await _enrollmentsRepository.AddEnrollmentAsync(enrollment);
        //add to user's enrollments list


        await _unitOfWork.CommitChangesAsync();

        return enrollment;
    }
}