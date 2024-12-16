using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetEnrollment;

public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, ErrorOr<Enrollment>>
{

    private readonly IEnrollmentsRepository _enrollmentsRepository;

    public GetEnrollmentQueryHandler(IEnrollmentsRepository enrollmentsRepository)
    {
        _enrollmentsRepository = enrollmentsRepository;
    }
    public async Task<ErrorOr<Enrollment>> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentsRepository.GetByIdAsync(request.EnrollmentId);

        return enrollment is null
            ? Error.NotFound(description: "Enrollment not found")
            : enrollment;
    }
}
