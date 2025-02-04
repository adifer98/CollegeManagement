using FluentValidation;

namespace CollegeManagement.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.name).MinimumLength(3)
            .MaximumLength(100).WithMessage("Name must be between 3 and 100 characters");
        
        RuleFor(x => x.email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.city).NotEmpty().WithMessage("City is required");
    }
}