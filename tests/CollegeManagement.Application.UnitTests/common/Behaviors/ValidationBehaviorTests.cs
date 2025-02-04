using CollegeManagement.Application.Common.Behaviors;
using CollegeManagement.Application.Users.Commands.CreateUser;
using CollegeManagement.Domain.Users;
using CollegeManagement.TestsCommon.Users;
using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace DefaultNamespace;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateUserCommand, ErrorOr<User>> _validationBehavior;
    private readonly IValidator<CreateUserCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<User>> _mockNextBehavior;

    public ValidationBehaviorTests()
    {
        _mockValidator = Substitute.For<IValidator<CreateUserCommand>>();
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<User>>>();
        _validationBehavior = new ValidationBehavior<CreateUserCommand, ErrorOr<User>>(_mockValidator);
    }
    
    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    {
        
        var user = UserFactory.CreateUser();
        _mockNextBehavior.Invoke().Returns(user);
        
        var createUserRequest = UserFactory.CreateCreateUserCommand();
        _mockValidator
            .ValidateAsync(createUserRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());
        
        var result = await _validationBehavior.Handle(
            createUserRequest,
            _mockNextBehavior,
            default);
        
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(user);
    }
    
    [Fact]
    public async Task InvokeBehavior_WhenValidatorResultIsInValid_ShouldReturnListOfErrors()
    {
        
        var createUserRequest = UserFactory.CreateCreateUserCommand();
        
        List<ValidationFailure> failures = [new("Foo", "Bad Foo")];
        
        _mockValidator
            .ValidateAsync(createUserRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(failures));
        
        var result = await _validationBehavior.Handle(
            createUserRequest,
            _mockNextBehavior,
            default);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("Foo");
        result.FirstError.Description.Should().Be("Bad Foo");
    }
}