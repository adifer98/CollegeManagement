using CollegeManagement.Application.Ratings.Commands.CreateRating;
using CollegeManagement.Contracts.Ratings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagement.Api.Controllers;

[Route("api/[controller]")]
public class RatingsController : ApiController
{
    private readonly ISender _mediator;
    private readonly ILogger<RatingsController> _logger;
    
    public RatingsController(ISender mediator, ILogger<RatingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRatings(CreateRatingRequest request)
    {
        _logger.LogInformation("Creating new Rating");
        
        var createRatingCommand = new CreateRatingCommand(
            CourseId: request.courseId,
            Rate: request.rate);
        
        var createRatingResult = await _mediator.Send(createRatingCommand);
        
        return createRatingResult.Match(
            rating => Ok(new RatingResponse(
                Id: rating.Id,
                CourseId: rating.CourseId,
                Rate: rating.Rate
            )), 
            Problem
        );
    }
}