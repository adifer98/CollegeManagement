namespace CollegeManagement.Contracts.Ratings;

public record CreateRatingRequest(Guid courseId, int rate);
