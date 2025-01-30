using FluentValidation;

namespace CollegeManagement.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.name).MinimumLength(2)
            .MaximumLength(100).WithMessage("Name must be between 2 and 100 characters");
        
        RuleFor(x => x.email).NotEmpty().WithMessage("Email is required");
        
        RuleFor(x => x.city).NotEmpty().WithMessage("City is required");
    }
}