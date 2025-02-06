namespace CollegeManagement.Contracts.Ratings;

public record RatingResponse(Guid Id, Guid CourseId, int Rate);