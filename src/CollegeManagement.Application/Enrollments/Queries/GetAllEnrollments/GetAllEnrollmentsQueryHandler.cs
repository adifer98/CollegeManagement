using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Domain.Enrollments;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Queries.GetAllEnrollments;

public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, ErrorOr<List<Enrollment>>>
{
    
    private readonly IEnrollmentsRepository _enrollmentsRepository;

    public GetAllEnrollmentsQueryHandler(IEnrollmentsRepository enrollmentsRepository)
    {
        _enrollmentsRepository = enrollmentsRepository;
    }
    
    public async Task<ErrorOr<List<Enrollment>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollmentsList = await _enrollmentsRepository.ListAsync();

        if (enrollmentsList.Count == 0)
        {
            return Error.NotFound(description: "There are no enrollments");
        }

        return enrollmentsList;
    }
}