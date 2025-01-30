using CollegeManagement.Application.Common.Behaviors;
using CollegeManagement.Application.Users.Commands.CreateUser;
using CollegeManagement.Domain.Users;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            // options.AddBehavior<IPipelineBehavior<CreateUserCommand, ErrorOr<User>>, CreateUserCommandBehavior>();
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        
        return services;
    }
}