namespace CollegeManagement.Domain.Ratings;

public class Rating
{
    public Guid Id { get; init; }
    
    public Guid CourseId { get; init; }
    
    public int Rate { get; init; }

    public Rating(
        Guid courseId,
        int rate)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        Rate = rate;
    }

    private Rating()
    {
    }
}