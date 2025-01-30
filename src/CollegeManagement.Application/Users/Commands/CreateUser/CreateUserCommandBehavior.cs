using CollegeManagement.Domain.Users;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandBehavior : IPipelineBehavior<CreateUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(
        CreateUserCommand request,
        RequestHandlerDelegate<ErrorOr<User>> next,
        CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors
                .Select(error => Error.Validation(
                    code: error.PropertyName, 
                    description: error.ErrorMessage))
                .ToList();
        }
        return await next();
    }
}