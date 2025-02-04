using FluentValidation;

namespace CollegeManagement.Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(2)
            .MaximumLength(100);
        
        RuleFor(x => x.Hours).GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}