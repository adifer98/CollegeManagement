using CollegeManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Enrollments.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, ErrorOr<Deleted>>
{
    private readonly IEnrollmentsRepository _enrollmentsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEnrollmentCommandHandler(IEnrollmentsRepository enrollmentsRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentsRepository = enrollmentsRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentsRepository.GetByIdAsync(request.enrollmentId);

        if (enrollment is null)
        {
            return Error.NotFound(description: "Enrollment not found");
        }
        
        await _enrollmentsRepository.RemoveEnrollmentAsync(enrollment);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}