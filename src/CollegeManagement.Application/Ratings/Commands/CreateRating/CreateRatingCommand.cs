using CollegeManagement.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace CollegeManagement.Application.Ratings.Commands.CreateRating;

public record CreateRatingCommand(Guid CourseId, int Rate) : IRequest<ErrorOr<Rating>>;