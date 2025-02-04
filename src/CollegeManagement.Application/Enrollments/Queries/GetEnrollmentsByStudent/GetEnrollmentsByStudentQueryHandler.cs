using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetEnrollmentsByStudent;

public class GetEnrollmentsByStudentQueryHandler : IRequestHandler<GetEnrollmentsByStudentQuery, ErrorOr<List<Enrollment>>>
{
    private readonly IEnrollmentsRepository _enrollmentsRepository;
    private readonly IUsersRepository _usersRepository;

    public GetEnrollmentsByStudentQueryHandler(
        IEnrollmentsRepository enrollmentsRepository,
        IUsersRepository usersRepository)
    {
        _enrollmentsRepository = enrollmentsRepository;
        _usersRepository = usersRepository;
    }
    
    public async Task<ErrorOr<List<Enrollment>>> Handle(GetEnrollmentsByStudentQuery request, CancellationToken cancellationToken)
    {
        var isUserExists = await _usersRepository.ExistsAsync(request.studentId);
        if (!isUserExists)
        {
            return Error.NotFound(description: "User not found");
        }

        var enrollmentsByStudent = await _enrollmentsRepository.ListByUserIdAsync(request.studentId);

        if (enrollmentsByStudent.Count == 0)
        {
            return Error.NotFound(description: "User got no enrollments");
        }
        
        return enrollmentsByStudent;
    }
}