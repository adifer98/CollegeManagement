using FluentValidation;

namespace CollegeManagement.Application.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("Course ID cannot be empty");
        
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID cannot be empty");
    }
}