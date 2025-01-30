
using FluentValidation;

namespace CollegeManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
    }
}